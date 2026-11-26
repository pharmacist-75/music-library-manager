using System.Text.Json.Serialization;

namespace MusicLibraryManager.Models.MusicBrainz;

#nullable enable
public sealed class ReleaseTrack
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("number")]
    public string Number { get; set; } = "";

    [JsonPropertyName("position")]
    public int Position { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("length")]
    public int? Length { get; set; }

    [JsonPropertyName("recording")]
    public Recording? Recording { get; set; }
}