using System;
using System.Collections.Generic;
using Sara.LogReader.Common;

namespace Sara.LogReader.Model.PatternNS
{
    public enum ScanType
    {
        FirstOccurance,
        Repeating
    }

    public enum EventType
    {
        /// <summary>
        /// A Start eventPattern marks when a EventPattern begins
        /// A Complex EventPattern can have multiple start events
        /// </summary>
        Start,
        /// <summary>
        /// A Body eventPattern is an entry that occurs during the EventPattern
        /// A series of body events is what defines a complex EventPattern
        /// A Body eventPattern is sometimes optional, the reason we add it is to maintain the TimeGapBefore
        /// </summary>
        Body,
        /// <summary>
        /// A Stop eventPattern marks when a EventPattern stops
        /// A Complex EventPattern can have multiple stop eventPattern
        /// </summary>
        Stop,
        /// <summary>
        /// A Reset eventPattern will require the EventPattern to start again before it is considered complete
        /// </summary>
        Reset,
        /// <summary>
        /// Searches for a single occurance of the EventPattern
        /// </summary>
        Search,
        TotalTime,
        TotalIdleTime,
        TotalOverlappingTime,
        Warning,
        MaxOverlaps,
        GapOutside,
        GapInside,
        Restart,
        /// <summary>
        /// Duration between a Start and Stop EventPattern
        /// </summary>
        Duration,
        IdleDuration,
        Unexpected
    }

    public interface IPattern
    {
        string Name { get; set; }
        List<IEventPattern> OrginalEvents { get; set; }
        List<IEventPattern> Events { get; set; }
        ScanType ScanType { get; set; }
        bool Found { get; set; }
        /// <summary>
        /// Returns True when the Pattern only contains Search Events
        /// </summary>
        bool OnlySearch { get; }
        /// <summary>
        /// Returns True when the Pattern finds a match
        /// </summary>
        bool Scan(ScanLineArgs args);
        event Action<IPattern> OnFound;
        void Reset();
        /// <summary>
        /// Scan the entire document at once for a Search EventPattern
        /// </summary>
        /// <returns></returns>
        void ScanSearch(string rawTextString);
    }

    public interface IEventPatternIdentifier
    {
        Guid Identifier { get; }
        List<Guid> DurationStopIdentifiers { get; set; }
    }

    public interface IEventPatternBase
    {
        EventType EventType { get; }
        EventOption Options { get; set; }
        /// <summary>
        /// When a step is found, the system will search for any of the values listed and add them as a column in the Pattern Report
        /// </summary>
        List<string> Values { get; set; }
    }

    public interface IEventPattern : IEventPatternBase, IEventPatternIdentifier
    {
        /// <summary>
        /// Name of the EventPattern or Regular Expression for the EventPattern
        /// </summary>
        string EventTag { get; set; }
        /// <summary>
        /// True when the line is found
        /// </summary>
        bool Found { get; set; }
        /// <summary>
        /// For each Search EventPattern, the system will scan the entire contents of the Document and 
        /// return the iLines that have a match
        /// </summary>
        List<iLineMatch> iLineSearchMatches { get; set; }
        /// <summary>
        /// True when an RequiredInOrder line is found before this line
        /// </summary>
        bool OutOfOrder { get; set; }
        /// <summary>
        /// True when the Start of a body was already found and this is the Stop.
        /// </summary>
        bool BodyStartFound { get; set; }
        bool BodyStopFound { get; set; }
        IPattern Parent { get; set; }
        void Reset();
        void NewIdentifier();
        string Path { get; set; }
        /// <summary>
        /// The last line where the step was successful
        /// </summary>
        // ReSharper disable once InconsistentNaming
        int iLine { get; set; }
        /// <summary>
        /// the line that matched the search criteria
        /// </summary>
        string Line { get; set; }
        int iLineBodyStop { get; set; }
        string LineBodyStop { get; set; }
        DateTime? DateTime { get; set; }
        DateTime? DateTimeBodyStop { get; set; }
        IEventPattern Clone(IPattern parent);
        TimeSpan Duration { get; set; }
        string Name { get; }
    }
}
