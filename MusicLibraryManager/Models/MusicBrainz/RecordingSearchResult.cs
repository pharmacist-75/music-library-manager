using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MusicLibraryManager.Models.MusicBrainz
{
    public sealed class RecordingSearchResult
    {
        [JsonPropertyName("recordings")]
        public List<Recording> Recordings { get; set; } = [];
    }
}
