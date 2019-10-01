using System;
using System.Linq;
using Sara.LogReader.Model;
using Sara.LogReader.Model.PatternNS;
using Sara.LogReader.Model.RecipeReport;
using Sara.WinForm.Notification;

namespace Sara.LogReader.Service.PatternServiceNS
{
    public static class RecipeReportService
    {
        private static AnalyzeRecipeArgs Args { get; set; }
        
        public static RecipeReportModel GetModel()
        {
            return new RecipeReportModel { Results = XmlDal.CacheModel.RecipeReports };
        }

        public static RecipeReportSelectionModel GetRecipeSelectionModel()
        {
            var model = new RecipeReportSelectionModel();

            foreach (var recipe in XmlDal.DataModel.PatternsModel.Patterns)
            {
                model.Recipes.Add(new RecipeSelection
                {
                    Name = recipe.ToString(),
                    RecipeId = recipe.PatternId
                });
            }

            model.Files = XmlDal.CacheModel.Files.Select(n => n.Path).ToList();

            return model;
        }

        public static void RunReport(RecipeReportSelectionModel model, Action<IStatusModel> statusUpdate, out bool cancelled)
        {
            try
            {
                Args = new AnalyzeRecipeArgs
                {
                    StatusUpdate = statusUpdate,
                    Model = model,
                    Report = XmlDal.CacheModel.GetRecipeReport(model.ReportName)
                };

                var tool = new AnalyzeRecipeLoop {Args = Args};
                tool.Execute(out cancelled);
            }
            finally
            {
                statusUpdate(StatusModel.Completed);
            }
        }

        public static void CancelReport()
        {
            Args.IsCancelled = true;
        }

        public static void DeleteReport(string key)
        {
            XmlDal.CacheModel.RemoveRecipeReport(key);
        }
    }
}
