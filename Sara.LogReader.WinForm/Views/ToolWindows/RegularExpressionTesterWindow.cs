using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using FastColoredTextBoxNS;
using Sara.Common.DateTimeNS;
using Sara.Common.Extension;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.ValueNS;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.ColorScheme.Modal;

namespace Sara.LogReader.WinForm.Views.ToolWindows
{
    public partial class RegularExpressionTesterWindow : Form
    {
        public RegularExpressionTesterViewModel ViewModel { get; set; }
        public Value Model { get; set; }

        public string RegularExpression { get; set; }

        public RegularExpressionTesterWindow()
        {
            InitializeComponent();

            ViewModel = new RegularExpressionTesterViewModel(this);
            Model = new Value();
            fctbDocument.CurrentLineColor = Color.Red;
            ColorService.Setup(this);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RegularExpressionTesterWindow_Load(object sender, EventArgs e)
        {
            Render();
        }

        private void Render()
        {
            tbRegularExpression.Text = Model.Expression;
            ckbFileInfo.Checked = true; // Model.FileInfo;

            fctbDocument.Text = ckbFileInfo.Checked
                           ? MainViewModel.Current.Text
                           : MainViewModel.Current.CurrentLine;

            ckbDistinct.Checked = Model.Distinct;

            if (Model.ExpressionOnly)
            {
                ckbFileInfo.Text = @"All Lines";
            }

            tvResult.Nodes.Clear();
            RenderTotal(null);
        }

        private void RenderTotal(TimeSpan? duration)
        {
            if (duration != null)
                lblTime.Text = string.Format("Duration: {0}", duration.Value.ToReadableString());
            lblTotalMatches.Text = string.Format("Total Matches: {0}",
                                                 tvResult.Nodes.Count.ToString(CultureInfo.InvariantCulture));
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            Model.Expression = tbRegularExpression.Text;
            Model.Distinct = ckbDistinct.Checked;
            Model.FileInfo = ckbFileInfo.Checked;
            ViewModel.Accept(Model);
            Close();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                var sw = new Stopwatch(MethodBase.GetCurrentMethod().Name);
                Cursor = Cursors.WaitCursor;
                // Do not test an emtpy expression - Sara LaFleur
                if (string.IsNullOrEmpty(tbRegularExpression.Text))
                    return;
                var sw2 = new Stopwatch("Time Test");
                TimeSpan duration;
                var model = ViewModel.Test(Model.ValueId,
                                           Model.Name,
                                           ckbFileInfo.Checked ? MainViewModel.Current.Model.File.RawTextString : fctbDocument.Text,
                                           tbRegularExpression.Text,
                                           ckbDistinct.Checked,
                                           ckbFileInfo.Checked,
                                           Model.DocumentMap,
                                           out duration);
                sw2.Stop(0);
                tvResult.BeginUpdate();
                try
                {
                    tvResult.Nodes.Clear();
                    foreach (var item in model.Values)
                    {
                        if (!ckbFileInfo.Checked)
                            item.iLine = MainViewModel.Current.CurrentiLine;
                        var node = new TreeNode { Tag = item, Text = item.ToString() };
                        tvResult.Nodes.Add(node);
                    }
                }
                finally
                {
                    tvResult.EndUpdate();
                }
                sw.Stop();
                RenderTotal(duration);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ckbCurrentLine_CheckedChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                if (MainViewModel.Current == null)
                    fctbDocument.Text = "";
                else
                    fctbDocument.Text = ckbFileInfo.Checked
                           ? MainViewModel.Current.Text
                           : MainViewModel.Current.CurrentLine;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void tvResult_DoubleClick(object sender, EventArgs e)
        {
            if (tvResult.SelectedNode == null)
                return;

            var item = tvResult.SelectedNode.Tag as ValueBookMark;
            if (item == null) return;

            MainViewModel.Current.GoToLine(item.iLine);
        }
        private List<string> _lines;
        private List<string> Lines
        {
            get
            {
                if (_lines != null)
                    return _lines;

                _lines = fctbDocument.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();

                return _lines;
            }

        }

        private void tvResult_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvResult.SelectedNode != null)
            {
                var item = tvResult.SelectedNode.Tag as ValueBookMark;
                if (item == null) return;

                fctbDocument.Selection.Start = new Place(0, item.iLine);
                fctbDocument.DoCaretVisible();

                tbCurrentLine.Text = Lines[item.iLine];
            }
        }
    }
}
