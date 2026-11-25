using System.Windows.Forms;

namespace MusicLibraryManager.Models
{
    public sealed class GridColumnDefinition
    {
        public required string Name { get; init; }
        public required string Header { get; init; }
        public int Width { get; init; } = 100;
        public bool Visible { get; init; } = true;
        public DataGridViewAutoSizeColumnMode AutoSizeMode { get; init; } = DataGridViewAutoSizeColumnMode.None;
    }
}
