using System.Linq;
using System.Text.RegularExpressions;
using Sara.LogReader.Common;
using Sara.LogReader.Model;
using Sara.LogReader.Model.HideOptionNS;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Service
{
    public static class HideOptionService
    {
        public static HideOptionsModel GetModel()
        {
            return new HideOptionsModel { Options = XmlDal.DataModel.HideOptionsModel.Options };
        }

        public static HideOptionModel Add()
        {
            var model = XmlDal.DataModel;
            return new HideOptionModel
            {
                Mode = InputMode.Add,
                Item = new HideOption(),
                SourceTypes = model.Options.FileTypes.OrderBy(n => n).ToList(),
            };
        }
        public static HideOptionModel Edit(int id)
        {
            var model = XmlDal.DataModel;
            return new HideOptionModel
            {
                Mode = InputMode.Edit,
                Item = XmlDal.DataModel.GetHideOption(id),
                SourceTypes = model.Options.FileTypes.OrderBy(n => n).ToList(),
            };
        }
        public static void Save(HideOptionModel model)
        {
            var data = XmlDal.DataModel;

            switch (model.Mode)
            {
                case InputMode.Add:
                    model.Item.HideOptionId = data.GetUniqueHideOptionId();
                    data.HideOptionsModel.Options.Add(model.Item);
                    break;
                case InputMode.Edit:
                    var item = data.GetHideOption(model.Item.HideOptionId);
                    item.Name = model.Item.Name;
                    break;
                case InputMode.Delete:
                    data.HideOptionsModel.Options.Remove(model.Item);
                    break;
            }

            XmlDal.Save();
            XmlDal.DataModel.HideOptionCacheDataController.Invalidate();
        }
        public static HideOptionModel Delete(int id)
        {
            var data = XmlDal.DataModel;

            var model = new HideOptionModel
            {
                Mode = InputMode.Delete,
                Item = data.GetHideOption(id)
            };

            Save(model);
            XmlDal.DataModel.HideOptionCacheDataController.Invalidate();
            return model;
        }

        public static string ApplyHideOptions(string sourceType, string source)
        {
            foreach (var item in XmlDal.DataModel.HideOptionsModel.Options.Where(n => n.SourceTypes.Count == 0 ||
                                                                                 n.SourceTypes.Contains(Keywords.ALL) ||
                                                                                 n.SourceTypes.Contains(sourceType)))
            {
                if (XmlDal.DataModel.Options.HideOptions.Contains($"{sourceType}_{item.Name}"))
                    source = Apply(item.RegularExpression, item.ReplaceWith, source);
            }
            return source;
        }

        public static string Apply(string expression, string replaceWith, string source)
        {
            Regex rgx = new Regex(expression);
            return rgx.Replace(source, replaceWith);
        }
    }
}
