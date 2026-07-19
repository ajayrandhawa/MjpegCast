# MJpegCast

<img src="mjpegcast.gif" />

> A lightweight, zero-dependency C# desktop screen sharing server that streams high-performance MJPEG directly to local network web browsers.

MJpegCast is a minimal, borderless Windows desktop utility written in C# (.NET 8.0) that broadcasts your screen to local network devices using a multithreaded MJPEG encoder. Designed with a clean, modern slate-indigo wizard UI, it includes an automatic browser reconnection watchdog and publishes as a standalone self-contained executable.

---

## Key Features

- **High-Performance Streaming**: Streams screen frames using a multithreaded C# MJPEG encoder directly to local TCP sockets without relying on external utilities (like FFmpeg).
- **Setup Wizard Flow**: A user-friendly 3-step configuration flow:
  1. *Host Connection settings* (IP/Port selection).
  2. *Quality & Monitor settings* (Screen selection, Scale, FPS, JPEG quality presets).
  3. *Share Dashboard* (Live preview, client counts, active bandwidth in MB/s, viewer URL).
- **Watchdog Connection Protection**: An integrated client-side ping watchdog checks server responsiveness via a `/ping` route every 5 seconds, auto-reconnecting the stream if it stalls.
- **Sleek Custom Aesthetics**:
  - Borderless frameless window (`FormBorderStyle = None`) with native Windows drop shadow.
  - Interactive hover state animations on all navigation elements.

---

## License & Credits

Developed by **Ajay Randhawa**.
