using System.Collections.Generic;
using System.Globalization;
using Sara.LogReader.Model.FileNS;

namespace Sara.LogReader.Model.NetworkMapping
{
    public class NetworkTargets
    {
        public NetworkTargets()
        {
            Targets = new List<NetworkMessageInfoModel>();
        }
        public List<NetworkMessageInfoModel> Targets { get; set; }
        public bool HasTargetFiles { get; set; }
    }

    public class NetworkMessageInfoModel
    {
        public NetworkMessageInfo Item { get; set; }
        public int? NetworkMapId { get; set; }
        public override string ToString()
        {
            return $"{(NetworkMapId == null ? "*" : NetworkMapId.Value.ToString(CultureInfo.InvariantCulture))} {Item}";
        }

    }
}