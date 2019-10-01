using System;
using Sara.LogReader.Model.ValueNS;
using Sara.LogReader.WinForm.Views.ToolWindows;

namespace Sara.LogReader.WinForm.Views.ToolWindows
{
    public static class RegularExpressionTest
    {
        public static void Test(string expression, Action<Value> callback)
        {
            var testWindow = new RegularExpressionTesterWindow
            {
                Model =
                {
                    Expression = expression,
                    ExpressionOnly = true,
                    Distinct = false,
                    FileInfo = false
                }
            };
            testWindow.ViewModel.UpdateRegularExpressionEvent += callback;
            testWindow.ShowDialog();
        }

        public static void TestTransform(string expression, string replaceWith, Action<Value> callback)
        {
            var testWindow = new RegularExpressionTransfromTesterWindow
            {
                Model =
                {
                    Expression = expression,
                    ReplaceWith = replaceWith,
                    ExpressionOnly = true,
                    Distinct = false,
                    FileInfo = false
                }
            };
            testWindow.ViewModel.UpdateRegularExpressionEvent += callback;
            testWindow.ShowDialog();
        }
    }
}
