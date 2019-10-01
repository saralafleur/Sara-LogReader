using System;
using System.Linq;
using Sara.LogReader.Model;
using Sara.LogReader.Model.NetworkReport;
using Sara.WinForm.Notification;

namespace Sara.LogReader.Service.NetworkServices
{
    public static class NetworkReportService
    {
        private static AnalyzeNetworkArgs Args { get; set; }
        
        public static NetworkReportModel GetModel()
        {
            return new NetworkReportModel {Results = XmlDal.CacheModel.NetworkReports};
        }

        public static NetworkReportSelectionModel GetNetworkSelectionModel()
        {
            var model = new NetworkReportSelectionModel
            {
                Files = XmlDal.CacheModel.Files.Select(n=> n.Path).ToList()
            };

            return model;
        }

        public static void RunReport(NetworkReportSelectionModel model, Action<IStatusModel> statusUpdate, out bool cancelled)
        {
            try
            {
                Args = new AnalyzeNetworkArgs
                {
                    StatusUpdate = statusUpdate,
                    Model = model,
                    Report = XmlDal.CacheModel.GetNetworkReport(model.ReportName)
                };

                var tool = new AnalyzeNetworkLoop {Args = Args};
                tool.Execute(out cancelled);
            }
            finally
            {
                statusUpdate(StatusModel.Completed);
            }
        }

        public static void CancelReport()
        {
            Args.IsCancelled = true;
        }

        public static void DeleteReport(string key)
        {
            XmlDal.CacheModel.RemoveNetworkReport(key);
        }
    }
}
