using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Sara.Common.DateTimeNS;
using Sara.Common.Extension;
using Sara.Logging;
using Sara.LogReader.Model;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.LogReaderNS;
using Sara.WinForm.Notification;

namespace Sara.LogReader.Service.SockDrawer
{
    public static class SockDrawerService
    {
        public static SockDrawerModel GetModel(DocumentModel model)
        {
            Log.WriteTrace("Entering SockDrawerModel.GetModel",typeof(SockDrawerService).FullName, MethodBase.GetCurrentMethod().Name);
            try
            {
                var data = XmlDal.CacheModel;

                if (model == null)
                    return new SockDrawerModel { ActiveFile = null, Files = data.Files };

                if (model.File == null)
                    throw new Exception("model.FileData should never be null");

                return new SockDrawerModel { ActiveFile = model.File, Files = data.Files };
            }
            finally
            {
                Log.WriteTrace("Exiting SockDrawerModel.GetModel",typeof(SockDrawerService).FullName, MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Queues a Thread to Update the SockDrawer
        /// </summary>
        public static void Update(Action<TimeSpan> updateCompletedCallback, Action<IStatusModel> statusCallback, bool forceClear)
        {
            Validate();
            statusCallback(StatusModel.Update("Updating Sock Drawer..."));

            ThreadPool.QueueUserWorkItem(UpdateAsync, new CallbackModel
            {
                UpdateCompletedCallback = updateCompletedCallback,
                StatusCallback = statusCallback,
                SearchSockDrawer = true,
                Files = XmlDal.CacheModel.Files.Select(n => n.Path).ToList(),
                ForceClear = forceClear
            });
            XmlDal.Save();
        }

        /// <summary>
        /// Queues a Thread to Update the SockDrawer
        /// </summary>
        public static void Update(Action<TimeSpan> updateCompletedCallback, Action<IStatusModel> statusCallback, List<string> files, bool forceClear)
        {
            Validate();
            statusCallback(StatusModel.Update("Update Active Starting", null));

            ThreadPool.QueueUserWorkItem(UpdateAsync, new CallbackModel
            {
                UpdateCompletedCallback = updateCompletedCallback,
                StatusCallback = statusCallback,
                SearchSockDrawer = false,
                Files = files,
                ForceClear = forceClear
            });
            XmlDal.Save();
        }

        private static void ClearCache(CallbackModel model)
        {
            var stopWatch = new Stopwatch(MethodBase.GetCurrentMethod().Name);
            try
            {
                if (model.ForceClear)
                {
                    var count = 0;
                    var _lastUpdate = DateTime.Now;
                    var total = model.Files.Count();

                    var query = from f in XmlDal.CacheModel.Files
                                join p in model.Files on f.Path equals p
                                select f;

                    foreach (var file in query)
                    {
                        Interlocked.Increment(ref count);
                        if (DateTimeExt.EverySecond(ref _lastUpdate))
                            model.StatusCallback(StatusModel.Update(null, $"Clearing FileData Cache {count} of {total}"));

                        if (file != null)
                            file.Clear();
                    }
                }
            }
            finally
            {
                stopWatch.Stop();
            }
            model.StatusCallback(StatusModel.AddPersistentDetail("Clearing FileData Cache, Complete... {0}", stopWatch.Duration.Value.ToShortReadableString()));
        }

        private static void UpdateAsync(object stateInfo)
        {
            Thread.CurrentThread.Name = "UpdateSockDrawer";
            var start = DateTime.Now;
            var model = (CallbackModel)stateInfo;
            model.StatusCallback(StatusModel.ClearPersistentDetail);
            model.StatusCallback(StatusModel.HideEstimatedTime);
            model.StatusCallback(StatusModel.Update(null, "Clearing FileData Cache..."));
            ClearCache(model);
            model.StatusCallback(StatusModel.Update(null, "Searching Sock Drawer for Files..."));

            if (model.SearchSockDrawer)
            {
                new SearchSockDrawer().Execute(model.StatusCallback);
                model.Files = XmlDal.CacheModel.Files.Where(n => !n.IsCached).Select(n => n.Path).ToList();
            }

            new ProcessSockDrawerLoop { Callback = model }.Execute(model.Files);

            TimeSpan duration = start.Difference(DateTime.Now);
            model.StatusCallback(StatusModel.Update(string.Format("Update Active completed taking {0}", duration.ToReadableString())));
            model.UpdateCompletedCallback(duration);
        }
        private static void Validate()
        {
            if (string.IsNullOrEmpty(XmlDal.DataModel.Options.SockDrawerFolder))
            {
                throw new Exception(
                    "You must configure the Sock Drawer Folder first.  To configure the Sock Drawer Folder, click on Settings.");
            }
        }

    }
}
