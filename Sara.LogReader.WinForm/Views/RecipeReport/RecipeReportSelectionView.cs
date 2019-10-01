using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Sara.Common.DateTimeNS;
using Sara.LogReader.Model.PatternNS;
using Sara.LogReader.Model.RecipeReport;
using Sara.LogReader.Model.SockDrawer;
using Sara.LogReader.Service;
using Sara.LogReader.WinForm.UI_Common;
using Sara.LogReader.WinForm.ViewModel;

namespace Sara.LogReader.WinForm.Views.RecipeReport
{
    public partial class NetworkReportSelectionView : Form
    {
        private RecipeReportSelectionModel Model { get; set; }
        public RecipeReportViewModel ViewModel { get; set; }

        public NetworkReportSelectionView()
        {
            InitializeComponent();
            AcceptButton = btnRun;
            CancelButton = btnCancel;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            foreach (RecipeSelection recipe in cklbRecipes.Items)
            {
                recipe.Selected = false;
            }

            foreach (RecipeSelection recipe in cklbRecipes.CheckedItems)
            {
                recipe.Selected = true;
            }

            Model.ReportName = tbReportName.Text;
            Model.Files.Clear();
            FindSelectedFiles(tvFiles.Nodes);

            Close();
            Model.Callback(Model);
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

        public void Render(RecipeReportSelectionModel model)
        {
            tbReportName.Text = ReportService.GenerateReportName();
            Model = model;
            cklbRecipes.Items.Clear();
            foreach (var recipe in model.Recipes.OrderBy(n => n.Name))
            {
                cklbRecipes.Items.Add(recipe);
            }
            RenderFiles();
        }

        private void RenderFiles()
        {
            TreeViewService.BuildTreeView(new BuildFileTreeViewArgs() { Callback = RenderFilesCallback });
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
                try
                {
                    tvFiles.Nodes.Clear();
                    TreeViewService.AddRange(nodes, tvFiles.Nodes);
                }
                finally
                {
                    tvFiles.EndUpdate();
                }
            }
            finally
            {
                stopWatch.Stop();
            }
        }
        private void tvFiles_AfterCheck(object sender, TreeViewEventArgs e)
        {
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

        private void ckRecipesAllorNone_CheckedChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < cklbRecipes.Items.Count; i++)
            {
                cklbRecipes.SetItemChecked(i, ckbRecipesAllorNone.Checked);
            }
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

        private void NetworkReportSelectionView_Shown(object sender, EventArgs e)
        {
            tbReportName.Focus();
        }
    }
}
