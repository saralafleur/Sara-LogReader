using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Sara.Cache;

namespace Sara.LogReader.Model.NetworkReport
{
    public class NetworkReportResultCacheData : ICacheData
    {
        public NetworkReportResultCacheData()
        {
            Results = new List<NetworkReportItem>();
            Summary = new List<NetworkReportSummary>();
        }

        public void Clear()
        {
            Results.Clear();
        }

        public bool IsCached { get; set; }
        [XmlIgnore]
        public Action<LoadingStatus, string> LoadStatusNotificationEvent { get; set; }
        public string Key { get; set; }
        [XmlIgnore]
        public CacheDataController DataController { get; set; }

        public string ReportName
        {
            get { return Key; }
            set { Key = value; }
        }

        public List<NetworkReportSummary> Summary { get; set; } 

        public List<NetworkReportItem> Results { get; set; }
        public List<string> Files { get; set; }

        public void Load(bool internalLoad)
        {
            // Do nothing at this point - Sara
        }

        public override string ToString()
        {
            return Key;
        }

    }
}
