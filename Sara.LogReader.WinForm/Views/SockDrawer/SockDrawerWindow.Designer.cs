
using Sara.LogReader.WinForm.Controls;

namespace Sara.LogReader.WinForm.Views.SockDrawer
{
    partial class SockDrawerWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SockDrawerWindow));
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.tsslLineCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTotalFiles = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            this.tvFiles = new Sara.WinForm.ControlsNS.BufferedTreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddFavoritetoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFavoriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.ckbShowBuildInfo = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hideDetailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUpdateAllForced = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUpdateSelectedGroupForced = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUpdateActiveForced = new System.Windows.Forms.ToolStripMenuItem();
            this.onlyNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUpdateAllNew = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUpdateSelectedGroupNew = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUpdateActiveNew = new System.Windows.Forms.ToolStripMenuItem();
            this.removeDuplicateFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslLineCount,
            this.tsslPath,
            this.tsslTotalFiles});
            this.statusBar.Location = new System.Drawing.Point(1, 395);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(658, 22);
            this.statusBar.TabIndex = 8;
            // 
            // tsslLineCount
            // 
            this.tsslLineCount.Name = "tsslLineCount";
            this.tsslLineCount.Size = new System.Drawing.Size(0, 17);
            // 
            // tsslPath
            // 
            this.tsslPath.Name = "tsslPath";
            this.tsslPath.Size = new System.Drawing.Size(574, 17);
            this.tsslPath.Spring = true;
            this.tsslPath.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // tsslTotalFiles
            // 
            this.tsslTotalFiles.Name = "tsslTotalFiles";
            this.tsslTotalFiles.Size = new System.Drawing.Size(69, 17);
            this.tsslTotalFiles.Text = "Total Files #";
            // 
            // StatusPanel
            // 
            this.StatusPanel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.StatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusPanel.Location = new System.Drawing.Point(130, 159);
            this.StatusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Padding = new System.Windows.Forms.Padding(1);
            this.StatusPanel.Size = new System.Drawing.Size(400, 100);
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = true;
            this.StatusPanel.SP_Enabled = true;
            this.StatusPanel.SP_FullScreen = true;
            this.StatusPanel.TabIndex = 10;
            this.StatusPanel.Visible = false;
            // 
            // tvFiles
            // 
            this.tvFiles.ContextMenuStrip = this.contextMenuStrip1;
            this.tvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFiles.HideSelection = false;
            this.tvFiles.Location = new System.Drawing.Point(1, 42);
            this.tvFiles.Name = "tvFiles";
            this.tvFiles.Size = new System.Drawing.Size(658, 353);
            this.tvFiles.TabIndex = 2;
            this.tvFiles.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvFiles_AfterExpand);
            this.tvFiles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFiles_AfterSelect);
            this.tvFiles.DoubleClick += new System.EventHandler(this.tvFiles_DoubleClick);
            this.tvFiles.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tvFiles_KeyPress);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddFavoritetoolStripMenuItem2,
            this.removeFavoriteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(163, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // AddFavoritetoolStripMenuItem2
            // 
            this.AddFavoritetoolStripMenuItem2.Name = "AddFavoritetoolStripMenuItem2";
            this.AddFavoritetoolStripMenuItem2.Size = new System.Drawing.Size(162, 22);
            this.AddFavoritetoolStripMenuItem2.Text = "Add to Favorite";
            this.AddFavoritetoolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // removeFavoriteToolStripMenuItem
            // 
            this.removeFavoriteToolStripMenuItem.Name = "removeFavoriteToolStripMenuItem";
            this.removeFavoriteToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.removeFavoriteToolStripMenuItem.Text = "Remove Favorite";
            this.removeFavoriteToolStripMenuItem.Click += new System.EventHandler(this.removeFavoriteToolStripMenuItem_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.SystemColors.Control;
            this.pnlTop.Controls.Add(this.ckbShowBuildInfo);
            this.pnlTop.Controls.Add(this.pictureBox2);
            this.pnlTop.Controls.Add(this.pictureBox1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(1, 25);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(658, 17);
            this.pnlTop.TabIndex = 1;
            // 
            // ckbShowBuildInfo
            // 
            this.ckbShowBuildInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbShowBuildInfo.AutoSize = true;
            this.ckbShowBuildInfo.Location = new System.Drawing.Point(559, 1);
            this.ckbShowBuildInfo.Name = "ckbShowBuildInfo";
            this.ckbShowBuildInfo.Size = new System.Drawing.Size(100, 17);
            this.ckbShowBuildInfo.TabIndex = 22;
            this.ckbShowBuildInfo.Text = "Show Build Info";
            this.ckbShowBuildInfo.UseVisualStyleBackColor = true;
            this.ckbShowBuildInfo.CheckedChanged += new System.EventHandler(this.ckbStatics_CheckedChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Sara.LogReader.WinForm.Properties.Resources.CollaspeAll;
            this.pictureBox2.Location = new System.Drawing.Point(30, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(14, 14);
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(14, 14);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideDetailToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(1, 1);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(658, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // hideDetailToolStripMenuItem
            // 
            this.hideDetailToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSettings,
            this.toolStripMenuItem1,
            this.onlyNewToolStripMenuItem,
            this.removeDuplicateFilesToolStripMenuItem});
            this.hideDetailToolStripMenuItem.Name = "hideDetailToolStripMenuItem";
            this.hideDetailToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.hideDetailToolStripMenuItem.Text = "Options";
            // 
            // btnSettings
            // 
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(196, 22);
            this.btnSettings.Text = "Settings";
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUpdateAllForced,
            this.btnUpdateSelectedGroupForced,
            this.btnUpdateActiveForced});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(196, 22);
            this.toolStripMenuItem1.Text = "Update All";
            // 
            // btnUpdateAllForced
            // 
            this.btnUpdateAllForced.Name = "btnUpdateAllForced";
            this.btnUpdateAllForced.Size = new System.Drawing.Size(166, 22);
            this.btnUpdateAllForced.Text = "Sock Drawer";
            this.btnUpdateAllForced.Click += new System.EventHandler(this.updateSockDrawerToolStripMenuItem_Click);
            // 
            // btnUpdateSelectedGroupForced
            // 
            this.btnUpdateSelectedGroupForced.Name = "btnUpdateSelectedGroupForced";
            this.btnUpdateSelectedGroupForced.Size = new System.Drawing.Size(166, 22);
            this.btnUpdateSelectedGroupForced.Text = "Selected Group";
            this.btnUpdateSelectedGroupForced.Click += new System.EventHandler(this.btnUpdateSelectedGroupForced_Click);
            // 
            // btnUpdateActiveForced
            // 
            this.btnUpdateActiveForced.Name = "btnUpdateActiveForced";
            this.btnUpdateActiveForced.Size = new System.Drawing.Size(166, 22);
            this.btnUpdateActiveForced.Text = "Active Document";
            this.btnUpdateActiveForced.Click += new System.EventHandler(this.btnUpdateActiveForced_Click);
            // 
            // onlyNewToolStripMenuItem
            // 
            this.onlyNewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUpdateAllNew,
            this.btnUpdateSelectedGroupNew,
            this.btnUpdateActiveNew});
            this.onlyNewToolStripMenuItem.Name = "onlyNewToolStripMenuItem";
            this.onlyNewToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.onlyNewToolStripMenuItem.Text = "Update New";
            // 
            // btnUpdateAllNew
            // 
            this.btnUpdateAllNew.Name = "btnUpdateAllNew";
            this.btnUpdateAllNew.Size = new System.Drawing.Size(166, 22);
            this.btnUpdateAllNew.Text = "Sock Drawer";
            this.btnUpdateAllNew.Click += new System.EventHandler(this.btnUpdateAllNew_Click);
            // 
            // btnUpdateSelectedGroupNew
            // 
            this.btnUpdateSelectedGroupNew.Name = "btnUpdateSelectedGroupNew";
            this.btnUpdateSelectedGroupNew.Size = new System.Drawing.Size(166, 22);
            this.btnUpdateSelectedGroupNew.Text = "Selected Group";
            this.btnUpdateSelectedGroupNew.Click += new System.EventHandler(this.btnUpdateSelectedGroupNew_Click);
            // 
            // btnUpdateActiveNew
            // 
            this.btnUpdateActiveNew.Name = "btnUpdateActiveNew";
            this.btnUpdateActiveNew.Size = new System.Drawing.Size(166, 22);
            this.btnUpdateActiveNew.Text = "Active Document";
            this.btnUpdateActiveNew.Click += new System.EventHandler(this.btnUpdateActiveNew_Click);
            // 
            // removeDuplicateFilesToolStripMenuItem
            // 
            this.removeDuplicateFilesToolStripMenuItem.Name = "removeDuplicateFilesToolStripMenuItem";
            this.removeDuplicateFilesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.removeDuplicateFilesToolStripMenuItem.Text = "Remove Duplicate Files";
            this.removeDuplicateFilesToolStripMenuItem.Click += new System.EventHandler(this.removeDuplicateFilesToolStripMenuItem_Click);
            // 
            // SockDrawerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(660, 418);
            this.Controls.Add(this.tvFiles);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.menuStrip1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SockDrawerWindow";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight;
            this.TabText = "Sock Drawer";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SockDrawerWindow_Paint);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel tsslLineCount;
        private System.Windows.Forms.ToolStripStatusLabel tsslPath;
        private System.Windows.Forms.ToolStripStatusLabel tsslTotalFiles;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;
        private Sara.WinForm.ControlsNS.BufferedTreeView tvFiles;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hideDetailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnSettings;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem btnUpdateAllForced;
        private System.Windows.Forms.ToolStripMenuItem btnUpdateSelectedGroupForced;
        private System.Windows.Forms.ToolStripMenuItem btnUpdateActiveForced;
        private System.Windows.Forms.ToolStripMenuItem onlyNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnUpdateAllNew;
        private System.Windows.Forms.ToolStripMenuItem btnUpdateSelectedGroupNew;
        private System.Windows.Forms.ToolStripMenuItem btnUpdateActiveNew;
        private System.Windows.Forms.ToolStripMenuItem removeDuplicateFilesToolStripMenuItem;
        private System.Windows.Forms.CheckBox ckbShowBuildInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeFavoriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddFavoritetoolStripMenuItem2;
    }
}