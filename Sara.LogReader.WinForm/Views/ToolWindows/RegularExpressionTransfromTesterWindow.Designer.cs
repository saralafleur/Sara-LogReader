using Sara.LogReader.WinForm.Controls;

namespace Sara.LogReader.WinForm.Views.ToolWindows
{
    partial class RegularExpressionTransfromTesterWindow
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
            this.tbReplaceWith = new System.Windows.Forms.TextBox();
            this.lblReplaceWith = new System.Windows.Forms.Label();
            this.ckbFileInfo = new System.Windows.Forms.CheckBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.tbRegularExpression = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fctbDocument = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tbCurrentLine = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fctbResult = new FastColoredTextBoxNS.FastColoredTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fctbDocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fctbResult)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbReplaceWith);
            this.panel2.Controls.Add(this.lblReplaceWith);
            this.panel2.Controls.Add(this.ckbFileInfo);
            this.panel2.Controls.Add(this.btnTest);
            this.panel2.Controls.Add(this.tbRegularExpression);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(900, 118);
            this.panel2.TabIndex = 1;
            // 
            // tbReplaceWith
            // 
            this.tbReplaceWith.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReplaceWith.Location = new System.Drawing.Point(15, 64);
            this.tbReplaceWith.Name = "tbReplaceWith";
            this.tbReplaceWith.Size = new System.Drawing.Size(869, 20);
            this.tbReplaceWith.TabIndex = 4;
            // 
            // lblReplaceWith
            // 
            this.lblReplaceWith.AutoSize = true;
            this.lblReplaceWith.Location = new System.Drawing.Point(12, 48);
            this.lblReplaceWith.Name = "lblReplaceWith";
            this.lblReplaceWith.Size = new System.Drawing.Size(69, 13);
            this.lblReplaceWith.TabIndex = 5;
            this.lblReplaceWith.Text = "Replace with";
            // 
            // ckbFileInfo
            // 
            this.ckbFileInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbFileInfo.AutoSize = true;
            this.ckbFileInfo.Checked = true;
            this.ckbFileInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbFileInfo.Location = new System.Drawing.Point(751, 94);
            this.ckbFileInfo.Name = "ckbFileInfo";
            this.ckbFileInfo.Size = new System.Drawing.Size(65, 17);
            this.ckbFileInfo.TabIndex = 3;
            this.ckbFileInfo.Text = "All Lines";
            this.ckbFileInfo.UseVisualStyleBackColor = true;
            this.ckbFileInfo.CheckedChanged += new System.EventHandler(this.ckbCurrentLine_CheckedChanged);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(822, 90);
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
            this.tbRegularExpression.Name = "tbRegularExpression";
            this.tbRegularExpression.Size = new System.Drawing.Size(869, 20);
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 118);
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
            this.splitContainer1.Panel2.Controls.Add(this.fctbResult);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(900, 364);
            this.splitContainer1.SplitterDistance = 185;
            this.splitContainer1.TabIndex = 11;
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
            this.fctbDocument.IsReplaceMode = false;
            this.fctbDocument.Location = new System.Drawing.Point(0, 40);
            this.fctbDocument.Name = "fctbDocument";
            this.fctbDocument.Paddings = new System.Windows.Forms.Padding(0);
            this.fctbDocument.ReadOnly = true;
            this.fctbDocument.SelectionChangedDelayedEnabled = false;
            this.fctbDocument.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctbDocument.Size = new System.Drawing.Size(900, 145);
            this.fctbDocument.TabIndex = 11;
            this.fctbDocument.Text = "fastColoredTextBox1";
            this.fctbDocument.Zoom = 100;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Test Value";
            // 
            // fctbResult
            // 
            this.fctbResult.AutoCompleteBracketsList = new char[] {
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
            this.fctbResult.AutoScrollMinSize = new System.Drawing.Size(107, 14);
            this.fctbResult.BackBrush = null;
            this.fctbResult.CharHeight = 14;
            this.fctbResult.CharWidth = 8;
            this.fctbResult.CurrentPenSize = 3;
            this.fctbResult.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctbResult.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fctbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fctbResult.DocumentPath = null;
            this.fctbResult.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.fctbResult.IsReplaceMode = false;
            this.fctbResult.Location = new System.Drawing.Point(0, 17);
            this.fctbResult.Name = "fctbResult";
            this.fctbResult.Paddings = new System.Windows.Forms.Padding(0);
            this.fctbResult.ReadOnly = true;
            this.fctbResult.SelectionChangedDelayedEnabled = false;
            this.fctbResult.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctbResult.Size = new System.Drawing.Size(900, 128);
            this.fctbResult.TabIndex = 16;
            this.fctbResult.Text = "fctbResult";
            this.fctbResult.Zoom = 100;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(900, 17);
            this.panel3.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Result";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAccept);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 145);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 30);
            this.panel1.TabIndex = 14;
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.Location = new System.Drawing.Point(741, 4);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 11;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(822, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // RegularExpressionTransfromTesterWindow
            // 
            this.AcceptButton = this.btnTest;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(900, 482);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Name = "RegularExpressionTransfromTesterWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Regular Expression Hide Tester";
            this.Load += new System.EventHandler(this.RegularExpressionTesterWindow_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fctbDocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fctbResult)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbRegularExpression;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.CheckBox ckbFileInfo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private FastColoredTextBoxNS.FastColoredTextBox fctbDocument;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbCurrentLine;
        private FastColoredTextBoxNS.FastColoredTextBox fctbResult;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbReplaceWith;
        private System.Windows.Forms.Label lblReplaceWith;
    }
}