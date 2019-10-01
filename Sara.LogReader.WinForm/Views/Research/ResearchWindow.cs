using System;
using System.Linq;
using System.Windows.Forms;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.Model.ResearchNS;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm;
using Sara.WinForm.Common;
using Sara.WinForm.CRUD;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;
using Sara.LogReader.Model.ResearchNS;

namespace Sara.LogReader.WinForm.Views.Research
{
    public partial class ResearchWindow : ToolWindow, IViewDock<ResearchCacheData, ResearchModel>
    {
        protected internal ResearchViewModel ViewModel { get; set; }

        public ResearchWindow()
        {
            InitializeComponent();
        }

        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void Render(ResearchCacheData model)
        {
            dgvResearch.DataSource = model.Items.OrderBy(n => n.Name).ToList();
            DataGridViewHelper.AutoSizeGrid(dgvResearch);
            DataGridViewHelper.MakeSortable(dgvResearch);
            RenderView();
            StatusUpdate(StatusModel.Completed);
        }
        public void StatusUpdate(IStatusModel model)
        {
            StatusPanel.StatusUpdate(model);
        }
        public void UpdateView(ResearchModel model)
        {
            if (model.Mode == InputMode.Delete)
                ViewModel.RenderDocument();
            else
                SelectRow(model.Item.ResearchId);
        }

        private bool SelectRow(int valueId)
        {
            foreach (DataGridViewRow row in dgvResearch.Rows)
            {
                var model = (Model.ResearchNS.Research)row.DataBoundItem;
                if (model.ResearchId != valueId) continue;

                row.Selected = true;
                dgvResearch.CurrentCell = row.Cells[0];
                return true;
            }
            return false;
        }
        private Model.ResearchNS.Research GetCurrentSelectedRowItem()
        {
            Model.ResearchNS.Research model = null;
            if (dgvResearch.CurrentRow != null)
            {
                model = (Model.ResearchNS.Research)dgvResearch.CurrentRow.DataBoundItem;
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

            ViewModel.Edit(model.ResearchId);
            SelectRow(model.ResearchId);
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

            // ConfirmDelete().DoStatus().

            UICommon.ConfirmDelete(delegate
            {
                StatusPanel.DoAction("Deleting...", delegate { ViewModel.Delete(model.ResearchId); });
            });
        }
        private void dgvValues_SelectionChanged(object sender, EventArgs e)
        {
            RenderView();
        }
    }
}
