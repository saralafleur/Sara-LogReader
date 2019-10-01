using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Linq;
using Sara.Common.Extension;
using Sara.Logging;
using Sara.LogReader.Model;
using Sara.LogReader.Model.DocumentMap;
using Sara.LogReader.Model.IDE;
using Sara.LogReader.Model.PatternNS;
using Sara.LogReader.Service;
using Sara.LogReader.Service.PatternServiceNS;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.Common;
using Sara.WinForm.ControlsNS;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Controls
{
    public partial class MonitorScriptIDE : UserControl
    {
        #region Properties
        private bool _isRendering;
        public Pattern Pattern { get; set; }
        public string Script
        {
            get { return tbScript.Text; }
            set
            {
                tbScript.Text = value;
                ckbShowScript.Checked = true;
                ViewOptions();
            }
        }
        public Action OnSave { get; set; }
        public Action<bool> OnFullScreen { get; set; }
        public bool SaveEnabled { get { return btnSave.Enabled; } set { btnSave.Enabled = value; } }
        private enum ViewState
        {
            Tree,
            Grid,
            Summary
        }
        private ViewState CurrentViewState { get; set; }
        private const string _Vertical = "Vertical";
        private const string _Horizontal = "Horizontal";
        #endregion

        #region Setup
        public MonitorScriptIDE()
        {
            InitializeComponent();

            UICommon.SetDoubleBuffered(this);
            DataGridViewHelper.AutoSizeGrid(dgvResults);

            CurrentViewState = ViewState.Summary; // Default
            dgvResults.Dock = DockStyle.Fill;
            tvResults.Dock = DockStyle.Fill;
            pnlSummary.Dock = DockStyle.Fill;
            HandleDestroyed += MonitorScriptIDE_HandleDestroyed;
            ViewOptions();
        }
        #endregion

        #region Render
        public void Render()
        {
            //TODO: btnRun should change based on if we have a document open or not - Sara
            btnRun.Enabled = MainViewModel.Current != null;
            var model = MonitorScriptIDEViewModel.GetModel();
            ckbShowTokens.Checked = model.ShowTokens;
            if (!UICommon.InDesignMode)
                tbScript.Values = MonitorScriptIDEViewModel.GetValues();
        }
        internal void RunScripts()
        {
            try
            {
                CheckAutoSave();
                PrepareResults();

                MonitorScriptIDEViewModel.Run(new TestArgs
                {
                    Script = tbScript.Text,
                    ShowTokens = ckbShowTokens.Checked,
                    ResultCallback = RenderResults,
                    Files = new List<string>() { MainViewModel.Current.Model.File.Path },
                    StatusUpdate = StatusPanel.StatusUpdate
                });
            }
            catch (Exception ex)
            {
                Log.WriteError("","",ex);
            }
        }
        internal void RunScriptsOnAll(Pattern pattern)
        {
            CheckAutoSave();
            PrepareResults();

            if (pattern == null)
            {
                var r = new PatternTestResults();
                r.Log.Add($"{DateTime.Now.ToShortTimeString()} You must select a Pattern");
                RenderResults(r);
                return;
            }

            StatusPanel.SP_DisplayRemainingTime = false;
            StatusPanel.StatusUpdate(StatusModel.Update("Preparing File List"));

            MonitorScriptIDEViewModel.RunScriptsOnAll(new TestArgs
            {
                Script = tbScript.Text,
                ShowTokens = ckbShowTokens.Checked,
                ResultCallback = RenderResults,
                Pattern = pattern,
                StatusUpdate = StatusPanel.StatusUpdate
            }, RunScriptOnAllCallback);

            return;
        }
        private void RunScriptOnAllCallback(bool @continue)
        {
            if (!@continue)
                StatusPanel.StatusUpdate(StatusModel.Completed);

            StatusPanel.SP_DisplayRemainingTime = true;
        }
        internal Color OverlayColor(int iLine)
        {
            if (!ckbOverlay.Checked)
                return Color.Transparent;

            if (_lastResults == null)
                return Color.Transparent;

            if (_lastResults.EventExists(iLine))
                return XmlDal.DataModel.Options.ColorScheme.Current.PatternOverlayBackColor;

            return Color.Transparent;
        }
        private PatternTestResults _lastResults;
        private void RenderResults(PatternTestResults result)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<PatternTestResults>(RenderResults), result);
                return;
            }

            _lastResults = result;

            try
            {
                #region Result Map
                if (result.PatternSummaries.Count() == 0)
                {
                    ckbResults.Checked = false;
                    ckbShowOutput.Checked = true;
                }
                else
                {
                    scMain.Panel2Collapsed = false;

                    List<PatternTestResult> patterns = new List<PatternTestResult>();

                    foreach (var item in result.PatternSummaries)
                    {
                        patterns.AddRange(item.Patterns);
                    }

                    StatusPanel.StatusUpdate(StatusModel.Update($"Rendering..."));
                    RenderTreeView(patterns);
                    RenderGrid(patterns);
                    RenderSummary(result.PatternSummaries);

                    ckbResults.Checked = true;
                }
                #endregion

                t.Text = string.Join(Environment.NewLine, result.Log.ToArray());

                AdjustSplitterDistance();
            }
            finally
            {
                StatusPanel.StatusUpdate(StatusModel.Completed);
            }
        }
        private void AdjustSplitterDistance()
        {
            //var topHeight = tbResults.Font.Height * tbResults.Lines.Count();
            //var bottomHeight = tbScript.Font.Height * tbScript.Lines.Count();

            //if (topHeight + bottomHeight >= scScript.Height)
            //{
            //    if (topHeight > scScript.Height)
            //        scScript.SplitterDistance = scScript.Height - (tbResults.Font.Height * 7) + 10;
            //    else
            //        scScript.SplitterDistance = topHeight + 10;

            //    tbResults.SelectionStart = tbResults.Text.Length;
            //    tbResults.ScrollToCaret();
            //}
            //else
            //    scScript.SplitterDistance = scScript.Height - bottomHeight - 10;
        }
        #endregion

        #region Render Grid
        private DataTable _lastDataTable;
        private void RenderGrid(List<PatternTestResult> patterns)
        {
            StatusPanel.StatusUpdate(StatusModel.Update($"Rendering Grid..."));
            //TODO: Return when ready, Removed due to larger size table, over 655555 I.e
            //var dt = MonitorScriptService.GetPatternsDataTable(patterns);
            //dgvResults.DataSource = dt;
            //_lastDataTable = dt;
            //DataGridViewHelper.AutoSizeGrid(dgvResults);
            //ColorService.Apply(dgvResults);
        }
        #endregion Render Grid

        #region Render TreeView
        private void UpNode(ref TreeNode parent)
        {
            if (parent == null) return;

            parent = parent.Parent;
        }
        private void AddNode(ref TreeNode parent, TreeNode child, bool isParent)
        {
            if (parent == null)
                tvResults.Nodes.Add(child);
            else
                parent.Nodes.Add(child);

            if (isParent)
                parent = child;
        }
        private const string _countPreText = " - (";
        private int PrepareCount(TreeNodeCollection nodes)
        {
            var totalCount = 0;
            foreach (TreeNode node in nodes)
            {
                var o = (DocumentEntry)node.Tag;

                var count = PrepareCount(node.Nodes);

                if (count > 0)
                    node.Text = $"{RemoveCount(node.Text)}{_countPreText}{count})";

                // If we are on an EventPattern Instance, then do not include the childern count - Sara
                if (o.Type == DocumentMapType.EventInstance)
                    count = 0;

                count++;
                totalCount += count;
            }
            return totalCount;
        }
        private string RemoveCount(string value)
        {
            if (value.LastIndexOf(_countPreText, StringComparison.Ordinal) == -1)
                return value;

            var result = value.Substring(0, value.LastIndexOf(_countPreText, StringComparison.Ordinal));
            return result;
        }
        private void RenderTreeView(List<PatternTestResult> patterns)
        {
            tvResults.BeginUpdate();
            tvResults.SuspendLayout();
            tvResults.Nodes.Clear();

            try
            {
                if (patterns.Count > 500 && ckbRenderTreeView.Checked)
                {
                    var result = MessageBox.Show($"Do you want to Render the TreeView?  The output contains {patterns.Count} patterns.", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                        return;
                }

                if (!ckbRenderTreeView.Checked) return;

                StatusPanel.StatusUpdate(StatusModel.Update($"Rendering TreeView..."));

                _isRendering = true;

                var _lastPath = string.Empty;
                TreeNode pathNode = null;

                TreeNode parent = null;
                foreach (var p in patterns)
                {
                    // FILE PATH NODE
                    if (p.Path != _lastPath &&
                        !p.Options.HideFilePath)
                    {
                        pathNode = new TreeNode(p.Path)
                        {
                            Tag = new DocumentEntry()
                            {
                                Name = p.Path,
                                iLine = 0,
                                Type = DocumentMapType.File,
                                Path = p.Path
                            }
                        };
                        parent = null; // Forces us to add this node in the root
                        AddNode(ref parent, pathNode, true);
                        _lastPath = p.Path;
                    }

                    if (!p.Options.HidePattern)
                    {
                        if (tvResults.Nodes.Count > 0 && ((parent == null && tvResults.Nodes[tvResults.Nodes.Count - 1].Text == p.Name) ||
                                                              (parent != null &&
                                                               parent.LastNode != null &&
                                                               parent.LastNode.Name == p.Name)))
                        {
                            parent = tvResults.Nodes[tvResults.Nodes.Count - 1];
                        }
                        else
                        {
                            // PATTERN NODE
                            var patternNode = new TreeNode(p.Name)
                            {
                                Tag = new DocumentEntry()
                                {
                                    Name = p.Name,
                                    iLine = p.StartiLine,
                                    Type = DocumentMapType.Event,
                                    Path = p.Path
                                }
                            };

                            AddNode(ref parent, patternNode, true);
                        }
                    }

                    foreach (var pe in p.Events)
                    {
                        if (pe.Options.Hide)
                            continue;
                        if (!pe.Options.HideEvent)
                        {
                            var peNode = new TreeNode(pe.Name)
                            {
                                Tag = new DocumentEntry
                                {
                                    Name = pe.Name,
                                    iLine = pe.StartiLine,
                                    Type = DocumentMapType.EventInstance,
                                    Path = p.Path
                                }
                            };

                            if (pe.EventType == EventType.GapOutside || pe.EventType == EventType.GapInside || pe.EventType == EventType.Duration)
                                peNode.NodeFont = new Font("Microsoft Sans Serif", 7.25f);

                            AddNode(ref parent, peNode, true);
                        }

                        foreach (var v in pe.Values)
                        {
                            var vText = v.Value.ToString();
                            if (v.Value is TimeSpan ts)
                                vText = ts.ToShortReadableString();

                            var valueNode = new TreeNode($"{v.Name} {vText}")
                            {
                                Tag = new DocumentEntry
                                {
                                    Name = v.Name,
                                    iLine = pe.StartiLine,
                                    Type = DocumentMapType.EventInstanceValue,
                                    Path = p.Path,
                                    Value = v.Value.ToString(),
                                    ValueObject = v.Value
                                }
                            };
                            AddNode(ref parent, valueNode, false);
                        }

                        if (!pe.Options.HideEvent)
                            UpNode(ref parent);
                    }

                    UpNode(ref parent);
                }
            }
            finally
            {
                PrepareCount(tvResults.Nodes);
                tvResults.ResumeLayout();
                tvResults.EndUpdate();
                _isRendering = false;
            }
            if (tvResults.Nodes.Count == 1)
                tvResults.Nodes[0].Expand();
        }
        #endregion Render TreeView

        #region Render Summary
        public enum RangeType
        {
            Integer,
            TimeSpan
        }
        #region CONST
        private const string IDLE = "Idle";
        private const string DURATION = "Duration";
        private const string KNOWN_IDLE_EVENTS = "Known Idle Events";
        private const string FREQUENCY = "Frequency";
        private const string UNEXPECTED = "Unexpected";
        #endregion

        private void RenderSummary(List<PatternTestSummary> summaries)
        {
            StatusPanel.StatusUpdate(StatusModel.Update($"Rendering Summary..."));

            tvSummary.Nodes.Clear();

            var _maxLength = GetMaxLength(summaries);

            foreach (var summary in summaries)
            {
                var _summary = tvSummary.Nodes.Add(summary.PatternName);

                var _durationNode = _summary.Nodes.Add(summary.Durations.Count == 0 ? $"{DURATION.ToFixedColumnRight(10)} - n/a" : DURATION);
                foreach (var item in summary.Durations)
                    AddItem(_maxLength, _durationNode, item);

                var _IdleNode = _summary.Nodes.Add(summary.KnownIdle.Count == 0 ? $"{IDLE.ToFixedColumnRight(10)} - n/a" : IDLE);
                foreach (var item in summary.KnownIdle)
                    AddItem(_maxLength, _IdleNode, item);

                var _frequencyNode = _summary.Nodes.Add(summary.Frequencies.Count == 0 ? $"{FREQUENCY.ToFixedColumnRight(10)} - n/a" : FREQUENCY);
                foreach (var item in summary.Frequencies)
                    AddItem(_maxLength, _frequencyNode, item);

                var _unexpectedNode = _summary.Nodes.Add(summary.Unexpected.Count == 0 ? $"{UNEXPECTED.ToFixedColumnRight(10)} - n/a" : UNEXPECTED);
                foreach (var item in summary.Unexpected)
                    AddItem(_maxLength, _unexpectedNode, item);
            }

            //    if (item.IdleDetail.Any())
            //    {
            //        var _IdleSummaryParent = current.Nodes.Add(IDLE);
            //        TreeNode _KnownIdle = null;
            //        var _index = 1;
            //        foreach (var detail in item.IdleDetail)
            //        {
            //            if (_index > 2)
            //            {
            //                _IdleSummaryParent = _KnownIdle;
            //                _IdleSummaryParent.Expand();
            //            }

            //            var n = AddItem(_maxLength, _IdleSummaryParent, detail);
            //            if (_index == 1)
            //                _KnownIdle = n.Nodes.Add(KNOWN_IDLE_EVENTS);

            //            _index++;
            //        }
            //    }

            // Expand all Level 0 & 1 nodes
            foreach (TreeNode level0 in tvSummary.Nodes)
            {
                level0.Expand();
                foreach (TreeNode level1 in level0.Nodes)
                {
                    if (level1.Text != FREQUENCY)
                        level1.Expand();

                    if (level1.Text == FREQUENCY)
                        level1.Text = $"{FREQUENCY} ({level1.Nodes.Count})";
                }
            }
        }

        /// <summary>
        /// Returns the MaxLength for EventName.  
        /// Used to keep the columns aligned.
        /// </summary>
        private int GetMaxLength(List<PatternTestSummary> summaries)
        {
            var _maxLength = 0;
            foreach (var item in summaries)
            {
                foreach (var item2 in item.Durations)
                {
                    var _max = item2.EventName.Length;
                    _maxLength = _max > _maxLength ? _max : _maxLength;
                }
                foreach (var item2 in item.KnownIdle)
                {
                    var _max = item2.EventName.Length;
                    _maxLength = _max > _maxLength ? _max : _maxLength;
                }
                foreach (var item2 in item.Frequencies)
                {
                    var _max = item2.EventName.Length;
                    _maxLength = _max > _maxLength ? _max : _maxLength;
                }
                foreach (var item2 in item.Unexpected)
                {
                    var _max = item2.EventName.Length;
                    _maxLength = _max > _maxLength ? _max : _maxLength;
                }
            }

            return _maxLength;
        }

        private static TreeNode AddItem(int maxLength, TreeNode parentNode, object item)
        {
            TreeNode n;
            if (item is Duration d)
                n = parentNode.Nodes.Add($"{d.ToString(maxLength)}");
            else if (item is Frequency f)
            {
                switch (f.FrequencyType)
                {
                    case FrequencyType.PerPattern:
                        if (string.IsNullOrEmpty(f.Path))
                            n = parentNode.Nodes.Add($"{f.ToString(maxLength)}");
                        else
                        {
                            n = new TreeNode(f.ToString(maxLength))
                            {
                                Tag = new DocumentEntry()
                                {
                                    Name = f.ToString(maxLength),
                                    iLine = f.iline,
                                    Type = DocumentMapType.EventInstance,
                                    Path = f.Path
                                }
                            };

                            parentNode.Nodes.Add(n);
                        }
                        break;
                    case FrequencyType.PerFile:
                        n = parentNode.Nodes.Add($"{f.ToString(maxLength)}");
                        break;
                    default:
                        throw new Exception("Unknown Type!");
                }
            }
            else if (item is CleanAttribute c)
                n = parentNode.Nodes.Add($"{c.ToString(maxLength)}");
            else
                throw new Exception("item is of unknown type!");

            if (item is CleanAttribute c2)
                AddItemChildern(n, c2);

            return n;
        }

        private static void AddItemChildern(TreeNode n, CleanAttribute item)
        {
            foreach (var @event in item.Events.OrderByDescending(n3 => n3.Duration))
            {
                var eNode = new TreeNode(@event.ToString().TrimStart())
                {
                    Tag = new DocumentEntry()
                    {
                        Name = @event.EventName,
                        iLine = @event.StartiLine,
                        Type = DocumentMapType.EventInstance,
                        Path = @event.Path
                    }
                };

                var startNode = n.Nodes.Add(eNode);

                if (@event.StartiLine != @event.EndiLine)
                    eNode.Nodes.Add(new TreeNode("End")
                    {
                        Tag = new DocumentEntry()
                        {
                            Name = @event.EventName,
                            iLine = @event.EndiLine,
                            Type = DocumentMapType.EventInstance,
                            Path = @event.Path
                        }
                    });

                foreach (var child in @event.ChildEvents.OrderByDescending(n2 => n2.Duration))
                {
                    var _childNode = new TreeNode(child.ToString())
                    {
                        Tag = new DocumentEntry()
                        {
                            Name = child.EventName,
                            iLine = child.StartiLine,
                            Type = DocumentMapType.EventInstance,
                            Path = child.Path
                        }
                    };
                    eNode.Nodes.Add(_childNode);
                    if (child.StartiLine != child.EndiLine)
                        _childNode.Nodes.Add(new TreeNode("End")
                        {
                            Tag = new DocumentEntry()
                            {
                                Name = child.EventName,
                                iLine = child.EndiLine,
                                Type = DocumentMapType.EventInstance,
                                Path = child.Path
                            }
                        });
                }

            }
        }
        #endregion Render Summary

        #region Events
        public event EventHandler ScriptChanged;
        private void MonitorScriptIDE_HandleDestroyed(object sender, System.EventArgs e)
        {
            if (!UICommon.InDesignMode)
                Save();
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            RunScripts();
        }

        private void CheckAutoSave()
        {
            if (ckbAutoSave.Checked && btnSave.Enabled)
                OnSave?.Invoke();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming soon...");
        }
        private void btnRunOnAll_Click(object sender, EventArgs e)
        {
            RunScriptsOnAll(Pattern);
        }
        private void tbScript_TextChanged(object sender, EventArgs e)
        {
            ScriptChanged(sender, e);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            tvResults.ExpandAll();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            tvResults.CollapseAll();
        }
        private void tvDocumentMap_AfterSelect(object sender, TreeViewEventArgs e)
        {
            GoToResultiLine();
        }
        private void ckbOverlay_CheckedChanged(object sender, EventArgs e)
        {
            MainViewModel.Current.RenderDocument();
        }
        private void tvDocumentMap_Enter(object sender, EventArgs e)
        {
            GoToResultiLine();
        }
        private bool _shown = false;
        private void MonitorScriptIDE_VisibleChanged(object sender, EventArgs e)
        {
            if (!_shown)
            {
                _shown = true;
                if (!DesignMode)
                    ColorService.Setup(this);
            }
        }
        private void cbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_lastResults == null) return;

            RenderResults(_lastResults);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            OnSave?.Invoke();
        }
        private void ckbFullScreen_CheckedChanged(object sender, EventArgs e)
        {
            OnFullScreen?.Invoke(!ckbFulleScreen.Checked);
        }
        private void ckbShowResults_CheckedChanged(object sender, EventArgs e)
        {
            ViewOptions();
        }
        private void ckbShowScript_CheckedChanged(object sender, EventArgs e)
        {
            ViewOptions();
        }
        private void btnOrientation_Click_1(object sender, EventArgs e)
        {
            btnOrientation.Text = btnOrientation.Text == _Vertical ? _Horizontal : _Vertical;
            ViewOptions();
        }
        private void btnExportToCsv_Click(object sender, EventArgs e)
        {
            //var dt = dgvResults.DataSource as DataTable;
            //if (dt == null)
            //    throw new Exception("DataSource must be of type DataTable");
            var dt = PatternService.PrepareCSVExport(_lastResults);

            // Temp to spend things up
            dt.ToCsv(@"Z:\?");

            MessageBox.Show("Done");

            //var saveFileDialog1 = new SaveFileDialog { Filter = @"CSV|*.csv|Text|*.txt", Title = @"Export to CSV" };
            //saveFileDialog1.ShowDialog();

            //// If the file name is not an empty string open it for saving.
            //if (saveFileDialog1.FileName != "")
            //{
            //    dt.ToCsv(saveFileDialog1.FileName);
            //}

        }
        private void btnTree_Click(object sender, EventArgs e)
        {
            CurrentViewState = ViewState.Tree;
            ViewOptions();
        }
        private void ckbShowOutput_CheckedChanged(object sender, EventArgs e)
        {
            ViewOptions();
        }
        private void btnSummary_Click(object sender, EventArgs e)
        {
            CurrentViewState = ViewState.Summary;
            ViewOptions();
        }
        private void btnGrid_Click(object sender, EventArgs e)
        {
            CurrentViewState = ViewState.Grid;
            ViewOptions();
        }
        private void tvSummary_AfterSelect(object sender, TreeViewEventArgs e)
        {
            GoToSummaryiLine();
        }
        private void tvSummary_Enter(object sender, EventArgs e)
        {
            GoToSummaryiLine();
        }
        #endregion Events

        #region Methods
        public void Save()
        {
            MonitorScriptIDEViewModel.SaveModel(new IDEModel
            {
                ShowTokens = ckbShowTokens.Checked
            });
        }
        private void PrepareResults()
        {
            //scScript.Panel2Collapsed = false;

            // I want the screen to clear for a fraction of a second so the User is aware the Pattern Scan ran again
            t.Text = "";
            Application.DoEvents();
            Thread.Sleep(200);
        }
        private void GoToResultiLine()
        {
            if (!ckbNavigation.Checked)
                return;

            // Do not GoToLine if we are Rendering - Sara
            if (_isRendering)
                return;
            if (tvResults.SelectedNode == null)
                return;
            var item = (DocumentEntry)tvResults.SelectedNode.Tag;
            if (item == null)
                return;

            ThreadPool.QueueUserWorkItem(m =>
            {
                MonitorScriptIDEViewModel.GoToLine(item);
                FocusResult();
            });
        }
        private void FocusResult()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FocusResult));
                return;
            }

            tvResults.Focus();
        }
        private void FocusSummary()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FocusSummary));
                return;
            }

            tvSummary.Focus();
        }
        private void ViewOptions()
        {
            scMain.Panel1Collapsed = !ckbShowScript.Checked;
            scMain.Panel2Collapsed = !ckbResults.Checked && !ckbShowOutput.Checked;
            btnOrientation.Enabled = !scMain.Panel1Collapsed;
            scResult.Panel1Collapsed = !ckbResults.Checked;
            scResult.Panel2Collapsed = !ckbShowOutput.Checked;
            btnTree.Enabled = btnGrid.Enabled = btnSummary.Enabled = ckbResults.Checked;
            switch (CurrentViewState)
            {
                case ViewState.Tree:
                    btnTree.BackColor = ColorService.ColorScheme.Current.ButtonSelectedBackColor;
                    btnGrid.BackColor = ColorService.ColorScheme.Current.ButtonBackColor;
                    btnSummary.BackColor = ColorService.ColorScheme.Current.ButtonBackColor;
                    btnTree.ForeColor = ColorService.ColorScheme.Current.ButtonSelectedForeColor;
                    btnGrid.ForeColor = ColorService.ColorScheme.Current.ButtonForeColor;
                    btnSummary.ForeColor = ColorService.ColorScheme.Current.ButtonForeColor;
                    tvResults.BringToFront();
                    break;
                case ViewState.Grid:
                    dgvResults.BringToFront();
                    btnTree.BackColor = ColorService.ColorScheme.Current.ButtonBackColor;
                    btnGrid.BackColor = ColorService.ColorScheme.Current.ButtonSelectedBackColor;
                    btnSummary.BackColor = ColorService.ColorScheme.Current.ButtonBackColor;
                    btnTree.ForeColor = ColorService.ColorScheme.Current.ButtonForeColor;
                    btnGrid.ForeColor = ColorService.ColorScheme.Current.ButtonSelectedForeColor;
                    btnSummary.ForeColor = ColorService.ColorScheme.Current.ButtonForeColor;
                    break;
                case ViewState.Summary:
                    btnTree.BackColor = ColorService.ColorScheme.Current.ButtonBackColor;
                    btnGrid.BackColor = ColorService.ColorScheme.Current.ButtonBackColor;
                    btnSummary.BackColor = ColorService.ColorScheme.Current.ButtonSelectedBackColor;
                    btnTree.ForeColor = ColorService.ColorScheme.Current.ButtonForeColor;
                    btnGrid.ForeColor = ColorService.ColorScheme.Current.ButtonForeColor;
                    btnSummary.ForeColor = ColorService.ColorScheme.Current.ButtonSelectedForeColor;
                    pnlSummary.BringToFront();
                    break;
                default:
                    throw new Exception("Invalid View State!");
            }
            switch (btnOrientation.Text)
            {
                case _Vertical:
                    scMain.Orientation = Orientation.Horizontal;
                    break;
                case _Horizontal:
                    scMain.Orientation = Orientation.Vertical;
                    break;
            }
        }
        private void GoToSummaryiLine()
        {
            if (!ckbNavigation.Checked)
                return;

            // Do not GoToLine if we are Rendering - Sara
            if (_isRendering)
                return;
            if (tvSummary.SelectedNode == null)
                return;
            var item = (DocumentEntry)tvSummary.SelectedNode.Tag;
            if (item == null)
                return;

            ThreadPool.QueueUserWorkItem(m =>
            {
                MonitorScriptIDEViewModel.GoToLine(item);
                FocusSummary();
            });
        }
        #endregion

        private void ckbWrap_CheckedChanged(object sender, EventArgs e)
        {
            tbScript.WordWrap = ckbWrap.Checked;
        }

        private void btnScriptFontIncrease_Click(object sender, EventArgs e)
        {
            float size = tbScript.Font.Size;
            size++;
            tbScript.Font = new Font(tbScript.Font.OriginalFontName, size);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float size = tbScript.Font.Size;
            size--;
            tbScript.Font = new Font(tbScript.Font.OriginalFontName, size);
        }
    }
}
