using System.Collections.Generic;

namespace Sara.LogReader.Model.NetworkMapNS
{
    public class NetworkMapOptions
    {
        /// <summary>
        /// When True the Network Map will not update when you change which Document you are viewing
        /// </summary>
        public bool Anchored { get; set; }
        public string AnchorIp { get; set; }
        public string AnchorFilePath { get; set; }
        public List<NetworkMapFile> Nodes { get; set; }

        public NetworkMapOptions()
        {
            Nodes = new List<NetworkMapFile>();
        }

        public void Clear()
        {
            Anchored = false;
            Nodes.Clear();
        }
    }
}
