using Sara.LogReader.WinForm.ViewModel;
using Sara.LogReader.Model.EventNS;
using Sara.WinForm.MVVM;

namespace Sara.LogReader.WinForm.Views.Events
{
    partial class AddEditEventLookupValueView : IView<EventLookupValueModel, object>
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

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
            this.ckbUseCategory = new System.Windows.Forms.CheckBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.tbCategory = new System.Windows.Forms.TextBox();
            this.btnDeleteCriteria = new System.Windows.Forms.Button();
            this.btnEditCriteria = new System.Windows.Forms.Button();
            this.btnAddCriteria = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbCriteria = new System.Windows.Forms.ListBox();
            this.btnDeleteValueName = new System.Windows.Forms.Button();
            this.btnEditValueName = new System.Windows.Forms.Button();
            this.btnAddValueName = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbValueNames = new System.Windows.Forms.ListBox();
            this.ckbOnlyNetwork = new System.Windows.Forms.CheckBox();
            this.btnDeleteCondition = new System.Windows.Forms.Button();
            this.btnEditCondition = new System.Windows.Forms.Button();
            this.btnAddCondition = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbConditions = new System.Windows.Forms.ListBox();
            this.cbLookupDirection = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(710, 203);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(629, 203);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ckbUseCategory
            // 
            this.ckbUseCategory.AutoSize = true;
            this.ckbUseCategory.Location = new System.Drawing.Point(158, 3);
            this.ckbUseCategory.Name = "ckbUseCategory";
            this.ckbUseCategory.Size = new System.Drawing.Size(90, 17);
            this.ckbUseCategory.TabIndex = 6;
            this.ckbUseCategory.Text = "Use Category";
            this.ckbUseCategory.UseVisualStyleBackColor = true;
            this.ckbUseCategory.CheckedChanged += new System.EventHandler(this.ckbUseCategory_CheckedChanged);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(6, 7);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(49, 13);
            this.lblCategory.TabIndex = 7;
            this.lblCategory.Text = "Category";
            // 
            // tbCategory
            // 
            this.tbCategory.Location = new System.Drawing.Point(7, 23);
            this.tbCategory.Name = "tbCategory";
            this.tbCategory.Size = new System.Drawing.Size(245, 20);
            this.tbCategory.TabIndex = 8;
            // 
            // btnDeleteCriteria
            // 
            this.btnDeleteCriteria.Location = new System.Drawing.Point(469, 56);
            this.btnDeleteCriteria.Name = "btnDeleteCriteria";
            this.btnDeleteCriteria.Size = new System.Drawing.Size(52, 23);
            this.btnDeleteCriteria.TabIndex = 113;
            this.btnDeleteCriteria.Text = "Delete";
            this.btnDeleteCriteria.UseVisualStyleBackColor = true;
            this.btnDeleteCriteria.Click += new System.EventHandler(this.btnDeleteCriteria_Click);
            // 
            // btnEditCriteria
            // 
            this.btnEditCriteria.Location = new System.Drawing.Point(411, 56);
            this.btnEditCriteria.Name = "btnEditCriteria";
            this.btnEditCriteria.Size = new System.Drawing.Size(52, 23);
            this.btnEditCriteria.TabIndex = 112;
            this.btnEditCriteria.Text = "Edit";
            this.btnEditCriteria.UseVisualStyleBackColor = true;
            this.btnEditCriteria.Click += new System.EventHandler(this.btnEditCriteria_Click);
            // 
            // btnAddCriteria
            // 
            this.btnAddCriteria.Location = new System.Drawing.Point(353, 56);
            this.btnAddCriteria.Name = "btnAddCriteria";
            this.btnAddCriteria.Size = new System.Drawing.Size(52, 23);
            this.btnAddCriteria.TabIndex = 111;
            this.btnAddCriteria.Text = "Add";
            this.btnAddCriteria.UseVisualStyleBackColor = true;
            this.btnAddCriteria.Click += new System.EventHandler(this.btnAddCriteria_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(275, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 110;
            this.label1.Text = "Criteria";
            // 
            // lbCriteria
            // 
            this.lbCriteria.FormattingEnabled = true;
            this.lbCriteria.Location = new System.Drawing.Point(276, 85);
            this.lbCriteria.Name = "lbCriteria";
            this.lbCriteria.Size = new System.Drawing.Size(245, 108);
            this.lbCriteria.TabIndex = 109;
            // 
            // btnDeleteValueName
            // 
            this.btnDeleteValueName.Location = new System.Drawing.Point(733, 56);
            this.btnDeleteValueName.Name = "btnDeleteValueName";
            this.btnDeleteValueName.Size = new System.Drawing.Size(52, 23);
            this.btnDeleteValueName.TabIndex = 118;
            this.btnDeleteValueName.Text = "Delete";
            this.btnDeleteValueName.UseVisualStyleBackColor = true;
            this.btnDeleteValueName.Click += new System.EventHandler(this.btnDeleteValueName_Click);
            // 
            // btnEditValueName
            // 
            this.btnEditValueName.Location = new System.Drawing.Point(675, 56);
            this.btnEditValueName.Name = "btnEditValueName";
            this.btnEditValueName.Size = new System.Drawing.Size(52, 23);
            this.btnEditValueName.TabIndex = 117;
            this.btnEditValueName.Text = "Edit";
            this.btnEditValueName.UseVisualStyleBackColor = true;
            this.btnEditValueName.Click += new System.EventHandler(this.btnEditValueName_Click);
            // 
            // btnAddValueName
            // 
            this.btnAddValueName.Location = new System.Drawing.Point(617, 56);
            this.btnAddValueName.Name = "btnAddValueName";
            this.btnAddValueName.Size = new System.Drawing.Size(52, 23);
            this.btnAddValueName.TabIndex = 116;
            this.btnAddValueName.Text = "Add";
            this.btnAddValueName.UseVisualStyleBackColor = true;
            this.btnAddValueName.Click += new System.EventHandler(this.btnAddValueName_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(539, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 115;
            this.label2.Text = "Value Names";
            // 
            // lbValueNames
            // 
            this.lbValueNames.FormattingEnabled = true;
            this.lbValueNames.Location = new System.Drawing.Point(540, 85);
            this.lbValueNames.Name = "lbValueNames";
            this.lbValueNames.Size = new System.Drawing.Size(245, 108);
            this.lbValueNames.TabIndex = 114;
            // 
            // ckbOnlyNetwork
            // 
            this.ckbOnlyNetwork.AutoSize = true;
            this.ckbOnlyNetwork.Location = new System.Drawing.Point(276, 3);
            this.ckbOnlyNetwork.Name = "ckbOnlyNetwork";
            this.ckbOnlyNetwork.Size = new System.Drawing.Size(178, 17);
            this.ckbOnlyNetwork.TabIndex = 119;
            this.ckbOnlyNetwork.Text = "Use Only Network Cached Data";
            this.ckbOnlyNetwork.UseVisualStyleBackColor = true;
            // 
            // btnDeleteCondition
            // 
            this.btnDeleteCondition.Location = new System.Drawing.Point(205, 56);
            this.btnDeleteCondition.Name = "btnDeleteCondition";
            this.btnDeleteCondition.Size = new System.Drawing.Size(52, 23);
            this.btnDeleteCondition.TabIndex = 124;
            this.btnDeleteCondition.Text = "Delete";
            this.btnDeleteCondition.UseVisualStyleBackColor = true;
            this.btnDeleteCondition.Click += new System.EventHandler(this.btnDeleteCondition_Click);
            // 
            // btnEditCondition
            // 
            this.btnEditCondition.Location = new System.Drawing.Point(147, 56);
            this.btnEditCondition.Name = "btnEditCondition";
            this.btnEditCondition.Size = new System.Drawing.Size(52, 23);
            this.btnEditCondition.TabIndex = 123;
            this.btnEditCondition.Text = "Edit";
            this.btnEditCondition.UseVisualStyleBackColor = true;
            this.btnEditCondition.Click += new System.EventHandler(this.btnEditCondition_Click);
            // 
            // btnAddCondition
            // 
            this.btnAddCondition.Location = new System.Drawing.Point(89, 56);
            this.btnAddCondition.Name = "btnAddCondition";
            this.btnAddCondition.Size = new System.Drawing.Size(52, 23);
            this.btnAddCondition.TabIndex = 122;
            this.btnAddCondition.Text = "Add";
            this.btnAddCondition.UseVisualStyleBackColor = true;
            this.btnAddCondition.Click += new System.EventHandler(this.btnAddCondition_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 121;
            this.label3.Text = "Condition";
            // 
            // lbConditions
            // 
            this.lbConditions.FormattingEnabled = true;
            this.lbConditions.Location = new System.Drawing.Point(12, 85);
            this.lbConditions.Name = "lbConditions";
            this.lbConditions.Size = new System.Drawing.Size(245, 108);
            this.lbConditions.TabIndex = 120;
            // 
            // cbLookupDirection
            // 
            this.cbLookupDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLookupDirection.FormattingEnabled = true;
            this.cbLookupDirection.Location = new System.Drawing.Point(469, 22);
            this.cbLookupDirection.Name = "cbLookupDirection";
            this.cbLookupDirection.Size = new System.Drawing.Size(140, 21);
            this.cbLookupDirection.TabIndex = 126;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(466, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 125;
            this.label4.Text = "Lookup Direction";
            // 
            // AddEditEventLookupValueView
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(801, 240);
            this.Controls.Add(this.cbLookupDirection);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnDeleteCondition);
            this.Controls.Add(this.btnEditCondition);
            this.Controls.Add(this.btnAddCondition);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbConditions);
            this.Controls.Add(this.ckbOnlyNetwork);
            this.Controls.Add(this.btnDeleteValueName);
            this.Controls.Add(this.btnEditValueName);
            this.Controls.Add(this.btnAddValueName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbValueNames);
            this.Controls.Add(this.btnDeleteCriteria);
            this.Controls.Add(this.btnEditCriteria);
            this.Controls.Add(this.btnAddCriteria);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbCriteria);
            this.Controls.Add(this.tbCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.ckbUseCategory);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddEditEventLookupValueView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EventPattern Lookup Value";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox ckbUseCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox tbCategory;
        private System.Windows.Forms.Button btnDeleteCriteria;
        private System.Windows.Forms.Button btnEditCriteria;
        private System.Windows.Forms.Button btnAddCriteria;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbCriteria;
        private System.Windows.Forms.Button btnDeleteValueName;
        private System.Windows.Forms.Button btnEditValueName;
        private System.Windows.Forms.Button btnAddValueName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbValueNames;
        private System.Windows.Forms.CheckBox ckbOnlyNetwork;
        private System.Windows.Forms.Button btnDeleteCondition;
        private System.Windows.Forms.Button btnEditCondition;
        private System.Windows.Forms.Button btnAddCondition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbConditions;
        private System.Windows.Forms.ComboBox cbLookupDirection;
        private System.Windows.Forms.Label label4;
        public void UpdateView(object selectedModel)
        {
            throw new System.NotImplementedException();
        }
    }
}