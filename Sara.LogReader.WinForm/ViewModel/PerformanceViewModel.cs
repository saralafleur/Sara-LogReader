using System.Windows.Forms;
using Sara.Common.DateTimeNS;
using Sara.LogReader.Model.Performance;
using Sara.LogReader.Service;
using Sara.LogReader.WinForm.Views.Values;
using Sara.WinForm.MVVM;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class PerformanceViewModel : ViewModelBase<PerformanceView, PerformanceData, PerformanceModel>, IViewModelBaseNonGeneric
    {
        public PerformanceViewModel()
        {
            var sw = new Stopwatch("Constructor PerformanceViewModel");
            View = new PerformanceView { ViewModel = this };
            PerformanceService.Update += OnUpdate;
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }

        private void OnUpdate()
        {
            View.Render(GetModel());
        }

        public override PerformanceData GetModel()
        {
            return PerformanceService.Model;
        }

        internal void Clear()
        {
            GetModel().Items.Clear();
            View.Render(GetModel());
        }
    }
}
