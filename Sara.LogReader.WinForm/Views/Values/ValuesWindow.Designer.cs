using Sara.WinForm.ControlsNS;

namespace Sara.LogReader.WinForm.Views.Values
{
    partial class ValuesWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.eventBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.ckbLazyLoad = new System.Windows.Forms.CheckBox();
            this.ckbFileInfo = new System.Windows.Forms.CheckBox();
            this.ckbDistinct = new System.Windows.Forms.CheckBox();
            this.ckbDocumentMap = new System.Windows.Forms.CheckBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRegularExpression = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlList = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvValues = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.ckbFilter = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.clbSourceType = new System.Windows.Forms.CheckedListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.clbCategory = new System.Windows.Forms.CheckedListBox();
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            ((System.ComponentModel.ISupportInitialize)(this.eventBindingSource)).BeginInit();
            this.pnlFilter.SuspendLayout();
            this.pnlList.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValues)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.label5);
            this.pnlFilter.Controls.Add(this.clbSourceType);
            this.pnlFilter.Controls.Add(this.label13);
            this.pnlFilter.Controls.Add(this.clbCategory);
            this.pnlFilter.Controls.Add(this.btnApply);
            this.pnlFilter.Controls.Add(this.btnClearFilter);
            this.pnlFilter.Controls.Add(this.ckbLazyLoad);
            this.pnlFilter.Controls.Add(this.ckbFileInfo);
            this.pnlFilter.Controls.Add(this.ckbDistinct);
            this.pnlFilter.Controls.Add(this.ckbDocumentMap);
            this.pnlFilter.Controls.Add(this.tbName);
            this.pnlFilter.Controls.Add(this.label2);
            this.pnlFilter.Controls.Add(this.tbRegularExpression);
            this.pnlFilter.Controls.Add(this.label1);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(389, 366);
            this.pnlFilter.TabIndex = 26;
            this.pnlFilter.Visible = false;
            // 
            // ckbLazyLoad
            // 
            this.ckbLazyLoad.AutoSize = true;
            this.ckbLazyLoad.Checked = true;
            this.ckbLazyLoad.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.ckbLazyLoad.Location = new System.Drawing.Point(116, 313);
            this.ckbLazyLoad.Name = "ckbLazyLoad";
            this.ckbLazyLoad.Size = new System.Drawing.Size(75, 17);
            this.ckbLazyLoad.TabIndex = 21;
            this.ckbLazyLoad.Text = "Lazy Load";
            this.ckbLazyLoad.ThreeState = true;
            this.ckbLazyLoad.UseVisualStyleBackColor = true;
            // 
            // ckbFileInfo
            // 
            this.ckbFileInfo.AutoSize = true;
            this.ckbFileInfo.Checked = true;
            this.ckbFileInfo.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.ckbFileInfo.Location = new System.Drawing.Point(264, 313);
            this.ckbFileInfo.Name = "ckbFileInfo";
            this.ckbFileInfo.Size = new System.Drawing.Size(63, 17);
            this.ckbFileInfo.TabIndex = 16;
            this.ckbFileInfo.Text = "File Info";
            this.ckbFileInfo.ThreeState = true;
            this.ckbFileInfo.UseVisualStyleBackColor = true;
            // 
            // ckbDistinct
            // 
            this.ckbDistinct.AutoSize = true;
            this.ckbDistinct.Checked = true;
            this.ckbDistinct.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.ckbDistinct.Location = new System.Drawing.Point(197, 313);
            this.ckbDistinct.Name = "ckbDistinct";
            this.ckbDistinct.Size = new System.Drawing.Size(61, 17);
            this.ckbDistinct.TabIndex = 15;
            this.ckbDistinct.Text = "Distinct";
            this.ckbDistinct.ThreeState = true;
            this.ckbDistinct.UseVisualStyleBackColor = true;
            // 
            // ckbDocumentMap
            // 
            this.ckbDocumentMap.AutoSize = true;
            this.ckbDocumentMap.Checked = true;
            this.ckbDocumentMap.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.ckbDocumentMap.Location = new System.Drawing.Point(11, 313);
            this.ckbDocumentMap.Name = "ckbDocumentMap";
            this.ckbDocumentMap.Size = new System.Drawing.Size(99, 17);
            this.ckbDocumentMap.TabIndex = 14;
            this.ckbDocumentMap.Text = "Document Map";
            this.ckbDocumentMap.ThreeState = true;
            this.ckbDocumentMap.UseVisualStyleBackColor = true;
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(12, 61);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(365, 20);
            this.tbName.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Name";
            // 
            // tbRegularExpression
            // 
            this.tbRegularExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRegularExpression.Location = new System.Drawing.Point(12, 22);
            this.tbRegularExpression.Name = "tbRegularExpression";
            this.tbRegularExpression.Size = new System.Drawing.Size(365, 20);
            this.tbRegularExpression.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Regular Expression";
            // 
            // pnlList
            // 
            this.pnlList.Controls.Add(this.panel4);
            this.pnlList.Controls.Add(this.panel3);
            this.pnlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlList.Location = new System.Drawing.Point(0, 0);
            this.pnlList.Name = "pnlList";
            this.pnlList.Size = new System.Drawing.Size(389, 617);
            this.pnlList.TabIndex = 27;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgvValues);
            this.panel4.Controls.Add(this.pnlFilter);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 42);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(389, 575);
            this.panel4.TabIndex = 6;
            // 
            // dgvValues
            // 
            this.dgvValues.AllowUserToOrderColumns = true;
            this.dgvValues.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvValues.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvValues.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvValues.Location = new System.Drawing.Point(0, 366);
            this.dgvValues.MultiSelect = false;
            this.dgvValues.Name = "dgvValues";
            this.dgvValues.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvValues.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvValues.RowHeadersVisible = false;
            this.dgvValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvValues.Size = new System.Drawing.Size(389, 209);
            this.dgvValues.TabIndex = 6;
            this.dgvValues.SelectionChanged += new System.EventHandler(this.dgvValues_SelectionChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ckbFilter);
            this.panel3.Controls.Add(this.btnDelete);
            this.panel3.Controls.Add(this.btnEdit);
            this.panel3.Controls.Add(this.btnAdd);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(389, 42);
            this.panel3.TabIndex = 5;
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(147, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(61, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(80, 12);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(61, 23);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(13, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(61, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(12, 336);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(61, 23);
            this.btnApply.TabIndex = 23;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Location = new System.Drawing.Point(79, 336);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(78, 23);
            this.btnClearFilter.TabIndex = 22;
            this.btnClearFilter.Text = "Clear Filter";
            this.btnClearFilter.UseVisualStyleBackColor = true;
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // ckbFilter
            // 
            this.ckbFilter.AutoSize = true;
            this.ckbFilter.Location = new System.Drawing.Point(214, 17);
            this.ckbFilter.Name = "ckbFilter";
            this.ckbFilter.Size = new System.Drawing.Size(48, 17);
            this.ckbFilter.TabIndex = 6;
            this.ckbFilter.Text = "Filter";
            this.ckbFilter.UseVisualStyleBackColor = true;
            this.ckbFilter.CheckedChanged += new System.EventHandler(this.ckbFilter_CheckedChanged_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 114;
            this.label5.Text = "Source Type";
            // 
            // clbSourceType
            // 
            this.clbSourceType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbSourceType.ColumnWidth = 300;
            this.clbSourceType.FormattingEnabled = true;
            this.clbSourceType.Location = new System.Drawing.Point(12, 213);
            this.clbSourceType.MultiColumn = true;
            this.clbSourceType.Name = "clbSourceType";
            this.clbSourceType.Size = new System.Drawing.Size(365, 94);
            this.clbSourceType.TabIndex = 115;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 84);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 116;
            this.label13.Text = "Categories";
            // 
            // clbCategory
            // 
            this.clbCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbCategory.ColumnWidth = 300;
            this.clbCategory.FormattingEnabled = true;
            this.clbCategory.Location = new System.Drawing.Point(12, 100);
            this.clbCategory.MultiColumn = true;
            this.clbCategory.Name = "clbCategory";
            this.clbCategory.Size = new System.Drawing.Size(365, 94);
            this.clbCategory.TabIndex = 113;
            // 
            // StatusPanel
            // 
            this.StatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusPanel.Location = new System.Drawing.Point(29, 482);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(330, 90);
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = false;
            this.StatusPanel.SP_Enabled = true;
            this.StatusPanel.SP_FullScreen = false;
            this.StatusPanel.TabIndex = 29;
            this.StatusPanel.Visible = false;
            // 
            // ValuesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 617);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.pnlList);
            this.HideOnClose = true;
            this.Name = "ValuesWindow";
            this.TabText = "Values";
            this.Text = "Values";
            ((System.ComponentModel.ISupportInitialize)(this.eventBindingSource)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlList.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvValues)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource eventBindingSource;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.CheckBox ckbFileInfo;
        private System.Windows.Forms.CheckBox ckbDistinct;
        private System.Windows.Forms.CheckBox ckbDocumentMap;
        public System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox tbRegularExpression;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlList;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox ckbLazyLoad;
        private System.Windows.Forms.DataGridView dgvValues;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.CheckBox ckbFilter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox clbSourceType;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckedListBox clbCategory;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;
    }
}