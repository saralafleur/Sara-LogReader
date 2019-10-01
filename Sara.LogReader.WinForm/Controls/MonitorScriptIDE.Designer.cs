using Sara.WinForm.ControlsNS;

namespace Sara.LogReader.WinForm.Controls
{
    partial class MonitorScriptIDE
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorScriptIDE));
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.tbScript = new Sara.WinForm.ControlsNS.AutoCompleteTextBox();
            this.scResult = new System.Windows.Forms.SplitContainer();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.tvSummary = new Sara.WinForm.ControlsNS.BufferedTreeView();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.tvResults = new Sara.WinForm.ControlsNS.BufferedTreeView();
            this.t = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSummary = new System.Windows.Forms.Button();
            this.btnGrid = new System.Windows.Forms.Button();
            this.btnExportToCsv = new System.Windows.Forms.Button();
            this.btnOrientation = new System.Windows.Forms.Button();
            this.btnTree = new System.Windows.Forms.Button();
            this.ckbNavigation = new System.Windows.Forms.CheckBox();
            this.ckbOverlay = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnScriptFontIncrease = new System.Windows.Forms.Button();
            this.ckbFulleScreen = new System.Windows.Forms.CheckBox();
            this.ckbWrap = new System.Windows.Forms.CheckBox();
            this.ckbRenderTreeView = new System.Windows.Forms.CheckBox();
            this.ckbAutoSave = new System.Windows.Forms.CheckBox();
            this.ckbShowOutput = new System.Windows.Forms.CheckBox();
            this.lblLine = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ckbResults = new System.Windows.Forms.CheckBox();
            this.ckbShowScript = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRunOnAll = new System.Windows.Forms.Button();
            this.ckbShowTokens = new System.Windows.Forms.CheckBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scResult)).BeginInit();
            this.scResult.Panel1.SuspendLayout();
            this.scResult.Panel2.SuspendLayout();
            this.scResult.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 64);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.tbScript);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.scResult);
            this.scMain.Panel2.Controls.Add(this.panel2);
            this.scMain.Size = new System.Drawing.Size(1634, 857);
            this.scMain.SplitterDistance = 398;
            this.scMain.TabIndex = 0;
            // 
            // tbScript
            // 
            this.tbScript.AcceptsTab = true;
            this.tbScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbScript.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbScript.Location = new System.Drawing.Point(0, 0);
            this.tbScript.Multiline = true;
            this.tbScript.Name = "tbScript";
            this.tbScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbScript.Size = new System.Drawing.Size(1634, 398);
            this.tbScript.TabAs2Spaces = true;
            this.tbScript.TabIndex = 0;
            this.tbScript.Values = null;
            this.tbScript.TextChanged += new System.EventHandler(this.tbScript_TextChanged);
            // 
            // scResult
            // 
            this.scResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scResult.Location = new System.Drawing.Point(0, 33);
            this.scResult.Name = "scResult";
            // 
            // scResult.Panel1
            // 
            this.scResult.Panel1.Controls.Add(this.pnlSummary);
            this.scResult.Panel1.Controls.Add(this.dgvResults);
            this.scResult.Panel1.Controls.Add(this.tvResults);
            // 
            // scResult.Panel2
            // 
            this.scResult.Panel2.Controls.Add(this.t);
            this.scResult.Size = new System.Drawing.Size(1634, 422);
            this.scResult.SplitterDistance = 544;
            this.scResult.TabIndex = 10;
            // 
            // pnlSummary
            // 
            this.pnlSummary.Controls.Add(this.tvSummary);
            this.pnlSummary.Location = new System.Drawing.Point(293, 251);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(200, 100);
            this.pnlSummary.TabIndex = 10;
            // 
            // tvSummary
            // 
            this.tvSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSummary.Location = new System.Drawing.Point(0, 0);
            this.tvSummary.Name = "tvSummary";
            this.tvSummary.Size = new System.Drawing.Size(200, 100);
            this.tvSummary.TabIndex = 10;
            this.tvSummary.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSummary_AfterSelect);
            this.tvSummary.Enter += new System.EventHandler(this.tvSummary_Enter);
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvResults.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResults.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvResults.EnableHeadersVisualStyles = false;
            this.dgvResults.Location = new System.Drawing.Point(19, 17);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResults.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvResults.ShowEditingIcon = false;
            this.dgvResults.Size = new System.Drawing.Size(215, 186);
            this.dgvResults.TabIndex = 8;
            // 
            // tvResults
            // 
            this.tvResults.Location = new System.Drawing.Point(257, 17);
            this.tvResults.Name = "tvResults";
            this.tvResults.Size = new System.Drawing.Size(149, 179);
            this.tvResults.TabIndex = 9;
            this.tvResults.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDocumentMap_AfterSelect);
            this.tvResults.Enter += new System.EventHandler(this.tvDocumentMap_Enter);
            // 
            // t
            // 
            this.t.Dock = System.Windows.Forms.DockStyle.Fill;
            this.t.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t.Location = new System.Drawing.Point(0, 0);
            this.t.MinimumSize = new System.Drawing.Size(4, 4);
            this.t.Multiline = true;
            this.t.Name = "t";
            this.t.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.t.Size = new System.Drawing.Size(1086, 422);
            this.t.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSummary);
            this.panel2.Controls.Add(this.btnGrid);
            this.panel2.Controls.Add(this.btnExportToCsv);
            this.panel2.Controls.Add(this.btnOrientation);
            this.panel2.Controls.Add(this.btnTree);
            this.panel2.Controls.Add(this.ckbNavigation);
            this.panel2.Controls.Add(this.ckbOverlay);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1634, 33);
            this.panel2.TabIndex = 1;
            // 
            // btnSummary
            // 
            this.btnSummary.Location = new System.Drawing.Point(3, 4);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(76, 23);
            this.btnSummary.TabIndex = 28;
            this.btnSummary.Text = "Summary";
            this.btnSummary.UseVisualStyleBackColor = true;
            this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
            // 
            // btnGrid
            // 
            this.btnGrid.Location = new System.Drawing.Point(141, 4);
            this.btnGrid.Name = "btnGrid";
            this.btnGrid.Size = new System.Drawing.Size(50, 23);
            this.btnGrid.TabIndex = 27;
            this.btnGrid.Text = "Grid";
            this.btnGrid.UseVisualStyleBackColor = true;
            this.btnGrid.Click += new System.EventHandler(this.btnGrid_Click);
            // 
            // btnExportToCsv
            // 
            this.btnExportToCsv.Location = new System.Drawing.Point(367, 4);
            this.btnExportToCsv.Name = "btnExportToCsv";
            this.btnExportToCsv.Size = new System.Drawing.Size(85, 23);
            this.btnExportToCsv.TabIndex = 4;
            this.btnExportToCsv.Text = "Export to CSV";
            this.btnExportToCsv.UseVisualStyleBackColor = true;
            this.btnExportToCsv.Click += new System.EventHandler(this.btnExportToCsv_Click);
            // 
            // btnOrientation
            // 
            this.btnOrientation.Location = new System.Drawing.Point(286, 4);
            this.btnOrientation.Name = "btnOrientation";
            this.btnOrientation.Size = new System.Drawing.Size(75, 23);
            this.btnOrientation.TabIndex = 7;
            this.btnOrientation.Text = "Vertical";
            this.btnOrientation.UseVisualStyleBackColor = true;
            this.btnOrientation.Click += new System.EventHandler(this.btnOrientation_Click_1);
            // 
            // btnTree
            // 
            this.btnTree.Location = new System.Drawing.Point(85, 4);
            this.btnTree.Name = "btnTree";
            this.btnTree.Size = new System.Drawing.Size(50, 23);
            this.btnTree.TabIndex = 26;
            this.btnTree.Text = "Tree";
            this.btnTree.UseVisualStyleBackColor = true;
            this.btnTree.Click += new System.EventHandler(this.btnTree_Click);
            // 
            // ckbNavigation
            // 
            this.ckbNavigation.AutoSize = true;
            this.ckbNavigation.Checked = true;
            this.ckbNavigation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbNavigation.Location = new System.Drawing.Point(569, 7);
            this.ckbNavigation.Name = "ckbNavigation";
            this.ckbNavigation.Size = new System.Drawing.Size(77, 17);
            this.ckbNavigation.TabIndex = 25;
            this.ckbNavigation.Text = "Navigation";
            this.toolTip1.SetToolTip(this.ckbNavigation, "When checked the selected result will navigate to the source");
            this.ckbNavigation.UseVisualStyleBackColor = true;
            // 
            // ckbOverlay
            // 
            this.ckbOverlay.AutoSize = true;
            this.ckbOverlay.Location = new System.Drawing.Point(501, 7);
            this.ckbOverlay.Name = "ckbOverlay";
            this.ckbOverlay.Size = new System.Drawing.Size(62, 17);
            this.ckbOverlay.TabIndex = 22;
            this.ckbOverlay.Text = "Overlay";
            this.toolTip1.SetToolTip(this.ckbOverlay, "Show Tokens created by Lexer\r\n");
            this.ckbOverlay.UseVisualStyleBackColor = true;
            this.ckbOverlay.CheckedChanged += new System.EventHandler(this.ckbOverlay_CheckedChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Sara.LogReader.WinForm.Properties.Resources.CollaspeAll;
            this.pictureBox2.Location = new System.Drawing.Point(479, 8);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(14, 14);
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(461, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(14, 14);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnScriptFontIncrease);
            this.panel1.Controls.Add(this.ckbFulleScreen);
            this.panel1.Controls.Add(this.ckbWrap);
            this.panel1.Controls.Add(this.ckbRenderTreeView);
            this.panel1.Controls.Add(this.ckbAutoSave);
            this.panel1.Controls.Add(this.ckbShowOutput);
            this.panel1.Controls.Add(this.lblLine);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.ckbResults);
            this.panel1.Controls.Add(this.ckbShowScript);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnRunOnAll);
            this.panel1.Controls.Add(this.ckbShowTokens);
            this.panel1.Controls.Add(this.btnRun);
            this.panel1.Controls.Add(this.btnHelp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1634, 64);
            this.panel1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(441, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 20);
            this.button1.TabIndex = 38;
            this.button1.Text = "A -";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnScriptFontIncrease
            // 
            this.btnScriptFontIncrease.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScriptFontIncrease.Location = new System.Drawing.Point(441, 34);
            this.btnScriptFontIncrease.Name = "btnScriptFontIncrease";
            this.btnScriptFontIncrease.Size = new System.Drawing.Size(38, 20);
            this.btnScriptFontIncrease.TabIndex = 37;
            this.btnScriptFontIncrease.Text = "A +";
            this.btnScriptFontIncrease.UseVisualStyleBackColor = true;
            this.btnScriptFontIncrease.Click += new System.EventHandler(this.btnScriptFontIncrease_Click);
            // 
            // ckbFulleScreen
            // 
            this.ckbFulleScreen.AutoSize = true;
            this.ckbFulleScreen.Location = new System.Drawing.Point(244, 41);
            this.ckbFulleScreen.Name = "ckbFulleScreen";
            this.ckbFulleScreen.Size = new System.Drawing.Size(79, 17);
            this.ckbFulleScreen.TabIndex = 8;
            this.ckbFulleScreen.Text = "Full Screen";
            this.ckbFulleScreen.UseVisualStyleBackColor = true;
            this.ckbFulleScreen.CheckedChanged += new System.EventHandler(this.ckbFullScreen_CheckedChanged);
            // 
            // ckbWrap
            // 
            this.ckbWrap.AutoSize = true;
            this.ckbWrap.Checked = true;
            this.ckbWrap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbWrap.Location = new System.Drawing.Point(326, 41);
            this.ckbWrap.Name = "ckbWrap";
            this.ckbWrap.Size = new System.Drawing.Size(52, 17);
            this.ckbWrap.TabIndex = 36;
            this.ckbWrap.Text = "Wrap";
            this.toolTip1.SetToolTip(this.ckbWrap, "Toggle script wrap");
            this.ckbWrap.UseVisualStyleBackColor = true;
            this.ckbWrap.CheckedChanged += new System.EventHandler(this.ckbWrap_CheckedChanged);
            // 
            // ckbRenderTreeView
            // 
            this.ckbRenderTreeView.AutoSize = true;
            this.ckbRenderTreeView.Checked = true;
            this.ckbRenderTreeView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbRenderTreeView.Location = new System.Drawing.Point(326, 23);
            this.ckbRenderTreeView.Name = "ckbRenderTreeView";
            this.ckbRenderTreeView.Size = new System.Drawing.Size(109, 17);
            this.ckbRenderTreeView.TabIndex = 35;
            this.ckbRenderTreeView.Text = "Render TreeView";
            this.toolTip1.SetToolTip(this.ckbRenderTreeView, "Render TreeView");
            this.ckbRenderTreeView.UseVisualStyleBackColor = true;
            // 
            // ckbAutoSave
            // 
            this.ckbAutoSave.AutoSize = true;
            this.ckbAutoSave.Checked = true;
            this.ckbAutoSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAutoSave.Location = new System.Drawing.Point(326, 5);
            this.ckbAutoSave.Name = "ckbAutoSave";
            this.ckbAutoSave.Size = new System.Drawing.Size(114, 17);
            this.ckbAutoSave.TabIndex = 34;
            this.ckbAutoSave.Text = "Auto Save on Run";
            this.ckbAutoSave.UseVisualStyleBackColor = true;
            // 
            // ckbShowOutput
            // 
            this.ckbShowOutput.AutoSize = true;
            this.ckbShowOutput.Location = new System.Drawing.Point(185, 5);
            this.ckbShowOutput.Name = "ckbShowOutput";
            this.ckbShowOutput.Size = new System.Drawing.Size(58, 17);
            this.ckbShowOutput.TabIndex = 33;
            this.ckbShowOutput.Text = "Output";
            this.toolTip1.SetToolTip(this.ckbShowOutput, "Show Output");
            this.ckbShowOutput.UseVisualStyleBackColor = true;
            this.ckbShowOutput.CheckedChanged += new System.EventHandler(this.ckbShowOutput_CheckedChanged);
            // 
            // lblLine
            // 
            this.lblLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblLine.Location = new System.Drawing.Point(0, 59);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(1634, 2);
            this.lblLine.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 61);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1634, 3);
            this.panel3.TabIndex = 32;
            // 
            // ckbResults
            // 
            this.ckbResults.AutoSize = true;
            this.ckbResults.Location = new System.Drawing.Point(185, 41);
            this.ckbResults.Name = "ckbResults";
            this.ckbResults.Size = new System.Drawing.Size(61, 17);
            this.ckbResults.TabIndex = 10;
            this.ckbResults.Text = "Results";
            this.toolTip1.SetToolTip(this.ckbResults, "Show results");
            this.ckbResults.UseVisualStyleBackColor = true;
            this.ckbResults.CheckedChanged += new System.EventHandler(this.ckbShowResults_CheckedChanged);
            // 
            // ckbShowScript
            // 
            this.ckbShowScript.AutoSize = true;
            this.ckbShowScript.Checked = true;
            this.ckbShowScript.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbShowScript.Location = new System.Drawing.Point(185, 23);
            this.ckbShowScript.Name = "ckbShowScript";
            this.ckbShowScript.Size = new System.Drawing.Size(53, 17);
            this.ckbShowScript.TabIndex = 9;
            this.ckbShowScript.Text = "Script";
            this.toolTip1.SetToolTip(this.ckbShowScript, "Show Script");
            this.ckbShowScript.UseVisualStyleBackColor = true;
            this.ckbShowScript.CheckedChanged += new System.EventHandler(this.ckbShowScript_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(94, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRunOnAll
            // 
            this.btnRunOnAll.Location = new System.Drawing.Point(3, 34);
            this.btnRunOnAll.Name = "btnRunOnAll";
            this.btnRunOnAll.Size = new System.Drawing.Size(85, 23);
            this.btnRunOnAll.TabIndex = 5;
            this.btnRunOnAll.Text = "Run on All";
            this.btnRunOnAll.UseVisualStyleBackColor = true;
            this.btnRunOnAll.Click += new System.EventHandler(this.btnRunOnAll_Click);
            // 
            // ckbShowTokens
            // 
            this.ckbShowTokens.AutoSize = true;
            this.ckbShowTokens.Location = new System.Drawing.Point(244, 5);
            this.ckbShowTokens.Name = "ckbShowTokens";
            this.ckbShowTokens.Size = new System.Drawing.Size(62, 17);
            this.ckbShowTokens.TabIndex = 2;
            this.ckbShowTokens.Text = "Tokens";
            this.toolTip1.SetToolTip(this.ckbShowTokens, "Show Tokens created by Lexer\r\n");
            this.ckbShowTokens.UseVisualStyleBackColor = true;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(3, 5);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(85, 23);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(94, 34);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(85, 23);
            this.btnHelp.TabIndex = 0;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // StatusPanel
            // 
            this.StatusPanel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.StatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusPanel.Location = new System.Drawing.Point(326, 147);
            this.StatusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Padding = new System.Windows.Forms.Padding(1);
            this.StatusPanel.Size = new System.Drawing.Size(400, 100);
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = true;
            this.StatusPanel.SP_Enabled = true;
            this.StatusPanel.SP_FullScreen = true;
            this.StatusPanel.TabIndex = 30;
            this.StatusPanel.Visible = false;
            // 
            // MonitorScriptIDE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.panel1);
            this.Name = "MonitorScriptIDE";
            this.Size = new System.Drawing.Size(1634, 921);
            this.VisibleChanged += new System.EventHandler(this.MonitorScriptIDE_VisibleChanged);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel1.PerformLayout();
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.scResult.Panel1.ResumeLayout(false);
            this.scResult.Panel2.ResumeLayout(false);
            this.scResult.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scResult)).EndInit();
            this.scResult.ResumeLayout(false);
            this.pnlSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.CheckBox ckbShowTokens;
        private System.Windows.Forms.Button btnExportToCsv;
        private System.Windows.Forms.Button btnRunOnAll;
        private AutoCompleteTextBox tbScript;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox ckbOverlay;
        private System.Windows.Forms.CheckBox ckbNavigation;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox ckbFulleScreen;
        private System.Windows.Forms.CheckBox ckbResults;
        private System.Windows.Forms.CheckBox ckbShowScript;
        private System.Windows.Forms.Button btnOrientation;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.Button btnTree;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox ckbShowOutput;
        private System.Windows.Forms.SplitContainer scResult;
        private Sara.WinForm.ControlsNS.BufferedTreeView tvResults;
        private System.Windows.Forms.TextBox t;
        private System.Windows.Forms.Button btnSummary;
        private System.Windows.Forms.Button btnGrid;
        private System.Windows.Forms.Panel pnlSummary;
        private Sara.WinForm.ControlsNS.BufferedTreeView tvSummary;
        private System.Windows.Forms.CheckBox ckbAutoSave;
        private System.Windows.Forms.CheckBox ckbRenderTreeView;
        private System.Windows.Forms.CheckBox ckbWrap;
        private System.Windows.Forms.Button btnScriptFontIncrease;
        private System.Windows.Forms.Button button1;
    }
}
