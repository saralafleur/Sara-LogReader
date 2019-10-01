using System;
using System.Collections.Generic;
using System.Linq;
using Sara.Common.Extension;
using Sara.LogReader.Model;
using Sara.LogReader.Model.IDE;
using Sara.LogReader.Model.NetworkMapping;
using Sara.LogReader.Model.PatternNS;
using Sara.LogReader.Service.Core.Pattern;
using Sara.LogReader.Service.Core.Service;
using Sara.MonitorScript.Lexer;
using ValueType = Sara.LogReader.Model.IDE.ValueType;

namespace Sara.LogReader.Service.PatternServiceNS
{
    public class PatternScanService
    {
        #region Properties
        /// <summary>
        /// Temporary store the path of the current file being scanned.
        /// </summary>
        private string _path;
        /// <summary>
        /// Temporary store the result object for the current file being scanned.
        /// </summary>
        private PatternTestResults _result { get; set; }
        #endregion Properties

        #region Events
        private void PatternReset(IPattern item, int iLine)
        {
            var r = item as PatternComplex;
            if (r == null)
                throw new Exception("item must be of type Pattern!");

            var foundMessage = $@"{DateTime.Now.ToShortTimeString()} Pattern Reset ""{item.Name}"" on line {iLine}";
            _result.Log.Add(foundMessage);
        }
        public void PatternFound(IPattern item)
        {
            var r = item as PatternComplex;
            if (r == null)
                throw new Exception("item must be of type Pattern!");

            AddResult(r);

        }
        #endregion Events

        #region Helper Methods
        private IEventPatternIdentifier FindEventResult(EventPattern e, List<PatternTestResultEventPattern> events)
        {
            return events.Where(n => n.Identifier == e.Identifier).FirstOrDefault();
        }
        private void ProcessValues(IEventPattern e, PatternTestResultEventPattern re)
        {
            #region Value
            if (e.Options.Value)
            {
                var prop = PropertyService.GetProperty(new LineArgs
                {
                    Path = _path,
                    iLine = e.iLine,
                    Line = e.Line
                });

                foreach (var valueName in e.Options.ValueParameters)
                {
                    var value = prop.FindPropertyValue(valueName);
                    if (value != null)
                    {
                        var v = new NameValue()
                        {
                            Name = valueName,
                            Value = value,
                            Type = Model.IDE.ValueType.Value
                        };
                        re.Values.Add(v);
                    }
                }
            }
            #endregion
        }
        private EventPattern GetPriorEventByName(List<IEventPattern> events, IEventPattern item, string eventTag)
        {
            var index = events.IndexOf(item) - 1;
            for (int i = index; i >= 0; i--)
            {
                if (!events[i].Found)
                    continue;
                if (events[i].EventTag != eventTag)
                    continue;
                return events[i] as EventPattern;
            }
            return null;
        }
        private EventPattern GetNextEventByName(List<IEventPattern> events, IEventPattern item, string eventTag)
        {
            var index = events.IndexOf(item) + 1;

            for (int i = index; i < events.Count; i++)
            {
                if (!events[i].Found)
                    continue;
                if (events[i].EventTag != eventTag)
                    continue;
                return events[i] as EventPattern;
            }

            return null;
        }
        /// <summary>
        /// Returns the next Found EventPattern. 
        /// Return NULL if we are at the end of the List.
        /// </summary>
        private EventPattern GetNextEvent(List<IEventPattern> events, IEventPattern item)
        {
            var index = events.IndexOf(item) + 1;

            for (int i = index; i < events.Count; i++)
            {
                if (!events[i].Found)
                    continue;
                return events[i] as EventPattern;
            }

            return null;
        }
        #endregion Helper Methods

