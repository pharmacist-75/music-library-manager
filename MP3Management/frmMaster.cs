using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using IdSharp.Tagging.ID3v1;
using IdSharp.Tagging.ID3v2;
using My;
using static PublicFunctions;

public partial class frmMaster : Form
{
    private string _action;
    private const int MaxLengthArtistV1 = 30;

    public frmMaster()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        // Dim files As FileInfo()
        // Dim fi As FileInfo
        // Dim parts() As String
        // Dim newFileName As String
        // files = New DirectoryInfo("I:\\").GetFiles("*Rock'n'roll Realschule*.mp3")
        // For Each fi In files
        //     parts = fi.Name.Split("-")
        //     newFileName = parts(0).Trim() & " - " & parts(3).Trim().Replace(".mp3", " (Mtv Unplugged).mp3")
        //     Debug.Print(fi.FullName.Replace(fi.Name, newFileName))
        //     If fi.FullName.Replace(fi.Name, newFileName) <> fi.FullName Then
        //         Try
        //             fi.MoveTo(fi.FullName.Replace(fi.Name, newFileName))
        //         Catch ex As Exception
        //             Beep()
        //         End Try
        //     End If
        // Next

        AllowDrop = true;
        txtDriveLetter.Text = Settings.Default.DriveLetter ?? Application.StartupPath.Substring(0, 2);
        DriveLetter = txtDriveLetter.Text;
        txtPlaylistFolder.Text = Settings.Default.PlaylistFolder ?? "";
        if (txtPlaylistFolder.Text != "")
        {
            PlaylistFolder = new DirectoryInfo(txtPlaylistFolder.Text);
        }
    }

    private void Form1_DragDrop(object sender, DragEventArgs e)
    {
        DroppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
        frmPlaylist playlist = new frmPlaylist();
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
        ID3v1Tag id3v1;
        ID3v2Tag id3v2;
        bool updateArtist;
        bool updateTitle;
        bool updateAlbum;

        foreach (DataGridViewRow row in dgvSongs.Rows)
        {
            updateArtist = ((row.Cells[1].Style.ForeColor == Color.Red || row.Cells[1].Style.ForeColor == Color.Blue) && row.Cells[1].Value != null);
            updateTitle = ((row.Cells[2].Style.ForeColor == Color.Red || row.Cells[2].Style.ForeColor == Color.Blue) && row.Cells[2].Value != null);
            updateAlbum = ((row.Cells[3].Style.ForeColor == Color.Red || row.Cells[3].Style.ForeColor == Color.Blue) && row.Cells[3].Value != null);

            if (updateAlbum || updateArtist || updateTitle)
            {
                if (optV1.Checked)
                {
                    id3v1 = new ID3v1Tag(row.Cells[0].Value.ToString());
                    if (updateArtist) id3v1.Artist = row.Cells[1].Value.ToString();
                    if (updateTitle) id3v1.Title = row.Cells[2].Value.ToString();
                    if (updateAlbum) id3v1.Album = row.Cells[3].Value.ToString();
                    id3v1.Save(row.Cells[0].Value.ToString());
                }
                else
                {
                    id3v2 = new ID3v2Tag(row.Cells[0].Value.ToString());
                    if (updateArtist) id3v2.Artist = row.Cells[1].Value.ToString();
                    if (updateTitle) id3v2.Title = row.Cells[2].Value.ToString();
                    if (updateAlbum) id3v2.Album = row.Cells[3].Value.ToString();
                    id3v2.Save(row.Cells[0].Value.ToString());
                }
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
        ID3v2Tag id3;
        StreamWriter sw;
        TimeSpan length;

        mp3Files = new DirectoryInfo(txtDriveLetter.Text).GetFiles("*.MP3");
        sw = new StreamWriter(txtDriveLetter.Text + "\\ID3_Tags_2.CSV", false, Encoding.UTF8);
        sw.WriteLine("Filename;Album;Artist;Comment;Disc;Genre;Length;Lyrics;PaddingSize;Song;Title;Track;Year");
        foreach (FileInfo mp3File in mp3Files)
        {
            try
            {
                id3 = new ID3v2Tag(mp3File.FullName);
                sw.Write(mp3File.FullName);
                sw.Write(";" + NNStr(id3.Album));
                sw.Write(";" + NNStr(id3.Artist).Replace(Environment.NewLine, ""));
                sw.Write(";" + ListToString(id3.CommentsList, " "));
                sw.Write(";" + NNStr(id3.DiscNumber).Replace(Environment.NewLine, ""));
                sw.Write(";" + NNStr(id3.Genre).Replace(Environment.NewLine, ""));
                if (id3.LengthMilliseconds != null)
                {
                    length = new TimeSpan(0, 0, 0, 0, NNInt(id3.LengthMilliseconds));
                    sw.Write(";" + length.Hours + ":" + length.Minutes + ":" + length.Seconds + ":" + length.Milliseconds);
                }
                else
                {
                    sw.Write(";");
                }
                sw.Write(";" + ListToString(id3.UnsynchronizedLyricsList, " "));
                sw.Write(";"); // & id3.PaddingSize.ToString())
                sw.Write(";" + NNStr(id3.Title).Replace(Environment.NewLine, ""));
                sw.Write(";" + NNStr(id3.Title).Replace(Environment.NewLine, ""));
                sw.Write(";" + NNStr(id3.TrackNumber).Replace(Environment.NewLine, ""));
                sw.Write(";" + NNStr(id3.Year));
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

    private void cmdBrowse_Click(object sender, EventArgs e)
    {
        DialogResult dr;
        FolderBrowserDialog1.SelectedPath = txtDriveLetter.Text + "\\PLAYLIST\\";
        dr = FolderBrowserDialog1.ShowDialog();
        if (dr == DialogResult.OK)
        {
            txtPlaylistFolder.Text = FolderBrowserDialog1.SelectedPath + "\\";
            Settings.Default.PlaylistFolder = txtPlaylistFolder.Text;
            Settings.Default.Save();
            if (txtPlaylistFolder.Text != "")
            {
                PlaylistFolder = new DirectoryInfo(txtPlaylistFolder.Text);
            }
        }
    }

    private void txtDriveLetter_TextChanged(object sender, EventArgs e)
    {
        DriveLetter = txtDriveLetter.Text;
    }

    private void cmdLoad_Click(object sender, EventArgs e)
    {
        FileInfo[] mp3Files;
        dgvSongs.Rows.Clear();
        mp3Files = new DirectoryInfo(DriveLetter + "\\" + txtFolder.Text).GetFiles(txtFilter.Text);
        dgvSongs.Visible = false;
        labStatus.Visible = true;
        foreach (FileInfo mp3File in mp3Files)
        {
            labStatus.Text = mp3File.FullName + "...";
            Application.DoEvents();
            if (optV1.Checked)
            {
                AddRowToGridview(ref dgvSongs, mp3File.FullName, Color.Black, "V1");
            }
            else
            {
                AddRowToGridview(ref dgvSongs, mp3File.FullName, Color.Black, "V2");
            }
        }
        dgvSongs.Sort(dgvSongs.Columns[0], ListSortDirection.Ascending);
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
            mp3File = new FileInfo(dgvSongs.Rows[e.RowIndex].Cells[0].Value.ToString());
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
            dgvSongs.Rows[e.RowIndex].Cells[1].Value = artist;
            dgvSongs.Rows[e.RowIndex].Cells[1].Style.ForeColor = Color.Red;
            dgvSongs.Rows[e.RowIndex].Cells[2].Value = parts[parts.Length - 1].Trim();
            dgvSongs.Rows[e.RowIndex].Cells[2].Style.ForeColor = Color.Red;
        }
        if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
        {
            mp3File = new FileInfo(dgvSongs.Rows[e.RowIndex].Cells[0].Value.ToString());
            newFileName = Convert.ToString(dgvSongs.Rows[e.RowIndex].Cells[1].Value);
            newFileName += " - ";
            newFileName += Convert.ToString(dgvSongs.Rows[e.RowIndex].Cells[2].Value);
            newFileName += ".mp3";
            mp3File.MoveTo(mp3File.DirectoryName + "\\" + newFileName.Replace("?", "").Replace("/", ""));
            dgvSongs.Rows[e.RowIndex].Cells[0].Value = mp3File.FullName;
        }
    }

    private void dgvSongs_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        dgvSongs.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Red;
    }

    private void cmdShowUnclear_Click(object sender, EventArgs e)
    {
        FileInfo[] mp3Files;
        ID3v2Tag id3v2;
        ID3v1Tag id3v1;
        string[] parts;
        string fileNameArtist;
        string tagArtist;
        string version;
        string selectedVersion;

        dgvSongs.Rows.Clear();
        mp3Files = new DirectoryInfo(DriveLetter + "\\" + txtFolder.Text).GetFiles(txtFilter.Text);
        dgvSongs.Visible = false;
        labStatus.Visible = true;
        if (optV1.Checked)
        {
            selectedVersion = "V1";
        }
        else
        {
            selectedVersion = "V2";
        }
        foreach (FileInfo mp3File in mp3Files)
        {
            labStatus.Text = mp3File.FullName + "...";
            Application.DoEvents();
            string[] myDelims = new string[] { " - " };
            parts = mp3File.Name.Replace(mp3File.Extension, "").Split(myDelims, StringSplitOptions.None);
            tagArtist = "";
            version = "";
            if (optV1.Checked)
            {
                id3v1 = new ID3v1Tag(mp3File.FullName);
                if (id3v1.Title != null)
                {
                    tagArtist = id3v1.Title.ToUpper();
                    version = "V1";
                }
                else
                {
                    id3v2 = new ID3v2Tag(mp3File.FullName);
                    if (id3v2.Title != null)
                    {
                        tagArtist = id3v2.Title.ToUpper();
                        version = "V2";
                    }
                }
            }
            else
            {
                id3v2 = new ID3v2Tag(mp3File.FullName);
                if (id3v2.Title != null)
                {
                    tagArtist = id3v2.Title.ToUpper();
                    version = "V2";
                }
                else
                {
                    id3v1 = new ID3v1Tag(mp3File.FullName);
                    if (id3v1.Title != null)
                    {
                        tagArtist = id3v1.Title.ToUpper();
                        version = "V1";
                    }
                }
            }
            fileNameArtist = "";
            // If parts.Length > 2 Then
            //     For i = 0 To parts.Length - 2
            //         fileNameArtist += parts(i) & "-"
            //     Next
            //     fileNameArtist = fileNameArtist.Trim("-")
            // Else
            // fileNameArtist = parts(0).Trim()
            fileNameArtist = parts[parts.Length - 1].Trim();
            // End If
            if (version == "")
            {
                AddRowToGridview(ref dgvSongs, mp3File.FullName, Color.Red, selectedVersion);
            }
            else
            {
                if (version != selectedVersion)
                {
                    AddRowToGridview(ref dgvSongs, mp3File.FullName, Color.Blue, version);
                }
                else
                {
                    if (selectedVersion == "V1" && fileNameArtist.Trim().Length > MaxLengthArtistV1)
                    {
                        fileNameArtist = fileNameArtist.Trim().Substring(0, MaxLengthArtistV1);
                    }
                    if (fileNameArtist.Trim().ToUpper() != tagArtist)
                    {
                        // If fileNameArtist.Trim() <> tagArtist Then
                        AddRowToGridview(ref dgvSongs, mp3File.FullName, Color.Black, version);
                    }
                }
            }
        }
        dgvSongs.Sort(dgvSongs.Columns[0], ListSortDirection.Ascending);
        labStatus.Visible = false;
        dgvSongs.Visible = true;
        _action = "SHOW_UNCLEAR";
    }
}
