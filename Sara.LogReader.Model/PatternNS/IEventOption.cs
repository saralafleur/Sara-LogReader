using System;
using System.Collections.Generic;

namespace Sara.LogReader.Model.PatternNS
{
    public enum EventOptionEnum
    {
        Required,
        /// <summary>
        /// There are times that optional log entries will occur in a EventPattern.  These optional entires if not captured will increase the TimeGapBefore TimeSpan, thus why we have optional. - Sara
        /// </summary>
        Optional,
        /// <summary>
        /// The log entry is required in the order added to the Complex Pattern
        /// </summary>
        RequiredInOrder,
        /// <summary>
        /// Measures the duration to the next EventPattern
        /// </summary>
        Time,
        /// <summary>
        /// The eventPattern can be found one or more times
        /// When used with Required, at least 1 eventPattern must be found, additional are optional
        /// </summary>
        OneOrMore
    }

    public class EventBaseLine
    {
        public EventBaseLine()
        {
            Min = new TimeSpan();
            Max = new TimeSpan();
        }
        public string EventName { get; set; }
        public TimeSpan Min { get; set; }
        public TimeSpan Max { get; set; }
    }

    public enum ParameterType
    {
        Name,
        Min,
        Max
    }

    public static class EventBaseLineHelper
    {
        /// <summary>
        /// Converts a string list to either a default Min of zero with Max as the first parameter
        /// OR
        /// Min as the first Parameter and Max as second
        /// </summary>
        /// <example>
        /// "2"        = min 0 | max 2
        /// "2","5"    = min 2 | max 5
        /// empty list = 
        /// </example>
        public static bool ToMinAndMax(this List<string> parameters, out int min, out int max)
        {
            min = 0;
            max = 0;
            if (parameters.Count == 0)
                return true;
            if (parameters.Count == 1)
            {
                if (int.TryParse(parameters[0], out max))
                    return true;
            }

            if (int.TryParse(parameters[0], out min))
                if (int.TryParse(parameters[1], out max))
                    return true;

            return false;
        }
        /// <summary>
        /// Converts a parameter list to a EventPattern Name and Baseline
        /// </summary>
        public static List<EventBaseLine> ToBaseLineList(this List<string> parameters)
        {
            // Example
            // "Name"
            // "Name", "Name"
            // "Name", "00:00:00", "00:00:00"
            // "Name", "00:00:00", "00:00:00", "Name"
            // "Name", "00:00:00", "00:00:00", "Name", "00:00:00", "00:00:00"
            // "Name", "Name", "00:00:00", "00:00:00"

            if (parameters.Count == 0)
                return new List<EventBaseLine>()
                {
                    new EventBaseLine()
                };

            // Check if the following is a Baseline
            // "00:00:00"
            if (parameters.Count == 1 && TimeSpan.TryParse(parameters[0], out TimeSpan t1))
            {
                return new List<EventBaseLine>()
                    {
                        new EventBaseLine()
                        {
                            Min = new TimeSpan(),
                            Max = t1
                        }
                    };
            }

            // Check if the following is a Baseline
            // "00:00:00", "00:00:00"
            if (parameters.Count == 2 && TimeSpan.TryParse(parameters[0], out TimeSpan test))
            {
                var result = new EventBaseLine()
                {
                    Min = test
                };

                if (TimeSpan.TryParse(parameters[1], out test))
                {
                    result.Max = test;
                    return new List<EventBaseLine>()
                    {
                        result
                    };
                }
            }

            var index = ParameterType.Name;

            EventBaseLine current = null;
            var results = new List<EventBaseLine>();

            foreach (var p in parameters)
            {
                switch (index)
                {
                    case ParameterType.Name:
                        current = new EventBaseLine()
                        {
                            EventName = p
                        };
                        index = ParameterType.Min;
                        break;
                    case ParameterType.Min:
                        if (TimeSpan.TryParse(p, out test))
                        {
                            current.Min = test;
                            index = ParameterType.Max;
                        }
                        else
                        {
                            results.Add(current);
                            current = new EventBaseLine()
                            {
                                EventName = p
                            };
                            index = ParameterType.Min;
                        }
                        break;
                    case ParameterType.Max:
                        if (TimeSpan.TryParse(p, out test))
                        {
                            current.Max = test;
                            results.Add(current);
                            current = null;
                            index = ParameterType.Name;
                        }
                        else
                        {
                            // There is no Min, only a Max
                            // Set Min to zero and the Max to what was collected for the Min
                            current.Max = current.Min;
                            current.Min = new TimeSpan();
                            results.Add(current);
                            current = null;
                            index = ParameterType.Name;
                        }
                        break;
                }
            }

            if (current != null)
            {
                // Check if the last EventPattern only has a Max and thus we need to adjust the record
                // "EventPattern", "0:0:1"
                if (current.Min.Ticks > 0 && current.Max.Ticks == 0)
                {
                    current.Max = current.Min;
                    current.Min = new TimeSpan();
                }

                results.Add(current);
            }

            return results;
        }
    }

