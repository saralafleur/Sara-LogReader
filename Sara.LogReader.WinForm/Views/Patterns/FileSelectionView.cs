using System;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using Sara.Common.DateTimeNS;
using Sara.LogReader.Model.PatternNS;
using Sara.LogReader.Model.SockDrawer;
using Sara.LogReader.WinForm.UI_Common;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.CRUD;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Patterns
{
    public partial class FileSelectionView : Form
    {
        private FileSelectionModel Model { get; set; }
        public FileSelectionView()
        {
            InitializeComponent();
            AcceptButton = btnOk;
            CancelButton = btnCancel;

            CRUDUIService.RenderEnumList<FileTreeViewGroupEnum>(cbGrouping, FileTreeViewGroupEnum.Customer);

            ColorService.Setup(this);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Model.Continue = false;
            Close();
            ThreadPool.QueueUserWorkItem(delegate { Model.Callback(null); });
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            Model.Continue = true;
            Model.Files.Clear();
            FindSelectedFiles(tvFiles.Nodes);

            Close();

            ThreadPool.QueueUserWorkItem(delegate { Model.Callback(Model); });
        }
        private void FindSelectedFiles(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                var file = node.Tag as TreeViewFile;
                if (file == null)
                    continue;

                if (node.Nodes.Count > 0)
                    FindSelectedFiles(node.Nodes);

                if (!node.Checked || file.FilePath == null) continue;

                Model.Files.Add(file.FilePath);
            }
        }
        public void Render(FileSelectionModel model)
        {
            var sw = new Stopwatch("Rendering File Selection for Patterns");
            Model = model;
            RenderFiles();
            sw.Stop(0);
        }
        private void RenderFiles()
        {
            TreeViewService.BuildTreeView(new BuildFileTreeViewArgs() { Callback = RenderFilesCallback,  Grouping = CRUDUIService.GetEnumValue<FileTreeViewGroupEnum>(cbGrouping) });
        }

        private void RenderFilesCallback(TreeNodeCollection nodes)
        {
            if (IsDisposed)
                return;

            if (InvokeRequired)
            {
                Invoke(new Action<TreeNodeCollection>(RenderFilesCallback), nodes);
                return;
            }

            var stopWatch = new Stopwatch(MethodBase.GetCurrentMethod().Name);
            try
            {
                tvFiles.BeginUpdate();
                tvFiles.SuspendLayout();
                _skipAfterCheck = true;
                try
                {
                    tvFiles.Nodes.Clear();
                    TreeViewService.AddRange(nodes, tvFiles.Nodes);
                    foreach (TreeNode node in tvFiles.Nodes)
                    {
                        CheckSelected(node);
                        ExpandSelected(node);
                    }
                }
                finally
                {
                    _skipAfterCheck = false;
                    tvFiles.ResumeLayout();
                    tvFiles.EndUpdate();
                }
            }
            finally
            {
                stopWatch.Stop();
            }
        }

        private bool ExpandSelected(TreeNode node)
        {
            var expandAny = false;
            foreach (TreeNode childNode in node.Nodes)
            {
                if (ExpandSelected(childNode))
                    expandAny = true;
            }

            if (node.Parent == null)
                return false;

            if (expandAny || (!node.Parent.Checked && node.Checked))
            {
                node.Parent.Expand();
                return true;
            }
            else
                return false;
        }

        private bool CheckSelected(TreeNode node)
        {
            var all = true;
            foreach (TreeNode childNode in node.Nodes)
            {
                if (!CheckSelected(childNode))
                    all = false;
            }

            var tag = node.Tag as TreeViewFile;
            if (tag == null || tag.FilePath == null)
            {
                node.Checked = all;
                return all;
            }

            if (Model.SelectedFiles.Contains(tag.FilePath))
            {
                node.Checked = true;
                return true;
            }
            else
                return false;
        }

        private bool _skipAfterCheck = false;
        private void tvFiles_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_skipAfterCheck)
                return;

            if (e == null) return;
            if (e.Node == null) return;

            // Check all childern
            foreach (TreeNode node in e.Node.Nodes)
            {
                if (node.Checked == e.Node.Checked)
                    continue;
                node.Checked = e.Node.Checked;
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            tvFiles.ExpandAll();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            tvFiles.CollapseAll();
        }
        private void ckbFilesAllOrNone_CheckedChanged(object sender, EventArgs e)
        {
            FilesCheckAllOrNone(tvFiles.Nodes, ckbFilesAllOrNone.Checked);
        }
        private void FilesCheckAllOrNone(TreeNodeCollection nodes, bool @checked)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = @checked;
                if (node.Nodes.Count > 0)
                    FilesCheckAllOrNone(node.Nodes, @checked);
            }
        }

        private void cbGrouping_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Visible)
                return;
            try
            {
                StatusPanel.StatusUpdate(StatusModel.Update("Updating grouping..."));
                RenderFiles();
            }
            finally
            {
                StatusPanel.StatusUpdate(StatusModel.Completed);
            }
            
        }
    }
}
