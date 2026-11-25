using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public partial class frmMaster
{
    private IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        try
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
        }
        finally
        {
            base.Dispose(disposing);
        }
    }

    private void InitializeComponent()
    {
        cmdUpdate = new Button();
        cmdCreateCSV = new Button();
        labMusicLibraryFolder = new Label();
        labPlaylistFolder = new Label();
        FolderBrowserDialog1 = new FolderBrowserDialog();
        txtPlaylistFolder = new TextBox();
        cmdBrowsePlaylistFolder = new Button();
        txtMusicLibraryFolder = new TextBox();
        dgvSongs = new DataGridView();
        cmdLoad = new Button();
        labStatus = new Label();
        txtFilter = new TextBox();
        labFilter = new Label();
        cmdShowUnclear = new Button();
        cmdBrowseMusicLibraryFolder = new Button();
        ((ISupportInitialize)dgvSongs).BeginInit();
        SuspendLayout();
        // 
        // cmdUpdate
        // 
        cmdUpdate.Location = new Point(462, 29);
        cmdUpdate.Margin = new Padding(4, 3, 4, 3);
        cmdUpdate.Name = "cmdUpdate";
        cmdUpdate.Size = new Size(88, 27);
        cmdUpdate.TabIndex = 0;
        cmdUpdate.Text = "Update";
        cmdUpdate.UseVisualStyleBackColor = true;
        cmdUpdate.Click += cmdUpdate_Click;
        // 
        // cmdCreateCSV
        // 
        cmdCreateCSV.Location = new Point(346, 29);
        cmdCreateCSV.Margin = new Padding(4, 3, 4, 3);
        cmdCreateCSV.Name = "cmdCreateCSV";
        cmdCreateCSV.Size = new Size(108, 27);
        cmdCreateCSV.TabIndex = 2;
        cmdCreateCSV.Text = "Create CSV";
        cmdCreateCSV.UseVisualStyleBackColor = true;
        cmdCreateCSV.Click += cmdCreateCSV_Click;
        // 
        // labMusicLibraryFolder
        // 
        labMusicLibraryFolder.AutoSize = true;
        labMusicLibraryFolder.Location = new Point(14, 10);
        labMusicLibraryFolder.Margin = new Padding(4, 0, 4, 0);
        labMusicLibraryFolder.Name = "labMusicLibraryFolder";
        labMusicLibraryFolder.Size = new Size(78, 15);
        labMusicLibraryFolder.TabIndex = 3;
        labMusicLibraryFolder.Text = "MusicLibrary:";
        // 
        // labPlaylistFolder
        // 
        labPlaylistFolder.AutoSize = true;
        labPlaylistFolder.Location = new Point(14, 36);
        labPlaylistFolder.Margin = new Padding(4, 0, 4, 0);
        labPlaylistFolder.Name = "labPlaylistFolder";
        labPlaylistFolder.Size = new Size(83, 15);
        labPlaylistFolder.TabIndex = 4;
        labPlaylistFolder.Text = "Playlist Folder:";
        // 
        // txtPlaylistFolder
        // 
        txtPlaylistFolder.Location = new Point(107, 32);
        txtPlaylistFolder.Margin = new Padding(4, 3, 4, 3);
        txtPlaylistFolder.Name = "txtPlaylistFolder";
        txtPlaylistFolder.Size = new Size(172, 23);
        txtPlaylistFolder.TabIndex = 5;
        // 
        // cmdBrowsePlaylistFolder
        // 
        cmdBrowsePlaylistFolder.Location = new Point(287, 30);
        cmdBrowsePlaylistFolder.Margin = new Padding(4, 3, 4, 3);
        cmdBrowsePlaylistFolder.Name = "cmdBrowsePlaylistFolder";
        cmdBrowsePlaylistFolder.Size = new Size(29, 27);
        cmdBrowsePlaylistFolder.TabIndex = 6;
        cmdBrowsePlaylistFolder.Text = "...";
        cmdBrowsePlaylistFolder.UseVisualStyleBackColor = true;
        cmdBrowsePlaylistFolder.Click += cmdBrowsePlaylistFolder_Click;
        // 
        // txtMusicLibraryFolder
        // 
        txtMusicLibraryFolder.Location = new Point(107, 7);
        txtMusicLibraryFolder.Margin = new Padding(4, 3, 4, 3);
        txtMusicLibraryFolder.Name = "txtMusicLibraryFolder";
        txtMusicLibraryFolder.Size = new Size(172, 23);
        txtMusicLibraryFolder.TabIndex = 7;
        // 
        // dgvSongs
        // 
        dgvSongs.AllowUserToAddRows = false;
        dgvSongs.AllowUserToResizeRows = false;
        dgvSongs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvSongs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvSongs.Location = new Point(14, 63);
        dgvSongs.Margin = new Padding(4, 3, 4, 3);
        dgvSongs.Name = "dgvSongs";
        dgvSongs.Size = new Size(1276, 357);
        dgvSongs.TabIndex = 8;
        dgvSongs.CellDoubleClick += dgvSongs_CellDoubleClick;
        dgvSongs.CellEndEdit += dgvSongs_CellEndEdit;
        // 
        // cmdLoad
        // 
        cmdLoad.Location = new Point(662, 30);
        cmdLoad.Margin = new Padding(4, 3, 4, 3);
        cmdLoad.Name = "cmdLoad";
        cmdLoad.Size = new Size(88, 27);
        cmdLoad.TabIndex = 9;
        cmdLoad.Text = "Load";
        cmdLoad.UseVisualStyleBackColor = true;
        cmdLoad.Click += cmdLoad_Click;
        // 
        // labStatus
        // 
        labStatus.AutoSize = true;
        labStatus.Location = new Point(658, 65);
        labStatus.Margin = new Padding(4, 0, 4, 0);
        labStatus.Name = "labStatus";
        labStatus.Size = new Size(39, 15);
        labStatus.TabIndex = 10;
        labStatus.Text = "Status";
        labStatus.Visible = false;
        // 
        // txtFilter
        // 
        txtFilter.Location = new Point(804, 32);
        txtFilter.Margin = new Padding(4, 3, 4, 3);
        txtFilter.Name = "txtFilter";
        txtFilter.Size = new Size(103, 23);
        txtFilter.TabIndex = 12;
        txtFilter.Text = "*.mp3";
        // 
        // labFilter
        // 
        labFilter.AutoSize = true;
        labFilter.Location = new Point(760, 36);
        labFilter.Margin = new Padding(4, 0, 4, 0);
        labFilter.Name = "labFilter";
        labFilter.Size = new Size(36, 15);
        labFilter.TabIndex = 11;
        labFilter.Text = "Filter:";
        // 
        // cmdShowUnclear
        // 
        cmdShowUnclear.Location = new Point(556, 30);
        cmdShowUnclear.Margin = new Padding(4, 3, 4, 3);
        cmdShowUnclear.Name = "cmdShowUnclear";
        cmdShowUnclear.Size = new Size(98, 27);
        cmdShowUnclear.TabIndex = 13;
        cmdShowUnclear.Text = "Show Unclear";
        cmdShowUnclear.UseVisualStyleBackColor = true;
        cmdShowUnclear.Click += cmdShowUnclear_Click;
        // 
        // cmdBrowseMusicLibraryFolder
        // 
        cmdBrowseMusicLibraryFolder.Location = new Point(287, 4);
        cmdBrowseMusicLibraryFolder.Margin = new Padding(4, 3, 4, 3);
        cmdBrowseMusicLibraryFolder.Name = "cmdBrowseMusicLibraryFolder";
        cmdBrowseMusicLibraryFolder.Size = new Size(29, 27);
        cmdBrowseMusicLibraryFolder.TabIndex = 14;
        cmdBrowseMusicLibraryFolder.Text = "...";
        cmdBrowseMusicLibraryFolder.UseVisualStyleBackColor = true;
        cmdBrowseMusicLibraryFolder.Click += cmdBrowseMusicLibraryFolder_Click;
        // 
        // frmMaster
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1304, 452);
        Controls.Add(cmdBrowseMusicLibraryFolder);
        Controls.Add(cmdShowUnclear);
        Controls.Add(txtFilter);
        Controls.Add(labFilter);
        Controls.Add(labStatus);
        Controls.Add(cmdLoad);
        Controls.Add(dgvSongs);
        Controls.Add(txtMusicLibraryFolder);
        Controls.Add(cmdBrowsePlaylistFolder);
        Controls.Add(txtPlaylistFolder);
        Controls.Add(labPlaylistFolder);
        Controls.Add(labMusicLibraryFolder);
        Controls.Add(cmdCreateCSV);
        Controls.Add(cmdUpdate);
        Margin = new Padding(4, 3, 4, 3);
        Name = "frmMaster";
        Text = "MusicLibraryManager";
        Load += Form1_Load;
        DragDrop += Form1_DragDrop;
        DragEnter += Form1_DragEnter;
        ((ISupportInitialize)dgvSongs).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    internal System.Windows.Forms.Button cmdUpdate;
    internal System.Windows.Forms.Button cmdCreateCSV;
    internal System.Windows.Forms.Label labMusicLibraryFolder;
    internal System.Windows.Forms.Label labPlaylistFolder;
    internal System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1;
    internal System.Windows.Forms.TextBox txtPlaylistFolder;
    internal System.Windows.Forms.Button cmdBrowsePlaylistFolder;
    internal System.Windows.Forms.Button cmdBrowseMusicLibraryFolder;
    internal System.Windows.Forms.TextBox txtMusicLibraryFolder;
    internal System.Windows.Forms.DataGridView dgvSongs;
    internal System.Windows.Forms.Button cmdLoad;
    internal System.Windows.Forms.Label labStatus;
    internal System.Windows.Forms.TextBox txtFilter;
    internal System.Windows.Forms.Label labFilter;
    internal System.Windows.Forms.Button cmdShowUnclear;
}
