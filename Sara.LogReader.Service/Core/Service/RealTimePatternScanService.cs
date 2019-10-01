using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sara.Logging;
using Sara.LogReader.Model.PatternNS;

namespace Sara.LogReader.Service.Core.Service
{
    /// <summary>
    /// Handles parsing of a log in realtime to gather metrics
    /// </summary>
    public class RealTimePatternScanService
    {
        /// <summary>
        /// List of Patterns to scan for
        /// </summary>
        private readonly List<IPattern> _patterns = new List<IPattern>();
        /// <summary>
        /// When the Scan starts, all patterns are placed in Active Patterns
        /// When a pattern is found it is removed, unless the Pattern is Repeating
        /// </summary>
        private readonly List<IPattern> _activePatterns = new List<IPattern>();
        private bool _isRunning;
        public bool IsEmpty { get { return _patterns.Count == 0; } }
        #region UnitTesting
        /// <summary>
        /// StoreLog is only used for Unit Testing and should never be turned on in a production environment - Sara
        /// </summary>
        public bool StoreLog { get; set; }
        public string StoredLog { get; set; }
        #endregion UnitTesting
        public RealTimePatternScanService()
        {
            StoredLog = string.Empty;
        }
        public void AddPattern(IPattern pattern)
        {
            _patterns.Add(pattern);
        }
        public void RemovePattern(IPattern pattern)
        {
            _patterns.Remove(pattern);
        }
        public void Start()
        {
            if (_isRunning)
                Stop();

            lock (_activePatterns)
            {
                _activePatterns.Clear();
                _activePatterns.AddRange(_patterns);
            }

            _isRunning = true;
        }
        public void Stop()
        {
            _isRunning = false;
        }
        public void Reset()
        {
            foreach (var model in _activePatterns)
            {
                model.Reset();
            }
        }
        /// <summary>
        /// This method is designed to handle parsing of log entries in real time
        /// </summary>
        public void AnalyzeLine(ScanLineArgs args)
        {
            var lastIndex = -99;
            try
            {
                if (!_isRunning)
                    return;

                #region UnitTesting
                if (StoreLog)
                    StoredLog += args.Line + Environment.NewLine;
                #endregion UnitTesting

                lock (_activePatterns)
                {
                    if (_activePatterns.Count == 0)
                        return;

                    for (var i = _activePatterns.Count - 1; i >= 0; i--)
                    {
                        lastIndex = i;
                        var item = _activePatterns[i];

                        if (!item.Scan(args)) continue;

                        switch (item.ScanType)
                        {
                            case ScanType.FirstOccurance:
                                _activePatterns.Remove(item);
                                break;
                            case ScanType.Repeating:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError($"_activeRecipies.Count={_activePatterns.Count}, i={lastIndex}",typeof(RealTimePatternScanService).FullName, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Scans an entire Document
        /// </summary>
        /// <param name="lines"></param>
        public void Scan(string rawTextString, string sourceType)
        {
            /////
            // Scan for all Search Events
            /////
            // A Search eventPattern should scan the entire document at once, rather then line by line
            // Thus we will handle searches upfront - Sara
            lock (_activePatterns)
            {
                if (_activePatterns.Count == 0)
                    return;

                for (var i = _activePatterns.Count - 1; i >= 0; i--)
                {
                    var item = _activePatterns[i];

                    item.ScanSearch(rawTextString);

                    if (item.OnlySearch)
                        _activePatterns.RemoveAt(i);
                }

                if (_activePatterns.Count == 0)
                    return;
            }

            /////
            // Scan for the Patttern
            /////
            var lines = rawTextString.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            var iLine = 0;
            var iLineEOF = lines.Count - 1;

            foreach (var line in lines)
            {
                AnalyzeLine(new ScanLineArgs()
                {
                    TimeStamp = DateTime.Now,
                    Line = line,
                    iLine = iLine,
                    LastLine = iLine == iLineEOF,
                    SourceType = sourceType
                });
                iLine++;
            }
        }
    }
}
