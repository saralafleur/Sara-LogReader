using System;
using System.Windows.Forms;
using Sara.LogReader.Model.LogReaderNS;
using Sara.WinForm.Notification;
using WeifenLuo.WinFormsUI.Docking;

namespace Sara.LogReader.WinForm.ViewModel
{
    public interface IMainView
    {
        Form ActiveMdiChild { get; }
        void ActiveDocumentChanged();
        DockPanel DockPanel { get; }
        Form[] MdiChildren { get; }
        bool RightToLeftLayout { get; set; }
        bool InvokeRequired { get; }
        object Invoke(Delegate method);
        object Invoke(Delegate method, params object[] args);
        IAsyncResult BeginInvoke(Delegate method);
        IAsyncResult BeginInvoke(Delegate method, params object[] args);
        void ShowError(string message);
        void StatusUpdate(IStatusModel model);
        void ActiveDocumentLineChanged();
        void LoadLayout(string configFile, DeserializeDockContent mDeserializeDockContent);
        void SetOptions(OptionsCacheData options);
        void SetCursor(Cursor waitCursor);
        void UpdateCurrentLine(string empty);
        void Refresh();
        void PrepareHideOptions();
    }
}
