using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Sara.Common.Extension;
using Sara.LogReader.Model.PatternNS;
using Sara.LogReader.Model.RecipeReport;
using Sara.LogReader.WinForm.Views.NetworkReport;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.ControlsNS;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.RecipeReport
{
    public partial class RecipeReportView : ToolWindow, IViewDock<RecipeReportModel, object>
    {
        protected internal RecipeReportViewModel ViewModel { get; set; }
        public RecipeReportModel Model { get; set; }

        public RecipeReportView()
        {
            InitializeComponent();
        }
        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void Render(RecipeReportModel model)
        {
            // Note: Invoke is handled by ViewModelBase - Sara

            StatusUpdate(StatusModel.Update("Rendering"));
            try
            {
                Model = model;

                lbResults.Items.Clear();
                Model.Results.Reverse();
                foreach (var result in Model.Results)
                {
                    lbResults.Items.Add(result);
                }

                if (lbResults.Items.Count > 0)
                    lbResults.SelectedIndex = 0;

                RenderResults();
            }
            finally
            {
                StatusUpdate(StatusModel.Completed);
            }
        }

        private class ColumnSort
        {
            public string Column { get; set; }
            public int Count { get; set; }
        }
        private List<string> AnomalyColumns { get; set; }
        private List<string> ValueColumns { get; set; } 
        private void RenderResults()
        {
            StatusUpdate(StatusModel.Update("Updating results..."));
            try
            {
                if (lbResults.SelectedItem == null)
                {
                    dataGridView1.DataSource = null;
                    btnDelete.Enabled = false;
                    btnRename.Enabled = false;
                    btnReRun.Enabled = false;
                    return;
                }

                btnDelete.Enabled = true;
                btnRename.Enabled = true;
                btnReRun.Enabled = true;

                var reportResult = lbResults.SelectedItem as RecipeReportResultCacheData;
                if (reportResult == null)
                    return;

                var dt = new DataTable();
                dt.Columns.Add(new DataColumn(RecipeReportItem.DATA, typeof(object)));
                dt.Columns.Add(new DataColumn(RecipeReportItem.RECIPEID, typeof(string)));
                dt.Columns.Add(new DataColumn(RecipeReportItem.PATH, typeof(string)));
                dt.Columns.Add(new DataColumn(RecipeReportItem.GROUP, typeof(string)));
                dt.Columns.Add(new DataColumn(RecipeReportItem.DURATION, typeof(double)));
                dt.Columns.Add(new DataColumn(RecipeReportItem.STARTTIME, typeof(DateTime)));
                dt.Columns.Add(new DataColumn(RecipeReportItem.STARTING_ILINE, typeof(int)));
                dt.Columns.Add(new DataColumn(RecipeReportItem.ENDING_ILINE, typeof(int)));
                dt.Columns.Add(new DataColumn(RecipeReportItem.VALUESCOUNT, typeof(int)));
                dt.Columns.Add(new DataColumn(RecipeReportItem.ANOMALYCOUNT, typeof(int)));


                var sortedAnomalies = AnomalyColumnSorts(reportResult);
                var sortedValues = ValueColumnSorts(reportResult);


                ValueColumns = new List<string>();
                foreach (var column in sortedValues)
                {
                    ValueColumns.Add(column.Column);
                    dt.Columns.Add(new DataColumn(column.Column, typeof(string)));
                }

                AnomalyColumns = new List<string>();
                foreach (var column in sortedAnomalies)
                {
                    AnomalyColumns.Add(column.Column);
                    dt.Columns.Add(new DataColumn(column.Column, typeof(string)));
                }

                foreach (var result in reportResult.Results.OrderBy(n => n.StartTime))
                {
                    var row = new List<object>
                    {
                        result,
                        $"{result.RecipeId}-{result.RecipeName}", 
                        result.Path,
                        result.Group,
                        Math.Round(result.Duration.TotalSeconds,4), 
                        result.StartTime,
                        result.StartingiLine, 
                        result.EndingiLine,
                        result.Values.Count,
                        result.Anomalies.Count
                    };

                    foreach (var sortedCol in sortedValues)
                    {
                        var found = false;
                        foreach (var col in result.Values.Where(col => sortedCol.Column == col.DistinctName))
                        {
                            row.Add(col.Value);
                            found = true;
                            break;
                        }
                        if (found) continue;
                        row.Add("");
                    }

                    foreach (var sortedCol in sortedAnomalies)
                    {
                        var found = false;
                        foreach (var col in result.Anomalies.Where(col => sortedCol.Column == col.DistinctName))
                        {
                            row.Add(col.Value);
                            found = true;
                            break;
                        }
                        if (found) continue;
                        row.Add("");
                    }

                    dt.Rows.Add(row.ToArray());
                }

                dataGridView1.DataSource = dt;
                // Hide Data Column
                var dataGridViewColumn = dataGridView1.Columns[RecipeReportItem.DATA];
                if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
                lblReceiptResultTotal.Text = dt.Rows.Count.ToString(CultureInfo.InvariantCulture);
            }
            finally
            {
                StatusUpdate(StatusModel.Completed);
            }
        }

        private static List<ColumnSort> AnomalyColumnSorts(RecipeReportResultCacheData reportResult)
        {
            // I need to sort the Anomalies Column so that the column with the most values is displayed first
            var columns = (from recipeResult in reportResult.Results
                from col in recipeResult.Anomalies
                select col.DistinctName).Distinct().ToList();
            var aColumns = columns.Select(column => new ColumnSort {Column = column}).ToList();

            foreach (var col in reportResult.Results.SelectMany(row => row.Anomalies))
            {
                foreach (var column in aColumns)
                {
                    if (column.Column == col.DistinctName)
                    {
                        column.Count++;
                        break;
                    }
                }
            }

            var test = from value in aColumns
                orderby value.Count descending
                select value;

            var testIt = test.ToList();
            return testIt;
        }
        private static List<ColumnSort> ValueColumnSorts(RecipeReportResultCacheData reportResult)
        {
            // I need to sort the Anomalies Column so that the column with the most values is displayed first
            var columns = (from recipeResult in reportResult.Results
                           from col in recipeResult.Values
                           select col.DistinctName).Distinct().ToList();
            var aColumns = columns.Select(column => new ColumnSort { Column = column }).ToList();

            foreach (var col in reportResult.Results.SelectMany(row => row.Values))
            {
                foreach (var column in aColumns)
                {
                    if (column.Column == col.DistinctName)
                    {
                        column.Count++;
                        break;
                    }
                }
            }

            var test = from value in aColumns
                       orderby value.Count descending
                       select value;

            var testIt = test.ToList();
            return testIt;
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
            btnCancel.Visible = value;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == RecipeReportItem.PATH)
            {
                var path = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string;
                ViewModel.GoToFile(path);
            }
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == RecipeReportItem.STARTING_ILINE)
            {
                var path = dataGridView1.Rows[e.RowIndex].Cells[RecipeReportItem.PATH].Value as string;
                ViewModel.GoToFile(path);
                var iLine = dataGridView1.Rows[e.RowIndex].Cells[RecipeReportItem.STARTING_ILINE].Value as int?;
                if (!iLine.HasValue)
                    throw new Exception("iLine must always have a value");
                ViewModel.GoToLine(iLine.Value);
            }
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == RecipeReportItem.ENDING_ILINE)
            {
                var path = dataGridView1.Rows[e.RowIndex].Cells[RecipeReportItem.PATH].Value as string;
                ViewModel.GoToFile(path);
                var iLine = dataGridView1.Rows[e.RowIndex].Cells[RecipeReportItem.ENDING_ILINE].Value as int?;
                if (!iLine.HasValue)
                    throw new Exception("iLine must always have a value");
                ViewModel.GoToLine(iLine.Value);
            }
            if (AnomalyColumns.Contains(dataGridView1.Columns[e.ColumnIndex].HeaderText))
            {
                var model = dataGridView1.Rows[e.RowIndex].Cells[RecipeReportItem.DATA].Value as RecipeReportItem;
                if (model == null)
                    throw new Exception("Cell Value must be of type RecipeResult");

                foreach (RecipeColumn anomaly in model.Anomalies)
                {
                    if (anomaly.DistinctName == dataGridView1.Columns[e.ColumnIndex].HeaderText)
                    {
                        var path = dataGridView1.Rows[e.RowIndex].Cells[RecipeReportItem.PATH].Value as string;
                        ViewModel.GoToFile(path);
                        var iLine = anomaly.EndingiLine;
                        ViewModel.GoToLine(iLine);
                    }
                }

            }
        }

        private void btnExportToCsv_Click(object sender, EventArgs e)
        {
            var dt = dataGridView1.DataSource as DataTable;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var model = lbResults.SelectedItem as RecipeReportResultCacheData;
            if (model == null)
                return;

            var result = MessageBox.Show(string.Format("Do you want to delete \"{0}\"?", model.ReportName), @"Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ViewModel.Delete(model.Key);
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            var reportResult = lbResults.SelectedItem as RecipeReportResultCacheData;
            if (reportResult == null)
                return;

            var view = new RenameView { NameField = reportResult.ReportName };
            if (view.ShowDialog() != DialogResult.OK) return;
            reportResult.ReportName = view.NameField;
            ViewModel.RenderDocument();
        }

        private void btnReRun_Click(object sender, EventArgs e)
        {
            var reportResult = lbResults.SelectedItem as RecipeReportResultCacheData;
            if (reportResult == null)
                return;

            ViewModel.RerunNetworkReport(reportResult);
        }

        private string _lastReportName = string.Empty;
        private void lbResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            var reportResult = lbResults.SelectedItem as RecipeReportResultCacheData;
            if (reportResult == null)
                return;

            if (reportResult.ReportName == _lastReportName)
                return;

            _lastReportName = reportResult.ReportName;

            RenderResults();
        }
    }
}
