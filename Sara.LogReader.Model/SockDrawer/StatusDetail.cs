namespace Sara.LogReader.Model.SockDrawer
{
    public enum StatusDetailAction
    {
        Add,
        Remove,
        Update
    }

    public class StatusDetail
    {
        public StatusDetailAction Action { get; set; }
        public int ThreadId { get; set; }
        public string Path { get; set; }
        public string FileName { get { return System.IO.Path.GetFileName(Path); } }
        public int LineCount { get; set; }
        public string Status { get; set; }
    }
}
