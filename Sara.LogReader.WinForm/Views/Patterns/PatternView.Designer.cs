

namespace Sara.LogReader.WinForm.Views.Patterns
{
    partial class PatternView
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
            this.scBody = new System.Windows.Forms.SplitContainer();
            this.lbPatterns = new System.Windows.Forms.ListBox();
            this.IDE = new Sara.LogReader.WinForm.Controls.MonitorScriptIDE();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            ((System.ComponentModel.ISupportInitialize)(this.scBody)).BeginInit();
            this.scBody.Panel1.SuspendLayout();
            this.scBody.Panel2.SuspendLayout();
            this.scBody.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // scBody
            // 
            this.scBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scBody.Location = new System.Drawing.Point(0, 43);
            this.scBody.Name = "scBody";
            this.scBody.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scBody.Panel1
            // 
            this.scBody.Panel1.Controls.Add(this.lbPatterns);
            // 
            // scBody.Panel2
            // 
            this.scBody.Panel2.Controls.Add(this.IDE);
            this.scBody.Size = new System.Drawing.Size(1009, 408);
            this.scBody.SplitterDistance = 79;
            this.scBody.TabIndex = 3;
            // 
            // lbPatterns
            // 
            this.lbPatterns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lbPatterns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPatterns.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(201)))), ((int)(((byte)(178)))));
            this.lbPatterns.FormattingEnabled = true;
            this.lbPatterns.Location = new System.Drawing.Point(0, 0);
            this.lbPatterns.Name = "lbPatterns";
            this.lbPatterns.Size = new System.Drawing.Size(1009, 79);
            this.lbPatterns.TabIndex = 2;
            this.lbPatterns.SelectedIndexChanged += new System.EventHandler(this.lbPatterns_SelectedIndexChanged);
            this.lbPatterns.Enter += new System.EventHandler(this.lbPatterns_Enter);
            // 
            // IDE
            // 
            this.IDE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.IDE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IDE.Font = new System.Drawing.Font("Consolas", 10F);
            this.IDE.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.IDE.Location = new System.Drawing.Point(0, 0);
            this.IDE.Name = "IDE";
            this.IDE.OnFullScreen = null;
            this.IDE.OnSave = null;
            this.IDE.Pattern = null;
            this.IDE.SaveEnabled = false;
            this.IDE.Script = "";
            this.IDE.Size = new System.Drawing.Size(1009, 325);
            this.IDE.TabIndex = 0;
            this.IDE.ScriptChanged += new System.EventHandler(this.tbScript_TextChanged);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnDelete);
            this.pnlTop.Controls.Add(this.btnAdd);
            this.pnlTop.Controls.Add(this.btnEdit);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1009, 43);
            this.pnlTop.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(174, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Red;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(12, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(93, 12);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.scBody);
            this.panel3.Controls.Add(this.pnlTop);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1009, 451);
            this.panel3.TabIndex = 23;
            // 
            // StatusPanel
            // 
            this.StatusPanel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.StatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusPanel.Location = new System.Drawing.Point(337, 178);
            this.StatusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Padding = new System.Windows.Forms.Padding(1);
            this.StatusPanel.Size = new System.Drawing.Size(335, 95);
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = false;
            this.StatusPanel.SP_Enabled = true;
            this.StatusPanel.SP_FullScreen = true;
            this.StatusPanel.TabIndex = 25;
            this.StatusPanel.Visible = false;
            // 
            // PatternView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1009, 451);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.panel3);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(201)))), ((int)(((byte)(178)))));
            this.HideOnClose = true;
            this.MinimumSize = new System.Drawing.Size(16, 200);
            this.Name = "PatternView";
            this.TabText = "Patterns";
            this.Text = "Patterns";
            this.Load += new System.EventHandler(this.PatternView_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PatternView_Paint);
            this.scBody.Panel1.ResumeLayout(false);
            this.scBody.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scBody)).EndInit();
            this.scBody.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.SplitContainer scBody;
        private System.Windows.Forms.ListBox lbPatterns;
        private System.Windows.Forms.Panel panel3;
        private Controls.MonitorScriptIDE IDE;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;
    }
}