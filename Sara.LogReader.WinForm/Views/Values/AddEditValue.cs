using System;
using System.Windows.Forms;
using Sara.LogReader.Common;
using Sara.LogReader.Model.Categories;
using Sara.LogReader.Model.DocumentMap;
using Sara.LogReader.Model.ValueNS;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.Common;
using Sara.WinForm.CRUD;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Values
{
    public partial class AddEditValue : Form
    {
        private ValueViewModel ViewModel { get; set; }
        public ValueModel Model { get; set; }

        public AddEditValue(ValueViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            ColorService.Setup(this);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                StatusPanel.StatusUpdate(StatusModel.Update("Saving..."));
                var i = Model.Item;

                i.Categories.Clear();
                foreach (var checkedItem in clbCategory.CheckedItems)
                {
                    i.Categories.Add((Category)checkedItem);
                }
                i.SourceTypes.Clear();
                foreach (var checkedItem in clbSourceType.CheckedItems)
                {
                    i.SourceTypes.Add(checkedItem.ToString());
                }

                i.Expression = tbExpression.Text;
                i.Name = cbValueName.Text;
                i.Sort = tbSort.Text;
                i.Distinct = ckbDistinct.Checked;
                i.FileInfo = ckbFileInfo.Checked;
                i.LazyLoad = ckbLazyLoad.Checked;
                i.DocumentMap = ckbDocumentMap.Checked;
                i.Level = CRUDUIService.GetEnumValue<DocumentMapLevel>(cbLevel);
                ViewModel.Save(Model);

                Close();
            }
            finally
            {
                StatusPanel.StatusUpdate(StatusModel.Completed);
                Cursor.Current = Cursors.Default;
            }
        }

        public void Render(ValueModel model)
        {
            Model = model;
            var i = model.Item;
            tbExpression.Text = i.Expression;
            ckbDocumentMap.Checked = i.DocumentMap;
            ckbFileInfo.Checked = i.FileInfo;
            ckbLazyLoad.Checked = i.LazyLoad;
            ckbDistinct.Checked = i.Distinct;
            tbSort.Text = i.Sort;
            cbValueName.Items.Clear();
            foreach (var item in Keywords.GetKeyWords())
            {
                cbValueName.Items.Add(item);
            }
            cbValueName.Text = i.Name;

            clbCategory.Items.Clear();
            foreach (var item in model.Categories)
            {
                clbCategory.Items.Add(item);

                foreach (var category in model.Item.Categories)
                {
                    if (item.CategoryId == category.CategoryId)
                        clbCategory.SetItemChecked(clbCategory.Items.Count - 1, true);
                }
            }
            UICommon.AutoColumnWidth(clbCategory);

            clbSourceType.Items.Clear();
            clbSourceType.Items.Add(Keywords.ALL,model.Item.SourceTypes.Contains(Keywords.ALL));
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
                    Text = @"Add Value";
                    break;
                case InputMode.Edit:
                    Text = @"Edit Value";
                    break;
            }

            CRUDUIService.RenderEnumList<DocumentMapLevel>(cbLevel,i.Level);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            TestRegularExpression(OnUpdateExpression);
        }

        private void TestRegularExpression(Action<Value> callback)
        {
            var testWindow = new RegularExpressionTesterWindow
            {
                Model =
                {
                    Expression = tbExpression.Text,
                    Distinct = ckbDistinct.Checked,
                    FileInfo = ckbFileInfo.Checked
                }
            };
            testWindow.ViewModel.UpdateRegularExpressionEvent += callback;
            testWindow.ShowDialog();
        }

        private void OnUpdateExpression(Value model)
        {
            tbExpression.Text = model.Expression;
            ckbDistinct.Checked = model.Distinct;
            ckbFileInfo.Checked = model.FileInfo;
        }

    }
}
