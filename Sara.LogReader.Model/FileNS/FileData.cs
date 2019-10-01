using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Sara.Cache;
using Sara.Common.DateTimeNS;
using Sara.Logging;
using Sara.LogReader.Common;
using Sara.LogReader.Model.DocumentMap;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.Model.LogReaderNS;

namespace Sara.LogReader.Model.FileNS
{
    public partial class FileData : ICacheData
    {
        /// <summary>
        /// Returns True when the FileData has a Title.
        /// </summary>
        public bool HasTitle { get { return (Title != Path); } }
        /// <summary>
        /// A combination of Host Name and IP
        /// </summary>
        /// <remarks>
        /// Title is used by SockDrawer in the TreeList
        /// </remarks>
        public string Title { get; set; }
        /// <summary>
        /// The DateTime of the first entry
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// The DateTime of the last entry
        /// </summary>
        public DateTime End { get; set; }
        public string Path { get; set; }
        /// <summary>
        /// A FileData (Source) is can be given a SourceType
        /// Events and Values can be limited to only one SourceType
        /// SourceType of null is applied to all
        /// </summary>
        public string SourceType { get; set; }
        /// <summary>
        /// True when the file is no longer located in the Path
        /// </summary>
        public bool IsMissing { get; set; }
        /// <summary>
        /// Returns the Start and End DateTime as a string
        /// </summary>
        public string Range
        {
            get
            {
                if (Start.Date == End.Date)
                    return $"{Start} to {End.ToLongTimeString()}";
                return $"{Start} to {End}";
            }
        }
        /// <summary>
        /// Number of lines in the Document
        /// </summary>
        public int Count;
        public OptionsCacheData Options { get; set; }
        private List<int> _cachedNewLines;
        /// <summary>
        /// Using Regular Expressions to locate Events and Values work great to quickly find information.
        /// However to know which line the match is located on requires the use of NewLine.
        /// Thus we Cache the NewLine results so we only perform the search once! - Sara
        /// </summary>
        [XmlIgnore]
        public List<int> CachedNewLine
        {
            get
            {
                if (_cachedNewLines == null)
                    _cachedNewLines = RegularExpression.NewLines(RawTextString);

                return _cachedNewLines;
            }
        }
        private List<int> _cachedNewLinesFirstLines;
        [XmlIgnore]
        public List<int> CachedNewLineFirstLines
        {
            get
            {
                if (_cachedNewLinesFirstLines == null)
                    _cachedNewLinesFirstLines = RegularExpression.NewLines(RawTextStringFirstLines);

                return _cachedNewLinesFirstLines;
            }
        }


