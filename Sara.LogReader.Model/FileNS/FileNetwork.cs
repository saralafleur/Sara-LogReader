using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Sara.LogReader.Model.NetworkMapping;
using Sara.LogReader.Model.Property;

namespace Sara.LogReader.Model.FileNS
{
    public class ValuePair
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class SourceLocator
    {
        public string Path { get; set; }
        // ReSharper disable once InconsistentNaming
        public int iLine { get; set; }
        /// <summary>
        /// Used to indicate which NetworkMap generated the Target
        /// If null then multiple NetworkMaps generated the same Target and a "*" will be displayed
        /// </summary>
        public int? NetworkMapId { get; set; }
    }

    public class NetworkPacketInfo
    {
        public string FilePath { get; set; }
        // ReSharper disable once InconsistentNaming
        public int iLine { get; set; }
        public string NetworkMessageName { get; set; }
        /// <summary>
        /// A List of all Properties for the given Entry
        /// </summary>
        public List<ValuePair> Values { get; set; }
        public NetworkDirection Direction { get; set; }
        public DateTime DateTime { get; set; }
        public string Node { get; set; }

        public NetworkPacketInfo()
        {
            Values = new List<ValuePair>();
        }
        public override string ToString()
        {
            return $"{NetworkHelper.NetworkDirectionToAbbr(Direction)} {NetworkMessageName}";
        }

        /// <summary>
        /// Returns the value that matches the name
        /// If no value is found returns null
        /// </summary>
        public string FindEventValue(string name)
        {
            return (from valuePair in Values
                    where String.Equals(valuePair.Name, name, StringComparison.CurrentCultureIgnoreCase)
                    select valuePair.Value).FirstOrDefault();
        }

        public string FindFileValue(string name)
        {
            var file = XmlDal.CacheModel.GetFile(FilePath);
            return file.FindFileValue(name);
        }
    }

    public class NetworkMessageInfo
    {
        public LineArgs SourceItem
        {
            get
            {
                return new LineArgs
                {
                    iLine = Source.iLine,
                    Path = Source.FilePath
                };
            }

        }
        public NetworkPacketInfo Source { get; set; }
        public List<SourceLocator> InternalTargets { get; set; }

        /// <summary>
        /// Wrapper to InternalTargets.  
        /// InternalTargets is just a reference to the actual detail data.
        /// </summary>
        /// <remarks>
        /// NEVER ACCESS THIS property directly!  Call NetworkMapService.GetNetworkMessagesBySourceLine instead
        /// </remarks>
        [XmlIgnore]
        public List<NetworkMessageInfoModel> Targets
        {
            get
            {
                lock (this)
                {
                    return (from target in InternalTargets
                            from file in XmlDal.CacheModel.Files
                            where file.Path == target.Path
                            from networkMessageInfo in file.Network.NetworkMessages
                            where networkMessageInfo.Source.iLine == target.iLine
                            select new NetworkMessageInfoModel
                            {
                                Item = networkMessageInfo,
                                NetworkMapId = target.NetworkMapId
                            }).ToList();
                }
            }
        }
        /// <summary>
        /// True when the Targets is Cached
        /// TargetsCached are set to false when FileData.NetworkMessagesCached is set to false
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public bool IsCached_Targets { get; set; }

        /// <summary>
        /// True when there are Target Files
        /// </summary>
        public bool HasTargetFiles { get; set; }

        public NetworkMessageInfo()
        {
            HasTargetFiles = false; // Default
            Source = new NetworkPacketInfo();
            InternalTargets = new List<SourceLocator>();
        }

        public override string ToString()
        {
            return Source.ToString();
        }

        /// <summary>
        /// Returns True if the target has the same key values 
        /// </summary>
        public bool Duplicate(NetworkMessageInfo target)
        {
            return (Source.FilePath != target.Source.FilePath &&
                    Source.iLine != target.Source.iLine);
        }
    }

    public class FileNetwork
    {
        public List<NetworkMessageInfo> NetworkMessages { get; set; }
        public List<Node> Nodes { get; set; }

        public FileNetwork()
        {
            NetworkMessages = new List<NetworkMessageInfo>();
            Nodes = new List<Node>();
        }
    }
}
