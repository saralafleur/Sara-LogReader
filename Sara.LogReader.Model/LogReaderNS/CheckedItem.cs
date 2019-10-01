namespace Sara.LogReader.Model.LogReaderNS
{
    /// <summary>
    /// Used by the Toolbox to show checked and unchecked items. - Sara LaFleur
    /// </summary>
    public class CheckedItem
    {
        public bool Checked { get; set; }
        public bool PriorChecked { get; set; }

        public bool Changed
        {
            get { return Checked != PriorChecked; }
        }
    }

}
