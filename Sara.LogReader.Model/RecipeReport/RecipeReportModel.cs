using System.Collections.Generic;

namespace Sara.LogReader.Model.RecipeReport
{
    public class RecipeReportModel
    {
        public List<RecipeReportResultCacheData> Results { get; set; }

        public RecipeReportModel()
        {
            Results = new List<RecipeReportResultCacheData>();
        }
    }
}
