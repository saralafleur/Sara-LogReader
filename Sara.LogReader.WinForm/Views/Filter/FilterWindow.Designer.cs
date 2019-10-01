
using Sara.LogReader.WinForm.Controls;

namespace Sara.LogReader.WinForm.Views.Filter
{
    partial class FilterWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterWindow));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.panel7 = new System.Windows.Forms.Panel();
            this.pnlView = new System.Windows.Forms.SplitContainer();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tvCategories = new Sara.WinForm.ControlsNS.BufferedTreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCApply = new System.Windows.Forms.Button();
            this.ckbCategoriesAllorNone = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            this.warningPanel = new Sara.WinForm.ControlsNS.WarningPanel();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlView)).BeginInit();
            this.pnlView.Panel1.SuspendLayout();
            this.pnlView.Panel2.SuspendLayout();
            this.pnlView.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Mouse.bmp");
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.pnlView);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 68);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(410, 468);
            this.panel7.TabIndex = 16;
            // 
            // pnlView
            // 
            this.pnlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlView.Location = new System.Drawing.Point(0, 0);
            this.pnlView.Name = "pnlView";
            this.pnlView.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // pnlView.Panel1
            // 
            this.pnlView.Panel1.Controls.Add(this.panel5);
            this.pnlView.Panel1Collapsed = true;
            // 
            // pnlView.Panel2
            // 
            this.pnlView.Panel2.Controls.Add(this.panel2);
            this.pnlView.Size = new System.Drawing.Size(410, 468);
            this.pnlView.SplitterDistance = 254;
            this.pnlView.TabIndex = 9;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(150, 254);
            this.panel5.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(410, 468);
            this.panel2.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tvCategories);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 28);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(410, 440);
            this.panel4.TabIndex = 2;
            // 
            // tvCategories
            // 
            this.tvCategories.CheckBoxes = true;
            this.tvCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCategories.Location = new System.Drawing.Point(0, 0);
            this.tvCategories.Name = "tvCategories";
            this.tvCategories.Size = new System.Drawing.Size(410, 440);
            this.tvCategories.TabIndex = 8;
            this.tvCategories.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.AfterCheck);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCApply);
            this.panel3.Controls.Add(this.ckbCategoriesAllorNone);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(410, 28);
            this.panel3.TabIndex = 1;
            // 
            // btnCApply
            // 
            this.btnCApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCApply.Location = new System.Drawing.Point(354, 4);
            this.btnCApply.Name = "btnCApply";
            this.btnCApply.Size = new System.Drawing.Size(53, 23);
            this.btnCApply.TabIndex = 8;
            this.btnCApply.Text = "Apply";
            this.btnCApply.UseVisualStyleBackColor = true;
            this.btnCApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // ckbCategoriesAllorNone
            // 
            this.ckbCategoriesAllorNone.AutoSize = true;
            this.ckbCategoriesAllorNone.Checked = true;
            this.ckbCategoriesAllorNone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbCategoriesAllorNone.Location = new System.Drawing.Point(25, 9);
            this.ckbCategoriesAllorNone.Name = "ckbCategoriesAllorNone";
            this.ckbCategoriesAllorNone.Size = new System.Drawing.Size(15, 14);
            this.ckbCategoriesAllorNone.TabIndex = 27;
            this.ckbCategoriesAllorNone.UseVisualStyleBackColor = true;
            this.ckbCategoriesAllorNone.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Categories";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // StatusPanel
            // 
            this.StatusPanel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.StatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusPanel.Location = new System.Drawing.Point(38, 222);
            this.StatusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Padding = new System.Windows.Forms.Padding(1);
            this.StatusPanel.Size = new System.Drawing.Size(335, 95);
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = false;
            this.StatusPanel.SP_Enabled = true;
            this.StatusPanel.SP_FullScreen = true;
            this.StatusPanel.TabIndex = 18;
            this.StatusPanel.Visible = false;
            // 
            // warningPanel
            // 
            this.warningPanel.BackColor = System.Drawing.SystemColors.Control;
            this.warningPanel.Content = null;
            this.warningPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.warningPanel.Location = new System.Drawing.Point(0, 2);
            this.warningPanel.Name = "warningPanel";
            this.warningPanel.Size = new System.Drawing.Size(410, 66);
            this.warningPanel.TabIndex = 14;
            this.warningPanel.WarningText = "Open a File";
            // 
            // FilterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(410, 538);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.warningPanel);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FilterWindow";
            this.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeftAutoHide;
            this.TabText = "Filter";
            this.Text = "Filter";
            this.panel7.ResumeLayout(false);
            this.pnlView.Panel1.ResumeLayout(false);
            this.pnlView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlView)).EndInit();
            this.pnlView.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private Sara.WinForm.ControlsNS.WarningPanel warningPanel;
        private System.Windows.Forms.Button btnCApply;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.SplitContainer pnlView;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ImageList imageList;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;
        private System.Windows.Forms.CheckBox ckbCategoriesAllorNone;
        private Sara.WinForm.ControlsNS.BufferedTreeView tvCategories;

    }
}