using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sara.Cache;
using Sara.Cache.DataStore;
using Sara.Common.DateTimeNS;
using Sara.Common.Extension;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.LogReaderNS;
using Sara.LogReader.Model.NetworkReport;
using Sara.LogReader.Model.RecipeReport;

namespace Sara.LogReader.Model.Storage
{
    public class BuildInitializeFileDetail
    {
        public TimeSpan LockWaitDuration { get; internal set; }
        public TimeSpan GetDataDuration { get; internal set; }
        public TimeSpan InitializeDuration { get; internal set; }
        public TimeSpan InitializeFileDuration { get; internal set; }

        public override string ToString()
        {
            return $@"Lock Wait {LockWaitDuration.ToShortReadableString()}
GetData {GetDataDuration.ToShortReadableString()}
Initialize {InitializeDuration.ToShortReadableString()}
Initialize Total {InitializeFileDuration.ToShortReadableString()}";
        }
    }
    public class CacheModel
    {
        public FavoriteFileCache Favorites
        {
            get
            {
                lock (Controller)
                {
                    var cacheDataController = Controller.GetData(typeof(FavoriteFileCache)).FirstOrDefault();
                    if (cacheDataController != null)
                        return cacheDataController.Data as FavoriteFileCache;

                    throw new NullReferenceException("FavoriteFileCache was not found in the Cache!");
                }
            }
        }
        public List<FileData> Files
        {
            get
            {
                return Controller.GetData(typeof(FileData)).Select(model => model.InternalData as FileData).ToList();
            }
        }

        #region Network Report
        public List<NetworkReportResultCacheData> NetworkReports
        {
            get
            {
                return Controller.GetData(typeof(NetworkReportResultCacheData)).Select(model => model.InternalData as NetworkReportResultCacheData).ToList();
            }
        }
        public NetworkReportResultCacheData GetNetworkReport(string name)
        {
            return GetReport<NetworkReportResultCacheData>(name);
        }
        public void RemoveNetworkReport(string key)
        {
            RemoveReport<NetworkReportResultCacheData>(key);
        }
        #endregion Network Report

        #region Recipe Report
        public List<RecipeReportResultCacheData> RecipeReports
        {
            get
            {
                return Controller.GetData(typeof(RecipeReportResultCacheData)).Select(model => model.InternalData as RecipeReportResultCacheData).ToList();
            }
        }
        public RecipeReportResultCacheData GetRecipeReport(string name)
        {
            var test = GetReport<RecipeReportResultCacheData>(name);
            return test;
        }
        public void RemoveRecipeReport(string key)
        {
            RemoveReport<RecipeReportResultCacheData>(key);
        }
        #endregion Recipe Report

        private CacheDataController InitializeReport<T>(string name) where T : class, ICacheData
        {
            lock (Controller)
            {
                var cacheDataController = Controller.GetData(name, typeof(T)).FirstOrDefault() ??
                                          CacheFactory.CreateCacheData(Controller, typeof(T));

                if (cacheDataController == null)
                    return null;

                if (cacheDataController.InternalData.Key == null)
                    cacheDataController.InternalData.Key = name;

                var internalReport = cacheDataController.InternalData as T;
                cacheDataController.Initialize(internalReport);

                return cacheDataController;
            }
        }
        public T GetReport<T>(string name) where T : class, ICacheData
        {
            var stopwatch = new Stopwatch(MethodBase.GetCurrentMethod().Name, false);
            try
            {
                lock (Controller)
                {
                    var cacheDataController = InitializeReport<T>(name);

                    if (cacheDataController == null)
                        return null;

                    return cacheDataController.Data as T;
                }
            }
            finally
            {
                stopwatch.Stop();
            }
        }
        public void RemoveReport<T>(string key) where T : class
        {
            lock (Controller)
            {
                var cacheDataController = Controller.GetData(key, typeof(T)).FirstOrDefault();
                if (cacheDataController == null)
                    return;

                Controller.Remove(cacheDataController);
            }
        }

        public static CacheController Controller { get; set; }
        private static string CacheFilePath { get; set; }

        public CacheModel(string cacheFilePath)
        {
            CacheFilePath = cacheFilePath;
        }

        public void Load()
        {
            Controller = new CacheController();
            var dataStore = new XmlCacheDataStore { Path = CacheFilePath };
            Controller.SetupDataStore(dataStore);
            Controller.Load();

            XmlDal.DataModel.WaitForLoad();

            foreach (var model in Controller.Cache.Where(model => model.InternalType == typeof(FileData)))
            {
                PrepareFileEvents(model);
            }

            LoadPersistent();
        }

        public void LoadPersistent()
        {
            if (Controller.EmptyByType(typeof(SettingsCacheData)))
                CacheFactory.CreateCacheData(Controller, typeof(SettingsCacheData));
            if (Controller.EmptyByType(typeof(FavoriteFileCache)))
                CacheFactory.CreateCacheData(Controller, typeof(FavoriteFileCache));
        }

