using System;

namespace Sara.LogReader.Model.PatternNS
{
    public class ScanLineArgs
    {
        /// <summary>
        /// TimeStamp when the entry was Scanned
        /// </summary>
        public DateTime TimeStamp { get; set; }
        public string Line { get; set; }
        public int iLine { get; set; }
        public bool LastLine { get; set; }
        public string SourceType { get; set; }
    }
}
