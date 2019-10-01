using System;
using System.Xml.Serialization;

namespace Sara.LogReader.Model.NetworkReport
{
    public class NetworkReportItem
    {
        public const string DATETIME_DIFF = "DateTimeDifference";
        public const string SYSTEM_CLOCK_DIFF = "SystemClockDifference";
        public const string SOURCE_PATH = "SourcePath";
        public const string SOURCE_DATETIME = "SourceDateTime";
        public const string SOURCE_NETWORK_MESSAGE = "SourceNetworkMessage";
        public const string SOURCE_ILINE = "SourceiLine";
        public const string TARGET_PATH = "TargetPath";
        public const string TARGET_DATETIME = "TargetDateTime";
        public const string TARGET_NETWORK_MESSAGE = "TargetNetworkMessage";
        public const string TARGET_ILINE = "TaretiLine";
        public const string TARGET_MATCHES = "TargetMatches";
        public const string DATETIME_DIFFERENCE = "DateTimeDifference";

        public string SourcePath { get; set; }
        public DateTime SourceDateTime { get; set; }
        public string SourceNetworkMessage { get; set; }
        public int SourceiLine { get; set; }
        public string TargetPath { get; set; }
        public DateTime TargetDateTime { get; set; }
        public string TargetNetworkMessage { get; set; }
        public int TargetiLine { get; set; }
        public int TargetMatches { get; set; }
        [XmlIgnore]
        public double DateTimeDifference
        {
            get
            {
                return string.IsNullOrEmpty(TargetNode) ? 0 : (SourceDateTime - TargetDateTime).TotalMilliseconds;
            }
        }

        public string SourceNode { get; set; }
        public string TargetNode { get; set; }
        public double SystemClockDelta { get; set; }
        [XmlIgnore]
        public double SystemClockDifference
        {
            get
            {
                return string.IsNullOrEmpty(TargetNode) ? 0 : (DateTimeDifference - SystemClockDelta);
            }
        }
    }
}
