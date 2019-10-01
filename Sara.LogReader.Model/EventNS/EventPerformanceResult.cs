using System;
using Sara.Common.Extension;

namespace Sara.LogReader.Model.EventNS
{
    public static class EventPerformanceConst
    {
        public const string EventId = "EventId";
        public const string FirstiLine = "FirstiLine";
        public const string DurationTS = "DurationTS";
    }
    public class EventPerformanceResult
    {
        public string Name { get; set; }
        public string Expression { get; set; }
        public TimeSpan DurationTS { get; set; }
        public string Duration { get { return DurationTS.ToReadableString(); } }
        public int Count { get; set; }
        /// <summary>
        /// Used in the View to know which EventPattern to Edit - Sara
        /// </summary>
        public int EventId { get; set; }
        public int FirstiLine { get; set; }
    }
}
