using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Sara.Cache;

namespace Sara.LogReader.Model.FileNS
{
    [XmlType("FavoriteFileCache")]
    public class FavoriteFileCache : ICacheData
    {
        public List<FavoriteFile> Files { get; set; }
        public FavoriteFileCache()
        {
            Files = new List<FavoriteFile>();
            Clear();
        }
        public void Clear()
        {
            Files.Clear();
        }

        public bool IsCached { get; set; }

        [XmlIgnore]
        public Action<LoadingStatus, string> LoadStatusNotificationEvent { get; set; }
        public string Key { get; set; }
        [XmlIgnore]
        public CacheDataController DataController { get; set; }

        public void Load(bool internalLoad)
        {
            // Do nothing on Load
        }
    }
}
