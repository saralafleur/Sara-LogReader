using System;
using System.Xml.Serialization;

namespace Sara.LogReader.Model.PatternNS
{
    public class RecipeColumn
    {
        public string DistinctName { get; set; }
        private TimeSpan _duration;

        [XmlIgnore]
        public TimeSpan Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        [XmlElement("Duration")]
        public long DurationTicks
        {
            get { return _duration.Ticks; }
            set { _duration = new TimeSpan(value); }
        }
        /// <summary>
        /// The Index where the EventPattern starts
        /// </summary>
        public int StartingiLine { get; set; }
        public int EndingiLine { get; set; }
        public object Value { get; set; }
    }

}
