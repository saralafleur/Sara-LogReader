using System;
using System.Linq;
using System.Reflection;
using Sara.Common.Threading;
using Sara.Logging;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.NetworkReport;

namespace Sara.LogReader.Service.NetworkServices
{
    public class AnalyzeNetworkMessageLoop : ThreadedLoop<NetworkMessageInfo>
    {
        protected override void RunItem(NetworkMessageInfo message)
        {
            var test = NetworkMapService.GetNetworkMessagesBySourceLine(message.SourceItem);

            var targetPath = string.Empty;
            var targetNetworkMessage = string.Empty;
            var targetDateTime = new DateTime();
            var targetiLine = 0;
            if (test.Targets.Count == 1)
            {
                targetPath = test.Targets[0].Item.Source.FilePath;
                targetNetworkMessage = test.Targets[0].Item.Source.NetworkMessageName;
                targetDateTime = test.Targets[0].Item.Source.DateTime;
                targetiLine = test.Targets[0].Item.Source.iLine;
            }

            var item = new NetworkReportItem
            {
                SourceNode = message.Source.Node,
                SourcePath = SourcePath,
                SourceNetworkMessage = message.Source.NetworkMessageName,
                SourceiLine = message.Source.iLine,
                SourceDateTime = message.Source.DateTime,
                TargetNode = test.Targets.Count == 1 ? test.Targets[0].Item.Source.Node : "",
                TargetPath = targetPath,
                TargetNetworkMessage = targetNetworkMessage,
                TargetiLine = targetiLine,
                TargetDateTime = targetDateTime,
                TargetMatches = test.Targets.Count()
            };
            Report.Results.Add(item);

            lock (Report.Summary)
            {
                var summary = Report.Summary.SingleOrDefault(n => n.SourceNode == item.SourceNode &&
                                                                  n.TargetNode == item.TargetNode);

                if (summary != null)
                {
                    summary.MessageCount++;
                }
                else
                {
                    Report.Summary.Add(new NetworkReportSummary
                    {
                        SourceNode = item.SourceNode,
                        TargetNode = item.TargetNode,
                        MessageCount = 1
                    });
                }
            }
        }

        protected override void ProgressUpdate(int current, int total)
        {
            if (current % 50 == 0)
            {
                Log.Write($"Processing {current} of {total}", typeof(AnalyzeNetworkLoop).FullName, MethodBase.GetCurrentMethod().Name, LogEntryType.Warning);
            }

        }

        public NetworkReportResultCacheData Report { get; set; }
        public string SourcePath { get; set; }
    }
}
