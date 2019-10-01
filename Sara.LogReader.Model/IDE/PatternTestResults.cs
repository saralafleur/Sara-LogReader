using System;
using System.Collections.Generic;
using System.Linq;
using Sara.Common.Extension;
using Sara.LogReader.Model.PatternNS;

namespace Sara.LogReader.Model.IDE
{
    public enum ValueType
    {
        TimeTo,
        TimeToOr,
        TimeFrom,
        TimeToNext,
        TimeToNextKnownIdle,
        Value
    }
    public enum FrequencyType
    {
        PerPattern,
        PerFile
    }
    public enum CleanType
    {
        Unexpected,
        Duration,
        KnownIdle,
        UnknownIdle
    }

    public class NameValue
    {
        public NameValue()
        {
            BaselineMin = new TimeSpan();
            BaselineMax = new TimeSpan();
        }
        public string Name { get; set; }
        public object Value { get; set; }
        public ValueType Type { get; set; }
        public Guid StartIdentifier { get; set; }
        public Guid StopIdentifier { get; set; }
        public TimeSpan BaselineMin { get; set; }
        public TimeSpan BaselineMax { get; set; }
        internal NameValue Clone()
        {
            var model = new NameValue()
            {
                Name = Name,
                Value = Value,
                Type = Type,
                StartIdentifier = StartIdentifier,
                StopIdentifier = StopIdentifier,
                BaselineMin = BaselineMin,
                BaselineMax = BaselineMax
            };
            return model;
        }
    }
    public class DurationColumn
    {
        public DurationColumn()
        {
            DurationOutsideOfDuration = new TimeSpan();
            DurationInsideOfDuration = new TimeSpan();
        }
        public int Column { get; set; }
        public Guid Start { get; set; }
        public Guid Stop { get; set; }
        public TimeSpan DurationOutsideOfDuration { get; set; }
        public TimeSpan DurationInsideOfDuration { get; set; }
    }
    public class PatternTestResultEventPattern : IEventPatternIdentifier
    {
        public Guid Identifier { get; private set; }

        public string EventTag { get; set; }

        private string _name;
        public string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(_name))
                    return _name;