    public class EventOption
    {
        public EventOption()
        {
            TimeToNextParameters = new List<string>();
            TimeToNextKnownIdleParameters = new List<string>();
            TimeToParameters = new List<string>();
            TimeToOrParameters = new List<string>();
            TimeFromParameters = new List<string>();
            ValueParameters = new List<string>();
            FrequencyParameters = new List<string>();
            FrequencyPerFileParameters = new List<string>();
        }
        public bool Required { get; set; }
        public bool RequiredInOrder { get; set; }
        public bool Optional { get; set; }

        internal EventOption Clone()
        {
            return new EventOption()
            {
                Required = Required,
                RequiredInOrder = RequiredInOrder,
                Optional = Optional,
                Unexpected = Unexpected,
                TimeToNextKnownIdle = TimeToNextKnownIdle,
                TimeToNextKnownIdleParameters = new List<string>(TimeToNextKnownIdleParameters),
                TimeToNext = TimeToNext,
                TimeToNextParameters = new List<string>(TimeToNextParameters),
                OneOrMore = OneOrMore,
                TimeFrom = TimeFrom,
                TimeFromParameters = new List<string>(TimeFromParameters),
                TimeTo = TimeTo,
                TimeToOr = TimeToOr,
                TimeToOrParameters = new List<string>(TimeToOrParameters),
                TimeToParameters = new List<string>(TimeToParameters),
                Frequency = Frequency,
                FrequencyParameters = new List<string>(FrequencyParameters),
                FrequencyPerFile = FrequencyPerFile,
                FrequencyPerFileParameters = new List<string>(FrequencyPerFileParameters),
                Hide = Hide,
                LastRepeat = LastRepeat,
                Prior = Prior,
                PriorIndex = PriorIndex,
                PriorPattern = PriorPattern,
                FirstRepeat = FirstRepeat,
                Value = Value,
                ValueParameters = new List<string>(ValueParameters),
                HideEvent = HideEvent,
                Name = Name,
                NameParameter = NameParameter,
                BodyStop = BodyStop,
                BodyStopParameter = BodyStopParameter
            };
        }

        /// <summary>
        /// Used to record the duration between an eventPattern and the next eventPattern.
        /// </summary>
        public bool TimeToNext { get; set; }
        public List<string> TimeToNextParameters { get; set; }
        public bool TimeToNextKnownIdle { get; set; }
        public List<string> TimeToNextKnownIdleParameters { get; set; }
        public bool TimeFrom { get; set; }
        public bool Hide { get; set; }
        public bool TimeTo { get; set; }
        public List<string> TimeToParameters { get; set; }
        /// <summary>
        /// When more then one EventPattern is specified, the first eventPattern encountered will be used.
        /// As compared to TimeTo, both Events will be measured for Duration.
        /// </summary>
        public bool TimeToOr { get; set; }
        public List<string> TimeToOrParameters { get; set; }
        public List<string> TimeFromParameters { get; set; }
        public bool OneOrMore { get; set; }
        public bool LastRepeat { get; set; }
        public bool Prior { get; set; }
        public int PriorIndex { get; set; }
        public string PriorPattern { get; set; }
        public bool FirstRepeat { get; set; }
        public bool Value { get; set; }
        public List<string> ValueParameters { get; set; }
        public bool HideEvent { get; set; }
        public bool Name { get; set; }
        public string NameParameter { get; set; }
        public bool BodyStop { get; set; }
        public string BodyStopParameter { get; set; }
        public bool Frequency { get; set; }
        public List<string> FrequencyParameters { get; set; }
        public bool FrequencyPerFile { get; set; }
        public List<string> FrequencyPerFileParameters { get; set; }
        public bool Unexpected { get; set; }
    }

}
