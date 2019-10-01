using System;
using System.Linq;

namespace Sara.LogReader.Model.FileNS
{
    /// <remarks>
    /// I did not want to include too much business Logic in the FileData class. 
    /// Generally I would place such logic into a Service in the Sara.LogReader.Services namespace
    /// However I needed to handle an invalidate eventPattern within the FileData itself and I don't want a reference to Sara.LogReader.Services since the Service Layer should be below the Modal Layer
    /// I don't have a good plan on how to best handle this, so for now I have created this partial class to seperate the Modal from the Business Logic - Sara
    /// </remarks>
    public partial class FileData
    {
        /// <summary>
        /// Returns the value for the given name.  If no value is found then return null.
        /// </summary>
        public string FindFileValue(string name)
        {
            return (from value in FileValues where value.Name == name select value.Value).FirstOrDefault();
        }

        /// <summary>
        /// Returns the line based on iLine
        /// If the file is not loaded, the system will load the file and then unload it
        /// </summary>
        public string GetLine(int iLine)
        {
            lock (this)
            {
                var unLoad = false;
                try
                {
                    if (!IsLoaded)
                    {
                        Load(true);
                        unLoad = true;
                    }
                    var lines = RawTextString.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
                    return lines[iLine];
                }
                finally
                {
                    if (unLoad)
                        UnLoad();
                }
            }
        }
    }
}
