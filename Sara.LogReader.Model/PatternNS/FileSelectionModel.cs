using System;
using System.Collections.Generic;

namespace Sara.LogReader.Model.PatternNS
{
    public class FileSelectionModel
    {
        public List<string> Files { get; set; }
        /// <summary>
        /// Used to send the prior Selected Files to FileData Selection
        /// </summary>
        public List<string> SelectedFiles { get; set; }
        public Action<FileSelectionModel> Callback { get; set; }
        public bool Continue { get; set; }
        public object Args { get; set; }

        public FileSelectionModel()
        {
            Files = new List<string>();
            Continue = false;
        }
    }
}