        public void RemoveFavoriteFile(string filePath, string favoriteGroup)
        {
            var item = Favorites.Files.FirstOrDefault(n => n.Path == filePath && n.FavoriteGroup == favoriteGroup);
            if (item == null) return;
            Favorites.Files.Remove(item);
        }

        public void Save()
        {
            Controller.Save();
        }

        #region Settings

        public SettingsCacheData Options
        {
            get
            {
                lock (Controller)
                {
                    var cacheDataController = Controller.GetData(typeof(SettingsCacheData)).FirstOrDefault();
                    if (cacheDataController != null)
                        return cacheDataController.Data as SettingsCacheData;
                    throw new NullReferenceException("Options was not found in the Cache!");
                }
            }
        }
        #endregion

        #region FileData

        /// <summary>
        /// Returns True if there is no FileData for the given path
        /// </summary>
        public bool EmptyFile(string path)
        {
            lock (Controller)
            {
                return Controller.EmptyByTypeAndKey(typeof(FileData), path);
            }
        }

        /// <summary>
        /// Removes a FileData from the Cache
        /// </summary>
        public void RemoveFile(string path)
        {
            lock (Controller)
            {
                var cacheDataController = Controller.GetData(path, typeof(FileData)).FirstOrDefault();
                if (cacheDataController == null)
                    return;

                Controller.Remove(cacheDataController);
            }
        }

        /// <summary>
        /// Initializes a FileData in the Cache
        /// Does not Load the file
        /// </summary>
        /// <param name="path"></param>
        public CacheDataController InitializeFile(string path)
        {
            var result = new BuildInitializeFileDetail();
            return InitializeFile(path, ref result);
        }
        public CacheDataController InitializeFile(string path, ref BuildInitializeFileDetail result)
        {
            var _result = new BuildInitializeFileDetail();
            var start = DateTime.Now;
            CacheDataController cacheDataController;
            var lockWait = DateTime.Now;
            lock (Controller)
            {
                _result.LockWaitDuration = (DateTime.Now - lockWait);
                var startGetData = DateTime.Now;
                cacheDataController = Controller.GetData(path, typeof(FileData)).FirstOrDefault() ??
                                          CacheFactory.CreateCacheData(Controller, typeof(FileData));
                _result.GetDataDuration = DateTime.Now - startGetData;
            }

            if (cacheDataController == null)
                return null;

            #region Initialize
            var start2 = DateTime.Now;
            var internalFile = cacheDataController.InternalData as FileData;
            // If the Data is not cached then create a FileData for first time use
            // Otherwise use the Cached version - Sara
            if (internalFile == null || internalFile.Path == null)
                internalFile = new FileData { Path = path };
            cacheDataController.Initialize(internalFile);

            PrepareFileEvents(cacheDataController);
            _result.InitializeDuration = DateTime.Now - start2;
            #endregion

            _result.InitializeFileDuration = DateTime.Now - start;

            result = _result;
            return cacheDataController;
        }

        /// <summary>
        /// Any time a Category, EventPattern or Value changes, we should invalidate the FileData
        /// </summary>
        /// <param name="cacheDataController"></param>
        private void PrepareFileEvents(CacheDataController cacheDataController)
        {
            var file = (cacheDataController.InternalData as FileData);
            if (file == null)
                throw new Exception("Error");

            // If the delegate was already assigned then this will remove it, otherwise the remove will be ignored. - Sara

            // ReSharper disable DelegateSubtraction
            XmlDal.DataModel.EventCacheDataController.InvalidateNotificationEvent -= file.InvalidateEvent;
            XmlDal.DataModel.ValueCacheDataController.InvalidateNotificationEvent -= file.InvalidateValue;

            // ReSharper restore DelegateSubtraction
            XmlDal.DataModel.EventCacheDataController.InvalidateNotificationEvent += file.InvalidateEvent;
            XmlDal.DataModel.ValueCacheDataController.InvalidateNotificationEvent += file.InvalidateValue;
        }

        /// <summary>
        /// Returns a reference to a FileData in the Cache.
        /// If the file does not exists in the Cache, then one will be added.
        /// </summary>
        /// <param name="path">Path of the file that is being loaded</param>
        /// <returns></returns>
        public FileData GetFile(string path)
        {
            var result = new BuildInitializeFileDetail();
            return GetFile(path, ref result);
        }
        public FileData GetFile(string path, ref BuildInitializeFileDetail result)
        {
            var cacheDataController = InitializeFile(path, ref result);

            if (cacheDataController == null)
                return null;

            return cacheDataController.Data as FileData;
        }

        #endregion FileData

        public void Clear()
        {
            lock (Controller)
            {
                Controller.Cache = new List<CacheDataController>();
                Controller.Save();
            }
        }
    }
}
