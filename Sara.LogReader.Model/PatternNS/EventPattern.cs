using System;
using System.Collections.Generic;
using Sara.LogReader.Common;

namespace Sara.LogReader.Model.PatternNS
{
    public class EventPattern : IEventPattern
    {
        #region Properties
        /// <summary>
        /// Name or Regular Expression
        /// </summary>
        public string EventTag { get; set; }
        private readonly string _regularExpression;
        public string RegularExpression { get { return _regularExpression; } }
        private readonly string _regularExpressionBodyStop;
        public string RegularExpressionBodyStop { get { return _regularExpressionBodyStop; } }
        private readonly EventType _EventType;
        public EventType EventType { get { return _EventType; } }
        public IPattern Parent { get; set; }
        public bool IsRegularExpression { get; set; }
        public bool BodyStartFound { get; set; }
        public bool BodyStopFound { get; set; }
        public List<string> Values { get; set; }
        /// <summary>
        /// iLine is the index of the line in the document.
        /// Zero based.
        /// </summary>
        public EventOption Options { get; set; }
        public override string ToString()
        {
            return $"{EventTag}{Name}";
        }
        #endregion

        public Guid Identifier { get; private set; }

        #region Found Properties
        public int iLine { get; set; }
        /// <summary>
        /// When we find a match, the line is stored in Line
        /// </summary>
        public string Line { get; set; }
        public int iLineBodyStop { get; set; }
        public string LineBodyStop { get; set; }
        /// <summary>
        /// Realtime DateTime when the entry was Scanned
        /// </summary>
        public DateTime? DateTime { get; set; }
        public DateTime? DateTimeBodyStop { get; set; }
        public bool Found { get; set; }
        public bool OutOfOrder { get; set; }
        #endregion

        public IEventPattern Clone(IPattern parent)
        {
            var model = new EventPattern(EventType, EventTag, RegularExpression, RegularExpressionBodyStop, Options, Values)
            {
                Parent = parent,
                IsRegularExpression = IsRegularExpression,
                Options = Options.Clone(),
                Identifier = Identifier
            };

            return model;
        }
        public event Action<IPattern> OnMatch;
        public List<iLineMatch> iLineSearchMatches { get; set; }
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// A list of Identifiers that stop a Duration
        /// </summary>
        /// <remarks>
        /// A duration has a start Identifier and a Stop Identifier.
        /// Durations are created using TimeFrom, TimeTo, TimeToNext attributes.
        /// To create a Duration, the Stop Identifier is added to the StartEvent's DurationStopIdentifier.
        /// We will iterate through the Events.  
        ///     When a eventPattern has values in DurationStopIdentifier, we know a Duration has started.
        ///     We will store these Identifiers in DurationStopIdentifer.
        ///     When we encounter the stored Identifier, we know the Duration has stopped.
        /// This structor will support multiple durations.
        /// </remarks>
        public List<Guid> DurationStopIdentifiers { get; set; }
        public string Path { get; set; }

        public string Name
        {
            get
            {
                var _name = EventTag;
                if (Options.Name)
                    _name = $"{Options.NameParameter}";
                return _name;
            }
        }

        public EventPattern(EventType eventType, string eventTag, string regularExpression, string regularExpressionBodyStop, EventOption option, List<string> values = null)
        {
            EventTag = eventTag;
            _regularExpression = regularExpression;
            _regularExpressionBodyStop = regularExpressionBodyStop;
            IsRegularExpression = true;
            _EventType = eventType;
            Options = option;
            Values = values ?? new List<string>();
            Identifier = Guid.NewGuid();
            DurationStopIdentifiers = new List<Guid>();
        }

        public void Reset()
        {
            iLine = -1;
            Line = string.Empty;
            iLineBodyStop = -1;
            LineBodyStop = string.Empty;
            Found = false;
            BodyStartFound = false;
            BodyStopFound = false;
            OutOfOrder = false;
            Values = new List<string>();
            DurationStopIdentifiers = new List<Guid>();
        }

        public void NewIdentifier()
        {
            Identifier = Guid.NewGuid();
        }

        public void Match(IPattern parent)
        {
            OnMatch?.Invoke(parent);
        }
    }
}
