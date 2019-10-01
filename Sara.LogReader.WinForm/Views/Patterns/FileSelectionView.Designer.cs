
using Sara.LogReader.WinForm.Controls;

namespace Sara.LogReader.WinForm.Views.Patterns
{
    partial class FileSelectionView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileSelectionView));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tvFiles = new Sara.WinForm.ControlsNS.BufferedTreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ckbFilesAllOrNone = new System.Windows.Forms.CheckBox();
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbGrouping = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 557);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(382, 30);
            this.panel1.TabIndex = 11;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(304, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(223, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // tvFiles
            // 
            this.tvFiles.CheckBoxes = true;
            this.tvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFiles.Location = new System.Drawing.Point(0, 36);
            this.tvFiles.Name = "tvFiles";
            this.tvFiles.Size = new System.Drawing.Size(382, 521);
            this.tvFiles.TabIndex = 0;
            this.tvFiles.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvFiles_AfterCheck);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbGrouping);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.ckbFilesAllOrNone);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(382, 36);
            this.panel3.TabIndex = 16;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Sara.LogReader.WinForm.Properties.Resources.CollaspeAll;
            this.pictureBox2.Location = new System.Drawing.Point(49, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(14, 14);
            this.pictureBox2.TabIndex = 19;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(31, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(14, 14);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // ckbFilesAllOrNone
            // 
            this.ckbFilesAllOrNone.AutoSize = true;
            this.ckbFilesAllOrNone.Location = new System.Drawing.Point(12, 12);
            this.ckbFilesAllOrNone.Name = "ckbFilesAllOrNone";
            this.ckbFilesAllOrNone.Size = new System.Drawing.Size(15, 14);
            this.ckbFilesAllOrNone.TabIndex = 17;
            this.ckbFilesAllOrNone.UseVisualStyleBackColor = true;
            this.ckbFilesAllOrNone.CheckedChanged += new System.EventHandler(this.ckbFilesAllOrNone_CheckedChanged);
            // 
            // StatusPanel
            // 
            this.StatusPanel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.StatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusPanel.Location = new System.Drawing.Point(61, 224);
            this.StatusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Padding = new System.Windows.Forms.Padding(1);
            this.StatusPanel.Size = new System.Drawing.Size(262, 100);
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = false;
            this.StatusPanel.SP_Enabled = true;
            this.StatusPanel.SP_FullScreen = true;
            this.StatusPanel.TabIndex = 20;
            this.StatusPanel.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Grouping";
            // 
            // cbGrouping
            // 
            this.cbGrouping.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrouping.FormattingEnabled = true;
            this.cbGrouping.Items.AddRange(new object[] {
            "Customer",
            "Customer|Year|Month|Week",
            "Customer|Year|Month",
            "Customer|Year",
            "Year|Month|Week",
            "Year|Month",
            "Week"});
            this.cbGrouping.Location = new System.Drawing.Point(126, 9);
            this.cbGrouping.Name = "cbGrouping";
            this.cbGrouping.Size = new System.Drawing.Size(172, 21);
            this.cbGrouping.TabIndex = 21;
            this.cbGrouping.SelectedIndexChanged += new System.EventHandler(this.cbGrouping_SelectedIndexChanged);
            // 
            // FileSelectionView
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(382, 587);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.tvFiles);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "FileSelectionView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Files";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panel3;
        private Sara.WinForm.ControlsNS.BufferedTreeView tvFiles;
        private System.Windows.Forms.CheckBox ckbFilesAllOrNone;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;
        private System.Windows.Forms.ComboBox cbGrouping;
        private System.Windows.Forms.Label label1;
    }
}