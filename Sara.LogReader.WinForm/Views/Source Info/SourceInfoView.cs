using System;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.Source_Info;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.SourceInfo
{
    public partial class SourceInfoView : ToolWindow, IViewDock<SourceInfoModel, object>
    {
// ReSharper disable UnusedAutoPropertyAccessor.Local
        internal SourceInfoViewModel ViewModel { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Local

        public SourceInfoView()
        {
            InitializeComponent();
            sourceInfo1.ShowWarning();
        }

        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void Render(SourceInfoModel model)
        {
            // Note: Invoke is handled by ViewModelBase - Sara
            try
            {
                StatusPanel.StatusUpdate(StatusModel.Update("Loading..."));
                if (model == null)
                {
                    return;
                }

                sourceInfo1.Render(model.File);
            }
            finally
            {
                StatusPanel.StatusUpdate(StatusModel.Completed);                
            }
        }

        public void StatusUpdate(IStatusModel model)
        {
            StatusPanel.StatusUpdate(model);
        }

        public void UpdateView(object selectedModel)
        {
            throw new NotImplementedException();
        }

        private void sourceInfo1_GoTo(ValueBookMark obj)
        {
            ViewModel.GoTo(obj);
        }
    }
}