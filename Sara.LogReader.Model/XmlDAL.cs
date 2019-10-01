using System;
using System.Reflection;
using System.Threading;
using Sara.Common.DateTimeNS;
using Sara.Logging;
using Sara.LogReader.Model.Storage;

namespace Sara.LogReader.Model
{
    public static class XmlDal
    {
        private static string DataFile { get; set; }
        private static string CacheFile { get; set; }
        public static DataModel DataModel { get; set; }
        public static CacheModel CacheModel { get; set; }
        public static string folder { get; private set; }
        public static object PerformanceServices { get; private set; }
        public static event Action DataLoaded;

        public static void Save()
        {

            CacheModel.Save();
            DataModel.Save();
        }
        public static void Load()
        {
            DataFile = DataPath.GetDataFilePath();
            CacheFile = DataPath.GetCacheFilePath();

            var DataReset = new ManualResetEvent(false);
            var CacheReset = new ManualResetEvent(false);

            ThreadPool.QueueUserWorkItem(m =>
            {
                // ReSharper restore AssignNullToNotNullAttribute
                var t = new Stopwatch("Loading XML DataFile");
                DataModel = new DataModel(DataFile);
                DataModel.Load();
                t.Stop(0);
                DataReset.Set();
                DataLoaded?.Invoke();
            });

            ThreadPool.QueueUserWorkItem(m =>
            {
                try
                {
                    var t2 = new Stopwatch("Loading XML CacheFile");
                    CacheModel = new CacheModel(CacheFile);
                    CacheModel.Load();
                    t2.Stop(0);
                }
                catch (Exception)
                {
                    // Create an empty CacheModel
                    CacheModel = new CacheModel(CacheFile);
                    CacheModel.LoadPersistent();
                    CacheReset.Set();
                    Log.WriteError("Warning: The Cache was corrupted.  A New Cache file will be created.  No Data was lost.",typeof(XmlDal).Name, MethodBase.GetCurrentMethod().Name);
                }
                CacheReset.Set();
            });

            DataReset.WaitOne();
            CacheReset.WaitOne();
        }
    }
}