        #region Duration Helper
        private void ProcessDurationOutsideInside(List<PatternTestResultEventPattern> model, List<DurationColumn> durationPattern)
        {
            foreach (var e in model.Where(n => n.EventType == EventType.Duration))
            {
                var dc = durationPattern.FirstOrDefault(n => n.Start == e.DurationStartIdentifier && n.Stop == e.DurationStopIdentifier);
                e.DurationOutsideOfDuration = dc.DurationOutsideOfDuration;
                e.DurationInsideOfDuration = dc.DurationInsideOfDuration;
            }
        }
        private static List<DurationColumn> PrepareDurationPattern(PatternTestResult p)
        {
            var _index = 0;
            var _durationStopColumns = new List<DurationColumn>();
            foreach (var e in p.Events)
            {
                if (e.DurationStopIdentifiers.Any())
                {
                    foreach (var stop in e.DurationStopIdentifiers)
                    {
                        if (!_durationStopColumns.Any(n => n.Start == e.Identifier && n.Stop == stop))
                        {
                            _durationStopColumns.Add(new DurationColumn()
                            {
                                Column = _index,
                                Start = e.Identifier,
                                Stop = stop
                            });
                            _index++;
                        }
                    }
                }
            }
            return _durationStopColumns;
        }

        #endregion Duration Helper

        public PatternTestResults Scan(List<PatternComplex> patterns, string path, PatternTestResults result)
        {
            _result = result ?? throw new Exception("result must never be null!");
            _path = path;

            try
            {
                var file = XmlDal.CacheModel.GetFile(path);

                var scanner = new RealTimePatternScanService();

                foreach (PatternComplex item in patterns.Where(n => n.Sources.Contains(file.SourceType)))
                {
                    var pattern = item.Clone();
                    pattern.PrepareForScan();
                    pattern.OnFound += PatternFound;
                    pattern.OnReset += PatternReset;
                    scanner.AddPattern(pattern);
                }

                if (scanner.IsEmpty)
                    return _result;

                var rawTextString = file.RawTextString;

                scanner.Start();
                scanner.Scan(rawTextString, file.SourceType);
                scanner.Stop();

                PostProces(_result);

                if (_result.PatternSummaries.Count() == 0)
                    _result.Log.Add($"{DateTime.Now.ToShortTimeString()} NO PATTERNS FOUND...");

                _result.Log.Add($"{DateTime.Now.ToShortTimeString()} Pattern Scan Complete...");

                return _result;
            }
            finally
            {
                _result = null;
                _path = null;
            }
        }

        #region Process Methods
        private void AddResult(PatternComplex r)
        {
            var p = new PatternTestResult
            {
                Name = r.Name,
                PatternName = r.Name,
                Path = _path,
                StartiLine = r.Events.Any() ? r.Events.First(n => n.Found).iLine : 0,
                EndiLine = r.Events.Any() ? r.Events.Last(n => n.Found).iLine : 0,
                Warnings = r.Warnings,
                TotalDuration = r.TotalDuration,
                Options = r.Options.Clone()
            };

            r.Events = r.Events.OrderBy(n => n.iLine).ToList();

            foreach (var e in r.Events.Where(n => n.Found))
            {
                e.Path = _path;

                #region Process Search
                if (e.EventType == EventType.Search)
                {
                    _result.Log.Add($@"{DateTime.Now.ToShortTimeString()} Pattern Search EventPattern ""{e.EventTag}"" found {e.iLineSearchMatches.Count} matches.");
                    foreach (var i in e.iLineSearchMatches)
                    {
                        var res = new PatternTestResultEventPattern(e, i.iLine);
                        e.iLine = i.iLine;
                        e.Line = i.Line;
                        ProcessValues(e, res);
                        p.Events.Add(res);
                    }
                    continue;
                }
                #endregion Process Search

                var re = new PatternTestResultEventPattern(e, e.iLine);
                ProcessDuration(r.Events, p.Events, e, re);
                ProcessValues(e, re);
                p.Events.Add(re);

                // TODO: What is this for, should it be moved to a Summary of Totals section? - Sara
                #region Process Stop
                if (e.BodyStopFound
                    && e.DateTime.HasValue
                    && e.DateTimeBodyStop.HasValue)
                {
                    var _duration = e.DateTimeBodyStop.Value - e.DateTime;
                    var b = new PatternTestResultEventPattern($"{_duration.Value.ToFixedTimeString()}",
                                                                 EventType.Duration,
                                                                 e.Path,
                                                                 e.iLineBodyStop,
                                                                 0,
                                                                 null,
                                                                 e.DateTimeBodyStop,
                                                                 _duration.Value);
                    p.Events.Add(b);
                }
                #endregion Process Stop
            }


            #region Add Processed Pattern to List
            p.Events = p.Events.OrderBy(n => n.StartiLine).ToList();
            var _summary = _result.PatternSummaries.Where(n => n.PatternName == p.PatternName).FirstOrDefault();
            if (_summary == null)
            {
                _summary = new PatternTestSummary(p.PatternName);
                _result.PatternSummaries.Add(_summary);
            }

            _summary.Patterns.Add(p);
            #endregion
        }

