using System.Collections.Generic;

namespace Sara.LogReader.Model.Settings
{
    public class SettingsModel
    {
        public string SockDrawerFolder { get; set; }
        public int NonLazyValueWarningTimeout { get; set; }
        public List<string> FileTypes { get; set; }

        public SettingsModel()
        {
            NonLazyValueWarningTimeout = 30; // Default to 30 seconds
        }
    }
}
