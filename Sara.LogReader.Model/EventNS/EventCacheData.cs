using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Sara.Cache;

namespace Sara.LogReader.Model.EventNS
{
    [XmlType("EventCacheData")]
    public class EventCacheData : ICacheData
    {
        public int LastUniqueEventId { get; set; }
        public List<EventLR> Events { get; set; }

        public EventCacheData()
        {
            Events = new List<EventLR>();
            Clear();
        }
        public void Clear()
        {
            LastUniqueEventId = 0;
            Events.Clear();
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
