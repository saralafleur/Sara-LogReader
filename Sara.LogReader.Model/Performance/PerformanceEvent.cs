using System;
using System.ComponentModel;
using Sara.Common.DateTimeNS;
using Sara.Common.Extension;

namespace Sara.LogReader.Model.Performance
{
    public enum PerformanceState
    {
        Started,
        OutOfOrder,
        Stopped
    }
    public class PerformanceEvent
    {
        private Stopwatch watch { get; set; }
        public string EventName { get; set; }
        public DateTime StartTime { get; set; }
        public string Duration { get; set; }
        public PerformanceState State { get; set; }
        public int ThreadId { get; set; }
        [Browsable(false)]
        public bool IsRunning { get; set; }

        public void Start()
        {
            if (watch != null)
                throw new Exception("You cannot start an EventPattern that is already started!");

            State = PerformanceState.Started;

            watch = new Stopwatch($"PE >>> {EventName}");
            StartTime = watch.StartTime;
            ThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            IsRunning = true;
        }
        public void Stop(bool OutOfOrderOk = false)
        {
            State = PerformanceState.OutOfOrder;

            if (OutOfOrderOk && watch == null)
                return;

            if (watch == null)
                throw new Exception($"You must call Start before you can call Stop on Performance EventPattern {EventName}!");

            State = PerformanceState.Stopped;

            watch.Stop(0);
            Duration = watch.Duration.Value.ToReadableString();
            DurationSeconds = watch.Duration.Value.Seconds;
            watch = null;
            IsRunning = false;
        }

        [Browsable(false)]
        public int PerformanceEventId { get; set; }
        [Browsable(false)]
        public int DurationSeconds { get; set; }
    }
}
