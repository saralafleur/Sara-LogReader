using System.Data;
using Sara.LogReader.Model.IDE;

namespace Sara.LogReader.Service.PatternServiceNS
{
    public static class PatternService
    {
        public static DataTable PrepareCSVExport(PatternTestResults lastResults)
        {
            var dt = new DataTable();

            // TODO: Need to update with new data structure. - Sara

            //dt.Columns.Add(new DataColumn("Id", typeof(int)));
            //dt.Columns.Add(new DataColumn("Total Duration", typeof(string)));
            //dt.Columns.Add(new DataColumn("Total Duration Outside", typeof(string)));
            //dt.Columns.Add(new DataColumn("Total Duration Inside", typeof(string)));
            //dt.Columns.Add(new DataColumn("Total IdleTime Outside", typeof(string)));
            //dt.Columns.Add(new DataColumn("Total IdleTime Inside", typeof(string)));
            //dt.Columns.Add(new DataColumn("Total Within Baseline", typeof(string)));
            //dt.Columns.Add(new DataColumn("Total Outside Baseline", typeof(string)));
            //dt.Columns.Add(new DataColumn("Total No Baseline", typeof(string)));

            //dt.Columns.Add(new DataColumn("Within Baseline", typeof(string)));
            //dt.Columns.Add(new DataColumn("Outside Baseline", typeof(string)));
            //dt.Columns.Add(new DataColumn("No Baseline", typeof(string)));
            //dt.Columns.Add(new DataColumn("Duration", typeof(string)));
            //dt.Columns.Add(new DataColumn("Duration Outside Of Duration", typeof(string)));
            //dt.Columns.Add(new DataColumn("Duration Inside Of Duration", typeof(string)));
            //dt.Columns.Add(new DataColumn("IdleTime Outside Of Duration", typeof(string)));
            //dt.Columns.Add(new DataColumn("IdleTime Inside Of Duration", typeof(string)));
            //dt.Columns.Add(new DataColumn("DateTime", typeof(string)));
            //dt.Columns.Add(new DataColumn("EventPattern", typeof(string)));
            //var maxPatternsForExcel = lastResults.Patterns.Where(n => n.Events.Any()).Max(n => n.Events[0].DurationPatternForExcel.Count());

            //var _col = 'a';
            //for (int n = 0; n < maxPatternsForExcel; n++)
            //{
            //    dt.Columns.Add(new DataColumn($"{_col}", typeof(char)));
            //    _col++;
            //}

            //var i = 1;
            //foreach (var item in lastResults.Patterns)
            //{
            //    var _totalDurationOutside = new TimeSpan();
            //    var _totalDurationInside = new TimeSpan();
            //    var _totalIdleTimeOutside = new TimeSpan();
            //    var _totalIdleTimeInside = new TimeSpan();
            //    var _totalWithinBaseline = 0;
            //    var _totalOutsideBaseline = 0;
            //    var _totalNoBaseline = 0;

            //    foreach (var e in item.Events)
            //    {
            //        _totalDurationOutside = _totalDurationOutside.Add(e.DurationOutsideOfDuration);
            //        _totalDurationInside = new TimeSpan().Add(e.DurationInsideOfDuration);
            //        _totalIdleTimeOutside = _totalIdleTimeOutside.Add(e.IdleTimeOutsideOfDuration);
            //        _totalIdleTimeInside = _totalIdleTimeInside.Add(e.IdleTimeInsideOfDuration);
            //        _totalWithinBaseline += (e.WithinBaseline ? 1 : 0);
            //        _totalOutsideBaseline += (e.OutsideBaseline ? 1 : 0);
            //        _totalNoBaseline += (e.NoBaseline ? 1 : 0);
            //    }

            //    foreach (var e in item.Events)
            //    {
            //        var row = new List<object>
            //        {
            //            i,
            //            item.TotalDuration.ToExcelString(),
            //            _totalDurationOutside.ToExcelString(),
            //            new TimeSpan().ToExcelString(),
            //            _totalIdleTimeOutside.ToExcelString(),
            //            _totalIdleTimeInside.ToExcelString(),
            //            _totalWithinBaseline.ToExcelString(),
            //            _totalOutsideBaseline.ToExcelString(),
            //            _totalNoBaseline.ToExcelString()
            //        };

            //        row.Add(e.WithinBaseline.ToExcelString());
            //        row.Add(e.OutsideBaseline.ToExcelString());
            //        row.Add(e.NoBaseline.ToExcelString());
            //        row.Add(e.Duration.ToExcelString());
            //        row.Add(e.DurationOutsideOfDuration.ToExcelString());
            //        row.Add(e.DurationInsideOfDuration.ToExcelString());
            //        row.Add(e.IdleTimeOutsideOfDuration.ToExcelString());
            //        row.Add(e.IdleTimeInsideOfDuration.ToExcelString());

            //        row.Add(e.DateTime.Value.ToString(DateTimeExt.DATE_FORMAT));
            //        row.Add(e.Name);
            //        for (int n = 0; n < maxPatternsForExcel; n++)
            //        {
            //            if (n < e.DurationPatternForExcel.Count)
            //                row.Add(e.DurationPatternForExcel[n] ? 'x' : ' ');
            //            else
            //                row.Add(' ');
            //        }
            //        // Add Row
            //        DataRow dr = dt.NewRow();
            //        dr.ItemArray = row.ToArray();
            //        dt.Rows.Add(dr);
            //    }
            //    i++;
            //}

            return dt;
        }
    }
}
