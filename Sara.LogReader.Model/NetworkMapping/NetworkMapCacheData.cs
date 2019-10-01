using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Sara.Cache;

namespace Sara.LogReader.Model.NetworkMapping
{
    [XmlType("NetworkMapCacheData")]
    public class NetworkMapCacheData : ICacheData
    {
        public int LastUniqueNetworkMapId { get; set; }
        public List<NetworkMap> NetworkMaps { get; set; }

        public List<NetworkMap> EnabledNetworkMaps
        {
            get { return NetworkMaps.Where(n => n.Enabled).ToList(); }
        } 

        public NetworkMapCacheData()
        {
            NetworkMaps = new List<NetworkMap>();
            Clear();
        }
        public void Clear()
        {
            LastUniqueNetworkMapId = 0;
            NetworkMaps.Clear();
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
