using System.Threading;
using Sara.Common.DateTimeNS;
using Sara.LogReader.Model.NetworkReport;
using Sara.LogReader.Service.NetworkServices;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;
using NetworkReportSelectionView = Sara.LogReader.WinForm.Views.NetworkReport.NetworkReportSelectionView;
using NetworkReportView = Sara.LogReader.WinForm.Views.NetworkReport.NetworkReportView;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class NetworkReportViewModel : ViewModelBase<NetworkReportView, NetworkReportModel, object>, IViewModelBaseNonGeneric
    {
        public NetworkReportViewModel()
        {
            var sw = new Stopwatch("Constructor NetworkReportViewModel");
            View = new NetworkReportView { ViewModel = this };
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }

        public override sealed NetworkReportModel GetModel()
        {
            return NetworkReportService.GetModel();
        }

        public void Run()
        {
            var model = NetworkReportService.GetNetworkSelectionModel();
            model.Callback = RunNetworkReport;
            var window = new NetworkReportSelectionView { ViewModel = this };
            window.Render(model);
            window.ShowDialog();
        }

        private void RunNetworkReport(NetworkReportSelectionModel model)
        {
            View.StatusUpdate(StatusModel.Update("Running Network Report"));
            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    View.EnableCancel(true);
                    bool wasCancelled;
                    NetworkReportService.RunReport(model, View.StatusUpdate, out wasCancelled);
                    if (wasCancelled)
                    {
                        View.ReportCancelled();
                    }
                    RenderDocument();
                }
                finally
                {
                    View.EnableCancel(false);
                    View.StatusUpdate(StatusModel.Completed);
                }
            });
        }

        public void CancelReport()
        {
            NetworkReportService.CancelReport();
        }

        public void GoToFile(string path)
        {
            MainViewModel.GoToFile(path);
        }

        public void GoToLine(int iLine)
        {
            MainViewModel.GoToLine(iLine);
        }

        public void Delete(string key)
        {
            NetworkReportService.DeleteReport(key);

            RenderDocument();
        }

        public void RerunNetworkReport(NetworkReportResultCacheData reportResult)
        {
            RunNetworkReport(new NetworkReportSelectionModel
            {
                Files = reportResult.Files,
                ReportName = string.Format("Re-run of {0}", reportResult.ReportName)
            });
        }
    }
}
