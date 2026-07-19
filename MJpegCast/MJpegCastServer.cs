using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MJpegCast
{
    public class TcpClientConnection
    {
        public TcpClient Client { get; }
        public NetworkStream Stream { get; }
        public BlockingCollection<byte[]>? ChunkQueue { get; set; }

        public TcpClientConnection(TcpClient client, NetworkStream stream)
        {
            Client = client;
            Stream = stream;
        }
    }

    public class MJpegCastServer : IDisposable
    {
        private readonly IPAddress _ipAddress;
        private readonly int _port;
        private readonly Screen _targetScreen;
        private readonly int _jpegQuality;
        private readonly double _scale;
        private readonly int _targetFps;

        private TcpListener? _tcpListener;
        private CancellationTokenSource? _cts;
        private Task? _listenerTask;
        private Task? _captureTask;

        private readonly List<TcpClientConnection> _activeClients = new();
        private readonly object _clientsLock = new();

        private int _frameCount;
        private long _bytesSent;
        private System.Threading.Timer? _statsTimer;

        // Events for UI communication
        public event Action<int>? ClientCountChanged;
        public event Action<double, double>? StatsUpdated; // (FPS, MB/s)
        public event Action<Bitmap>? FrameCaptured; // For local preview

        public bool IsRunning { get; private set; }

        public MJpegCastServer(IPAddress ipAddress, int port, Screen targetScreen, int jpegQuality, double scale, int targetFps)
        {
            _ipAddress = ipAddress;
            _port = port;
            _targetScreen = targetScreen;
            _jpegQuality = Math.Clamp(jpegQuality, 10, 100);
            _scale = Math.Clamp(scale, 0.1, 1.0);
            _targetFps = Math.Clamp(targetFps, 1, 60);
        }

        public void Start()
        {
            if (IsRunning) return;

            _cts = new CancellationTokenSource();
            _tcpListener = new TcpListener(_ipAddress, _port);

            try
            {
                _tcpListener.Start();
            }
            catch (Exception ex)
            {
                _tcpListener = null;
                throw new InvalidOperationException($"Failed to start TCP listener on {_ipAddress}:{_port}.\nDetails: {ex.Message}", ex);
            }

            IsRunning = true;
            _frameCount = 0;
            _bytesSent = 0;

            _captureTask = Task.Run(() => CaptureLoopAsync(_cts.Token), _cts.Token);
            _listenerTask = Task.Run(() => ListenLoopAsync(_cts.Token), _cts.Token);
            _statsTimer = new System.Threading.Timer(CalculateStats, null, 1000, 1000);
        }

        public void Stop()
        {
            if (!IsRunning) return;

            IsRunning = false;
            _cts?.Cancel();

            try { _tcpListener?.Stop(); } catch { }

            _statsTimer?.Dispose();
            _statsTimer = null;

            lock (_clientsLock)
            {
                foreach (var client in _activeClients)
                {
                    try { client.Client.Close(); } catch { }
                    client.ChunkQueue?.Dispose();
                }
                _activeClients.Clear();
            }

            try
            {
                var tasksToWait = new List<Task>();
                if (_listenerTask != null) tasksToWait.Add(_listenerTask);
                if (_captureTask != null) tasksToWait.Add(_captureTask);
                if (tasksToWait.Count > 0)
                {
                    Task.WaitAll(tasksToWait.ToArray(), 2000);
                }
            }
            catch { }

            _cts?.Dispose();
            _cts = null;
            _tcpListener = null;

            ClientCountChanged?.Invoke(0);
        }

        private async Task ListenLoopAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested && _tcpListener != null)
            {
                try
                {
                    var tcpClient = await _tcpListener.AcceptTcpClientAsync(ct);
                    if (ct.IsCancellationRequested) break;
                    _ = Task.Run(() => HandleTcpClientAsync(tcpClient, ct), ct);
                }
                catch (Exception)
                {
                    if (ct.IsCancellationRequested) break;
                }
            }
        }

        private async Task HandleTcpClientAsync(TcpClient tcpClient, CancellationToken ct)
        {
            try
            {
                using (tcpClient)
                using (var stream = tcpClient.GetStream())
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, ct);
                    if (bytesRead == 0) return;

                    string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string[] lines = request.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                    if (lines.Length == 0) return;

                    string requestLine = lines[0];
                    string[] parts = requestLine.Split(' ');
                    if (parts.Length < 2) return;

                    string url = parts[1];
                    int queryIndex = url.IndexOf('?');
                    string path = queryIndex >= 0 ? url.Substring(0, queryIndex) : url;

                    if (path == "/" || path.Equals("/index.html", StringComparison.OrdinalIgnoreCase))
                    {
                        byte[] htmlBytes = Encoding.UTF8.GetBytes(GetViewerHtml());
                        byte[] header = Encoding.UTF8.GetBytes(
                            "HTTP/1.1 200 OK\r\n" +
                            "Content-Type: text/html; charset=utf-8\r\n" +
                            $"Content-Length: {htmlBytes.Length}\r\n" +
                            "Access-Control-Allow-Origin: *\r\n" +
                            "Connection: close\r\n\r\n"
                        );
                        await stream.WriteAsync(header, 0, header.Length, ct);
                        await stream.WriteAsync(htmlBytes, 0, htmlBytes.Length, ct);
                        await stream.FlushAsync(ct);
                    }
                    else if (path.Equals("/ping", StringComparison.OrdinalIgnoreCase))
                    {
                        byte[] pingBytes = Encoding.UTF8.GetBytes("pong");
                        byte[] header = Encoding.UTF8.GetBytes(
                            "HTTP/1.1 200 OK\r\n" +
                            "Content-Type: text/plain\r\n" +
                            $"Content-Length: {pingBytes.Length}\r\n" +
                            "Access-Control-Allow-Origin: *\r\n" +
                            "Connection: close\r\n\r\n"
                        );
                        await stream.WriteAsync(header, 0, header.Length, ct);
                        await stream.WriteAsync(pingBytes, 0, pingBytes.Length, ct);
                        await stream.FlushAsync(ct);
                    }
                    else if (path.Equals("/stream", StringComparison.OrdinalIgnoreCase))
                    {
                        var client = new TcpClientConnection(tcpClient, stream);
                        
                        lock (_clientsLock)
                        {
                            _activeClients.Add(client);
                        }
                        TriggerClientCountChanged();

                        client.ChunkQueue = new BlockingCollection<byte[]>(new ConcurrentQueue<byte[]>(), 10);
                        await StreamMjpegToClientAsync(client, ct);
                    }
                    else
                    {
                        byte[] error = Encoding.UTF8.GetBytes("Not Found");
                        byte[] header = Encoding.UTF8.GetBytes(
                            "HTTP/1.1 404 Not Found\r\n" +
                            "Content-Type: text/plain\r\n" +
                            $"Content-Length: {error.Length}\r\n" +
                            "Connection: close\r\n\r\n"
                        );
                        await stream.WriteAsync(header, 0, header.Length, ct);
                        await stream.WriteAsync(error, 0, error.Length, ct);
                        await stream.FlushAsync(ct);
                    }
                }
            }
            catch (Exception)
            {
                // Closed
            }
        }

        private async Task StreamMjpegToClientAsync(TcpClientConnection client, CancellationToken ct)
        {
            var stream = client.Stream;
            var queue = client.ChunkQueue;
            if (queue == null) return;

            try
            {
                byte[] initialHeader = Encoding.UTF8.GetBytes(
                    "HTTP/1.1 200 OK\r\n" +
                    "Content-Type: multipart/x-mixed-replace; boundary=--frame\r\n" +
                    "Cache-Control: no-cache, private\r\n" +
                    "Pragma: no-cache\r\n" +
                    "Expires: 0\r\n" +
                    "Connection: keep-alive\r\n" +
                    "Access-Control-Allow-Origin: *\r\n\r\n"
                );
                await stream.WriteAsync(initialHeader, 0, initialHeader.Length, ct);
                await stream.FlushAsync(ct);

                foreach (var frameBytes in queue.GetConsumingEnumerable(ct))
                {
                    byte[] header = Encoding.UTF8.GetBytes($"--frame\r\nContent-Type: image/jpeg\r\nContent-Length: {frameBytes.Length}\r\n\r\n");
                    
                    await stream.WriteAsync(header, 0, header.Length, ct);
                    await stream.WriteAsync(frameBytes, 0, frameBytes.Length, ct);
                    await stream.WriteAsync(Encoding.UTF8.GetBytes("\r\n"), 0, 2, ct);
                    await stream.FlushAsync(ct);

                    Interlocked.Add(ref _bytesSent, header.Length + frameBytes.Length + 2);
                }
            }
            catch (Exception)
            {
                // Client disconnected
            }
            finally
            {
                lock (_clientsLock)
                {
                    _activeClients.Remove(client);
                }
                queue.Dispose();
                client.ChunkQueue = null;
                TriggerClientCountChanged();
            }
        }

        private void BroadcastFrameBytes(byte[] frameBytes)
        {
            lock (_clientsLock)
            {
                foreach (var client in _activeClients)
                {
                    if (client.ChunkQueue != null)
                    {
                        while (client.ChunkQueue.Count >= 2)
                        {
                            client.ChunkQueue.TryTake(out _);
                        }
                        client.ChunkQueue.TryAdd(frameBytes);
                    }
                }
            }
        }

        private async Task CaptureLoopAsync(CancellationToken ct)
        {
            var interval = 1000 / _targetFps;
            var jpegCodec = GetEncoder(ImageFormat.Jpeg);
            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)_jpegQuality);

            var bounds = _targetScreen.Bounds;
            int destWidth = (int)(bounds.Width * _scale);
            int destHeight = (int)(bounds.Height * _scale);
            if (destWidth % 2 != 0) destWidth--;
            if (destHeight % 2 != 0) destHeight--;

            int localPreviewFrameCounter = 0;

            while (!ct.IsCancellationRequested)
            {
                var startTime = DateTime.UtcNow;

                try
                {
                    // 1. Capture screen area
                    using var screenBmp = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
                    using (var g = Graphics.FromImage(screenBmp))
                    {
                        g.CopyFromScreen(bounds.Left, bounds.Top, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
                        try { DrawMouseCursor(g, bounds); } catch { }
                    }

                    // 2. Scale
                    Bitmap frameBmp;
                    if (_scale < 1.0)
                    {
                        frameBmp = new Bitmap(destWidth, destHeight, PixelFormat.Format32bppArgb);
                        using var g = Graphics.FromImage(frameBmp);
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                        g.DrawImage(screenBmp, 0, 0, destWidth, destHeight);
                    }
                    else
                    {
                        frameBmp = new Bitmap(screenBmp);
                    }

                    // 3. Handle local preview (every frame in MJPEG mode)
                    localPreviewFrameCounter++;
                    if (localPreviewFrameCounter >= 1)
                    {
                        localPreviewFrameCounter = 0;
                        if (FrameCaptured != null)
                        {
                            int thumbWidth = 320;
                            int thumbHeight = (int)(bounds.Height * ((double)thumbWidth / bounds.Width));
                            var previewBmp = new Bitmap(thumbWidth, thumbHeight, PixelFormat.Format32bppArgb);
                            using (var g = Graphics.FromImage(previewBmp))
                            {
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                                g.DrawImage(frameBmp, 0, 0, thumbWidth, thumbHeight);
                            }
                            FrameCaptured.Invoke(previewBmp);
                        }
                    }

                    // 4. Process frame for clients (Compress to JPEG)
                    byte[] jpegBytes;
                    using (var ms = new MemoryStream())
                    {
                        if (jpegCodec != null)
                        {
                            frameBmp.Save(ms, jpegCodec, encoderParams);
                            jpegBytes = ms.ToArray();
                            BroadcastFrameBytes(jpegBytes);
                        }
                    }

                    frameBmp.Dispose();
                    Interlocked.Increment(ref _frameCount);
                }
                catch (Exception ex)
                {
                    try
                    {
                        System.IO.File.AppendAllText("capture_error.log", $"[{DateTime.UtcNow}] Capture loop error: {ex.Message}\n{ex.StackTrace}\n\n");
                    }
                    catch { }
                }

                var elapsed = (int)(DateTime.UtcNow - startTime).TotalMilliseconds;
                var sleepTime = Math.Max(1, interval - elapsed);

                try
                {
                    await Task.Delay(sleepTime, ct);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }
        }

        private static ImageCodecInfo? GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageEncoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid) return codec;
            }
            return null;
        }

        private void CalculateStats(object? state)
        {
            int frames = Interlocked.Exchange(ref _frameCount, 0);
            long bytes = Interlocked.Exchange(ref _bytesSent, 0);

            // FPS display calculations
            double fps = frames;
            if (double.IsNaN(fps) || double.IsInfinity(fps) || _activeClients.Count == 0)
            {
                fps = 0.0;
            }

            double mbSec = (double)bytes / (1024.0 * 1024.0);
            StatsUpdated?.Invoke(fps, mbSec);
        }

        private void TriggerClientCountChanged()
        {
            int count;
            lock (_clientsLock)
            {
                count = _activeClients.Count;
            }
            ClientCountChanged?.Invoke(count);
        }

        private string GetViewerHtml()
        {
            return GetMjpegPlayerHtml();
        }

        private string GetMjpegPlayerHtml()
        {
            return @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>MJpegCast Live Stream</title>
    <style>
        :root {
            --bg-color: #09090b;
            --card-bg: #18181b;
            --card-border: #27272a;
            --text-color: #fafafa;
            --text-muted: #a1a1aa;
            --accent-color: #6366f1;
            --accent-hover: #4f46e5;
            --success-color: #22c55e;
        }
        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }
        body {
            background-color: var(--bg-color);
            color: var(--text-color);
            font-family: -apple-system, BlinkMacSystemFont, ""Segoe UI"", Roboto, Helvetica, Arial, sans-serif;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            min-height: 100vh;
            overflow: hidden;
            padding: 20px;
        }
        .container {
            width: 100%;
            max-width: 1200px;
            display: flex;
            flex-direction: column;
            gap: 15px;
            height: 90vh;
        }
        header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 12px 24px;
            background: var(--card-bg);
            border-radius: 12px;
            border: 1px solid var(--card-border);
            box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1);
        }
        .title-group {
            display: flex;
            align-items: center;
            gap: 12px;
        }
        .pulse {
            width: 12px;
            height: 12px;
            background-color: var(--success-color);
            border-radius: 50%;
            box-shadow: 0 0 0 0 rgba(34, 197, 94, 0.7);
            animation: pulse-animation 2s infinite;
        }
        @keyframes pulse-animation {
            0% { transform: scale(0.95); box-shadow: 0 0 0 0 rgba(34, 197, 94, 0.7); }
            70% { transform: scale(1); box-shadow: 0 0 0 8px rgba(34, 197, 94, 0); }
            100% { transform: scale(0.95); box-shadow: 0 0 0 0 rgba(34, 197, 94, 0); }
        }
        h1 {
            font-size: 1.25rem;
            font-weight: 600;
            letter-spacing: -0.025em;
        }
        .status-badge {
            background: rgba(255, 255, 255, 0.08);
            padding: 4px 10px;
            border-radius: 6px;
            font-size: 0.75rem;
            font-weight: 500;
            color: var(--text-muted);
            border: 1px solid rgba(255, 255, 255, 0.05);
        }
        .stream-wrapper {
            flex-grow: 1;
            background-color: #020202;
            border-radius: 12px;
            overflow: hidden;
            display: flex;
            align-items: center;
            justify-content: center;
            position: relative;
            border: 1px solid var(--card-border);
            box-shadow: 0 20px 25px -5px rgba(0, 0, 0, 0.5);
        }
        #stream-img {
            max-width: 100%;
            max-height: 100%;
            object-fit: contain;
            user-select: none;
            -webkit-user-drag: none;
        }
        #stream-img.actual {
            max-width: none;
            max-height: none;
            object-fit: none;
        }
        #stream-img.cover {
            width: 100%;
            height: 100%;
            object-fit: cover;
        }
        .controls {
            position: absolute;
            bottom: 24px;
            left: 50%;
            transform: translateX(-50%);
            display: flex;
            gap: 8px;
            background: rgba(9, 9, 11, 0.85);
            padding: 6px 12px;
            border-radius: 9999px;
            backdrop-filter: blur(8px);
            border: 1px solid var(--card-border);
            opacity: 0;
            transition: opacity 0.2s ease;
        }
        .stream-wrapper:hover .controls, .controls:hover {
            opacity: 1;
        }
        .btn {
            background: transparent;
            border: none;
            color: var(--text-color);
            cursor: pointer;
            padding: 8px 14px;
            border-radius: 9999px;
            font-size: 0.875rem;
            font-weight: 500;
            transition: background-color 0.15s;
        }
        .btn:hover { background: rgba(255, 255, 255, 0.1); }
        .btn.active { background: var(--accent-color); color: white; }
        .error-overlay {
            position: absolute;
            top: 0; left: 0; right: 0; bottom: 0;
            background: rgba(9, 9, 11, 0.9);
            display: none;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            gap: 16px;
            z-index: 10;
        }
        .watermark {
            position: absolute;
            bottom: 16px;
            right: 16px;
            background: rgba(9, 9, 11, 0.55);
            color: rgba(250, 250, 250, 0.45);
            font-size: 0.75rem;
            font-weight: 600;
            padding: 5px 10px;
            border-radius: 6px;
            letter-spacing: 0.05em;
            pointer-events: none;
            backdrop-filter: blur(4px);
            border: 1px solid rgba(255, 255, 255, 0.05);
            user-select: none;
            z-index: 5;
        }
    </style>
