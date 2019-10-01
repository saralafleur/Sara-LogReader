using System.Collections.Generic;
using Sara.LogReader.Model.FileNS;

namespace Sara.LogReader.Model.NetworkMapNS
{
    public class NetworkMapFile
    {
        public bool Selected { get; set; }
        public NodeBase Node { get; set; }
        public List<string> FilePaths { get; set; }

        public NetworkMapFile()
        {
            FilePaths = new List<string>();
        }

        public override string ToString()
        {
            return Node.ToString();
        }
    }
}
