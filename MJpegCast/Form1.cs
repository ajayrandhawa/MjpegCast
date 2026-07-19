using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MJpegCast
{
    public partial class Form1 : Form
    {
        private int _currentStep = 1;
        private bool _isStreaming = false;
        private MJpegCastServer? _server;
        private readonly List<Screen> _screensList = new();
        private Point _dragStartPoint = Point.Empty;

        // Custom window dropshadow creation params
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            
            // Custom window border drawing
            this.Paint += Form1_Paint;
            
            // Dragging handlers for the custom title bar
            titleBarPanel.MouseDown += TitleBar_MouseDown;
            titleBarPanel.MouseMove += TitleBar_MouseMove;
            lblTitle.MouseDown += TitleBar_MouseDown;
            lblTitle.MouseMove += TitleBar_MouseMove;
            lblSubtitle.MouseDown += TitleBar_MouseDown;
            lblSubtitle.MouseMove += TitleBar_MouseMove;

            // Custom border painting for Step Card Panels
            step1NetworkPanel.Paint += CardPanel_Paint;
            step2SourcePanel.Paint += CardPanel_Paint;
            step3SharingPanel.Paint += CardPanel_Paint;

            // Separators
            titleBarPanel.Paint += TitleBarPanel_Paint;
            footerPanel.Paint += FooterPanel_Paint;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            try
            {
                if (System.IO.File.Exists("icon.ico"))
                {
                    this.Icon = new Icon("icon.ico");
                }
                else if (System.IO.File.Exists("app.ico"))
                {
                    this.Icon = new Icon("app.ico");
                }
            }
            catch { }

            PopulateIPAddresses();
            PopulateScreens();

            // Hook quality profile events
            radioLow.CheckedChanged += QualityRadioButton_CheckedChanged;
            radioMedium.CheckedChanged += QualityRadioButton_CheckedChanged;
            radioHigh.CheckedChanged += QualityRadioButton_CheckedChanged;
            radioCustom.CheckedChanged += QualityRadioButton_CheckedChanged;

            scaleTrackBar.Scroll += (s, ev) => UpdateSliderLabels();
            fpsTrackBar.Scroll += (s, ev) => UpdateSliderLabels();
            qualityTrackBar.Scroll += (s, ev) => UpdateSliderLabels();

            // Setup custom title buttons
            btnClose.Click += (s, ev) => this.Close();
            btnMinimize.Click += (s, ev) => this.WindowState = FormWindowState.Minimized;

            // Footer navigation buttons
            nextButton.Click += NextButton_Click;
            backButton.Click += BackButton_Click;
            exitButton.Click += (s, ev) => this.Close();
            copyUrlButton.Click += CopyUrlButton_Click;

            // Interactive hover animations
            SetupButtonHoverEffects();

            // Initial UI state
            _currentStep = 1;
            QualityRadioButton_CheckedChanged(null, EventArgs.Empty);
            UpdateStepUi();
        }

        #region Title Bar Dragging
        private void TitleBar_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void TitleBar_MouseMove(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - _dragStartPoint.X;
                this.Top += e.Y - _dragStartPoint.Y;
            }
        }
        #endregion

        #region Custom Painting & Borders
        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            using (var pen = new Pen(Color.FromArgb(203, 213, 225), 1)) // Slate 300
            {
                e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }

        private void CardPanel_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is Panel panel)
            {
                using (var pen = new Pen(Color.FromArgb(226, 232, 240), 1)) // Slate 200
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
                }
            }
        }

        private void TitleBarPanel_Paint(object? sender, PaintEventArgs e)
        {
            using (var pen = new Pen(Color.FromArgb(241, 245, 249), 1)) // Slate 100
            {
                e.Graphics.DrawLine(pen, 0, titleBarPanel.Height - 1, titleBarPanel.Width, titleBarPanel.Height - 1);
            }
        }

        private void FooterPanel_Paint(object? sender, PaintEventArgs e)
        {
            using (var pen = new Pen(Color.FromArgb(241, 245, 249), 1)) // Slate 100
            {
                e.Graphics.DrawLine(pen, 0, 0, footerPanel.Width, 0);
            }
        }
        #endregion

        #region Button Hover Visual Feedback
        private void SetupButtonHoverEffects()
        {
            nextButton.MouseEnter += (s, ev) => {
                if (_currentStep == 3)
                {
                    nextButton.BackColor = _isStreaming ? Color.FromArgb(220, 38, 38) : Color.FromArgb(5, 150, 105);
                }
                else
                {
                    nextButton.BackColor = Color.FromArgb(67, 56, 202); // Darker Indigo
                }
            };
            nextButton.MouseLeave += (s, ev) => {
                if (_currentStep == 3)
                {
                    nextButton.BackColor = _isStreaming ? Color.FromArgb(239, 68, 68) : Color.FromArgb(16, 185, 129);
                }
                else
                {
                    nextButton.BackColor = Color.FromArgb(79, 70, 229); // Indigo 600
                }
            };

            copyUrlButton.MouseEnter += (s, ev) => copyUrlButton.BackColor = Color.FromArgb(226, 232, 240); // Slate 200
            copyUrlButton.MouseLeave += (s, ev) => copyUrlButton.BackColor = Color.FromArgb(241, 245, 249); // Slate 100
            
            backButton.MouseEnter += (s, ev) => backButton.BackColor = Color.FromArgb(226, 232, 240);
            backButton.MouseLeave += (s, ev) => backButton.BackColor = Color.FromArgb(241, 245, 249);
            
            exitButton.MouseEnter += (s, ev) => exitButton.BackColor = Color.FromArgb(226, 232, 240);
            exitButton.MouseLeave += (s, ev) => exitButton.BackColor = Color.FromArgb(241, 245, 249);
        }
        #endregion

        private void PopulateIPAddresses()
        {
            ipComboBox.Items.Clear();
            ipComboBox.Items.Add("0.0.0.0 (All Interfaces)");

            try
            {
                foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni.OperationalStatus == OperationalStatus.Up && 
                        ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    {
                        var ipProps = ni.GetIPProperties();
                        foreach (var addr in ipProps.UnicastAddresses)
                        {
                            if (addr.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                ipComboBox.Items.Add(addr.Address.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving network adapters: {ex.Message}", "Network Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            ipComboBox.Items.Add("127.0.0.1 (Loopback)");
            ipComboBox.SelectedIndex = ipComboBox.Items.Count > 1 ? 1 : 0;
        }

        private void PopulateScreens()
        {
            screenComboBox.Items.Clear();
            _screensList.Clear();

            var screens = Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                var scr = screens[i];
                _screensList.Add(scr);
                string text = $"Monitor {i + 1} - {(scr.Primary ? "[Primary] " : "")}{scr.Bounds.Width}x{scr.Bounds.Height}";
                screenComboBox.Items.Add(text);
            }

            if (screenComboBox.Items.Count > 0)
            {
                screenComboBox.SelectedIndex = 0;
            }
        }



        private void QualityRadioButton_CheckedChanged(object? sender, EventArgs e)
        {
            bool isCustom = radioCustom.Checked;
            
            scaleTrackBar.Enabled = isCustom;
            fpsTrackBar.Enabled = isCustom;
            qualityTrackBar.Enabled = isCustom;

            if (radioLow.Checked)
            {
                scaleTrackBar.Value = 50;
                fpsTrackBar.Value = 10;
                qualityTrackBar.Value = 35;
            }
            else if (radioMedium.Checked)
            {
                scaleTrackBar.Value = 75;
                fpsTrackBar.Value = 15;
                qualityTrackBar.Value = 60;
            }
            else if (radioHigh.Checked)
            {
                scaleTrackBar.Value = 100;
                fpsTrackBar.Value = 30;
                qualityTrackBar.Value = 80;
            }

            UpdateSliderLabels();
        }

        private void UpdateSliderLabels()
        {
            scaleSliderLabel.Text = $"Scale: {scaleTrackBar.Value}%";
            fpsSliderLabel.Text = $"Framerate: {fpsTrackBar.Value} FPS";
            qualitySliderLabel.Text = $"JPEG Quality: {qualityTrackBar.Value}%";
        }

        private void UpdateStepUi()
        {
            // Toggle panels visibility
            step1NetworkPanel.Visible = (_currentStep == 1);
            step2SourcePanel.Visible = (_currentStep == 2);
            step3SharingPanel.Visible = (_currentStep == 3);

            // Step config
            if (_currentStep == 1)
            {
                lblSubtitle.Text = "—  Step 1 of 3: Host Settings";
                backButton.Enabled = false;
                nextButton.Text = "Next";
                nextButton.BackColor = Color.FromArgb(79, 70, 229);
            }
            else if (_currentStep == 2)
            {
                lblSubtitle.Text = "—  Step 2 of 3: Source & Quality";
                backButton.Enabled = true;
                nextButton.Text = "Next";
                nextButton.BackColor = Color.FromArgb(79, 70, 229);
            }
            else if (_currentStep == 3)
            {
                lblSubtitle.Text = "—  Step 3 of 3: Stream Control";
                backButton.Enabled = !_isStreaming;
                
                if (_isStreaming)
                {
                    nextButton.Text = "Stop Sharing";
                    nextButton.BackColor = Color.FromArgb(239, 68, 68); // Crimson Red
                    statusIndicator.Text = "● STREAMING LIVE (MJPEG)";
                    statusIndicator.ForeColor = Color.FromArgb(34, 197, 94);
                }
                else
                {
                    nextButton.Text = "Start Sharing";
                    nextButton.BackColor = Color.FromArgb(16, 185, 129); // Emerald Green
                    statusIndicator.Text = "● Streaming Offline";
                    statusIndicator.ForeColor = Color.FromArgb(148, 163, 184);
                }
            }

            // Sync connection URL display on step 3
            if (_currentStep == 3)
            {
                string ipStr = ipComboBox.SelectedItem?.ToString() ?? "0.0.0.0";
                if (ipStr.Contains(" "))
                {
                    ipStr = ipStr.Split(' ')[0];
                }

                if (ipStr == "0.0.0.0")
                {
                    string displayIp = "127.0.0.1";
                    for (int i = 1; i < ipComboBox.Items.Count; i++)
                    {
                        string? possibleIp = ipComboBox.Items[i]?.ToString();
                        if (possibleIp != null && !possibleIp.Contains("Loopback") && !possibleIp.Contains("0.0.0.0"))
                        {
                            displayIp = possibleIp.Split(' ')[0];
                            break;
                        }
                    }
                    urlTextBox.Text = $"http://{displayIp}:{portNumeric.Value}";
                }
                else
                {
                    urlTextBox.Text = $"http://{ipStr}:{portNumeric.Value}";
                }
            }
        }

        private void NextButton_Click(object? sender, EventArgs e)
        {
            if (_currentStep == 1)
            {
                _currentStep = 2;
                UpdateStepUi();
            }
            else if (_currentStep == 2)
            {
                _currentStep = 3;
                UpdateStepUi();
            }
            else if (_currentStep == 3)
            {
                if (_isStreaming)
                {
                    StopStreaming();
                }
                else
                {
                    StartStreaming();
                }
            }
        }

        private void BackButton_Click(object? sender, EventArgs e)
        {
            if (_isStreaming) return; // Prevent navigation while streaming

            if (_currentStep > 1)
            {
                _currentStep--;
                UpdateStepUi();
            }
        }

        private void CopyUrlButton_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(urlTextBox.Text))
            {
                Clipboard.SetText(urlTextBox.Text);
                MessageBox.Show("Viewer link copied to clipboard!", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void StartStreaming()
        {
            if (_isStreaming) return;

            string ipStr = ipComboBox.SelectedItem?.ToString() ?? "0.0.0.0";
            if (ipStr.Contains(" "))
            {
                ipStr = ipStr.Split(' ')[0];
            }
            var ip = IPAddress.Parse(ipStr);
            int port = (int)portNumeric.Value;

            if (screenComboBox.SelectedIndex < 0 || screenComboBox.SelectedIndex >= _screensList.Count)
            {
                MessageBox.Show("Please select a valid monitor screen.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Screen screen = _screensList[screenComboBox.SelectedIndex];

            double scale = scaleTrackBar.Value / 100.0;
            int fps = fpsTrackBar.Value;
            int quality = qualityTrackBar.Value;

            try
            {
                _server = new MJpegCastServer(ip, port, screen, quality, scale, fps);
                
                _server.ClientCountChanged += Server_ClientCountChanged;
                _server.StatsUpdated += Server_StatsUpdated;
                _server.FrameCaptured += Server_FrameCaptured;

                _server.Start();

                _isStreaming = true;
                UpdateStepUi();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start streaming server:\n{ex.Message}", "Streaming Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StopStreaming();
            }
        }

        private void StopStreaming()
        {
            if (!_isStreaming) return;

            _isStreaming = false;

            if (_server != null)
            {
                _server.ClientCountChanged -= Server_ClientCountChanged;
                _server.StatsUpdated -= Server_StatsUpdated;
                _server.FrameCaptured -= Server_FrameCaptured;

                try
                {
                    _server.Stop();
                    _server.Dispose();
                }
                catch { }
                _server = null;
            }

            var oldImage = previewBox.Image;
            previewBox.Image = null;
            oldImage?.Dispose();

            lblClients.Text = "Active Viewers: 0";
            lblFps.Text = "Framerate: 0.0 FPS";
            lblBandwidth.Text = "Bandwidth: 0.00 MB/s";

            UpdateStepUi();
        }

        private void Server_ClientCountChanged(int count)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<int>(Server_ClientCountChanged), count);
                return;
            }
            lblClients.Text = $"Active Viewers: {count}";
        }

        private void Server_StatsUpdated(double fps, double bandwidthMb)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<double, double>(Server_StatsUpdated), fps, bandwidthMb);
                return;
            }
            lblFps.Text = $"Framerate: {fps:0.0} FPS";
            lblBandwidth.Text = $"Bandwidth: {bandwidthMb:0.00} MB/s";
        }

        private void Server_FrameCaptured(Bitmap frame)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<Bitmap>(Server_FrameCaptured), frame);
                return;
            }

            var oldImage = previewBox.Image;
            previewBox.Image = frame;
            oldImage?.Dispose();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopStreaming();
            base.OnFormClosing(e);
        }
    }
}
