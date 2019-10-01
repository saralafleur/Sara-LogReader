using System.Data;

namespace Sara.LogReader.Model.NetworkMapNS
{
    public class NetworkMapModel
    {
        public NetworkMapOptions NetworkMap { get; set; }
        public DataTable DataTable { get; set; }
        public string CurrentAnchor { get; set; }
        public NetworkMapFile CurrentAnchorMapFile { get; set; }
    }
}
