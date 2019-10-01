

namespace Sara.LogReader.WinForm.Views.Document
{
    partial class DocumentWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentWindow));
            this.contextMenuTabPage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.followNetworkMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scBase = new System.Windows.Forms.SplitContainer();
            this.sourceInfo = new Sara.LogReader.WinForm.Controls.SourceInfo();
            this.fctbDocument = new FastColoredTextBoxNS.FastColoredTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            this.contextMenuTabPage.SuspendLayout();
            this.PopupMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scBase)).BeginInit();
            this.scBase.Panel1.SuspendLayout();
            this.scBase.Panel2.SuspendLayout();
            this.scBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fctbDocument)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuTabPage
            // 
            this.contextMenuTabPage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem3,
            this.toolStripMenuItem1,
            this.closeAllToolStripMenuItem});
            this.contextMenuTabPage.Name = "contextMenuTabPage";
            this.contextMenuTabPage.Size = new System.Drawing.Size(192, 70);
            // 
            // menuItem3
            // 
            this.menuItem3.Name = "menuItem3";
            this.menuItem3.Size = new System.Drawing.Size(191, 22);
            this.menuItem3.Text = "&Close";
            this.menuItem3.Click += new System.EventHandler(this.Close_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(191, 22);
            this.toolStripMenuItem1.Text = "Close All But This One";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.CloseAllButThisOne_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.closeAllToolStripMenuItem.Text = "Close All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAll_Click);
            // 
            // PopupMenu
            // 
            this.PopupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.followNetworkMessageToolStripMenuItem});
            this.PopupMenu.Name = "PopupMenu";
            this.PopupMenu.Size = new System.Drawing.Size(207, 26);
            // 
            // followNetworkMessageToolStripMenuItem
            // 
            this.followNetworkMessageToolStripMenuItem.Name = "followNetworkMessageToolStripMenuItem";
            this.followNetworkMessageToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.followNetworkMessageToolStripMenuItem.Text = "Follow Network Message";
            this.followNetworkMessageToolStripMenuItem.Click += new System.EventHandler(this.followNetworkMessageToolStripMenuItem_Click);
            // 
            // scBase
            // 
            this.scBase.BackColor = System.Drawing.SystemColors.ControlDark;
            this.scBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scBase.Location = new System.Drawing.Point(0, 4);
            this.scBase.Name = "scBase";
            this.scBase.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scBase.Panel1
            // 
            this.scBase.Panel1.Controls.Add(this.sourceInfo);
            // 
            // scBase.Panel2
            // 
            this.scBase.Panel2.Controls.Add(this.fctbDocument);
            this.scBase.Size = new System.Drawing.Size(851, 796);
            this.scBase.SplitterDistance = 246;
            this.scBase.TabIndex = 12;
            // 
            // sourceInfo
            // 
            this.sourceInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceInfo.Location = new System.Drawing.Point(0, 0);
            this.sourceInfo.Name = "sourceInfo";
            this.sourceInfo.Size = new System.Drawing.Size(851, 246);
            this.sourceInfo.TabIndex = 0;
            this.sourceInfo.GoTo += new System.Action<Sara.LogReader.Model.FileNS.ValueBookMark>(this.sourceInfo_GoTo);
            // 
            // fctbDocument
            // 
            this.fctbDocument.AutoCompleteBracketsList = new char[0];
            this.fctbDocument.AutoScrollMinSize = new System.Drawing.Size(37, 23);
            this.fctbDocument.BackBrush = null;
            this.fctbDocument.CharHeight = 23;
            this.fctbDocument.CharWidth = 13;
            this.fctbDocument.CurrentPenSize = 3;
            this.fctbDocument.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctbDocument.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fctbDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fctbDocument.DocumentPath = null;
            this.fctbDocument.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fctbDocument.IsReplaceMode = false;
            this.fctbDocument.Location = new System.Drawing.Point(0, 0);
            this.fctbDocument.Name = "fctbDocument";
            this.fctbDocument.Paddings = new System.Windows.Forms.Padding(0);
            this.fctbDocument.ReadOnly = true;
            this.fctbDocument.SelectionChangedDelayedEnabled = true;
            this.fctbDocument.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctbDocument.Size = new System.Drawing.Size(851, 546);
            this.fctbDocument.TabIndex = 2;
            this.fctbDocument.Zoom = 100;
            this.fctbDocument.SelectionChangedDelayed += new System.EventHandler(this.fctbDocument_SelectionChangedDelayed);
            this.fctbDocument.Click += new System.EventHandler(this.fctbDocument_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // StatusPanel
            // 
            this.StatusPanel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.StatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusPanel.Location = new System.Drawing.Point(225, 350);
            this.StatusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Padding = new System.Windows.Forms.Padding(1);
            this.StatusPanel.Size = new System.Drawing.Size(400, 100);
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = false;
            this.StatusPanel.SP_Enabled = true;
            this.StatusPanel.SP_FullScreen = true;
            this.StatusPanel.TabIndex = 13;
            this.StatusPanel.Visible = false;
            // 
            // DocumentWindow
            // 
            this.ClientSize = new System.Drawing.Size(851, 800);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.scBase);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "DocumentWindow";
            this.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.TabPageContextMenuStrip = this.contextMenuTabPage;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Document_FormClosed);
            this.Shown += new System.EventHandler(this.DocumentWindow_Shown);
            this.Enter += new System.EventHandler(this.Document_Enter);
            this.contextMenuTabPage.ResumeLayout(false);
            this.PopupMenu.ResumeLayout(false);
            this.scBase.Panel1.ResumeLayout(false);
            this.scBase.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scBase)).EndInit();
            this.scBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fctbDocument)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuTabPage;
        private System.Windows.Forms.ToolStripMenuItem menuItem3;
        private System.Windows.Forms.ToolTip toolTip;
        private string _mFileName = string.Empty;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip PopupMenu;
        private System.Windows.Forms.ToolStripMenuItem followNetworkMessageToolStripMenuItem;
        private System.Windows.Forms.SplitContainer scBase;
        private FastColoredTextBoxNS.FastColoredTextBox fctbDocument;
        private Controls.SourceInfo sourceInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;
    }
}