        private void PostProcessDuration(List<PatternTestResultEventPattern> events, PatternTestSummary summary)
        {
            foreach (var @event in events.Where(n => n.OutsideBaseline && n.EventType == EventType.Duration))
                summary.Durations.FirstOrDefault(n => n.EventName == @event.EventName).Events.Add(@event);
        }
        private void PostProcessKnownIdle(List<PatternTestResultEventPattern> events, PatternTestSummary summary)
        {
            foreach (var @event in events.Where(n => n.OutsideBaseline && n.EventType == EventType.IdleDuration))
            {
                var _temp = summary.KnownIdle.FirstOrDefault(n => n.EventName == @event.EventName);

                if (_temp == null)
                {
                    _temp = new Duration()
                    {
                        EventName = @event.EventName,
                        Min = @event.BaselineMin,
                        Max = @event.BaselineMax
                    };
                    summary.KnownIdle.Add(_temp);
                }

                _temp.Events.Add(@event);
            }
        }
        private void PostProcessUnexpected(List<PatternTestResultEventPattern> events, PatternTestSummary summary)
        {
            foreach (var @event in events.Where(n => n.Options.Unexpected))
                summary.Unexpected.FirstOrDefault(n => n.EventName == (@event.EventName ?? @event.Name)).Events.Add(@event);
        }
        private void PostProcessFrequency(PatternTestResult pattern, PatternTestSummary patternSummary)
        {
            foreach (var e in pattern.Events.Where(n => n.Options.Frequency || n.Options.FrequencyPerFile))
            {
                if (e.Options.Frequency && e.Options.FrequencyParameters.ToMinAndMax(out int _min, out int _max))
                {
                    Frequency _freq;
                    if (patternSummary.Frequencies.Any(n => n.EventName == e.Name
                                                         && n.FrequencyType == FrequencyType.PerPattern
                                                         && n.PatternIdentifier == pattern.Identifier))
                        patternSummary.Frequencies.FirstOrDefault(n => n.EventName == e.Name
                                                                    && n.FrequencyType == FrequencyType.PerPattern
                                                                    && n.PatternIdentifier == pattern.Identifier).Events.Add(e);
                    else
                    {
                        _freq = new Frequency()
                        {
                            EventName = e.Name,
                            FrequencyType = FrequencyType.PerPattern,
                            iline = pattern.StartiLine,
                            Path = pattern.Path,
                            Min = _min,
                            Max = _max,
                            PatternIdentifier = pattern.Identifier
                        };
                        _freq.Events.Add(e);
                        patternSummary.Frequencies.Add(_freq);
                    }
                }
                if (e.Options.FrequencyPerFile && e.Options.FrequencyPerFileParameters.ToMinAndMax(out _min, out _max))
                {
                    if (patternSummary.Frequencies.Any(n => n.FrequencyType == FrequencyType.PerFile 
                                                         && n.EventName == e.Name 
                                                         && n.Path == e.Path))
                        patternSummary.Frequencies.FirstOrDefault(n => n.FrequencyType == FrequencyType.PerFile 
                                                               && n.EventName == e.Name 
                                                               && n.Path == e.Path).Events.Add(e);
                    else
                    {
                        Frequency _freq;
                        _freq = new Frequency()
                        {
                            EventName = e.Name,
                            FrequencyType = FrequencyType.PerFile,
                            Min = _min,
                            Max = _max,
                            Path = pattern.Path
                        };
                        _freq.Events.Add(e);
                        patternSummary.Frequencies.Add(_freq);
                    }
                }
            }

            for (int i = patternSummary.Frequencies.Count - 1; i >= 0; i--)
            {
                var freq = patternSummary.Frequencies[i];

                if (freq.Count >= freq.Min && freq.Count <= freq.Max && freq.Count > 0)
                    patternSummary.Frequencies.RemoveAt(i);
            }
            patternSummary.Frequencies = patternSummary.Frequencies.OrderByDescending(n => n.Count).ToList();
        }
        private void ProcessDuration(List<IEventPattern> events, List<PatternTestResultEventPattern> sourceEvents, IEventPattern eventPattern, PatternTestResultEventPattern re)
        {
            #region TimeToNextKnownIdle
            if (eventPattern.Options.TimeToNextKnownIdle)
            {
                var _baseLine = eventPattern.Options.TimeToNextKnownIdleParameters.ToBaseLineList().First();

                var nextEvent = GetNextEvent(events, eventPattern);
                if (nextEvent != null &&
                    eventPattern.DateTime.HasValue &&
                    nextEvent.DateTime.HasValue)
                {
                    re.DurationStopIdentifiers.Add(nextEvent.Identifier);

                    var v = new NameValue()
                    {
                        Name = $"{Keyword._TimeToNextKnownIdle}({nextEvent.EventTag})",
                        Value = eventPattern.DateTime.Value.Difference(nextEvent.DateTime.Value),
                        Type = Model.IDE.ValueType.TimeToNextKnownIdle,
                        StartIdentifier = eventPattern.Identifier,
                        StopIdentifier = nextEvent.Identifier,
                        BaselineMin = _baseLine.Min,
                        BaselineMax = _baseLine.Max
                    };
                    // To keep the unique value of the column, do not include time in the name
                    //if (v.Value is TimeSpan d)
                    //    re.Name = $"{re.Name} - {d.ToShortReadableString()}";
                    re.Values.Add(v);
                }
            }
            #endregion TimeToNextKnownIdle

            #region TimeToNext
            if (eventPattern.Options.TimeToNext)
            {
                var _baseLine = eventPattern.Options.TimeToNextParameters.ToBaseLineList().First();

                var nextEvent = GetNextEvent(events, eventPattern);
                if (nextEvent != null &&
                    eventPattern.DateTime.HasValue &&
                    nextEvent.DateTime.HasValue)
                {
                    re.DurationStopIdentifiers.Add(nextEvent.Identifier);

                    var v = new NameValue()
                    {
                        Name = $"{Keyword._TimeToNext}({nextEvent.EventTag})",
                        Value = eventPattern.DateTime.Value.Difference(nextEvent.DateTime.Value),
                        Type = Model.IDE.ValueType.TimeToNext,
                        StartIdentifier = eventPattern.Identifier,
                        StopIdentifier = nextEvent.Identifier,
                        BaselineMin = _baseLine.Min,
                        BaselineMax = _baseLine.Max
                    };
                    // To keep the unique value of the column, do not include time in the name
                    //if (v.Value is TimeSpan d)
                    //    re.Name = $"{re.Name} - {d.ToShortReadableString()}";
                    re.Values.Add(v);
                }
            }
            #endregion TimeToNext

            #region TimeToOr
            if (eventPattern.Options.TimeToOr)
            {
                // Find the EventPattern that occurs first
                EventPattern toEventPattern = null;
                EventBaseLine toEventParameter = null;

                var parameters = eventPattern.Options.TimeToOrParameters.ToBaseLineList();

                foreach (var param in parameters)
                {
                    var tempEvent = GetNextEventByName(events, eventPattern, param.EventName);
                    if (toEventPattern == null || tempEvent.iLine < toEventPattern.iLine)
                    {
                        toEventPattern = tempEvent;
                        toEventParameter = param;
                    }
                }

                if (toEventPattern != null &&
                    eventPattern.DateTime.HasValue &&
                    toEventPattern.DateTime.HasValue)
                {
                    re.DurationStopIdentifiers.Add(toEventPattern.Identifier);

                    var v = new NameValue()
                    {
                        Name = $"{Keyword._TimeToOr}({toEventPattern.EventTag})",
                        Value = eventPattern.DateTime.Value.Difference(toEventPattern.DateTime.Value),
                        Type = ValueType.TimeToOr,
                        StartIdentifier = re.Identifier,
                        StopIdentifier = toEventPattern.Identifier,
                        BaselineMin = toEventParameter.Min,
                        BaselineMax = toEventParameter.Max
                    };
                    re.Values.Add(v);
                }
            }
            #endregion

            #region TimeTo
            if (eventPattern.Options.TimeTo)
            {
                var parameters = eventPattern.Options.TimeToParameters.ToBaseLineList();
                foreach (var param in parameters)
                {
                    var toEvent = GetNextEventByName(events, eventPattern, param.EventName);

                    if (toEvent != null &&
                        eventPattern.DateTime.HasValue &&
                        toEvent.DateTime.HasValue)
                    {
                        re.DurationStopIdentifiers.Add(toEvent.Identifier);

                        var v = new NameValue()
                        {
                            Name = $"{Keyword._TimeTo}({toEvent.EventTag})",
                            Value = eventPattern.DateTime.Value.Difference(toEvent.DateTime.Value),
                            Type = ValueType.TimeTo,
                            StartIdentifier = re.Identifier,
                            StopIdentifier = toEvent.Identifier,
                            BaselineMin = param.Min,
                            BaselineMax = param.Max
                        };
                        re.Values.Add(v);
                    }
                }
            }
            #endregion

            #region TimeFrom
            if (eventPattern.Options.TimeFrom)
            {
                var parameters = eventPattern.Options.TimeFromParameters.ToBaseLineList();

                foreach (var param in parameters)
                {
                    var fromEvent = GetPriorEventByName(events, eventPattern, param.EventName);

                    if (fromEvent != null &&
                        eventPattern.DateTime.HasValue &&
                        fromEvent.DateTime.HasValue)
                    {
                        FindEventResult(fromEvent, sourceEvents).DurationStopIdentifiers.Add(re.Identifier);

                        var v = new NameValue()
                        {
                            Name = $"{Keyword._TimeFrom}({fromEvent.EventTag})",
                            Value = fromEvent.DateTime.Value.Difference(eventPattern.DateTime.Value),
                            Type = ValueType.TimeFrom,
                            StartIdentifier = fromEvent.Identifier,
                            StopIdentifier = eventPattern.Identifier,
                            BaselineMin = param.Min,
                            BaselineMax = param.Max
                        };
                        re.Values.Add(v);
                    }
                }
            }
            #endregion
        }

