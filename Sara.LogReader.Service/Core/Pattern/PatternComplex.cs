using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sara.Logging;
using Sara.LogReader.Common;
using Sara.LogReader.Model.PatternNS;

namespace Sara.LogReader.Service.Core.Pattern
{
    public class PatternComplex : IPattern
    {
        #region Properties
        private bool Active { get; set; }
        public bool Found { get; set; }
        public ScanType ScanType { get; set; }
        public List<IEventPattern> Events { get; set; }
        public List<string> Warnings { get; set; }
        private bool _hasStarted;
        public string Name { get; set; }
        public TimeSpan TotalDuration
        {
            get
            {
                if (!Events.Any())
                    return new TimeSpan();

                var start = Events.Where(n => n.Found).First();
                var stop = Events.Where(n => n.Found).Last();

                if (!start.DateTime.HasValue || !stop.DateTime.HasValue)
                    return new TimeSpan(0);

                return stop.DateTime.Value - start.DateTime.Value;
            }
        }
        public bool OnlySearch
        {
            get
            {
                foreach (var item in Events)
                {
                    if (item.EventType != EventType.Search)
                        return false;
                }
                return true;
            }
        }
        /// <summary>
        /// List of Sources this Pattern applies too
        /// </summary>
        public List<string> Sources { get; set; }
        public PatternOptions Options { get; set; }
        public List<IEventPattern> OrginalEvents { get; set; }
        private EventPattern LastFound { get; set; }
        #endregion

        #region Events
        /// <summary>
        /// Triggered when a Pattern is found
        /// </summary>
        public event Action<IPattern> OnFound;
        public event Action<IPattern, int> OnReset;
        #endregion

        #region Setup
        public PatternComplex(string name, ScanType scanType, Action<IPattern> callback)
        {
            Initialize(name, scanType, callback);
        }
        public PatternComplex(string name, ScanType scanType)
        {
            Initialize(name, scanType, null);
        }
        public void Initialize(string name, ScanType scanType, Action<IPattern> callback)
        {
            Events = new List<IEventPattern>();
            OrginalEvents = new List<IEventPattern>();
            Options = new PatternOptions();
            Sources = new List<string>();
            Warnings = new List<string>();
            Name = name;
            Active = true;
            _hasStarted = false;
            ScanType = scanType;
            if (callback != null)
                OnFound += callback;
        }
        #endregion

        #region Methods
        public PatternComplex Clone()
        {
            var model = new PatternComplex(Name, ScanType, OnFound)
            {
                Options = Options.Clone()
            };
            foreach (IEventPattern e in Events)
                model.Events.Add(e.Clone(model));

            foreach (IEventPattern e in OrginalEvents)
                model.OrginalEvents.Add(e.Clone(model));

            foreach (var source in Sources)
                model.Sources.Add(source);

            foreach (var warning in Warnings)
                model.Warnings.Add(warning);

            return model;
        }
        private string RemoveTimeZone(string dateTime)
        {
            return dateTime == null ? "" : dateTime.Substring(0, dateTime.IndexOf("M ", StringComparison.Ordinal) + 1);
        }
        private bool TryParseWithTimeZoneRemoval(string value, out DateTime result)
        {
            return DateTime.TryParse(RemoveTimeZone(value), out result);
        }
        public void PrepareForScan()
        {
            OnFound = null;
            OnReset = null;
            OrginalEvents.Clear();
            foreach (var step in Events)
                OrginalEvents.Add(step.Clone(this));
        }
        public void Reset()
        {
            _hasStarted = false;
            Events.Clear();

            foreach (var step in OrginalEvents)
                Events.Add(step.Clone(this));

            foreach (var step in Events)
            {
                step.Reset();
            }
        }
        public void AddEvent(IEventPattern e)
        {
            e.Parent = this;
            Events.Add(e);
        }
        #endregion

