using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using MusicLibraryManager.Models;
using MusicLibraryManager.Services;
using static PublicFunctions;

public partial class frmMaster : Form
{
    private string _action;
    private MusicLibrarySettings _settings = new();
    private readonly ContextMenuStrip _columnMenu = new();

    private void InitializeColumnMenu()
    {
        dgvSongs.ColumnHeaderMouseClick += DgvSongs_ColumnHeaderMouseClick;
    }

    public frmMaster()
    {
        InitializeComponent();
        InitializeColumns(dgvSongs);
        InitializeColumnMenu();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        AllowDrop = true;

        LoadSettings();

        MusicLibraryFolder = _settings.MusicLibraryFolder ?? Application.StartupPath;
        txtMusicLibraryFolder.Text = MusicLibraryFolder;

        txtPlaylistFolder.Text = _settings.PlaylistFolder ?? "";
        if (txtPlaylistFolder.Text != "")
        {
            PlaylistFolder = new DirectoryInfo(txtPlaylistFolder.Text);
        }
    }

    private void Form1_DragDrop(object sender, DragEventArgs e)
    {
        DroppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
        FrmPlaylist playlist = new();
        playlist.Show();
    }

    private void Form1_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effect = DragDropEffects.Copy;
        }
    }

    private void cmdUpdate_Click(object sender, EventArgs e)
    {
        bool updateArtist;
        bool updateTitle;
        bool updateAlbum;

        foreach (DataGridViewRow row in dgvSongs.Rows)
        {
            updateArtist = ((row.Cells["Artist"].Style.ForeColor == Color.Red || row.Cells["Artist"].Style.ForeColor == Color.Blue) && row.Cells["Artist"].Value != null);
            updateTitle = ((row.Cells["Title"].Style.ForeColor == Color.Red || row.Cells["Title"].Style.ForeColor == Color.Blue) && row.Cells["Title"].Value != null);
            updateAlbum = ((row.Cells["Album"].Style.ForeColor == Color.Red || row.Cells["Album"].Style.ForeColor == Color.Blue) && row.Cells["Album"].Value != null);

            if (updateAlbum || updateArtist || updateTitle)
            {
                string fileNamePath = Path.Combine(row.Cells["Folder"].Value.ToString(), row.Cells["Filename"].Value.ToString());
                FileInfo fileInfo = new(fileNamePath);

                MusicMetadata metadata = MusicMetadataService.LoadMetadata(fileInfo.FullName);
                if (updateArtist) metadata.Artist = row.Cells["Artist"].Value.ToString();
                if (updateTitle) metadata.Title = row.Cells["Title"].Value.ToString();
                if (updateAlbum) metadata.Album = row.Cells["Album"].Value.ToString();
                MusicMetadataService.UpdateMetadata(metadata, fileInfo.FullName);
            }
        }

        MessageBox.Show("Update done!");

        switch (_action)
        {
            case "LOAD":
                cmdLoad_Click(this, new EventArgs());
                break;

            case "SHOW_UNCLEAR":
                cmdShowUnclear_Click(this, new EventArgs());
                break;
        }
    }

    private void cmdCreateCSV_Click(object sender, EventArgs e)
    {
        FileInfo[] mp3Files;
        StreamWriter sw;
        TimeSpan length;

        mp3Files = new DirectoryInfo(txtMusicLibraryFolder.Text).GetFiles("*.MP3");
        sw = new StreamWriter(txtMusicLibraryFolder.Text + "\\ID3_Tags_2.CSV", false, Encoding.UTF8);
        sw.WriteLine("Filename;Album;Artist;Comment;Disc;Genre;Length;Lyrics;PaddingSize;Song;Title;Track;Year");
        foreach (FileInfo mp3File in mp3Files)
        {
            try
            {
                MusicMetadata metadata = MusicMetadataService.LoadMetadata(mp3File.FullName);

                sw.Write(mp3File.FullName);
                sw.Write(";" + NNStr(metadata.Album));
                sw.Write(";" + NNStr(metadata.Artist).Replace(Environment.NewLine, ""));
                sw.Write(";" + ListToString(metadata.Comments, " "));
                sw.Write(";" + NNStr(metadata.DiscNumber).Replace(Environment.NewLine, ""));
                sw.Write(";" + NNStr(metadata.Genre).Replace(Environment.NewLine, ""));
                if (metadata.LengthMilliseconds != null)
                {
                    length = new TimeSpan(0, 0, 0, 0, NNInt(metadata.LengthMilliseconds));
                    sw.Write(";" + length.Hours + ":" + length.Minutes + ":" + length.Seconds + ":" + length.Milliseconds);
                }
                else
                {
                    sw.Write(";");
                }
                sw.Write(";" + ListToString(metadata.Lyrics, " "));
                sw.Write(";");
                sw.Write(";" + NNStr(metadata.Title).Replace(Environment.NewLine, ""));
                sw.Write(";" + NNStr(metadata.Title).Replace(Environment.NewLine, ""));
                sw.Write(";" + NNStr(metadata.TrackNumber).Replace(Environment.NewLine, ""));
                sw.Write(";" + NNStr(metadata.Year));
            }
            catch (Exception)
            {
                sw.Write(";M I S S I N G  T A G");
            }
            finally
            {
                sw.WriteLine();
            }
        }
        sw.Close();
    }

    private void cmdBrowsePlaylistFolder_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog1.SelectedPath = txtMusicLibraryFolder.Text + "\\PLAYLIST\\";
        DialogResult dr = FolderBrowserDialog1.ShowDialog();
        if (dr == DialogResult.OK)
        {
            txtPlaylistFolder.Text = FolderBrowserDialog1.SelectedPath + "\\";
            _settings.PlaylistFolder = txtPlaylistFolder.Text;
            SaveSettings();

            if (txtPlaylistFolder.Text != "")
            {
                PlaylistFolder = new DirectoryInfo(txtPlaylistFolder.Text);
            }
        }
    }

    private void cmdLoad_Click(object sender, EventArgs e)
    {
        FileInfo[] mp3Files;
        dgvSongs.Rows.Clear();
        mp3Files = new DirectoryInfo(txtMusicLibraryFolder.Text).GetFiles(txtFilter.Text);
        dgvSongs.Visible = false;
        labStatus.Visible = true;
        foreach (FileInfo mp3File in mp3Files)
        {
            labStatus.Text = mp3File.FullName + "...";
            Application.DoEvents();
            AddRowToGridview(dgvSongs, mp3File, Color.Black);
        }
        dgvSongs.Sort(dgvSongs.Columns["Filename"], ListSortDirection.Ascending);
        labStatus.Visible = false;
        dgvSongs.Visible = true;
        _action = "LOAD";
    }

    private void dgvSongs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        string[] parts;
        FileInfo mp3File;
        string artist;
        string[] myDelims = new string[] { " - " };
        string newFileName;

        if (e.ColumnIndex == 0)
        {
            string fileNamePath = Path.Combine(dgvSongs.Rows[e.RowIndex].Cells["Directory"].Value.ToString(), dgvSongs.Rows[e.RowIndex].Cells["Filename"].Value.ToString());
            mp3File = new FileInfo(fileNamePath);
            parts = mp3File.Name.Replace(mp3File.Extension, "").Split(myDelims, StringSplitOptions.None);
            artist = "";
            if (parts.Length > 2)
            {
                for (int i = 0; i <= parts.Length - 2; i++)
                {
                    artist += parts[i] + "-";
                }
                artist = artist.Trim('-');
            }
            else
            {
                artist = parts[0].Trim();
            }
            dgvSongs.Rows[e.RowIndex].Cells["Artist"].Value = artist;
            dgvSongs.Rows[e.RowIndex].Cells["Artist"].Style.ForeColor = Color.Red;
            dgvSongs.Rows[e.RowIndex].Cells["Title"].Value = parts[parts.Length - 1].Trim();
            dgvSongs.Rows[e.RowIndex].Cells["Title"].Style.ForeColor = Color.Red;
        }
        if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
        {
            string fileNamePath = Path.Combine(dgvSongs.Rows[e.RowIndex].Cells["Directory"].Value.ToString(), dgvSongs.Rows[e.RowIndex].Cells["Filename"].Value.ToString());
            mp3File = new FileInfo(fileNamePath);
            newFileName = Convert.ToString(dgvSongs.Rows[e.RowIndex].Cells["Artist"].Value);
            newFileName += " - ";
            newFileName += Convert.ToString(dgvSongs.Rows[e.RowIndex].Cells["Title"].Value);
            newFileName += ".mp3";
            mp3File.MoveTo(mp3File.DirectoryName + "\\" + newFileName.Replace("?", "").Replace("/", ""));
            dgvSongs.Rows[e.RowIndex].Cells["Filename"].Value = mp3File.FullName;
        }
    }

    private void dgvSongs_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        dgvSongs.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Red;
    }

    private void cmdShowUnclear_Click(object sender, EventArgs e)
    {
        FileInfo[] mp3Files;
        string[] parts;
        string fileNameArtist;
        string tagArtist;

        dgvSongs.Rows.Clear();
        mp3Files = new DirectoryInfo(txtMusicLibraryFolder.Text).GetFiles(txtFilter.Text);
        dgvSongs.Visible = false;
        labStatus.Visible = true;

        foreach (FileInfo mp3File in mp3Files)
        {
            labStatus.Text = mp3File.FullName + "...";
            Application.DoEvents();
            string[] myDelims = new string[] { " - " };
            parts = mp3File.Name.Replace(mp3File.Extension, "").Split(myDelims, StringSplitOptions.None);
            tagArtist = "";

            MusicMetadata metadata = MusicMetadataService.LoadMetadata(mp3File.FullName);

            if (metadata.Title != null)
            {
                tagArtist = metadata.Title.ToUpper();
            }

            fileNameArtist = "";
            fileNameArtist = parts[parts.Length - 1].Trim();

            if (tagArtist == "")
            {
                AddRowToGridview(dgvSongs, mp3File, Color.Red);
            }
            else
            {
                if (fileNameArtist.Trim().ToUpper() != tagArtist)
                {
                    AddRowToGridview(dgvSongs, mp3File, Color.Black);
                }
            }
        }

        dgvSongs.Sort(dgvSongs.Columns["Filename"], ListSortDirection.Ascending);
        labStatus.Visible = false;
        dgvSongs.Visible = true;
        _action = "SHOW_UNCLEAR";
    }

    private void cmdBrowseMusicLibraryFolder_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog1.SelectedPath = txtMusicLibraryFolder.Text;
        DialogResult dr = FolderBrowserDialog1.ShowDialog();
        if (dr == DialogResult.OK)
        {
            txtMusicLibraryFolder.Text = FolderBrowserDialog1.SelectedPath + "\\";
            _settings.MusicLibraryFolder = txtMusicLibraryFolder.Text;
            SaveSettings();

            if (txtMusicLibraryFolder.Text != "")
            {
                MusicLibraryFolder = txtMusicLibraryFolder.Text;
            }
        }
    }

    private void SaveSettings()
    {
        string settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

        JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(new { MusicLibrary = _settings }, options);

        File.WriteAllText(settingsPath, json);
    }

    private void LoadSettings()
    {
        string settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (File.Exists(settingsPath))
        {
            using JsonDocument document = JsonDocument.Parse(File.ReadAllText(settingsPath));
            _settings = document.RootElement.GetProperty("MusicLibrary").Deserialize<MusicLibrarySettings>();
        }
        else
        {
            _settings = new MusicLibrarySettings();
        }
    }

    private void DgvSongs_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right)
        {
            return;
        }

        _columnMenu.Items.Clear();

        foreach (DataGridViewColumn column in dgvSongs.Columns)
        {
            ToolStripMenuItem item = new()
            {
                Text = column.HeaderText,
                Checked = column.Visible,
                CheckOnClick = true,
                Tag = column,
                Enabled = column.Name != "FileName"
            };

            item.CheckedChanged += ColumnMenuItem_CheckedChanged;

            _columnMenu.Items.Add(item);
        }

        _columnMenu.Show(Cursor.Position);
    }

    private void ColumnMenuItem_CheckedChanged(object sender, EventArgs e)
    {
        if (sender is not ToolStripMenuItem item || item.Tag is not DataGridViewColumn column)
        {
            return;
        }

        column.Visible = item.Checked;
    }
}
