using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Sara.Cache;

namespace Sara.LogReader.Model.PatternNS
{
    [XmlType("PatternCacheData")]
    public class PatternCacheData : ICacheData
    {
        public List<Pattern> Patterns { get; set; }
        public int LastUniquePatternId { get; set; }

        public PatternCacheData()
        {
            Patterns = new List<Pattern>();
            Clear();
        }

        public void Clear()
        {
            Patterns.Clear();
            LastUniquePatternId = 0;
        }

        public bool IsCached { get; set; }

        [XmlIgnore]
        public Action<LoadingStatus, string> LoadStatusNotificationEvent { get; set; }
        public string Key { get; set; }
        [XmlIgnore]
        public CacheDataController DataController { get; set; }

        public void Load(bool internalLoad)
        {
            // Do Nothing
        }
    }
}
