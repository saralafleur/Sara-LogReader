using System.Windows.Forms;
using Sara.Common.DateTimeNS;
using Sara.LogReader.Model.HideOptionNS;
using Sara.LogReader.Service;
using Sara.LogReader.WinForm.Views.HideOptions;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class HideOptionViewModel : ViewModelBase<HideOptionsWindow, HideOptionsModel, HideOptionModel>, IViewModelBaseNonGeneric
    {
        public HideOptionViewModel()
        {
            var sw = new Stopwatch("Constructor HideOptionViewModel");
            View = new HideOptionsWindow { ViewModel = this };
            //Note: I removed the call to Render because the Windows Handle for the View was not ready, I don't know why, but we will call Render from the MainViewModel within the 'Others' list - Sara
            //Render();
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }

        public override sealed HideOptionsModel GetModel()
        {
            return HideOptionService.GetModel();
        }

        public void Add()
        {
            var model = HideOptionService.Add();
            var window = new AddEditHideOptions { ViewModel = this };
            window.Render(model);
            window.ShowDialog();
        }
        public void Edit(int id)
        {
            var model = HideOptionService.Edit(id);
            var window = new AddEditHideOptions { ViewModel = this };
            window.Render(model);
            window.ShowDialog();
        }
        public void Delete(int id)
        {
            StatusUpdate(StatusModel.Update("Deleting"));
            var model = HideOptionService.Delete(id);
            View.UpdateView(model);
            MainViewModel.PrepareHideOptions();
            MainViewModel.ApplyHideOptions();
        }
        public void Save(HideOptionModel model)
        {
            StatusUpdate(StatusModel.Update("Saving"));
            HideOptionService.Save(model);
            View.UpdateView(model);
            MainViewModel.PrepareHideOptions();
            MainViewModel.ApplyHideOptions();
        }
    }
}
