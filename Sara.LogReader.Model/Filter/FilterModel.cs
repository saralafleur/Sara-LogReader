using System.Collections.Generic;
using Sara.LogReader.Model.Categories;

namespace Sara.LogReader.Model.Filter
{
    public class FilterModel
    {
        public FilterModel()
        {
            Categories = new List<Category>();
        }
        public List<Category> Categories { get; set; }
    }
}
