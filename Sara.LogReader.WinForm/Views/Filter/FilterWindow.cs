using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Sara.LogReader.Model.Filter;
using Sara.LogReader.Model.LogReaderNS;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Filter
{
    public partial class FilterWindow : ToolWindow, IViewDock<FilterModel, object>
    {
// ReSharper disable UnusedAutoPropertyAccessor.Local
        internal FilterViewModel ViewModel { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Local

        public FilterWindow()
        {
            InitializeComponent();
            ShowWarning();
        }

        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void Render(FilterModel model)
        {
            // Note: Invoke is handled by ViewModelBase - Sara
            try
            {
                StatusPanel.StatusUpdate(StatusModel.Update("Loading..."));
                if (model == null)
                {
                    ShowWarning();

                    return;
                }

                HideWarning();

                tvCategories.Nodes.Clear();
                foreach (var item in model.Categories.OrderBy(n => n.Name))
                {
                    var node = tvCategories.Nodes.Add(item.Name);
                    node.Checked = item.Checked;
                    node.Tag = item;
                }
            }
            finally
            {
                StatusPanel.StatusUpdate(StatusModel.Completed);                
            }
        }

        private void HideWarning()
        {
            warningPanel.Visible = false;
            panel7.Visible = true;
        }

        private void ShowWarning()
        {
            panel7.Visible = false;
            warningPanel.Visible = true;
            warningPanel.Dock = DockStyle.Fill;
        }

        public void StatusUpdate(IStatusModel model)
        {
            StatusPanel.StatusUpdate(model);
        }

        public void UpdateView(object selectedModel)
        {
            throw new NotImplementedException();
        }

        private void ApplyFilter()
        {
            foreach (TreeNode node in tvCategories.Nodes)
            {
                node.ForeColor = Color.Black;
            }
            ViewModel.ApplyFilter();
        }

        private void CheckAllCategories(bool value)
        {
            foreach (TreeNode node in tvCategories.Nodes)
            {
                node.Checked = value;
            }
        }
        private bool _ignoreAfter;
        private void AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e == null) return;
            if (e.Node == null) return;

            lock (this)
            {
                if (!_ignoreAfter)
                {
                    _ignoreAfter = true;
                    try
                    {
                        // Check all childern
                        foreach (TreeNode node in e.Node.Nodes)
                        {
                            if (node.Checked == e.Node.Checked)
                                continue;
                            node.Checked = e.Node.Checked;
                        }

                        if (e.Node.Parent != null && !e.Node.Checked)
                        {
                            e.Node.Parent.Checked = false;
                        }
                    }
                    finally
                    {
                        _ignoreAfter = false;
                    }
                }
            }

            if (e.Node.Tag == null) return;
            var checkedItem = (CheckedItem) e.Node.Tag;

            checkedItem.Checked = e.Node.Checked;

            e.Node.ForeColor = checkedItem.Changed ? Color.Red : ColorService.ColorScheme.Current.ButtonForeColor;
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckAllCategories(ckbCategoriesAllorNone.Checked);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}