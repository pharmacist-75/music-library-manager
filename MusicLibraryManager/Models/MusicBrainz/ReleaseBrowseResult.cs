using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MusicLibraryManager.Models.MusicBrainz;

public sealed class ReleaseBrowseResult
{
    [JsonPropertyName("release-count")]
    public int Count { get; set; }

    [JsonPropertyName("releases")]
    public List<Release> Releases { get; set; } = [];
}