        public void ScanSearch(string rawTextString)
        {
            foreach (var e in Events.Where(n => (n.EventType == EventType.Search)))
            {
                e.iLineSearchMatches = RegularExpression.GetLines((e as EventPattern).RegularExpression, rawTextString);
                if (e.iLineSearchMatches.Count > 0)
                    e.Found = true;
            }

            // It the Pattern only contains Search Events and at least one EventPattern was found, then the Pattern Found is True. -Sara
            if (OnlySearch && (Events.Where(n => n.Found).Count() > 0))
            {
                Found = true;
                OnFound?.Invoke(this);
            }
        }
        /// <summary>
        /// Returns true when the Step found a match
        /// </summary>
        public bool StepScan(EventPattern step, ScanLineArgs args)
        {
            var s = string.Empty;

            var expression = step.RegularExpression;
            if (step.Options.BodyStop && step.BodyStartFound)
                expression = step.RegularExpressionBodyStop;

            step.Found = step.IsRegularExpression ? RegularExpression.HasMatch(args.Line, expression) :
                                               Common.GetStringFromLog(args.Line, step.RegularExpression, ref s);
            if (step.Found)
            {
                Log.Write($"   {step.Parent.Name}: Step {step.EventType}, \"{step.RegularExpression.Insert(1, "__")}\"",typeof(EventPattern).FullName, MethodBase.GetCurrentMethod().Name, LogEntryType.Debug);

                // 'Prior' option requires that a exact EventPattern is found prior to this EventPattern
                if (step.Options.Prior &&
                    LastFound != null)
                {
                    var _priorStep = step.Parent.Events[step.Parent.Events.IndexOf(step) - step.Options.PriorIndex];
                    if ((!_priorStep.Found && _priorStep.EventTag == step.Options.PriorPattern) || _priorStep.EventTag != step.Options.PriorPattern)
                    {
                        Log.Write($"EventPattern found, however the expected Prior EventPattern should be '{step.Options.PriorPattern}' at Index {step.Options.PriorIndex}", typeof(EventPattern).FullName, MethodBase.GetCurrentMethod().Name, LogEntryType.Debug);
                        step.Found = false;
                        return false;
                    }
                }

                // 'FirstRepeat' will only allow the first match to stay a match, any events that are found after are skipped
                if (step.Options.FirstRepeat &&
                    LastFound != null &&
                    LastFound.Options.FirstRepeat &&
                    LastFound.EventTag == step.EventTag)
                {
                    step.Found = false;
                    return false;
                }


                if (step.Options.BodyStop)
                {
                    if (step.BodyStartFound)
                    {
                        step.DateTimeBodyStop = RegularExpression.GetDateTime(EventService.GetDateTimeRegularExpression(args.SourceType), args.Line);
                        step.iLineBodyStop = args.iLine;
                        step.LineBodyStop = args.Line;
                    }
                    else
                    {
                        step.DateTime = RegularExpression.GetDateTime(EventService.GetDateTimeRegularExpression(args.SourceType), args.Line);
                        step.iLine = args.iLine;
                        step.Line = args.Line;
                    }
                }
                else
                {
                    step.DateTime = RegularExpression.GetDateTime(EventService.GetDateTimeRegularExpression(args.SourceType), args.Line);
                    step.iLine = args.iLine;
                    step.Line = args.Line;
                }

                step.Found = true;
                step.Match(step.Parent);


                // Last Repeat
                if (step.Options.LastRepeat &&
                    LastFound != null &&
                    LastFound.Options.LastRepeat && 
                    step.EventTag == LastFound.EventTag)
                {
                    step.Parent.Events.Remove(LastFound);
                    LastFound = null;
                }

                // 'BodyStop' option represents a start and stop eventPattern.  
                if (step.Options.BodyStop)
                {
                    if (!step.BodyStartFound)
                    {
                        step.BodyStartFound = true;
                        step.Found = false;
                        return false;
                    }

                    step.BodyStopFound = true;
                }

                // When an EventPattern is found that can occur OneOrMore times
                // Clone the EventPattern
                // If Required or RequiredInOrder, remove these flags and make the next EventPattern Optional
                if (step.Options.OneOrMore)
                {
                    var index = step.Parent.Events.FindIndex(p => p.Identifier == step.Identifier);
                    var e = step.Clone(step.Parent);
                    e.NewIdentifier();
                    if (step.Options.Required || step.Options.RequiredInOrder)
                    {
                        e.Options.Required = false;
                        e.Options.RequiredInOrder = false;
                        e.Options.Optional = true;
                    }
                    step.Parent.Events.Insert(index + 1, e);
                }
                LastFound = step;
            }

            return step.Found;
        }

