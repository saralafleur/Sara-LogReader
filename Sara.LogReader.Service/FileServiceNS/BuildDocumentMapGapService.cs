using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sara.Common.DateTimeNS;
using Sara.Common.Extension;
using Sara.LogReader.Common;
using Sara.LogReader.Model;
using Sara.LogReader.Model.DocumentMap;
using Sara.LogReader.Model.FileNS;

namespace Sara.LogReader.Service.FileServiceNS
{
    public class TwoLines
    {
        public string FirstLine { get; set; }
        public int FirstiLine { get; set; }
        public string SecondLine { get; set; }
        public int SecondiLine { get; set; }
    }

    public class BuildDocumentMapGapService
    {
        private int DocumentMapGapMinDuration;

        private void RunItem(TwoLines t)
        {
            if (!HasGap(t, DocumentMapGapMinDuration, out TimeSpan diff)) return;

            //var start = new DocumentEntry
            //{
            //    Name = "GAP START",
            //    iLine = t.FirstiLine,
            //    Text = t.FirstLine.RemoveInvalidXMLChars(),
            //    Type = DocumentMapType.TimeGAPStart,
            //    Level = DocumentMapLevel.Sibling,
            //};
            var stop = new DocumentEntry
            {
                Name = diff.ToShortReadableString(),
                iLine = t.SecondiLine,
                Text = t.SecondLine.RemoveInvalidXMLChars(),
                Type = DocumentMapType.TimeGAPEnd,
                Level = DocumentMapLevel.Sibling,
                ValueObject = diff
            };
            lock (Result)
            {

                //Result.Add(start);
                Result.Add(stop);
            }
        }

        private static bool HasGap(TwoLines t, int threshold, out TimeSpan diff)
        {
            DateTime firstDate;
            DateTime secondDate;

            if (
                !DateTimeExt.TryParseWithTimeZoneRemoval(RegularExpression.GetTextBetweenTwoCharacters("<", ">", t.FirstLine), out firstDate) ||
                !DateTimeExt.TryParseWithTimeZoneRemoval(RegularExpression.GetTextBetweenTwoCharacters("<", ">", t.SecondLine), out secondDate))
            {
                diff = new TimeSpan(0);
                return false;
            }

            diff = secondDate - firstDate;

            return (diff.TotalMilliseconds >= threshold);
        }

        internal static List<DocumentEntry> PostProcess(List<DocumentEntry> list)
        {
            var lastEntry = list[list.Count-1];
            for (int i = list.Count - 2; i >= 0; i--)
            {
                if (list[i].Type == DocumentMapType.TimeGAPEnd && lastEntry.Type == DocumentMapType.TimeGAPEnd)
                {
                    list[i].ValueObject = (TimeSpan)list[i].ValueObject + (TimeSpan)lastEntry.ValueObject;
                    list[i].Name = ((TimeSpan)list[i].ValueObject).ToShortReadableString();
                    list.RemoveAt(i + 1);
                }
                lastEntry = list[i];
            }

            // TimeSpan is not friendly to our XML Data Store and we do not need it, so remove it here.
            Parallel.ForEach(list, (item) => {
                if (item.Type == DocumentMapType.TimeGAPEnd)
                    item.ValueObject = null;
            });

            return list;
        }

        private static IEnumerable<TwoLines> PrepareLines(IList<string> rawText)
        {
            var result = new List<TwoLines>();
            string priorLine = null;
            var priorLineIndex = 0;
            for (var i = 0; i < rawText.Count - 1; i++)
            {
                // This code should only execute once. - Sara
                if (priorLine == null)
                {
                    priorLine = rawText[i];
                    priorLineIndex = i;
                    continue;
                }

                // Skip lines that are not standard format
                if (String.IsNullOrEmpty(rawText[i]) || rawText[i][0] != '<')
                    continue;

                result.Add(new TwoLines
                {
                    FirstLine = priorLine,
                    FirstiLine = priorLineIndex,
                    SecondLine = rawText[i],
                    SecondiLine = i
                });

                priorLine = rawText[i];
                priorLineIndex = i;
            }
            return result;
        }
        public List<DocumentEntry> Result { get; set; }
        public List<DocumentEntry> Execute(FileData file)
        {
            var sw = new Stopwatch($"Build DocumentMap Gaps for \"{file.Path}\"");
            DocumentMapGapMinDuration = XmlDal.DataModel.Options.DocumentMapGapMinDuration;
            Result = new List<DocumentEntry>();

            Parallel.ForEach(new List<TwoLines>(PrepareLines(file.RawText)), (item) =>
            {
                RunItem(item);
            });

            sw.Stop(0);
            return Result;
        }
    }
}
