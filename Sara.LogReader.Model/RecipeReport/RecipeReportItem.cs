using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Sara.LogReader.Model.PatternNS;

namespace Sara.LogReader.Model.RecipeReport
{
    public class RecipeReportItem
    {
        public const string STARTING_ILINE = "StartingiLine";
        public const string ENDING_ILINE = "EndingiLine";
        public const string GROUP = "Group";
        public const string PATH = "Path";
        public const string DURATION = "Duration";
        public const string STARTTIME = "StartTime";
        public const string RECIPEID = "RecipeId";
        public const string DATA = "xDATAx";
        public const string ANOMALYCOUNT = "Anomalies";
        public const string VALUESCOUNT = "Values";

        public RecipeReportItem()
        {
            Anomalies = new List<RecipeColumn>();           
            Values = new List<RecipeColumn>(); 
        }

        /// <summary>
        /// The Index where the EventPattern starts
        /// </summary>
        public int StartingiLine { get; set; }
        public int EndingiLine { get; set; }
        public int RecipeId { get; set; }
        public string Path { get; set; }
        public string Group { get; set; }
        private TimeSpan _duration;
        [XmlIgnore]
        public TimeSpan Duration { get { return _duration; } set { _duration = value; } }
        [XmlElement("Duration")]
        public long DurationTicks { get { return _duration.Ticks; } set { _duration = new TimeSpan(value); } }

        public DateTime? StartTime { get; set; }

        public string RecipeName { get; set; }

        public List<RecipeColumn> Anomalies { get; set; }
        public List<RecipeColumn> Values { get; set; } 
    }
}
