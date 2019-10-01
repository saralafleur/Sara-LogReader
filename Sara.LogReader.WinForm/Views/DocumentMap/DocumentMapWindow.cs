using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Sara.Common.DateTimeNS;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.Common;
using Sara.LogReader.Model.DocumentMap;
using Sara.LogReader.Service;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm;
using Sara.WinForm.ColorScheme;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;
using Sara.WinForm.Common;

namespace Sara.LogReader.WinForm.Views.DocumentMap
{
    public partial class DocumentMapWindow : ToolWindow, IViewDock<List<DocumentEntry>, object>, IColorSchemeControl
    {
        #region Properties
        // ReSharper disable NotAccessedField.Local
        internal DocumentMapViewModel ViewModel;
        // ReSharper restore NotAccessedField.Local
        public bool StartupReady { get { return MainViewModel.StartupComplete; } }
        private bool _isRendering;
        public bool ShowTimeGap { get { return ckbTimeGap.Checked; } }
        public bool ShowNetwork { get { return ckbNetwork.Checked; } }
        public bool ShowValues { get { return ckbValue.Checked; } }
        public bool ShowEvents { get { return ckbEvents.Checked; } }
        public bool Filter { get { return ckbFilter.Checked; } }
        public bool ShowGap { get { return ckbMapGap.Checked; } }
        public string GapDuration { get { return ntbGapDuration.Text; } }
        public bool ApplyOverlay { get { return ckbOverlay.Checked; } }
        public TreeNode Root { get { return tvMapEntries.Nodes[0]; } }
        #endregion

        #region Setup
        public DocumentMapWindow()
        {
            InitializeComponent();

            UICommon.SetDoubleBuffered(this);

            ShowWarning();

            ntbGapDuration.Text = DefaultValue.DOCUMENT_MAP_GAP_MIN_DURATION.ToString(CultureInfo.InvariantCulture);

            tvMapEntries.Font = new Font(FontFamily.GenericMonospace, tvMapEntries.Font.Size);
            SplitView.Panel2Collapsed = true;
            tvMapEntriesSplit.Font = new Font(FontFamily.GenericMonospace, tvMapEntries.Font.Size);
            MainViewModel.CurrentLineChangedEvent += CurrentLineChanged;
            ColorService.Setup(this);
        }
        ~DocumentMapWindow()
        {
            MainViewModel.CurrentLineChangedEvent -= CurrentLineChanged;
        }
        #endregion

        #region Render
        public void Render(List<DocumentEntry> model)
        {
            var r = new ReleaseUI("DocumentMap.Render");
            PerformanceService.StartEvent($"{PerformanceService.CONST_RenderDocumentMap}{ViewModel.LastPath}");
            // Note: Invoke is handled through ViewModelbase - Sara
            if (_isRendering) return;

            if (!MainViewModel.StartupComplete)
            {
                StatusPanel.StatusUpdate(StatusModel.Completed);
                return;
            }

            if (model == null)
            {
                ShowWarning();
                StatusPanel.StatusUpdate(StatusModel.Completed);
                return;
            }

            _isRendering = true;
            wpOpen.Visible = false;
            panel3.Visible = true;

            StatusPanel.StatusUpdate(StatusModel.StartStopWatch);
            StatusPanel.StatusUpdate(StatusModel.Update("Loading", ""));

            r.Stop();

            ThreadPool.QueueUserWorkItem(delegate
            {
                var sw = new Stopwatch("DocumentMap.Render.BuildTreeView");
                Thread.CurrentThread.Name = "Building.DocumentMap.TreeView";
                ViewModel.BuildTreeView(model, RenderCallback);
                sw.Stop(0);
            });
        }
        private const int @break = 50;
        private void RenderCallback(TreeNodeCollection nodes)
        {
            var r = new ReleaseUI("DocumentMap.RenderCallback", true);
            if (IsDisposed)
                return;

            if (InvokeRequired)
            {
                Invoke(new Action<TreeNodeCollection>(RenderCallback), nodes);
                return;
            }

            #region Clear
            // Note: We are on the MainUI Thread.  We want to limit our activity to 50ms or less before we call Application.DoEvents (r.DoEvents)
            r.DoEvents("starting");
            var n = 0;
            for (int i = tvMapEntries.Nodes.Count - 1; i >= 0; i--)
            {
                n++;
                tvMapEntries.Nodes.RemoveAt(i);
                if (n % @break == 0)
                    r.DoEvents("RemoveAt break");
            }
            r.DoEvents("RemoveAt done");
            #endregion

            #region Add
            n = 0;
            foreach (TreeNode node in nodes)
            {
                n++;
                tvMapEntries.Nodes.Add(node);
                if (n % @break == 0)
                    r.DoEvents("Node.Add break");
            }
            r.DoEvents("Node.Add done");
            #endregion

            tvMapEntriesSplit.Clone(tvMapEntries);
            _isRendering = false;
            r.DoEvents("Clone");
            StatusPanel.StatusUpdate(StatusModel.Completed);
            ViewModel.RenderComplete();
            PerformanceService.StopEvent($"{PerformanceService.CONST_RenderDocumentMap}{ViewModel.LastPath}");
            r.Stop();
        }

