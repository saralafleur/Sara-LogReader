using System.Collections.Generic;
using Sara.LogReader.Model.DocumentMap;

namespace Sara.LogReader.Model.FileNS
{
    public class ValueBookMarkModel
    {
        public List<ValueBookMark> Values { get; set; }
        public bool Success { get; set; }

        public ValueBookMarkModel()
        {
            Values = new List<ValueBookMark>();
        }
    }

    public class ValueBookMark
    {
        public ValueBookMark()
        {
            Level = DocumentMapLevel.Root;
        }
        
        public int ValueId { get; set; }
        public bool DocumentMap { get; set; }
        public string Sort { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        // ReSharper disable InconsistentNaming
        public int iLine { get; set; }
        // ReSharper restore InconsistentNaming
        /// <summary>
        /// When True the this entry will be added  to the FileData Info section
        /// </summary>
        public bool FileInfo { get; set; }

        /// <summary>
        /// Determines the level in the DocumentMapTreeView
        /// </summary>
        public DocumentMapLevel Level { get; set; }
        /// <summary>
        /// When True this item is a Duration entry created by the Document Map
        /// </summary>
        public bool DocumentMapDuration { get; set; }

        public override string ToString()
        {
            // The iLine in the List<string> is zero based, but the UI is 1 based, so I have added a 1 to the iLine - Sara
            return $"{Name} ({iLine + 1}): {Value}";
        }
    }
}
