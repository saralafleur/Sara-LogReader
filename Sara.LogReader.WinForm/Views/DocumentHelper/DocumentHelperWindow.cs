using System;
using System.Drawing;
using System.Windows.Forms;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.DocumentHelper
{
    public partial class DocumentHelperWindow : ToolWindow, IViewDock<object, object>
    {
        public DocumentHelperViewModel ViewModel { get; set; }

        public DocumentHelperWindow()
        {
            InitializeComponent();
            lbEvents.Font = new Font(FontFamily.GenericMonospace, lbEvents.Font.Size);
        }

        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void Render(object m)
        {
            // Note: Invoke is handled by ViewModelBase - Sara
            if (m == null)
            {
                panel2.Visible = false;
                warningPanel.Visible = true;
                warningPanel.Dock = DockStyle.Fill;
                StatusPanel.StatusUpdate(StatusModel.Completed);
                return;
            }

            warningPanel.Visible = false;
            StatusPanel.StatusUpdate(StatusModel.Update("Loading..."));
            try
            {
                panel2.Visible = true;

            }
            finally
            {
                StatusPanel.StatusUpdate(StatusModel.Completed);
            }
        }

        private void ckbOnlyNetwork_CheckedChanged(object sender, EventArgs e)
        {
            ViewModel.RenderDocument();
        }
        private void ckbHideDocumented_CheckedChanged(object sender, EventArgs e)
        {
            ViewModel.RenderDocument();
        }
        private void lbEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectionChanged();
        }
        private void lbEvents_Enter(object sender, EventArgs e)
        {
            SelectionChanged();
        }

        /// <summary>
        /// Updates the UI based on the new selection
        /// </summary>
        private void SelectionChanged()
        {
            var enabled = (lbEvents.SelectedItem != null);
            btnPrior.Enabled = enabled;
            btnNext.Enabled = enabled;

        }

        public void StatusUpdate(IStatusModel model)
        {
            StatusPanel.StatusUpdate(model);
        }

        public void UpdateView(object selectedModel)
        {
            throw new NotImplementedException();
        }
    }
}
