using System.Collections.Generic;

namespace Sara.LogReader.Model.FileNS
{
    public class SockDrawerModel
    {
        public FileData ActiveFile { get; set; }
        public List<FileData> Files { get; set; }

        public SockDrawerModel()
        {
            Files = new List<FileData>();
        }
    }
}
