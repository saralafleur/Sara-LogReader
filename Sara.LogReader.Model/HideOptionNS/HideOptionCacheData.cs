using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Sara.Cache;

namespace Sara.LogReader.Model.HideOptionNS
{
    [XmlType("HideOptionCacheData")]
    public class HideOptionCacheData : ICacheData
    {
        public List<HideOption> Options { get; set; }
        public int LastUniqueHideOptionId { get; set; }

        public HideOptionCacheData()
        {
            Options = new List<HideOption>();
            Clear();
        }

        public void Clear()
        {
            Options.Clear();
            LastUniqueHideOptionId = 0;
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
