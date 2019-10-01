using Sara.LogReader.Model;
using Sara.LogReader.Model.PatternNS;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Service.PatternServiceNS
{
    public static class PatternCRUDService
    {
        public static PatternsModel GetModel()
        {
            return new PatternsModel { Patterns = XmlDal.DataModel.PatternsModel.Patterns };
        }

        public static PatternModel AddPattern()
        {
            return new PatternModel
            {
                Mode = InputMode.Add,
                Item = new Pattern()
            };
        }
        public static PatternModel EditPattern(int patternId)
        {
            return new PatternModel
            {
                Mode = InputMode.Edit,
                Item = XmlDal.DataModel.GetPattern(patternId)
            };
        }
        public static void SavePattern(PatternModel model)
        {
            var data = XmlDal.DataModel;

            switch (model.Mode)
            {
                case InputMode.Add:
                    model.Item.PatternId = data.GetUniquePatternId();
                    data.PatternsModel.Patterns.Add(model.Item);
                    break;
                case InputMode.Edit:
                    var item = data.GetPattern(model.Item.PatternId);
                    item.Name = model.Item.Name;
                    break;
                case InputMode.Delete:
                    data.PatternsModel.Patterns.Remove(model.Item);
                    break;
            }

            XmlDal.Save();
            XmlDal.DataModel.PatternCacheDataController.Invalidate();
        }
        public static PatternModel DeletePattern(int patternId)
        {
            var data = XmlDal.DataModel;

            var model = new PatternModel
                {
                    Mode = InputMode.Delete,
                    Item = data.GetPattern(patternId)
                };

            SavePattern(model);
            XmlDal.DataModel.PatternCacheDataController.Invalidate();
            return model;
        }
    }
}
