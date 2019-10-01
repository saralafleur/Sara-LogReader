using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Sara.Common.DateTimeNS;
using Sara.LogReader.Model.DocumentMap;
using Sara.LogReader.Model.IDE;
using Sara.LogReader.Model.PatternNS;
using Sara.LogReader.Service;
using Sara.LogReader.WinForm.Views.Patterns;
using Sara.WinForm.ControlsNS;
using Sara.WinForm.CRUD;
using Sara.MonitorScript.Lexer;
using Sara.LogReader.Service.PatternServiceNS;

namespace Sara.LogReader.WinForm.ViewModel
{
    public static class MonitorScriptIDEViewModel
    {
        #region Render
        public static IDEModel GetModel()
        {
            return IDEService.GetModel();
        }
        #endregion

        #region Methods
        internal static void Run(TestArgs args)
        {
            ThreadPool.QueueUserWorkItem(m =>
            {
                IDEService.Test(args);
                args.ResultCallback?.Invoke(args.Result);
            });
        }
        public static void SaveModel(IDEModel model)
        {
            IDEService.SaveModel(model);
        }
        internal static List<LookupValue> GetValues()
        {
            var _values = new List<LookupValue>();
            foreach (var item in Keyword.PatternKeywords)
            {
                if (!_values.Any(n => n.Name == item && n.Type == Sara.WinForm.ControlsNS.ValueType.Keyword))
                    _values.Add(new LookupValue
                    {
                        Type = Sara.WinForm.ControlsNS.ValueType.Keyword,
                        Name = item
                    });
            }
            foreach (var item in EventService.GetEventKeywords())
            {
                if (!_values.Any(n => n.Name == item && n.Type == Sara.WinForm.ControlsNS.ValueType.Event))
                    _values.Add(new LookupValue
                    {
                        Type = Sara.WinForm.ControlsNS.ValueType.Event,
                        Name = item
                    });
            }

            var model = SettingsViewModel.GetSettingsModel();
            foreach (var item in model.FileTypes)
            {
                if (!_values.Any(n => n.Name == item && n.Type == Sara.WinForm.ControlsNS.ValueType.FileType))
                    _values.Add(new LookupValue
                    {
                        Type = Sara.WinForm.ControlsNS.ValueType.FileType,
                        Name = item
                    });
            }

            return _values;
        }
        internal static void GoToLine(DocumentEntry item)
        {
            if (MainViewModel.Current == null)
                MainViewModel.GoToFile(item.Path);

            if (MainViewModel.Current.Model.File.Path != item.Path)
                MainViewModel.Current.GoToFileAndLine(item.Path, item.iLine);
            else
                MainViewModel.Current.GoToLine(item.iLine);
        }
        #endregion 

        #region RunOnAll Select Files
        internal static void RunScriptsOnAll(TestArgs args, Action<bool> callback)
        {
            ThreadPool.QueueUserWorkItem(d =>
            {
                var model = new FileSelectionModel { Args = args, SelectedFiles = args.Pattern == null ? new List<string>() : args.Pattern.SelectedFiles };
                model.Callback = RunScriptOnAllCallback;
                var window = new FileSelectionView();

                window.Render(model);
                window.ShowDialog();
                callback?.Invoke(model.Continue);
            });
        }
        private static void RunScriptOnAllCallback(FileSelectionModel model)
        {
            if (model == null)
                return;

            TestArgs args = model.Args as TestArgs;
            if (args == null)
                throw new Exception("Args must be of type TestArgs!");

            args.Files = model.Files;
            args.Pattern.SelectedFiles = model.Files;

            ThreadPool.QueueUserWorkItem(d =>
            {
                var sw = new Stopwatch("Saving Pattern Selected Files");
                // Remember which files where selected for the next Run - Sara
                var m = new PatternModel() { Item = args.Pattern, Mode = InputMode.Edit };
                PatternCRUDService.SavePattern(m);
                sw.Stop(0);
            });

            Run(args);
        }
        #endregion
    }
}
