using System;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Model.PatternNS
{
    public class StepModel
    {
        public InputMode Mode { get; set; }
        public Step Item { get; set; }
        public Action<StepModel> Callback { get; set; }
    }
}