        public void UpdateView(object selectedModel)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GoTo
        private void GoToCurrentLine()
        {
            // Do not GoToLine if we are Rendering - Sara
            if (_isRendering)
                return;
            var item = (DocumentEntry)tvMapEntries.SelectedNode.Tag;
            if (item == null)
                return;
            ViewModel.GoToLine(item.iLine);
        }
        /// <summary>
        /// Prevents DocumentMap from calling back to the Document Window with a GoToiLine - Sara
        /// </summary>
        bool _respondingToGoToiLine = false;
        private bool GoToiLine(IEnumerable n, int iLine)
        {

            if (InvokeRequired)
            {
                return (bool)Invoke(
                    new Func<bool>(() => GoToiLine(n, iLine))
                );
            }
            _respondingToGoToiLine = true;
            try
            {
                // Check if the current item is already on the iLine being requested.
                // Sometimes there can be multiple document map entries that use the same iLine and to avoid a flicker or 
                // having your down cursor go back up one, this code stops that. - Sara
                if (tvMapEntries.SelectedNode != null)
                {
                    var currentItem = (DocumentEntry)tvMapEntries.SelectedNode.Tag;
                    if (currentItem != null)
                    {
                        if (currentItem.iLine == iLine)
                            return true;
                    }
                }

                return GoToiLineRecursive(n, iLine);
            }
            finally
            {
                _respondingToGoToiLine = false;
            }
        }
        private bool GoToiLineRecursive(IEnumerable n, int iLine)
        {
            foreach (TreeNode node in n)
            {
                var item = (DocumentEntry)node.Tag;
                if (item == null)
                    continue;

                if (item.iLine == iLine && (item.Type != DocumentMapType.TimeGAPStart && item.Type != DocumentMapType.TimeGAPEnd))
                {
                    tvMapEntries.SelectedNode = node;
                    node.EnsureVisible();
                    return true;
                }

                if (node.Nodes.Count > 0)
                    if (GoToiLine(node.Nodes, iLine))
                        return true;
            }
            return false;
        }
        #endregion

        #region Helpers
        private void ShowWarning()
        {
            panel3.Visible = false;
            wpOpen.Visible = true;
            wpOpen.Dock = DockStyle.Fill;
        }
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }
        #endregion

