using System;
using Sara.LogReader.Common;

namespace Sara.LogReader.Service
{
    public static class ReportService
    {
        public static string GenerateReportName()
        {
            return $"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}";
        }

        public static string GenerateReRunReportName(string name)
        {
            var runInteger = 1;
            if (name.Contains("Re-run"))
            {
                var run = RegularExpression.GetFirstValue(name, @"\((.*)\)");
                if (int.TryParse(run, out runInteger))
                    runInteger++;
            }

            var nameOnly = RegularExpression.GetFirstValue(name, @"\) for (.*)") ?? name;

            return $"Re-run ({runInteger}) for {nameOnly}";
        }
    }
}
