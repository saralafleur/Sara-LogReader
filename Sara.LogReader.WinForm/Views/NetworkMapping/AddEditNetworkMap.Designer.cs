

namespace Sara.LogReader.WinForm.Views.NetworkMapping
{
    partial class AddEditNetworkMap
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
            this.cbOnlyUseFallthrough = new System.Windows.Forms.CheckBox();
            this.nudPriority = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCriteriaName = new System.Windows.Forms.TextBox();
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            this.btnDeleteValue = new System.Windows.Forms.Button();
            this.btnEditValue = new System.Windows.Forms.Button();
            this.btnAddValue = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.lbCriteria = new System.Windows.Forms.ListBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbExpression = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbEnabled = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPriority)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbEnabled);
            this.panel1.Controls.Add(this.cbOnlyUseFallthrough);
            this.panel1.Controls.Add(this.nudPriority);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbCriteriaName);
            this.panel1.Controls.Add(this.StatusPanel);
            this.panel1.Controls.Add(this.btnDeleteValue);
            this.panel1.Controls.Add(this.btnEditValue);
            this.panel1.Controls.Add(this.btnAddValue);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.lbCriteria);
            this.panel1.Controls.Add(this.btnTest);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.tbExpression);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(514, 327);
            this.panel1.TabIndex = 24;
            // 
            // cbOnlyUseFallthrough
            // 
            this.cbOnlyUseFallthrough.AutoSize = true;
            this.cbOnlyUseFallthrough.Location = new System.Drawing.Point(113, 129);
            this.cbOnlyUseFallthrough.Name = "cbOnlyUseFallthrough";
            this.cbOnlyUseFallthrough.Size = new System.Drawing.Size(202, 17);
            this.cbOnlyUseFallthrough.TabIndex = 4;
            this.cbOnlyUseFallthrough.Text = "Only use if prior Maps have no results";
            this.cbOnlyUseFallthrough.UseVisualStyleBackColor = true;
            // 
            // nudPriority
            // 
            this.nudPriority.Location = new System.Drawing.Point(14, 128);
            this.nudPriority.Name = "nudPriority";
            this.nudPriority.Size = new System.Drawing.Size(93, 20);
            this.nudPriority.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 109;
            this.label2.Text = "Priority";
            // 
            // tbCriteriaName
            // 
            this.tbCriteriaName.Location = new System.Drawing.Point(14, 89);
            this.tbCriteriaName.Name = "tbCriteriaName";
            this.tbCriteriaName.Size = new System.Drawing.Size(413, 20);
            this.tbCriteriaName.TabIndex = 2;
            // 
            // StatusPanel
            // 
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = false;
            this.StatusPanel.Location = new System.Drawing.Point(96, 151);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(354, 71);
            this.StatusPanel.TabIndex = 25;
            this.StatusPanel.Visible = false;
            // 
            // btnDeleteValue
            // 
            this.btnDeleteValue.Location = new System.Drawing.Point(456, 141);
            this.btnDeleteValue.Name = "btnDeleteValue";
            this.btnDeleteValue.Size = new System.Drawing.Size(52, 23);
            this.btnDeleteValue.TabIndex = 107;
            this.btnDeleteValue.Text = "Delete";
            this.btnDeleteValue.UseVisualStyleBackColor = true;
            this.btnDeleteValue.Click += new System.EventHandler(this.btnDeleteValue_Click);
            // 
            // btnEditValue
            // 
            this.btnEditValue.Location = new System.Drawing.Point(398, 141);
            this.btnEditValue.Name = "btnEditValue";
            this.btnEditValue.Size = new System.Drawing.Size(52, 23);
            this.btnEditValue.TabIndex = 106;
            this.btnEditValue.Text = "Edit";
            this.btnEditValue.UseVisualStyleBackColor = true;
            this.btnEditValue.Click += new System.EventHandler(this.btnEditValue_Click);
            // 
            // btnAddValue
            // 
            this.btnAddValue.Location = new System.Drawing.Point(340, 141);
            this.btnAddValue.Name = "btnAddValue";
            this.btnAddValue.Size = new System.Drawing.Size(52, 23);
            this.btnAddValue.TabIndex = 105;
            this.btnAddValue.Text = "Add";
            this.btnAddValue.UseVisualStyleBackColor = true;
            this.btnAddValue.Click += new System.EventHandler(this.btnAddValue_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 151);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 13);
            this.label14.TabIndex = 104;
            this.label14.Text = "Critera";
            // 
            // lbCriteria
            // 
            this.lbCriteria.FormattingEnabled = true;
            this.lbCriteria.Location = new System.Drawing.Point(13, 167);
            this.lbCriteria.Name = "lbCriteria";
            this.lbCriteria.Size = new System.Drawing.Size(495, 121);
            this.lbCriteria.TabIndex = 5;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(433, 47);
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
            this.label4.Location = new System.Drawing.Point(9, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Name";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(434, 297);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(353, 297);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbExpression
            // 
            this.tbExpression.Location = new System.Drawing.Point(12, 50);
            this.tbExpression.Name = "tbExpression";
            this.tbExpression.Size = new System.Drawing.Size(415, 20);
            this.tbExpression.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Regular Expression";
            // 
            // cbEnabled
            // 
            this.cbEnabled.AutoSize = true;
            this.cbEnabled.Checked = true;
            this.cbEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEnabled.Location = new System.Drawing.Point(12, 12);
            this.cbEnabled.Name = "cbEnabled";
            this.cbEnabled.Size = new System.Drawing.Size(65, 17);
            this.cbEnabled.TabIndex = 110;
            this.cbEnabled.Text = "Enabled";
            this.cbEnabled.UseVisualStyleBackColor = true;
            // 
            // AddEditNetworkMap
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(514, 327);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEditNetworkMap";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Network Map";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPriority)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.TextBox tbExpression;
        private System.Windows.Forms.Label label1;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;
        private System.Windows.Forms.Button btnDeleteValue;
        private System.Windows.Forms.Button btnEditValue;
        private System.Windows.Forms.Button btnAddValue;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ListBox lbCriteria;
        public System.Windows.Forms.TextBox tbCriteriaName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbOnlyUseFallthrough;
        private System.Windows.Forms.NumericUpDown nudPriority;
        private System.Windows.Forms.CheckBox cbEnabled;

    }
}