        #region Events
        private void tvMapEntries_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_respondingToGoToiLine) return;

            GoToCurrentLine();
        }
        private void tvMapEntries_DoubleClick(object sender, EventArgs e)
        {
            if (tvMapEntries.SelectedNode == null)
                return;

            var model = tvMapEntries.SelectedNode.Tag as DocumentEntry;
            if (model == null)
                return;

            ViewModel.GoToEntry(model);
        }
        private void ckbHideTimeGap_Click(object sender, EventArgs e)
        {
            ViewModel.RenderDocument();
        }
        private void ckbHideNetwork_Click(object sender, EventArgs e)
        {
            ViewModel.RenderDocument();
        }
        private void ckbHideValue_Click(object sender, EventArgs e)
        {
            ViewModel.RenderDocument();
        }
        private void ckbHideEvents_Click(object sender, EventArgs e)
        {
            ViewModel.RenderDocument();
        }
        public void StatusUpdate(IStatusModel model)
        {
            StatusPanel.StatusUpdate(model);
        }
        private void tvMapEntries_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            var model = e.Node.Tag as DocumentEntry;
            if (model == null)
            {
                e.DrawDefault = true;
                return;
            }

            Brush color;

            switch (model.Type)
            {
                case DocumentMapType.TimeGAPStart:
                case DocumentMapType.TimeGAPEnd:
                    color = Brushes.White;
                    break;
                case DocumentMapType.Network:
                    color = Brushes.Yellow;
                    break;
                case DocumentMapType.Value:
                    color = Brushes.Red;
                    break;
                case DocumentMapType.Event:
                    color = Brushes.Blue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            int width = 9;
            e.Graphics.FillRectangle(Brushes.Black, Rectangle.Inflate(new Rectangle(0, e.Node.Bounds.Y, width, e.Node.Bounds.Height), -2, -2));
            e.Graphics.FillRectangle(color, Rectangle.Inflate(new Rectangle(0, e.Node.Bounds.Y, width, e.Node.Bounds.Height), -3, -3));

            if (model.HighlightColor != "" && model.HighlightColor != "Transparent")
            {
                width = Width;
                e.Graphics.FillRectangle(Brushes.Black, Rectangle.Inflate(new Rectangle(100, e.Node.Bounds.Y, width, e.Node.Bounds.Height), -2, -2));
                e.Graphics.FillRectangle(new SolidBrush(Color.FromName(model.HighlightColor)), Rectangle.Inflate(new Rectangle(100, e.Node.Bounds.Y, width, e.Node.Bounds.Height), -3, -3));
            }


            e.DrawDefault = true;
        }
        private void ckbPatterns_Click(object sender, EventArgs e)
        {
            ViewModel.RenderDocument();
        }
        private void cbCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            ckbNetwork.Checked = ckbAll.Checked;
            ckbEvents.Checked = ckbAll.Checked;
            ckbPatterns.Checked = ckbAll.Checked;
            ckbTimeGap.Checked = ckbAll.Checked;
            ckbValue.Checked = ckbAll.Checked;
            ViewModel.RenderDocument();
        }
        private void ckbSplitView_CheckedChanged(object sender, EventArgs e)
        {
            SplitView.Panel2Collapsed = !ckbSplitView.Checked;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                tvMapEntries.BeginUpdate();
                tvMapEntries.CollapseAll();
                tvMapEntries.SelectedNode = tvMapEntries.Nodes[0];
                tvMapEntries.SelectedNode.EnsureVisible();
            }
            finally
            {
                tvMapEntries.EndUpdate();
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                tvMapEntries.SuspendLayout();
                tvMapEntries.BeginUpdate();
                tvMapEntries.ExpandAll();
                tvMapEntries.SelectedNode = tvMapEntries.Nodes[0];
                tvMapEntries.SelectedNode.EnsureVisible();
            }
            finally
            {
                tvMapEntries.EndUpdate();
                tvMapEntries.ResumeLayout();
            }
        }
        private void cbFilter_CheckedChanged(object sender, EventArgs e)
        {
            ViewModel.RenderDocument();
        }
        private void tvMapEntries_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13) return;

            e.Handled = true;

            GoToCurrentLine();
            ViewModel.SetFocusOnDocument();
        }
        private void tvMapEntries_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (e.Shift && e.Alt)
                    {
                        var tv = sender as TreeView;
                        if (tv != null) tv.TopNode = tv.SelectedNode;
                        e.Handled = true;
                        return;
                    }
                    if (e.Shift)
                    {
                        tvMapEntries.ScrollUp();
                        e.Handled = true;
                        return;
                    }
                    if (sender == tvMapEntries && ckbSyncScroll.Checked && tvMapEntriesSplit.SelectedNode != null)
                    {
                        tvMapEntriesSplit.SelectedNode = tvMapEntriesSplit.SelectedNode.PrevVisibleNode;
                    }
                    break;
                case Keys.Down:
                    if (e.Shift)
                    {
                        tvMapEntries.ScrollDown();
                        e.Handled = true;
                    }
                    if (sender == tvMapEntries && ckbSyncScroll.Checked && tvMapEntriesSplit.SelectedNode != null)
                    {
                        tvMapEntriesSplit.SelectedNode = tvMapEntriesSplit.SelectedNode.NextVisibleNode;
                    }
                    return;
            }
            base.OnKeyDown(e);
        }
        private void CurrentLineChanged()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(CurrentLineChanged));
                return;
            }

            if (MainViewModel.Current == null)
                return;

            GoToiLine(tvMapEntries.Nodes, MainViewModel.Current.CurrentiLine);
        }
        private void btnApplyGap_Click(object sender, EventArgs e)
        {
            ViewModel.RenderDocument();
        }
        #endregion

        private void ckbOverlay_CheckedChanged(object sender, EventArgs e)
        {
            ViewModel.RenderMainDocument();
            StatusUpdate(StatusModel.Completed);
        }

        public void ApplyColorScheme()
        {
            hbTimeGap.HighlightColor = Color.White;
            hbValues.HighlightColor = Color.Red;
            hbNetwork.HighlightColor = Color.Yellow;
            hbEvents.HighlightColor = Color.Blue;
            hbPatterns.HighlightColor = Color.Lime;
        }
    }
}
