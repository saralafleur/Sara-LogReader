using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Sara.Common.DateTimeNS;
using Sara.Common.Extension;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.SockDrawer;
using Sara.LogReader.Service;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.WinForm.UI_Common;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.Common;
using Sara.WinForm.ControlsNS;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.SockDrawer
{
    public partial class SockDrawerWindow : ToolWindow, IViewDock<SockDrawerModel, object>
    {
        public SockDrawerViewModel ViewModel { get; set; }
        public SockDrawerModel Model { get; set; }
        internal bool SkipTreeViewRender
        {
            get
            {
                return _skipTreeViewRender;
            }
            set
            {
                _skipTreeViewRender = value;
                // When we set 'SkipTreeView' to false, we need to invalidate the TreeView as well so it will render. -Sara
                if (!value)
                    _treeViewInvalidated = true;
            }
        }
        private bool _skipTreeViewRender { get; set; }
        /// <summary>
        /// Only Render the TreeView when it has been invalidated
        /// The design here is that we only want to Render the TreeView Once, unless the data changes,
        /// then it will be invalidated and Rendered. - Sara
        /// </summary>
        private bool _treeViewInvalidated;

        public SockDrawerWindow()
        {
            _treeViewInvalidated = true;
            InitializeComponent();
            
        }
        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void Render(SockDrawerModel model)
        {
            OutputService.Log("Rendering SockDrawer.");
            StatusPanel.StatusUpdate(StatusModel.StartStopWatch);
            StatusPanel.SP_FullScreen = false;
            StatusPanel.SP_DisplayRemainingTime = false;
            StatusPanel.StatusUpdate(StatusModel.Update("Loading"));

            Model = model;

            if (!_treeViewInvalidated)
                return;

            _treeViewInvalidated = false;
            RenderTreeView();
        }
        private void RenderTreeView()
        {
            if (SkipTreeViewRender)
            {
                SkipTreeViewRender = false; // reset
                return;
            }

            ViewModel.BuildFileTreeView(RenderTreeViewCallback, ckbShowBuildInfo.Checked);
        }
        private void RenderTreeViewCallback(TreeNodeCollection nodes)
        {
            if (IsDisposed)
                return;

            if (InvokeRequired)
            {
                Invoke(new Action<TreeNodeCollection>(RenderTreeViewCallback), nodes);
                return;
            }

            var stopWatch = new Stopwatch(MethodBase.GetCurrentMethod().Name);
            try
            {
                tvFiles.BeginUpdate();
                try
                {
                    tvFiles.Nodes.Clear();
                    TreeViewService.AddRange(nodes, tvFiles.Nodes);
                    tsslTotalFiles.Text = string.Format("Total Files {0} ", Model.Files.Count);
                }
                finally
                {
                    tvFiles.EndUpdate();
                    StatusPanel.StatusUpdate(StatusModel.Completed);
                    StatusPanel.SP_DisplayRemainingTime = true;
                }
            }
            finally
            {
                stopWatch.Stop();
                StatusUpdate(StatusModel.Completed);
                StatusPanel.SP_DisplayRemainingTime = true;
                StatusPanel.SP_FullScreen = true;
            }
        }

        internal void SetFocus(string path)
        {
            var node = FindNodeByPath(tvFiles.Nodes[0], path);

            if (node == null) return;

            tvFiles.SelectedNode = node;
        }

        private TreeNode FindNodeByPath(TreeNode node, string path)
        {
            var model = node.Tag as TreeViewFile;
            if (model == null) return null;

            if (!model.Favorite && model.FilePath == path)
                return node;

            foreach (TreeNode item in node.Nodes)
            {
                var foundNode = FindNodeByPath(item, path);

                if (foundNode != null)
                    return foundNode;
            }

            return null;
        }

        public void UpdateCompletedCallback(TimeSpan duration)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<TimeSpan>(UpdateCompletedCallback), duration);
                return;
            }
            StatusUpdate(StatusModel.ClearPersistentDetail);
            MessageBox.Show(string.Format("Update Complete.  {0}", duration.ToReadableString()), "Complete");

            EnableButtons(true);
            StatusUpdate(StatusModel.Completed);
            ViewModel.RenderDocument();
        }
        private void UpdateAll(bool forceClear)
        {
            EnableButtons(false);
            _treeViewInvalidated = true;
            try
            {
                ViewModel.Update(forceClear);
            }
            catch (Exception ex)
            {
                ErrorPanel.ShowError(this, ex.Message, CloseCallback);
            }
        }
        private void CloseCallback()
        {
            EnableButtons(true);
        }
        private void UpdateSelectedGroup(bool forceClear)
        {
            if (tvFiles.SelectedNode == null)
                return;

            EnableButtons(false);
            _treeViewInvalidated = true;
            try
            {
                ViewModel.UpdateSelectedGroup(tvFiles.SelectedNode, forceClear);
            }
            catch (Exception ex)
            {
                ErrorPanel.ShowError(this, ex.Message, CloseCallback);
            }
        }
        private void UpdateActive(bool forceClear)
        {
            EnableButtons(false);
            _treeViewInvalidated = true;
            try
            {
                ViewModel.UpdateActive(forceClear);
            }
            catch (Exception ex)
            {
                ErrorPanel.ShowError(this, ex.Message, CloseCallback);
            }
        }
        private void EnableButtons(bool value)
        {
            btnUpdateAllForced.Enabled = value;
            btnUpdateActiveForced.Enabled = value;
            btnUpdateSelectedGroupForced.Enabled = value;
            btnUpdateAllNew.Enabled = value;
            btnUpdateActiveNew.Enabled = value;
            btnUpdateSelectedGroupNew.Enabled = value;
            btnSettings.Enabled = value;
        }

        public void StatusUpdate(IStatusModel model)
        {
            StatusPanel.StatusUpdate(model);
        }
        public void UpdateView(object selectedModel)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///  Removes the previous Range and adds a new range of values
        /// </summary>
        private void tvNameValue_DoubleClick(object sender, EventArgs e)
        {
        }
        private void GoToDocument()
        {
            ViewModel.GoToDocument(tvFiles.SelectedNode, GoToDocumentCallback);
        }
        private void GoToDocumentCallback()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(GoToDocumentCallback));
                return;
            }

            tvFiles.Focus();
        }
        private void tvFiles_DoubleClick(object sender, EventArgs e)
        {
            GoToDocument();
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            ViewModel.Settings();
        }
        /// <summary>
        /// Display the Path of the selected file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var model = e.Node.Tag as TreeViewFile;
            if (model == null) return;
            if (model.FilePath == null) return;
            tsslPath.Text = model.FilePath;
        }
        private void tvFiles_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                GoToDocument();
                e.Handled = true;
            }
        }
        private void btnToggleDetail_Click(object sender, EventArgs e)
        {
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                tvFiles.BeginUpdate();
                tvFiles.ExpandAll();
                tvFiles.Nodes[0].EnsureVisible();
            }
            finally
            {
                tvFiles.EndUpdate();
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                tvFiles.BeginUpdate();
                tvFiles.CollapseAll();
                tvFiles.Nodes[0].EnsureVisible();
            }
            finally
            {
                tvFiles.EndUpdate();
            }
        }
        private void updateSockDrawerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateAll(true);
        }
        private void btnUpdateAllNew_Click(object sender, EventArgs e)
        {
            UpdateAll(false);
        }
        private void btnUpdateSelectedGroupNew_Click(object sender, EventArgs e)
        {
            UpdateSelectedGroup(false);
        }
        private void btnUpdateActiveNew_Click(object sender, EventArgs e)
        {
            UpdateActive(false);
        }
        private void btnUpdateSelectedGroupForced_Click(object sender, EventArgs e)
        {
            UpdateSelectedGroup(true);
        }
        private void btnUpdateActiveForced_Click(object sender, EventArgs e)
        {
            UpdateActive(true);
        }
        private void removeDuplicateFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatusUpdate(StatusModel.StartStopWatch);
            StatusUpdate(StatusModel.Update("Finding Duplicate Files"));
            ThreadPool.QueueUserWorkItem(delegate
            {
                var moved = ViewModel.RemoveDuplicateFiles();
                MessageBox.Show(string.Format("{0} duplicate files moved to Duplicate Folder.", moved));
                StatusUpdate(StatusModel.Completed);
            });
        }
        private void tvFiles_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (tvFiles.SelectedNode == null) return;

            // It seemed to make sense if there is only one child under the parent to simple open the child up. - Sara
            if (tvFiles.SelectedNode.Nodes.Count == 1)
                tvFiles.SelectedNode.Nodes[0].Expand();
        }

        private void ckbStatics_CheckedChanged(object sender, EventArgs e)
        {
            RenderTreeView();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tvFiles.SelectedNode == null) return;

            var model = tvFiles.SelectedNode.Tag as TreeViewFile;
            if (model == null) return;
            removeFavoriteToolStripMenuItem.Enabled = model.Favorite;
            AddFavoritetoolStripMenuItem2.Enabled = !model.Favorite && !string.IsNullOrEmpty(model.FilePath);
        }

        private void removeFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var model = tvFiles.SelectedNode.Tag as TreeViewFile;

            if (model == null) return;

            if (model.Favorite)
            {
                RemoveFavorite(tvFiles.SelectedNode);
            }

            SkipTreeViewRender = false;
            RenderTreeView();
        }

        private void RemoveFavorite(TreeNode node)
        {
            var model = node.Tag as TreeViewFile;
            if (model == null) return;

            if (!string.IsNullOrEmpty(model.FilePath))
            {
                ViewModel.RemoveFavoriteFile(model.FilePath, model.FavoriteGroup);
            }

            foreach (TreeNode item in node.Nodes)
            {
                RemoveFavorite(item);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var model = tvFiles.SelectedNode.Tag as TreeViewFile;
            if (model == null) return;


            ViewModel.AddFavorite(model.FilePath);
        }

        private void SockDrawerWindow_Paint(object sender, PaintEventArgs e)
        {
            UICommon.AddWindowBorder(e, this);
        }
    }
}