</head>
<body>
    <div class=""container"">
        <header>
            <div class=""title-group"">
                <div class=""pulse""></div>
                <h1>MJpegCast Live Stream</h1>
                <span class=""status-badge"">MJPEG Playback</span>
            </div>
            <div style=""font-size: 0.875rem; color: var(--text-muted);"">
                Direct browser capture stream
            </div>
        </header>
        <div class=""stream-wrapper"" id=""wrapper"">
            <img id=""stream-img"" src=""/stream"" alt=""Live Screen Stream"">
            <div class=""watermark"">MJpegCast | Ajay Randhawa</div>
            
            <div class=""error-overlay"" id=""error-screen"">
                <div style=""font-size: 1.25rem; font-weight: 600;"">Stream Disconnected</div>
                <div style=""color: var(--text-muted); font-size: 0.875rem;"">Connecting to sharing server...</div>
                <button class=""btn active"" onclick=""reconnect()"" style=""margin-top: 8px;"">Reconnect Now</button>
            </div>

            <div class=""controls"">
                <button class=""btn active"" id=""btn-fit"" onclick=""setFit('fit')"">Fit Screen</button>
                <button class=""btn"" id=""btn-actual"" onclick=""setFit('actual')"">Original Size</button>
                <button class=""btn"" id=""btn-cover"" onclick=""setFit('cover')"">Fill Screen</button>
                <button class=""btn"" onclick=""toggleFullscreen()"">Fullscreen</button>
            </div>
        </div>
    </div>
    <script>
        const img = document.getElementById('stream-img');
        const wrapper = document.getElementById('wrapper');
        const errorScreen = document.getElementById('error-screen');
        const btnFit = document.getElementById('btn-fit');
        const btnActual = document.getElementById('btn-actual');
        const btnCover = document.getElementById('btn-cover');
        let isReconnecting = false;
        let pingInterval = null;

        function setFit(mode) {
            img.className = '';
            btnFit.classList.remove('active');
            btnActual.classList.remove('active');
            btnCover.classList.remove('active');

            if (mode === 'actual') {
                img.classList.add('actual');
                btnActual.classList.add('active');
            } else if (mode === 'cover') {
                img.classList.add('cover');
                btnCover.classList.add('active');
            } else {
                btnFit.classList.add('active');
            }
        }

        function toggleFullscreen() {
            if (!document.fullscreenElement) {
                wrapper.requestFullscreen();
            } else {
                document.exitFullscreen();
            }
        }

        function reconnect() {
            isReconnecting = true;
            img.src = '/stream?t=' + new Date().getTime();
        }

        img.onload = function() {
            isReconnecting = false;
            errorScreen.style.display = 'none';
        };

        img.onerror = function() {
            errorScreen.style.display = 'flex';
            if (!isReconnecting) {
                setTimeout(reconnect, 3000);
            }
        };

        function startPingWatchdog() {
            if (pingInterval) clearInterval(pingInterval);
            pingInterval = setInterval(async () => {
                try {
                    const controller = new AbortController();
                    const timeoutId = setTimeout(() => controller.abort(), 2000);
                    const res = await fetch('/ping', { cache: 'no-store', signal: controller.signal });
                    clearTimeout(timeoutId);
                    if (!res.ok) throw new Error();
                } catch (err) {
                    console.log('Server unreachable, attempting to reconnect stream...');
                    errorScreen.style.display = 'flex';
                    if (!isReconnecting) {
                        reconnect();
                    }
                }
            }, 5000);
        }

        // Start connection watchdog on load
        startPingWatchdog();
    </script>
