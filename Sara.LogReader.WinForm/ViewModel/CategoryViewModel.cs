using System.Windows.Forms;
using Sara.LogReader.WinForm.Views.Categories;
using Sara.Common.DateTimeNS;
using Sara.LogReader.Model.Categories;
using Sara.LogReader.Service;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class CategoryViewModel : ViewModelBase<CategoryWindow, CategoriesModel, CategoryModel>, IViewModelBaseNonGeneric
    {
        public CategoryViewModel()
        {
            var sw = new Stopwatch("Constructor CategoryViewModel");
            View = new CategoryWindow { ViewModel = this };
            //Note: I removed the call to Render because the Windows Handle for the View was not ready, I don't know why, but we will call Render from the MainViewModel within the 'Others' list - Sara
            //Render();
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }

        public override sealed CategoriesModel GetModel()
        {
            return CategoryService.GetModel();
        }

        public void Add()
        {
            var model = CategoryService.Add();
            var window = new AddEditCategory { ViewModel = this };
            window.Render(model);
            window.ShowDialog();
        }
        public void Edit(int id)
        {
            var model = CategoryService.Edit(id);
            var window = new AddEditCategory { ViewModel = this };
            window.Render(model);
            window.ShowDialog();
        }
        public void Delete(int id)
        {
            StatusUpdate(StatusModel.Update("Deleting"));
            var model = CategoryService.Delete(id);
            View.UpdateView(model);
        }
        public void Save(CategoryModel model)
        {
            StatusUpdate(StatusModel.Update("Saving"));
            CategoryService.Save(model);
            View.UpdateView(model);
        }
    }
}
