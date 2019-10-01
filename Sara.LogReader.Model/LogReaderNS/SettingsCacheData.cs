using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Sara.Cache;

namespace Sara.LogReader.Model.LogReaderNS
{
    [XmlType("SettingsCacheData")]
    public class SettingsCacheData : ICacheData
    {
        #region ICacheData
        public bool IsCached { get; set; }
        [XmlIgnore]
        public Action<LoadingStatus, string> LoadStatusNotificationEvent { get; set; }
        public string Key { get; set; }
        [XmlIgnore]
        public CacheDataController DataController { get; set; }
        #endregion

        #region Setup
        public SettingsCacheData()
        {
            CachedSourceTypes = new List<CachedSourceType>();
            Clear();
        }
        public void Clear()
        {
            CachedSourceTypes.Clear();
        }
        public void Load(bool internalLoad)
        {
            // Do nothing on Load
        }
        #endregion

        #region Data
        public List<CachedSourceType> CachedSourceTypes { get; set; }
        public void UpdateCachedSourceType(string type, string path)
        {
            lock (CachedSourceTypes)
            {
                // Remove prior value
                foreach (var c in CachedSourceTypes)
                {
                    var item = c.Files.FirstOrDefault(p => p == path);
                    if (item != null)
                        c.Files.Remove(item);
                }

                var cache = CachedSourceTypes.FirstOrDefault(n => n.Type == type);

                if (cache == null)
                {
                    cache = new CachedSourceType() { Type = type };
                    CachedSourceTypes.Add(cache);
                }

                cache.Files.Add(path);
            }
        }
        #endregion
    }
}
