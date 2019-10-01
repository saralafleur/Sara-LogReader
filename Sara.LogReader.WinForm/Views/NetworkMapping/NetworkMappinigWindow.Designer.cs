
using Sara.LogReader.WinForm.Controls;

namespace Sara.LogReader.WinForm.Views.NetworkMapping
{
    partial class NetworkMappingWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.eventBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            this.scAutoMap = new System.Windows.Forms.SplitContainer();
            this.scOverview = new System.Windows.Forms.SplitContainer();
            this.tvNodes = new Sara.WinForm.ControlsNS.BufferedTreeView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnCollaspeUI = new System.Windows.Forms.Button();
            this.pbExpandAll = new System.Windows.Forms.PictureBox();
            this.pbCollaspeAll = new System.Windows.Forms.PictureBox();
            this.lbNetworkMaps = new System.Windows.Forms.ListBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnCopy = new System.Windows.Forms.Button();
            this.ckShowAutoMap = new System.Windows.Forms.CheckBox();
            this.ckbShowAll = new System.Windows.Forms.CheckBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.scNetworkMapOverview = new System.Windows.Forms.SplitContainer();
            this.scNetworkMap = new System.Windows.Forms.SplitContainer();
            this.panel10 = new System.Windows.Forms.Panel();
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslFileCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbAutoMap = new System.Windows.Forms.CheckBox();
            this.cbShowCriteria = new System.Windows.Forms.CheckBox();
            this.lblFiles = new System.Windows.Forms.Label();
            this.btnMapNetwork = new System.Windows.Forms.Button();
            this.lbNetworkMessages = new System.Windows.Forms.ListBox();
            this.statusStrip5 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslNetworkMessagesCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.scCriteria = new System.Windows.Forms.SplitContainer();
            this.dgvCriteria = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblDateTimeDifference = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCriteriaMatches = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvRecommendedMatches = new System.Windows.Forms.DataGridView();
            this.statusStrip4 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslRecommendedMatchesCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.scValues = new System.Windows.Forms.SplitContainer();
            this.dgvSourceProperties = new System.Windows.Forms.DataGridView();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSourceValueCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvTargetProperties = new System.Windows.Forms.DataGridView();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip3 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTargetValueCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.statusPanel1 = new Sara.WinForm.ControlsNS.StatusPanel();
            this.warningPanel = new Sara.WinForm.ControlsNS.WarningPanel();
            ((System.ComponentModel.ISupportInitialize)(this.eventBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scAutoMap)).BeginInit();
            this.scAutoMap.Panel1.SuspendLayout();
            this.scAutoMap.Panel2.SuspendLayout();
            this.scAutoMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scOverview)).BeginInit();
            this.scOverview.Panel1.SuspendLayout();
            this.scOverview.Panel2.SuspendLayout();
            this.scOverview.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExpandAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCollaspeAll)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scNetworkMapOverview)).BeginInit();
            this.scNetworkMapOverview.Panel1.SuspendLayout();
            this.scNetworkMapOverview.Panel2.SuspendLayout();
            this.scNetworkMapOverview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scNetworkMap)).BeginInit();
            this.scNetworkMap.Panel1.SuspendLayout();
            this.scNetworkMap.Panel2.SuspendLayout();
            this.scNetworkMap.SuspendLayout();
            this.panel10.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scCriteria)).BeginInit();
            this.scCriteria.Panel1.SuspendLayout();
            this.scCriteria.Panel2.SuspendLayout();
            this.scCriteria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCriteria)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecommendedMatches)).BeginInit();
            this.statusStrip4.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scValues)).BeginInit();
            this.scValues.Panel1.SuspendLayout();
            this.scValues.Panel2.SuspendLayout();
            this.scValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSourceProperties)).BeginInit();
            this.panel8.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTargetProperties)).BeginInit();
            this.panel9.SuspendLayout();
            this.statusStrip3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusPanel
            // 
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = false;
            this.StatusPanel.Location = new System.Drawing.Point(37, 170);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(314, 64);
            this.StatusPanel.TabIndex = 31;
            this.StatusPanel.Visible = false;
            // 
            // scAutoMap
            // 
            this.scAutoMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scAutoMap.Location = new System.Drawing.Point(0, 66);
            this.scAutoMap.Name = "scAutoMap";
            // 
            // scAutoMap.Panel1
            // 
            this.scAutoMap.Panel1.Controls.Add(this.scOverview);
            this.scAutoMap.Panel1MinSize = 180;
            // 
            // scAutoMap.Panel2
            // 
            this.scAutoMap.Panel2.Controls.Add(this.scNetworkMapOverview);
            this.scAutoMap.Size = new System.Drawing.Size(1317, 808);
            this.scAutoMap.SplitterDistance = 180;
            this.scAutoMap.TabIndex = 37;
            // 
            // scOverview
            // 
            this.scOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scOverview.Location = new System.Drawing.Point(0, 0);
            this.scOverview.Name = "scOverview";
            this.scOverview.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scOverview.Panel1
            // 
            this.scOverview.Panel1.Controls.Add(this.tvNodes);
            this.scOverview.Panel1.Controls.Add(this.panel6);
            this.scOverview.Panel1MinSize = 200;
            // 
            // scOverview.Panel2
            // 
            this.scOverview.Panel2.Controls.Add(this.lbNetworkMaps);
            this.scOverview.Panel2.Controls.Add(this.panel5);
            this.scOverview.Size = new System.Drawing.Size(180, 808);
            this.scOverview.SplitterDistance = 200;
            this.scOverview.TabIndex = 35;
            // 
            // tvNodes
            // 
            this.tvNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvNodes.Location = new System.Drawing.Point(0, 28);
            this.tvNodes.Name = "tvNodes";
            this.tvNodes.Size = new System.Drawing.Size(180, 172);
            this.tvNodes.TabIndex = 1;
            this.tvNodes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tvNodes_KeyPress);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnCollaspeUI);
            this.panel6.Controls.Add(this.pbExpandAll);
            this.panel6.Controls.Add(this.pbCollaspeAll);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(180, 28);
            this.panel6.TabIndex = 0;
            // 
            // btnCollaspeUI
            // 
            this.btnCollaspeUI.Location = new System.Drawing.Point(50, 4);
            this.btnCollaspeUI.Name = "btnCollaspeUI";
            this.btnCollaspeUI.Size = new System.Drawing.Size(60, 21);
            this.btnCollaspeUI.TabIndex = 28;
            this.btnCollaspeUI.Text = "Clean UI";
            this.btnCollaspeUI.UseVisualStyleBackColor = true;
            this.btnCollaspeUI.Click += new System.EventHandler(this.btnCollaspeUI_Click);
            // 
            // pbExpandAll
            // 
            this.pbExpandAll.Image = global::Sara.LogReader.WinForm.Properties.Resources.ExpandAll;
            this.pbExpandAll.Location = new System.Drawing.Point(28, 6);
            this.pbExpandAll.Name = "pbExpandAll";
            this.pbExpandAll.Size = new System.Drawing.Size(16, 16);
            this.pbExpandAll.TabIndex = 27;
            this.pbExpandAll.TabStop = false;
            this.pbExpandAll.Click += new System.EventHandler(this.pbExpandAll_Click);
            // 
            // pbCollaspeAll
            // 
            this.pbCollaspeAll.Image = global::Sara.LogReader.WinForm.Properties.Resources.CollaspeAll;
            this.pbCollaspeAll.Location = new System.Drawing.Point(6, 6);
            this.pbCollaspeAll.Name = "pbCollaspeAll";
            this.pbCollaspeAll.Size = new System.Drawing.Size(16, 16);
            this.pbCollaspeAll.TabIndex = 26;
            this.pbCollaspeAll.TabStop = false;
            this.pbCollaspeAll.Click += new System.EventHandler(this.pbCollaspeAll_Click);
            // 
            // lbNetworkMaps
            // 
            this.lbNetworkMaps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNetworkMaps.FormattingEnabled = true;
            this.lbNetworkMaps.Location = new System.Drawing.Point(0, 54);
            this.lbNetworkMaps.Name = "lbNetworkMaps";
            this.lbNetworkMaps.Size = new System.Drawing.Size(180, 550);
            this.lbNetworkMaps.TabIndex = 5;
            this.lbNetworkMaps.SelectedIndexChanged += new System.EventHandler(this.lbNetworkMaps_SelectedIndexChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnCopy);
            this.panel5.Controls.Add(this.ckShowAutoMap);
            this.panel5.Controls.Add(this.ckbShowAll);
            this.panel5.Controls.Add(this.btnDelete);
            this.panel5.Controls.Add(this.btnEdit);
            this.panel5.Controls.Add(this.btnAdd);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(180, 54);
            this.panel5.TabIndex = 0;
            // 
            // btnCopy
            // 
            this.btnCopy.Enabled = false;
            this.btnCopy.Location = new System.Drawing.Point(138, 3);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(40, 23);
            this.btnCopy.TabIndex = 12;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // ckShowAutoMap
            // 
            this.ckShowAutoMap.AutoSize = true;
            this.ckShowAutoMap.Location = new System.Drawing.Point(80, 32);
            this.ckShowAutoMap.Name = "ckShowAutoMap";
            this.ckShowAutoMap.Size = new System.Drawing.Size(102, 17);
            this.ckShowAutoMap.TabIndex = 11;
            this.ckShowAutoMap.Text = "Show Auto Map";
            this.ckShowAutoMap.UseVisualStyleBackColor = true;
            this.ckShowAutoMap.CheckedChanged += new System.EventHandler(this.ckAutoMap_CheckedChanged);
            // 
            // ckbShowAll
            // 
            this.ckbShowAll.AutoSize = true;
            this.ckbShowAll.Checked = true;
            this.ckbShowAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbShowAll.Location = new System.Drawing.Point(6, 32);
            this.ckbShowAll.Name = "ckbShowAll";
            this.ckbShowAll.Size = new System.Drawing.Size(67, 17);
            this.ckbShowAll.TabIndex = 10;
            this.ckbShowAll.Text = "Show All";
            this.ckbShowAll.UseVisualStyleBackColor = true;
            this.ckbShowAll.CheckedChanged += new System.EventHandler(this.ckbShowAll_CheckedChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(85, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(47, 23);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(44, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(35, 23);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(35, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // scNetworkMapOverview
            // 
            this.scNetworkMapOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scNetworkMapOverview.Location = new System.Drawing.Point(0, 0);
            this.scNetworkMapOverview.Name = "scNetworkMapOverview";
            // 
            // scNetworkMapOverview.Panel1
            // 
            this.scNetworkMapOverview.Panel1.Controls.Add(this.scNetworkMap);
            this.scNetworkMapOverview.Panel1MinSize = 160;
            // 
            // scNetworkMapOverview.Panel2
            // 
            this.scNetworkMapOverview.Panel2.Controls.Add(this.scCriteria);
            this.scNetworkMapOverview.Size = new System.Drawing.Size(1133, 808);
            this.scNetworkMapOverview.SplitterDistance = 160;
            this.scNetworkMapOverview.TabIndex = 1;
            // 
            // scNetworkMap
            // 
            this.scNetworkMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scNetworkMap.Location = new System.Drawing.Point(0, 0);
            this.scNetworkMap.Name = "scNetworkMap";
            this.scNetworkMap.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scNetworkMap.Panel1
            // 
            this.scNetworkMap.Panel1.Controls.Add(this.panel10);
            this.scNetworkMap.Panel1.Controls.Add(this.statusStrip1);
            this.scNetworkMap.Panel1.Controls.Add(this.panel1);
            this.scNetworkMap.Panel1MinSize = 200;
            // 
            // scNetworkMap.Panel2
            // 
            this.scNetworkMap.Panel2.Controls.Add(this.lbNetworkMessages);
            this.scNetworkMap.Panel2.Controls.Add(this.statusStrip5);
            this.scNetworkMap.Panel2.Controls.Add(this.panel3);
            this.scNetworkMap.Size = new System.Drawing.Size(160, 808);
            this.scNetworkMap.SplitterDistance = 264;
            this.scNetworkMap.TabIndex = 2;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.lbFiles);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 67);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(160, 175);
            this.panel10.TabIndex = 11;
            // 
            // lbFiles
            // 
            this.lbFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFiles.FormattingEnabled = true;
            this.lbFiles.Location = new System.Drawing.Point(0, 0);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.Size = new System.Drawing.Size(160, 175);
            this.lbFiles.TabIndex = 10;
            this.lbFiles.SelectedIndexChanged += new System.EventHandler(this.lbFiles_SelectedIndexChanged);
            this.lbFiles.DoubleClick += new System.EventHandler(this.lbFiles_DoubleClick);
            this.lbFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbFiles_MouseDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tsslFileCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 242);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(160, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(81, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // tsslFileCount
            // 
            this.tsslFileCount.Name = "tsslFileCount";
            this.tsslFileCount.Size = new System.Drawing.Size(64, 17);
            this.tsslFileCount.Text = "Count ###";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbAutoMap);
            this.panel1.Controls.Add(this.cbShowCriteria);
            this.panel1.Controls.Add(this.lblFiles);
            this.panel1.Controls.Add(this.btnMapNetwork);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(160, 67);
            this.panel1.TabIndex = 5;
            // 
            // cbAutoMap
            // 
            this.cbAutoMap.AutoSize = true;
            this.cbAutoMap.Checked = true;
            this.cbAutoMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoMap.Location = new System.Drawing.Point(95, 29);
            this.cbAutoMap.Name = "cbAutoMap";
            this.cbAutoMap.Size = new System.Drawing.Size(72, 17);
            this.cbAutoMap.TabIndex = 13;
            this.cbAutoMap.Text = "Auto Map";
            this.cbAutoMap.UseVisualStyleBackColor = true;
            // 
            // cbShowCriteria
            // 
            this.cbShowCriteria.AutoSize = true;
            this.cbShowCriteria.Location = new System.Drawing.Point(10, 29);
            this.cbShowCriteria.Name = "cbShowCriteria";
            this.cbShowCriteria.Size = new System.Drawing.Size(88, 17);
            this.cbShowCriteria.TabIndex = 12;
            this.cbShowCriteria.Text = "Show Criteria";
            this.cbShowCriteria.UseVisualStyleBackColor = true;
            this.cbShowCriteria.CheckedChanged += new System.EventHandler(this.cbShowCriteria_CheckedChanged);
            // 
            // lblFiles
            // 
            this.lblFiles.AutoSize = true;
            this.lblFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiles.Location = new System.Drawing.Point(3, 48);
            this.lblFiles.Name = "lblFiles";
            this.lblFiles.Size = new System.Drawing.Size(59, 13);
            this.lblFiles.TabIndex = 5;
            this.lblFiles.Text = "Files (All)";
            // 
            // btnMapNetwork
            // 
            this.btnMapNetwork.Location = new System.Drawing.Point(3, 3);
            this.btnMapNetwork.Name = "btnMapNetwork";
            this.btnMapNetwork.Size = new System.Drawing.Size(99, 23);
            this.btnMapNetwork.TabIndex = 4;
            this.btnMapNetwork.Text = "Map Network";
            this.btnMapNetwork.UseVisualStyleBackColor = true;
            this.btnMapNetwork.Click += new System.EventHandler(this.btnMapNetwork_Click);
            // 
            // lbNetworkMessages
            // 
            this.lbNetworkMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNetworkMessages.FormattingEnabled = true;
            this.lbNetworkMessages.Location = new System.Drawing.Point(0, 48);
            this.lbNetworkMessages.Name = "lbNetworkMessages";
            this.lbNetworkMessages.Size = new System.Drawing.Size(160, 470);
            this.lbNetworkMessages.TabIndex = 9;
            this.lbNetworkMessages.SelectedIndexChanged += new System.EventHandler(this.lbNetworkMessages_SelectedIndexChanged);
            // 
            // statusStrip5
            // 
            this.statusStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel5,
            this.tsslNetworkMessagesCount});
            this.statusStrip5.Location = new System.Drawing.Point(0, 518);
            this.statusStrip5.Name = "statusStrip5";
            this.statusStrip5.Size = new System.Drawing.Size(160, 22);
            this.statusStrip5.TabIndex = 8;
            this.statusStrip5.Text = "statusStrip5";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(81, 17);
            this.toolStripStatusLabel5.Spring = true;
            // 
            // tsslNetworkMessagesCount
            // 
            this.tsslNetworkMessagesCount.Name = "tsslNetworkMessagesCount";
            this.tsslNetworkMessagesCount.Size = new System.Drawing.Size(64, 17);
            this.tsslNetworkMessagesCount.Text = "Count ###";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(160, 48);
            this.panel3.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Network Messages (All)";
            // 
            // scCriteria
            // 
            this.scCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scCriteria.Location = new System.Drawing.Point(0, 0);
            this.scCriteria.Name = "scCriteria";
            this.scCriteria.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scCriteria.Panel1
            // 
            this.scCriteria.Panel1.Controls.Add(this.dgvCriteria);
            this.scCriteria.Panel1.Controls.Add(this.panel2);
            // 
            // scCriteria.Panel2
            // 
            this.scCriteria.Panel2.Controls.Add(this.splitContainer1);
            this.scCriteria.Size = new System.Drawing.Size(969, 808);
            this.scCriteria.SplitterDistance = 214;
            this.scCriteria.TabIndex = 1;
            // 
            // dgvCriteria
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCriteria.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCriteria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCriteria.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCriteria.Location = new System.Drawing.Point(0, 48);
            this.dgvCriteria.MultiSelect = false;
            this.dgvCriteria.Name = "dgvCriteria";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCriteria.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCriteria.RowHeadersVisible = false;
            this.dgvCriteria.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCriteria.Size = new System.Drawing.Size(969, 166);
            this.dgvCriteria.TabIndex = 11;
            this.dgvCriteria.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCriteria_RowEnter);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblDateTimeDifference);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.lblCriteriaMatches);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(969, 48);
            this.panel2.TabIndex = 9;
            // 
            // lblDateTimeDifference
            // 
            this.lblDateTimeDifference.AutoSize = true;
            this.lblDateTimeDifference.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTimeDifference.Location = new System.Drawing.Point(257, 30);
            this.lblDateTimeDifference.Name = "lblDateTimeDifference";
            this.lblDateTimeDifference.Size = new System.Drawing.Size(35, 13);
            this.lblDateTimeDifference.TabIndex = 7;
            this.lblDateTimeDifference.Text = "XXXX";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(123, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "DateTime Difference:";
            // 
            // lblCriteriaMatches
            // 
            this.lblCriteriaMatches.AutoSize = true;
            this.lblCriteriaMatches.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriteriaMatches.Location = new System.Drawing.Point(3, 29);
            this.lblCriteriaMatches.Name = "lblCriteriaMatches";
            this.lblCriteriaMatches.Size = new System.Drawing.Size(99, 13);
            this.lblCriteriaMatches.TabIndex = 5;
            this.lblCriteriaMatches.Text = "Criteria Matches";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvRecommendedMatches);
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip4);
            this.splitContainer1.Panel1.Controls.Add(this.panel4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.scValues);
            this.splitContainer1.Panel2.Controls.Add(this.panel7);
            this.splitContainer1.Size = new System.Drawing.Size(969, 590);
            this.splitContainer1.SplitterDistance = 196;
            this.splitContainer1.TabIndex = 1;
            // 
            // dgvRecommendedMatches
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecommendedMatches.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRecommendedMatches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecommendedMatches.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvRecommendedMatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecommendedMatches.Location = new System.Drawing.Point(0, 30);
            this.dgvRecommendedMatches.MultiSelect = false;
            this.dgvRecommendedMatches.Name = "dgvRecommendedMatches";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecommendedMatches.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvRecommendedMatches.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecommendedMatches.Size = new System.Drawing.Size(969, 144);
            this.dgvRecommendedMatches.TabIndex = 14;
            // 
            // statusStrip4
            // 
            this.statusStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.tsslRecommendedMatchesCount});
            this.statusStrip4.Location = new System.Drawing.Point(0, 174);
            this.statusStrip4.Name = "statusStrip4";
            this.statusStrip4.Size = new System.Drawing.Size(969, 22);
            this.statusStrip4.TabIndex = 13;
            this.statusStrip4.Text = "statusStrip4";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(890, 17);
            this.toolStripStatusLabel3.Spring = true;
            // 
            // tsslRecommendedMatchesCount
            // 
            this.tsslRecommendedMatchesCount.Name = "tsslRecommendedMatchesCount";
            this.tsslRecommendedMatchesCount.Size = new System.Drawing.Size(64, 17);
            this.tsslRecommendedMatchesCount.Text = "Count ###";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(969, 30);
            this.panel4.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Recommended Matches";
            // 
            // scValues
            // 
            this.scValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scValues.Location = new System.Drawing.Point(0, 26);
            this.scValues.Name = "scValues";
            // 
            // scValues.Panel1
            // 
            this.scValues.Panel1.Controls.Add(this.dgvSourceProperties);
            this.scValues.Panel1.Controls.Add(this.panel8);
            this.scValues.Panel1.Controls.Add(this.statusStrip2);
            // 
            // scValues.Panel2
            // 
            this.scValues.Panel2.Controls.Add(this.dgvTargetProperties);
            this.scValues.Panel2.Controls.Add(this.panel9);
            this.scValues.Panel2.Controls.Add(this.statusStrip3);
            this.scValues.Size = new System.Drawing.Size(969, 364);
            this.scValues.SplitterDistance = 320;
            this.scValues.TabIndex = 3;
            // 
            // dgvSourceProperties
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSourceProperties.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvSourceProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSourceProperties.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvSourceProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSourceProperties.Location = new System.Drawing.Point(0, 26);
            this.dgvSourceProperties.MultiSelect = false;
            this.dgvSourceProperties.Name = "dgvSourceProperties";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSourceProperties.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvSourceProperties.RowHeadersVisible = false;
            this.dgvSourceProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSourceProperties.Size = new System.Drawing.Size(320, 316);
            this.dgvSourceProperties.TabIndex = 14;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label4);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(320, 26);
            this.panel8.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Source";
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.tsslSourceValueCount});
            this.statusStrip2.Location = new System.Drawing.Point(0, 342);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(320, 22);
            this.statusStrip2.TabIndex = 12;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(241, 17);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // tsslSourceValueCount
            // 
            this.tsslSourceValueCount.Name = "tsslSourceValueCount";
            this.tsslSourceValueCount.Size = new System.Drawing.Size(64, 17);
            this.tsslSourceValueCount.Text = "Count ###";
            // 
            // dgvTargetProperties
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTargetProperties.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvTargetProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTargetProperties.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvTargetProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTargetProperties.Location = new System.Drawing.Point(0, 26);
            this.dgvTargetProperties.Name = "dgvTargetProperties";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTargetProperties.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvTargetProperties.RowHeadersVisible = false;
            this.dgvTargetProperties.Size = new System.Drawing.Size(645, 316);
            this.dgvTargetProperties.TabIndex = 15;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label5);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(645, 26);
            this.panel9.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Target";
            // 
            // statusStrip3
            // 
            this.statusStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel4,
            this.tsslTargetValueCount});
            this.statusStrip3.Location = new System.Drawing.Point(0, 342);
            this.statusStrip3.Name = "statusStrip3";
            this.statusStrip3.Size = new System.Drawing.Size(645, 22);
            this.statusStrip3.TabIndex = 13;
            this.statusStrip3.Text = "statusStrip3";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(566, 17);
            this.toolStripStatusLabel4.Spring = true;
            // 
            // tsslTargetValueCount
            // 
            this.tsslTargetValueCount.Name = "tsslTargetValueCount";
            this.tsslTargetValueCount.Size = new System.Drawing.Size(64, 17);
            this.tsslTargetValueCount.Text = "Count ###";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label2);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(969, 26);
            this.panel7.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Values";
            // 
            // statusPanel1
            // 
            this.statusPanel1.SP_DefaultStatusSize = true;
            this.statusPanel1.SP_DisplayRemainingTime = false;
            this.statusPanel1.Location = new System.Drawing.Point(368, 385);
            this.statusPanel1.Name = "statusPanel1";
            this.statusPanel1.Size = new System.Drawing.Size(581, 104);
            this.statusPanel1.TabIndex = 38;
            this.statusPanel1.Visible = false;
            // 
            // warningPanel
            // 
            this.warningPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.warningPanel.Location = new System.Drawing.Point(0, 0);
            this.warningPanel.Name = "warningPanel";
            this.warningPanel.Size = new System.Drawing.Size(1317, 66);
            this.warningPanel.TabIndex = 39;
            this.warningPanel.WarningText = "Open a File";
            // 
            // NetworkMappingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1317, 874);
            this.Controls.Add(this.scAutoMap);
            this.Controls.Add(this.warningPanel);
            this.Controls.Add(this.statusPanel1);
            this.Controls.Add(this.StatusPanel);
            this.HideOnClose = true;
            this.Name = "NetworkMappingWindow";
            this.TabText = "Network Mapping";
            this.Text = "Network Mapping";
            this.Shown += new System.EventHandler(this.NetworkMappingWindow_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.eventBindingSource)).EndInit();
            this.scAutoMap.Panel1.ResumeLayout(false);
            this.scAutoMap.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scAutoMap)).EndInit();
            this.scAutoMap.ResumeLayout(false);
            this.scOverview.Panel1.ResumeLayout(false);
            this.scOverview.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scOverview)).EndInit();
            this.scOverview.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbExpandAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCollaspeAll)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.scNetworkMapOverview.Panel1.ResumeLayout(false);
            this.scNetworkMapOverview.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scNetworkMapOverview)).EndInit();
            this.scNetworkMapOverview.ResumeLayout(false);
            this.scNetworkMap.Panel1.ResumeLayout(false);
            this.scNetworkMap.Panel1.PerformLayout();
            this.scNetworkMap.Panel2.ResumeLayout(false);
            this.scNetworkMap.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scNetworkMap)).EndInit();
            this.scNetworkMap.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip5.ResumeLayout(false);
            this.statusStrip5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.scCriteria.Panel1.ResumeLayout(false);
            this.scCriteria.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scCriteria)).EndInit();
            this.scCriteria.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCriteria)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecommendedMatches)).EndInit();
            this.statusStrip4.ResumeLayout(false);
            this.statusStrip4.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.scValues.Panel1.ResumeLayout(false);
            this.scValues.Panel1.PerformLayout();
            this.scValues.Panel2.ResumeLayout(false);
            this.scValues.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scValues)).EndInit();
            this.scValues.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSourceProperties)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTargetProperties)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.statusStrip3.ResumeLayout(false);
            this.statusStrip3.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource eventBindingSource;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;
        private System.Windows.Forms.SplitContainer scAutoMap;
        private System.Windows.Forms.SplitContainer scOverview;
        private Sara.WinForm.ControlsNS.BufferedTreeView tvNodes;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.PictureBox pbExpandAll;
        private System.Windows.Forms.PictureBox pbCollaspeAll;
        private System.Windows.Forms.ListBox lbNetworkMaps;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox ckShowAutoMap;
        private System.Windows.Forms.CheckBox ckbShowAll;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.SplitContainer scNetworkMapOverview;
        private System.Windows.Forms.SplitContainer scNetworkMap;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblFiles;
        private System.Windows.Forms.Button btnMapNetwork;
        private System.Windows.Forms.CheckBox cbShowCriteria;
        private System.Windows.Forms.SplitContainer scCriteria;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCriteriaMatches;
        private System.Windows.Forms.Button btnCollaspeUI;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer scValues;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbAutoMap;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tsslSourceValueCount;
        private System.Windows.Forms.StatusStrip statusStrip3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel tsslTargetValueCount;
        private System.Windows.Forms.StatusStrip statusStrip4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel tsslRecommendedMatchesCount;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.StatusStrip statusStrip5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel tsslNetworkMessagesCount;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lbNetworkMessages;
        private System.Windows.Forms.DataGridView dgvSourceProperties;
        private System.Windows.Forms.DataGridView dgvTargetProperties;
        private System.Windows.Forms.DataGridView dgvCriteria;
        private System.Windows.Forms.DataGridView dgvRecommendedMatches;
        private Sara.WinForm.ControlsNS.StatusPanel statusPanel1;
        private System.Windows.Forms.Label lblDateTimeDifference;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsslFileCount;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.ListBox lbFiles;
        private System.Windows.Forms.Button btnCopy;
        private Sara.WinForm.ControlsNS.WarningPanel warningPanel;


    }
}