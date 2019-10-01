using System.Linq;
using System.Reflection;
using Sara.Logging;
using Sara.LogReader.Model;
using Sara.LogReader.Model.ValueNS;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Service
{
    public static class ValueService
    {
        public static ValueCacheData GetModel()
        {
            return XmlDal.DataModel.ValuesModel;
        }
        public static ValueModel Add()
        {
            var model = new Value();
            return Add(model);
        }
        public static ValueModel Add(Value model)
        {
            return new ValueModel
            {
                Mode = InputMode.Add,
                SaveEvent = Save,
                Item = model,
                Categories = XmlDal.DataModel.CategoriesModel.Categories.OrderBy(n => n.Name).ToList(),
                SourceTypes = XmlDal.DataModel.Options.FileTypes.OrderBy(n => n).ToList()
            };
        }
        public static ValueModel Edit(int valueId)
        {
            var model = XmlDal.DataModel.GetValue(valueId);
            return new ValueModel
            {
                Mode = InputMode.Edit,
                SaveEvent = Save,
                Item = model,
                Categories = XmlDal.DataModel.CategoriesModel.Categories.OrderBy(n => n.Name).ToList(),
                SourceTypes = XmlDal.DataModel.Options.FileTypes.OrderBy(n => n).ToList()
            };
        }
        public static ValueModel Delete(int valueId)
        {
            var model = new ValueModel
            {
                Mode = InputMode.Delete,
                Item = XmlDal.DataModel.GetValue(valueId)
            };
            Save(model);
            return model;
        }
        public static void Save(ValueModel model)
        {
            Log.WriteEnter(typeof(ValueService).FullName, MethodBase.GetCurrentMethod().Name);
            var dataModel = XmlDal.DataModel;

            switch (model.Mode)
            {
                case InputMode.Add:
                    model.Item.ValueId = XmlDal.DataModel.GetUniqueValueId();
                    dataModel.ValuesModel.Values.Add(model.Item);
                    break;
                case InputMode.Edit:
                    var item = dataModel.GetValue(model.Item.ValueId);
                    item.Copy(model.Item);
                    break;
                case InputMode.Delete:
                    dataModel.ValuesModel.Values.Remove(model.Item);
                    break;
            }

            XmlDal.Save();
            XmlDal.DataModel.ValueCacheDataController.Invalidate();
        }

        public static Value GetValueById(int valueId)
        {
            return XmlDal.DataModel.GetValue(valueId);
        }
    }
}