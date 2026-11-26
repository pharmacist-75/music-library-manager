using System.Text.Json.Serialization;

namespace MusicLibraryManager.Models.MusicBrainz;

#nullable enable
public sealed class ArtistCredit
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("artist")]
    public Artist? Artist { get; set; }
}
