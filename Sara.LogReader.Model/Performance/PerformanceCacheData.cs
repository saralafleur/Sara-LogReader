using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sara.LogReader.Model.Performance
{
    [XmlType("PerformanceCacheData")]
    public class PerformanceData
    {
        public int LastUniqueId { get; set; }
        public List<PerformanceEvent> Items { get; set; }

        public PerformanceData()
        {
            Items = new List<PerformanceEvent>();
            Clear();
        }
        public void Clear()
        {
            LastUniqueId = 0;
            Items.Clear();
        }
    }
}
