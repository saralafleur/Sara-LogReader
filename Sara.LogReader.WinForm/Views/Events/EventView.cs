using System;
using System.Linq;
using System.Windows.Forms;
using Sara.LogReader.WinForm.Views.ToolWindows;
using System.Reflection;
using Sara.Logging;
using Sara.LogReader.Model;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.Common;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Events
{
    public partial class EventView : ToolWindow, IViewDock<EventCacheData, EventModel>
    {
        internal EventViewModel ViewModel { get; set; }

        public EventView()
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

        public void Render(EventCacheData model)
        {
            // Note: Invoke is handled by ViewModelBase - Sara
            dgvEvents.DataSource = model.Events.OrderBy(n => n.Name).ToList();

            dgvEvents.DataSource = model.Events
                .Where(n => (!ckbFilter.Checked ||
                             (
                               UICommon.WhereCheckListBox(clbSourceType, n.SourceTypes) &&
                               UICommon.WhereCheckListBox(clbCategory, n.Categories) &&
                               UICommon.WhereText(tbName, n.Name) &&
                               UICommon.WhereText(tbRegularExpression, n.RegularExpression) &&
                               UICommon.WhereCheck(ckbDocumentMap, n.DocumentMap)
                             )
                            )
                      )
                .OrderBy(n => n.Name).ToList();


            DataGridViewHelper.AutoSizeGrid(dgvEvents);

            RenderView();
            StatusUpdate(StatusModel.Completed);
        }

        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void StatusUpdate(IStatusModel model)
        {
            StatusPanel.StatusUpdate(model);
        }

        /// <summary>
        /// Navigates to a EventPattern based on the eventId
        /// </summary>
        protected internal void GoToEventId(int eventId)
        {
            if (SelectRow(eventId))
                Show();
        }
        private bool SelectRow(int eventId)
        {
            try
            {
                foreach (var row in from DataGridViewRow row in dgvEvents.Rows
                                    let model = (EventLR)row.DataBoundItem
                                    where model.EventId == eventId
                                    select row)
                {
                    row.Selected = true;
                    // If FirstDisplayScrollingRowIndex == -1, then the grid is not visible on the screen - Sara
                    if (dgvEvents.Visible && (dgvEvents.FirstDisplayedScrollingRowIndex != -1))
                    {
                        dgvEvents.CurrentCell = row.Cells[0];
                        dgvEvents.FirstDisplayedScrollingRowIndex = row.Index;
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.WriteError(typeof(EventView).FullName, MethodBase.GetCurrentMethod().Name,ex);
                return false;
            }
        }

        private EventLR GetCurrentSelectedRowItem()
        {
            EventLR model = null;
            if (dgvEvents.CurrentRow != null)
            {
                model = (EventLR)dgvEvents.CurrentRow.DataBoundItem;
            }
            return model;
        }
        public void UpdateView(EventModel selectedModel)
        {
            SelectRow(selectedModel.Item.EventId);
        }
        private void RenderView()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(RenderView));
                return;
            }

            var model = GetCurrentSelectedRowItem();

            btnEdit.Enabled = btnDelete.Enabled = model != null;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var model = GetCurrentSelectedRowItem();
            if (model == null)
                return;
            ViewModel.Edit(model.EventId);
            SelectRow(model.EventId);
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
                ViewModel.Delete(model.EventId);
            }
        }
        private void dgvEvents_SelectionChanged(object sender, EventArgs e)
        {
            RenderView();
        }

        private void ckbFilter_CheckedChanged(object sender, EventArgs e)
        {
            pnlFilter.Visible = ckbFilter.Checked;
            ViewModel.RenderDocument();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ViewModel.RenderDocument();
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            tbRegularExpression.Text = "";
            tbName.Text = "";
            ckbDocumentMap.CheckState = CheckState.Indeterminate;
            UICommon.ClearCheckListBox(clbCategory);
            clbCategory.SetItemChecked(0, true);
            UICommon.ClearCheckListBox(clbSourceType);
            clbSourceType.SetItemChecked(0, true);
        }
    }
}
