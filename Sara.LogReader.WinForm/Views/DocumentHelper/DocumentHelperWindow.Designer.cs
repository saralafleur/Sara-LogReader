

using Sara.WinForm.ControlsNS;

namespace Sara.LogReader.WinForm.Views.DocumentHelper
{
    partial class DocumentHelperWindow
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
            this.warningPanel = new WarningPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbEvents = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblNetoworkMethod = new System.Windows.Forms.Label();
            this.lblMethod = new System.Windows.Forms.Label();
            this.lblClass = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.ckbOnlyNetwork = new System.Windows.Forms.CheckBox();
            this.ckbHideDocumented = new System.Windows.Forms.CheckBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrior = new System.Windows.Forms.Button();
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // warningPanel
            // 
            this.warningPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.warningPanel.Location = new System.Drawing.Point(0, 0);
            this.warningPanel.Name = "warningPanel";
            this.warningPanel.Size = new System.Drawing.Size(723, 150);
            this.warningPanel.TabIndex = 19;
            this.warningPanel.WarningText = "Feature Not Ready...";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 150);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(723, 164);
            this.panel2.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(723, 164);
            this.panel1.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbEvents);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 66);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(723, 98);
            this.panel4.TabIndex = 6;
            // 
            // lbEvents
            // 
            this.lbEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEvents.FormattingEnabled = true;
            this.lbEvents.Location = new System.Drawing.Point(0, 0);
            this.lbEvents.Name = "lbEvents";
            this.lbEvents.Size = new System.Drawing.Size(723, 98);
            this.lbEvents.TabIndex = 4;
            this.lbEvents.SelectedIndexChanged += new System.EventHandler(this.lbEvents_SelectedIndexChanged);
            this.lbEvents.Enter += new System.EventHandler(this.lbEvents_Enter);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.lblNetoworkMethod);
            this.panel3.Controls.Add(this.lblMethod);
            this.panel3.Controls.Add(this.lblClass);
            this.panel3.Controls.Add(this.lblTotal);
            this.panel3.Controls.Add(this.ckbOnlyNetwork);
            this.panel3.Controls.Add(this.ckbHideDocumented);
            this.panel3.Controls.Add(this.btnNext);
            this.panel3.Controls.Add(this.btnPrior);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(723, 66);
            this.panel3.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Black;
            this.panel5.Location = new System.Drawing.Point(301, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1, 57);
            this.panel5.TabIndex = 12;
            // 
            // lblNetoworkMethod
            // 
            this.lblNetoworkMethod.AutoSize = true;
            this.lblNetoworkMethod.Location = new System.Drawing.Point(311, 43);
            this.lblNetoworkMethod.Name = "lblNetoworkMethod";
            this.lblNetoworkMethod.Size = new System.Drawing.Size(127, 13);
            this.lblNetoworkMethod.TabIndex = 11;
            this.lblNetoworkMethod.Text = "Network Method: xxxxxxx";
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(311, 26);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(99, 13);
            this.lblMethod.TabIndex = 10;
            this.lblMethod.Text = "Method: xxxxxxxxxx";
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Location = new System.Drawing.Point(311, 9);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(78, 13);
            this.lblClass.TabIndex = 9;
            this.lblClass.Text = "Class: xxxxxxxx";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(132, 13);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(58, 13);
            this.lblTotal.TabIndex = 8;
            this.lblTotal.Text = "Total: XXX";
            // 
            // ckbOnlyNetwork
            // 
            this.ckbOnlyNetwork.AutoSize = true;
            this.ckbOnlyNetwork.Location = new System.Drawing.Point(13, 36);
            this.ckbOnlyNetwork.Name = "ckbOnlyNetwork";
            this.ckbOnlyNetwork.Size = new System.Drawing.Size(90, 17);
            this.ckbOnlyNetwork.TabIndex = 7;
            this.ckbOnlyNetwork.Text = "Only Network";
            this.ckbOnlyNetwork.UseVisualStyleBackColor = true;
            this.ckbOnlyNetwork.Click += new System.EventHandler(this.ckbOnlyNetwork_CheckedChanged);
            // 
            // ckbHideDocumented
            // 
            this.ckbHideDocumented.AutoSize = true;
            this.ckbHideDocumented.Location = new System.Drawing.Point(13, 13);
            this.ckbHideDocumented.Name = "ckbHideDocumented";
            this.ckbHideDocumented.Size = new System.Drawing.Size(112, 17);
            this.ckbHideDocumented.TabIndex = 6;
            this.ckbHideDocumented.Text = "Hide Documented";
            this.ckbHideDocumented.UseVisualStyleBackColor = true;
            this.ckbHideDocumented.Click += new System.EventHandler(this.ckbHideDocumented_CheckedChanged);
            // 
            // btnNext
            // 
            this.btnNext.Enabled = false;
            this.btnNext.Location = new System.Drawing.Point(212, 35);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnPrior
            // 
            this.btnPrior.Enabled = false;
            this.btnPrior.Location = new System.Drawing.Point(131, 35);
            this.btnPrior.Name = "btnPrior";
            this.btnPrior.Size = new System.Drawing.Size(75, 23);
            this.btnPrior.TabIndex = 4;
            this.btnPrior.Text = "Prior";
            this.btnPrior.UseVisualStyleBackColor = true;
            // 
            // StatusPanel
            // 
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = false;
            this.StatusPanel.Location = new System.Drawing.Point(166, -18);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(396, 89);
            this.StatusPanel.TabIndex = 23;
            this.StatusPanel.Visible = false;
            // 
            // DocumentHelperWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 314);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.warningPanel);
            this.HideOnClose = true;
            this.Name = "DocumentHelperWindow";
            this.TabText = "Document Helper";
            this.Text = "Document Helper";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Sara.WinForm.ControlsNS.WarningPanel warningPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListBox lbEvents;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.CheckBox ckbOnlyNetwork;
        private System.Windows.Forms.CheckBox ckbHideDocumented;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrior;
        private System.Windows.Forms.Label lblNetoworkMethod;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.Panel panel5;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;



    }
}