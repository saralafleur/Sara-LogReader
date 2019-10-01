using System.IO;

namespace Sara.LogReader.Model
{
    public static class DataPath
    {
        private const string NCR_LOGREADER_DATA_FOLDER = "Data\\";
        private const string NCR_LOGREADER_CACHE_XML = "Sara.LogReader.Cache.xml";
        private const string NCR_LOGREADER_DATA_XML = "Sara.LogReader.Data.xml";
        private const string NCR_LOGREADER_PERFORMANCE_XML = "Sara.LogReader.Performance.xml";
        private const string NCR_LOGREADER_DOCKPANEL_CONFIG = "DockPanel.config";
        private static string GetDataFolder()
        {
            var folder = Path.Combine(Path.GetDirectoryName(ExecutablePath),
                                      NCR_LOGREADER_DATA_FOLDER);
#pragma warning disable CS0618 // Type or member is obsolete
            var overrideFolder = System.Configuration.ConfigurationSettings.AppSettings.Get("DataFolder");
#pragma warning restore CS0618 // Type or member is obsolete
            if (!string.IsNullOrEmpty(overrideFolder))
                folder = overrideFolder;
            return folder;
        }
        public static string ExecutablePath { get; set; }
        public static string GetConfigFilePath()
        {
            return Path.Combine(GetDataFolder(), NCR_LOGREADER_DOCKPANEL_CONFIG);
        }
        internal static string GetDataFilePath()
        {
            return GetXMLFilePath(NCR_LOGREADER_DATA_XML);
        }
        public static string GetPerformanceFilePath()
        {
            return GetXMLFilePath(NCR_LOGREADER_PERFORMANCE_XML);
        }
        private static string GetXMLFilePath(string file)
        {
            return Path.Combine(GetDataFolder(), file);
        }
        internal static string GetCacheFilePath()
        {
            return Path.Combine(GetDataFolder(), NCR_LOGREADER_CACHE_XML);
        }
    }
}
