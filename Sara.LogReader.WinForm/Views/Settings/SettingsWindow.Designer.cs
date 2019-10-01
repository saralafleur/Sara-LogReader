

namespace Sara.LogReader.WinForm.Views.Settings
{
    partial class SettingsWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbSockDrawerFolder = new System.Windows.Forms.TextBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lbFileTypes = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.tbFileType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbNonLazyValueWarningTimeout = new Sara.WinForm.ControlsNS.NumericTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnEditColorScheme = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbColorScheme = new System.Windows.Forms.ComboBox();
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sock Drawer Folder";
            // 
            // tbSockDrawerFolder
            // 
            this.tbSockDrawerFolder.Enabled = false;
            this.tbSockDrawerFolder.Location = new System.Drawing.Point(9, 25);
            this.tbSockDrawerFolder.Name = "tbSockDrawerFolder";
            this.tbSockDrawerFolder.Size = new System.Drawing.Size(260, 20);
            this.tbSockDrawerFolder.TabIndex = 1;
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(274, 66);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(355, 66);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(275, 22);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbFileTypes
            // 
            this.lbFileTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbFileTypes.FormattingEnabled = true;
            this.lbFileTypes.Location = new System.Drawing.Point(12, 48);
            this.lbFileTypes.Name = "lbFileTypes";
            this.lbFileTypes.Size = new System.Drawing.Size(337, 251);
            this.lbFileTypes.TabIndex = 6;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(355, 20);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(355, 51);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 9;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.button3_Click);
            // 
            // tbFileType
            // 
            this.tbFileType.Location = new System.Drawing.Point(12, 22);
            this.tbFileType.Name = "tbFileType";
            this.tbFileType.Size = new System.Drawing.Size(337, 20);
            this.tbFileType.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "File Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Non-Lazy Value Warning Timeout";
            // 
            // tbNonLazyValueWarningTimeout
            // 
            this.tbNonLazyValueWarningTimeout.AllowSpace = false;
            this.tbNonLazyValueWarningTimeout.Location = new System.Drawing.Point(12, 65);
            this.tbNonLazyValueWarningTimeout.Name = "tbNonLazyValueWarningTimeout";
            this.tbNonLazyValueWarningTimeout.Size = new System.Drawing.Size(163, 20);
            this.tbNonLazyValueWarningTimeout.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbFileTypes);
            this.panel1.Controls.Add(this.btnRemove);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.tbFileType);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 87);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(437, 300);
            this.panel1.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbSockDrawerFolder);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tbNonLazyValueWarningTimeout);
            this.panel2.Controls.Add(this.btnSelect);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(437, 87);
            this.panel2.TabIndex = 15;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnEditColorScheme);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.cbColorScheme);
            this.panel3.Controls.Add(this.btnAccept);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 387);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(437, 97);
            this.panel3.TabIndex = 16;
            // 
            // btnEditColorScheme
            // 
            this.btnEditColorScheme.Location = new System.Drawing.Point(275, 19);
            this.btnEditColorScheme.Name = "btnEditColorScheme";
            this.btnEditColorScheme.Size = new System.Drawing.Size(75, 23);
            this.btnEditColorScheme.TabIndex = 14;
            this.btnEditColorScheme.Text = "Edit";
            this.btnEditColorScheme.UseVisualStyleBackColor = true;
            this.btnEditColorScheme.Click += new System.EventHandler(this.btnEditColorScheme_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Color Scheme";
            // 
            // cbColorScheme
            // 
            this.cbColorScheme.FormattingEnabled = true;
            this.cbColorScheme.Location = new System.Drawing.Point(12, 19);
            this.cbColorScheme.Name = "cbColorScheme";
            this.cbColorScheme.Size = new System.Drawing.Size(257, 21);
            this.cbColorScheme.TabIndex = 5;
            // 
            // StatusPanel
            // 
            this.StatusPanel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.StatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusPanel.Location = new System.Drawing.Point(53, 197);
            this.StatusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Padding = new System.Windows.Forms.Padding(1);
            this.StatusPanel.Size = new System.Drawing.Size(330, 90);
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = false;
            this.StatusPanel.SP_Enabled = true;
            this.StatusPanel.SP_FullScreen = false;
            this.StatusPanel.TabIndex = 32;
            this.StatusPanel.Visible = false;
            // 
            // SettingsWindow
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(437, 484);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SockDrawerSettingsWindow_Load);
            this.Shown += new System.EventHandler(this.SettingsWindow_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSockDrawerFolder;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ListBox lbFileTypes;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.TextBox tbFileType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Sara.WinForm.ControlsNS.NumericTextBox tbNonLazyValueWarningTimeout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbColorScheme;
        private System.Windows.Forms.Button btnEditColorScheme;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;
    }
}