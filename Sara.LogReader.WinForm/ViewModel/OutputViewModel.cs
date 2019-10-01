using Sara.LogReader.WinForm.Views.Output;
using Sara.WinForm.MVVM;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class OutputViewModel : ViewModelBase<OutputView, object, object>, IViewModelBaseNonGeneric
    {
        public override object GetModel()
        {
            return null;
        }

        public OutputViewModel()
        {
            View = new OutputView { ViewModel = this };
        }
    }
}
