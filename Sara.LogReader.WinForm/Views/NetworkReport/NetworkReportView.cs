using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Sara.Common.Extension;
using Sara.LogReader.Model.NetworkReport;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.ControlsNS;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;
using WeifenLuo.WinFormsUI.Docking;

namespace Sara.LogReader.WinForm.Views.NetworkReport
{
    public partial class NetworkReportView : ToolWindow, IViewDock<NetworkReportModel, object>
    {
        protected internal NetworkReportViewModel ViewModel { get; set; }
        public NetworkReportModel Model { get; set; }

        public NetworkReportView()
        {
            InitializeComponent();
        }

        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void Render(NetworkReportModel model)
        {
            // Note: Invoke is handled by ViewModelBase - Sara

            Model = model;

            StatusUpdate(StatusModel.Update("Rendering"));
            try
            {
                lbResults.Items.Clear();
                Model.Results.Reverse();
                foreach (var result in Model.Results)
                {
                    lbResults.Items.Add(result);
                }

                if (lbResults.Items.Count > 0)
                    lbResults.SelectedIndex = 0;

                lblNetworkCount.Text = lbResults.Items.Count.ToString(CultureInfo.InvariantCulture);

                RenderResults();
            }
            finally
            {
                StatusUpdate(StatusModel.Completed);
            }
        }

        private void RenderResults()
        {
            StatusUpdate(StatusModel.Update("Updating results..."));
            try
            {
                if (lbResults.SelectedItem == null)
                {
                    dgvResults.DataSource = null;
                    btnDelete.Enabled = false;
                    btnRename.Enabled = false;
                    btnReRun.Enabled = false;
                    return;
                }

                btnDelete.Enabled = true;
                btnRename.Enabled = true;
                btnReRun.Enabled = true;

                var reportResult = lbResults.SelectedItem as NetworkReportResultCacheData;
                if (reportResult == null)
                    return;

                var dt = new DataTable();
                dt.Columns.Add(new DataColumn(NetworkReportItem.SOURCE_PATH, typeof(string)));
                dt.Columns.Add(new DataColumn(NetworkReportItem.SOURCE_DATETIME, typeof(DateTime)));
                dt.Columns.Add(new DataColumn(NetworkReportItem.SOURCE_NETWORK_MESSAGE, typeof(string)));
                dt.Columns.Add(new DataColumn(NetworkReportItem.SOURCE_ILINE, typeof(int)));
                dt.Columns.Add(new DataColumn(NetworkReportItem.TARGET_PATH, typeof(string)));
                dt.Columns.Add(new DataColumn(NetworkReportItem.TARGET_DATETIME, typeof(DateTime)));
                dt.Columns.Add(new DataColumn(NetworkReportItem.TARGET_NETWORK_MESSAGE, typeof(string)));
                dt.Columns.Add(new DataColumn(NetworkReportItem.TARGET_ILINE, typeof(int)));
                dt.Columns.Add(new DataColumn(NetworkReportItem.TARGET_MATCHES, typeof(int)));
                dt.Columns.Add(new DataColumn(NetworkReportItem.DATETIME_DIFF, typeof(double)));
                dt.Columns.Add(new DataColumn(NetworkReportItem.SYSTEM_CLOCK_DIFF, typeof(double)));

                foreach (var result in reportResult.Results)
                {
                    var row = new List<object>
                    {
                        result.SourcePath,
                        result.SourceDateTime,
                        result.SourceNetworkMessage,
                        result.SourceiLine,
                        result.TargetPath,
                        result.TargetDateTime,
                        result.TargetNetworkMessage,
                        result.TargetiLine,
                        result.TargetMatches,
                        result.DateTimeDifference,
                        result.SystemClockDifference
                    };

                    dt.Rows.Add(row.ToArray());
                }

                dgvResults.DataSource = dt;

                dgvSummary.DataSource = reportResult.Summary;

                // Hide iLine Columns
                var column = dgvResults.Columns[NetworkReportItem.SOURCE_ILINE];
                if (column != null) column.Visible = false;
                column = dgvResults.Columns[NetworkReportItem.TARGET_ILINE];
                if (column != null) column.Visible = false;

                lblResultTotal.Text = dt.Rows.Count.ToString(CultureInfo.InvariantCulture);
            }
            finally
            {
                StatusUpdate(StatusModel.Completed);
            }
        }

