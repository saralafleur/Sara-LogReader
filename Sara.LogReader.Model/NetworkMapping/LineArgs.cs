using System;
using Sara.WinForm.Notification;

namespace Sara.LogReader.Model.NetworkMapping
{
    /// <summary>
    /// Represents an entry in a FileData
    /// </summary>
    public class LineArgs
    {
        public string Path { get; set; }
        private string _line;

        /// <summary>
        /// Line can be assigned externally, if not the system will use Path and iLine to load the Line from a FileData
        /// </summary>
        public string Line
        {
            get
            {
                if (_line != null)
                    return _line;

                var file = XmlDal.CacheModel.GetFile(Path);
                if (file == null)
                    return null;

                _line = file.GetLine(iLine);
                return _line;
            }
            set { _line = value; }
        }

        // ReSharper disable once InconsistentNaming
        public int iLine { get; set; }
        public Action<IStatusModel> StatusUpdateEvent { get; set; }

        public void StatusUpdate(IStatusModel model)
        {
            StatusUpdateEvent?.Invoke(model);
        }
    }
}
