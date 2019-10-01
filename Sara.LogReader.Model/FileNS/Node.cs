using System;
using Sara.Common.Extension;

namespace Sara.LogReader.Model.FileNS
{
    public enum NodeSource
    {
        Local,
        Remote
    }


    public class NodeBase
    {
        public NodeBase()
        {
            if (SourceEnd < SourceStart)
                throw new DateTimeExt.InvalidDateRangeException();
        }
        protected bool Equals(NodeBase other)
        {
            return string.Equals(Ip, other.Ip) && string.Equals(Port, other.Port) && string.Equals(Host, other.Host) && string.Equals(SourceType, other.SourceType) && SourceStart.Equals(other.SourceStart) && SourceEnd.Equals(other.SourceEnd);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Ip != null ? Ip.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Port != null ? Port.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Host != null ? Host.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (SourceType != null ? SourceType.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ SourceStart.GetHashCode();
                hashCode = (hashCode*397) ^ SourceEnd.GetHashCode();
                return hashCode;
            }
        }

        public string Ip { get; set; }
        public string Port { get; set; }
        public string Host { get; set; }
        public string SourceType { get; set; }
        /// <summary>
        /// The Source FileData Start Time
        /// </summary>
        public DateTime SourceStart { get; set; }
        /// <summary>
        /// The Source FileData End Time
        /// </summary>
        public DateTime SourceEnd { get; set; }
        public static string FormatIpAndHostHame(string ip, string hostName)
        {
            if (!string.IsNullOrEmpty(hostName) && hostName.Trim() != string.Empty)
                return $"{ip} - {hostName}";
            return $"{ip}";
        }

        public static string NodeToString(string sourceType, string ip, string host)
        {
            return $"{sourceType} : {ip}{(!string.IsNullOrEmpty(host) ? " (" + host + ")" : "")}";
        }
        public override string ToString()
        {
            return NodeToString(SourceType, Ip, Host);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((NodeBase) obj);
        }
    }

    /// <summary>
    /// Represents a Node on the Network
    /// </summary>
    public class Node : NodeBase
    {
        public NodeSource Source { get; set; }
        /// <summary>
        /// The first occurance of the Node in the Log
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int iLine { get; set; }

    }
}