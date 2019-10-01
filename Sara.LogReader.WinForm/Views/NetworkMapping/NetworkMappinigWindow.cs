using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Sara.Logging;
using Sara.LogReader.Common;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.NetworkMapping;
using Sara.LogReader.WinForm.Views.ToolWindows;
using TreeNode = System.Windows.Forms.TreeNode;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm;
using Sara.WinForm.Common;
using Sara.WinForm.CRUD;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.NetworkMapping
{
    public partial class NetworkMappingWindow : ToolWindow, IViewDock<object, object>
    {
        protected internal NetworkMapingViewModel ViewModel { get; set; }

        #region Form

        public void UpdateView(NetworkMapModel model)
        {
            switch (model.Mode)
            {
                case InputMode.Add:
                    lbNetworkMaps.Items.Add(model.Item);
                    lbNetworkMaps.SelectedIndex = lbNetworkMaps.Items.Count - 1;
                    break;
                case InputMode.Edit:
                    var index = lbNetworkMaps.SelectedIndex;
                    if (index > -1)
                        lbNetworkMaps.Items[index] = model.Item;
                    break;
                case InputMode.Delete:
                    lbNetworkMaps.Items.Remove(model.Item);
                    break;
            }

            SelectNetworkMap();
        }

        private void SetSelection(NetworkMap model)
        {
            lbNetworkMaps.SelectedIndex = lbNetworkMaps.FindString(model.ToString());
            SelectNetworkMap();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var model = (NetworkMap)lbNetworkMaps.SelectedItem;
            if (!ViewModel.Edit(model.NetworkMapId)) return;

            RefreshView();
            SetSelection(model);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ViewModel.Add()) return;

            RefreshView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbNetworkMaps.SelectedItem == null)
                return;

            var confirmResult = MessageBox.Show(@"Are you sure you want to delete this item?", @"Confirm Delete!",
                                    MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                ViewModel.Delete(((NetworkMap)lbNetworkMaps.SelectedItem).NetworkMapId);
                RefreshView();
            }
        }

        private void RefreshView()
        {
            ClearView(true, true, true);
            ViewModel.ResetNetworkLinks();
            ViewModel.RenderCommon(ViewModel.GetModel(), false, true);
        }

        public void StatusUpdate(IStatusModel model)
        {
            if (IsDisposed)
                return;

            if (InvokeRequired)
            {
                Invoke(new Action<IStatusModel>(StatusUpdate), model);
                return;
            }

            StatusPanel.StatusUpdate(model);
        }

        public void UpdateView(object selectedModel)
        {
            throw new NotImplementedException();
        }

        private void pbCollaspeAll_Click(object sender, EventArgs e)
        {
            tvNodes.CollapseAll();
        }

        private void pbExpandAll_Click(object sender, EventArgs e)
        {
            tvNodes.ExpandAll();
        }

        private void lbNetworkMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnabledButtons();

            if (cbAutoMap.Checked)
                RenderNetworkFiles();
        }

        private void ckAutoMap_CheckedChanged(object sender, EventArgs e)
        {
            scAutoMap.Panel2Collapsed = !ckShowAutoMap.Checked;
        }

        private void btnMapNetwork_Click(object sender, EventArgs e)
        {
            SelectNetworkMap();
        }

        private void ClearView(bool clearFile, bool clearSourceProperties, bool clearCriteria)
        {
            try
            {
                lblDateTimeDifference.Text = "";
                if (clearFile) lbFiles.BeginUpdate();
                lbNetworkMessages.BeginUpdate();
                if (clearCriteria) dgvCriteria.SuspendLayout();
                dgvRecommendedMatches.SuspendLayout();
                if (clearSourceProperties) dgvSourceProperties.SuspendLayout();
                dgvTargetProperties.SuspendLayout();

                if (clearFile) lbFiles.Items.Clear();
                lbNetworkMessages.Items.Clear();
                if (clearCriteria) dgvCriteria.DataSource = null;
                dgvRecommendedMatches.DataSource = null;
                if (clearSourceProperties) dgvSourceProperties.DataSource = null;
                dgvTargetProperties.DataSource = null;

                tsslFileCount.Text = "";
                tsslNetworkMessagesCount.Text = "";
                tsslSourceValueCount.Text = "";
                tsslTargetValueCount.Text = "";
            }
            finally
            {
                if (clearFile) lbFiles.EndUpdate();
                lbNetworkMessages.EndUpdate();
                if (clearCriteria) dgvCriteria.ResumeLayout();
                dgvRecommendedMatches.ResumeLayout();
                if (clearSourceProperties) dgvSourceProperties.ResumeLayout();
                dgvTargetProperties.ResumeLayout();
            }
        }

        private void cbShowCriteria_CheckedChanged(object sender, EventArgs e)
        {
            scNetworkMapOverview.Panel2Collapsed = !cbShowCriteria.Checked;
        }

        private void lbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.WriteWarning("Network Mapping - lbFiles_SelectedIndexChanged",typeof(NetworkMappingWindow).FullName,
                      MethodBase.GetCurrentMethod().Name);

            var model = (NetworkMap)lbNetworkMaps.SelectedItem;
            if (model == null) return;

            var file = SelectedFile();
            if (file == null) return;

            RenderSourceValues(ViewModel.GetSourceMessage());
            ViewModel.BuildTargetNetworkMessages(RenderTargetNetworkMessages, file);
        }

        private FileData SelectedFile()
        {
            return lbFiles.SelectedItem == null ? null : ((NetworkMapFile)lbFiles.SelectedItem).File;
        }

        private void btnCollaspeUI_Click(object sender, EventArgs e)
        {
            scAutoMap.SplitterDistance = scAutoMap.Panel1MinSize;
            scNetworkMapOverview.SplitterDistance = scNetworkMapOverview.Panel1MinSize;
            scValues.SplitterDistance = scValues.Width / 2;
        }

        private void lbFiles_DoubleClick(object sender, EventArgs e)
        {
            var file = SelectedFile();
            if (file == null) return;


            ViewModel.OpenFile(file.Path);
        }

        private void lbNetworkMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            var file = SelectedFile();
            if (file == null) return;

            var message = SelectedNetworkMessage();

            if (message == null)
                return;

            PrepareCriteriaMatches();

            RenderRecommendedMatches(ViewModel.GetRecommendedMatches(new LineArgs { Path = file.Path, iLine = message.Source.iLine }));

            if (message.Source.FilePath != file.Path)
            {
                // The user is moving around in the UI too fast and events are chaining up and loading the wrong data, this will prevent that! - Sara
                Log.WriteWarning("UI Sync Issue, we are looking at the wrong file!", typeof(NetworkMappingWindow).FullName, MethodBase.GetCurrentMethod().Name);
                return;
            }

            RenderTargetValues(file.GetNetworkMessage(message.Source.iLine));
        }

        private NetworkMessageInfo SelectedNetworkMessage()
        {
            return lbNetworkMessages.SelectedItem == null ? null : ((NetworkMessageInfoModel)lbNetworkMessages.SelectedItem).Item;
        }

        #endregion From

        public NetworkMappingWindow()
        {
            InitializeComponent();

            UICommon.SetDoubleBuffered(dgvCriteria);
            UICommon.SetDoubleBuffered(dgvRecommendedMatches);
            UICommon.SetDoubleBuffered(dgvSourceProperties);
            UICommon.SetDoubleBuffered(dgvTargetProperties);

            ShowWarning();

            scAutoMap.Panel2Collapsed = !ckShowAutoMap.Checked;
            ckShowAutoMap.Checked = true;
            cbShowCriteria.Checked = true;
            ckbShowAll.Checked = false;
        }

        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void Render(object m)
        {
            ViewModel.RenderCommon(m, false, false);
        }

        private void ShowWarning()
        {
            scAutoMap.Visible = false;
            warningPanel.Visible = true;
            warningPanel.Dock = DockStyle.Fill;
        }

        private void HideWarning()
        {
            warningPanel.Visible = false;
            scAutoMap.Visible = true;
        }

        private void EnabledButtons()
        {
            btnAdd.Enabled = true;
            btnCopy.Enabled = btnEdit.Enabled = btnDelete.Enabled = lbNetworkMaps.Items.Count > 0 && lbNetworkMaps.SelectedIndex > -1;
        }

        private bool SkipRenderOnce { get; set; }

        public void RenderCallback(object m, bool lineChanged)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, bool>(RenderCallback), m, lineChanged);
                return;
            }

            if (MainViewModel.Current == null || m == null)
            {
                ShowWarning();
                return;
            }

            HideWarning();

            if (SkipRenderOnce)
            {
                SkipRenderOnce = false;
                return;
            }

            if (lineChanged && ckbShowAll.Checked)
            {
                Log.WriteTrace("Show All is True and LineFS Changed Triggered, thus skipping Render...", typeof(NetworkMappingWindow).FullName, MethodBase.GetCurrentMethod().Name);
                return;
            }

            StatusUpdate(StatusModel.Update(NetworkMapingViewModel.STATUS_TITLE));
            try
            {
                RenderNodes();

                var model = m as NetworkMapCacheData;
                if (model == null)
                    throw new InvalidCastException("NetworkMapCacheData");

                if (MainViewModel.Current != null)
                {
                    lbNetworkMaps.BeginUpdate();
                    try
                    {
                        lbNetworkMaps.Items.Clear();
                        foreach (var item in model.NetworkMaps.OrderBy(n => n.Priority))
                        {
                            if (!ckbShowAll.Checked)
                            {
                                if (
                                    !RegularExpression.HasMatch(MainViewModel.Current.CurrentLine,
                                        item.RegularExpression))
                                    continue;
                            }
                            lbNetworkMaps.Items.Add(item);
                        }
                    }
                    finally
                    {
                        lbNetworkMaps.EndUpdate();
                    }
                }
                SelectNetworkMap();
            }
            finally
            {
                StatusUpdate(StatusModel.Completed);
            }
            EnabledButtons();
        }

        private void RenderNodes()
        {
            if (MainViewModel.Current == null)
                return;

            try
            {
                tvNodes.BeginUpdate();
                tvNodes.Nodes.Clear();
                var query = from node in MainViewModel.Current.Model.File.Network.Nodes
                            orderby node.iLine
                            select node;

                var localNode = tvNodes.Nodes.Add("LOCAL");
                var remoteNode = tvNodes.Nodes.Add("REMOTE");

                foreach (var node in query)
                {
                    var foundNodeIP = false;
                    switch (node.Source)
                    {
                        case NodeSource.Local:
                            foreach (TreeNode nodeIP in localNode.Nodes)
                            {
                                if (nodeIP.Text == node.ToString())
                                {
                                    foundNodeIP = true;
                                    var foundNodePort = false;
                                    foreach (TreeNode nodePort in nodeIP.Nodes)
                                    {
                                        if (nodePort.ToString() == node.Port)
                                            foundNodePort = true;
                                    }
                                    if (!foundNodePort)
                                        nodeIP.Nodes.Add(new TreeNode(node.Port) { Tag = node });
                                }
                            }

                            if (!foundNodeIP)
                            {
                                var nodeIP = new TreeNode(node.ToString()) { Tag = node };
                                localNode.Nodes.Add(nodeIP);
                                nodeIP.Nodes.Add(new TreeNode(node.Port));
                            }
                            break;
                        case NodeSource.Remote:
                            foreach (TreeNode nodeIP in remoteNode.Nodes)
                            {
                                if (nodeIP.Text == node.ToString())
                                {
                                    foundNodeIP = true;
                                    var foundNodePort = false;
                                    foreach (TreeNode nodePort in nodeIP.Nodes)
                                    {
                                        if (nodePort.ToString() == node.Port)
                                            foundNodePort = true;
                                    }
                                    if (!foundNodePort)
                                        nodeIP.Nodes.Add(new TreeNode(node.Port) { Tag = node });
                                }
                            }

                            if (!foundNodeIP)
                            {
                                var nodeIP = new TreeNode(node.ToString()) { Tag = node };
                                remoteNode.Nodes.Add(nodeIP);
                                nodeIP.Nodes.Add(new TreeNode(node.Port));
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                }
                tvNodes.CollapseAll();
                foreach (TreeNode node in tvNodes.Nodes)
                {
                    node.Expand();
                }
            }
            finally
            {
                tvNodes.EndUpdate();
            }
        }

        private void SelectNetworkMap()
        {
            if (lbNetworkMaps.Items.Count > 0)
            {
                lbNetworkMaps.SelectedItem = lbNetworkMaps.Items[0];
            }
            else
                ClearView(true, true, true);
        }

        private void RenderNetworkFiles()
        {
            if (MainViewModel.Current == null)
                return;

            var model = (NetworkMap)lbNetworkMaps.SelectedItem;
            if (model == null)
            {
                ClearView(false, true, true);
                return;
            }

            var files = ViewModel.GetNetworkMapFiles();

            try
            {
                lbFiles.BeginUpdate();
                lbFiles.Items.Clear();

                foreach (var file in files)
                {
                    lbFiles.Items.Add(file);
                }
            }
            finally
            {
                lbFiles.EndUpdate();
            }

            if (lbFiles.Items.Count > 0)
                lbFiles.SelectedItem = lbFiles.Items[0];

            if (lbFiles.SelectedItem == null)
            {
                ClearView(true, true, true);
            }

            tsslFileCount.Text = string.Format("Count {0}", lbFiles.Items.Count);
        }

        /// <summary>
        /// When a File is selected, the Network Messages for that file are Rendered
        /// </summary>
        private void RenderTargetNetworkMessages()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(RenderTargetNetworkMessages));
                return;
            }

            var file = SelectedFile();
            if (file == null)
                return;

            ViewModel.GetCurrentLineNetworkMessages(RenderTargetNetworkMessagesCallback);
        }

        private void RenderTargetNetworkMessagesCallback(NetworkTargets messages)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<NetworkTargets>(RenderTargetNetworkMessagesCallback), messages);
                return;
            }

            try
            {
                lbNetworkMessages.BeginUpdate();
                lbNetworkMessages.Items.Clear();

                foreach (var message in messages.Targets.OrderBy(n => n.Item.Source.iLine))
                {
                    lbNetworkMessages.Items.Add(message);
                }

                if (lbNetworkMessages.Items.Count > 0)
                    lbNetworkMessages.SelectedIndex = 0;

                if (lbNetworkMessages.SelectedItem == null)
                {
                    PrepareCriteriaMatches();
                    ClearView(false, false, false);
                }
            }
            finally
            {
                lbNetworkMessages.EndUpdate();
            }

            tsslNetworkMessagesCount.Text = string.Format("Count {0}", messages.Targets.Count());
        }

        private void PrepareCriteriaMatches()
        {
            var networkMap = (NetworkMap)lbNetworkMaps.SelectedItem;
            if (networkMap == null) return;

            var file = SelectedFile();
            if (file == null) return;

            bool noNetworkMessages;
            int iLine;
            var path = file.Path;
            var message = SelectedNetworkMessage();

            // When null there are no Network Messages, so we are only looking at File Matches
            if (message == null)
            {
                noNetworkMessages = true;
                iLine = -1;
            }
            else
            {
                noNetworkMessages = false;
                iLine = message.Source.iLine;
                path = message.SourceItem.Path;
            }

            var model = ViewModel.GetCriteriaMatches(networkMap.NetworkMapId,
                new LineArgs
                {
                    Path = path,
                    iLine = iLine
                },
                noNetworkMessages);

            RenderCriteriaMatches(model);
        }

        private void RenderCriteriaMatches(CriteriaMatchModel model)
        {
            try
            {
                dgvCriteria.SuspendLayout();
                dgvCriteria.DataSource = model.Matches;
                dgvCriteria.Columns[0].ReadOnly = true;
                DataGridViewHelper.AutoSizeGrid(dgvCriteria);

                if (model.DateTimeDifference != null)
                {
                    lblDateTimeDifference.Text = model.DateTimeDifference.ToString();
                }

                foreach (
                    var row in
                        dgvCriteria.Rows.Cast<DataGridViewRow>()
                            .Where(row => Convert.ToBoolean(row.Cells["IsMatch"].Value) == false))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    if (row.Selected)
                        row.DefaultCellStyle.SelectionBackColor = Color.Magenta;
                }
            }
            finally
            {
                dgvCriteria.ResumeLayout();
            }
        }

        private void RenderRecommendedMatches(IEnumerable<CriteriaMatch> values)
        {
            try
            {
                dgvRecommendedMatches.SuspendLayout();
                dgvRecommendedMatches.DataSource = values;
                HideColumn(dgvRecommendedMatches, "Enabled");
                HideColumn(dgvRecommendedMatches, "IsMatch");
                HideColumn(dgvRecommendedMatches, "SourceType");
                HideColumn(dgvRecommendedMatches, "TargetType");
                HideColumn(dgvRecommendedMatches, "Operator");
            }
            finally
            {
                dgvRecommendedMatches.ResumeLayout();
            }
            tsslRecommendedMatchesCount.Text = string.Format("Count {0}", values.Count());
        }

        private void HideColumn(DataGridView dgv, string name)
        {
            var col = dgv.Columns[name];
            if (col == null)
                return;
            col.Visible = false;
        }

        private void RenderTargetValues(NetworkMessageInfo msg)
        {
            try
            {
                dgvTargetProperties.SuspendLayout();
                dgvTargetProperties.DataSource = msg.Source.Values;
                DataGridViewHelper.AutoSizeGrid(dgvTargetProperties);
            }
            finally
            {
                dgvTargetProperties.ResumeLayout();
            }
            tsslTargetValueCount.Text = string.Format("Count {0}", msg.Source.Values.Count());
        }

        private void RenderSourceValues(NetworkMessageInfo msg)
        {
            try
            {
                dgvSourceProperties.ResumeLayout();
                dgvSourceProperties.DataSource = msg.Source.Values;
                scValues.SplitterDistance = scValues.Width / 2;
                DataGridViewHelper.AutoSizeGrid(dgvSourceProperties);
            }
            finally
            {
                dgvSourceProperties.ResumeLayout();
            }

            tsslSourceValueCount.Text = string.Format("Count {0}", msg.Source.Values.Count());
        }

        private void ckbShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ViewModel == null)
                return;

            ViewModel.RenderForced();
        }

        private void tvNodes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13) return;

            e.Handled = true;
            if (tvNodes.SelectedNode == null)
                return;

            var networkNode = tvNodes.SelectedNode.Tag as Node;
            if (networkNode == null)
                return;

            SkipRenderOnce = true;
            ViewModel.GoToLine(networkNode.iLine);
        }

        private void lbFiles_MouseDown(object sender, MouseEventArgs e)
        {
            var selectedIndex = lbFiles.IndexFromPoint(e.X, e.Y);

            if (e.Button != MouseButtons.Right)
                return;

            var item = lbFiles.Items[selectedIndex] as NetworkMapFile;
            if (item == null)
                return;
            MessageBox.Show(string.Format("File Path: \"{0}\"", item.File.Path));
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (lbNetworkMaps.SelectedItem == null)
                return;

            StatusUpdate(StatusModel.StartStopWatch);
            StatusUpdate(StatusModel.Update("Copying", ""));
            try
            {
                ViewModel.Copy(((NetworkMap)lbNetworkMaps.SelectedItem).NetworkMapId);
                MessageBox.Show(@"Item has been copied");
                RefreshView();
            }
            finally
            {
                StatusUpdate(StatusModel.Completed);
            }
        }

        private void dgvCriteria_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToBoolean(dgvCriteria.Rows[e.RowIndex].Cells["IsMatch"].Value) == false && dgvCriteria.Rows[e.RowIndex].Selected)
                dgvCriteria.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Magenta;
        }

        private void NetworkMappingWindow_Shown(object sender, EventArgs e)
        {
            ViewModel.RenderDocument();
        }
    }
}
