using System;
using System.Collections.Generic;

namespace Sara.LogReader.Model.NetworkReport
{
    public class NetworkReportSelectionModel
    {
        public List<string> Files { get; set; }
        public Action<NetworkReportSelectionModel> Callback { get; set; }
        public string ReportName { get; set; }

        public NetworkReportSelectionModel()
        {
            Files = new List<string>();
        }
    }
}
