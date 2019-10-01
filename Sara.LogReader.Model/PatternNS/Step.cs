using System.Collections.Generic;

namespace Sara.LogReader.Model.PatternNS
{
    public class Step : IEventPatternBase
    {
        public Step()
        {
            Values = new List<string>();
        }
        public EventType EventType { get; set; }
        public EventOption Options { get; set; }
        public List<string> Values { get; set; }
        public int EventId { get; set; }
        public string EventText { get; set; }
        public override string ToString()
        {
            return $"{EventType} {Options} - {EventText}";
        }

    }
}
