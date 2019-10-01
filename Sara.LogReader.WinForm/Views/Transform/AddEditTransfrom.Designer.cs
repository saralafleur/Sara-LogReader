namespace Sara.LogReader.WinForm.Views.HideOptions
{
    partial class AddEditHideOptions
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
            this.lblOption = new System.Windows.Forms.Label();
            this.tbOption = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.clbSourceType = new System.Windows.Forms.CheckedListBox();
            this.btnTestEvent = new System.Windows.Forms.Button();
            this.tbRegularExpression = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbReplaceWith = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblOption
            // 
            this.lblOption.AutoSize = true;
            this.lblOption.Location = new System.Drawing.Point(12, 9);
            this.lblOption.Name = "lblOption";
            this.lblOption.Size = new System.Drawing.Size(54, 13);
            this.lblOption.TabIndex = 0;
            this.lblOption.Text = "Transform";
            // 
            // tbOption
            // 
            this.tbOption.Location = new System.Drawing.Point(15, 25);
            this.tbOption.Name = "tbOption";
            this.tbOption.Size = new System.Drawing.Size(257, 20);
            this.tbOption.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(352, 242);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(433, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(499, 28);
            this.label2.TabIndex = 112;
            this.label2.Text = "Source Type (Used to limit which File Type the EventPattern is applied to.  \"ANY\" will a" +
    "pply to all File Types)";
            // 
            // clbSourceType
            // 
            this.clbSourceType.ColumnWidth = 300;
            this.clbSourceType.FormattingEnabled = true;
            this.clbSourceType.Location = new System.Drawing.Point(15, 157);
            this.clbSourceType.MultiColumn = true;
            this.clbSourceType.Name = "clbSourceType";
            this.clbSourceType.Size = new System.Drawing.Size(496, 79);
            this.clbSourceType.TabIndex = 4;
            // 
            // btnTestEvent
            // 
            this.btnTestEvent.Location = new System.Drawing.Point(436, 62);
            this.btnTestEvent.Name = "btnTestEvent";
            this.btnTestEvent.Size = new System.Drawing.Size(75, 23);
            this.btnTestEvent.TabIndex = 2;
            this.btnTestEvent.Text = "Test";
            this.btnTestEvent.UseVisualStyleBackColor = true;
            this.btnTestEvent.Click += new System.EventHandler(this.btnTestEvent_Click);
            // 
            // tbRegularExpression
            // 
            this.tbRegularExpression.Location = new System.Drawing.Point(15, 64);
            this.tbRegularExpression.Name = "tbRegularExpression";
            this.tbRegularExpression.Size = new System.Drawing.Size(415, 20);
            this.tbRegularExpression.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 115;
            this.label8.Text = "Regular Expression";
            // 
            // tbReplaceWith
            // 
            this.tbReplaceWith.Location = new System.Drawing.Point(15, 103);
            this.tbReplaceWith.Name = "tbReplaceWith";
            this.tbReplaceWith.Size = new System.Drawing.Size(415, 20);
            this.tbReplaceWith.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 117;
            this.label1.Text = "Replace with";
            // 
            // AddEditHideOptions
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(521, 273);
            this.Controls.Add(this.tbReplaceWith);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTestEvent);
            this.Controls.Add(this.tbRegularExpression);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clbSourceType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbOption);
            this.Controls.Add(this.lblOption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEditHideOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Option";
            this.Shown += new System.EventHandler(this.AddEditHideOptions_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOption;
        private System.Windows.Forms.TextBox tbOption;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox clbSourceType;
        private System.Windows.Forms.Button btnTestEvent;
        public System.Windows.Forms.TextBox tbRegularExpression;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox tbReplaceWith;
        private System.Windows.Forms.Label label1;
    }
}