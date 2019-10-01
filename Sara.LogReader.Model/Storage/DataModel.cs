using System;
using System.Linq;
using System.Threading;
using Sara.Cache;
using Sara.Cache.DataStore;
using Sara.LogReader.Model.Categories;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.Model.HideOptionNS;
using Sara.LogReader.Model.IDE;
using Sara.LogReader.Model.LogReaderNS;
using Sara.LogReader.Model.NetworkMapping;
using Sara.LogReader.Model.PatternNS;
using Sara.LogReader.Model.ResearchNS;
using Sara.LogReader.Model.ValueNS;
using EventLR = Sara.LogReader.Model.EventNS.EventLR;

namespace Sara.LogReader.Model.Storage
{
    /// <summary>
    /// DataModel contains the configuration data for the Sara.LogReader.
    /// Events, Values, Categories and anything else that is used to define how we look at logs.
    /// </summary>
    public class DataModel
    {
        public CacheController Controller { get; set; }
        private static string DataFile { get; set; }

        public DataModel(string dataFilePath)
        {
            DataFile = dataFilePath;
        }
        private ManualResetEvent _loaded = new ManualResetEvent(false);
        /// <summary>
        /// ThreadSafe way to Wait for Loaded to Complete
        /// </summary>
        public void WaitForLoad()
        {
            _loaded.WaitOne();
        }
        public void Load()
        {
            Controller = new CacheController();
            var dataStore2 = new XmlCacheDataStore { Path = DataFile };
            Controller.SetupDataStore(dataStore2);
            Controller.Load();

            if (Controller.EmptyByType(typeof(OptionsCacheData))) 
                CacheFactory.CreateCacheData(Controller, typeof (OptionsCacheData));
        
            if (Controller.EmptyByType(typeof(CategoryCacheData))) 
                CacheFactory.CreateCacheData(Controller, typeof (CategoryCacheData));

            if (Controller.EmptyByType(typeof(HideOptionCacheData)))
                CacheFactory.CreateCacheData(Controller, typeof(HideOptionCacheData));

            if (Controller.EmptyByType(typeof(PatternCacheData)))
                CacheFactory.CreateCacheData(Controller, typeof(PatternCacheData));

            if (Controller.EmptyByType(typeof(EventCacheData)))
                CacheFactory.CreateCacheData(Controller, typeof(EventCacheData));

            if (Controller.EmptyByType(typeof(ValueCacheData)))
                CacheFactory.CreateCacheData(Controller, typeof(ValueCacheData));

            if (Controller.EmptyByType(typeof(ResearchCacheData)))
                CacheFactory.CreateCacheData(Controller, typeof(ResearchCacheData));

            if (Controller.EmptyByType(typeof(NetworkMapCacheData)))
                CacheFactory.CreateCacheData(Controller, typeof(NetworkMapCacheData));
            _loaded.Set();
        }
        public void Save()
        {
            Controller.Save();
        }

        public OptionsCacheData Options
        {
            get
            {
                var cacheDataController = Controller.GetData(typeof(OptionsCacheData)).FirstOrDefault();
                if (cacheDataController != null)
                    return cacheDataController.Data as OptionsCacheData;
                throw new NullReferenceException("Options was not found in the Cache!");
            }
        }

        #region Category
        public CacheDataController CategoryCacheDataController
        {
            get
            {
                var cacheDataController = Controller.GetData(typeof(CategoryCacheData)).FirstOrDefault();
                if (cacheDataController == null)
                    throw new NullReferenceException("CategoryCacheData was not found in the Cache!");
                return cacheDataController;
            }
        }
        public CategoryCacheData CategoriesModel
        {
            get
            {
                var model = CategoryCacheDataController.Data as CategoryCacheData;
                if (model == null)
                    throw new NullReferenceException("CategoriesModel return a null cache object!");
                return model;
            }
        }
        public int GetUniqueCategoryId()
        {
            var model = CategoriesModel;
            model.LastUniqueCategoryId++;
            return model.LastUniqueCategoryId;
        }
        public Category GetCategory(int categoryId)
        {
            return CategoriesModel.Categories.FirstOrDefault(item => item.CategoryId == categoryId);
        }
        #endregion Category

        #region HideOptions
        public CacheDataController HideOptionCacheDataController
        {
            get
            {
                var cacheDataController = Controller.GetData(typeof(HideOptionCacheData)).FirstOrDefault();
                if (cacheDataController == null)
                    throw new NullReferenceException("HideOptionCacheData was not found in the Cache!");
                return cacheDataController;
            }
        }
        public HideOptionCacheData HideOptionsModel
        {
            get
            {
                var model = HideOptionCacheDataController.Data as HideOptionCacheData;
                if (model == null)
                    throw new NullReferenceException("HideOptionsModel return a null cache object!");
                return model;
            }
        }
        public int GetUniqueHideOptionId()
        {
            var model = HideOptionsModel;
            model.LastUniqueHideOptionId++;
            return model.LastUniqueHideOptionId;
        }
        public HideOption GetHideOption(int hideOptionId)
        {
            return HideOptionsModel.Options.FirstOrDefault(item => item.HideOptionId == hideOptionId);
        }
        #endregion HideOption

