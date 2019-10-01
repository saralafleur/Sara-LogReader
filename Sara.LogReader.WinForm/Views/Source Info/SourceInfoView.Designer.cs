
using Sara.LogReader.WinForm.Controls;

namespace Sara.LogReader.WinForm.Views.SourceInfo
{
    partial class SourceInfoView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourceInfoView));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            this.sourceInfo1 = new Sara.LogReader.WinForm.Controls.SourceInfo();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Mouse.bmp");
            // 
            // StatusPanel
            // 
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = false;
            this.StatusPanel.Location = new System.Drawing.Point(38, 222);
            this.StatusPanel.SP_FullScreen = true;
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(335, 95);
            this.StatusPanel.TabIndex = 18;
            this.StatusPanel.Visible = false;
            // 
            // sourceInfo1
            // 
            this.sourceInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceInfo1.Location = new System.Drawing.Point(0, 2);
            this.sourceInfo1.Name = "sourceInfo1";
            this.sourceInfo1.Size = new System.Drawing.Size(410, 534);
            this.sourceInfo1.TabIndex = 19;
            this.sourceInfo1.GoTo += new System.Action<Sara.LogReader.Model.FileNS.ValueBookMark>(this.sourceInfo1_GoTo);
            // 
            // SourceInfoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(410, 538);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.sourceInfo1);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SourceInfoView";
            this.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeftAutoHide;
            this.TabText = "Source Info";
            this.Text = "Source Info";
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.ImageList imageList;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;
        private Controls.SourceInfo sourceInfo1;
    }
}