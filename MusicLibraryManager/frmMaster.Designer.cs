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
        this.cmdUpdate = new System.Windows.Forms.Button();
        this.cmdCreateCSV = new System.Windows.Forms.Button();
        this.labDrive = new System.Windows.Forms.Label();
        this.labPlaylistFolder = new System.Windows.Forms.Label();
        this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
        this.txtPlaylistFolder = new System.Windows.Forms.TextBox();
        this.cmdBrowse = new System.Windows.Forms.Button();
        this.txtDriveLetter = new System.Windows.Forms.TextBox();
        this.dgvSongs = new System.Windows.Forms.DataGridView();
        this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colArtist = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colAlbum = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.cmdLoad = new System.Windows.Forms.Button();
        this.labStatus = new System.Windows.Forms.Label();
        this.txtFilter = new System.Windows.Forms.TextBox();
        this.labFilter = new System.Windows.Forms.Label();
        this.cmdShowUnclear = new System.Windows.Forms.Button();
        this.optV1 = new System.Windows.Forms.RadioButton();
        this.optV2 = new System.Windows.Forms.RadioButton();
        this.txtFolder = new System.Windows.Forms.TextBox();
        ((ISupportInitialize)this.dgvSongs).BeginInit();
        this.SuspendLayout();
        // cmdUpdate
        this.cmdUpdate.Location = new System.Drawing.Point(396, 25);
        this.cmdUpdate.Name = "cmdUpdate";
        this.cmdUpdate.Size = new System.Drawing.Size(75, 23);
        this.cmdUpdate.TabIndex = 0;
        this.cmdUpdate.Text = "Update";
        this.cmdUpdate.UseVisualStyleBackColor = true;
        // cmdCreateCSV
        this.cmdCreateCSV.Location = new System.Drawing.Point(297, 25);
        this.cmdCreateCSV.Name = "cmdCreateCSV";
        this.cmdCreateCSV.Size = new System.Drawing.Size(93, 23);
        this.cmdCreateCSV.TabIndex = 2;
        this.cmdCreateCSV.Text = "Create CSV";
        this.cmdCreateCSV.UseVisualStyleBackColor = true;
        // labDrive
        this.labDrive.AutoSize = true;
        this.labDrive.Location = new System.Drawing.Point(12, 9);
        this.labDrive.Name = "labDrive";
        this.labDrive.Size = new System.Drawing.Size(35, 13);
        this.labDrive.TabIndex = 3;
        this.labDrive.Text = "Drive:";
        // labPlaylistFolder
        this.labPlaylistFolder.AutoSize = true;
        this.labPlaylistFolder.Location = new System.Drawing.Point(12, 31);
        this.labPlaylistFolder.Name = "labPlaylistFolder";
        this.labPlaylistFolder.Size = new System.Drawing.Size(74, 13);
        this.labPlaylistFolder.TabIndex = 4;
        this.labPlaylistFolder.Text = "Playlist Folder:";
        // txtPlaylistFolder
        this.txtPlaylistFolder.Location = new System.Drawing.Point(92, 28);
        this.txtPlaylistFolder.Name = "txtPlaylistFolder";
        this.txtPlaylistFolder.Size = new System.Drawing.Size(148, 20);
        this.txtPlaylistFolder.TabIndex = 5;
        // cmdBrowse
        this.cmdBrowse.Location = new System.Drawing.Point(246, 26);
        this.cmdBrowse.Name = "cmdBrowse";
        this.cmdBrowse.Size = new System.Drawing.Size(25, 23);
        this.cmdBrowse.TabIndex = 6;
        this.cmdBrowse.Text = "...";
        this.cmdBrowse.UseVisualStyleBackColor = true;
        // txtDriveLetter
        this.txtDriveLetter.Location = new System.Drawing.Point(92, 6);
        this.txtDriveLetter.Name = "txtDriveLetter";
        this.txtDriveLetter.Size = new System.Drawing.Size(26, 20);
        this.txtDriveLetter.TabIndex = 7;
        // dgvSongs
        this.dgvSongs.AllowUserToAddRows = false;
        this.dgvSongs.AllowUserToResizeRows = false;
        this.dgvSongs.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
        this.dgvSongs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvSongs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colFileName, this.colArtist, this.colTitle, this.colAlbum });
        this.dgvSongs.Location = new System.Drawing.Point(12, 55);
        this.dgvSongs.Name = "dgvSongs";
        this.dgvSongs.Size = new System.Drawing.Size(1094, 309);
        this.dgvSongs.TabIndex = 8;
        // colFileName
        this.colFileName.HeaderText = "FileName";
        this.colFileName.Name = "colFileName";
        this.colFileName.Width = 300;
        // colArtist
        this.colArtist.HeaderText = "Artist";
        this.colArtist.Name = "colArtist";
        this.colArtist.Width = 200;
        // colTitle
        this.colTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
        this.colTitle.HeaderText = "Title";
        this.colTitle.Name = "colTitle";
        // colAlbum
        this.colAlbum.HeaderText = "Album";
        this.colAlbum.Name = "colAlbum";
        this.colAlbum.Width = 200;
        // cmdLoad
        this.cmdLoad.Location = new System.Drawing.Point(567, 26);
        this.cmdLoad.Name = "cmdLoad";
        this.cmdLoad.Size = new System.Drawing.Size(75, 23);
        this.cmdLoad.TabIndex = 9;
        this.cmdLoad.Text = "Load";
        this.cmdLoad.UseVisualStyleBackColor = true;
        // labStatus
        this.labStatus.AutoSize = true;
        this.labStatus.Location = new System.Drawing.Point(564, 56);
        this.labStatus.Name = "labStatus";
        this.labStatus.Size = new System.Drawing.Size(37, 13);
        this.labStatus.TabIndex = 10;
        this.labStatus.Text = "Status";
        this.labStatus.Visible = false;
        // txtFilter
        this.txtFilter.Location = new System.Drawing.Point(689, 28);
        this.txtFilter.Name = "txtFilter";
        this.txtFilter.Size = new System.Drawing.Size(89, 20);
        this.txtFilter.TabIndex = 12;
        this.txtFilter.Text = "*.mp3";
        // labFilter
        this.labFilter.AutoSize = true;
        this.labFilter.Location = new System.Drawing.Point(651, 31);
        this.labFilter.Name = "labFilter";
        this.labFilter.Size = new System.Drawing.Size(32, 13);
        this.labFilter.TabIndex = 11;
        this.labFilter.Text = "Filter:";
        // cmdShowUnclear
        this.cmdShowUnclear.Location = new System.Drawing.Point(477, 26);
        this.cmdShowUnclear.Name = "cmdShowUnclear";
        this.cmdShowUnclear.Size = new System.Drawing.Size(84, 23);
        this.cmdShowUnclear.TabIndex = 13;
        this.cmdShowUnclear.Text = "Show Unclear";
        this.cmdShowUnclear.UseVisualStyleBackColor = true;
        // optV1
        this.optV1.AutoSize = true;
        this.optV1.Location = new System.Drawing.Point(567, 9);
        this.optV1.Name = "optV1";
        this.optV1.Size = new System.Drawing.Size(38, 17);
        this.optV1.TabIndex = 14;
        this.optV1.Text = "V1";
        this.optV1.UseVisualStyleBackColor = true;
        // optV2
        this.optV2.AutoSize = true;
        this.optV2.Checked = true;
        this.optV2.Location = new System.Drawing.Point(611, 9);
        this.optV2.Name = "optV2";
        this.optV2.Size = new System.Drawing.Size(38, 17);
        this.optV2.TabIndex = 15;
        this.optV2.TabStop = true;
        this.optV2.Text = "V2";
        this.optV2.UseVisualStyleBackColor = true;
        // txtFolder
        this.txtFolder.Location = new System.Drawing.Point(124, 6);
        this.txtFolder.Name = "txtFolder";
        this.txtFolder.Size = new System.Drawing.Size(116, 20);
        this.txtFolder.TabIndex = 16;
        // frmMaster
        this.AutoScaleDimensions = new System.Drawing.SizeF(6.0f, 13.0f);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1118, 392);
        this.Controls.Add(this.txtFolder);
        this.Controls.Add(this.optV2);
        this.Controls.Add(this.optV1);
        this.Controls.Add(this.cmdShowUnclear);
        this.Controls.Add(this.txtFilter);
        this.Controls.Add(this.labFilter);
        this.Controls.Add(this.labStatus);
        this.Controls.Add(this.cmdLoad);
        this.Controls.Add(this.dgvSongs);
        this.Controls.Add(this.txtDriveLetter);
        this.Controls.Add(this.cmdBrowse);
        this.Controls.Add(this.txtPlaylistFolder);
        this.Controls.Add(this.labPlaylistFolder);
        this.Controls.Add(this.labDrive);
        this.Controls.Add(this.cmdCreateCSV);
        this.Controls.Add(this.cmdUpdate);
        this.Name = "frmMaster";
        this.Text = "MP3 Management";
        ((ISupportInitialize)this.dgvSongs).EndInit();
        this.Load += new EventHandler(Form1_Load);
        this.DragDrop += new DragEventHandler(Form1_DragDrop);
        this.DragEnter += new DragEventHandler(Form1_DragEnter);
        this.cmdUpdate.Click += new EventHandler(cmdUpdate_Click);
        this.cmdCreateCSV.Click += new EventHandler(cmdCreateCSV_Click);
        this.cmdBrowse.Click += new EventHandler(cmdBrowse_Click);
        this.txtDriveLetter.TextChanged += new EventHandler(txtDriveLetter_TextChanged);
        this.cmdLoad.Click += new EventHandler(cmdLoad_Click);
        this.dgvSongs.CellDoubleClick += new DataGridViewCellEventHandler(dgvSongs_CellDoubleClick);
        this.dgvSongs.CellEndEdit += new DataGridViewCellEventHandler(dgvSongs_CellEndEdit);
        this.cmdShowUnclear.Click += new EventHandler(cmdShowUnclear_Click);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    internal System.Windows.Forms.Button cmdUpdate;
    internal System.Windows.Forms.Button cmdCreateCSV;
    internal System.Windows.Forms.Label labDrive;
    internal System.Windows.Forms.Label labPlaylistFolder;
    internal System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1;
    internal System.Windows.Forms.TextBox txtPlaylistFolder;
    internal System.Windows.Forms.Button cmdBrowse;
    internal System.Windows.Forms.TextBox txtDriveLetter;
    internal System.Windows.Forms.DataGridView dgvSongs;
    internal System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
    internal System.Windows.Forms.DataGridViewTextBoxColumn colArtist;
    internal System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
    internal System.Windows.Forms.DataGridViewTextBoxColumn colAlbum;
    internal System.Windows.Forms.Button cmdLoad;
    internal System.Windows.Forms.Label labStatus;
    internal System.Windows.Forms.TextBox txtFilter;
    internal System.Windows.Forms.Label labFilter;
    internal System.Windows.Forms.Button cmdShowUnclear;
    internal System.Windows.Forms.RadioButton optV1;
    internal System.Windows.Forms.RadioButton optV2;
    internal System.Windows.Forms.TextBox txtFolder;
}
