using System.Collections.Generic;

namespace Sara.LogReader.Model.NetworkReport
{
    public class NetworkReportModel
    {
        public List<NetworkReportResultCacheData> Results { get; set; }

        public NetworkReportModel()
        {
            Results = new List<NetworkReportResultCacheData>();
        }
    }
}
