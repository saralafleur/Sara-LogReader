using System;
using System.Collections.Generic;
using Sara.LogReader.Model.PatternNS;

namespace Sara.LogReader.Service.Core.Pattern
{
    /// <summary>
    /// Pattern for finding a string
    /// </summary>
    public class PatternString : IPattern
    {
        public string SearchText { get; set; }
        public ScanType ScanType { get; set; }
        public event Action<IPattern> OnFound;

        public void Reset()
        {
            // Does nothing yet
        }

        /// <summary> 
        /// Once the Pattern is found, Value is populated with the result
        /// </summary>
        public string Value { get; set; }

        public bool Found { get; set; }

        public string Name { get; set; }

        public bool OnlySearch { get { return false; } }

        public List<IEventPattern> Events
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public List<IEventPattern> OrginalEvents { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Returns true when the Pattern found a match
        /// </summary>
        public bool Scan(ScanLineArgs args)
        {
            var s = string.Empty;

            if (Common.GetStringFromLog(args.Line, SearchText, ref s))
            {
                Value = s;
                OnFound?.Invoke(this);
                Found = true;
                return true;
            }

            return false;
        }

        public bool ScanSearch()
        {
            throw new NotImplementedException();
        }

        public void ScanSearch(string rawText)
        {
            throw new NotImplementedException();
        }

        public PatternString(string name, string searchText, ScanType searchType, Action<IPattern> callback)
        {
            Name = name;
            SearchText = searchText;
            ScanType = searchType;
            OnFound += callback;
        }
    }
}