        private void PostProces(PatternTestResults result)
        {
            foreach (var item in result.PatternSummaries)
            {
                foreach (var p in item.Patterns)
                {
                    var _totalIdleTime = new TimeSpan();
                    if (p.Options.IdleTimeOption)
                        _totalIdleTime = IdleTimePostProcess(p);

                    PostProcessFrequency(p, item);
                    PostProcessUnexpected(p.Events, item);
                    PostProcessKnownIdle(p.Events, item);
                    PostProcessDuration(p.Events, item);

                    #region Summary

                    if (p.Options.TotalTimeOption)
                    {
                        p.Events.Add(new PatternTestResultEventPattern($"{p.TotalDuration.ToFixedTimeString()} - {MonitorScriptService.TOTALTIME}",
                                                               EventType.TotalTime,
                                                               p.Path,
                                                               p.StartiLine,
                                                               p.EndiLine,
                                                               p.Events.Last().Line,
                                                               p.Events.Last().DateTime,
                                                               p.TotalDuration));

                        p.Name = $"{p.Name} {p.TotalDuration.ToShortReadableString()}";
                    }

                    #region TotalIdleTime
                    if (p.Options.IdleTimeOption)
                    {
                        p.Events.Add(new PatternTestResultEventPattern($"{_totalIdleTime.ToFixedTimeString()} - {MonitorScriptService.TOTALIDLETIME}",
                                                               EventType.TotalIdleTime,
                                                               p.Path,
                                                               p.StartiLine,
                                                               p.EndiLine,
                                                               p.Events.Last().Line,
                                                               p.Events.Last().DateTime,
                                                               _totalIdleTime));
                    }
                    #endregion TotalIdleTime

                    if (true) //p.Warnings.Count() > 0)
                    {
                        p.Events.Add(new PatternTestResultEventPattern($"     Warning - Stop not found ....",
                                                               EventType.Warning,
                                                               p.Path,
                                                               p.StartiLine,
                                                               p.EndiLine,
                                                               p.Events.Last().Line,
                                                               p.Events.Last().DateTime,
                                                               new TimeSpan()));

                    }

                    #endregion Summary
                }
            }
        }
        private TimeSpan IdleTimePostProcess(PatternTestResult p)
        {
            var model = new List<PatternTestResultEventPattern>();
            PatternTestResultEventPattern prior = null;
            DateTime startGAP = DateTime.MinValue;
            var _durationStops = new List<DurationColumn>();

            var _durationPattern = PrepareDurationPattern(p);
            var _maxDurationPatterns = _durationPattern.Count();

            var _totalKnownIdle = new TimeSpan();
            var _totalKnownIdleEvents = new List<PatternTestResultEventPattern>();
            var _totalUnknownIdle = new TimeSpan();
            var _totalUnKnownIdleEvents = new List<PatternTestResultEventPattern>();

            // TODO: We need to move CleanPlaceholders to their own list and then reference them when we are ready. - Sara
            // The issue is that we have added it to Events, and Events is processed,  Thus the PlaceHolders are going throug that process when we don't want them too.
            // Better to have their own list and then when needed, reference them for 'Clean'

            foreach (var e in p.Events)
            {
                ////
                // Idle Time from Prior EventPattern
                ////
                if (prior != null
                    && e.DateTime.HasValue
                    && e.DateTime != startGAP
                    && !e.Options.Hide
                    && !prior.Options.BodyStop)
                {
                    var _duration = e.DateTime.Value - startGAP;
                    var _caption = " - Idle Time*";
                    var _eventType = EventType.GapInside;

                    if (!_durationStops.Any())
                    {

                        _caption = $" - Idle Time ({prior.Name} to {e.Name})";
                        _eventType = EventType.GapOutside;
                        _totalUnknownIdle = _totalUnknownIdle.Add(_duration);
                    }

                    var ne = new PatternTestResultEventPattern($"{_duration.ToFixedTimeString()}{_caption}",
                                                         _eventType,
                                                         p.Path,
                                                         prior.EndiLine,
                                                         e.EndiLine,
                                                         null,
                                                         e.DateTime,
                                                         _duration);

                    // GAP - 
                    AddEvent(model, _durationStops, _durationPattern, _maxDurationPatterns, ne);

                    if (!_durationStops.Any())
                        _totalUnKnownIdleEvents.Add(ne);

                }

                // Duration has started
                if (e.DurationStopIdentifiers.Count > 0)
                    foreach (var ds in e.DurationStopIdentifiers)
                        _durationStops.Add(_durationPattern.FirstOrDefault(n => n.Start == e.Identifier && n.Stop == ds));

                AddEvent(model, _durationStops, _durationPattern, _maxDurationPatterns, e.Clone());

                var tempStops = _durationStops.Where(n => n.Stop == e.Identifier).ToList();

                if (!e.Options.Hide
                    && !(prior != null && prior.Options.BodyStop))
                {
                    foreach (var di in e.Values.Where(n => n.Type != Model.IDE.ValueType.Value))
                    {
                        switch (di.Type)
                        {
                            case ValueType.TimeTo:
                            case ValueType.TimeToNext:
                            case ValueType.TimeToNextKnownIdle:
                                // Duration has ended
                                // If the EventPattern Identifier is in _durationEvents, then the Duration has ended and it should be removed.
                                // Note: The given Identifier may be in the list more then once, if there are multiple durations that are ending.
                                _durationStops.RemoveAll(n => n.Stop == e.Identifier);
                                break;
                            case ValueType.TimeFrom:
                                if (_durationStops.Any(n => n.Stop == e.Identifier))
                                    _durationStops.AddRange(tempStops);
                                break;
                        }

                        if (di.Value is TimeSpan _d)
                        {
                            var eventName = string.Empty;
                            if (!string.IsNullOrEmpty(e.Name))
                                eventName = $"[{e.Name}] ";
                            var m = new PatternTestResultEventPattern($"{_d.ToFixedTimeString()} - {eventName}Duration:{di.Name}",
                                                                 di.Type == ValueType.TimeToNextKnownIdle ? EventType.IdleDuration : EventType.Duration,
                                                                 p.Path,
                                                                 p.Events.FirstOrDefault(n => n.Identifier == di.StartIdentifier).StartiLine,
                                                                 p.Events.FirstOrDefault(n => n.Identifier == di.StopIdentifier).StartiLine,
                                                                 null,
                                                                 e.DateTime,
                                                                 _d)
                            {
                                DurationStartIdentifier = di.StartIdentifier,
                                DurationStopIdentifier = di.StopIdentifier,
                                EventName = e.Name,
                            };

                            var _duration = (TimeSpan)di.Value;

                            if (di.BaselineMin.Ticks == 0 && di.BaselineMax.Ticks == 0)
                                m.NoBaseline = true;
                            else if (_duration < di.BaselineMin || _duration > di.BaselineMax)
                                m.OutsideBaseline = true;
                            else
                                m.WithinBaseline = true;

                            AddEvent(model, _durationStops, _durationPattern, di.BaselineMin, di.BaselineMax, _maxDurationPatterns, m);

                            if (di.Type == ValueType.TimeToNextKnownIdle)
                            {
                                _totalKnownIdle = _totalKnownIdle.Add(_d);
                                _totalKnownIdleEvents.Add(m);
                            }
                        }
                    }
                }

                // Duration has ended
                // If the EventPattern Identifier is in _durationEvents, then the Duration has ended and it should be removed.
                // Note: The given Identifier may be in the list more then once, if there are multiple durations that are ending.
                _durationStops.RemoveAll(n => n.Stop == e.Identifier);

                if (e.DateTime.HasValue && !e.Options.Hide)
                    startGAP = e.DateTime.Value;

                prior = e;
            }

            if (p.Options.KnownIdle)
            {
                var m = new PatternTestResultEventPattern($"{_totalKnownIdle.ToFixedTimeString()} - [Known Idle] Duration",
                                                     EventType.IdleDuration,
                                                     p.Path,
                                                     p.StartiLine,
                                                     p.StartiLine,
                                                     null,
                                                     p.Events[0].DateTime,
                                                     _totalKnownIdle)
                {
                    DurationStartIdentifier = p.Events[0].Identifier,
                    DurationStopIdentifier = p.Events[0].Identifier,
                    EventName = "Known Idle",
                    ChildEvents = _totalKnownIdleEvents
                };

                var parameters = p.Options.KnownIdleParameters.ToBaseLineList();

                if (parameters.Count == 1)
                {
                    var _min = parameters[0].Min;
                    var _max = parameters[0].Max;

                    if (_min.Ticks == 0 && _max.Ticks == 0)
                        m.NoBaseline = true;
                    else if (_totalKnownIdle < _min || _totalKnownIdle > _max)
                        m.OutsideBaseline = true;
                    else
                        m.WithinBaseline = true;

                    AddEvent(model, _durationStops, _durationPattern, _min, _max, _maxDurationPatterns, m);
                }
            }

            if (p.Options.UnknownIdle)
            {
                var m = new PatternTestResultEventPattern($"{_totalUnknownIdle.ToFixedTimeString()} - [Unknown Idle] Duration",
                                                     EventType.IdleDuration,
                                                     p.Path,
                                                     p.StartiLine,
                                                     p.StartiLine,
                                                     null,
                                                     p.Events[0].DateTime,
                                                     _totalUnknownIdle)
                {
                    DurationStartIdentifier = p.Events[0].Identifier,
                    DurationStopIdentifier = p.Events[0].Identifier,
                    EventName = "Unknown Idle",
                    ChildEvents = _totalUnKnownIdleEvents,
                    Duration = _totalUnknownIdle
                };

                var parameters = p.Options.UnknownIdleParameters.ToBaseLineList();

                if (parameters.Count == 1)
                {
                    var _min = parameters[0].Min;
                    var _max = parameters[0].Max;

                    if (_min.Ticks == 0 && _max.Ticks == 0)
                        m.NoBaseline = true;
                    else if (_totalUnknownIdle < _min || _totalUnknownIdle > _max)
                        m.OutsideBaseline = true;
                    else
                        m.WithinBaseline = true;

                    AddEvent(model, _durationStops, _durationPattern, _min, _max, _maxDurationPatterns, m);
                }
            }

            ProcessDurationOutsideInside(model, _durationPattern);

            p.Events = model;
            return _totalUnknownIdle;
        }
        private void AddEvent(List<PatternTestResultEventPattern> model, List<DurationColumn> stops, List<DurationColumn> durationPattern,
                              int max, PatternTestResultEventPattern eventPattern)
        {
            AddEvent(model, stops, durationPattern, new TimeSpan(), new TimeSpan(), max, eventPattern);
        }
        private void AddEvent(List<PatternTestResultEventPattern> model, List<DurationColumn> stops, List<DurationColumn> durationPattern,
                              TimeSpan baselineMin, TimeSpan BaselineMax, int max, PatternTestResultEventPattern eventPattern)
        {
            for (int i = 0; i < max; i++)
                eventPattern.DurationPatternForExcel.Add(stops.Any(n => n.Column == i));

            eventPattern.BaselineMin = baselineMin;
            eventPattern.BaselineMax = BaselineMax;

            // Add Time for Duration Outside or Inside
            switch (eventPattern.EventType)
            {
                case EventType.GapOutside:
                case EventType.GapInside:
                    var _leftFound = false;
                    foreach (var stop in stops)
                    {
                        var c = durationPattern[stop.Column];
                        if (!_leftFound)
                        {
                            _leftFound = true;
                            switch (eventPattern.EventType)
                            {
                                case EventType.GapOutside:
                                    c.DurationOutsideOfDuration = c.DurationOutsideOfDuration.Add(eventPattern.IdleTimeOutsideOfDuration);
                                    break;
                                case EventType.GapInside:
                                    c.DurationOutsideOfDuration = c.DurationOutsideOfDuration.Add(eventPattern.IdleTimeInsideOfDuration);
                                    break;
                                default:
                                    throw new Exception("EventType not supported!");
                            }
                        }
                        else
                        {
                            switch (eventPattern.EventType)
                            {
                                case EventType.GapOutside:
                                    c.DurationInsideOfDuration = c.DurationOutsideOfDuration.Add(eventPattern.IdleTimeOutsideOfDuration);
                                    break;
                                case EventType.GapInside:
                                    c.DurationInsideOfDuration = c.DurationInsideOfDuration.Add(eventPattern.IdleTimeInsideOfDuration);
                                    break;
                                default:
                                    throw new Exception("EventType not supported!");
                            }

                        }
                    }
                    break;
            }

            model.Add(eventPattern);
        }
        #endregion Process Methods
    }
}
