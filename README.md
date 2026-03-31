# VideoScheduler

VideoScheduler is a cross-platform VLC-based video scheduler that allows you to create automated TV-like programming schedules. The application controls VLC media player via HTTP to play videos at specific times, complete with commercial breaks, bumpers, and automatic episode progression.

## Installation

1. Download the latest release ZIP file
2. Extract to any folder
3. Run `VideoScheduler.exe`
4. Access the web UI at `http://localhost:5050`

### Configure Web Port

The app's HTTP port is configurable via `appsettings.json`:

```json
{
  "Server": {
    "Port": 5050
  }
}
```

Change `Server:Port` to any free port from `1` to `65535`, then restart VideoScheduler.
If the configured port is invalid or already in use, startup will fail with a clear error.

### VLC Setup (Required)

VideoScheduler controls VLC via its HTTP interface. To enable this:

1. Open VLC media player
2. Go to **Tools → Preferences**
3. At the bottom, select **All** under "Show settings"
4. Navigate to **Interface → Main interfaces**
5. Check **Web**
6. Navigate to **Interface → Main interfaces → Lua**
7. Under "Lua HTTP", set the password to `password`
8. Restart VLC
9. Launch VLC with the web interface enabled, or it will start automatically

> **Note:** VLC must be running for VideoScheduler to control playback.

## Getting Started

### 1. Scan Your Library

Go to **Library** and scan your media folder. The scanner expects the following structure:

```
Root Folder/
├── Shows/
│   └── {Show Name}/
│       └── Season {#}/
│           └── {Episode#}.mp4
├── Movies/
│   └── {Movie}.mp4
├── Bumpers/
│   └── {Bumper}.mp4
└── Commercials/
    └── {Commercial}.mp4
```

The scanner will automatically detect and categorize your content.

### 2. Create Show Runs

Go to **Runs** to create "runs" for your TV shows. A run tracks which episode should play next and automatically advances after each playback.

### 3. Create Segments

Go to **Segments** (Templates) to build reusable content blocks. A segment can include:
- **Show Run** - Plays the next episode from a run
- **Random** - Picks a random asset from a category or group
- **Specific Asset** - Plays a specific bumper or commercial
- **Commercial Break** - Fills time with commercials (auto or fixed duration)

### 4. Build Your Schedule

Go to **Schedule** to assign segments to time blocks for each day of the week. Time blocks define when content should play.

### 5. Generate & Sync

On the **Home** page:
1. Set the number of days to forecast
2. Click **Generate** to build the playlist
3. Enable **Sync with VLC** to start automated playback

## Features

| Feature | Description |
|---------|-------------|
| **Library Scanner** | Scans folders for shows, movies, bumpers, and commercials |
| **Asset Groups** | Create custom groups of assets for randomization |
| **Show Runs** | Track episode progression with automatic advancement |
| **Segment Templates** | Build reusable content structures |
| **Weekly Schedule** | Define time blocks for each day |
| **Auto Commercial Breaks** | Automatically fill remaining time with commercials |
| **VLC Sync** | Automated playback control with drift correction |
| **Now Playing** | Live status display of current playback |
| **Playlist Forecast** | Preview upcoming content for multiple days |

## Data Storage

All data is stored as YAML files in the `DataFiles/` folder:
- `library.yaml` - Scanned media library, groups, and runs
- `templates.yaml` - Segment templates
- `schedule.yaml` - Weekly schedule configuration
- `appstate.yaml` - Current playlist and app state

## System Requirements

- Windows 10/11
- .NET 10.0 Runtime
- VLC media player with HTTP interface enabled

## Running on Linux

The application can run on Linux without the system tray icon:

```bash
dotnet VideoScheduler.dll
```

Note: The Windows Forms tray icon is only available on Windows builds.

## Demo


> **Note:**  A beta demo is coming soon.

## Discord

Join the [Discord](https://discord.com/channels/677685944521850900/1076154356270899270/1260385784868110376) server for support!

## Copyright Warning

This application is intended only for personal use to view videos that are legally obtained and authorized for use by respective copyright holders. Use of this application does not grant license for use of any copyrighted material.

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

## Support

You can show your support by buying me a coffee! Donations are never required and will more than likely actually be spent on pizza.

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://buymeacoffee.com/jasonssoftware)
