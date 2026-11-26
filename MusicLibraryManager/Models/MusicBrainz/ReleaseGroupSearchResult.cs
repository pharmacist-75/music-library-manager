using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MusicLibraryManager.Models.MusicBrainz;

public sealed class ReleaseGroupSearchResult
{
    [JsonPropertyName("release-group-count")]
    public int Count { get; set; }

    [JsonPropertyName("release-groups")]
    public List<ReleaseGroup> ReleaseGroups { get; set; } = [];
}