using System.Drawing;
using System.Windows.Forms;
using Sara.Common.DateTimeNS;
using Sara.LogReader.Model;
using Sara.LogReader.Model.PatternNS;
using Sara.LogReader.Service.PatternServiceNS;
using Sara.LogReader.WinForm.Views.Patterns;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class PatternViewModel : ViewModelBase<PatternView, PatternsModel, PatternModel>,IViewModelBaseNonGeneric
    {
        public PatternViewModel()
        {
            var sw = new Stopwatch("Constructor PatternViewModel");
            View = new PatternView {ViewModel = this};
            XmlDal.DataModel.EventCacheDataController.InvalidateNotificationEvent += View.ListenToEventChange;
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }
        
        public override PatternsModel GetModel()
        {
            return PatternCRUDService.GetModel();
        }

        public void AddPattern()
        {
            var model = PatternCRUDService.AddPattern();
            var window = new AddEditPattern { ViewModel = this};
            window.Render(model);
            window.ShowDialog();
        }
        public void EditPattern(int patternId)
        {
            var model = PatternCRUDService.EditPattern(patternId);
            var window = new AddEditPattern { ViewModel = this };
            window.Render(model);
            window.ShowDialog();
        }
        public void DeletePattern(int patternId)
        {
            var model = PatternCRUDService.DeletePattern(patternId);
            View.UpdateView(model);
        }
        public void SavePattern(PatternModel model)
        {
            StatusUpdate(StatusModel.FullScreenOff);
            StatusUpdate(StatusModel.Update("Saving Pattern"));
            PatternCRUDService.SavePattern(model);
            View.UpdateView(model);
            StatusUpdate(StatusModel.Completed);
            StatusUpdate(StatusModel.FullScreenOn);
        }

        internal void RunScripts()
        {
            if (View.Visible)
                View.RunScripts();
        }

        internal void RunScriptsOnAll()
        {
            if (View.Visible)
                View.RunScriptsOnAll();
        }

        internal Color OverlayColor(int iLine)
        {
            if (View is PatternView v)
                return v.OverlayColor(iLine);
            return Color.Transparent;
        }
    }
}
