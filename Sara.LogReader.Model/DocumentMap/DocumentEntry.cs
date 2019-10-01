namespace Sara.LogReader.Model.DocumentMap
{
    public enum DocumentMapType
    {
        TimeGAPEnd,
        Value,
        Event,
        EventInstance,
        EventInstanceValue,
        Pattern,
        File,
        Network,
        TimeGAPStart
    }

    public enum DocumentMapLevel
    {
        Sibling = 0,
        Root = 1,
        Level1 = 2,
        Level2 = 3,
        Level3 = 4
    }

    public class DocumentEntry
    {
        public DocumentEntry()
        {
            HighlightColor = "";
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public object ValueObject { get; set; }
        public bool TopNode { get; set; }
        /// <summary>
        /// Path of the Source
        /// </summary>
        public string Path { get; set; }
        // Index of the Line
        // ReSharper disable InconsistentNaming
        public int iLine { get; set; }
        // ReSharper restore InconsistentNaming

        // Raw Text for the line
        public string Text { get; set; }
        /// <summary>
        /// True when this entry is a TimeGap
        /// </summary>
        public DocumentMapType Type { get; set; }

        /// <summary>
        /// Determines the level in the TreeView.
        /// Level 0 is at the root
        /// </summary>
        public DocumentMapLevel Level { get; set; }

        /// <summary>
        /// Identity of the Entry (I.e. Value or EventPattern)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Comes from EventPattern.DocumentMapFiltered
        /// Used to filter Events by Document Map Filtered
        /// </summary>
        public bool Filtered { get; set; }
        public int Identifier { get; set; }
        public int? ParentId { get; set; }
        public string HighlightColor { get; set; }
        public string Sort { get; set; }
    }
}
