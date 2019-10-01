

namespace Sara.LogReader.WinForm.Views.NetworkMapNS
{
    partial class NetworkMapView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wpOpen = new Sara.WinForm.ControlsNS.WarningPanel();
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cklbSources = new System.Windows.Forms.CheckedListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExpand = new System.Windows.Forms.Button();
            this.ckbAnchor = new System.Windows.Forms.CheckBox();
            this.dgvSequenceDiagram = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSequenceDiagram)).BeginInit();
            this.SuspendLayout();
            // 
            // wpOpen
            // 
            this.wpOpen.Dock = System.Windows.Forms.DockStyle.Top;
            this.wpOpen.Location = new System.Drawing.Point(0, 0);
            this.wpOpen.Name = "wpOpen";
            this.wpOpen.Size = new System.Drawing.Size(660, 150);
            this.wpOpen.TabIndex = 20;
            this.wpOpen.WarningText = "Open a File";
            // 
            // StatusPanel
            // 
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = false;
            this.StatusPanel.Location = new System.Drawing.Point(215, 206);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(231, 58);
            this.StatusPanel.TabIndex = 22;
            this.StatusPanel.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 150);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(660, 320);
            this.panel1.TabIndex = 24;
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
            this.splitContainer1.Panel1.Controls.Add(this.cklbSources);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvSequenceDiagram);
            this.splitContainer1.Size = new System.Drawing.Size(660, 320);
            this.splitContainer1.SplitterDistance = 73;
            this.splitContainer1.TabIndex = 26;
            // 
            // cklbSources
            // 
            this.cklbSources.CheckOnClick = true;
            this.cklbSources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cklbSources.FormattingEnabled = true;
            this.cklbSources.Location = new System.Drawing.Point(309, 0);
            this.cklbSources.Name = "cklbSources";
            this.cklbSources.Size = new System.Drawing.Size(351, 73);
            this.cklbSources.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExpand);
            this.panel2.Controls.Add(this.ckbAnchor);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(309, 73);
            this.panel2.TabIndex = 4;
            // 
            // btnExpand
            // 
            this.btnExpand.Location = new System.Drawing.Point(12, 29);
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(94, 23);
            this.btnExpand.TabIndex = 5;
            this.btnExpand.Text = "Apply (Expand)";
            this.btnExpand.UseVisualStyleBackColor = true;
            this.btnExpand.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // ckbAnchor
            // 
            this.ckbAnchor.AutoSize = true;
            this.ckbAnchor.Location = new System.Drawing.Point(12, 6);
            this.ckbAnchor.Name = "ckbAnchor";
            this.ckbAnchor.Size = new System.Drawing.Size(60, 17);
            this.ckbAnchor.TabIndex = 4;
            this.ckbAnchor.Text = "Anchor";
            this.ckbAnchor.UseVisualStyleBackColor = true;
            this.ckbAnchor.CheckedChanged += new System.EventHandler(this.ckbAnchor_CheckedChanged);
            // 
            // dgvSequenceDiagram
            // 
            this.dgvSequenceDiagram.AllowUserToAddRows = false;
            this.dgvSequenceDiagram.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSequenceDiagram.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSequenceDiagram.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSequenceDiagram.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSequenceDiagram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSequenceDiagram.Location = new System.Drawing.Point(0, 0);
            this.dgvSequenceDiagram.Name = "dgvSequenceDiagram";
            this.dgvSequenceDiagram.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSequenceDiagram.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSequenceDiagram.Size = new System.Drawing.Size(660, 243);
            this.dgvSequenceDiagram.TabIndex = 25;
            this.dgvSequenceDiagram.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSequenceDiagram_KeyDown);
            // 
            // NetworkMapView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 470);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.wpOpen);
            this.HideOnClose = true;
            this.Name = "NetworkMapView";
            this.TabText = "Network Map";
            this.Text = "Network Map";
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSequenceDiagram)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sara.WinForm.ControlsNS.WarningPanel wpOpen;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox ckbAnchor;
        private System.Windows.Forms.CheckedListBox cklbSources;
        private System.Windows.Forms.DataGridView dgvSequenceDiagram;
        private System.Windows.Forms.Button btnExpand;
    }
}