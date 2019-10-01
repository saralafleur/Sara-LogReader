using System;
using System.Threading;
using System.Windows.Forms;
using Sara.LogReader.WinForm.Views.ToolWindows;
using System.ComponentModel;
using Sara.Common.Extension;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.EventPerformance
{
    public partial class EventPerformanceTest : ToolWindow, IViewDock<object, object>
    {
        #region Properties
        public bool StartupReady { get { return MainViewModel.StartupComplete; } }
        public EventPerformanceTestViewModel ViewModel { get; internal set; }
        #endregion

        #region Setup
        public EventPerformanceTest()
        {
            InitializeComponent();
        }
        #endregion

        #region Render
        public void Render(object model)
        {
            if (model == null)
            {
                wpOpen.ShowWarning();
                StatusPanel.StatusUpdate(StatusModel.Completed);
                return;
            }

            wpOpen.HideWarning();

            StatusPanel.StatusUpdate(StatusModel.Completed);
        }
        #endregion

        #region Events
        private int _lastSelectedEventId = -1;
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (dgvResults.SelectedRows.Count > 0)
                _lastSelectedEventId = (int)dgvResults.SelectedRows[0].Cells[EventPerformanceConst.EventId].Value;

            if (MainViewModel.Current == null)
            {
                MessageBox.Show(@"You must first open a File.");
                return;
            }
            StatusPanel.StatusUpdate(StatusModel.Update("Running Test..."));
            StatusPanel.StatusUpdate(StatusModel.StartStopWatch);

            btnEditEvent.Enabled = false;

            ThreadPool.QueueUserWorkItem(delegate
            {
                var model = ViewModel.RunTest();
                RunCallback(model);
            });
        }
        private void RunCallback(PerformanceResult model)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<PerformanceResult>(RunCallback), model);
                return;
            }

            dgvResults.DataSource = model.Result;
            DataGridViewHelper.MakeSortable(dgvResults);
            DataGridViewHelper.AutoSizeGrid(dgvResults);
            dgvResults.Sort(dgvResults.Columns[EventPerformanceConst.DurationTS], ListSortDirection.Descending);
            dgvResults.Columns[EventPerformanceConst.DurationTS].HeaderCell.SortGlyphDirection = SortOrder.Descending;
            dgvResults.ReadOnly = true;

            if (_lastSelectedEventId != -1)
            {
                dgvResults.ClearSelection();
                foreach (DataGridViewRow row in dgvResults.Rows)
                {
                    if (row.Cells[EventPerformanceConst.EventId].Value.ToString().Equals(_lastSelectedEventId.ToString()))
                    {
                        row.Selected = true;
                        dgvResults.CurrentCell = row.Cells[0];
                        break;
                    }
                }
            }

            lblTotalDuration.Text = $"Total Duration: {model.TotalDuration.ToReadableString()}";
            lblTotalEvents.Text = $"Total Events: {model.TotalEvents}";
            btnEditEvent.Enabled = true;
            StatusPanel.StatusUpdate(StatusModel.Completed);
        }
        private void btnEditEvent_Click(object sender, EventArgs e)
        {
            if (dgvResults.SelectedRows.Count == 0)
            {
                MessageBox.Show(@"You must first select a EventPattern.");
                return;
            }

            var eventId = (int)dgvResults.SelectedRows[0].Cells[EventPerformanceConst.EventId].Value;
            MainViewModel.EditEvent(eventId);
        }
        public void StatusUpdate(IStatusModel model)
        {
            StatusPanel.StatusUpdate(model);
        }
        public void UpdateView(object selectedModel)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void dgvResults_DoubleClick(object sender, EventArgs e)
        {
            var iLine = (int)dgvResults.SelectedRows[0].Cells[EventPerformanceConst.FirstiLine].Value;
            MainViewModel.GoToLine(iLine);
        }
    }
}
