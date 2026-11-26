using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MusicLibraryManager.Models.MusicBrainz;

#nullable enable
public sealed class ReleaseMedium
{
    [JsonPropertyName("position")]
    public int Position { get; set; }

    [JsonPropertyName("format")]
    public string? Format { get; set; }

    [JsonPropertyName("track-count")]
    public int TrackCount { get; set; }

    [JsonPropertyName("tracks")]
    public List<ReleaseTrack> Tracks { get; set; } = [];
}