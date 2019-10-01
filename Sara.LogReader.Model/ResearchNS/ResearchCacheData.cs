using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Sara.Cache;

namespace Sara.LogReader.Model.ResearchNS
{
    [XmlType("ResearchCacheData")]
    public class ResearchCacheData : ICacheData
    {
        public int LastUniqueId { get; set; }
        public List<Research> Items { get; set; }

        public ResearchCacheData()
        {
            Items = new List<Research>();
            Clear();
        }
        public void Clear()
        {
            LastUniqueId = 0;
            Items.Clear();
        }

        public bool IsCached { get; set; }

        [XmlIgnore]
        public Action<LoadingStatus, string> LoadStatusNotificationEvent { get; set; }
        public string Key { get; set; }
        [XmlIgnore]
        public CacheDataController DataController { get; set; }

        public void Load(bool internalLoad)
        {
            // Do nothing
        }
    }
}
