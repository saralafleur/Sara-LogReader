using System.ComponentModel;

namespace Sara.LogReader.Model.NetworkReport
{
    public class NetworkReportSummary
    {
        public string SourceNode { get; set; }
        public string TargetNode { get; set; }
        public int MessageCount { get; set; }
        [DisplayName("SystemClockDelta_ms")]
        public string SystemClockDelta { get; set; }
        [DisplayName("0 to 499 ms")]
        public int MessageDelta0To500Ms { get; set; }
        [DisplayName("500 to 999 ms")]
        public int MessageDelta500To1000Ms { get; set; }
        [DisplayName("1000 to 1999 ms")]
        public int MessageDelta1000To2000Ms { get; set; }
        [DisplayName(">= 2000 ms")]
        public int MessageDeltaPlus2000Ms { get; set; }
    }
}
