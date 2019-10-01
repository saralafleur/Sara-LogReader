
using Sara.LogReader.WinForm.Controls;

namespace Sara.LogReader.WinForm.Views.ToolWindows
{
    partial class RegularExpressionTesterWindow
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.ckbDistinct = new System.Windows.Forms.CheckBox();
            this.ckbFileInfo = new System.Windows.Forms.CheckBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.tbRegularExpression = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.fctbDocument = new FastColoredTextBoxNS.FastColoredTextBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblTotalMatches = new System.Windows.Forms.Label();
            this.tvResult = new Sara.WinForm.ControlsNS.BufferedTreeView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbCurrentLine = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fctbDocument)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ckbDistinct);
            this.panel2.Controls.Add(this.ckbFileInfo);
            this.panel2.Controls.Add(this.btnTest);
            this.panel2.Controls.Add(this.tbRegularExpression);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(900, 109);
            this.panel2.TabIndex = 1;
            // 
            // ckbDistinct
            // 
            this.ckbDistinct.AutoSize = true;
            this.ckbDistinct.Location = new System.Drawing.Point(96, 82);
            this.ckbDistinct.Name = "ckbDistinct";
            this.ckbDistinct.Size = new System.Drawing.Size(61, 17);
            this.ckbDistinct.TabIndex = 2;
            this.ckbDistinct.Text = "Distinct";
            this.ckbDistinct.UseVisualStyleBackColor = true;
            // 
            // ckbFileInfo
            // 
            this.ckbFileInfo.AutoSize = true;
            this.ckbFileInfo.Checked = true;
            this.ckbFileInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbFileInfo.Location = new System.Drawing.Point(163, 82);
            this.ckbFileInfo.Name = "ckbFileInfo";
            this.ckbFileInfo.Size = new System.Drawing.Size(434, 17);
            this.ckbFileInfo.TabIndex = 3;
            this.ckbFileInfo.Text = "File Info (Searches the entire document and includes the results in the File Info" +
    " section)";
            this.ckbFileInfo.UseVisualStyleBackColor = true;
            this.ckbFileInfo.CheckedChanged += new System.EventHandler(this.ckbCurrentLine_CheckedChanged);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(15, 78);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // tbRegularExpression
            // 
            this.tbRegularExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRegularExpression.Location = new System.Drawing.Point(15, 25);
            this.tbRegularExpression.Multiline = true;
            this.tbRegularExpression.Name = "tbRegularExpression";
            this.tbRegularExpression.Size = new System.Drawing.Size(869, 47);
            this.tbRegularExpression.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Regular Expression";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 109);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fctbDocument);
            this.splitContainer1.Panel1.Controls.Add(this.tbCurrentLine);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblTime);
            this.splitContainer1.Panel2.Controls.Add(this.lblTotalMatches);
            this.splitContainer1.Panel2.Controls.Add(this.tvResult);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel2.Controls.Add(this.btnAccept);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Size = new System.Drawing.Size(900, 373);
            this.splitContainer1.SplitterDistance = 186;
            this.splitContainer1.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Test Value";
            // 
            // fctbDocument
            // 
            this.fctbDocument.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fctbDocument.AutoScrollMinSize = new System.Drawing.Size(179, 14);
            this.fctbDocument.BackBrush = null;
            this.fctbDocument.CharHeight = 14;
            this.fctbDocument.CharWidth = 8;
            this.fctbDocument.CurrentPenSize = 3;
            this.fctbDocument.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctbDocument.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fctbDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fctbDocument.DocumentPath = null;
            this.fctbDocument.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.fctbDocument.IsReplaceMode = false;
            this.fctbDocument.Location = new System.Drawing.Point(0, 40);
            this.fctbDocument.Name = "fctbDocument";
            this.fctbDocument.Paddings = new System.Windows.Forms.Padding(0);
            this.fctbDocument.ReadOnly = true;
            this.fctbDocument.SelectionChangedDelayedEnabled = false;
            this.fctbDocument.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctbDocument.Size = new System.Drawing.Size(900, 146);
            this.fctbDocument.TabIndex = 11;
            this.fctbDocument.Text = "fastColoredTextBox1";
            this.fctbDocument.Zoom = 100;
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(129, 150);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(28, 13);
            this.lblTime.TabIndex = 15;
            this.lblTime.Text = "###";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTotalMatches
            // 
            this.lblTotalMatches.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalMatches.AutoSize = true;
            this.lblTotalMatches.Location = new System.Drawing.Point(14, 150);
            this.lblTotalMatches.Name = "lblTotalMatches";
            this.lblTotalMatches.Size = new System.Drawing.Size(28, 13);
            this.lblTotalMatches.TabIndex = 14;
            this.lblTotalMatches.Text = "###";
            // 
            // tvResult
            // 
            this.tvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvResult.Location = new System.Drawing.Point(17, 21);
            this.tvResult.Name = "tvResult";
            this.tvResult.Size = new System.Drawing.Size(867, 118);
            this.tvResult.TabIndex = 10;
            this.tvResult.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvResult_AfterSelect);
            this.tvResult.DoubleClick += new System.EventHandler(this.tvResult_DoubleClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(809, 145);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.Location = new System.Drawing.Point(728, 145);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 11;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Result";
            // 
            // tbCurrentLine
            // 
            this.tbCurrentLine.BackColor = System.Drawing.Color.Beige;
            this.tbCurrentLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbCurrentLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCurrentLine.Location = new System.Drawing.Point(0, 0);
            this.tbCurrentLine.Multiline = true;
            this.tbCurrentLine.Name = "tbCurrentLine";
            this.tbCurrentLine.Size = new System.Drawing.Size(900, 40);
            this.tbCurrentLine.TabIndex = 13;
            // 
            // RegularExpressionTesterWindow
            // 
            this.AcceptButton = this.btnTest;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(900, 482);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Name = "RegularExpressionTesterWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Regular Expression Tester";
            this.Load += new System.EventHandler(this.RegularExpressionTesterWindow_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fctbDocument)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbRegularExpression;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.CheckBox ckbFileInfo;
        private System.Windows.Forms.CheckBox ckbDistinct;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private FastColoredTextBoxNS.FastColoredTextBox fctbDocument;
        private System.Windows.Forms.Label lblTotalMatches;
        private Sara.WinForm.ControlsNS.BufferedTreeView tvResult;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.TextBox tbCurrentLine;
    }
}