        /// <summary>
        /// Returns true when the Pattern found a match
        /// </summary>
        public bool Scan(ScanLineArgs args)
        {
            if (!Active) return false;

            // The Pattern was found, thus we no longer need to search for it
            if (Found) return false;

            #region Start
            // To start a EventPattern, search for any START line
            // There can be one or more start lines, any of them will start the EventPattern
            if (!_hasStarted)
            {

                foreach (var step in Events.Where(n => (n.EventType == EventType.Start ||
                                                       n.EventType == EventType.Search) && n.Found == false))
                {
                    if (!StepScan(step as EventPattern, args)) continue;

                    if (step.EventType == EventType.Search)
                    {
                        OnFound?.Invoke(this);

                        Log.WriteError($"{Name}: Pattern Search was found.", typeof(PatternComplex).FullName, MethodBase.GetCurrentMethod().Name);

                        if (ScanType == ScanType.Repeating)
                            Reset();

                        return true;
                    }

                    _hasStarted = true;
                    return false; // Full patter is not found yet
                }
                return false; // Full EventPattern is not found yet
            }
            #endregion Start

            #region Body
            // Within the body of the EventPattern you will find 3 types of lines: Optional, Required, RequiredInOrder
            // Once a line is found, then don't search for it again
            // Optional and Required lines can occur in any order up to a RequiredInOrder line
            // Start from the top of the list of body messages until you run into a RequiredInOrder
            // Attempt to find the first RequiredInOrder line you run into, but not the second
            // Once a RequiredinOrder is found, then any lines prior to the RequriedInOrder that is not found should be flagged OutOfOrder
            foreach (var step in Events.Where(n => n.EventType == EventType.Body && n.Found == false))
            {
                if (step.Options.RequiredInOrder)
                {
                    if (StepScan(step as EventPattern, args))
                    {
                        // Set all non-found lines prior to the RequiredInOrder as Out of Order
                        foreach (var step2 in Events.Where(n => n.EventType == EventType.Body && n.Found == false))
                        {
                            if (step2 == step) break;

                            step2.OutOfOrder = true;
                        }
                    }
                    return false; // Full EventPattern is not found yet
                }

                if (StepScan(step as EventPattern, args))
                    return false; // Full EventPattern is not found yet

                // If we found the start of a 'stop & stop' pair, then move onto the next line. - Sara
                if (step.BodyStartFound) break;
            }
            #endregion

            #region Stop
            // To Stop a EventPattern, all required lines must be found and then a stop line
            // If you run into a Stop line before all required lines are found then the EventPattern is reset and not found
            foreach (var step in Events.Where(n => n.EventType == EventType.Stop || 
                                              n.EventType == EventType.Reset ||
                                              n.EventType == EventType.Restart).Where(n => n.Found == false))
            {
                if (!StepScan(step as EventPattern, args)) continue;

                if (step.EventType == EventType.Reset)
                {
                    Log.WriteError($"{Name}: Pattern was Reset!",typeof(PatternComplex).FullName, MethodBase.GetCurrentMethod().Name);
                    OnReset?.Invoke(this, args.iLine);
                    Reset();
                    return false; // Full EventPattern is not found, but reset found - Failure!
                }

                if (step.EventType == EventType.Restart)
                {
                    var newEvents = new List<IEventPattern>();
                    // Remove all steps above the Restart
                    var index = Events.IndexOf(step) + (step.Options.OneOrMore ? 1 : 0);
                    for (int i = index; i < Events.Count; i++)
                    {
                        newEvents.Add(Events[i]);
                    }
                    Events = newEvents;
                    return false;
                }

                var success = true;
                // Check if all Required and RequiredInOrder were found
                foreach (var step3 in Events.Where(n => n.Found == false))
                {
                    if (!step3.Options.Required && !step3.Options.RequiredInOrder)
                        continue;

                    success = false;
                    break;
                }

                if (!success)
                {
                    Log.WriteError($"{Name}: Pattern started but was not complete!",typeof(PatternComplex).FullName, MethodBase.GetCurrentMethod().Name);
                    Reset();
                    return false; // Full EventPattern is not found, but stop found - Failure!
                }

                Log.Write($"   {Name}: Pattern FOUND!!!", typeof(PatternComplex).FullName, MethodBase.GetCurrentMethod().Name, LogEntryType.Debug);

                OnFound?.Invoke(this);

                if (ScanType == ScanType.Repeating)
                    Reset();

                return true; // Full eventPattern found!
            }
            #endregion Stop

            if (args.LastLine && _hasStarted)
            {
                var success = true;
                foreach (var step3 in Events.Where(n => n.Found == false))
                {
                    if (!step3.Options.Required && !step3.Options.RequiredInOrder)
                        continue;

                    success = false;
                    break;
                }

                if (!success)
                {
                    return false;
                }

                OnFound?.Invoke(this);

                return true; // Full EventPattern found!
            }

            // To Reset a EventPattern, a reset line must be found
            // If this occurs the EventPattern is reset

            return false;
        }
    }
}
