using Sara.Common.DateTimeNS;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.Source_Info;
using Sara.LogReader.WinForm.Views.SourceInfo;
using Sara.WinForm.MVVM;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class SourceInfoViewModel : ViewModelBase<SourceInfoView, SourceInfoModel, object>, IViewModelBaseNonGeneric
    {
        public SourceInfoViewModel()
        {
            var sw = new Stopwatch("Constructor SourceInfoViewModel");
            View = new SourceInfoView { ViewModel = this };
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }
        public override SourceInfoModel GetModel()
        {
            var model = new SourceInfoModel();
            if (MainViewModel.Current == null)
                return null;

            model.File = MainViewModel.Current.Model.File;

            return model;
        }

        internal void GoTo(ValueBookMark obj)
        {
            MainViewModel.Current.GoTo(obj);
        }
    }
}
