using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Sara.Common.DateTimeNS;
using Sara.Common.Extension;
using Sara.LogReader.Model;
using Sara.WinForm.Notification;

namespace Sara.LogReader.Service.SockDrawer
{
    public class SearchSockDrawer
    {
        private int _found;
        private DateTime _lastUpdate = DateTime.Now;
        public Action<IStatusModel> StatusUpdate { get; set; }

        public void Execute(Action<IStatusModel> statusUpdate)
        {
            var stopWatch = new Stopwatch(MethodBase.GetCurrentMethod().Name);
            try
            {
                StatusUpdate = statusUpdate;
                if (!Directory.Exists(XmlDal.DataModel.Options.SockDrawerFolder))
                {
                    StatusUpdate(StatusModel.Error("Sock Drawer Folder is not configured!"));
                    return;
                }

                string[] files = GetFiles();

                CheckedForDeletedFiles(files);
                CheckForNewFiles(files);
            }
            finally
            {
                stopWatch.Stop();
            }
        }

        private static string[] GetFiles()
        {
            var logFiles = Directory.GetFiles(XmlDal.DataModel.Options.SockDrawerFolder, "*.log", SearchOption.AllDirectories).ToList();
            var txtFiles = Directory.GetFiles(XmlDal.DataModel.Options.SockDrawerFolder, "*.txt", SearchOption.AllDirectories).ToList();
            logFiles.AddRange(txtFiles);
            var files = logFiles.ToArray();
            return files;
        }

        private void CheckForNewFiles(string[] files)
        {
            var stopWatch = new Stopwatch(MethodBase.GetCurrentMethod().Name);
            try
            {
                StatusUpdate(StatusModel.Update(null, "Checking for New Files..."));
                DateTime _lastUpdate = DateTime.Now;

                // Using r.DefaultIfEmtpy will set the right values to null where there is no match - Sara
                var query = from p in files
                            join f in XmlDal.CacheModel.Files on p equals f.Path into r
                            from f in r.DefaultIfEmpty()
                            where f == null
                            select p;

                var test = query.ToList();
                var count = test.Count;
                int current = 0;

                Parallel.ForEach(test, (path) =>
                {
                    Interlocked.Increment(ref current);
                    if (DateTimeExt.EverySecond(ref _lastUpdate))
                        StatusUpdate(StatusModel.Update(null, $"Initializing New Files {current} of {count}"));
                    XmlDal.CacheModel.InitializeFile(path);
                });
            }
            finally
            {
                stopWatch.Stop();
            }
            StatusUpdate(StatusModel.AddPersistentDetail("Checking for New Files, Complete... {0}", stopWatch.Duration.Value.ToShortReadableString()));
        }

        /// <summary>
        /// Remove any files that are not found in the SockDrawer
        /// </summary>
        /// <param name="files"></param>
        private void CheckedForDeletedFiles(string[] files)
        {
            var stopWatch = new Stopwatch(MethodBase.GetCurrentMethod().Name);
            try
            {
                StatusUpdate(StatusModel.Update(null, "Checking for Deleted Files..."));
                DateTime _lastUpdate = DateTime.Now;

                // Using r.DefaultIfEmtpy will set the right values to null where there is no match - Sara
                var query = from f in XmlDal.CacheModel.Files
                            join p in files on f.Path equals p into r
                            from p in r.DefaultIfEmpty()
                            where p == null
                            select f.Path;

                var test = query.ToList();
                var count = test.Count;
                int current = 0;

                Parallel.ForEach(test, (path) => {
                    Interlocked.Increment(ref current);
                    if (DateTimeExt.EverySecond(ref _lastUpdate))
                        StatusUpdate(StatusModel.Update(null, $"Removing Deleted Files {current} of {count}"));
                    XmlDal.CacheModel.RemoveFile(path);
                });
            }
            finally
            {
                stopWatch.Stop();
            }
            StatusUpdate(StatusModel.AddPersistentDetail("Checking for Deleted Files, Complete... {0}",stopWatch.Duration.Value.ToShortReadableString()));
        }
    }
}
