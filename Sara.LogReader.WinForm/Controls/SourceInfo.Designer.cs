

namespace Sara.LogReader.WinForm.Controls
{
    partial class SourceInfo
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
            this.pnlBase = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tvNameValue = new Sara.WinForm.ControlsNS.BufferedTreeView();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblType = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.warningPanel = new Sara.WinForm.ControlsNS.WarningPanel();
            this.ckbNavigation = new System.Windows.Forms.CheckBox();
            this.pnlBase.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBase
            // 
            this.pnlBase.Controls.Add(this.pnlContent);
            this.pnlBase.Controls.Add(this.warningPanel);
            this.pnlBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBase.Location = new System.Drawing.Point(0, 0);
            this.pnlBase.Name = "pnlBase";
            this.pnlBase.Size = new System.Drawing.Size(1011, 508);
            this.pnlBase.TabIndex = 20;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.tvNameValue);
            this.pnlContent.Controls.Add(this.pnlTop);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 84);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1011, 424);
            this.pnlContent.TabIndex = 23;
            // 
            // tvNameValue
            // 
            this.tvNameValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvNameValue.HideSelection = false;
            this.tvNameValue.Location = new System.Drawing.Point(0, 93);
            this.tvNameValue.Name = "tvNameValue";
            this.tvNameValue.Size = new System.Drawing.Size(1011, 331);
            this.tvNameValue.TabIndex = 23;
            this.tvNameValue.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvNameValue_AfterSelect);
            this.tvNameValue.DoubleClick += new System.EventHandler(this.tvNameValue_DoubleClick);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.SystemColors.Control;
            this.pnlTop.Controls.Add(this.ckbNavigation);
            this.pnlTop.Controls.Add(this.lblType);
            this.pnlTop.Controls.Add(this.label9);
            this.pnlTop.Controls.Add(this.lblEnd);
            this.pnlTop.Controls.Add(this.lblStart);
            this.pnlTop.Controls.Add(this.lblPath);
            this.pnlTop.Controls.Add(this.label3);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1011, 93);
            this.pnlTop.TabIndex = 22;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(62, 61);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(16, 13);
            this.lblType.TabIndex = 17;
            this.lblType.Text = "...";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(13, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Type:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnd.Location = new System.Drawing.Point(62, 44);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(16, 13);
            this.lblEnd.TabIndex = 15;
            this.lblEnd.Text = "...";
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStart.Location = new System.Drawing.Point(62, 27);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(16, 13);
            this.lblStart.TabIndex = 14;
            this.lblStart.Text = "...";
            // 
            // lblPath
            // 
            this.lblPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPath.Location = new System.Drawing.Point(65, 8);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(936, 20);
            this.lblPath.TabIndex = 13;
            this.lblPath.Text = "...";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "End:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Start:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Path:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // warningPanel
            // 
            this.warningPanel.BackColor = System.Drawing.SystemColors.Control;
            this.warningPanel.Content = this.pnlContent;
            this.warningPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.warningPanel.Location = new System.Drawing.Point(0, 0);
            this.warningPanel.Name = "warningPanel";
            this.warningPanel.Size = new System.Drawing.Size(1011, 84);
            this.warningPanel.TabIndex = 22;
            this.warningPanel.WarningText = "Open a File";
            // 
            // ckbNavigation
            // 
            this.ckbNavigation.AutoSize = true;
            this.ckbNavigation.Location = new System.Drawing.Point(65, 76);
            this.ckbNavigation.Name = "ckbNavigation";
            this.ckbNavigation.Size = new System.Drawing.Size(77, 17);
            this.ckbNavigation.TabIndex = 18;
            this.ckbNavigation.Text = "Navigation";
            this.ckbNavigation.UseVisualStyleBackColor = true;
            // 
            // SourceInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBase);
            this.Name = "SourceInfo";
            this.Size = new System.Drawing.Size(1011, 508);
            this.VisibleChanged += new System.EventHandler(this.SourceInfo_VisibleChanged);
            this.pnlBase.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlBase;
        private Sara.WinForm.ControlsNS.WarningPanel warningPanel;
        private System.Windows.Forms.Panel pnlContent;
        private Sara.WinForm.ControlsNS.BufferedTreeView tvNameValue;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.TextBox lblPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckbNavigation;
    }
}
