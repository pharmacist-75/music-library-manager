using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MusicLibraryManager.Models.MusicBrainz;

public sealed class ArtistSearchResult
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("artists")]
    public List<Artist> Artists { get; set; } = [];
}