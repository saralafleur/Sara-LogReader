using System.Windows.Forms;
using System.Reflection;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Drawing;
using Sara.Common.DateTimeNS;
using Sara.LogReader.Common;
using Sara.LogReader.Model.FileNS;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.Common;

namespace Sara.LogReader.WinForm.Controls
{
    public partial class SourceInfo : UserControl
    {
        public SourceInfo()
        {
            InitializeComponent();
            UICommon.ApplyLabelStyle(lblPath);
        }

        public void Render(FileData model)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<FileData>(Render), model);
                return;
            }

            var stopwatch = new Stopwatch(MethodBase.GetCurrentMethod().Name);
            if (model == null)
            {
                lblPath.Text = "";
                lblStart.Text = "";
                lblEnd.Text = "";
                lblType.Text = "";
                tvNameValue.Nodes.Clear();
                warningPanel.ShowWarning();
            }
            else
            {
                warningPanel.HideWarning();
                lblPath.Text = model.Path;
                lblStart.Text = model.Start.ToString(CultureInfo.InvariantCulture);
                lblEnd.Text = model.End.ToString(CultureInfo.InvariantCulture);
                lblType.Text = model.SourceType ?? Keywords.SOURCE_TYPE_UNKNOWN;
                tvNameValue.BeginUpdate();
                tvNameValue.SuspendLayout();
                try
                {
                    tvNameValue.Nodes.Clear();
                    foreach (var item in model.FileValues)
                    {
                        if (item.FileInfo == false)
                            continue;

                        var node = new TreeNode(item.ToString()) { Tag = item };
                        tvNameValue.Nodes.Add(node);
                    }

                    #region Group Nodes
                    var found = false;
                    do
                    {
                        TreeNode lastNode = null;
                        foreach (TreeNode node in tvNameValue.Nodes)
                        {
                            found = false;
                            if (lastNode != null && lastNode.Tag != null)
                            {
                                if (node.Tag is ValueBookMark currentItem &&
                                    currentItem.Name == (lastNode.Tag as ValueBookMark).Name)
                                {
                                    string _name = currentItem.Name;
                                    lastNode.Text = _name;
                                    var n = new TreeNode(lastNode.Tag.ToString()) { Tag = lastNode.Tag };
                                    lastNode.Nodes.Add(n);
                                    lastNode.Tag = null;

                                    for (int i = tvNameValue.Nodes.Count - 1; i >= 0; i--)
                                    {
                                        if (tvNameValue.Nodes[i].Tag != null &&
                                            (tvNameValue.Nodes[i].Tag as ValueBookMark).Name == _name)
                                        {
                                            var moveNode = tvNameValue.Nodes[i];
                                            tvNameValue.Nodes.Remove(moveNode);
                                            lastNode.Nodes.Add(moveNode);
                                        }
                                    }
                                    lastNode.Text = $"{lastNode.Text} ({lastNode.Nodes.Count})";
                                    found = true;
                                    break;
                                }
                            }
                            lastNode = node;
                        }
                    } while (found);
                    #endregion
                }
                finally
                {
                    tvNameValue.ResumeLayout();
                    tvNameValue.EndUpdate();
                }
                AddRangeDocumentMapDuration(model.DocumentMapDurations);
            }
            stopwatch.Stop();
        }

        private void AddRangeDocumentMapDuration(List<ValueBookMark> items)
        {
            if (items == null)
                return;

            if (InvokeRequired)
            {
                Invoke(new Action<List<ValueBookMark>>(AddRangeDocumentMapDuration), items);
                return;
            }

            tvNameValue.BeginUpdate();
            tvNameValue.SuspendLayout();
            try
            {
                for (var i = tvNameValue.Nodes.Count - 1; i >= 0; i--)
                {
                    var value = tvNameValue.Nodes[i].Tag as ValueBookMark;
                    if (value == null) continue;

                    if (value.DocumentMapDuration)
                        tvNameValue.Nodes.RemoveAt(i);
                }

                foreach (var item in items)
                {
                    tvNameValue.Nodes.Add(new TreeNode(item.Name) { Tag = item, ForeColor = Color.Red });
                }
            }
            finally
            {
                tvNameValue.ResumeLayout();
                tvNameValue.EndUpdate();
            }
        }

        /// <summary>
        /// This eventPattern occurs when a value is double clicked
        /// </summary>
        public event Action<ValueBookMark> GoTo;

        private void tvNameValue_DoubleClick(object sender, EventArgs e)
        {
            if (tvNameValue.SelectedNode == null) return;

            var item = tvNameValue.SelectedNode.Tag as ValueBookMark;
            if (item == null) return;

            GoTo?.Invoke(item);
        }
        public void ShowWarning()
        {
            warningPanel.ShowWarning();
        }
        public void HideWarning()
        {
            warningPanel.HideWarning();
        }

        private bool _shown = false;
        private void SourceInfo_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                if (_shown) return;
                _shown = true;

                if (!DesignMode)
                    ColorService.Setup(this);
            }
        }

        private void tvNameValue_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (ckbNavigation.Checked)
            {
                var item = tvNameValue.SelectedNode.Tag as ValueBookMark;
                if (item == null) return;

                GoTo?.Invoke(item);
            }
        }
    }
}