        #region Pattern
        public CacheDataController PatternCacheDataController
        {
            get
            {
                var cacheDataController = Controller.GetData(typeof(PatternCacheData)).FirstOrDefault();
                if (cacheDataController == null)
                    throw new NullReferenceException("PatternCacheData was not found in the Cache!");
                return cacheDataController;
            }
        }
        public PatternCacheData PatternsModel
        {
            get
            {
                var model = PatternCacheDataController.Data as PatternCacheData;
                if (model == null)
                    throw new NullReferenceException("PatternsModel return a null cache object!");
                return model;
            }
        }
        public int GetUniquePatternId()
        {
            var model = PatternsModel;
            model.LastUniquePatternId++;
            return model.LastUniquePatternId;
        }
        public Pattern GetPattern(int patternId)
        {
            return PatternsModel.Patterns.FirstOrDefault(item => item.PatternId == patternId);
        }
        #endregion Pattern

        #region EventPattern
        public CacheDataController EventCacheDataController
        {
            get
            {
                var cacheDataController = Controller.GetData(typeof(EventCacheData)).FirstOrDefault();
                if (cacheDataController == null)
                    throw new NullReferenceException("EventCacheData was not found in the Cache!");
                return cacheDataController;
            }
        }
        public EventCacheData EventsModel
        {
            get
            {
                var model = EventCacheDataController.Data as EventCacheData;
                if (model == null)
                    throw new NullReferenceException("EventsModel return a null cache object!");
                return model;
            }
        }
        public int GetUniqueEventId()
        {
            EventsModel.LastUniqueEventId++;
            return EventsModel.LastUniqueEventId;
        }
        public EventLR GetEvent(int eventId)
        {
            return EventsModel.Events.FirstOrDefault(item => item.EventId == eventId);
        }
        #endregion EventPattern

        #region Value
        
        public CacheDataController ValueCacheDataController
        {
            get
            {
                var cacheDataController = Controller.GetData(typeof(ValueCacheData)).FirstOrDefault();
                if (cacheDataController == null)
                    throw new NullReferenceException("ValueCacheData was not found in the Cache!");

                return cacheDataController;
            }
        }
        public ValueCacheData ValuesModel
        {
            get
            {
                var model = ValueCacheDataController.Data as ValueCacheData;
                if (model == null)
                    throw new NullReferenceException("ValuesModel return a null cache object!");
                return model;
            }
        }
        public int GetUniqueValueId()
        {
            ValuesModel.LastUniqueValueId++;
            return ValuesModel.LastUniqueValueId;
        }
        public Value GetValue(int valueId)
        {
            return ValuesModel.Values.FirstOrDefault(item => item.ValueId == valueId);
        }
        #endregion Value

        #region Research
        public CacheDataController ResearchCacheDataController
        {
            get
            {
                var cacheDataController = Controller.GetData(typeof(ResearchCacheData)).FirstOrDefault();
                if (cacheDataController == null)
                    throw new NullReferenceException("ResearchCacheData was not found in the Cache!");

                return cacheDataController;
            }
        }
        public ResearchCacheData ResearchsModel
        {
            get
            {
                var model = ResearchCacheDataController.Data as ResearchCacheData;
                if (model == null)
                    throw new NullReferenceException("ResearchsModel return a null cache object!");
                return model;
            }
        }
        public int GetUniqueResearchId()
        {
            ResearchsModel.LastUniqueId++;
            return ResearchsModel.LastUniqueId;
        }
        public Research GetResearch(int id)
        {
            return ResearchsModel.Items.FirstOrDefault(item => item.ResearchId == id);
        }
        #endregion Research

        #region NetworkMapping
        public CacheDataController NetworkMapCacheDataController
        {
            get
            {
                var cacheDataController = Controller.GetData(typeof(NetworkMapCacheData)).FirstOrDefault();
                if (cacheDataController == null)
                    throw new NullReferenceException("NetworkMapCacheData was not found in the Cache!");

                return cacheDataController;
            }
        }
        public NetworkMapCacheData NetworkMappingModel
        {
            get
            {
                var model = NetworkMapCacheDataController.Data as NetworkMapCacheData;
                if (model == null)
                    throw new NullReferenceException("NetworkMapModel return a null cache object!");
                return model;
            }
        }
        public int GetUniqueNetworkMapId()
        {
            NetworkMappingModel.LastUniqueNetworkMapId++;
            return NetworkMappingModel.LastUniqueNetworkMapId;
        }
        public NetworkMap GetNetworkMap(int networkMapId)
        {
            return NetworkMappingModel.NetworkMaps.FirstOrDefault(item => item.NetworkMapId == networkMapId);
        }

        public IDEModel GetIDEModel()
        {
            if (Options.IDEScript == null)
                return new IDEModel();

            return Options.IDEScript;
        }

        public void SaveIDEModel(IDEModel model)
        {
            Options.IDEScript = model;
            Save();
        }
        #endregion NetworkMapping
    }
}
