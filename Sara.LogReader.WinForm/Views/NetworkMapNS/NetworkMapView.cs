using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Sara.Logging;
using Sara.LogReader.Model;
using Sara.LogReader.Model.NetworkMapNS;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.NetworkMapNS
{
    public partial class NetworkMapView : ToolWindow, IViewDock<NetworkMapModel, object>
    {
        protected internal NetworkMapViewModel ViewModel { get; set; }

        public NetworkMapView()
        {
            InitializeComponent();

            ShowWarning();
        }

        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void Render(NetworkMapModel model)
        {
            // Note: Invoke is handled by ViewModelBase - Sara

            if (model == null)
            {
                StatusPanel.StatusUpdate(StatusModel.Completed);
                ShowWarning();
                return;
            }
            
            Log.WriteTrace(string.Format("Network Map View for file \"{0}\"", Path.GetFileName(model.NetworkMap.AnchorFilePath)), typeof(NetworkMapView).FullName, MethodBase.GetCurrentMethod().Name);

            ckbAnchor.Checked = model.NetworkMap.Anchored;
            ckbAnchor.Text = string.Format("Anchor - {0}", model.CurrentAnchor);
            
            HideWarning();
            
            StatusUpdate(StatusModel.Update("Rendering"));
            try
            {
                cklbSources.Items.Clear();
                foreach (var sequenceSource in model.NetworkMap.Nodes)
                {
                    cklbSources.Items.Add(sequenceSource, sequenceSource.Selected);
                }

                dgvSequenceDiagram.SuspendLayout();
                dgvSequenceDiagram.DataSource = null;
                dgvSequenceDiagram.DataSource = model.DataTable;
            }
            finally
            {
                dgvSequenceDiagram.ResumeLayout();
                DataGridViewHelper.AutoSizeGridCell(dgvSequenceDiagram);
                StatusUpdate(StatusModel.Completed);
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

        private void HideWarning()
        {
            panel1.Visible = true;
            wpOpen.Visible = false;
        }
        private void ShowWarning()
        {
            panel1.Visible = false;
            wpOpen.Visible = true;
            wpOpen.Dock = DockStyle.Fill;
        }
        private void ckbAnchor_CheckedChanged(object sender, EventArgs e)
        {
            ViewModel.SetAnchor(ckbAnchor.Checked);
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            foreach (var networkMapFile in XmlDal.DataModel.Options.NetworkMap.Nodes)
            {
                networkMapFile.Selected = false;
            }

            foreach (NetworkMapFile item in cklbSources.CheckedItems)
            {
                item.Selected = true;
            }
            ViewModel.RenderDocument();
        }
        private void dgvSequenceDiagram_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                int col = dgvSequenceDiagram.CurrentCell.ColumnIndex;
                int row = dgvSequenceDiagram.CurrentCell.RowIndex;

                var msg = dgvSequenceDiagram.Rows[row].Cells[col].Value as NetworkMapViewModel.SequenceMessage;
                if (msg == null)
                    return;

                MainViewModel.GoToFile(msg.Message.Source.FilePath);
                MainViewModel.GoToLine(msg.Message.Source.iLine);
                dgvSequenceDiagram.Focus();

                e.Handled = true;
            }
        }
    }
}
