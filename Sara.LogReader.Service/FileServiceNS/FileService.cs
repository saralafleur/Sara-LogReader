using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Sara.Common.DateTimeNS;
using Sara.Common.Extension;
using Sara.Logging;
using Sara.LogReader.Common;
using Sara.LogReader.Model;
using Sara.LogReader.Model.DocumentMap;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.ValueNS;

namespace Sara.LogReader.Service.FileServiceNS
{
    public class BuildSourceTypeDetail
    {
        public int SourceTypeEventCount { get; internal set; }
        public int SourceTypeEventScans { get; internal set; }
        public TimeSpan SourceTypeDuration { get; internal set; }
        public TimeSpan GetLineDuration { get; internal set; }

        public override string ToString()
        {
            return $@"SourceType EventPattern Count {SourceTypeEventCount}
SourceTypeEventScans {SourceTypeEventScans}
GetLine Duration {GetLineDuration.ToShortReadableString()}
SourceType Duration {SourceTypeDuration.ToShortReadableString()}";
        }
    }

    public class BuildNonLazyValuesDetail
    {
        public BuildNonLazyValuesDetail()
        {
            ValueDurations = new List<ValuePair>();
        }
        public bool FileCached { get; set; }
        public int Count { get; set; }
        public TimeSpan TotalDuration { get; internal set; }
        public List<ValuePair> ValueDurations { get; internal set; }
        public TimeSpan AddSourceTypeDuration { get; internal set; }

        public override string ToString()
        {
            var notation = string.Empty;
            if (FileCached)
                notation += @"Total
FileData Cached";

            var Values = string.Empty;

            foreach (var item in ValueDurations)
            {
                if (Values == string.Empty)
                    Values += $"v:{item.Name} = {item.Value}";
                else
                    Values += $@"
v:{item.Name} = {item.Value}";
            }

            return $@"{notation}Non Lazy Value(s) Count {Count}
{Values}
Add SourceType Duration {AddSourceTypeDuration.ToShortReadableString()}
Non Lazy Total Duration {TotalDuration.ToShortReadableString()}";
        }
    }
    public class BuildRangeDetail
    {
        public bool FileMissing { get; set; }
        public bool FileCached { get; set; }
        public bool FirstLineEmpty { get; set; }
        public bool LastLineEmpty { get; set; }
        public TimeSpan StartTimeDuration { get; internal set; }
        public TimeSpan StopTimeDuration { get; internal set; }

        public override string ToString()
        {
            var notation = string.Empty;
            if (FileMissing)
                notation += @"
FileData Missing";
            if (FileCached)
                notation += @"
FileData Cached";
            if (FirstLineEmpty)
                notation += @"
FileData Text First Lines is empty!";
            if (LastLineEmpty)
                notation += @"
FileData Text Last Lines is empty!";

            return $@"Build Start & Stop{notation}
StartTime Duration {StartTimeDuration.ToShortReadableString()}
StopTime Duration {StopTimeDuration.ToShortReadableString()}";
        }
    }
    public class BuildDetail
    {
        public BuildDetail()
        {
            RangeDetail = new BuildRangeDetail();
            NonLazyValuesDetail = new BuildNonLazyValuesDetail();
            SourceTypeDetail = new BuildSourceTypeDetail();
        }
        public bool FileMissing { get; set; }
        public bool FileCached { get; set; }
        public TimeSpan FastFileLoadDuration { get; internal set; }
        public BuildRangeDetail RangeDetail { get; internal set; }
        public TimeSpan StartAndStopDuruation { get; internal set; }
        public BuildSourceTypeDetail SourceTypeDetail { get; internal set; }
        public TimeSpan TitleDuration { get; internal set; }
        public TimeSpan FileLoadDuration { get; internal set; }
        public bool LazyLoad { get; internal set; }
        public TimeSpan BuildDuration { get; internal set; }
        public BuildNonLazyValuesDetail NonLazyValuesDetail { get; internal set; }
        public override string ToString()
        {
            var notation = string.Empty;
            if (FileMissing)
                notation += @"
FileData Missing";
            if (FileCached)
                notation += @"
FileData Cached";
            if (LazyLoad)
                notation += @"
Lazy Load";

            return $@"FILE BUILD SUMMARY{notation}
  Build Duration {BuildDuration.ToShortReadableString()}
  Fast FileData Load Duration {FastFileLoadDuration.ToShortReadableString()}
  {RangeDetail}
  Start & Stop Duration {StartAndStopDuruation.ToShortReadableString()}
  Title Duration {TitleDuration.ToShortReadableString()}
  FileData Load Duration {FileLoadDuration.ToShortReadableString()}

  { SourceTypeDetail}

  {NonLazyValuesDetail}";
        }
    }
    public static class FileService
    {
        public const string NO_CATEGORY_TEXT = "<No Category>";

