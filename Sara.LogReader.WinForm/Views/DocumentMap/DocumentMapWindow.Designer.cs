

namespace Sara.LogReader.WinForm.Views.DocumentMap
{
    partial class DocumentMapWindow
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SplitView = new System.Windows.Forms.SplitContainer();
            this.tvMapEntries = new Sara.WinForm.ControlsNS.BufferedTreeView();
            this.tvMapEntriesSplit = new Sara.WinForm.ControlsNS.BufferedTreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hbPatterns = new Sara.WinForm.ControlsNS.HighlightBar();
            this.hbEvents = new Sara.WinForm.ControlsNS.HighlightBar();
            this.hbNetwork = new Sara.WinForm.ControlsNS.HighlightBar();
            this.hbValues = new Sara.WinForm.ControlsNS.HighlightBar();
            this.hbTimeGap = new Sara.WinForm.ControlsNS.HighlightBar();
            this.ckbOverlay = new System.Windows.Forms.CheckBox();
            this.ckbMapGap = new System.Windows.Forms.CheckBox();
            this.btnApplyGap = new System.Windows.Forms.Button();
            this.ntbGapDuration = new Sara.WinForm.ControlsNS.NumericTextBox();
            this.ckbFilter = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ckbSyncScroll = new System.Windows.Forms.CheckBox();
            this.ckbSplitView = new System.Windows.Forms.CheckBox();
            this.ckbAll = new System.Windows.Forms.CheckBox();
            this.ckbPatterns = new System.Windows.Forms.CheckBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.ckbEvents = new System.Windows.Forms.CheckBox();
            this.ckbValue = new System.Windows.Forms.CheckBox();
            this.ckbNetwork = new System.Windows.Forms.CheckBox();
            this.ckbTimeGap = new System.Windows.Forms.CheckBox();
            this.wpOpen = new Sara.WinForm.ControlsNS.WarningPanel();
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitView)).BeginInit();
            this.SplitView.Panel1.SuspendLayout();
            this.SplitView.Panel2.SuspendLayout();
            this.SplitView.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 150);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1179, 378);
            this.panel3.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.SplitView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 73);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1179, 305);
            this.panel2.TabIndex = 6;
            // 
            // SplitView
            // 
            this.SplitView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitView.Location = new System.Drawing.Point(0, 0);
            this.SplitView.Name = "SplitView";
            // 
            // SplitView.Panel1
            // 
            this.SplitView.Panel1.Controls.Add(this.tvMapEntries);
            // 
            // SplitView.Panel2
            // 
            this.SplitView.Panel2.Controls.Add(this.tvMapEntriesSplit);
            this.SplitView.Size = new System.Drawing.Size(1179, 305);
            this.SplitView.SplitterDistance = 393;
            this.SplitView.TabIndex = 0;
            // 
            // tvMapEntries
            // 
            this.tvMapEntries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMapEntries.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvMapEntries.HideSelection = false;
            this.tvMapEntries.LineColor = System.Drawing.Color.White;
            this.tvMapEntries.Location = new System.Drawing.Point(0, 0);
            this.tvMapEntries.Name = "tvMapEntries";
            this.tvMapEntries.Size = new System.Drawing.Size(393, 305);
            this.tvMapEntries.TabIndex = 2;
            this.tvMapEntries.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tvMapEntries_DrawNode);
            this.tvMapEntries.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMapEntries_AfterSelect);
            this.tvMapEntries.DoubleClick += new System.EventHandler(this.tvMapEntries_DoubleClick);
            this.tvMapEntries.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvMapEntries_KeyDown);
            this.tvMapEntries.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tvMapEntries_KeyPress);
            // 
            // tvMapEntriesSplit
            // 
            this.tvMapEntriesSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMapEntriesSplit.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvMapEntriesSplit.HideSelection = false;
            this.tvMapEntriesSplit.LineColor = System.Drawing.Color.White;
            this.tvMapEntriesSplit.Location = new System.Drawing.Point(0, 0);
            this.tvMapEntriesSplit.Name = "tvMapEntriesSplit";
            this.tvMapEntriesSplit.Size = new System.Drawing.Size(782, 305);
            this.tvMapEntriesSplit.TabIndex = 3;
            this.tvMapEntriesSplit.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tvMapEntries_DrawNode);
            this.tvMapEntriesSplit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvMapEntries_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.hbPatterns);
            this.panel1.Controls.Add(this.hbEvents);
            this.panel1.Controls.Add(this.hbNetwork);
            this.panel1.Controls.Add(this.hbValues);
            this.panel1.Controls.Add(this.hbTimeGap);
            this.panel1.Controls.Add(this.ckbOverlay);
            this.panel1.Controls.Add(this.ckbMapGap);
            this.panel1.Controls.Add(this.btnApplyGap);
            this.panel1.Controls.Add(this.ntbGapDuration);
            this.panel1.Controls.Add(this.ckbFilter);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.ckbSyncScroll);
            this.panel1.Controls.Add(this.ckbSplitView);
            this.panel1.Controls.Add(this.ckbAll);
            this.panel1.Controls.Add(this.ckbPatterns);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.ckbEvents);
            this.panel1.Controls.Add(this.ckbValue);
            this.panel1.Controls.Add(this.ckbNetwork);
            this.panel1.Controls.Add(this.ckbTimeGap);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1179, 73);
            this.panel1.TabIndex = 5;
            // 
            // hbPatterns
            // 
            this.hbPatterns.BackColor = System.Drawing.Color.Black;
            this.hbPatterns.HighlightColor = System.Drawing.Color.Lime;
            this.hbPatterns.Location = new System.Drawing.Point(90, 6);
            this.hbPatterns.Name = "hbPatterns";
            this.hbPatterns.Padding = new System.Windows.Forms.Padding(1);
            this.hbPatterns.Size = new System.Drawing.Size(6, 13);
            this.hbPatterns.TabIndex = 33;
            // 
            // hbEvents
            // 
            this.hbEvents.BackColor = System.Drawing.Color.Black;
            this.hbEvents.HighlightColor = System.Drawing.Color.Blue;
            this.hbEvents.Location = new System.Drawing.Point(8, 56);
            this.hbEvents.Name = "hbEvents";
            this.hbEvents.Padding = new System.Windows.Forms.Padding(1);
            this.hbEvents.Size = new System.Drawing.Size(6, 13);
            this.hbEvents.TabIndex = 32;
            // 
            // hbNetwork
            // 
            this.hbNetwork.BackColor = System.Drawing.Color.Black;
            this.hbNetwork.HighlightColor = System.Drawing.Color.Yellow;
            this.hbNetwork.Location = new System.Drawing.Point(8, 40);
            this.hbNetwork.Name = "hbNetwork";
            this.hbNetwork.Padding = new System.Windows.Forms.Padding(1);
            this.hbNetwork.Size = new System.Drawing.Size(6, 13);
            this.hbNetwork.TabIndex = 31;
            // 
            // hbValues
            // 
            this.hbValues.BackColor = System.Drawing.Color.Black;
            this.hbValues.HighlightColor = System.Drawing.Color.Red;
            this.hbValues.Location = new System.Drawing.Point(8, 23);
            this.hbValues.Name = "hbValues";
            this.hbValues.Padding = new System.Windows.Forms.Padding(1);
            this.hbValues.Size = new System.Drawing.Size(6, 13);
            this.hbValues.TabIndex = 30;
            // 
            // hbTimeGap
            // 
            this.hbTimeGap.BackColor = System.Drawing.Color.Black;
            this.hbTimeGap.HighlightColor = System.Drawing.Color.White;
            this.hbTimeGap.Location = new System.Drawing.Point(8, 6);
            this.hbTimeGap.Name = "hbTimeGap";
            this.hbTimeGap.Padding = new System.Windows.Forms.Padding(1);
            this.hbTimeGap.Size = new System.Drawing.Size(6, 13);
            this.hbTimeGap.TabIndex = 29;
            // 
            // ckbOverlay
            // 
            this.ckbOverlay.AutoSize = true;
            this.ckbOverlay.Location = new System.Drawing.Point(169, 39);
            this.ckbOverlay.Name = "ckbOverlay";
            this.ckbOverlay.Size = new System.Drawing.Size(62, 17);
            this.ckbOverlay.TabIndex = 28;
            this.ckbOverlay.Tag = "";
            this.ckbOverlay.Text = "Overlay";
            this.ckbOverlay.UseVisualStyleBackColor = true;
            this.ckbOverlay.CheckedChanged += new System.EventHandler(this.ckbOverlay_CheckedChanged);
            // 
            // ckbMapGap
            // 
            this.ckbMapGap.AutoSize = true;
            this.ckbMapGap.Location = new System.Drawing.Point(169, 55);
            this.ckbMapGap.Name = "ckbMapGap";
            this.ckbMapGap.Size = new System.Drawing.Size(70, 17);
            this.ckbMapGap.TabIndex = 27;
            this.ckbMapGap.Tag = "";
            this.ckbMapGap.Text = "Map Gap";
            this.ckbMapGap.UseVisualStyleBackColor = true;
            // 
            // btnApplyGap
            // 
            this.btnApplyGap.Location = new System.Drawing.Point(264, 48);
            this.btnApplyGap.Name = "btnApplyGap";
            this.btnApplyGap.Size = new System.Drawing.Size(44, 23);
            this.btnApplyGap.TabIndex = 26;
            this.btnApplyGap.Text = "Apply";
            this.btnApplyGap.UseVisualStyleBackColor = true;
            this.btnApplyGap.Click += new System.EventHandler(this.btnApplyGap_Click);
            // 
            // ntbGapDuration
            // 
            this.ntbGapDuration.AllowSpace = false;
            this.ntbGapDuration.Location = new System.Drawing.Point(264, 25);
            this.ntbGapDuration.Name = "ntbGapDuration";
            this.ntbGapDuration.Size = new System.Drawing.Size(44, 20);
            this.ntbGapDuration.TabIndex = 25;
            this.ntbGapDuration.Text = "9999";
            // 
            // ckbFilter
            // 
            this.ckbFilter.AutoSize = true;
            this.ckbFilter.Location = new System.Drawing.Point(98, 55);
            this.ckbFilter.Name = "ckbFilter";
            this.ckbFilter.Size = new System.Drawing.Size(60, 17);
            this.ckbFilter.TabIndex = 24;
            this.ckbFilter.Tag = "";
            this.ckbFilter.Text = "Filtered";
            this.ckbFilter.UseVisualStyleBackColor = true;
            this.ckbFilter.CheckedChanged += new System.EventHandler(this.cbFilter_CheckedChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Sara.LogReader.WinForm.Properties.Resources.ExpandAll;
            this.pictureBox2.Location = new System.Drawing.Point(292, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.TabIndex = 23;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Sara.LogReader.WinForm.Properties.Resources.CollaspeAll;
            this.pictureBox1.Location = new System.Drawing.Point(270, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // ckbSyncScroll
            // 
            this.ckbSyncScroll.AutoSize = true;
            this.ckbSyncScroll.Location = new System.Drawing.Point(169, 22);
            this.ckbSyncScroll.Name = "ckbSyncScroll";
            this.ckbSyncScroll.Size = new System.Drawing.Size(79, 17);
            this.ckbSyncScroll.TabIndex = 21;
            this.ckbSyncScroll.Tag = "";
            this.ckbSyncScroll.Text = "Sync Scroll";
            this.ckbSyncScroll.UseVisualStyleBackColor = true;
            // 
            // ckbSplitView
            // 
            this.ckbSplitView.AutoSize = true;
            this.ckbSplitView.Location = new System.Drawing.Point(169, 6);
            this.ckbSplitView.Name = "ckbSplitView";
            this.ckbSplitView.Size = new System.Drawing.Size(102, 17);
            this.ckbSplitView.TabIndex = 20;
            this.ckbSplitView.Tag = "";
            this.ckbSplitView.Text = "Show Split View";
            this.ckbSplitView.UseVisualStyleBackColor = true;
            this.ckbSplitView.CheckedChanged += new System.EventHandler(this.ckbSplitView_CheckedChanged);
            // 
            // ckbAll
            // 
            this.ckbAll.AutoSize = true;
            this.ckbAll.Checked = true;
            this.ckbAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAll.Location = new System.Drawing.Point(98, 22);
            this.ckbAll.Name = "ckbAll";
            this.ckbAll.Size = new System.Drawing.Size(37, 17);
            this.ckbAll.TabIndex = 19;
            this.ckbAll.Tag = "";
            this.ckbAll.Text = "All";
            this.ckbAll.UseVisualStyleBackColor = true;
            this.ckbAll.CheckedChanged += new System.EventHandler(this.cbCheckAll_CheckedChanged);
            // 
            // ckbPatterns
            // 
            this.ckbPatterns.AutoSize = true;
            this.ckbPatterns.Checked = true;
            this.ckbPatterns.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbPatterns.Location = new System.Drawing.Point(98, 5);
            this.ckbPatterns.Name = "ckbPatterns";
            this.ckbPatterns.Size = new System.Drawing.Size(65, 17);
            this.ckbPatterns.TabIndex = 16;
            this.ckbPatterns.Tag = "";
            this.ckbPatterns.Text = "Patterns";
            this.ckbPatterns.UseVisualStyleBackColor = true;
            this.ckbPatterns.Click += new System.EventHandler(this.ckbPatterns_Click);
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Black;
            this.panel9.Location = new System.Drawing.Point(419, 22);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(0, 0);
            this.panel9.TabIndex = 12;
            // 
            // ckbEvents
            // 
            this.ckbEvents.AutoSize = true;
            this.ckbEvents.Checked = true;
            this.ckbEvents.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbEvents.Location = new System.Drawing.Point(18, 55);
            this.ckbEvents.Name = "ckbEvents";
            this.ckbEvents.Size = new System.Drawing.Size(59, 17);
            this.ckbEvents.TabIndex = 3;
            this.ckbEvents.Tag = "";
            this.ckbEvents.Text = "Events";
            this.ckbEvents.UseVisualStyleBackColor = true;
            this.ckbEvents.Click += new System.EventHandler(this.ckbHideEvents_Click);
            // 
            // ckbValue
            // 
            this.ckbValue.AutoSize = true;
            this.ckbValue.Checked = true;
            this.ckbValue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbValue.Location = new System.Drawing.Point(18, 22);
            this.ckbValue.Name = "ckbValue";
            this.ckbValue.Size = new System.Drawing.Size(58, 17);
            this.ckbValue.TabIndex = 2;
            this.ckbValue.Tag = "";
            this.ckbValue.Text = "Values";
            this.ckbValue.UseVisualStyleBackColor = true;
            this.ckbValue.Click += new System.EventHandler(this.ckbHideValue_Click);
            // 
            // ckbNetwork
            // 
            this.ckbNetwork.AutoSize = true;
            this.ckbNetwork.Checked = true;
            this.ckbNetwork.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbNetwork.Location = new System.Drawing.Point(18, 39);
            this.ckbNetwork.Name = "ckbNetwork";
            this.ckbNetwork.Size = new System.Drawing.Size(66, 17);
            this.ckbNetwork.TabIndex = 1;
            this.ckbNetwork.Tag = "";
            this.ckbNetwork.Text = "Network";
            this.ckbNetwork.UseVisualStyleBackColor = true;
            this.ckbNetwork.Click += new System.EventHandler(this.ckbHideNetwork_Click);
            // 
            // ckbTimeGap
            // 
            this.ckbTimeGap.AutoSize = true;
            this.ckbTimeGap.Checked = true;
            this.ckbTimeGap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbTimeGap.Location = new System.Drawing.Point(18, 5);
            this.ckbTimeGap.Name = "ckbTimeGap";
            this.ckbTimeGap.Size = new System.Drawing.Size(74, 17);
            this.ckbTimeGap.TabIndex = 0;
            this.ckbTimeGap.Tag = "";
            this.ckbTimeGap.Text = "Time GAP";
            this.ckbTimeGap.UseVisualStyleBackColor = true;
            this.ckbTimeGap.Click += new System.EventHandler(this.ckbHideTimeGap_Click);
            // 
            // wpOpen
            // 
            this.wpOpen.BackColor = System.Drawing.SystemColors.Control;
            this.wpOpen.Content = null;
            this.wpOpen.Dock = System.Windows.Forms.DockStyle.Top;
            this.wpOpen.Location = new System.Drawing.Point(0, 0);
            this.wpOpen.Name = "wpOpen";
            this.wpOpen.Size = new System.Drawing.Size(1179, 150);
            this.wpOpen.TabIndex = 19;
            this.wpOpen.WarningText = "Open a File";
            // 
            // StatusPanel
            // 
            this.StatusPanel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.StatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusPanel.Location = new System.Drawing.Point(299, 265);
            this.StatusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Padding = new System.Windows.Forms.Padding(1);
            this.StatusPanel.Size = new System.Drawing.Size(581, 104);
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = false;
            this.StatusPanel.SP_Enabled = true;
            this.StatusPanel.SP_FullScreen = true;
            this.StatusPanel.TabIndex = 22;
            this.StatusPanel.Visible = false;
            // 
            // DocumentMapWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 528);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.wpOpen);
            this.DoubleBuffered = true;
            this.HideOnClose = true;
            this.Name = "DocumentMapWindow";
            this.TabText = "Document Map";
            this.Text = "DocumentMap";
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.SplitView.Panel1.ResumeLayout(false);
            this.SplitView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitView)).EndInit();
            this.SplitView.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox ckbEvents;
        private System.Windows.Forms.CheckBox ckbValue;
        private System.Windows.Forms.CheckBox ckbNetwork;
        private System.Windows.Forms.CheckBox ckbTimeGap;
        private Sara.WinForm.ControlsNS.WarningPanel wpOpen;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.CheckBox ckbPatterns;
        private System.Windows.Forms.CheckBox ckbAll;
        private System.Windows.Forms.CheckBox ckbSplitView;
        private System.Windows.Forms.SplitContainer SplitView;
        private Sara.WinForm.ControlsNS.BufferedTreeView tvMapEntries;
        private Sara.WinForm.ControlsNS.BufferedTreeView tvMapEntriesSplit;
        private System.Windows.Forms.CheckBox ckbSyncScroll;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox ckbFilter;
        private System.Windows.Forms.Button btnApplyGap;
        private Sara.WinForm.ControlsNS.NumericTextBox ntbGapDuration;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;
        private System.Windows.Forms.CheckBox ckbMapGap;
        private System.Windows.Forms.CheckBox ckbOverlay;
        private Sara.WinForm.ControlsNS.HighlightBar hbPatterns;
        private Sara.WinForm.ControlsNS.HighlightBar hbEvents;
        private Sara.WinForm.ControlsNS.HighlightBar hbNetwork;
        private Sara.WinForm.ControlsNS.HighlightBar hbValues;
        private Sara.WinForm.ControlsNS.HighlightBar hbTimeGap;
    }
}