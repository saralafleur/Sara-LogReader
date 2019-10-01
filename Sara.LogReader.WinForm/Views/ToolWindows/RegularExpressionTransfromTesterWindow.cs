using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Sara.Common.DateTimeNS;
using Sara.LogReader.Model.ValueNS;
using Sara.LogReader.Service;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.ColorScheme.Modal;

namespace Sara.LogReader.WinForm.Views.ToolWindows
{
    public partial class RegularExpressionTransfromTesterWindow : Form
    {
        public RegularExpressionHideTesterViewModel ViewModel { get; set; }
        public Value Model { get; set; }

        public string RegularExpression { get; set; }

        public RegularExpressionTransfromTesterWindow()
        {
            InitializeComponent();

            ViewModel = new RegularExpressionHideTesterViewModel(this);
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
            tbReplaceWith.Text = Model.ReplaceWith;
            ckbFileInfo.Checked = true;

            fctbDocument.Text = ckbFileInfo.Checked
                           ? MainViewModel.Current.Text
                           : MainViewModel.Current.CurrentLine;

            fctbResult.Text = string.Empty;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            Model.Expression = tbRegularExpression.Text;
            Model.FileInfo = ckbFileInfo.Checked;
            Model.ReplaceWith = tbReplaceWith.Text;
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

                fctbResult.Text = HideOptionService.Apply(tbRegularExpression.Text, tbReplaceWith.Text, fctbDocument.Text);
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
    }
}
