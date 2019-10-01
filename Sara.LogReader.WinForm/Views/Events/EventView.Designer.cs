

namespace Sara.LogReader.WinForm.Views.Events
{
    partial class EventView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.eventBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlList = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvEvents = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.clbSourceType = new System.Windows.Forms.CheckedListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.clbCategory = new System.Windows.Forms.CheckedListBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.ckbDocumentMap = new System.Windows.Forms.CheckBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRegularExpression = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbFilter = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.eventBindingSource)).BeginInit();
            this.pnlList.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).BeginInit();
            this.panel3.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlList
            // 
            this.pnlList.Controls.Add(this.panel4);
            this.pnlList.Controls.Add(this.panel3);
            this.pnlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlList.Location = new System.Drawing.Point(0, 0);
            this.pnlList.Name = "pnlList";
            this.pnlList.Size = new System.Drawing.Size(493, 625);
            this.pnlList.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgvEvents);
            this.panel4.Controls.Add(this.pnlFilter);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 42);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(493, 583);
            this.panel4.TabIndex = 7;
            // 
            // dgvEvents
            // 
            this.dgvEvents.AllowUserToResizeRows = false;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEvents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEvents.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEvents.Location = new System.Drawing.Point(0, 366);
            this.dgvEvents.MultiSelect = false;
            this.dgvEvents.Name = "dgvEvents";
            this.dgvEvents.ReadOnly = true;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEvents.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvEvents.RowHeadersVisible = false;
            this.dgvEvents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEvents.Size = new System.Drawing.Size(493, 217);
            this.dgvEvents.TabIndex = 7;
            this.dgvEvents.SelectionChanged += new System.EventHandler(this.dgvEvents_SelectionChanged);
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
            this.panel3.Size = new System.Drawing.Size(493, 42);
            this.panel3.TabIndex = 5;
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(147, 10);
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
            this.btnEdit.Location = new System.Drawing.Point(80, 10);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(61, 23);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(13, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(61, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // StatusPanel
            // 
            this.StatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusPanel.Location = new System.Drawing.Point(69, 277);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(354, 71);
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = false;
            this.StatusPanel.SP_Enabled = true;
            this.StatusPanel.SP_FullScreen = false;
            this.StatusPanel.TabIndex = 28;
            this.StatusPanel.Visible = false;
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.label5);
            this.pnlFilter.Controls.Add(this.clbSourceType);
            this.pnlFilter.Controls.Add(this.label13);
            this.pnlFilter.Controls.Add(this.clbCategory);
            this.pnlFilter.Controls.Add(this.btnApply);
            this.pnlFilter.Controls.Add(this.btnClearFilter);
            this.pnlFilter.Controls.Add(this.ckbDocumentMap);
            this.pnlFilter.Controls.Add(this.tbName);
            this.pnlFilter.Controls.Add(this.label2);
            this.pnlFilter.Controls.Add(this.tbRegularExpression);
            this.pnlFilter.Controls.Add(this.label1);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(493, 366);
            this.pnlFilter.TabIndex = 27;
            this.pnlFilter.Visible = false;
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
            this.clbSourceType.Size = new System.Drawing.Size(469, 94);
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
            this.clbCategory.Size = new System.Drawing.Size(469, 94);
            this.clbCategory.TabIndex = 113;
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
            this.tbName.Size = new System.Drawing.Size(469, 20);
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
            this.tbRegularExpression.Size = new System.Drawing.Size(469, 20);
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
            // ckbFilter
            // 
            this.ckbFilter.AutoSize = true;
            this.ckbFilter.Location = new System.Drawing.Point(214, 14);
            this.ckbFilter.Name = "ckbFilter";
            this.ckbFilter.Size = new System.Drawing.Size(48, 17);
            this.ckbFilter.TabIndex = 7;
            this.ckbFilter.Text = "Filter";
            this.ckbFilter.UseVisualStyleBackColor = true;
            this.ckbFilter.CheckedChanged += new System.EventHandler(this.ckbFilter_CheckedChanged);
            // 
            // EventView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 625);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.pnlList);
            this.HideOnClose = true;
            this.Name = "EventView";
            this.TabText = "Events";
            this.Text = "Events";
            ((System.ComponentModel.ISupportInitialize)(this.eventBindingSource)).EndInit();
            this.pnlList.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource eventBindingSource;
        private System.Windows.Forms.Panel pnlList;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvEvents;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox clbSourceType;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckedListBox clbCategory;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.CheckBox ckbDocumentMap;
        public System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox tbRegularExpression;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckbFilter;
    }
}