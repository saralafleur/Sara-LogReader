using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading.Tasks;
using FastColoredTextBoxNS;
using Sara.Common.DateTimeNS;
using Sara.Common.Debug;
using Sara.Logging;
using Sara.LogReader.Model;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.LogReaderNS;
using Sara.LogReader.Model.NetworkMapping;
using Sara.LogReader.Service;
using Sara.LogReader.WinForm.ViewModel;
using Sara.LogReader.WinForm.Views.Document;
using Sara.WinForm.ColorScheme;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.Common;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Document
{
    public partial class DocumentWindow : DockContent, IViewDock<string, object>, IColorSchemeControl
    {
        #region Properties
        public DocumentViewModel ViewModel { get; set; }
        public bool StartupReady { get { return MainViewModel.StartupComplete; } }
        public string FileName
        {
            get { return _mFileName; }
        }
        public int CurrentiLine
        {
            get { return fctbDocument.Selection.Start.iLine; }
        }
        public string CurrentLine
        {
            get { return fctbDocument.Lines[fctbDocument.Selection.Start.iLine]; }
        }
        private readonly Color _flashColor = Color.Transparent;
        private Point _lastLowerLeft;
        private int _lastGoToLine;
        private WaitHandle[] _waitHandles;
        #endregion Properties

        #region Setup
        public DocumentWindow()
        {
            InitializeComponent();

            _waitHandles = new WaitHandle[]
            {
              _renderComplete,
              Shutdown.WaitHandle
            };
            CachedLinePropertyObjects = new List<object>();
            UICommon.SetDoubleBuffered(fctbDocument);
            fctbDocument.CurrentLineColor = Color.Red;
            fctbDocument.PaintLineFullAccess += OnPaintLineFullAccess;
            ViewModel = new DocumentViewModel(this);
            scBase.SplitterDistance = 250;
            MainViewModel.DocumentFontChanged += OnDocumentFontChanged;
        }

        private void OnDocumentFontChanged(Font font)
        {
            fctbDocument.Font = font;
        }

        ~DocumentWindow()
        {
            MainViewModel.DocumentFontChanged -= OnDocumentFontChanged;
        }


        private void OnPaintLineFullAccess(Line line, int iLine)
        {
            ViewModel.GetLineHighlight(line, iLine);
        }

        protected override string GetPersistString()
        {
            // Add extra information into the persist string for this document
            // so that it is available when deserialized.
            return GetType() + "," + FileName + "," + Text;
        }
        public void UpdateView(object selectedModel)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Prepares the Events for the DocumentWindow and returns the DocumentControl
        /// </summary>
        public IDocumentControl PrepareDocument(string path)
        {
            _mFileName = path;
            fctbDocument.DocumentPath = path;
            ToolTipText = path;

            if (path != string.Empty)
            {
                ViewModel.CollaspeAllEvent += CollaspeAll;
                ViewModel.ExpandAllEvent += ExpandAll;
                ViewModel.GoToLineEvent += OnGoToLine;
                ViewModel.FocusEvent += OnFocus;

                return new DocumentControl(fctbDocument);
            }

            return null;
        }
        #endregion Setup

        #region Render
        private ManualResetEvent _renderComplete = new ManualResetEvent(false);
        public void Render(string model)
        {
            _renderComplete.Reset();
            var r = new ReleaseUI($"Render ReleaseUI for {ViewModel.Model.File.Path}", true);
            ViewModel.RenderStarted();
            SuspendLayout();
            try
            {
                if (model == null)
                {
                    Log.WriteError("Model is NULL, this should not happen!", typeof(DocumentWindow).FullName, MethodBase.GetCurrentMethod().Name);
                    return;
                }


                if (InvokeRequired)
                {
                    Invoke(new Action<string>(Render), model);
                    return;
                }

                PerformanceService.StartEvent($"{PerformanceService.CONST_RenderDocument}{ViewModel.Model.File.Path}");
                OutputService.Log($"Document Render for {ViewModel.Model.File.Path}");
                Log.WriteEnter(typeof(DocumentWindow).FullName, MethodBase.GetCurrentMethod().Name);

                // Setting SelectionChangedDelayedEnabled for the first time
                if (!fctbDocument.SelectionChangedDelayedEnabled)
                    fctbDocument.SelectionChangedDelayedEnabled = true;
                scBase.Panel1Collapsed = !XmlDal.DataModel.Options.DocumentSourceInfo;
                PreCachePropertyLineData();
                r.DoEvents();
                sourceInfo.Render(ViewModel.Model.File);
                fctbDocument.ClearCache();
                r.DoEvents();
                fctbDocument.Text = model;
                r.DoEvents();
                ApplyFolding();
                // Force the control to paint itself now

                fctbDocument.Invalidate();
                StatusUpdate(StatusModel.Completed);
                Log.WriteTrace("Document Render Complete...", typeof(DocumentWindow).FullName, MethodBase.GetCurrentMethod().Name);
                PerformanceService.StopEvent($"Rebuild Active Document for {ViewModel.Model.File.Path}", true);
                PerformanceService.StopEvent($"{PerformanceService.CONST_RenderDocument}{ViewModel.Model.File.Path}", true);
                r.Stop();
            }
            finally
            {
                ResumeLayout();
                ViewModel.DocumentRenderComplete();
                _renderComplete.Set();
            }
        }
        /// <summary>
        /// Cache of the LineFS Proprty Objects that are used when the document is first Shown
        /// </summary>
        private List<object> CachedLinePropertyObjects { get; set; }
        private void PreCachePropertyLineData()
        {
            CachedLinePropertyObjects.Clear();

            // When we first open a document, all visible lines will lookup their property object on the Main UI Thread.
            // To avoid this, I will cache the visible lines + 5
            // This will allow the Main UI to continue un-interrupted - Sara
            var visibleLines = (fctbDocument.Height / fctbDocument.Font.Height) + 5;
            var stopIndex = ViewModel.Model.File.RawText.Count - 1 >= visibleLines
                ? visibleLines
                : ViewModel.Model.File.RawText.Count - 1;

            Parallel.For(0, stopIndex, i =>
            {
                object property = null;
                GetLinePropertyObject(ref property, i);
                lock (CachedLinePropertyObjects)
                    CachedLinePropertyObjects.Add(property);
            });
        }
        private void ApplyFolding()
        {
            if (!XmlDal.DataModel.Options.DocumentFolding)
                return;

            var sw = new Stopwatch($"ApplyFolding for {ViewModel.Model.File.Count} lines");
            foreach (var foldingEvent in ViewModel.FoldingEvents)
            {
                fctbDocument.Range.SetFoldingMarkers(foldingEvent.StartingFolding, foldingEvent.EndingFolding);
            }
            sw.Stop(100);
            Log.WriteTrace($"Exiting ApplyFolder for \"{_mFileName}\"", typeof(DocumentWindow).FullName, MethodBase.GetCurrentMethod().Name);
        }
        /// <summary>
        /// Return True when a Lookup was performed
        /// </summary>
        private bool GetLinePropertyObject(ref object lineData, int index)
        {
            try
            {
                if (lineData != null)
                    return false;
                {
                    if (CachedLinePropertyObjects.Count != 0 &&
                        index <= CachedLinePropertyObjects.Count - 1)
                    {
                        lineData = CachedLinePropertyObjects[index];
                        return true;
                    }

                    lineData = PropertyService.GetProperty(new LineArgs
                    {
                        Path = ViewModel.Model.File.Path,
                        iLine = index,
                        // Do not use the text from the fctbDocument, it will remove special characters! - Sara
                        Line = ViewModel.Model.File.RawText[index]
                    });
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(typeof(DocumentWindow).FullName, MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        #endregion Render

        #region Events
        public void StatusUpdate(IStatusModel model)
        {
            StatusPanel.StatusUpdate(model);
        }
        private void ExpandAll()
        {
            fctbDocument.ExpandAllFoldingBlocks();
        }
        private void CollaspeAll()
        {
            fctbDocument.CollapseAllFoldingBlocks();
        }
        private void OnFocus()
        {
            fctbDocument.Focus();
        }
        private void fctbDocument_SelectionChangedDelayed(object sender, EventArgs e)
        {
            ViewModel.CurrentLineChanged();
        }
        private void Document_FormClosed(object sender, FormClosedEventArgs e)
        {
            ViewModel.RemoveDocument();
        }
        private void Document_Enter(object sender, EventArgs e)
        {
            ViewModel.CheckCurrent();
            ViewModel.CurrentLineChanged();
        }
        private void fctbDocument_Click(object sender, EventArgs e)
        {
            var me = (MouseEventArgs)e;
            if (me.Button != MouseButtons.Right) return;
            if (!ViewModel.CurrentLineIsNetworkMessage) return;

            var ptLowerLeft = new Point(me.X, me.Y);
            ptLowerLeft = fctbDocument.PointToScreen(ptLowerLeft);
            SetupMenu(ptLowerLeft, false);
        }
        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void CloseAll_Click(object sender, EventArgs e)
        {
            ViewModel.CloseAllDocuments();
        }
        private void CloseAllButThisOne_Click(object sender, EventArgs e)
        {
            ViewModel.CloseAllButThisOne();
        }
        private void NavigateNetwork_Click(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;
            if (menu == null)
                return;

            var item = menu.Tag as NetworkMessageInfoModel;
            if (item == null)
                return;

            ViewModel.GoToFileAndLine(item.Item.Source.FilePath, item.Item.Source.iLine);
        }
        private void followNetworkMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewModel.FollowNetworkMessage();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // TODO: Fix Later - Sara
            //// Collaspe
            //if (keyData == (Keys.Control | Keys.Oemcomma))
            //{
            //    var line = fctbDocument.Find FindNextExpandedStartFoldingLine(CurrentiLine);
            //    if (line != -1)
            //    {
            //        fctbDocument.CollapseFoldingBlock(line);
            //    }
            //    OnGoToLine(line);
            //}
            //// Expand
            //if (keyData == (Keys.Control | Keys.OemPeriod))
            //{
            //    var line = fctbDocument.FindNextCollaspedStartFoldingLine(CurrentiLine);
            //    if (line != -1)
            //    {
            //        fctbDocument.ExpandFoldedBlock(line);
            //    }
            //    OnGoToLine(line);
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion Events

        #region GoTo

        private void OnGoToLine(int iLine)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(OnGoToLine), iLine);
                return;
            }

            if (fctbDocument.LinesCount == 0)
            {
                Log.WriteError("The Document is blank when you attempted a GoToLine!  This should never happen!!!", typeof(DocumentWindow).FullName, MethodBase.GetCurrentMethod().Name);
                return;
            }

            // We run a Thread here to prevent us from waiting on the Main UI Thread while we wait for the WaitAny to single - Sara
            ThreadPool.QueueUserWorkItem(d =>
            {
                var sw = new Stopwatch($"OnGoToLine {iLine}");
                var sw2 = new Stopwatch($"OnGoToLine WaitOne for {iLine}");
                WaitHandle.WaitAny(_waitHandles);
                lock (_renderComplete)
                {
                    if (_lastGoToLine == iLine)
                        return;
                    sw2.Stop(0);

                    Invoke(new Action(delegate
                    {
                        try
                        {
                            fctbDocument.Selection.Start = new Place(0, iLine);
                            fctbDocument.DoCaretVisible();
                        }
                        catch (Exception ex)
                        {
                            Log.WriteError(typeof(DocumentWindow).FullName, MethodBase.GetCurrentMethod().Name, ex);
                        }
                    }));
                    _lastGoToLine = iLine;
                }
                sw.Stop(0);
            });

        }
        private void sourceInfo_GoTo(ValueBookMark obj)
        {
            ViewModel.GoTo(obj);
        }
        #endregion GoTo

        #region Popup Menu
        protected internal void SetupMenuShortCut()
        {
            if (!ViewModel.CurrentLineIsNetworkMessage) return;

            var ptLowerLeft = new Point(0, 0);
            ptLowerLeft = fctbDocument.PointToScreen(ptLowerLeft);
            SetupMenu(ptLowerLeft, true);
        }
        private void SetupMenu(Point lowerLeft, bool isShortCut)
        {
            _lastLowerLeft = lowerLeft;
            if (isShortCut)
                ViewModel.GetNetworkMessages(SetupDirect);
            else
                ViewModel.GetNetworkMessages(SetupMenuCallback);
        }
        private void SetupDirect(NetworkTargets targets)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<NetworkTargets>(SetupDirect), targets);
                return;
            }

            if (targets.Targets.Count == 1)
            {
                ViewModel.GoToFileAndLine(targets.Targets[0].Item.SourceItem.Path, targets.Targets[0].Item.SourceItem.iLine);
            }
            else
            {
                SetupMenuCallback(targets);
            }
        }
        private void SetupMenuCallback(NetworkTargets targets)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<NetworkTargets>(SetupMenuCallback), targets);
                return;
            }

            PopupMenu.Items.Clear();
            foreach (var item in targets.Targets.Select(model => new ToolStripMenuItem(model.ToString(), null, NavigateNetwork_Click) { Tag = model }))
            {
                PopupMenu.Items.Add(item);
            }

            if (PopupMenu.Items.Count == 0)
            {
                PopupMenu.Items.Add(targets.HasTargetFiles
                    ? new ToolStripLabel("File Found - No Network Match", null, false, null)
                    : new ToolStripLabel("File NOT Found", null, false, null));
            }
            PopupMenu.Width++;
            PopupMenu.Width--;
            PopupMenu.Show(_lastLowerLeft);
        }
        #endregion Popup Menu

        #region Visual Ping
        public void PingCurrentLine()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(PingCurrentLine));
                return;
            }

            ThreadPool.QueueUserWorkItem(delegate
            {
                const int wait = 30;
                Thread.Sleep(wait);
                PingCurrentLineCallback(Color.Red, 30);
                Thread.Sleep(wait);
                PingCurrentLineCallback(Color.Red, 25);
                Thread.Sleep(wait);
                PingCurrentLineCallback(Color.Red, 20);
                Thread.Sleep(wait);
                PingCurrentLineCallback(Color.Red, 15);
                Thread.Sleep(wait);
                PingCurrentLineCallback(Color.Red, 10);
                Thread.Sleep(wait);
                PingCurrentLineCallback(Color.Red, 5);
                Thread.Sleep(wait);
                PingCurrentLineCallback(Color.Red, 3);
            });
        }
        private void PingCurrentLineCallback(Color color, int penSize)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Color, int>(PingCurrentLineCallback), color, penSize);
                return;
            }

            fctbDocument.CurrentLineColor = color;
            fctbDocument.CurrentPenSize = penSize;
            fctbDocument.Refresh();
        }
        #endregion Visual Ping

        private void DocumentWindow_Shown(object sender, EventArgs e)
        {
            StatusUpdate(StatusModel.Update("Waiting"));

            ColorService.Setup(this);
        }

        public void ApplyColorScheme()
        {
            fctbDocument.BackColor = ColorService.ColorScheme.Current.ControlBackColor;
            fctbDocument.ForeColor = ColorService.ColorScheme.Current.ControlForeColor;
            fctbDocument.IndentBackColor = ColorService.ColorScheme.Current.ControlBackColor;
            fctbDocument.LineNumberColor = ColorService.ColorScheme.Current.ControlForeColor;
        }
    }
}