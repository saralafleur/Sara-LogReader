using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Sara.Cache;

namespace Sara.LogReader.Model.ValueNS
{
    [XmlType("ValueCacheData")]
    public class ValueCacheData : ICacheData
    {
        public int LastUniqueValueId { get; set; }
        public List<Value> Values { get; set; }

        public ValueCacheData()
        {
            Values = new List<Value>();
            Clear();
        }
        public void Clear()
        {
            LastUniqueValueId = 0;
            Values.Clear();
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
