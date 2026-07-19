# MJpegCast

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

## Repository Structure

```text
├── MJpegCast.sln          # Visual Studio solution file
├── .gitignore             # Ignored compilation and IDE files (bin, obj, .vs)
└── MJpegCast/             # Main project directory
    ├── MJpegCast.csproj   # Project metadata & configurations
    ├── Program.cs         # Application entry point
    ├── MJpegCastServer.cs # HTTP streaming web server logic
    ├── Form1.cs           # Form logic & event handlers
    ├── Form1.Designer.cs  # Form UI setup code
    └── Form1.resx         # Form localized resource mappings
```

---

## Getting Started

### Prerequisites

- [Windows 10 / 11](https://www.microsoft.com/windows)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### Building the Project

To build and compile the application locally:

```bash
dotnet build
```

### Creating a Static Standalone Build

To compile a single-file, self-contained standalone executable (`MJpegCast.exe`) that runs on any Windows x64 computer without requiring the .NET Runtime to be pre-installed:

```bash
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:PublishReadyToRun=true --self-contained true
```

The compiled standalone executable will be generated at:
`MJpegCast/bin/Release/net8.0-windows/win-x64/publish/MJpegCast.exe`

---

## License & Credits

Developed by **Ajay Randhawa**.
