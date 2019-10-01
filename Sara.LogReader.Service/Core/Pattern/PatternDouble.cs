using System;
using System.Collections.Generic;
using Sara.LogReader.Model.PatternNS;

namespace Sara.LogReader.Service.Core.Pattern
{
    /// <summary>
    /// Pattern for finding a Double (type) value
    /// </summary>
    public class PatternDouble : IPattern
    {
        public bool Found { get; set; }
        public string SearchText { get; set; }
        public ScanType ScanType { get; set; }
        public event Action<IPattern> OnFound;

        public void Reset()
        {
            // Does nothing
        }

        /// <summary>
        /// Once the Pattern is found, Value is populated with the result
        /// </summary>
        public double Value { get; set; }

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
            var value = double.NaN;
            if (Common.GetDoubleFromLog(args.Line, SearchText, ref value))
            {
                Value = value;
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

        public PatternDouble(string name, string searchText, ScanType searchType, Action<IPattern> callback)
        {
            Name = Name;
            SearchText = searchText;
            ScanType = searchType;
            OnFound += callback;
        }
    }
}
