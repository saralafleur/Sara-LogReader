using System;
using System.Collections.Generic;
using Sara.Common.DateTimeNS;
using Sara.Common.SortableBindingList;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.Service.FileServiceNS;
using Sara.LogReader.WinForm.Views.EventPerformance;
using Sara.WinForm.MVVM;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class PerformanceResult
    {
        public SortableBindingList<EventPerformanceResult> Result { get; set; }
        public TimeSpan TotalDuration { get; set; }
        public int TotalEvents;

        public PerformanceResult()
        {
            Result = new SortableBindingList<EventPerformanceResult>();
        }
    }

    public class EventPerformanceTestViewModel : ViewModelBase<EventPerformanceTest, object, object>, IViewModelBaseNonGeneric
    {
        public EventPerformanceTestViewModel()
        {
            View = new EventPerformanceTest { ViewModel = this };
        }

        public PerformanceResult RunTest()
        {
            var result = new PerformanceResult();
            if (MainViewModel.Current == null)
                throw new Exception("You must first select a File!");

            var stopwatch = new Stopwatch("");
            try
            {
                var results = new List<EventPerformanceResult>();
                DocumentMapService.GetLines(MainViewModel.Current.Model.File,results, out result.TotalEvents);
                result.Result = new SortableBindingList<EventPerformanceResult>(results);
            }
            finally
            {
                stopwatch.Stop();
                if (stopwatch.Duration != null) result.TotalDuration = stopwatch.Duration.Value;
            }
            return result;
        }

        public override object GetModel()
        {
            return MainViewModel.Current != null ? MainViewModel.Current.Model.File : null;
        }
    }
}
