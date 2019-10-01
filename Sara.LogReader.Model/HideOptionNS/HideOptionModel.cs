using System.Collections.Generic;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Model.HideOptionNS
{
    public class HideOptionModel
    {
        public HideOptionModel()
        {
            SourceTypes = new List<string>();
        }
        public InputMode Mode { get; set; }
        public HideOption Item { get; set; }
        public List<string> SourceTypes { get; set; }
    }
}
