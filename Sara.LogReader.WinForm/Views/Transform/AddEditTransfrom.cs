using System;
using System.Windows.Forms;
using Sara.LogReader.Common;
using Sara.LogReader.Model.HideOptionNS;
using Sara.LogReader.Model.ValueNS;
using Sara.LogReader.WinForm.ViewModel;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.Common;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.WinForm.Views.HideOptions
{
    public partial class AddEditHideOptions : Form
    {
        private HideOptionModel Model { get; set; }
        public HideOptionViewModel ViewModel { get; set; }

        public AddEditHideOptions()
        {
            InitializeComponent();
            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Model.Item.Name = tbOption.Text;

            var i = Model.Item;

            i.RegularExpression = tbRegularExpression.Text;
            i.ReplaceWith = tbReplaceWith.Text;

            i.SourceTypes.Clear();
            foreach (var checkedItem in clbSourceType.CheckedItems)
            {
                i.SourceTypes.Add(checkedItem.ToString());
            }

            if (i.SourceTypes.Count == 0)
                i.SourceTypes.Add(Keywords.ALL);

            ViewModel.Save(Model);
            Close();
        }
        public void Render(HideOptionModel model)
        {
            Model = model;
            tbOption.Text = Model.Item.Name;
            tbRegularExpression.Text = Model.Item.RegularExpression;
            tbReplaceWith.Text = Model.Item.ReplaceWith;
            clbSourceType.Items.Clear();
            clbSourceType.Items.Add(Keywords.ALL, model.Item.SourceTypes.Contains(Keywords.ALL));
            foreach (var item in model.SourceTypes)
            {
                clbSourceType.Items.Add(item);
                foreach (var sourceType in model.Item.SourceTypes)
                {
                    if (item == sourceType)
                        clbSourceType.SetItemChecked(clbSourceType.Items.Count - 1, true);
                }
            }
            UICommon.AutoColumnWidth(clbSourceType);

            switch (model.Mode)
            {
                case InputMode.Add:
                    Text = @"Add Transform";
                    break;
                case InputMode.Edit:
                    Text = @"Edit Transform";
                    break;
            }

        }

        private void btnTestEvent_Click(object sender, EventArgs e)
        {
            if (MainViewModel.Current == null)
            {
                MessageBox.Show("A document must be open before you can test an Expression.", "Warning");
                return;
            }

            RegularExpressionTest.TestTransform(tbRegularExpression.Text, tbReplaceWith.Text, (Value m) =>
            {
                tbRegularExpression.Text = m.Expression;
                tbReplaceWith.Text = m.ReplaceWith;
            });
        }

        private void AddEditHideOptions_Shown(object sender, EventArgs e)
        {
            ColorService.Setup(this);
        }
    }
}
