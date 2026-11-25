using System.Text.Json.Serialization;

namespace MusicLibraryManager.Models.MusicBrainz;

public sealed class Release
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

    [JsonPropertyName("date")]
    public string? Date { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("track-count")]
    public int? TrackCount { get; set; }

    [JsonPropertyName("release-group")]
    public ReleaseGroup? ReleaseGroup { get; set; }
}