        public void StatusUpdate(IStatusModel model)
        {
            StatusPanel.StatusUpdate(model);
        }

        public void UpdateView(object selectedModel)
        {
            throw new NotImplementedException();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            ViewModel.Run();
        }

        public void EnableCancel(bool value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(EnableCancel), value);
                return;
            }
            if (value)
            {
                btnCancel.Text = @"Cancel";
                btnCancel.Enabled = true;
            }

            btnCancel.Visible = value;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Text = @"Cancelling";
            btnCancel.Enabled = false;
            ViewModel.CancelReport();
        }

        public void ReportCancelled()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ReportCancelled));
                return;
            }

            ErrorPanel.ShowError(this, "Report was cancelled", null);
        }

        private void btnExportToCsv_Click(object sender, EventArgs e)
        {
            var dt = dgvResults.DataSource as DataTable;
            if (dt == null)
                throw new Exception("DataSource must be of type DataTable");

            var saveFileDialog1 = new SaveFileDialog { Filter = @"CSV|*.csv|Text|*.txt", Title = @"Export to CSV" };
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                dt.ToCsv(saveFileDialog1.FileName);
            }
        }

        private string _lastReportName = string.Empty;
        private void lbResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            var reportResult = lbResults.SelectedItem as NetworkReportResultCacheData;
            if (reportResult == null)
                return;

            if (reportResult.ReportName == _lastReportName)
                return;

            _lastReportName = reportResult.ReportName;

            RenderResults();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var model = lbResults.SelectedItem as NetworkReportResultCacheData;
            if (model == null)
                return;

            var result = MessageBox.Show(string.Format("Do you want to delete \"{0}\"?",model.ReportName), @"Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ViewModel.Delete(model.Key);
            }
        }

        private void dgvResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            var headerText = dgvResults.Columns[e.ColumnIndex].HeaderText;
            var valueObject = dgvResults.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            switch (headerText)
            {
                case NetworkReportItem.TARGET_PATH:
                case NetworkReportItem.SOURCE_PATH:
                    {
                        var path = valueObject as string;
                        ViewModel.GoToFile(path);
                    }
                    break;
                case NetworkReportItem.SOURCE_NETWORK_MESSAGE:
                    {
                        var path = dgvResults.Rows[e.RowIndex].Cells[NetworkReportItem.SOURCE_PATH].Value as string;
                        ViewModel.GoToFile(path);
                        var iLine = dgvResults.Rows[e.RowIndex].Cells[NetworkReportItem.SOURCE_ILINE].Value as int?;
                        if (!iLine.HasValue)
                            return;
                        ViewModel.GoToLine(iLine.Value);
                    }
                    break;
                case NetworkReportItem.TARGET_NETWORK_MESSAGE:
                    {
                        var path = dgvResults.Rows[e.RowIndex].Cells[NetworkReportItem.TARGET_PATH].Value as string;
                        ViewModel.GoToFile(path);
                        var iLine = dgvResults.Rows[e.RowIndex].Cells[NetworkReportItem.TARGET_ILINE].Value as int?;
                        if (!iLine.HasValue)
                            return;
                        ViewModel.GoToLine(iLine.Value);
                    }
                    break;
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            var reportResult = lbResults.SelectedItem as NetworkReportResultCacheData;
            if (reportResult == null)
                return;

            var view = new RenameView { NameField = reportResult.ReportName };
            if (view.ShowDialog() != DialogResult.OK) return;
            reportResult.ReportName = view.NameField;
            ViewModel.RenderDocument();
        }

        private void btnReRun_Click(object sender, EventArgs e)
        {
            var reportResult = lbResults.SelectedItem as NetworkReportResultCacheData;
            if (reportResult == null)
                return;

            ViewModel.RerunNetworkReport(reportResult);
        }
    }
}