                _name = EventTag;
                if (Options.Name)
                    _name = Options.NameParameter;
                return _name;
            }
        }

        public PatternTestResultEventPattern(IEventPattern e, int piLine)
        {
            Initialize(e.EventTag, e.EventType, e.Path, piLine, piLine, e.Line, e.DateTime, new TimeSpan());
            Options = e.Options;
            Identifier = e.Identifier;
            Duration = e.Duration;
            DurationStopIdentifiers = e.DurationStopIdentifiers;
        }
        public PatternTestResultEventPattern(string eventTag,
                                      EventType eventType,
                                      string path,
                                      int startiLine,
                                      int endiLine,
                                      string line,
                                      DateTime? dateTime,
                                      TimeSpan duration)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("Path cannot be null or empty!");

            Initialize(eventTag, eventType, path, startiLine, endiLine, line, dateTime, duration);
            Identifier = new Guid();

        }
        private void Initialize(string eventTag, EventType eventType, string path, int startiLine, int endiLine,
                                string line, DateTime? dateTime, TimeSpan duration)
        {
            Values = new List<NameValue>();
            Options = new EventOption();

            EventTag = eventTag;
            EventType = eventType;
            DateTime = dateTime;
            Path = path;
            StartiLine = startiLine;
            EndiLine = endiLine;
            Line = line;
            switch (eventType)
            {
                case EventType.GapOutside:
                    IdleTimeOutsideOfDuration = duration;
                    break;
                case EventType.GapInside:
                    IdleTimeInsideOfDuration = duration;
                    break;
                case EventType.Duration:
                    Duration = duration;
                    break;
            }
            DurationStopIdentifiers = new List<Guid>();
            DurationPatternForExcel = new List<bool>();
            Duration = duration;
            ChildEvents = new List<PatternTestResultEventPattern>();
        }

        public EventType EventType { get; private set; }
        public int StartiLine { get; private set; }
        public int EndiLine { get; private set; }
        /// <summary>
        /// When an EventPattern is tagged with 'TimeFrom', 'TimeToNext', and 'TimeTo' 
        /// the duration between these two events is populated into the Duration field.
        /// </summary>
        /// <example>
        /// ---- Duration
        /// ==== Idle Time
        /// ----====----====
        /// </example>
        public TimeSpan Duration { get; set; }
        public Guid DurationStartIdentifier { get; set; }
        public Guid DurationStopIdentifier { get; set; }
        /// <summary>
        /// When 2 or more durations overlap, the left most duration will total the DurationOutsideOfDuration time.
        /// The durations to the right will total their time into DurationInsideOfDuration
        /// If you add DurationOutsideOfDuration with IdleTimeOutsideOfDuration it should equal Total Duration.
        /// </example>
        public TimeSpan DurationOutsideOfDuration { get; set; }
        /// <summary>
        /// Total time within a Duration that is inside of another Duration
        /// </example>
        public TimeSpan DurationInsideOfDuration { get; set; }
        /// <summary>
        /// IdleTime outside of a Duration
        /// </summary>
        /// <example>
        /// ---- Duration
        /// ==== Idle Time
        /// ------====----====
        ///       ^ IdleTimeOutsideOfDuration 
        /// </example>
        public TimeSpan IdleTimeOutsideOfDuration { get; set; }
        /// <summary>
        /// IdleTime within a Duration.  A Duration will include the time from the
        /// start eventPattern to the stop eventPattern.  This can include Idle Time as well, thus 
        /// this field.
        /// </summary>
        /// <example>
        /// ---- Duration
        /// ==== Idle Time
        /// ------====----====
        ///   ==
        ///   ^ IdleTimeInsideOfDuration 
        /// </example>
        public TimeSpan IdleTimeInsideOfDuration { get; set; }
        public List<NameValue> Values { get; set; }
        public EventOption Options { get; private set; }
        public string Line { get; private set; }
        public DateTime? DateTime { get; private set; }
        public List<Guid> DurationStopIdentifiers { get; set; }
        /// <summary>
        /// Contains a mapping of the Durations for a given session that will be represented in Excel
        /// </summary>
        public List<bool> DurationPatternForExcel { get; set; }
        public bool WithinBaseline { get; set; }
        public bool OutsideBaseline { get; set; }
        public bool NoBaseline { get; set; }
        public string EventName { get; set; }
        public string Path { get; set; }
        public TimeSpan BaselineMin { get; set; }
        public TimeSpan BaselineMax { get; set; }
        public List<PatternTestResultEventPattern> ChildEvents { get; set; }

        public PatternTestResultEventPattern Clone()
        {
            var model = new PatternTestResultEventPattern(EventTag, EventType, Path, StartiLine, EndiLine, Line, DateTime, new TimeSpan())
            {
                Duration = new TimeSpan(Duration.Ticks),
                IdleTimeOutsideOfDuration = new TimeSpan(IdleTimeOutsideOfDuration.Ticks),
                Options = Options.Clone(),
                DurationStopIdentifiers = DurationStopIdentifiers,
                ChildEvents = new List<PatternTestResultEventPattern>(ChildEvents)
            };
            foreach (var item in Values)
                model.Values.Add(item.Clone());

            return model;
        }
        public override string ToString()
        {
            return Name;
        }
    }

    public class PatternTestResult
    {
        public PatternTestResult()
        {
            Events = new List<PatternTestResultEventPattern>();
            Warnings = new List<string>();
            Options = new PatternOptions();
            _identifier = Guid.NewGuid();
        }
        private Guid _identifier;
        public Guid Identifier { get { return _identifier; } }
        public PatternOptions Options { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string PatternName { get; set; }
        public int StartiLine { get; set; }
        public int EndiLine { get; set; }
        public List<PatternTestResultEventPattern> Events { get; set; }
        public TimeSpan TotalDuration { get; set; }
        public List<string> Warnings { get; set; }

        internal PatternTestResult Clone()
        {
            throw new NotImplementedException();
        }
    }

    public class CleanAttribute
    {
        public CleanAttribute()
        {
            Events = new List<PatternTestResultEventPattern>();
        }
        public string EventName { get; set; }
        public int Count { get { return Events.Count; } }
        public List<PatternTestResultEventPattern> Events { get; set; }

        internal CleanAttribute Clone()
        {
            var clone = new CleanAttribute()
            {
                EventName = EventName
            };

            foreach (var item in Events)
                clone.Events.Add(item.Clone());

            return clone;
        }

        public string ToString(int maxLength)
        {
            var _temp = "";

            if (Count == 0)
                _temp = $"Clean";
            else
                _temp = $"{Count.ToString().ToFixedColumnRight(5)}";

            return $"{EventName.ToFixedColumnRight(maxLength)} - {_temp}";
        }
    }

    public class Frequency : CleanAttribute
    {
        public int iline;

        public string Name { get { return $"{EventName} { FrequencyType.ToString().PadRight(10)}"; } }
        /// <summary>
        /// Note: PerFile will group by EventName and Path
        ///       PerPattern will group by EventName only
        /// </summary>
        public string Path { get; set; }
        public FrequencyType FrequencyType { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        /// <summary>
        /// Returns True if Count is within Min and Max
        /// </summary>
        public bool Clean
        {
            get
            {
                return (Count >= Min && Count <= Max);
            }
        }

        public Guid PatternIdentifier { get; set; }
        internal new Frequency Clone()
        {
            var clone = new Frequency()
            {
                EventName = EventName
            };

            foreach (var item in Events)
                clone.Events.Add(item.Clone());

            clone.Path = Path;
            clone.FrequencyType = FrequencyType;
            clone.Min = Min;
            clone.Max = Max;
            return clone;
        }
        public new string ToString(int maxLength)
        {
            var _temp = string.Empty;
            if (Count == 0)
                _temp = $"Clean";
            else
                _temp = $"{Count.ToString().ToFixedColumnRight(5)}";

            return $"{Name.ToFixedColumnRight(maxLength)} {Min.ToString().ToFixedColumnRight(3)} to {Max.ToString().ToFixedColumnRight(3)} - {_temp}";
        }
    }

    public class Duration : CleanAttribute
    {
        public TimeSpan Min { get; set; }
        public TimeSpan Max { get; set; }
        /// <summary>
        /// Returns True if Count is within Min and Max
        /// </summary>
        public bool Clean
        {
            get
            {
                return (Count == 0);
            }
        }
        internal new Duration Clone()
        {
            var clone = new Duration()
            {
                EventName = EventName
            };

            foreach (var item in Events)
                clone.Events.Add(item.Clone());

            clone.Min = Min;
            clone.Max = Max;
            return clone;
        }
        public new string ToString(int maxLength)
        {
            var _temp = "";

            if (Count == 0)
                _temp = $"Clean";
            else
            {
                var w = 4;
                var i5 = Events.Count(n => n.Duration < new TimeSpan(0,0,5));
                var i5to15 = Events.Count(n => n.Duration >= new TimeSpan(0, 0, 5) && n.Duration < new TimeSpan(0,0,15));
                var i15to30 = Events.Count(n => n.Duration >= new TimeSpan(0, 0, 15) && n.Duration < new TimeSpan(0, 0, 30));
                var i30to60 = Events.Count(n => n.Duration >= new TimeSpan(0, 0, 30) && n.Duration < new TimeSpan(0, 1, 0));
                var i60 = Events.Count(n => n.Duration >= new TimeSpan(0, 1, 0));
                var s5 = i5 == 0 ? "".ToFixedColumnRight(w) : i5.ToString().ToFixedColumnRight(w);
                var s5to15 = i5to15 == 0 ? "".ToFixedColumnRight(w) : i5to15.ToString().ToFixedColumnRight(w);
                var s15to30 = i15to30 == 0 ? "".ToFixedColumnRight(w) : i15to30.ToString().ToFixedColumnRight(w);
                var s30to60 = i30to60 == 0 ? "".ToFixedColumnRight(w) : i30to60.ToString().ToFixedColumnRight(w);
                var s60 = i60 == 0 ? "".ToFixedColumnRight(w) : i60.ToString().ToFixedColumnRight(w);

                _temp = $"{Count.ToString().ToFixedColumnRight(5)} | < 5s {s5} | 5-15s {s5to15} | 15-30s {s15to30} | 30-60s {s30to60} | > 60s {s60} |";
            }

            return $"{EventName.ToFixedColumnRight(maxLength)} {Min.ToCleanString()} to {Max.ToCleanString()} - {_temp}";
        }
    }

    public class PatternTestSummary
    {
        public PatternTestSummary(string patternName)
        {
            _patternName = patternName;
            Patterns = new List<PatternTestResult>();
            Options = new PatternOptions();
            Frequencies = new List<Frequency>();
            Unexpected = new List<CleanAttribute>();
            KnownIdle = new List<Duration>();
            Durations = new List<Duration>();
        }
        private string _patternName;
        public string PatternName
        {
            get
            {
                if (!Options.PatternUnexpected)
                    return _patternName;

                if (Patterns.Count == 0)
                    return $"{_patternName} - Unexpected Clean";

                return $"{_patternName} - Unexpected Found";
            }
        }
        public List<PatternTestResult> Patterns { get; set; }
        /// <summary>
        /// Pattern attribute that indicates if a Pattern is expected or not
        /// </summary>
        public PatternOptions Options { get; set; }
        /// <summary>
        /// Frequencies are display as an aggergate result and not per Pattern
        /// Hence the data is in the Summary and not on the Pattern
        /// </summary>
        public List<Frequency> Frequencies { get; set; }
        public List<CleanAttribute> Unexpected { get; set; }
        public List<Duration> KnownIdle { get; set; }
        public List<Duration> Durations { get; set; }

        /// <summary>
        /// Generic method to merge CleanAttribute List's.
        /// </summary>
        private void MergeEvents<T>(List<T> source, List<T> target) where T : CleanAttribute
        {
            for (int i = 0; i < source.Count; i++)
            {
                var sourceItem = source[i];
                var _targetItem = target.FirstOrDefault(n => n.EventName == sourceItem.EventName);
                if (_targetItem == null)
                {
                    target.Add(sourceItem);
                    continue;
                }

                _targetItem.Events.AddRange(sourceItem.Events);
            }
        }

        private void MergeFrequencyEvents<T>(List<T> source, List<T> target) where T : Frequency
        {
            for (int i = 0; i < source.Count; i++)
            {
                var sourceItem = source[i];
                // Ok, the thinking behind Path is that a perPattern will always have matching Path and perFile has the same
                // thus no need to split out logic based on FrequencyType - Sara
                var _targetItem = target.FirstOrDefault(n => n.EventName == sourceItem.EventName
                                                          && n.PatternIdentifier == sourceItem.PatternIdentifier
                                                          && n.Path == sourceItem.Path);
                if (_targetItem == null)
                {
                    target.Add(sourceItem);
                    continue;
                }

                _targetItem.Events.AddRange(sourceItem.Events);
            }
        }

        internal void Merge(PatternTestSummary source)
        {
            Patterns.AddRange(source.Patterns);
            // Note: Options for the same Pattern Summary should be the same, thus no merge - Sara
            MergeFrequencyEvents(source.Frequencies, Frequencies);
            MergeEvents(source.Unexpected, Unexpected);
            MergeEvents(source.KnownIdle, KnownIdle);
            MergeEvents(source.Durations, Durations);
        }

        internal PatternTestSummary Clone()
        {
            var clone = new PatternTestSummary(_patternName);

            foreach (var pattern in Patterns)
                clone.Patterns.Add(pattern.Clone());

            clone.Options = Options.Clone();

            foreach (var item in Frequencies)
                clone.Frequencies.Add(item.Clone());

            foreach (var item in Unexpected)
                clone.Unexpected.Add(item.Clone());

            foreach (var item in KnownIdle)
                clone.KnownIdle.Add(item.Clone());

            foreach (var item in Durations)
                clone.Durations.Add(item.Clone());

            return clone;
        }
    }

    public class PatternTestResults
    {
        public PatternTestResults()
        {
            Log = new List<string>();
            PatternSummaries = new List<PatternTestSummary>();
            CleanPatternSummaries = new List<PatternTestSummary>();
        }

        public PatternTestResults(PatternTestResults source) : this()
        {
            foreach (var summary in source.CleanPatternSummaries)
                PatternSummaries.Add(summary.Clone());
        }

        public List<string> Log { get; set; }
        public List<PatternTestSummary> PatternSummaries { get; private set; }
        /// <summary>
        /// Scanning for patterns is multi-threaded.  This meas we will create mulitple PatternTestResults 
        /// and merge the findings into a single PatterTestResults.
        /// To avoid calculating the clean patterns again and again, we will store them in the master record
        /// and clone the results to the working PatterTestResults for the given thread.
        /// </summary>
        public List<PatternTestSummary> CleanPatternSummaries { get; private set; }

        public bool EventExists(int iLine)
        {
            foreach (var summary in PatternSummaries)
            {
                foreach (var pattern in summary.Patterns)
                {
                    foreach (var @event in pattern.Events)
                    {
                        if (@event.StartiLine == iLine)
                            return true;
                    }
                }
            }
            return false;
        }

        public void Merge(PatternTestResults sourceResults)
        {
            Log.AddRange(sourceResults.Log);

            for (int i = 0; i < sourceResults.PatternSummaries.Count; i++)
            {
                var sourceSummary = sourceResults.PatternSummaries[i];
                var _targetSummary = PatternSummaries.Where(n => n.PatternName == sourceSummary.PatternName).FirstOrDefault();

                if (_targetSummary == null)
                {
                    PatternSummaries.Add(sourceSummary);
                    continue;
                }

                _targetSummary.Merge(sourceSummary);
            }
        }
    }
}
