using Sara.LogReader.WinForm.Views.DocumentHelper;
using Sara.Common.DateTimeNS;
using Sara.WinForm.MVVM;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class DocumentHelperViewModel : ViewModelBase<DocumentHelperWindow, object, object>, IViewModelBaseNonGeneric
    {
        public DocumentHelperViewModel()
        {
            var sw = new Stopwatch("Constructor DocumentHelperViewModel");
            View = new DocumentHelperWindow { ViewModel = this };
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }

        public override sealed object GetModel()
        {
            return null;
        }
    }
}
