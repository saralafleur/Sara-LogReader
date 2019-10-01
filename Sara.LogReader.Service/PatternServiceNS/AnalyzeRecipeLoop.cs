using System;
using System.Collections.Generic;
using System.Linq;
using Sara.Common.Debug;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.PatternNS;
using Sara.LogReader.Model.RecipeReport;
using Sara.WinForm.Notification;

namespace Sara.LogReader.Service.PatternServiceNS
{
    public class AnalyzeRecipeArgs
    {
        public Action<IStatusModel> StatusUpdate { get; set; }
        public RecipeReportSelectionModel Model { get; set; }
        public RecipeReportResultCacheData Report { get; set; }
        public volatile bool IsCancelled;
    }


    public class AnalyzeRecipeLoop
    {
        private Trace _run;
        private Trace _load;
        private Trace _analyseFile;
        private Trace _unload;

        private List<Pattern> _recipies;

        public AnalyzeRecipeLoop()
        {
            _recipies = new List<Pattern>();
            _run = new Trace();
            _load = new Trace();
            _analyseFile = new Trace();
            _unload = new Trace();
        }

        public AnalyzeRecipeArgs Args { get; set; }

        //private long _found;
        private static void EnsureDistinct(RecipeReportItem rr)
        {
            // Now make each DistinctName distinct again by adding a number at the end
            var foundName = new List<string>();
            foreach (var col in rr.Anomalies)
            {
                foundName.Add(col.DistinctName);
                if (foundName.Contains(col.DistinctName))
                {
                    foundName.Add(col.DistinctName);
                    var count = foundName.Count(n => n == col.DistinctName);
                    col.DistinctName = $"{col.DistinctName} {count - 1}";
                }
            }
            foreach (var col in rr.Values)
            {
                foundName.Add(col.DistinctName);
                if (foundName.Contains(col.DistinctName))
                {
                    foundName.Add(col.DistinctName);
                    var count = foundName.Count(n => n == col.DistinctName);
                    col.DistinctName = $"{col.DistinctName} {count - 1}";
                }
            }
        }

        //private DateTime _lastProgressUpdate;

        //private int _testTotal;
        //private int _testCurrent;
        //private int _totalWorkers = 0;

        private void RunRecipe(Pattern recipe, FileData file)
        {
            return;
            //var iLine = 0;
            //var rt = new RealTimePatternScanService();
            //var cr = new PatternComplex(recipe.Name, ScanType.Repeating, delegate (IPattern item)
            //{
            //    #region Body
            //    Interlocked.Increment(ref _found);
            //    var r = item as PatternComplex;
            //    if (r == null)
            //        throw new Exception("item must be of type ComplexRecipe!");

            //    var rr =
            //        new RecipeReportItem
            //        {
            //            StartingiLine = r.Events.First().iLine,
            //            EndingiLine = r.Events.Last(n => n.Found).iLine,
            //            Path = file.Path,
            //            RecipeId = recipe.PatternId,
            //            RecipeName = recipe.Name
            //        };

            //    #region Get Start & End
            //    var startProperty = PropertyService.GetProperty(new LineArgs { Path = file.Path, iLine = rr.StartingiLine, LineFS = file.RawText[rr.StartingiLine] });
            //    DateTime startTest;
            //    DateTime? start = null;
            //    if (DateTime.TryParse(startProperty.FindPropertyValue(Keywords.DATETIME), out startTest))
            //    {
            //        start = startTest;
            //    }

            //    var endProperty = PropertyService.GetProperty(new LineArgs { Path = file.Path, iLine = rr.EndingiLine, LineFS = file.RawText[rr.EndingiLine] });
            //    DateTime endTest;
            //    DateTime? end = null;
            //    if (DateTime.TryParse(endProperty.FindPropertyValue(Keywords.DATETIME), out endTest))
            //    {
            //        end = endTest;
            //    }
            //    #endregion

            //    rr.StartTime = start;
            //    if (start.HasValue && end.HasValue)
            //    {
            //        rr.Duration = start.Value.Difference(end.Value);
            //    }

            //    foreach (var step in r.Events)
            //    {
            //        if (step.Values.Count == 0) continue;

            //        var property = PropertyService.GetProperty(new LineArgs
            //        {
            //            Path = file.Path,
            //            iLine = step.iLine,
            //            LineFS = file.RawText[step.iLine]
            //        });

            //        foreach (var value in step.Values)
            //        {
            //            var v = property.FindPropertyValue(value);

            //            if (v != null)
            //            {
            //                rr.Values.Add(new RecipeColumn
            //                {
            //                    DistinctName = value,
            //                    Value = v,
            //                    StartingiLine = step.iLine,
            //                    EndingiLine = step.iLine
            //                });
            //            }
            //        }
            //    }

            //    if (recipe.FindGap)
            //    {
            //        for (var i = rr.StartingiLine + 1; i <= rr.EndingiLine; i++)
            //        {
            //            TimeSpan diff;
            //            if (!BuildDocumentMapGaps.HasGap(file.RawText[i - 1], file.RawText[i], recipe.FindGapMs,
            //                out diff))
            //                continue;

            //            var property = PropertyService.GetProperty(new LineArgs
            //            {
            //                Path = file.Path,
            //                iLine = i,
            //                LineFS = file.RawText[i]
            //            });
            //            if (property.EventIds.Count == 0)
            //                continue;

            //            var distinctName = EventService.GetFirstName(property);

            //            rr.Anomalies.Add(new RecipeColumn
            //            {
            //                DistinctName = distinctName ?? "GAP",
            //                Duration = diff,
            //                Value = diff.TotalSeconds.ToString(CultureInfo.InvariantCulture),
            //                StartingiLine = i - 1,
            //                EndingiLine = i
            //            });
            //        }
            //    }

            //    EnsureDistinct(rr);
            //    rr.Group = file.GetGroupFolder(XmlDal.DataModel.Options.SockDrawerFolder);
            //    Args.Report.Results.Add(rr);
            //    #endregion Body
            //});

            //foreach (var step in recipe.Steps)
            //{
            //    var e = XmlDal.DataModel.GetEvent(step.EventId);
            //    if (e == null)
            //    {
            //        Log.Write(typeof(AnalyzeRecipeLoop).FullName, MethodBase.GetCurrentMethod().Name, LogEntryType.Error, string.Format("Unable to locate EventId: {0}", step.EventId));
            //        continue;
            //    }

            //    cr.AddEvent(new EventPattern(step.EventType, e.RegularExpression, step.EventText, step.EventOption, step.Values) { IsRegularExpression = true });
            //}
            //rt.AddPattern(cr);

            //rt.Start();

            //foreach (var line in file.RawText)
            //{
            //    rt.AnalyzeLine(new ScanLineArgs()
            //    {
            //        TimeStamp = DateTime.Now,
            //        LineFS = line,
            //        iLine = iLine,
            //        LastLine = iLine == file.RawText.Count - 1
            //    });
            //    iLine++;
            //}

            //rt.Stop();
            //// Setting rt to null forces the garrabe collection to happen sooner. - Sara
            //// ReSharper disable once RedundantAssignment
            //rt = null;
        }