        public static BuildDetail Build(FileData file, bool lazy)
        {
            var result = new BuildDetail();
            var built = false;
            var stopWatch = new Stopwatch($"{MethodBase.GetCurrentMethod().Name} - {Path.GetFileName(file.Path)}");
            try
            {
                lock (file)
                {

                    if (!System.IO.File.Exists(file.Path))
                    {
                        file.IsMissing = true;
                        result.FileMissing = true;
                        return result;
                    }

                    if (file.IsCached)
                    {
                        result.FileCached = true;
                        return result;
                    }

                    built = true;

                    var start = DateTime.Now;
                    file.FastLoad(100);
                    result.FastFileLoadDuration = DateTime.Now - start;

                    try
                    {
                        start = DateTime.Now;
                        result.RangeDetail = BuildStartAndStopDateTime(file);
                        result.StartAndStopDuruation = DateTime.Now - start;

                        file.IsCached = true;

                        result.SourceTypeDetail = BuildSourceType(file);
                        result.NonLazyValuesDetail = BuildNonLazyFileValues(file);

                        start = DateTime.Now;
                        BuildTitle(file);
                        result.TitleDuration = DateTime.Now - start;

                        // Lazy Load prevents the system from loading data until they are needed.
                        // When lazy is True the system is now ready for the data
                        if (lazy)
                        {
                            // NOTE: Lazy Load requires the entire file to be loaded, not just the First and Last Lines - Sara
                            start = DateTime.Now;
                            // Load will load the file text and unLoad will remove the file text from memory - Sara
                            if (file.IsLoaded)
                                file.SkipUnLoad = true;
                            else
                                file.Load(true);

                            result.FileLoadDuration = DateTime.Now - start;

                            BuildLazyFileValues(file);
                            BuildFoldingEvents(file);

                            file.IsCached_Values = true;
                        }
                        else
                            result.LazyLoad = true;
                    }
                    finally
                    {
                        if (file.SkipUnLoad)
                            file.SkipUnLoad = false;
                        else
                            file.UnLoad();
                    }
                }
            }
            finally
            {
                stopWatch.Stop(1000);

                if (built)
                {
                    file.ReplaceFileInfoValue(Fields.BUILD_LASTBUILD, DateTime.Now.ToString());
                    file.ReplaceFileInfoValue(Fields.BUILD_DURATION, stopWatch.Duration.ToString());
                    result.BuildDuration = stopWatch.Duration.Value;
                }
            }
            return result;
        }

        /// <summary>
        /// Builds the Network EventPattern Values for a given file
        /// </summary>
        public static void BuildNetworkMessages(FileData file)
        {
            // TODO: I need to make sure this runs in the background and does not delay the log view
            return;
            //if (file.IsCached_Lazy_NetworkMessages)
            //    return;

            //Log.Write(typeof(FileService).FullName, MethodBase.GetCurrentMethod().Name, LogEntryType.Trace, string.Format("BuildNetwork - \"{0}\"", file.Path));

            //var unload = false;
            //var stopwatch = new Stopwatch(MethodBase.GetCurrentMethod().Name);
            //try
            //{
            //    if (!file.IsLoaded)
            //    {
            //        file.Load(true);
            //        unload = true;
            //    }
            //    var loop = new BuildFileNetworkMessagesLoop();
            //    loop.Build(file);
            //}
            //finally
            //{
            //    if (unload)
            //        file.UnLoad();
            //    stopwatch.Stop(2000);
            //    file.IsCached_Lazy_NetworkMessages = true;
            //}
        }

        /// <summary>
        /// Builds the folding Events
        /// </summary>
        private static void BuildFoldingEvents(FileData file)
        {
            lock (file.FoldingEvents)
            {
                if (file.IsCached_Lazy_Events)
                    return;

                file.FoldingEvents.Clear();

                foreach (var e in XmlDal.DataModel.EventsModel.Events.Where(n => n.FoldingEventId != -1))
                {
                    var endingFolding = XmlDal.DataModel.EventsModel.Events.FirstOrDefault(n => n.EventId == e.FoldingEventId);

                    if (endingFolding == null)
                        continue;

                    file.FoldingEvents.Add(new FoldingEvent
                    {
                        StartingFolding = e.RegularExpression,
                        EndingFolding = endingFolding.RegularExpression
                    });
                }
                file.IsCached_Lazy_Events = true;
            }
        }
        /// <summary>
        /// Sets the Title of the file based on HostName and IP
        /// </summary>
        private static void BuildTitle(FileData file)
        {
            lock (file)
            {
                if (file.IsCached_Title)
                    return;

                var hostName = file.FindFileValue(Keywords.HOSTNAME);
                var ip = file.FindFileValue(Keywords.IP);
                if (hostName == null && ip == null)
                    file.Title = Keywords.SOURCE_TYPE_UNKNOWN;
                else
                {
                    if (hostName == null)
                        file.Title = ip;
                    else if (ip == null)
                        file.Title = hostName;
                    else
                        file.Title = $"{hostName}/{ip}";
                }
                file.IsCached_Title = true;
            }
        }

