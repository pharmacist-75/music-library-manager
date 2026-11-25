using MusicLibraryManager;
using MusicLibraryManager.Models;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static PublicFunctions;

public partial class FrmPlaylist : Form
{
    public FrmPlaylist()
    {
        InitializeComponent();
        InitializeColumns(dgvSongs);
    }

    private void FrmPlaylist_Load(object sender, EventArgs e)
    {
        FileInfo[] availablePlayLists;
        availablePlayLists = PlaylistFolder.GetFiles("*.m3u");
        cmbPlaylist.Items.AddRange(availablePlayLists);
        cmbPlaylist.Items.Add("<newPlayList.m3u>");
        cmbPlaylist.SelectedText = "<newPlayList.m3u>";
        chkMerge.Visible = false;

        foreach (string file in DroppedFiles)
        {
            AddRowToGridview(dgvSongs, new FileInfo(file), Color.Green);
        }
    }

    private void CmdCancel_Click(object sender, EventArgs e)
    {
        Dispose();
    }

    private void CmdOk_Click(object sender, EventArgs e)
    {
        string playList;
        StreamWriter sw;
        string mp3File;
        bool skip;

        if (cmbPlaylist.Text == "<newPlayList.m3u>")
        {
            FileNameDialog fileNameDialog = new("newPlayList.m3u");
            fileNameDialog.ShowDialog();

            playList = fileNameDialog.FileName;

            if (!playList.ToLower().EndsWith(".m3u"))
            {
                playList += ".m3u";
            }
        }
        else
        {
            playList = cmbPlaylist.Text;
        }

        if (playList != "")
        {
            sw = new StreamWriter(PlaylistFolder.FullName + playList, false);
            foreach (DataGridViewRow row in dgvSongs.Rows)
            {
                if (chkMerge.Checked)
                {
                    skip = row.Cells["FileName"].Style.ForeColor == Color.Red;
                    skip |= (row.Cells["FileName"].Style.ForeColor == Color.LightGray);
                }
                else
                {
                    skip = row.Cells["FileName"].Style.ForeColor != Color.Green;
                }
                if (!skip)
                {
                    mp3File = $"..{row.Cells["FileName"].Value.ToString()[2..]}";
                    sw.WriteLine(mp3File);
                }
            }
            sw.Close();
        }
        Dispose();
    }

    private void CmbPlaylist_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkMerge.Checked = false;
        if (File.Exists(PlaylistFolder.FullName + cmbPlaylist.Text))
        {
            chkMerge.Checked = true;
        }
        chkMerge.Visible = cmbPlaylist.Text != "<newPlayList.m3u>";
    }

    private void ChkMerge_CheckedChanged(object sender, EventArgs e)
    {
        StreamReader sr;
        string mp3File;
        Color rowColor;
        bool rowRemoved;

        if (chkMerge.Checked == false)
        {
            do
            {
                rowRemoved = false;
                foreach (DataGridViewRow row in dgvSongs.Rows)
                {
                    if (row.Cells["FileName"].Style.ForeColor != Color.Green)
                    {
                        dgvSongs.Rows.Remove(row);
                        rowRemoved = true;
                        break;
                    }
                }
            } while (rowRemoved);
        }
        else
        {
            sr = new StreamReader(PlaylistFolder.FullName + cmbPlaylist.Text);
            while (sr.Peek() >= 0)
            {
                mp3File = sr.ReadLine();
                mp3File = string.Concat(MusicLibraryFolder, mp3File.AsSpan(2));
                if (File.Exists(mp3File))
                {
                    if (DroppedFiles.Contains(mp3File))
                    {
                        rowColor = Color.Red;
                    }
                    else
                    {
                        rowColor = Color.Blue;
                    }
                }
                else
                {
                    rowColor = Color.LightGray;
                }
                AddRowToGridview(dgvSongs, new FileInfo(mp3File), rowColor);
            }
            sr.Close();
        }
    }

    private void CmdValidatePlaylists_Click(object sender, EventArgs e)
    {
        FileInfo[] availablePlayLists;
        StreamReader sr;
        FileInfo mp3File;
        string fileName;

        availablePlayLists = PlaylistFolder.GetFiles("*.m3u");
        foreach (FileInfo playList in availablePlayLists)
        {
            sr = new StreamReader(playList.FullName);
            while (sr.Peek() >= 0)
            {
                fileName = sr.ReadLine().Replace("..\\", MusicLibraryFolder + "\\");
                mp3File = new FileInfo(fileName);
                if (!mp3File.Exists)
                {
                    Debug.Print(playList.Name + " missing file " + mp3File.FullName);
                }
            }
            sr.Close();
        }
        Debug.Print("finished.");
    }
}
