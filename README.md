# Quick Cut - PC Encoder

PC-side encoder companion for the [Quick Cut](https://play.google.com/store/apps/details?id=com.kch.quickcut) Android app.

Plan your video edits on your phone — cut, crop, and rotate — then let your PC handle the encoding automatically via SMB.

## How It Works

1. Open a video in the Quick Cut Android app
2. Mark cut points, set crop and rotation
3. Save the edit plan → transferred to PC via SMB
4. PC Encoder detects the file and encodes automatically

For SMB connection setup, refer to the [Setup Guide](https://changhyeon0352.github.io/quickcutNetworkSettingsGuide/setup-guide.html).

## Requirements

- Windows 10 or later
- Quick Cut Android app
- Both devices on the same Wi-Fi network

## Installation

1. Download `QuickCut.zip` from [Releases](https://github.com/changhyeon0352/QuickCut-Pc-Encoder/releases)
2. Extract the ZIP file
3. Run `VideoCutMarkerEncoder.exe`

> ⚠️ Windows may show an "Unknown Publisher" warning — this is expected for apps without a code signing certificate. Click **"More info"** → **"Run anyway"** to proceed.

## Features

- Automatic video encoding from SMB share
- Supports cutting, cropping, and rotation
- Multiple codec support (H.264, H.265 / CPU, NVIDIA, AMD)
- System tray application with auto-start support
- Separate and Merge output modes

## Security

The SHA256 hash shown in the Releases page matches the hash in the VirusTotal results below, confirming that the file has not been modified.

- SHA256: `fe4ed3e638986a25db05fa3827a497f9a2545938f3c697c88da2f2e6009f70b0`
- [VirusTotal scan results (0/66)](https://www.virustotal.com/gui/file/fe4ed3e638986a25db05fa3827a497f9a2545938f3c697c88da2f2e6009f70b0)

## License

FFmpeg is included under the [LGPL license](https://ffmpeg.org/legal.html).
