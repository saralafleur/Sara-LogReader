using Sara.LogReader.Model;
using Sara.LogReader.Model.Categories;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Service
{
    public static class CategoryService
    {
        public static CategoriesModel GetModel()
        {
            return new CategoriesModel { Categories = XmlDal.DataModel.CategoriesModel.Categories };
        }

        public static CategoryModel Add()
        {
            return new CategoryModel
            {
                Mode = InputMode.Add,
                Item = new Category()
            };
        }
        public static CategoryModel Edit(int id)
        {
            return new CategoryModel
            {
                Mode = InputMode.Edit,
                Item = XmlDal.DataModel.GetCategory(id)
            };
        }
        public static void Save(CategoryModel model)
        {
            var data = XmlDal.DataModel;

            switch (model.Mode)
            {
                case InputMode.Add:
                    model.Item.CategoryId = data.GetUniqueCategoryId();
                    data.CategoriesModel.Categories.Add(model.Item);
                    break;
                case InputMode.Edit:
                    var item = data.GetCategory(model.Item.CategoryId);
                    item.Name = model.Item.Name;
                    break;
                case InputMode.Delete:
                    data.CategoriesModel.Categories.Remove(model.Item);
                    break;
            }

            XmlDal.Save();
            XmlDal.DataModel.CategoryCacheDataController.Invalidate();
        }
        public static CategoryModel Delete(int id)
        {
            var data = XmlDal.DataModel;

            var model = new CategoryModel
                {
                    Mode = InputMode.Delete,
                    Item = data.GetCategory(id)
                };

            Save(model);
            XmlDal.DataModel.CategoryCacheDataController.Invalidate();
            return model;
        }
    }
}
