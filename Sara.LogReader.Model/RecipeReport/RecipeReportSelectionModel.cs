using System;
using System.Collections.Generic;
using Sara.LogReader.Model.PatternNS;

namespace Sara.LogReader.Model.RecipeReport
{
    public class RecipeReportSelectionModel
    {
        public List<string> Files { get; set; }
        public List<RecipeSelection> Recipes { get; set; }
        public Action<RecipeReportSelectionModel> Callback { get; set; }
        public string ReportName { get; set; }

        public RecipeReportSelectionModel()
        {
            Files = new List<string>();
            Recipes = new List<RecipeSelection>();
        }
    }
}
