using System;
using System.Linq;
using Sara.LogReader.Model.Performance;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Values
{
    public partial class PerformanceView : ToolWindow, IViewDock<PerformanceData, PerformanceModel>
    {
        protected internal PerformanceViewModel ViewModel { get; set; }

        public PerformanceView()
        {
            InitializeComponent();
        }

        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void Render(PerformanceData model)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<PerformanceData>(Render), model);
                return;
            }

            dgvPerformanceEvents.DataSource = model.Items.OrderByDescending(n => n.StartTime).ToList();
            DataGridViewHelper.AutoSizeGrid(dgvPerformanceEvents);
            DataGridViewHelper.MakeSortable(dgvPerformanceEvents);

            StatusUpdate(StatusModel.Completed);
        }
        public void StatusUpdate(IStatusModel model)
        {
            StatusPanel.StatusUpdate(model);
        }

        public void UpdateView(PerformanceModel selectedModel)
        {
            throw new NotImplementedException();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ViewModel.Clear();
        }
    }
}
