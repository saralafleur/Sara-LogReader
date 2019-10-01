using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Sara.Cache;

namespace Sara.LogReader.Model.Categories
{
    [XmlType("CategoryCacheData")]
    public class CategoryCacheData : ICacheData
    {
        public List<Category> Categories { get; set; }
        public int LastUniqueCategoryId { get; set; }

        public CategoryCacheData()
        {
            Categories = new List<Category>();
            Clear();
        }

        public void Clear()
        {
            Categories.Clear();
            LastUniqueCategoryId = 0;
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
