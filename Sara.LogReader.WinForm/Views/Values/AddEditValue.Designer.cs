

namespace Sara.LogReader.WinForm.Views.Values
{
    partial class AddEditValue
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.clbSourceType = new System.Windows.Forms.CheckedListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.clbCategory = new System.Windows.Forms.CheckedListBox();
            this.ckbLazyLoad = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.ckbDistinct = new System.Windows.Forms.CheckBox();
            this.cbValueName = new System.Windows.Forms.ComboBox();
            this.ckbFileInfo = new System.Windows.Forms.CheckBox();
            this.ckbDocumentMap = new System.Windows.Forms.CheckBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbExpression = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSort = new System.Windows.Forms.TextBox();
            this.lblSort = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbSort);
            this.panel1.Controls.Add(this.lblSort);
            this.panel1.Controls.Add(this.StatusPanel);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.clbSourceType);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.clbCategory);
            this.panel1.Controls.Add(this.ckbLazyLoad);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.cbLevel);
            this.panel1.Controls.Add(this.ckbDistinct);
            this.panel1.Controls.Add(this.cbValueName);
            this.panel1.Controls.Add(this.ckbFileInfo);
            this.panel1.Controls.Add(this.ckbDocumentMap);
            this.panel1.Controls.Add(this.btnTest);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.tbExpression);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(514, 404);
            this.panel1.TabIndex = 24;
            // 
            // StatusPanel
            // 
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = false;
            this.StatusPanel.Location = new System.Drawing.Point(82, 157);
            this.StatusPanel.SP_FullScreen = false;
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(354, 71);
            this.StatusPanel.TabIndex = 25;
            this.StatusPanel.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(487, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Source Type (Used to limit which File Type the EventPattern is applied to.  \"ANY\" will" +
    " apply to all File Types)";
            // 
            // clbSourceType
            // 
            this.clbSourceType.FormattingEnabled = true;
            this.clbSourceType.Location = new System.Drawing.Point(12, 257);
            this.clbSourceType.MultiColumn = true;
            this.clbSourceType.Name = "clbSourceType";
            this.clbSourceType.Size = new System.Drawing.Size(496, 94);
            this.clbSourceType.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 128);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 112;
            this.label13.Text = "Categories";
            // 
            // clbCategory
            // 
            this.clbCategory.FormattingEnabled = true;
            this.clbCategory.Location = new System.Drawing.Point(12, 144);
            this.clbCategory.MultiColumn = true;
            this.clbCategory.Name = "clbCategory";
            this.clbCategory.Size = new System.Drawing.Size(496, 94);
            this.clbCategory.TabIndex = 5;
            // 
            // ckbLazyLoad
            // 
            this.ckbLazyLoad.AutoSize = true;
            this.ckbLazyLoad.Location = new System.Drawing.Point(110, 366);
            this.ckbLazyLoad.Name = "ckbLazyLoad";
            this.ckbLazyLoad.Size = new System.Drawing.Size(75, 17);
            this.ckbLazyLoad.TabIndex = 9;
            this.ckbLazyLoad.Text = "Lazy Load";
            this.ckbLazyLoad.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(358, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(109, 13);
            this.label10.TabIndex = 92;
            this.label10.Text = "Document Map Level";
            // 
            // cbLevel
            // 
            this.cbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevel.FormattingEnabled = true;
            this.cbLevel.Location = new System.Drawing.Point(361, 65);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(147, 21);
            this.cbLevel.TabIndex = 3;
            // 
            // ckbDistinct
            // 
            this.ckbDistinct.AutoSize = true;
            this.ckbDistinct.Location = new System.Drawing.Point(191, 366);
            this.ckbDistinct.Name = "ckbDistinct";
            this.ckbDistinct.Size = new System.Drawing.Size(61, 17);
            this.ckbDistinct.TabIndex = 10;
            this.ckbDistinct.Text = "Distinct";
            this.ckbDistinct.UseVisualStyleBackColor = true;
            // 
            // cbValueName
            // 
            this.cbValueName.FormattingEnabled = true;
            this.cbValueName.Location = new System.Drawing.Point(12, 65);
            this.cbValueName.Name = "cbValueName";
            this.cbValueName.Size = new System.Drawing.Size(343, 21);
            this.cbValueName.TabIndex = 2;
            // 
            // ckbFileInfo
            // 
            this.ckbFileInfo.AutoSize = true;
            this.ckbFileInfo.Location = new System.Drawing.Point(258, 366);
            this.ckbFileInfo.Name = "ckbFileInfo";
            this.ckbFileInfo.Size = new System.Drawing.Size(63, 17);
            this.ckbFileInfo.TabIndex = 11;
            this.ckbFileInfo.Text = "File Info";
            this.ckbFileInfo.UseVisualStyleBackColor = true;
            // 
            // ckbDocumentMap
            // 
            this.ckbDocumentMap.AutoSize = true;
            this.ckbDocumentMap.Location = new System.Drawing.Point(11, 366);
            this.ckbDocumentMap.Name = "ckbDocumentMap";
            this.ckbDocumentMap.Size = new System.Drawing.Size(93, 17);
            this.ckbDocumentMap.TabIndex = 8;
            this.ckbDocumentMap.Text = "Documet Map";
            this.ckbDocumentMap.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(433, 22);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Value Name";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(433, 362);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(352, 362);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbExpression
            // 
            this.tbExpression.Location = new System.Drawing.Point(12, 25);
            this.tbExpression.Name = "tbExpression";
            this.tbExpression.Size = new System.Drawing.Size(415, 20);
            this.tbExpression.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Regular Expression";
            // 
            // tbSort
            // 
            this.tbSort.Location = new System.Drawing.Point(12, 105);
            this.tbSort.Name = "tbSort";
            this.tbSort.Size = new System.Drawing.Size(95, 20);
            this.tbSort.TabIndex = 4;
            // 
            // lblSort
            // 
            this.lblSort.AutoSize = true;
            this.lblSort.Location = new System.Drawing.Point(9, 89);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(26, 13);
            this.lblSort.TabIndex = 116;
            this.lblSort.Text = "Sort";
            // 
            // AddEditValue
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(514, 404);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEditValue";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Value";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox ckbDistinct;
        private System.Windows.Forms.ComboBox cbValueName;
        private System.Windows.Forms.CheckBox ckbFileInfo;
        private System.Windows.Forms.CheckBox ckbDocumentMap;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.TextBox tbExpression;
        private System.Windows.Forms.Label label1;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbLevel;
        private System.Windows.Forms.CheckBox ckbLazyLoad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox clbSourceType;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckedListBox clbCategory;
        public System.Windows.Forms.TextBox tbSort;
        private System.Windows.Forms.Label lblSort;
    }
}