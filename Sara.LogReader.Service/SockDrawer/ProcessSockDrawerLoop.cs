using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Sara.Common.DateTimeNS;
using Sara.Common.Extension;
using Sara.Logging;
using Sara.LogReader.Model;
using Sara.LogReader.Model.Storage;
using Sara.LogReader.Service.FileServiceNS;
using Sara.WinForm.Notification;

namespace Sara.LogReader.Service.SockDrawer
{
    public class ProcessFileDetail
    {
        public TimeSpan TotalDuration { get; internal set; }
        public TimeSpan TotalTimeDuration { get; internal set; }
        public double AverageTimeMS { get; internal set; }
        public BuildDetail Detail { get; internal set; }
        public BuildInitializeFileDetail InitializeFileDetail { get; internal set; }

        public override string ToString()
        {
            return $@"TotalMS {Math.Round(TotalTimeDuration.TotalMilliseconds)} | AvgMS {Math.Round(AverageTimeMS)}
Total {TotalTimeDuration.ToShortReadableString()}
Total FileData Duration {TotalDuration.ToShortReadableString()}

{InitializeFileDetail}

{Detail}";
        }
    }
    public class CallbackModel
    {
        public Action<TimeSpan> UpdateCompletedCallback { get; set; }
        public Action<IStatusModel> StatusCallback { get; set; }
        public bool SearchSockDrawer { get; set; }
        public List<string> Files { get; set; }
        /// <summary>
        /// When True all files will have their FileInfoCached set to False
        /// </summary>
        public bool ForceClear { get; set; }
    }

    public class ProcessSockDrawerLoop
    {
        public CallbackModel Callback { get; set; }
        private DateTime _lastUpdate = DateTime.Now;

        public void Execute(List<string> files)
        {
            var stopwatch = new Stopwatch(MethodBase.GetCurrentMethod().Name, true);
            try
            {
                Callback.StatusCallback(StatusModel.StartStopWatch);

                var count = files.Count;
                var current = 0;

                var start = DateTime.Now;
                Parallel.ForEach(files, (filePath) =>
                {
                    Interlocked.Increment(ref current);

                    var fileStart = DateTime.Now;

                    var detail = new BuildInitializeFileDetail();                    
                    var file = XmlDal.CacheModel.GetFile(filePath, ref detail);
                    if (file == null)
                    {
                        Log.WriteError("ProcessSockDrawer.RunItem FilePath returned a null file!",typeof(ProcessSockDrawerLoop).FullName, MethodBase.GetCurrentMethod().Name);
                        return;
                    }

                    var result = new ProcessFileDetail();
                    result.InitializeFileDetail = detail;

                    var buildStart = DateTime.Now;
                    result.Detail = FileService.Build(file, false);
                    result.TotalDuration = DateTime.Now - fileStart;
                    result.TotalTimeDuration = DateTime.Now - start;
                    result.AverageTimeMS = result.TotalTimeDuration.TotalMilliseconds / current;
                    if (DateTimeExt.EverySecond(ref _lastUpdate))
                    {
                        var totalTimeMS = (DateTime.Now - start).TotalMilliseconds;
                        Callback.StatusCallback(StatusModel.Update($@"Processing FileData {current} of {count}",
                            result.ToString()
                            ));
                    }
                });

                Callback.StatusCallback(StatusModel.StopStopWatch);
            }
            finally
            {
                stopwatch.Stop();
            }
        }
    }
}
