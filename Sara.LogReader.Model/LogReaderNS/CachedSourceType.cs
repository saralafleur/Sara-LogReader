using System.Collections.Generic;

namespace Sara.LogReader.Model.LogReaderNS
{
    public class CachedSourceType
    {
        public string Type { get; set; }
        public List<string> Files { get; set; }

        public CachedSourceType()
        {
            Files = new List<string>();
        }
    }
}
