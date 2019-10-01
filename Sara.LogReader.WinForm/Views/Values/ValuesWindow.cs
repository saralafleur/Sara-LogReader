using System;
using System.Linq;
using System.Windows.Forms;
using Sara.LogReader.Model;
using Sara.LogReader.Model.ValueNS;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.Common;
using Sara.WinForm.CRUD;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Values
{
    public partial class ValuesWindow : ToolWindow, IViewDock<ValueCacheData, ValueModel>
    {
        protected internal ValueViewModel ViewModel { get; set; }
        public ValuesWindow()
        {
            InitializeComponent();

            ColorService.Setup(this);

            clbSourceType.Items.Clear();
            clbSourceType.Items.Add(UICommon.SelectAll);
            foreach (var item in XmlDal.DataModel.Options.FileTypes)
                clbSourceType.Items.Add(item);
            clbSourceType.SetItemChecked(0, true);
            UICommon.AutoColumnWidth(clbSourceType);

            clbCategory.Items.Clear();
            clbCategory.Items.Add(UICommon.SelectAll);
            foreach (var item in XmlDal.DataModel.CategoriesModel.Categories.OrderBy(n => n.Name).ToList())
                clbCategory.Items.Add(item.Name);
            clbCategory.SetItemChecked(0, true);
            UICommon.AutoColumnWidth(clbCategory);
        }

        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void Render(ValueCacheData model)
        {
            dgvValues.DataSource = model.Values
                .Where(n => (!ckbFilter.Checked ||
                             (
                               UICommon.WhereCheckListBox(clbSourceType, n.SourceTypes) &&
                               UICommon.WhereCheckListBox(clbCategory, n.Categories) &&
                               UICommon.WhereText(tbName, n.Name) &&
                               UICommon.WhereText(tbRegularExpression, n.Expression) &&
                               UICommon.WhereCheck(ckbLazyLoad, n.LazyLoad) &&
                               UICommon.WhereCheck(ckbDocumentMap, n.DocumentMap) &&
                               UICommon.WhereCheck(ckbDistinct, n.Distinct) &&
                               UICommon.WhereCheck(ckbFileInfo, n.FileInfo)
                             )
                            )
                      )
                .OrderBy(n => n.Name).ToList();
            DataGridViewHelper.AutoSizeGrid(dgvValues);
            DataGridViewHelper.MakeSortable(dgvValues);
            RenderView();
            StatusUpdate(StatusModel.Completed);
        }


        public void StatusUpdate(IStatusModel model)
        {
            StatusPanel.StatusUpdate(model);
        }
        public void UpdateView(ValueModel model)
        {
            if (model.Mode == InputMode.Delete)
                ViewModel.RenderDocument();
            else
                SelectRow(model.Item.ValueId);
        }

        /// <summary>
        /// Navigates to a Value based on the valueId
        /// </summary>
        protected internal void OnGoToValueId(int valueId)
        {
            if (SelectRow(valueId))
                Show();
        }
        private bool SelectRow(int valueId)
        {
            foreach (DataGridViewRow row in dgvValues.Rows)
            {
                var model = (Value)row.DataBoundItem;
                if (model.ValueId != valueId) continue;

                row.Selected = true;
                dgvValues.CurrentCell = row.Cells[0];
                return true;
            }
            return false;
        }
        private Value GetCurrentSelectedRowItem()
        {
            Value model = null;
            if (dgvValues.CurrentRow != null)
            {
                model = (Value)dgvValues.CurrentRow.DataBoundItem;
            }
            return model;
        }
        private void RenderView()
        {
            var model = GetCurrentSelectedRowItem();

            btnEdit.Enabled = btnDelete.Enabled = model != null;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var model = GetCurrentSelectedRowItem();
            if (model == null)
                return;

            ViewModel.Edit(model.ValueId);
            SelectRow(model.ValueId);
            RenderView();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ViewModel.Add();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var model = GetCurrentSelectedRowItem();
            if (model == null) return;

            var confirmResult = MessageBox.Show(@"Are you sure you want to delete this item?", @"Confirm Delete!",
                                    MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                ViewModel.Delete(model.ValueId);
            }
        }
        private void dgvValues_SelectionChanged(object sender, EventArgs e)
        {
            RenderView();
        }
        private void ckbFilter_CheckedChanged_1(object sender, EventArgs e)
        {
            pnlFilter.Visible = ckbFilter.Checked;
            ViewModel.RenderDocument();
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            tbRegularExpression.Text = "";
            tbName.Text = "";
            ckbDocumentMap.CheckState = CheckState.Indeterminate;
            ckbLazyLoad.CheckState = CheckState.Indeterminate;
            ckbDistinct.CheckState = CheckState.Indeterminate;
            ckbFileInfo.CheckState = CheckState.Indeterminate;
            UICommon.ClearCheckListBox(clbCategory);
            clbCategory.SetItemChecked(0, true);
            UICommon.ClearCheckListBox(clbSourceType);
            clbSourceType.SetItemChecked(0, true);
        }


        private void btnApply_Click(object sender, EventArgs e)
        {
            ViewModel.RenderDocument();
        }
    }
}
