using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Sara.LogReader.Model;
using Sara.LogReader.Model.IDE;
using Sara.LogReader.Model.PatternNS;

namespace Sara.LogReader.Service
{
    public static class MonitorScriptService
    {
        public class Column
        {
            public Column()
            {
                Index = -1;
            }
            public string Name { get; set; }
            public int Index { get; set; }
            public override string ToString()
            {
                return $"{Index.ToString().PadLeft(3, ' ')} - {Name}";
            }
        }

        public static string GetGroupFolder(string sockDrawerFolder, string path)
        {
            var folder = "Root";
            if (string.IsNullOrEmpty(sockDrawerFolder)) return folder;

            var folders = path.Replace(sockDrawerFolder, "").Split('\\');
            if (folder.Count() > 2)
                folder = folders[1];
            return folder;
        }


        public const string PATH = "Path";
        public const string GROUP = "Group";
        public const string ILINESTARTING = "Starting iLine";
        public const string ILINEENDING = "Ending iLine";
        public const string PATTERNDURATION = "Duration";
        public const string PATTERNNAME = "Pattern Name";
        public const string EVENTNAME = "EventPattern Name";
        public const string ILINEEVENT = "EventPattern iLine";
        public const string TOTALTIME = "Total Time";
        public const string TOTALIDLETIME = "Total Idle Time";
        public const string TOTALOVERLAPPING = "Total Overlapping Time";
        public const string MAXOVERLAPS = "Max Overlaps";
        public const string TIMEGAP = "Time Gap";
        public const string WARNING = "Warning";
        public const string DURATION = "Duration";

        public static DataTable GetPatternsDataTable(List<PatternTestResult> patterns)
        {
            var dt = new DataTable();

            var _columns = new List<string>();


            foreach (var p in patterns)
            {
                var _newColumns = new List<Column>();
                foreach (var e in p.Events)
                    _newColumns.Add(new Column() { Name = GetColumnName(e) });

                MergeInOrder(_newColumns, _columns);
            }

            _columns.Insert(0, PATH);
            _columns.Insert(1, GROUP);
            _columns.Insert(2, PATTERNNAME);
            _columns.Insert(3, PATTERNDURATION);

            var _uniqueColumns = MakeColumnNamesUnique(_columns);

            foreach (var c in _uniqueColumns)
                dt.Columns.Add(new DataColumn(c, typeof(string)));

            var sockDrawerFolder = XmlDal.DataModel.Options.SockDrawerFolder;

            var current = 0;
            foreach (var p in patterns)
            {
                current++;
                //if (current % 100 == 0)
                //    StatusPanel.StatusUpdate(StatusModel.Update($"Rendering {current} of {patterns.Count}"));

                var row = new List<object>
                    {
                        p.Path,
                        GetGroupFolder(sockDrawerFolder, p.Path),
                        p.PatternName,
                        p.TotalDuration,
                    };
                while (row.Count != _columns.Count)
                    row.Add("");

                var index = 3;
                foreach (var e in p.Events)
                {
                    var name = GetColumnName(e);
                    //Find the next column that matches
                    while (name != _columns[index])
                        index++;

                    row[index] = e.Name;

                    //foreach (var v in pe.Values)
                    //{
                    //    var vText = v.Value.ToString();
                    //    if (v.Value is TimeSpan ts)
                    //        vText = ts.ToShortReadableString();

                    //    var valueNode = new TreeNode($"{v.Name} {vText}")
                    //    {
                    //        Tag = new DocumentEntry
                    //        {
                    //            Name = v.Name,
                    //            iLine = pe.iLine,
                    //            Type = DocumentMapType.EventInstanceValue,
                    //            Path = p.Path,
                    //            Value = v.Value.ToString(),
                    //            ValueObject = v.Value
                    //        }
                    //    };
                    //    AddNode(ref parent, valueNode, false);
                    //}


                }
                dt.Rows.Add(row.ToArray());
            }


            return dt;
        }

        private static List<string> MakeColumnNamesUnique(List<string> columns)
        {
            var result = new List<string>();
            foreach (var c in columns)
            {
                if (!result.Contains(c))
                {
                    result.Add(c);
                    continue;
                }

                int count = 1;
                while (true)
                {
                    var cc = $"{c}<{count}>";
                    if (result.Contains(cc))
                    {
                        count++;
                        continue;
                    }
                    result.Add(cc);
                    break;
                }
            }
            return result;
        }

        private static void MergeInOrder(List<Column> newColumns, List<string> columns)
        {
            // If this is the first row, then simple add the columns
            if (columns.Count == 0)
            {
                foreach (var c in newColumns)
                    columns.Add(c.Name);
                return;
            }

            // First Pass Simple Match
            var cIndex = 0;
            var nIndex = 0;
            while (cIndex < columns.Count && nIndex < newColumns.Count)
            {
                if (newColumns[nIndex].Name == columns[cIndex])
                {
                    newColumns[nIndex].Index = cIndex;
                    nIndex++;
                    cIndex++;
                    continue;
                }

                // Does the new column exists looking forward, if not skip it
                var jumpIndex = ForwardLookup(columns, cIndex + 1, newColumns[nIndex].Name);
                if (jumpIndex != -1)
                {
                    cIndex = jumpIndex;
                    continue;
                }

                // New column does not exists yet, so skip it
                nIndex++;
            }

            // Now Insert new colums
            var lastIndex = columns.Count - 1;
            for (int i = newColumns.Count - 1; i >= 0; i--)
            {
                if (newColumns[i].Index == -1)
                {
                    columns.Insert(lastIndex, newColumns[i].Name);
                    continue;
                }
                lastIndex = newColumns[i].Index;
            }

        }

        /// <summary>
        /// Looking forward check to see if the column name exists in Columns
        /// </summary>
        private static int ForwardLookup(List<string> columns, int cIndex, string name)
        {
            for (int i = cIndex; i < columns.Count; i++)
                if (columns[i] == name)
                    return i;
            return -1;
        }

        private static string GetColumnName(PatternTestResultEventPattern pe)
        {
            switch (pe.EventType)
            {
                case EventType.Start:
                case EventType.Body:
                case EventType.Stop:
                case EventType.Reset:
                case EventType.Search:
                case EventType.Restart:
                    return pe.Name;
                case EventType.TotalTime:
                    return TOTALTIME;
                case EventType.GapOutside:
                case EventType.GapInside:
                    return TIMEGAP;
                case EventType.TotalIdleTime:
                    return TOTALIDLETIME;
                case EventType.MaxOverlaps:
                    return MAXOVERLAPS;
                case EventType.TotalOverlappingTime:
                    return TOTALOVERLAPPING;
                case EventType.Warning:
                    return WARNING;
                case EventType.Duration:
                    return DURATION;
                default:
                    throw new Exception("Unknown EventType");
            }
        }
    }
}
