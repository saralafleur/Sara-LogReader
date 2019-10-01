using System;
using Sara.LogReader.Model.LogReaderNS;

namespace Sara.LogReader.Model.Categories
{
    public class Category : CheckedItem, ICloneable
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public object Clone()
        {
            var c = new Category()
            {
                Name = this.Name,
                CategoryId = this.CategoryId
            };
            return c;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
