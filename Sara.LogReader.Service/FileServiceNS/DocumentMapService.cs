using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Sara.Common.DateTimeNS;
using Sara.Logging;
using Sara.LogReader.Common;
using Sara.LogReader.Model;
using Sara.LogReader.Model.DocumentMap;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.Property;

namespace Sara.LogReader.Service.FileServiceNS
{
    public static class DocumentMapService
    {
        public static void BuildDocumentMap(FileData file)
        {
            lock (file.DocumentMap)
            {
                PerformanceService.StartEvent($"{PerformanceService.CONST_BuildDocumentMap}{file.Path}");
                try
                {
                    #region LazyLoad
                    if (file.IsCached_Lazy_DocumentMap)
                    {
                        Log.Write("Pulling DocumentMap from Cache",typeof(DocumentMapService).FullName, MethodBase.GetCurrentMethod().Name,
                            LogEntryType.Warning);
                        return;
                    }
                    #endregion

                    file.DocumentMap.Clear();
                    List<iLineMatch> events = new List<iLineMatch>();
                    var performanceResults = new List<EventPerformanceResult>();
                    var results = new List<DocumentEntry>();
                    int totalEvents;
                    events = GetLines(file, performanceResults, out totalEvents);
                    file.DocumentMap = ProcessLines(file, events);
                    BuildDocumentMapValues(file);
                    file.DocumentMap.AddRange(new BuildDocumentMapGapService().Execute(file));
                    file.DocumentMap = BuildDocumentMapGapService.PostProcess(file.DocumentMap.OrderBy(n => n.iLine).ThenBy(n => n.Sort).ToList());
                }
                finally
                {
                    file.IsCached_Lazy_DocumentMap = true;
                }
                PerformanceService.StopEvent($"{PerformanceService.CONST_BuildDocumentMap}{file.Path}");
            }
        }
        private static List<DocumentEntry> ProcessLines(FileData file, List<iLineMatch> events)
        {
            var sw = new Stopwatch($"Build DocumentMap ProcessLines for for \"{file.Path}\"");
            var result = new List<DocumentEntry>();
            try
            {
                var Lines = file.RawTextString.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();

                Parallel.ForEach(events, (match) =>
                {
                    var text = Lines[match.iLine];
                    var model = match.model as EventLR;
                    if (model == null)
                        throw new Exception($"{typeof(DocumentMapService).FullName}.{MethodBase.GetCurrentMethod().Name} model can never be null!");

                    #region Network EventPattern
                    if (model.Network != NetworkDirection.Na)
                    {
                        #region Network
                        var networkMessageName = EventService.FindNetworkMessageName(text);
                        switch (model.Network)
                        {
                            case NetworkDirection.Send:
                                networkMessageName = $"{Keywords.SEND_ABBR} {networkMessageName}";
                                break;
                            case NetworkDirection.SendBlocking:
                                networkMessageName = $"{Keywords.SEND_BLOCKING_ABBR} {networkMessageName}";
                                break;
                            case NetworkDirection.Receive:
                                networkMessageName = $"{Keywords.RECEIVE_ABBR} {networkMessageName}";
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        #endregion
                        var item = new DocumentEntry
                        {
                            Name = networkMessageName,
                            iLine = match.iLine,
                            Text = text.RemoveInvalidXMLChars(),
                            Value = networkMessageName,
                            Type = DocumentMapType.Network,
                            Id = model.EventId,
                            Level = DocumentMapLevel.Sibling,
                            Filtered = model.DocumentMapFiltered,
                        };
                        lock (result) result.Add(item);

                        return;
                    }
                    #endregion Network EventPattern

                    #region EventPattern
                    var item2 = new DocumentEntry
                    {
                        Name = model.Name,
                        iLine = match.iLine,
                        Text = text.RemoveInvalidXMLChars(),
                        Value = "",
                        Type = DocumentMapType.Event,
                        Id = model.EventId,
                        Level = model.Level,
                        Filtered = model.DocumentMapFiltered,
                        HighlightColor = model.DocumentMapHighlightColor == null ? "" : model.DocumentMapHighlightColor,
                        Sort = model.Sort
                    };
                    lock (result) result.Add(item2);
                    #endregion EventPattern
                });
            }
            catch (Exception ex)
            {
                Log.WriteError(typeof(DocumentMapService).FullName, MethodBase.GetCurrentMethod().Name, ex);
            }
            sw.Stop(0);
            return result;
        }
        public static List<iLineMatch> GetLines(FileData file, List<EventPerformanceResult> performanceResults, out int totalEvents)
        {
            var sw2 = new Stopwatch($"Build DocumentMap GetLines for \"{file.Path}\"");
            var e = XmlDal.DataModel.EventsModel.Events;
            var rawTextString = file.RawTextString;
            var _events = new List<iLineMatch>();
            totalEvents = e.Where(n => n.ContainsSourceType(file.SourceType) && n.DocumentMap).Count();
            //foreach (var item in new List<EventPattern>(e.Where(n => n.ContainsSourceType(file.SourceType) && n.DocumentMap)))
            //{
            //}
            Parallel.ForEach(new List<EventLR>(e.Where(n => n.ContainsSourceType(file.SourceType) && n.DocumentMap)), (item) =>
            {
                var sw = new Stopwatch($"Get Lines for Expression \"{item.RegularExpression}\"");
                var performanceResult = new EventPerformanceResult
                {
                    Name = item.Name,
                    Expression = item.RegularExpression,
                    EventId = item.EventId
                };
                var matches = RegularExpression.GetLines(item.RegularExpression, rawTextString, item, file.CachedNewLine);

                if (matches.Count > 0)
                    performanceResult.FirstiLine = matches[0].iLine;

                lock (_events)
                    _events.AddRange(matches);
                sw.Stop(1000);

                if (sw.Duration != null) performanceResult.DurationTS = sw.Duration.Value;
                performanceResult.Count = matches.Count;
                lock (performanceResults)
                    performanceResults.Add(performanceResult);
            });
            sw2.Stop(0);
            return _events;
        }
        private static void BuildDocumentMapValues(FileData file)
        {
            var stopwatch = new Stopwatch($"Build DocumentMap Values for \"{file.Path}\"");
            try
            {
                if (file == null)
                    return;

                var text = string.Empty;
                var lines =
                    file.RawTextString.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();

                Parallel.ForEach(file.FileValues.Where(item => item.DocumentMap), (item) =>
                {
                    if (lines.Count >= item.iLine - 1)
                        text = lines[item.iLine];

                    var value = ValueService.GetValueById(item.ValueId);
                    // Why would value be null, do we have another list that has an old reference that is no longer valid.
                    // I deleted a value and then GetValueByid returned a null value, thus I added this code
                    // TODO: I need to research why - Sara
                    if (value == null) return;

                    var filtered = value.DocumentMapFiltered;

                    var de = new DocumentEntry
                    {
                        Name = item.Name,
                        iLine = item.iLine,
                        Value = item.Value,
                        Type = DocumentMapType.Value,
                        Id = item.ValueId,
                        Level = item.Level,
                        Text = text,
                        Filtered = filtered,
                        Sort = item.Sort
                    };

                    lock (file.DocumentMap)
                        file.DocumentMap.Add(de);
                });
            }
            catch (Exception ex)
            {
                // We are updating the data in the background
                // Once the update is complete the system will trigger for this view to Render - Sara
                if (ex is InvalidOperationException && ex.Message == "Collection was modified; enumeration operation may not execute.")
                {
                    // Do Nothing
                }
                else
                    throw;
            }
            stopwatch.Stop(0);
        }
    }
}
