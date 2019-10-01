using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Sara.LogReader.Model.FileNS;

namespace Sara.LogReader.Model.NetworkMapping
{
    public class NetworkMapFile
    {
        public FileData File { get; set; }
        public List<int> NetworkMapId { get; set; }

        public NetworkMapFile()
        {
            NetworkMapId = new List<int>();
        }

        private string NetworkMapIdToString()
        {
            return NetworkMapId.Aggregate("", (current, id) => string.IsNullOrEmpty(current) ? id.ToString(CultureInfo.InvariantCulture) : $"{current}, {id}");
        }

        public override string ToString()
        {
            return $"{NetworkMapIdToString()} - {File}";
        }
    }
}