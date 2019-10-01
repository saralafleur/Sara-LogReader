using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Sara.Common.DateTimeNS;
using Sara.Common.Threading;
using Sara.Logging;
using Sara.LogReader.Model;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.NetworkReport;
using Sara.WinForm.Notification;

namespace Sara.LogReader.Service.NetworkServices
{
    public class AnalyzeNetworkArgs
    {
        public Action<IStatusModel> StatusUpdate { get; set; }
        public NetworkReportSelectionModel Model { get; set; }
        public NetworkReportResultCacheData Report { get; set; }
        public volatile bool IsCancelled;
    }

    public class DeltaItem
    {
        public double Delta { get; set; }
    }

    public class AnalyzeNetworkLoop : ThreadedLoop<string>
    {
        private const string TITLE = "Network Report";

        public AnalyzeNetworkArgs Args { get; set; }

        protected override void RunItem(string path)
        {
            if (Args.IsCancelled)
            {
                Cancel();
                return;
            }

            var stopwatch = new Stopwatch(MethodBase.GetCurrentMethod().Name, true);
            try
            {
                Log.WriteWarning("---",typeof(AnalyzeNetworkLoop).FullName, MethodBase.GetCurrentMethod().Name);
                Log.WriteWarning($"Running AnalyzeNetwork for file \"{path}\"",typeof(AnalyzeNetworkLoop).FullName, MethodBase.GetCurrentMethod().Name);
                var file = XmlDal.CacheModel.GetFile(path);
                Log.WriteWarning("BuildNetwork on Source",typeof(AnalyzeNetworkLoop).FullName, MethodBase.GetCurrentMethod().Name);
                NetworkService.CheckNetworkMessages(file, null, "");

                Log.WriteWarning($"{file.Network.NetworkMessages.Count} Network Messages to process...", typeof(AnalyzeNetworkLoop).FullName, MethodBase.GetCurrentMethod().Name);

                lock (file.Network)
                {
                    var loop = new AnalyzeNetworkMessageLoop
                    {
                        Queue = new Queue<NetworkMessageInfo>(file.Network.NetworkMessages),
                        Report = Args.Report,
                        SourcePath = file.Path
                    };
                    loop.Run();
                }
            }
            finally
            {
                Log.WriteWarning($"Completed AnalyzeNetwork for file \"{path}\"",typeof(AnalyzeNetworkLoop).FullName, MethodBase.GetCurrentMethod().Name);
                stopwatch.Stop();
            }
        }

        private void Cancel()
        {
            Cancelled = true;
            Log.WriteWarning(@"
*****
** Cancelling...
*****",typeof(AnalyzeNetworkLoop).FullName, MethodBase.GetCurrentMethod().Name);

        }

        private void GenerateSummary()
        {
            Log.WriteWarning("Generating Summary...",typeof(AnalyzeNetworkLoop).FullName, MethodBase.GetCurrentMethod().Name);

            foreach (var summary in Args.Report.Summary)
            {
                if (string.IsNullOrEmpty(summary.SourceNode) ||
                    string.IsNullOrEmpty(summary.TargetNode))
                {
                    summary.SystemClockDelta = "N/A";
                    continue;
                }

                var sorted = new List<DeltaItem>();

                var summary1 = summary;
                lock (Args.Report)
                {
                    sorted.AddRange(
                        Args.Report.Results.Where(
                            detail => detail.SourceNode == summary1.SourceNode && detail.TargetNode == summary1.TargetNode)
                            .Select(detail => new DeltaItem { Delta = detail.DateTimeDifference }));
                }
                sorted = sorted.OrderBy(n => n.Delta).ToList();

                var count = sorted.Count();
                var third = count * .33;

                // Only look at 1/3 of the list
                var thirdList = new List<DeltaItem>();
                for (var i = 0; i < count - 1; i++)
                {
                    if (i < third)
                        continue;
                    if (i > third * 3)
                        continue;

                    thirdList.Add(new DeltaItem
                    {
                        Delta = sorted[i].Delta
                    });
                }

                double systemDelta;
                if (thirdList.Count == 0)
                {
                    summary.SystemClockDelta = "N/A";
                    systemDelta = 0;
                }
                else
                {
                    systemDelta = Math.Ceiling(thirdList.Average(n => n.Delta));
                    summary.SystemClockDelta = systemDelta.ToString(CultureInfo.InvariantCulture);
                    var summary2 = summary;
                    foreach (
                        var item in
                            Args.Report.Results.Where(
                                n => n.SourceNode == summary2.SourceNode && n.TargetNode == summary2.TargetNode)
                        )
                    {
                        item.SystemClockDelta = systemDelta;
                    }
                }
                summary.MessageDelta0To500Ms = sorted.Count(n => Math.Abs(n.Delta - systemDelta) < 500);
                summary.MessageDelta500To1000Ms = sorted.Count(n => Math.Abs(n.Delta - systemDelta) >= 500 &&
                                                                    Math.Abs(n.Delta - systemDelta) < 1000);
                summary.MessageDelta1000To2000Ms = sorted.Count(n => Math.Abs(n.Delta - systemDelta) >= 1000 &&
                                                                     Math.Abs(n.Delta - systemDelta) < 2000);
                summary.MessageDeltaPlus2000Ms = sorted.Count(n => Math.Abs(n.Delta - systemDelta) >= 2000);
            }
            Log.WriteWarning("Summary Complete",typeof(AnalyzeNetworkLoop).FullName, MethodBase.GetCurrentMethod().Name);
        }

        private string _priorStatus = string.Empty;
        protected override void ProgressUpdate(int current, int total)
        {
            var status = $@"Analyzing {current} of {total}";
            if (_priorStatus == status)
                return;

            _priorStatus = status;

            Args.StatusUpdate(StatusModel.Update(TITLE, status, total, current));
        }

        public void Execute(out bool cancelled)
        {
            Log.WriteWarning("Running AnalyzeNetwork...",typeof(AnalyzeNetworkLoop).FullName, MethodBase.GetCurrentMethod().Name);
            Args.StatusUpdate(StatusModel.Update(TITLE, ""));
            try
            {
                Args.IsCancelled = false;

                Args.Report.Files = Args.Model.Files;

                Queue = new Queue<string>(Args.Model.Files);

                WorkerLimit = 10;

                Args.StatusUpdate(StatusModel.StartStopWatch);
                Args.StatusUpdate(StatusModel.ShowEstimatedTime);

                Run();

                GenerateSummary();

                Args.StatusUpdate(StatusModel.StopStopWatch);

                if (!Args.IsCancelled)
                    XmlDal.CacheModel.Save();

                cancelled = Args.IsCancelled;
            }
            finally
            {
                Args.StatusUpdate(StatusModel.Completed);
            }
        }
    }
}
