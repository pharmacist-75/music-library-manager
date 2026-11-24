using System.Collections.Generic;

namespace MusicLibraryManager.Models
{
    public class MusicMetadata
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string AlbumArtist { get; set; }
        public string TrackNumber { get; set; }
        public string DiscNumber { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; }
        public List<MusicComment> Comments { get; set; } = [];
        public int? LengthMilliseconds { get; set; }
        public List<MusicLyrics> Lyrics { get; set; } = [];
    }
}
