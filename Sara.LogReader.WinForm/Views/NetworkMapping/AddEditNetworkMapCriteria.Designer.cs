namespace Sara.LogReader.WinForm.Views.NetworkMapping
{
    partial class AddEditNetworkMapCriteria
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSourceType = new System.Windows.Forms.ComboBox();
            this.cbTargetType = new System.Windows.Forms.ComboBox();
            this.tbTimeCondition = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbSourceName = new System.Windows.Forms.ComboBox();
            this.cbTargetName = new System.Windows.Forms.ComboBox();
            this.tbSourceValue = new System.Windows.Forms.TextBox();
            this.lblSourceValue = new System.Windows.Forms.Label();
            this.cbSourceValue = new System.Windows.Forms.CheckBox();
            this.cbTargetValue = new System.Windows.Forms.CheckBox();
            this.tbTargetValue = new System.Windows.Forms.TextBox();
            this.lblTargetValue = new System.Windows.Forms.Label();
            this.cbEnabled = new System.Windows.Forms.CheckBox();
            this.cbOperator = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(338, 273);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(257, 273);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 101;
            this.label4.Text = "Source Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 100;
            this.label3.Text = "Source Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 99;
            this.label2.Text = "Target Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 98;
            this.label1.Text = "Target Name";
            // 
            // cbSourceType
            // 
            this.cbSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSourceType.FormattingEnabled = true;
            this.cbSourceType.Items.AddRange(new object[] {
            "File Value",
            "EventPattern Value"});
            this.cbSourceType.Location = new System.Drawing.Point(215, 48);
            this.cbSourceType.Name = "cbSourceType";
            this.cbSourceType.Size = new System.Drawing.Size(200, 21);
            this.cbSourceType.TabIndex = 1;
            // 
            // cbTargetType
            // 
            this.cbTargetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTargetType.FormattingEnabled = true;
            this.cbTargetType.Items.AddRange(new object[] {
            "File Value",
            "EventPattern Value"});
            this.cbTargetType.Location = new System.Drawing.Point(214, 167);
            this.cbTargetType.Name = "cbTargetType";
            this.cbTargetType.Size = new System.Drawing.Size(200, 21);
            this.cbTargetType.TabIndex = 5;
            // 
            // tbTimeCondition
            // 
            this.tbTimeCondition.Location = new System.Drawing.Point(8, 246);
            this.tbTimeCondition.Name = "tbTimeCondition";
            this.tbTimeCondition.Size = new System.Drawing.Size(405, 20);
            this.tbTimeCondition.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 230);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 103;
            this.label5.Text = "Time Condition (MS)";
            // 
            // cbSourceName
            // 
            this.cbSourceName.FormattingEnabled = true;
            this.cbSourceName.Location = new System.Drawing.Point(9, 48);
            this.cbSourceName.Name = "cbSourceName";
            this.cbSourceName.Size = new System.Drawing.Size(200, 21);
            this.cbSourceName.TabIndex = 0;
            // 
            // cbTargetName
            // 
            this.cbTargetName.FormattingEnabled = true;
            this.cbTargetName.Location = new System.Drawing.Point(8, 167);
            this.cbTargetName.Name = "cbTargetName";
            this.cbTargetName.Size = new System.Drawing.Size(200, 21);
            this.cbTargetName.TabIndex = 4;
            // 
            // tbSourceValue
            // 
            this.tbSourceValue.Location = new System.Drawing.Point(9, 88);
            this.tbSourceValue.Name = "tbSourceValue";
            this.tbSourceValue.Size = new System.Drawing.Size(405, 20);
            this.tbSourceValue.TabIndex = 3;
            // 
            // lblSourceValue
            // 
            this.lblSourceValue.AutoSize = true;
            this.lblSourceValue.Location = new System.Drawing.Point(6, 72);
            this.lblSourceValue.Name = "lblSourceValue";
            this.lblSourceValue.Size = new System.Drawing.Size(71, 13);
            this.lblSourceValue.TabIndex = 105;
            this.lblSourceValue.Text = "Source Value";
            // 
            // cbSourceValue
            // 
            this.cbSourceValue.AutoSize = true;
            this.cbSourceValue.Location = new System.Drawing.Point(83, 71);
            this.cbSourceValue.Name = "cbSourceValue";
            this.cbSourceValue.Size = new System.Drawing.Size(112, 17);
            this.cbSourceValue.TabIndex = 2;
            this.cbSourceValue.Text = "Use Source Value";
            this.cbSourceValue.UseVisualStyleBackColor = true;
            this.cbSourceValue.CheckedChanged += new System.EventHandler(this.cbSourceValue_CheckedChanged);
            // 
            // cbTargetValue
            // 
            this.cbTargetValue.AutoSize = true;
            this.cbTargetValue.Location = new System.Drawing.Point(79, 190);
            this.cbTargetValue.Name = "cbTargetValue";
            this.cbTargetValue.Size = new System.Drawing.Size(109, 17);
            this.cbTargetValue.TabIndex = 6;
            this.cbTargetValue.Text = "Use Target Value";
            this.cbTargetValue.UseVisualStyleBackColor = true;
            this.cbTargetValue.CheckedChanged += new System.EventHandler(this.cbTargetValue_CheckedChanged);
            // 
            // tbTargetValue
            // 
            this.tbTargetValue.Location = new System.Drawing.Point(8, 207);
            this.tbTargetValue.Name = "tbTargetValue";
            this.tbTargetValue.Size = new System.Drawing.Size(405, 20);
            this.tbTargetValue.TabIndex = 7;
            // 
            // lblTargetValue
            // 
            this.lblTargetValue.AutoSize = true;
            this.lblTargetValue.Location = new System.Drawing.Point(5, 191);
            this.lblTargetValue.Name = "lblTargetValue";
            this.lblTargetValue.Size = new System.Drawing.Size(68, 13);
            this.lblTargetValue.TabIndex = 108;
            this.lblTargetValue.Text = "Target Value";
            // 
            // cbEnabled
            // 
            this.cbEnabled.AutoSize = true;
            this.cbEnabled.Checked = true;
            this.cbEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEnabled.Location = new System.Drawing.Point(9, 12);
            this.cbEnabled.Name = "cbEnabled";
            this.cbEnabled.Size = new System.Drawing.Size(65, 17);
            this.cbEnabled.TabIndex = 109;
            this.cbEnabled.Text = "Enabled";
            this.cbEnabled.UseVisualStyleBackColor = true;
            // 
            // cbOperator
            // 
            this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperator.FormattingEnabled = true;
            this.cbOperator.Location = new System.Drawing.Point(8, 127);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Size = new System.Drawing.Size(200, 21);
            this.cbOperator.TabIndex = 110;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 111;
            this.label6.Text = "Operator";
            // 
            // AddEditNetworkMapCriteria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 304);
            this.Controls.Add(this.cbOperator);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbEnabled);
            this.Controls.Add(this.cbTargetValue);
            this.Controls.Add(this.tbTargetValue);
            this.Controls.Add(this.lblTargetValue);
            this.Controls.Add(this.cbSourceValue);
            this.Controls.Add(this.tbSourceValue);
            this.Controls.Add(this.lblSourceValue);
            this.Controls.Add(this.cbTargetName);
            this.Controls.Add(this.cbSourceName);
            this.Controls.Add(this.tbTimeCondition);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbTargetType);
            this.Controls.Add(this.cbSourceType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddEditNetworkMapCriteria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Map Criteria";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSourceType;
        private System.Windows.Forms.ComboBox cbTargetType;
        public System.Windows.Forms.TextBox tbTimeCondition;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbSourceName;
        private System.Windows.Forms.ComboBox cbTargetName;
        public System.Windows.Forms.TextBox tbSourceValue;
        private System.Windows.Forms.Label lblSourceValue;
        private System.Windows.Forms.CheckBox cbSourceValue;
        private System.Windows.Forms.CheckBox cbTargetValue;
        public System.Windows.Forms.TextBox tbTargetValue;
        private System.Windows.Forms.Label lblTargetValue;
        private System.Windows.Forms.CheckBox cbEnabled;
        private System.Windows.Forms.ComboBox cbOperator;
        private System.Windows.Forms.Label label6;

    }
}