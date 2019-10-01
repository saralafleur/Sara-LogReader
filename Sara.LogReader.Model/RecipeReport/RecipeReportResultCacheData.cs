using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Sara.Cache;
using Sara.LogReader.Model.PatternNS;

namespace Sara.LogReader.Model.RecipeReport
{
    public class RecipeReportResultCacheData : ICacheData
    {
        public RecipeReportResultCacheData()
        {
            Results = new List<RecipeReportItem>();
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

        public List<RecipeReportItem> Results { get; set; }
        public List<string> Files { get; set; }
        public string ReportName
        {
            get { return Key; }
            set { Key = value; }
        }

        public List<RecipeSelection> Recipes { get; set; }

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
