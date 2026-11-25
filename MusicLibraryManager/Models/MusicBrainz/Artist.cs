using System.Text.Json.Serialization;

namespace MusicLibraryManager.Models.MusicBrainz;

public sealed class Artist
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
}
