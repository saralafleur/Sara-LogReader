using System.Linq;
using System.Reflection;
using Sara.Logging;
using Sara.LogReader.Model;
using Sara.LogReader.Model.ResearchNS;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Service
{
    public static class ResearchService
    {
        public static ResearchCacheData GetModel()
        {
            return XmlDal.DataModel.ResearchsModel;
        }
        public static ResearchModel Add()
        {
            var model = new Research();
            return Add(model);
        }
        public static ResearchModel Add(Research model)
        {
            return new ResearchModel
            {
                Mode = InputMode.Add,
                SaveEvent = Save,
                Item = model,
                Categories = XmlDal.DataModel.CategoriesModel.Categories.OrderBy(n => n.Name).ToList(),
            };
        }
        public static ResearchModel Edit(int id)
        {
            var model = XmlDal.DataModel.GetResearch(id);
            return new ResearchModel
            {
                Mode = InputMode.Edit,
                SaveEvent = Save,
                Item = model,
                Categories = XmlDal.DataModel.CategoriesModel.Categories.OrderBy(n => n.Name).ToList()
            };
        }
        public static ResearchModel Delete(int id)
        {
            var model = new ResearchModel
            {
                Mode = InputMode.Delete,
                Item = XmlDal.DataModel.GetResearch(id)
            };
            Save(model);
            return model;
        }
        public static void Save(ResearchModel model)
        {
            Log.WriteEnter(typeof(ResearchService).FullName, MethodBase.GetCurrentMethod().Name);
            var dataModel = XmlDal.DataModel;

            switch (model.Mode)
            {
                case InputMode.Add:
                    model.Item.ResearchId = XmlDal.DataModel.GetUniqueResearchId();
                    dataModel.ResearchsModel.Items.Add(model.Item);
                    break;
                case InputMode.Edit:
                    var item = dataModel.GetResearch(model.Item.ResearchId);
                    item.Copy(model.Item);
                    break;
                case InputMode.Delete:
                    dataModel.ResearchsModel.Items.Remove(model.Item);
                    break;
            }

            XmlDal.Save();
            XmlDal.DataModel.ResearchCacheDataController.Invalidate();
        }

        public static Research GetById(int id)
        {
            return XmlDal.DataModel.GetResearch(id);
        }
    }
}