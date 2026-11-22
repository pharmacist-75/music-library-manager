using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public partial class FrmPlaylist
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
        this.labPlaylist = new System.Windows.Forms.Label();
        this.cmbPlaylist = new System.Windows.Forms.ComboBox();
        this.dgvSongs = new System.Windows.Forms.DataGridView();
        this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colArtist = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colAlbum = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.cmdOk = new System.Windows.Forms.Button();
        this.cmdCancel = new System.Windows.Forms.Button();
        this.chkMerge = new System.Windows.Forms.CheckBox();
        this.labFileNotFould = new System.Windows.Forms.Label();
        this.labDuplicatedEntry = new System.Windows.Forms.Label();
        this.labPreviouslySaved = new System.Windows.Forms.Label();
        this.labNewEntry = new System.Windows.Forms.Label();
        this.cmdValidatePlaylists = new System.Windows.Forms.Button();
        ((ISupportInitialize)this.dgvSongs).BeginInit();
        this.SuspendLayout();
        // labPlaylist
        this.labPlaylist.AutoSize = true;
        this.labPlaylist.Location = new System.Drawing.Point(12, 15);
        this.labPlaylist.Name = "labPlaylist";
        this.labPlaylist.Size = new System.Drawing.Size(39, 13);
        this.labPlaylist.TabIndex = 0;
        this.labPlaylist.Text = "Playlist";
        // cmbPlaylist
        this.cmbPlaylist.FormattingEnabled = true;
        this.cmbPlaylist.Location = new System.Drawing.Point(98, 12);
        this.cmbPlaylist.Name = "cmbPlaylist";
        this.cmbPlaylist.Size = new System.Drawing.Size(235, 21);
        this.cmbPlaylist.Sorted = true;
        this.cmbPlaylist.TabIndex = 1;
        // dgvSongs
        this.dgvSongs.AllowUserToAddRows = false;
        this.dgvSongs.AllowUserToResizeRows = false;
        this.dgvSongs.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
        this.dgvSongs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvSongs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colFileName, this.colArtist, this.colTitle, this.colAlbum });
        this.dgvSongs.Location = new System.Drawing.Point(12, 55);
        this.dgvSongs.Name = "dgvSongs";
        this.dgvSongs.Size = new System.Drawing.Size(1094, 303);
        this.dgvSongs.TabIndex = 2;
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
        // cmdOk
        this.cmdOk.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
        this.cmdOk.Location = new System.Drawing.Point(1031, 10);
        this.cmdOk.Name = "cmdOk";
        this.cmdOk.Size = new System.Drawing.Size(75, 23);
        this.cmdOk.TabIndex = 3;
        this.cmdOk.Text = "OK";
        this.cmdOk.UseVisualStyleBackColor = true;
        // cmdCancel
        this.cmdCancel.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
        this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.cmdCancel.Location = new System.Drawing.Point(940, 10);
        this.cmdCancel.Name = "cmdCancel";
        this.cmdCancel.Size = new System.Drawing.Size(75, 23);
        this.cmdCancel.TabIndex = 4;
        this.cmdCancel.Text = "Cancel";
        this.cmdCancel.UseVisualStyleBackColor = true;
        // chkMerge
        this.chkMerge.AutoSize = true;
        this.chkMerge.Location = new System.Drawing.Point(339, 14);
        this.chkMerge.Name = "chkMerge";
        this.chkMerge.Size = new System.Drawing.Size(149, 17);
        this.chkMerge.TabIndex = 5;
        this.chkMerge.Text = "merge with existing playlist";
        this.chkMerge.UseVisualStyleBackColor = true;
        // labFileNotFould
        this.labFileNotFould.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
        this.labFileNotFould.AutoSize = true;
        this.labFileNotFould.ForeColor = System.Drawing.Color.LightGray;
        this.labFileNotFould.Location = new System.Drawing.Point(356, 361);
        this.labFileNotFould.Name = "labFileNotFould";
        this.labFileNotFould.Size = new System.Drawing.Size(92, 13);
        this.labFileNotFould.TabIndex = 6;
        this.labFileNotFould.Text = "MP3-File not fould";
        // labDuplicatedEntry
        this.labDuplicatedEntry.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
        this.labDuplicatedEntry.AutoSize = true;
        this.labDuplicatedEntry.ForeColor = System.Drawing.Color.Red;
        this.labDuplicatedEntry.Location = new System.Drawing.Point(268, 361);
        this.labDuplicatedEntry.Name = "labDuplicatedEntry";
        this.labDuplicatedEntry.Size = new System.Drawing.Size(82, 13);
        this.labDuplicatedEntry.TabIndex = 7;
        this.labDuplicatedEntry.Text = "duplicated entry";
        // labPreviouslySaved
        this.labPreviouslySaved.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
        this.labPreviouslySaved.AutoSize = true;
        this.labPreviouslySaved.ForeColor = System.Drawing.Color.Blue;
        this.labPreviouslySaved.Location = new System.Drawing.Point(111, 361);
        this.labPreviouslySaved.Name = "labPreviouslySaved";
        this.labPreviouslySaved.Size = new System.Drawing.Size(151, 13);
        this.labPreviouslySaved.TabIndex = 8;
        this.labPreviouslySaved.Text = "previously saved to this playlist";
        // labNewEntry
        this.labNewEntry.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
        this.labNewEntry.AutoSize = true;
        this.labNewEntry.ForeColor = System.Drawing.Color.Green;
        this.labNewEntry.Location = new System.Drawing.Point(12, 361);
        this.labNewEntry.Name = "labNewEntry";
        this.labNewEntry.Size = new System.Drawing.Size(93, 13);
        this.labNewEntry.TabIndex = 9;
        this.labNewEntry.Text = "New song to save";
        // cmdValidatePlaylists
        this.cmdValidatePlaylists.Location = new System.Drawing.Point(640, 10);
        this.cmdValidatePlaylists.Name = "cmdValidatePlaylists";
        this.cmdValidatePlaylists.Size = new System.Drawing.Size(115, 23);
        this.cmdValidatePlaylists.TabIndex = 14;
        this.cmdValidatePlaylists.Text = "Validate Playlists";
        this.cmdValidatePlaylists.UseVisualStyleBackColor = true;
        // frmPlaylist
        this.AcceptButton = this.cmdOk;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6.0f, 13.0f);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.CancelButton = this.cmdCancel;
        this.ClientSize = new System.Drawing.Size(1118, 392);
        this.Controls.Add(this.cmdValidatePlaylists);
        this.Controls.Add(this.labNewEntry);
        this.Controls.Add(this.labPreviouslySaved);
        this.Controls.Add(this.labDuplicatedEntry);
        this.Controls.Add(this.labFileNotFould);
        this.Controls.Add(this.chkMerge);
        this.Controls.Add(this.cmdCancel);
        this.Controls.Add(this.cmdOk);
        this.Controls.Add(this.dgvSongs);
        this.Controls.Add(this.cmbPlaylist);
        this.Controls.Add(this.labPlaylist);
        this.Name = "frmPlaylist";
        this.Text = "Playlist";
        ((ISupportInitialize)this.dgvSongs).EndInit();
        this.Load += new EventHandler(FrmPlaylist_Load);
        this.cmdCancel.Click += new EventHandler(CmdCancel_Click);
        this.cmdOk.Click += new EventHandler(CmdOk_Click);
        this.cmbPlaylist.SelectedIndexChanged += new EventHandler(CmbPlaylist_SelectedIndexChanged);
        this.chkMerge.CheckedChanged += new EventHandler(ChkMerge_CheckedChanged);
        this.cmdValidatePlaylists.Click += new EventHandler(CmdValidatePlaylists_Click);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    internal System.Windows.Forms.Label labPlaylist;
    internal System.Windows.Forms.ComboBox cmbPlaylist;
    internal System.Windows.Forms.DataGridView dgvSongs;
    internal System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
    internal System.Windows.Forms.DataGridViewTextBoxColumn colArtist;
    internal System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
    internal System.Windows.Forms.DataGridViewTextBoxColumn colAlbum;
    internal System.Windows.Forms.Button cmdOk;
    internal System.Windows.Forms.Button cmdCancel;
    internal System.Windows.Forms.CheckBox chkMerge;
    internal System.Windows.Forms.Label labFileNotFould;
    internal System.Windows.Forms.Label labDuplicatedEntry;
    internal System.Windows.Forms.Label labPreviouslySaved;
    internal System.Windows.Forms.Label labNewEntry;
    internal System.Windows.Forms.Button cmdValidatePlaylists;
}
