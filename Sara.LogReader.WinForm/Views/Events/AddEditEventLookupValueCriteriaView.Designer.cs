namespace Sara.LogReader.WinForm.Views.Events
{
    partial class AddEditEventLookupValueCriteriaView
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
            this.cbTargetName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSourceName = new System.Windows.Forms.Label();
            this.cbOperator = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbCriteriaType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTargetValue = new System.Windows.Forms.TextBox();
            this.lblTargetValue = new System.Windows.Forms.Label();
            this.cbSourceName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(355, 129);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(274, 129);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbTargetName
            // 
            this.cbTargetName.FormattingEnabled = true;
            this.cbTargetName.Location = new System.Drawing.Point(12, 20);
            this.cbTargetName.Name = "cbTargetName";
            this.cbTargetName.Size = new System.Drawing.Size(418, 21);
            this.cbTargetName.TabIndex = 100;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 99;
            this.label4.Text = "Target Name";
            // 
            // lblSourceName
            // 
            this.lblSourceName.AutoSize = true;
            this.lblSourceName.Location = new System.Drawing.Point(9, 87);
            this.lblSourceName.Name = "lblSourceName";
            this.lblSourceName.Size = new System.Drawing.Size(72, 13);
            this.lblSourceName.TabIndex = 96;
            this.lblSourceName.Text = "Source Name";
            // 
            // cbOperator
            // 
            this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperator.FormattingEnabled = true;
            this.cbOperator.Location = new System.Drawing.Point(12, 63);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Size = new System.Drawing.Size(200, 21);
            this.cbOperator.TabIndex = 112;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 113;
            this.label6.Text = "Operator";
            // 
            // cbCriteriaType
            // 
            this.cbCriteriaType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCriteriaType.FormattingEnabled = true;
            this.cbCriteriaType.Location = new System.Drawing.Point(230, 63);
            this.cbCriteriaType.Name = "cbCriteriaType";
            this.cbCriteriaType.Size = new System.Drawing.Size(200, 21);
            this.cbCriteriaType.TabIndex = 114;
            this.cbCriteriaType.SelectedIndexChanged += new System.EventHandler(this.cbCriteriaType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 115;
            this.label2.Text = "Criteria Type";
            // 
            // tbTargetValue
            // 
            this.tbTargetValue.Location = new System.Drawing.Point(230, 103);
            this.tbTargetValue.Name = "tbTargetValue";
            this.tbTargetValue.Size = new System.Drawing.Size(200, 20);
            this.tbTargetValue.TabIndex = 116;
            // 
            // lblTargetValue
            // 
            this.lblTargetValue.AutoSize = true;
            this.lblTargetValue.Location = new System.Drawing.Point(227, 87);
            this.lblTargetValue.Name = "lblTargetValue";
            this.lblTargetValue.Size = new System.Drawing.Size(68, 13);
            this.lblTargetValue.TabIndex = 117;
            this.lblTargetValue.Text = "Target Value";
            // 
            // cbSourceName
            // 
            this.cbSourceName.FormattingEnabled = true;
            this.cbSourceName.Location = new System.Drawing.Point(12, 103);
            this.cbSourceName.Name = "cbSourceName";
            this.cbSourceName.Size = new System.Drawing.Size(200, 21);
            this.cbSourceName.TabIndex = 118;
            // 
            // AddEditEventLookupValueCriteriaView
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(442, 166);
            this.Controls.Add(this.cbSourceName);
            this.Controls.Add(this.tbTargetValue);
            this.Controls.Add(this.lblTargetValue);
            this.Controls.Add(this.cbCriteriaType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbOperator);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbTargetName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblSourceName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddEditEventLookupValueCriteriaView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EventPattern Lookup Criteria";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbTargetName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSourceName;
        private System.Windows.Forms.ComboBox cbOperator;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbCriteriaType;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox tbTargetValue;
        private System.Windows.Forms.Label lblTargetValue;
        private System.Windows.Forms.ComboBox cbSourceName;
    }
}