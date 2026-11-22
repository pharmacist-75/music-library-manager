using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static PublicFunctions;

public partial class frmPlaylist : Form
{
    public frmPlaylist()
    {
        InitializeComponent();
    }

    private void frmPlaylist_Load(object sender, EventArgs e)
    {
        FileInfo[] availablePlayLists;
        availablePlayLists = PlaylistFolder.GetFiles("*.m3u");
        cmbPlaylist.Items.AddRange(availablePlayLists);
        cmbPlaylist.Items.Add("<newPlayList.m3u>");
        cmbPlaylist.SelectedText = "<newPlayList.m3u>";
        chkMerge.Visible = false;

        foreach (string file in DroppedFiles)
        {
            AddRowToGridview(ref dgvSongs, file, Color.Green, "V1");
        }
    }

    private void cmdCancel_Click(object sender, EventArgs e)
    {
        Dispose();
    }

    private void cmdOk_Click(object sender, EventArgs e)
    {
        string playList = "";
        StreamWriter sw;
        string mp3File;
        bool skip;

        if (cmbPlaylist.Text == "<newPlayList.m3u>")
        {
            MessageBox.Show("Please enter a name for the new playlist.", "Playlist Name Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //playList = InputBox("Name of PlayList: ");
            //if (!playList.ToLower().EndsWith(".m3u"))
            //{
            //    playList += ".m3u";
            //}
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
                    skip = row.Cells[0].Style.ForeColor == Color.Red;
                    skip = skip | (row.Cells[0].Style.ForeColor == Color.LightGray);
                }
                else
                {
                    skip = row.Cells[0].Style.ForeColor != Color.Green;
                }
                if (!skip)
                {
                    mp3File = ".." + row.Cells[0].Value.ToString().Substring(2);
                    sw.WriteLine(mp3File);
                }
            }
            sw.Close();
        }
        Dispose();
    }

    private void cmbPlaylist_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkMerge.Checked = false;
        if (File.Exists(PlaylistFolder.FullName + cmbPlaylist.Text))
        {
            chkMerge.Checked = true;
        }
        chkMerge.Visible = cmbPlaylist.Text != "<newPlayList.m3u>";
    }

    private void chkMerge_CheckedChanged(object sender, EventArgs e)
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
                    if (row.Cells[0].Style.ForeColor != Color.Green)
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
                mp3File = DriveLetter + mp3File.Substring(2);
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
                AddRowToGridview(ref dgvSongs, mp3File, rowColor, "V1");
            }
            sr.Close();
        }
    }

    private void cmdValidatePlaylists_Click(object sender, EventArgs e)
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
                fileName = sr.ReadLine().Replace("..\\", DriveLetter + "\\");
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
