using System.Collections.Generic;
using System.ComponentModel;
using Sara.LogReader.Model.Categories;

namespace Sara.LogReader.Model.ResearchNS
{
    public class Research
    {
        public Research()
        {
            Categories = new List<Category>();
        }
        [Browsable(false)]
        public int ResearchId { get; set; }
        /// <summary>
        /// This use to be DocumentMapName
        /// </summary>
        public string Name { get; set; }
        public List<Category> Categories { get; set; }
        public override string ToString()
        {
            return $"{ResearchId}-{Name}";
        }

        public void Copy(Research item)
        {
            Name = item.Name;
            Categories = item.Categories;
        }
    }
}
