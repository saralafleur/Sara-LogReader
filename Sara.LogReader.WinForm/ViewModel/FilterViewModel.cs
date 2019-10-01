using Sara.Common.DateTimeNS;
using Sara.LogReader.Model;
using Sara.LogReader.Model.Filter;
using Sara.LogReader.WinForm.Views.Filter;
using Sara.WinForm.MVVM;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class FilterViewModel : ViewModelBase<FilterWindow, FilterModel, object>, IViewModelBaseNonGeneric
    {
        public FilterViewModel()
        {
            var sw = new Stopwatch("Constructor FilterViewModel");
            View = new FilterWindow { ViewModel = this };
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }
        public override FilterModel GetModel()
        {
            var model = new FilterModel();
            if (MainViewModel.Current == null)
                return null;

            model.Categories = XmlDal.DataModel.CategoriesModel.Categories;

            return model;
        }

        public void ApplyFilter()
        {
            XmlDal.DataModel.CategoryCacheDataController.Invalidate();
        }
    }
}
