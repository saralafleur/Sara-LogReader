using System;
using System.Linq;
using System.Reflection;
using Sara.Common.XML;
using Sara.Logging;
using Sara.LogReader.Model;
using Sara.LogReader.Model.Performance;

namespace Sara.LogReader.Service
{
    public static class PerformanceService
    {
        #region Const
        public const string CONST_Startup = "Startup";
        public const string CONST_LoadingDataFile = "Startup - Loading DataFile";
        public const string CONST_LoadingLayout = "Startup - Loading Layout";
        public const string CONST_LoadingOther = "Startup - Other";
        public const string CONST_TimeTest = "Time Test";
        public const string CONST_OpenDocument = "Open Document for ";
        public const string CONST_RenderDocument = "Render Document for ";
        public const string CONST_BuildDocumentMap = "Build Document Map for ";
        public const string CONST_RenderDocumentMap = "Render Document Map for ";
        #endregion

        public static event Action Update;

        static PerformanceService()
        {
            Model = Serialize.Load<PerformanceData>(DataPath.GetPerformanceFilePath());
        }

        static void Save()
        {
            lock (Model)
            {
                Serialize.Save(Model, DataPath.GetPerformanceFilePath());
            }
        }

        public static PerformanceData Model { get; set; }

        public static int GetUniquePerformanceId()
        {
            Model.LastUniqueId++;
            return Model.LastUniqueId;
        }
        public static PerformanceEvent GetPerformanceEvent(int id)
        {
            return Model.Items.FirstOrDefault(item => item.PerformanceEventId == id);
        }

        public static void StartEvent(string name)
        {
            var e = new PerformanceEvent()
            {
                EventName = name
            };

            e.Start();

            // If we have not loaded the Cache yet, then temporary store the EventPattern.
            Model.Items.Add(e);
        }

        public static void StopEvent(string name, bool outOfOrderOk = false)
        {
            PerformanceEvent e;
                e = Model.Items.Where(n => n.EventName == name && n.IsRunning).OrderByDescending(n => n.StartTime).FirstOrDefault();

            if (e == null)
            {
                Log.Write($"Performance EventPattern '{name}' was not found!",typeof(PerformanceService).FullName, MethodBase.GetCurrentMethod().Name, LogEntryType.Warning);
                return;
            }

            e.Stop(outOfOrderOk);
            Save();
            Update?.Invoke();
        }

        public static int GetLastDuration(string name)
        {
            var m = Model.Items.Where(n => n.EventName == name && !n.IsRunning).OrderByDescending(n => n.StartTime).FirstOrDefault();
            if (m == null)
                return 0;
            else
                return m.DurationSeconds;
        }
    }
}