</body>
</html>";
        }

        #region Win32 Cursor Capture API

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT { public int x; public int y; }

        [StructLayout(LayoutKind.Sequential)]
        private struct CURSORINFO { public int cbSize; public int flags; public IntPtr hCursor; public POINT ptScreenPos; }

        [StructLayout(LayoutKind.Sequential)]
        private struct ICONINFO { public bool fIcon; public int xHotspot; public int yHotspot; public IntPtr hbmMask; public IntPtr hbmColor; }

        [DllImport("user32.dll", EntryPoint = "GetCursorInfo")]
        private static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll", EntryPoint = "GetIconInfo")]
        private static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

        [DllImport("user32.dll", EntryPoint = "DrawIcon")]
        private static extern bool DrawIcon(IntPtr hDC, int x, int y, IntPtr hIcon);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        private static extern bool DeleteObject(IntPtr hObject);

        private const int CURSOR_SHOWING = 0x00000001;

        private void DrawMouseCursor(Graphics g, Rectangle bounds)
        {
            var pci = new CURSORINFO { cbSize = Marshal.SizeOf(typeof(CURSORINFO)) };
            if (GetCursorInfo(out pci) && pci.flags == CURSOR_SHOWING)
            {
                if (GetIconInfo(pci.hCursor, out var iconInfo))
                {
                    int cursorX = pci.ptScreenPos.x - bounds.Left - iconInfo.xHotspot;
                    int cursorY = pci.ptScreenPos.y - bounds.Top - iconInfo.yHotspot;

                    IntPtr hdc = g.GetHdc();
                    try { DrawIcon(hdc, cursorX, cursorY, pci.hCursor); }
                    finally { g.ReleaseHdc(hdc); }

                    if (iconInfo.hbmMask != IntPtr.Zero) DeleteObject(iconInfo.hbmMask);
                    if (iconInfo.hbmColor != IntPtr.Zero) DeleteObject(iconInfo.hbmColor);
                }
            }
        }

        #endregion

        public void Dispose() => Stop();
    }
}