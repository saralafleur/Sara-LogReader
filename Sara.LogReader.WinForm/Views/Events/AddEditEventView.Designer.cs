

namespace Sara.LogReader.WinForm.Views.Events
{
    partial class AddEditEventView
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.clbSourceType = new System.Windows.Forms.CheckedListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbSort = new System.Windows.Forms.TextBox();
            this.cbFoldingEventId = new System.Windows.Forms.ComboBox();
            this.lblSort = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ckbDurationFromSibling = new System.Windows.Forms.CheckBox();
            this.clbCategory = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbRegularExpression = new System.Windows.Forms.TextBox();
            this.cbDocumentMapHighlightColor = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCopyExample = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.ckbGapNormal = new System.Windows.Forms.CheckBox();
            this.tbExample = new System.Windows.Forms.TextBox();
            this.btnTestEvent = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSourceType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.ckbDocumentMap = new System.Windows.Forms.CheckBox();
            this.ckbIgnoreName = new System.Windows.Forms.CheckBox();
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ckbTimeFromParent = new System.Windows.Forms.CheckBox();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.cbNetworkCommunication = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnEditValue = new System.Windows.Forms.Button();
            this.lbValues = new System.Windows.Forms.ListBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnAddValue = new System.Windows.Forms.Button();
            this.btnDeleteValue = new System.Windows.Forms.Button();
            this.lbLookupValues = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddLookupValue = new System.Windows.Forms.Button();
            this.btnEditLookupValue = new System.Windows.Forms.Button();
            this.btnDeleteLookupValue = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbDocumentation = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbIgnoreDocumentation = new System.Windows.Forms.CheckBox();
            this.StatusPanel = new Sara.WinForm.ControlsNS.StatusPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.StatusPanel);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(521, 462);
            this.panel1.TabIndex = 60;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(521, 433);
            this.tabControl1.TabIndex = 120;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.clbSourceType);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.tbSort);
            this.tabPage1.Controls.Add(this.cbFoldingEventId);
            this.tabPage1.Controls.Add(this.lblSort);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.ckbDurationFromSibling);
            this.tabPage1.Controls.Add(this.clbCategory);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.tbRegularExpression);
            this.tabPage1.Controls.Add(this.cbDocumentMapHighlightColor);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.btnCopyExample);
            this.tabPage1.Controls.Add(this.tbName);
            this.tabPage1.Controls.Add(this.btnCapture);
            this.tabPage1.Controls.Add(this.ckbGapNormal);
            this.tabPage1.Controls.Add(this.tbExample);
            this.tabPage1.Controls.Add(this.btnTestEvent);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.cbSourceType);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.ckbDocumentMap);
            this.tabPage1.Controls.Add(this.ckbIgnoreName);
            this.tabPage1.Controls.Add(this.cbLevel);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.ckbTimeFromParent);
            this.tabPage1.Controls.Add(this.cbColor);
            this.tabPage1.Controls.Add(this.cbNetworkCommunication);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(513, 404);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // clbSourceType
            // 
            this.clbSourceType.FormattingEnabled = true;
            this.clbSourceType.Location = new System.Drawing.Point(191, 198);
            this.clbSourceType.MultiColumn = true;
            this.clbSourceType.Name = "clbSourceType";
            this.clbSourceType.Size = new System.Drawing.Size(315, 124);
            this.clbSourceType.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 78;
            this.label8.Text = "Regular Expression";
            // 
            // tbSort
            // 
            this.tbSort.Location = new System.Drawing.Point(238, 58);
            this.tbSort.Name = "tbSort";
            this.tbSort.Size = new System.Drawing.Size(97, 20);
            this.tbSort.TabIndex = 118;
            // 
            // cbFoldingEventId
            // 
            this.cbFoldingEventId.FormattingEnabled = true;
            this.cbFoldingEventId.Location = new System.Drawing.Point(9, 341);
            this.cbFoldingEventId.Name = "cbFoldingEventId";
            this.cbFoldingEventId.Size = new System.Drawing.Size(246, 21);
            this.cbFoldingEventId.TabIndex = 12;
            // 
            // lblSort
            // 
            this.lblSort.AutoSize = true;
            this.lblSort.Location = new System.Drawing.Point(235, 42);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(26, 13);
            this.lblSort.TabIndex = 119;
            this.lblSort.Text = "Sort";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 325);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 76;
            this.label6.Text = "Ending EventPattern";
            // 
            // ckbDurationFromSibling
            // 
            this.ckbDurationFromSibling.AutoSize = true;
            this.ckbDurationFromSibling.Location = new System.Drawing.Point(140, 147);
            this.ckbDurationFromSibling.Name = "ckbDurationFromSibling";
            this.ckbDurationFromSibling.Size = new System.Drawing.Size(123, 17);
            this.ckbDurationFromSibling.TabIndex = 117;
            this.ckbDurationFromSibling.Text = "Duration from Sibling";
            this.ckbDurationFromSibling.UseVisualStyleBackColor = true;
            // 
            // clbCategory
            // 
            this.clbCategory.FormattingEnabled = true;
            this.clbCategory.Location = new System.Drawing.Point(9, 183);
            this.clbCategory.Name = "clbCategory";
            this.clbCategory.Size = new System.Drawing.Size(176, 139);
            this.clbCategory.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(337, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 13);
            this.label5.TabIndex = 116;
            this.label5.Text = "Document Map Highlight Color";
            // 
            // tbRegularExpression
            // 
            this.tbRegularExpression.Location = new System.Drawing.Point(9, 19);
            this.tbRegularExpression.Name = "tbRegularExpression";
            this.tbRegularExpression.Size = new System.Drawing.Size(415, 20);
            this.tbRegularExpression.TabIndex = 0;
            // 
            // cbDocumentMapHighlightColor
            // 
            this.cbDocumentMapHighlightColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbDocumentMapHighlightColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDocumentMapHighlightColor.FormattingEnabled = true;
            this.cbDocumentMapHighlightColor.Location = new System.Drawing.Point(340, 138);
            this.cbDocumentMapHighlightColor.Name = "cbDocumentMapHighlightColor";
            this.cbDocumentMapHighlightColor.Size = new System.Drawing.Size(165, 21);
            this.cbDocumentMapHighlightColor.TabIndex = 8;
            this.cbDocumentMapHighlightColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbDocumentMapHighlightColor_DrawItem);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 79;
            this.label9.Text = "Name";
            // 
            // btnCopyExample
            // 
            this.btnCopyExample.Location = new System.Drawing.Point(466, 379);
            this.btnCopyExample.Name = "btnCopyExample";
            this.btnCopyExample.Size = new System.Drawing.Size(39, 23);
            this.btnCopyExample.TabIndex = 18;
            this.btnCopyExample.Text = "Copy";
            this.btnCopyExample.UseVisualStyleBackColor = true;
            this.btnCopyExample.Click += new System.EventHandler(this.btnCopyExample_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(9, 58);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(223, 20);
            this.tbName.TabIndex = 2;
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(408, 379);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(52, 23);
            this.btnCapture.TabIndex = 17;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // ckbGapNormal
            // 
            this.ckbGapNormal.AutoSize = true;
            this.ckbGapNormal.Location = new System.Drawing.Point(117, 124);
            this.ckbGapNormal.Name = "ckbGapNormal";
            this.ckbGapNormal.Size = new System.Drawing.Size(219, 17);
            this.ckbGapNormal.TabIndex = 7;
            this.ckbGapNormal.Text = "Gap in Log is normal before this message";
            this.ckbGapNormal.UseVisualStyleBackColor = true;
            // 
            // tbExample
            // 
            this.tbExample.BackColor = System.Drawing.Color.Beige;
            this.tbExample.Location = new System.Drawing.Point(9, 381);
            this.tbExample.Name = "tbExample";
            this.tbExample.Size = new System.Drawing.Size(393, 20);
            this.tbExample.TabIndex = 16;
            // 
            // btnTestEvent
            // 
            this.btnTestEvent.Location = new System.Drawing.Point(430, 17);
            this.btnTestEvent.Name = "btnTestEvent";
            this.btnTestEvent.Size = new System.Drawing.Size(75, 23);
            this.btnTestEvent.TabIndex = 1;
            this.btnTestEvent.Text = "Test";
            this.btnTestEvent.UseVisualStyleBackColor = true;
            this.btnTestEvent.Click += new System.EventHandler(this.btnTestEvent_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 365);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 111;
            this.label4.Text = "Example";
            // 
            // cbSourceType
            // 
            this.cbSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSourceType.FormattingEnabled = true;
            this.cbSourceType.Location = new System.Drawing.Point(6, 97);
            this.cbSourceType.Name = "cbSourceType";
            this.cbSourceType.Size = new System.Drawing.Size(226, 21);
            this.cbSourceType.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(191, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(314, 31);
            this.label2.TabIndex = 110;
            this.label2.Text = "Source Type (Used to limit which File Type the EventPattern is applied to.  \"ANY\" will a" +
    "pply to all File Types)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 81);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(226, 13);
            this.label16.TabIndex = 85;
            this.label16.Text = "Source Type (Used to identify the source type)";
            // 
            // ckbDocumentMap
            // 
            this.ckbDocumentMap.AutoSize = true;
            this.ckbDocumentMap.Location = new System.Drawing.Point(12, 124);
            this.ckbDocumentMap.Name = "ckbDocumentMap";
            this.ckbDocumentMap.Size = new System.Drawing.Size(99, 17);
            this.ckbDocumentMap.TabIndex = 6;
            this.ckbDocumentMap.Text = "Document Map";
            this.ckbDocumentMap.UseVisualStyleBackColor = true;
            // 
            // ckbIgnoreName
            // 
            this.ckbIgnoreName.AutoSize = true;
            this.ckbIgnoreName.Location = new System.Drawing.Point(47, 41);
            this.ckbIgnoreName.Name = "ckbIgnoreName";
            this.ckbIgnoreName.Size = new System.Drawing.Size(87, 17);
            this.ckbIgnoreName.TabIndex = 103;
            this.ckbIgnoreName.Text = "Ignore Name";
            this.ckbIgnoreName.UseVisualStyleBackColor = true;
            // 
            // cbLevel
            // 
            this.cbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevel.FormattingEnabled = true;
            this.cbLevel.Location = new System.Drawing.Point(261, 341);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(244, 21);
            this.cbLevel.TabIndex = 13;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 167);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 97;
            this.label13.Text = "Categories";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(258, 324);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(109, 13);
            this.label10.TabIndex = 90;
            this.label10.Text = "Document Map Level";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(337, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 13);
            this.label12.TabIndex = 95;
            this.label12.Text = "Highlight Color";
            // 
            // ckbTimeFromParent
            // 
            this.ckbTimeFromParent.AutoSize = true;
            this.ckbTimeFromParent.Location = new System.Drawing.Point(11, 147);
            this.ckbTimeFromParent.Name = "ckbTimeFromParent";
            this.ckbTimeFromParent.Size = new System.Drawing.Size(123, 17);
            this.ckbTimeFromParent.TabIndex = 9;
            this.ckbTimeFromParent.Text = "Duration from Parent";
            this.ckbTimeFromParent.UseVisualStyleBackColor = true;
            // 
            // cbColor
            // 
            this.cbColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Location = new System.Drawing.Point(340, 97);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(165, 21);
            this.cbColor.TabIndex = 5;
            this.cbColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbColor_DrawItem);
            // 
            // cbNetworkCommunication
            // 
            this.cbNetworkCommunication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNetworkCommunication.FormattingEnabled = true;
            this.cbNetworkCommunication.Location = new System.Drawing.Point(341, 58);
            this.cbNetworkCommunication.Name = "cbNetworkCommunication";
            this.cbNetworkCommunication.Size = new System.Drawing.Size(165, 21);
            this.cbNetworkCommunication.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(338, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(122, 13);
            this.label11.TabIndex = 93;
            this.label11.Text = "Network Communication";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnEditValue);
            this.tabPage2.Controls.Add(this.lbValues);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.btnAddValue);
            this.tabPage2.Controls.Add(this.btnDeleteValue);
            this.tabPage2.Controls.Add(this.lbLookupValues);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.btnAddLookupValue);
            this.tabPage2.Controls.Add(this.btnEditLookupValue);
            this.tabPage2.Controls.Add(this.btnDeleteLookupValue);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(513, 404);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Values";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnEditValue
            // 
            this.btnEditValue.Location = new System.Drawing.Point(142, 6);
            this.btnEditValue.Name = "btnEditValue";
            this.btnEditValue.Size = new System.Drawing.Size(52, 23);
            this.btnEditValue.TabIndex = 101;
            this.btnEditValue.Text = "Edit";
            this.btnEditValue.UseVisualStyleBackColor = true;
            this.btnEditValue.Click += new System.EventHandler(this.btnEditValue_Click);
            // 
            // lbValues
            // 
            this.lbValues.FormattingEnabled = true;
            this.lbValues.Location = new System.Drawing.Point(7, 35);
            this.lbValues.Name = "lbValues";
            this.lbValues.Size = new System.Drawing.Size(498, 147);
            this.lbValues.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(39, 13);
            this.label14.TabIndex = 99;
            this.label14.Text = "Values";
            // 
            // btnAddValue
            // 
            this.btnAddValue.Location = new System.Drawing.Point(84, 6);
            this.btnAddValue.Name = "btnAddValue";
            this.btnAddValue.Size = new System.Drawing.Size(52, 23);
            this.btnAddValue.TabIndex = 100;
            this.btnAddValue.Text = "Add";
            this.btnAddValue.UseVisualStyleBackColor = true;
            this.btnAddValue.Click += new System.EventHandler(this.btnAddValue_Click);
            // 
            // btnDeleteValue
            // 
            this.btnDeleteValue.Location = new System.Drawing.Point(200, 6);
            this.btnDeleteValue.Name = "btnDeleteValue";
            this.btnDeleteValue.Size = new System.Drawing.Size(52, 23);
            this.btnDeleteValue.TabIndex = 102;
            this.btnDeleteValue.Text = "Delete";
            this.btnDeleteValue.UseVisualStyleBackColor = true;
            this.btnDeleteValue.Click += new System.EventHandler(this.btnDeleteValue_Click);
            // 
            // lbLookupValues
            // 
            this.lbLookupValues.FormattingEnabled = true;
            this.lbLookupValues.Location = new System.Drawing.Point(7, 221);
            this.lbLookupValues.Name = "lbLookupValues";
            this.lbLookupValues.Size = new System.Drawing.Size(498, 173);
            this.lbLookupValues.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 105;
            this.label1.Text = "Lookup Values";
            // 
            // btnAddLookupValue
            // 
            this.btnAddLookupValue.Location = new System.Drawing.Point(84, 192);
            this.btnAddLookupValue.Name = "btnAddLookupValue";
            this.btnAddLookupValue.Size = new System.Drawing.Size(52, 23);
            this.btnAddLookupValue.TabIndex = 106;
            this.btnAddLookupValue.Text = "Add";
            this.btnAddLookupValue.UseVisualStyleBackColor = true;
            this.btnAddLookupValue.Click += new System.EventHandler(this.btnAddLookupValue_Click);
            // 
            // btnEditLookupValue
            // 
            this.btnEditLookupValue.Location = new System.Drawing.Point(142, 192);
            this.btnEditLookupValue.Name = "btnEditLookupValue";
            this.btnEditLookupValue.Size = new System.Drawing.Size(52, 23);
            this.btnEditLookupValue.TabIndex = 107;
            this.btnEditLookupValue.Text = "Edit";
            this.btnEditLookupValue.UseVisualStyleBackColor = true;
            this.btnEditLookupValue.Click += new System.EventHandler(this.btnEditLookupValue_Click);
            // 
            // btnDeleteLookupValue
            // 
            this.btnDeleteLookupValue.Location = new System.Drawing.Point(200, 192);
            this.btnDeleteLookupValue.Name = "btnDeleteLookupValue";
            this.btnDeleteLookupValue.Size = new System.Drawing.Size(52, 23);
            this.btnDeleteLookupValue.TabIndex = 108;
            this.btnDeleteLookupValue.Text = "Delete";
            this.btnDeleteLookupValue.UseVisualStyleBackColor = true;
            this.btnDeleteLookupValue.Click += new System.EventHandler(this.btnDeleteLookupValue_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tbDocumentation);
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(513, 404);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Documentation";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbDocumentation
            // 
            this.tbDocumentation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDocumentation.Location = new System.Drawing.Point(3, 33);
            this.tbDocumentation.Name = "tbDocumentation";
            this.tbDocumentation.Size = new System.Drawing.Size(507, 368);
            this.tbDocumentation.TabIndex = 19;
            this.tbDocumentation.Text = "";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.cbIgnoreDocumentation);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(507, 30);
            this.panel3.TabIndex = 70;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 69;
            this.label3.Text = "Documentation";
            // 
            // cbIgnoreDocumentation
            // 
            this.cbIgnoreDocumentation.AutoSize = true;
            this.cbIgnoreDocumentation.Location = new System.Drawing.Point(91, 7);
            this.cbIgnoreDocumentation.Name = "cbIgnoreDocumentation";
            this.cbIgnoreDocumentation.Size = new System.Drawing.Size(131, 17);
            this.cbIgnoreDocumentation.TabIndex = 14;
            this.cbIgnoreDocumentation.Text = "Ignore Documentation";
            this.cbIgnoreDocumentation.UseVisualStyleBackColor = true;
            this.cbIgnoreDocumentation.CheckedChanged += new System.EventHandler(this.cbIgnoreDocumentation_CheckedChanged);
            // 
            // StatusPanel
            // 
            this.StatusPanel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.StatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusPanel.Location = new System.Drawing.Point(86, 611);
            this.StatusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Padding = new System.Windows.Forms.Padding(1);
            this.StatusPanel.Size = new System.Drawing.Size(365, 79);
            this.StatusPanel.SP_DefaultStatusSize = true;
            this.StatusPanel.SP_DisplayRemainingTime = false;
            this.StatusPanel.SP_Enabled = true;
            this.StatusPanel.SP_FullScreen = false;
            this.StatusPanel.TabIndex = 88;
            this.StatusPanel.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 433);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(521, 29);
            this.panel2.TabIndex = 121;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(362, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(443, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddEditEventView
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(521, 462);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEditEventView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add EventPattern";
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox ckbDocumentMap;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbSourceType;
        private System.Windows.Forms.Button btnTestEvent;
        private System.Windows.Forms.CheckBox ckbGapNormal;
        public System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox tbRegularExpression;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckedListBox clbCategory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbFoldingEventId;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RichTextBox tbDocumentation;
        private System.Windows.Forms.Label label3;
        private Sara.WinForm.ControlsNS.StatusPanel StatusPanel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbLevel;
        private System.Windows.Forms.CheckBox ckbTimeFromParent;
        private System.Windows.Forms.CheckBox cbIgnoreDocumentation;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbNetworkCommunication;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbColor;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnDeleteValue;
        private System.Windows.Forms.Button btnEditValue;
        private System.Windows.Forms.Button btnAddValue;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ListBox lbValues;
        private System.Windows.Forms.CheckBox ckbIgnoreName;
        private System.Windows.Forms.Button btnDeleteLookupValue;
        private System.Windows.Forms.Button btnEditLookupValue;
        private System.Windows.Forms.Button btnAddLookupValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbLookupValues;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox clbSourceType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCopyExample;
        private System.Windows.Forms.Button btnCapture;
        public System.Windows.Forms.TextBox tbExample;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbDocumentMapHighlightColor;
        private System.Windows.Forms.CheckBox ckbDurationFromSibling;
        public System.Windows.Forms.TextBox tbSort;
        private System.Windows.Forms.Label lblSort;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel3;
    }
}