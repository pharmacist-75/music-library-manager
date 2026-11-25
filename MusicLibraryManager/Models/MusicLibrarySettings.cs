using System.Collections.Generic;

namespace MusicLibraryManager.Models
{
    #nullable enable
    public sealed class MusicLibrarySettings
    {
        public string MusicLibraryFolder { get; set; } = "";
        public string PlaylistFolder { get; set; } = "";
        public List<string>? VisibleColumns { get; set; }
    }
}
