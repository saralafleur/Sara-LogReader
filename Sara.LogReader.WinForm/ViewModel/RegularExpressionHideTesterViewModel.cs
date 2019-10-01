using System;
using Sara.Common.DateTimeNS;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.ValueNS;
using Sara.LogReader.Service.FileServiceNS;
using Sara.LogReader.WinForm.Views.ToolWindows;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class RegularExpressionHideTesterViewModel
    {
        public RegularExpressionTransfromTesterWindow View { get; set; }

        public string Value { get; set; }

        public Action<Value> UpdateRegularExpressionEvent;

        public RegularExpressionHideTesterViewModel(RegularExpressionTransfromTesterWindow view)
        {
            View = view;
        }

        public void Accept(Value model)
        {
            UpdateRegularExpression(model);
        }

        private void UpdateRegularExpression(Value model)
        {
            UpdateRegularExpressionEvent?.Invoke(model);
        }

        public ValueBookMarkModel Test(int valueId, 
                                       string name, 
                                       string value, 
                                       string expression, 
                                       bool fileInfo, 
                                       bool documentMap, 
                                       out TimeSpan duration)
        {
            var sw = new Stopwatch("RegularExpression Test");
            try
            {
                return FileService.GetRegularExpressionValue(new Value
                {
                    ValueId = valueId,
                    Name = name,
                    Expression = expression,
                    FileInfo = fileInfo,
                    DocumentMap = documentMap
                },
                value, MainViewModel.Current.Model.File.CachedNewLine, out duration);
            }
            finally
            {
                sw.Stop(0);
            }
        }
    }
}