        /// <summary>
        /// True when the FileData contents are loaded
        /// </summary>
        [XmlIgnore]
        public bool IsLoaded { get; set; }
        /// <summary>
        /// When True the next UnLoad eventPattern will skip the UnLoad and reset SkipUnLoad to False
        /// </summary>
        [XmlIgnore]
        public bool SkipUnLoad { get; set; }
        /// <summary>
        /// If the EventPattern or Value is flagged to measure the time duration from the Parent Node
        /// </summary>
        [XmlIgnore]
        public List<ValueBookMark> DocumentMapDurations { get; set; }
        /// <summary>
        /// Contains a list of patterns in the log based on Class, Method, Network Method Name. - Sara LaFleur
        /// </summary>
        public List<FoldingEvent> FoldingEvents { get; set; }
        public List<DocumentEntry> DocumentMap { get; set; }
        public List<ValueBookMark> NonLazyValues { get; set; }
        public List<ValueBookMark> LazyValues { get; set; }
        /// <summary>
        /// Contains the FileValues including Lazy and Non-Lazy
        /// </summary>
        public List<ValueBookMark> FileValues
        {
            get
            {
                var result = new List<ValueBookMark>();
                result.AddRange(NonLazyValues);
                result.AddRange(LazyValues);
                return result;
            }
        }
        public FileNetwork Network { get; set; }
        private string _rawTextString;
        private object _rawTextSyncObject = new object();
        [XmlIgnore]
        public string RawTextString
        {
            get
            {
                // I believe this is causing a deadlock - Sara
                if (!IsLoaded)
                    Load(true);
                return _rawTextString;
            }
        }
        private List<string> _rawText;
        /// <remarks>
        /// By design we do not wan this saved in the xml data file. - Sara LaFleur
        /// </remarks>
        [XmlIgnore]
        public List<string> RawText
        {
            get
            {
                // This code will force RawTextString to load and thus populating _rawText, Odd I know - Sara
                // I believe this is causing a deadlock - Sara
                if (!IsLoaded)
                    Load(true);
                return _rawText;
            }
        }
        [XmlIgnore]
        private string _rawTextStringFirstLines;
        /// <summary>
        /// Optimization used to limit the number of .ToString on the RawTextFirstLine List
        /// </summary>
        public string RawTextStringFirstLines
        {
            get { return _rawTextStringFirstLines; }
        }
        private List<string> _rawTextFirstLines;
        /// <summary>
        /// Used by SockDrawer Search to speed up the search
        /// </summary>
        [XmlIgnore]
        public List<string> RawTextFirstLines
        {
            get
            {
                return _rawTextFirstLines;
            }
            set
            {
                _rawTextFirstLines = value;
                _rawTextStringFirstLines = string.Join(Environment.NewLine, value.ToArray());

            }
        }
        [XmlIgnore]
        private string _rawTextStringLastLines;
        /// <summary>
        /// Optimization used to limit the number of .ToString on the RawTextLastLine List
        /// </summary>
        public string RawTextStringLastLines
        {
            get { return _rawTextStringLastLines; }
        }
        private List<string> _rawTextLastLines;
        /// <summary>
        /// Used by SockDrawer Search to speed up the search
        /// </summary>
        [XmlIgnore]
        public List<string> RawTextLastLines
        {
            get
            {
                return _rawTextLastLines;
            }
            set
            {
                _rawTextLastLines = value;
                _rawTextStringLastLines = string.Join(Environment.NewLine, value.ToArray());

            }
        }
        public FileData()
        {
            Initialize();
        }
        private void Initialize()
        {
            NonLazyValues = new List<ValueBookMark>();
            LazyValues = new List<ValueBookMark>();
            FoldingEvents = new List<FoldingEvent>();
            DocumentMap = new List<DocumentEntry>();
            Network = new FileNetwork();
        }
        [XmlIgnore]
        public DocumentModel DocumentModel { get; set; }
        [XmlIgnore]
        public Action<LoadingStatus, string> LoadStatusNotificationEvent { get; set; }
        public string Key { get { return Path; } set { Path = value; } }
        [XmlIgnore]
        public CacheDataController DataController { get; set; }
        public int SourceTypeiLine { get; set; }

        public string Host
        {
            get { return FindFileValue(Keywords.HOSTNAME); }
        }
        public string Ip
        {
            get { return FindFileValue(Keywords.IP); }
        }
        public override string ToString()
        {
            return $"{System.IO.Path.GetFileName(Path)}";
        }

