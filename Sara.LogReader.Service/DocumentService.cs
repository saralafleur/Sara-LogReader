using Sara.Common.DateTimeNS;
using Sara.LogReader.Model.LogReaderNS;
using Sara.LogReader.Model.NetworkMapping;
using Sara.LogReader.Model.Property;
using Sara.LogReader.Service.FileServiceNS;

namespace Sara.LogReader.Service
{
    public static class DocumentService
    {

        /// <summary>
        /// Returns
        /// </summary>
        public static void BuildDocument(DocumentModel model)
        {
            var stopwatch = new Stopwatch("BuildDocument");
            try
            {
                FileService.Build(model.File,true);
            }
            finally
            {
                stopwatch.Stop();
            }
        }
        /// <summary>
        /// Prepares the Document Map
        /// </summary>
        public static string AnalysizePercentDocumentated(DocumentModel model)
        {
            var totalMatches = 0;
            // ReSharper disable LoopCanBeConvertedToQuery
            var index = 0;
            foreach (var line in model.File.RawText)
            // ReSharper restore LoopCanBeConvertedToQuery
            {
                var property = PropertyService.GetProperty(new LineArgs
                {
                    Path = model.File.Path,
                    iLine = index,
                    Line = line
                });

                if (property is PropertyLookup)
                    totalMatches++;
                index++;
            }
            return totalMatches == 0
                       ? "There is no documentation for this log."
                       : $"{(totalMatches / (float) model.File.RawText.Count) * 100:F}% of the log is documentated";
        }
    }
}
