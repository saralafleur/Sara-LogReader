using System.Threading;
using System.Windows.Forms;
using Sara.Common.DateTimeNS;
using Sara.LogReader.Model.RecipeReport;
using Sara.LogReader.Service;
using Sara.LogReader.Service.PatternServiceNS;
using Sara.LogReader.WinForm.Views.RecipeReport;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class RecipeReportViewModel : ViewModelBase<RecipeReportView, RecipeReportModel, object>, IViewModelBaseNonGeneric
    {
        public RecipeReportViewModel()
        {
            var sw = new Stopwatch("Constructor RecipeReportViewModel");
            View = new RecipeReportView { ViewModel = this };
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }

        public override RecipeReportModel GetModel()
        {
            return RecipeReportService.GetModel();
        }

        public void Run()
        {
            var model = RecipeReportService.GetRecipeSelectionModel();
            model.Callback = RunRecipeReport;
            var window = new NetworkReportSelectionView { ViewModel = this };
            window.Render(model);
            window.ShowDialog();

        }

        private void RunRecipeReport(RecipeReportSelectionModel model)
        {
            View.StatusUpdate(StatusModel.StartStopWatch);
            View.StatusUpdate(StatusModel.Update("Running Recipe Report"));
            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    View.EnableCancel(true);
                    bool wasCancelled;
                    RecipeReportService.RunReport(model, View.StatusUpdate, out wasCancelled);
                    if (wasCancelled)
                    {
                        View.ReportCancelled();
                    }
                    RenderDocument();
                }
                finally
                {
                    View.EnableCancel(false);
                    View.StatusUpdate(StatusModel.Completed);
                }
            });
        }

        public void CancelReport()
        {
            RecipeReportService.CancelReport();
        }

        public void GoToFile(string path)
        {
            MainViewModel.GoToFile(path);
        }

        public void GoToLine(int iLine)
        {
            MainViewModel.GoToLine(iLine);
        }

        public void Delete(string key)
        {
            RecipeReportService.DeleteReport(key);

            RenderDocument();
        }

        public void RerunNetworkReport(RecipeReportResultCacheData reportResult)
        {

            RunRecipeReport(new RecipeReportSelectionModel
            {
                Files = reportResult.Files,
                Recipes = reportResult.Recipes,
                ReportName = ReportService.GenerateReRunReportName(reportResult.ReportName)
            });
        }

    }
}