        /// <summary>
        /// Sets the Start and Stop for the file
        /// </summary>
        private static BuildRangeDetail BuildStartAndStopDateTime(FileData file)
        {

            var result = new BuildRangeDetail();
            try
            {
                lock (file)
                {
                    if (file.IsCached)
                    {
                        result.FileCached = true;
                        return result;
                    }

                    if (file.RawTextFirstLines.Count == 0)
                    {
                        result.FirstLineEmpty = true;
                        return result;
                    }

                    if (file.RawTextLastLines.Count == 0)
                    {
                        result.LastLineEmpty = true;
                        return result;
                    }

                    var stopWatch = new Stopwatch(MethodBase.GetCurrentMethod().Name, false);
                    try
                    {

                        /////
                        // START
                        /////
                        var start = DateTime.Now;
                        // Try DateTime first and then Date Keyword
                        var startDateTime = RegularExpression.GetFirstValue(file.RawTextStringFirstLines, "(?:\\A|\\r|\\n|\\r\\n)<(.*)>");
                        if (DateTimeExt.TryParseWithTimeZoneRemoval(startDateTime, out DateTime testDateTime))
                        {
                            file.Start = testDateTime;
                        }
                        result.StartTimeDuration = DateTime.Now - start;

                        /////
                        // END
                        /////
                        start = DateTime.Now;
                        var endDateTime = RegularExpression.GetFirstValue(file.RawTextStringLastLines, "(?:\\A|\\r|\\n|\\r\\n)<(.*)>");
                        var found = false;
                        if (DateTimeExt.TryParseWithTimeZoneRemoval(endDateTime, out testDateTime))
                        {
                            found = true;
                            file.End = testDateTime;
                        }
                        result.StopTimeDuration = DateTime.Now - start;

                        // We are looking for a large difference, so adding 30 seconds to the End will account for a 30 second window. - Sara
                        if (file.Start > file.End.Add(new TimeSpan(0, 0, 30)))
                            throw new DateTimeExt.InvalidDateRangeException(string.Format("Start: {0} is greater then End: {1}, End Found: {2}, endDateTime: {4}/n-----/n{3}",
                                                                file.Start,
                                                                file.End.Add(new TimeSpan(0, 0, 30)),
                                                                found,
                                                                file.RawTextStringLastLines,
                                                                endDateTime));
                    }
                    finally
                    {
                        stopWatch.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(typeof(FileService).FullName, MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }
        /// <summary>
        /// A EventPattern can be tag with a FileType.  Using such a EventPattern the system will determine the type of file.
        /// </summary>
        private static BuildSourceTypeDetail BuildSourceType(FileData file)
        {
            var result = new BuildSourceTypeDetail();
            lock (file)
            {
                var start = DateTime.Now;

                if (file.IsCached_SourceType)
                {
                    result.SourceTypeDuration = DateTime.Now - start;
                    return result;
                }

                // Default to Unknown
                file.SourceType = Keywords.SOURCE_TYPE_UNKNOWN;

                var stopWatch = new Stopwatch(MethodBase.GetCurrentMethod().Name, false);
                var count = 0;
                try
                {
                    var test = XmlDal.DataModel.EventsModel.Events.Where(n => n.SourceType != null).ToList();
                    result.SourceTypeEventCount = test.Count();
                    foreach (var e in test)
                    {
                        count++;
                        var firstMatch = RegularExpression.GetFirstMatch(file.RawTextStringFirstLines, e.RegularExpression);
                        if (firstMatch.Captures.Count == 0) continue;
                        var startGetLine = DateTime.Now;
                        file.SourceTypeiLine = RegularExpression.GetLine(file.RawTextStringFirstLines, firstMatch);
                        result.GetLineDuration = DateTime.Now - startGetLine;
                        file.SourceType = e.SourceType;
                        break;
                    }
                    XmlDal.CacheModel.Options.UpdateCachedSourceType(file.SourceType, file.Path);
                }
                finally
                {
                    file.IsCached_SourceType = true;
                    stopWatch.Stop();
                }
                result.SourceTypeEventScans = count;
                result.SourceTypeDuration = DateTime.Now - start;
                return result;
            }
        }
        /// <summary>
        /// Builds a list of FileData Values
        /// </summary>
        private static BuildNonLazyValuesDetail BuildNonLazyFileValues(FileData file)
        {
            var result = new BuildNonLazyValuesDetail();
            var start = DateTime.Now;

            lock (file.NonLazyValues)
            {
                if (file.IsCached_Values)
                {
                    result.FileCached = true;
                    return result;
                }

                var stopwatch = new Stopwatch(MethodBase.GetCurrentMethod().Name);
                try
                {
                    file.NonLazyValues.Clear();
                    var test = XmlDal.DataModel.ValuesModel.Values
                        .Where(n => !n.LazyLoad
                            && n.FileInfo
                            && n.ContainsSourceType(file.SourceType)).ToList();
                    
                    result.Count = test.Count;

                    Log.Write($"Searching for {test.Count} FileData Info Values",typeof(FileService).FullName, MethodBase.GetCurrentMethod().Name, LogEntryType.Warning);

                    foreach (var model in test)
                    {
                        var s1 = DateTime.Now;
                        var stopwatch2 = new Stopwatch("Non Lazy Value");
                        var valueResult = GetValue(file, model, false);
                        if (valueResult.Success)
                            file.NonLazyValues.AddRange(valueResult.Values);
                        var duration = stopwatch2.Stop(model.Name, 200);
                        if (duration >= XmlDal.DataModel.Options.NonLazyValueWarningTimeout)
                            NotificationService.Warning(
                                $@"WARNING: Non Lazy Value ""{model.Name}"" took more then {duration} seconds.  
Consider optimizing the Regular Expression.
Expression: '{model.Expression}'");
                        result.ValueDurations.Add(new ValuePair() { Name = model.Name, Value = (DateTime.Now - s1).ToShortReadableString() });
                    }

                    var addSourceTypeStart = DateTime.Now;
                    // Add SourceType
                    if (!string.IsNullOrEmpty(file.SourceType))
                    {
                        file.NonLazyValues.Add(new ValueBookMark
                        {
                            DocumentMap = false,
                            FileInfo = true,
                            iLine = file.SourceTypeiLine,
                            Level = DocumentMapLevel.Sibling,
                            Name = Keywords.SOURCE_TYPE,
                            Value = file.SourceType,
                            ValueId = -1
                        });
                    }
                    result.AddSourceTypeDuration = DateTime.Now - addSourceTypeStart;
                }
                finally
                {
                    file.IsCached_Values = true;
                    stopwatch.Stop(2000);
                }
                result.TotalDuration = DateTime.Now - start;
                return result;
            }
        }
        /// <summary>
        /// Builds a list of FileData Values
        /// </summary>
        private static void BuildLazyFileValues(FileData file)
        {
            lock (file.LazyValues)
            {
                if (file.IsCached_Lazy_Values)
                    return;

                var stopwatch = new Stopwatch(MethodBase.GetCurrentMethod().Name);
                try
                {
                    file.LazyValues.Clear();

                    foreach (var model in XmlDal.DataModel.ValuesModel.Values
                        .Where(n => n.LazyLoad || !n.FileInfo).Where(n => n.ContainsSourceType(file.SourceType)))
                    {
                        var result = GetValue(file, model, true);
                        if (result.Success)
                            file.LazyValues.AddRange(result.Values);
                    }
                }
                finally
                {
                    file.IsCached_Lazy_Values = true;
                    stopwatch.Stop();
                }
            }
        }
        /// <summary>
        /// Performs a Regular Expression search for a given value
        /// </summary>
        /// <returns></returns>
        private static ValueBookMarkModel GetValue(FileData file, Value model, bool lazyLoad)
        {
            if (String.IsNullOrEmpty(model.Expression))
                return new ValueBookMarkModel { Success = false };

            Stopwatch stopwatch = null;

            if (!model.Distinct)
                stopwatch = new Stopwatch(MethodBase.GetCurrentMethod().Name);
            try
            {
                TimeSpan duration;
                if (lazyLoad)
                    // This will load the entire file!
                    return GetRegularExpressionValue(model, file.RawTextString, file.CachedNewLine, out duration);
                else
                    // This will use only the First 100 Lines, thus much faster.
                    // However this means that the Non Lazy Value must be found in the first 100!
                    return GetRegularExpressionValue(model, file.RawTextStringFirstLines, file.CachedNewLineFirstLines, out duration);
            }
            finally
            {
                if (!model.Distinct && stopwatch != null)
                {
                    stopwatch.Stop($"WARNING: Non-Distinct FileValue exceeded 200ms to load! - {model.Expression}", 200);
                }
            }

        }
        /// <summary>
        /// Looks for Values in the FileData
        /// </summary>
        public static ValueBookMarkModel GetRegularExpressionValue(Value model, string value, List<int> cachedNewLine, out TimeSpan duration)
        {
            var sw3 = new Stopwatch($"{MethodBase.GetCurrentMethod().Name} - {(string.IsNullOrEmpty(model.Name) ? model.Expression : model.Name)}");
            var result = new ValueBookMarkModel();

            if (string.IsNullOrEmpty(model.Expression))
            {
                result.Success = false;
                sw3.Stop();
                duration = sw3.Duration.Value;
                return result;
            }

            try
            {
                if (model.Distinct)
                {

                    var firstMatch = RegularExpression.GetFirstMatch(value, model.Expression);
                    if (!firstMatch.Success)
                    {
                        result.Success = false;
                        sw3.Stop();
                        duration = sw3.Duration.Value;
                        return result;
                    }

                    var test = string.Empty;
                    if (firstMatch.Groups.Count == 2)
                        test = firstMatch.Groups[1].ToString();

                    result.Values.Add(new ValueBookMark
                    {
                        ValueId = model.ValueId,
                        FileInfo = model.FileInfo,
                        DocumentMap = model.DocumentMap,
                        Name = model.Name,
                        iLine = RegularExpression.GetLine(value, firstMatch),
                        Value = test,
                        Level = model.Level
                    });
                    result.Success = true;
                }
                else
                {
                    var iLines = RegularExpression.GetLines(model.Expression, value, null, cachedNewLine).ToList();

                    if (iLines.Count == 0)
                    {
                        result.Success = false;
                        sw3.Stop();
                        duration = sw3.Duration.Value;
                        return result;
                    }

                    foreach (var item in iLines)
                    {
                        result.Values.Add(new ValueBookMark
                        {
                            ValueId = model.ValueId,
                            FileInfo = model.FileInfo,
                            DocumentMap = model.DocumentMap,
                            Name = model.Name,
                            Level = model.Level,
                            iLine = item.iLine,
                            Value = item.Value
                        });

                    }
                    result.Success = true;
                }
            }
            catch (Exception)
            {
                result.Success = false;
            }
            sw3.Stop();
            duration = sw3.Duration.Value;
            return result;
        }

        public static void ResetFileNetworkData()
        {
            foreach (var file in XmlDal.CacheModel.Files)
            {
                file.IsCached_Lazy_NetworkMessages = false;
            }
        }

        public static int RemoveDuplicateFiles()
        {
            var sockDrawerFolder = XmlDal.DataModel.Options.SockDrawerFolder;

            var parentPath = Path.Combine(Directory.GetParent(sockDrawerFolder).FullName, "Sock Drawer Duplicates");


            var test = XmlDal.CacheModel.Files.Select(file => new
            {
                file.Path,
                DupData = file.GetDuplicateText()
            }).ToList();

            var moved = 0;

            for (var i = test.Count - 1; i >= 0; i--)
            {
                var target = test[i];
                foreach (var file in test)
                {
                    if (target.Path == file.Path)
                        continue;

                    if (target.DupData != file.DupData) continue;


                    var fileName = target.Path.Contains(sockDrawerFolder) ? target.Path.Remove(0, sockDrawerFolder.Count() + 1) : Path.GetFileName(target.Path);

                    if (fileName == null)
                        continue;

                    var destPath = Path.Combine(parentPath, fileName);
                    var destDir = Path.GetDirectoryName(destPath);

                    if (destDir == null)
                        continue;

                    if (!Directory.Exists(destDir))
                        Directory.CreateDirectory(destDir);


                    Log.Write($"Moving \"{target.Path}\" to \"{destPath}\"",typeof(FileService).FullName, MethodBase.GetCurrentMethod().Name, LogEntryType.Warning);

                    // If the file was does not exists on the Disk, then we can't move it - Sara
                    if (System.IO.File.Exists(target.Path) && !System.IO.File.Exists(destPath))
                        System.IO.File.Move(target.Path, destPath);
                    moved++;
                    break;
                }
            }
            return moved;
        }

        public static void ResetFileNetworkLinks()
        {
            foreach (var msg in XmlDal.CacheModel.Files.SelectMany(file => file.Network.NetworkMessages))
            {
                msg.IsCached_Targets = false;
            }
        }
    }
}
