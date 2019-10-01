using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Sara.Cache;
using Sara.LogReader.Common;
using Sara.LogReader.Model.ColorScheme;
using Sara.LogReader.Model.IDE;
using Sara.LogReader.Model.NetworkMapNS;
using Sara.WinForm.ColorScheme.Modal;

namespace Sara.LogReader.Model.LogReaderNS
{
    [XmlType("OptionsCacheData")]
    public class OptionsCacheData : ICacheData
    {
        public OptionsCacheData()
        {
            NetworkMap = new NetworkMapOptions();
            FileTypes = new List<string>();
            HideOptions = new List<string>();
            Clear();
            ColorScheme = new ColorSchemeCollection<ColorSchemeLogReaderModal>();
            RestoreOpenDocumentsList = new List<string>();
        }
        public string SockDrawerFolder { get; set; }
        public List<string> HideOptions { get; set; }
        public bool DateTime { get; set; }
        public bool EntryType { get; set; }
        public bool ThreadId { get; set; }
        public bool Class { get; set; }
        public bool Method { get; set; }
        public bool NetworkInfo { get; set; }

        public bool All
        {
            get { return DateTime && EntryType && ThreadId && Class && Method && NetworkInfo; }
        }

        public void Clear()
        {
            // Default
            DateTime = false;
            EntryType = false;
            ThreadId = false;
            Class = false;
            Method = false;
            NetworkInfo = false;
            DocumentFolding = false;
            // Default
            SockDrawerFolder = string.Empty;
            FileTypes.Clear();
            NetworkMap.Clear();
            DocumentMapGapMinDuration = DefaultValue.DOCUMENT_MAP_GAP_MIN_DURATION;
            ColorScheme = new ColorSchemeCollection<ColorSchemeLogReaderModal>();
        }

        /// <summary>
        /// List of FileTypes, used to filter which Events and Values are used
        /// </summary>
        public List<string> FileTypes { get; set; }
        public int NonLazyValueWarningTimeout { get; set; }

        public bool IsCached { get; set; }

        [XmlIgnore]
        public Action<LoadingStatus, string> LoadStatusNotificationEvent { get; set; }
        public string Key { get; set; }
        [XmlIgnore]
        public CacheDataController DataController { get; set; }

        public bool ShowDocumentMapLineNumber { get; set; }
        public bool ShowToolBar { get; set; }
        public bool ShowCurrentLineBar { get; set; }
        /// <summary>
        /// When True all docuemnts that were open will be re-opened next time the Appliation is launched.
        /// </summary>
        public bool RestoreOpenDocuments { get; set; }
        public bool DocumentFolding { get; set; }
        /// <summary>
        /// When True the Source Info will be displayed at the top of each Document Map.
        /// </summary>
        public bool DocumentSourceInfo { get; set; }
        public NetworkMapOptions NetworkMap { get; set; }
        /// <summary>
        /// Any gap that is greater then the specified duration will appear as a Gap in the Document Map
        /// </summary>
        public int DocumentMapGapMinDuration { get; set; }
        public IDEModel IDEScript { get; set; }
        /// <summary>
        /// List of Open documents that will be restored when the Application launches again
        /// </summary>
        public List<string> RestoreOpenDocumentsList { get; set; }
        private ColorSchemeCollection<ColorSchemeLogReaderModal> _colorScheme;
        public ColorSchemeCollection<ColorSchemeLogReaderModal> ColorScheme
        {
            get
            {
                return _colorScheme;
            }
            set
            {
                _colorScheme = value;

                ColorService.LoadCollection(value.Collection);
                ColorService.ColorScheme.ActiveColorScheme = value.ActiveColorScheme;
                ColorService.Invalidate();
            }
        }

        public void Load(bool internalLoad)
        {
            // Do nothing on Load
        }
    }
}