        public NetworkMessageInfo GetNetworkMessage(int iLine)
        {
            lock (Network)
            {
                return Network.NetworkMessages.FirstOrDefault(msg => msg.Source.iLine == iLine);
            }
        }
        /// <summary>
        /// Takes the SockDrawerFolder combined with the Path to determine if there is a Group Folder is use.
        /// If no Group Folder is use, then return Root
        /// </summary>
        public string GetGroupFolder(string sockDrawerFolder)
        {
            var folder = "Root";
            if (string.IsNullOrEmpty(sockDrawerFolder)) return folder;

            var folders = Path.Replace(sockDrawerFolder, "").Split('\\');
            if (folder.Count() > 2)
                folder = folders[1];
            return folder;
        }
        private static string ReadAllText(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var textReader = new StreamReader(fileStream))
            {
                return textReader.ReadToEnd();
            }
        }
        /// <summary>
        /// Used by Sock Drawer to prevent storing in memory the text. - Sara
        /// </summary>
        public void UnLoad()
        {
            // NOTE: The Setting on the following properties will clear the RawTextString, RawTextFirstLineString, and RawTextLastLinesString
            _rawText = new List<string>();
            RawTextFirstLines = new List<string>();
            RawTextLastLines = new List<string>();
            _cachedNewLines = null;
            _cachedNewLinesFirstLines = null;
            _rawTextString = null;
            //GC.Collect();
            IsLoaded = false;
        }
        /// <summary>
        /// Loads a FileData into memory.  internalLoad is used internally to skip the actual load
        /// </summary>
        public void Load(bool internalLoad)
        {
            if (!internalLoad)
                return;

            Log.WriteTrace($"Loading file \"{Path}\"",typeof(FileData).FullName, MethodBase.GetCurrentMethod().Name);

            var stopwatch = new Stopwatch(MethodBase.GetCurrentMethod().Name, false);
            try
            {
                if (_rawText == null || _rawText.Count == 0)
                {
                    _rawTextString = ReadAllText(Path).Replace("\n", Environment.NewLine);
                    _rawText = _rawTextString == null ? new List<string>() : _rawTextString.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
                    if (_rawText.Count == 1 && string.IsNullOrEmpty(_rawText[0]))
                        _rawText = new List<string>();
                }
                if (_rawText != null) Count = _rawText.Count;
                IsLoaded = true;
            }
            finally
            {
                stopwatch.Stop();
            }
        }
        /// <summary>
        /// Reads a block of data from the end of the file and converts that data to lines
        /// Skips the first line as it may be incomplete
        /// Number of lines will be random based on the size of data in each line
        /// Used by Search Sock Drawer for speed
        /// Does not load the entire file, only the last block of the file, thus the speed of the method
        /// </summary>
        private List<string> ReadLastLines()
        {
            var result = new List<string>();
            var size = 1024 * 10;

            using (var reader = new StreamReader(Path))
            {
                if (reader.BaseStream.Length > size)
                {
                    reader.BaseStream.Seek(-size, SeekOrigin.End);
                }

                string line;
                var skipFirstLine = true;
                while ((line = reader.ReadLine()) != null)
                {
                    if (skipFirstLine)
                    {
                        skipFirstLine = false;
                        continue;
                    }
                    result.Add(line);
                }
            }
            return result;
        }
        /// <summary>
        /// Loads only the first 100 lines and the last 100 into special fields used by SockDrawer Search
        /// </summary>
        public void FastLoad(int lines)
        {
            Log.WriteTrace($"Fast Loading file \"{Path}\"", typeof(FileData).FullName, MethodBase.GetCurrentMethod().Name);

            var stopwatch = new Stopwatch(MethodBase.GetCurrentMethod().Name, false);
            try
            {
                RawTextFirstLines = System.IO.File.ReadLines(Path).Take(lines).ToList();
                RawTextLastLines = ReadLastLines();
            }
            finally
            {
                stopwatch.Stop();
            }
        }
        /// <summary>
        /// Returns the first 200 characters to be used in determing a duplicate file
        /// </summary>
        /// <returns></returns>
        public string GetDuplicateText()
        {
            if (!System.IO.File.Exists(Path))
                return "";

            using (var fileStream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var textReader = new StreamReader(fileStream))
            {
                var c = new char[200];
                textReader.Read(c, 0, c.Length);

                return new string(c);
            }
        }
        /// <summary>
        /// Removes any prior values with the same name before adding the new value.
        /// Adds a FileData Value that will appear in the FileData Info section of the SockDrawer
        /// </summary>
        public void ReplaceFileInfoValue(string name, string value)
        {
            ValueBookMark prior = NonLazyValues.SingleOrDefault(n => n.Name == name);
            if (prior != null) NonLazyValues.Remove(prior);
            NonLazyValues.Add(new ValueBookMark() { Name = name, Value = value, FileInfo = true });
        }
    }
}