        private void RunFile(FileData file)
        {
//            Interlocked.Increment(ref _totalWorkers);
//            try
//            {
//                if (Args.IsCancelled)
//                    return;

//                var _startRun = DateTime.Now;

//                var wasLoaded = false;
//                try
//                {
//                    var _startLoad = DateTime.Now;

//                    if (!file.IsLoaded)
//                    {
//                        file.Load(true);
//                        wasLoaded = true;
//                    }
//                    _load.Add(_startLoad);

//                    var _startAnalyseFile = DateTime.Now;
//                    foreach (var recipe in _recipies)
//                    {
//                        if (file.SourceType == recipe.SourceType)
//                            RunRecipe(recipe, file);
//                    }
//                    _analyseFile.Add(_startAnalyseFile);
//                }
//                finally
//                {
//                    var _startUnLoad = DateTime.Now;
//                    if (wasLoaded)
//                        file.UnLoad();
//                    _unload.Add(_startUnLoad);
//                }

//                _run.Add(_startRun);

//                var AdditionalThreadInfo = string.Format(@"
//            {0,-5} Analyse FileData(ms)
//            {1,-5} Load FileData(ms)
//            {3,-5} Unload FileData(ms),
//            {2,-5} Total(ms)
//Total Workers: {4},
//Items per Second: {5}",
//                    _analyseFile.AverageDurationMS,
//                    _load.AverageDurationMS,
//                    _run.AverageDurationMS,
//                    _unload.AverageDurationMS,
//                    _totalWorkers,
//                    (_testCurrent / (DateTime.Now - _startExecute).TotalSeconds).ToString());

//                Interlocked.Increment(ref _testCurrent);
//                lock (this)
//                {
//                    // Only update every 1/2 second
//                    if ((DateTime.Now - _lastProgressUpdate).TotalMilliseconds > 1000)
//                    {
//                        Args.StatusUpdate(StatusModel.Update("Recipe Report", string.Format(@"Analyzing {0} of {1}, found: {2}, {3}",
//                            _testCurrent, _testTotal, Interlocked.Read(ref _found), AdditionalThreadInfo),
//                            _testTotal, _testCurrent));
//                        _lastProgressUpdate = DateTime.Now;
//                    }
//                }
//            }
//            finally
//            {
//                Interlocked.Decrement(ref _totalWorkers);
//            }

        }

        //private DateTime _startExecute;
        public void Execute(out bool cancelled)
        {
            cancelled = false;
            //Args.StatusUpdate(StatusModel.ClearPersistentDetail);

            //_lastProgressUpdate = DateTime.Now;
            //Args.IsCancelled = false;
            //// Save for when we want to re-run the report
            //Args.Report.Files = Args.Model.Files;
            //Args.Report.Recipes = Args.Model.Recipes;

            //Args.StatusUpdate(StatusModel.Update("Preparing data..."));

            //var _startPrepare = DateTime.Now;
            //_recipies = (from r in Args.Model.Recipes
            //             where r.Selected
            //             select XmlDal.DataModel.GetRecipe(r.RecipeId)).ToList();

            //var _files = (from r in _recipies
            //              join f in XmlDal.CacheModel.Files
            //              on r.SourceType equals f.SourceType
            //              join f2 in Args.Model.Files
            //              on f.Path equals f2
            //              select f).ToList();
            //_testTotal = _files.Count();
            //Args.StatusUpdate(StatusModel.AddPersistentDetail(string.Format("1. Preparing List Complete... {0}", (DateTime.Now - _startPrepare).ToReadableString())));
            //try
            //{
            //    Args.StatusUpdate(StatusModel.Update("1. Processing data..."));

            //    _startExecute = DateTime.Now;
            //    Parallel.ForEach(_files, RunFile);

            //    if (!Args.IsCancelled)
            //        XmlDal.CacheModel.Save();

            //    cancelled = Args.IsCancelled;
            //}
            //finally
            //{
            //    Args.StatusUpdate(StatusModel.Completed);
            //}
        }
    }
}
