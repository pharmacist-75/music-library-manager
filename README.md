# MusicLibraryManager

MusicLibraryManager is a Windows desktop application for managing MP3 files and their metadata.

The application provides a graphical user interface for viewing and editing ID3 tags, managing playlists, and working with MP3 files.

## Features

- View and edit ID3v1 and ID3v2 metadata
- Manage artist, title, and album information
- Rename MP3 files based on their metadata
- Create, merge, and validate M3U playlists
- Export metadata to CSV
- Identify missing or inconsistent metadata

## Technology

- C#
- .NET 10
- Windows Forms
- IdSharp.Tagging 2.0.3 for MP3 metadata handling
- MSTest for unit testing

## Requirements

- Windows
- .NET 10 Desktop Runtime
- Visual Studio 2026 with .NET desktop development support (for development)

## Getting Started

1. Clone the repository.
2. Open `MusicLibraryManager.sln` in Visual Studio.
3. Restore the NuGet dependencies.
4. Build and run the application.

Alternatively, build the solution using the .NET CLI:

```bash
dotnet restore
dotnet build
dotnet test
```

## Development

The application was originally developed in Visual Basic .NET using .NET Framework 4.8 and has since been migrated to C# and .NET 10.

Planned functional enhancements include:

- Extended ID3 metadata management
- MusicBrainz integration for retrieving artist, album, and track information
- Album cover management
- Automatic organization of music files into artist and album directories
- Preview and validation of file operations before applying changes

## License

No license has been specified. All rights reserved.