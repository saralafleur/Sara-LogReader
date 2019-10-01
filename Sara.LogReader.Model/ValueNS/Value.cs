using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Sara.LogReader.Common;
using Sara.LogReader.Model.Categories;
using Sara.LogReader.Model.DocumentMap;

namespace Sara.LogReader.Model.ValueNS
{
    public class Value
    {
        public Value()
        {
            SourceTypes = new List<string>();
            Level = DocumentMapLevel.Sibling;
            Categories = new List<Category>();
        }
        [Browsable(false)]

        public int ValueId { get; set; }
        /// <summary>
        /// This use to be DocumentMapName
        /// </summary>
        public string Name { get; set; }
        public string ReplaceWith { get; set; }
        public string Sort { get; set; }
        public string Expression { get; set; }
        [Browsable(false)]
        public bool DocumentMap { get; set; }
        [Browsable(false)]
        public bool Distinct { get; set; }
        [Browsable(false)]
        public bool FileInfo { get; set; }
        /// <summary>
        /// When True the Value is only loaded when the FileData is opened.
        /// LazyLoad is used to delay when a value is loaded.
        /// </summary>
        [Browsable(false)]
        public bool LazyLoad { get; set; }
        /// <summary>
        /// List of FileTypes that will use the value.
        /// </summary>
        [Browsable(false)]
        public List<string> SourceTypes { get; set; }
        [Browsable(false)]
        public bool ExpressionOnly { get; set; }
        [Browsable(false)]
        public DocumentMapLevel Level { get; set; }
        [Browsable(false)]
        public List<Category> Categories { get; set; }
        /// <summary>
        /// Used to tag Events into a Filter Category that can be used by the DocumentMap 
        /// </summary>
        [Browsable(false)]
        public bool DocumentMapFiltered
        {
            get
            {
                return (from category
                            in Categories
                        from category1
                            in XmlDal.DataModel.CategoriesModel.Categories
                        where category.Name == category1.Name
                           && category1.Checked
                        select category).Any();
            }
        }

        public override string ToString()
        {
            return $"{ValueId}-{Name} - {Expression}";
        }

        public void Copy(Value item)
        {
            Name = item.Name;
            Expression = item.Expression;
            DocumentMap = item.DocumentMap;
            Distinct = item.Distinct;
            FileInfo = item.FileInfo;
            LazyLoad = item.LazyLoad;
            SourceTypes = item.SourceTypes;
            Categories = item.Categories;
        }

        /// <summary>
        /// Returns True if the SourceTypes contains ALL or the sourceType parameter.
        /// </summary>
        public bool ContainsSourceType(string sourceType)
        {
            // SourceTypes.Count == 0 is here when there is no Source Type selected - Sara
            return SourceTypes.Count==0 || 
                   SourceTypes.Contains(Keywords.ALL) ||
                   SourceTypes.Contains(sourceType);
        }

        /// <summary>
        /// Returns a string of SourceTypes
        /// </summary>
        public string SourceType
        {
            get
            {
                if (SourceTypes.Count == 0)
                    return Keywords.SOURCE_TYPE_UNKNOWN;
                var result = string.Empty;

                foreach (var sourceType in SourceTypes)
                {
                    if (string.IsNullOrEmpty(result))
                    {
                        result = sourceType;
                        continue;
                    }
                    result = $"{result}, {sourceType}";
                }
                return result;
            }
        }
    }
}
