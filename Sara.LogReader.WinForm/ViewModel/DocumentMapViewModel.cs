using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Sara.LogReader.WinForm.Views.DocumentMap;
using Sara.Common.DateTimeNS;
using Sara.Common.Extension;
using Sara.Common.Threading;
using Sara.LogReader.Common;
using Sara.LogReader.Model;
using Sara.LogReader.Model.DocumentMap;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Service;
using Sara.LogReader.Service.FileServiceNS;
using Sara.WinForm.MVVM;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class DocumentMapViewModel : ViewModelBase<DocumentMapWindow, List<DocumentEntry>, object>, IViewModelBaseNonGeneric
    {
        #region Properties
        internal List<ValueBookMark> DocumentMapDurations { get; set; }
        private DocumentMapWindow _viewStored;
        private DocumentMapWindow _View
        {
            get
            {
                if (_viewStored != null)
                    return _viewStored;

                _viewStored = View as DocumentMapWindow;
                if (_viewStored == null)
                    throw new InvalidCastException($"View must be of type {typeof(DocumentMapWindow)}");
                return _viewStored;
            }
        }
        public string LastPath { get { return _current == null ? "" : _current.Path; } }
        private FileData _current { get; set; }
        private IgnoreBacklogQueue<FileData> _queue { get; set; }
        public object ColorSerive { get; private set; }
        #endregion

        #region Setup
        public DocumentMapViewModel()
        {
            var sw = new Stopwatch("Constructor DocumentMapViewModel");
            DocumentMapDurations = new List<ValueBookMark>();
            View = new DocumentMapWindow { ViewModel = this };
            XmlDal.DataModel.CategoryCacheDataController.InvalidateNotificationEvent += InvalidateFilter;
            _queue = new IgnoreBacklogQueue<FileData>();
            _queue.ProcessItemEvent += QueueRender;
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }
        ~DocumentMapViewModel()
        {
            // ReSharper disable once DelegateSubtraction
            XmlDal.DataModel.CategoryCacheDataController.InvalidateNotificationEvent -= InvalidateFilter;
        }
        public void Exit()
        {
            _queue.Exit();
        }
        #endregion

        #region Render
        private void QueueRender(FileData file)
        {
            OutputService.Log($"Document Map Queue Render for {MainViewModel.Current.Model.File.Path}");
            _current = file;
            base.RenderDocument();
        }

        internal Color OverlayColor(int iLine)
        {
            if (!View.ApplyOverlay)
                return Color.Transparent;

            if (_current.DocumentMap.Exists(n => n.iLine == iLine))
                return XmlDal.DataModel.Options.ColorScheme.Current.DocumentMapOverlayBackColor;
            return Color.Transparent;
        }

        public void RenderComplete()
        {
            _queue.ItemComplete();
        }
        public override void RenderDocument()
        {
            if (MainViewModel.Current == null)
                return;

            if (!View.Visible) return;

            OutputService.Log($"DocumentMap Render Request for {MainViewModel.Current.Model.File.Path}");
            _queue.Add(MainViewModel.Current.Model.File);
        }
        public override sealed List<DocumentEntry> GetModel()
        {
            DocumentMapService.BuildDocumentMap(_current);
            return _current.DocumentMap;
        }
        /// <summary>
        /// When the Category CacheDataController is invalidated, meaning the Category selection changed, this method is called
        /// </summary>
        private void InvalidateFilter()
        {
            if (MainViewModel.Current == null)
                return;

            MainViewModel.Current.Model.File.IsCached_Lazy_DocumentMap = false;
            RenderDocument();
        }
        #endregion

        #region BuildTree
        public void BuildTreeView(IEnumerable<DocumentEntry> model, Action<TreeNodeCollection> callback)
        {
            lock (this)
            {
                DocumentMapDurations.Clear();
                var root = new TreeNode().Nodes;
                TreeNodeCollection lastLevel1 = null;
                TreeNodeCollection lastLevel2 = null;
                TreeNodeCollection lastLevel3 = null;

                #region Loop

                foreach (var item in model.OrderBy(n => n.iLine).ThenBy(n => n.Type))
                {
                    if ((!_View.ShowTimeGap && (item.Type == DocumentMapType.TimeGAPEnd || item.Type == DocumentMapType.TimeGAPStart)) ||
                        (!_View.ShowNetwork && item.Type == DocumentMapType.Network) ||
                        (!_View.ShowValues && item.Type == DocumentMapType.Value) ||
                        (!_View.ShowEvents && item.Type == DocumentMapType.Event))
                        continue;

                    if (_View.Filter && !item.Filtered)
                        continue;

                    TreeNode node;

                    switch (item.Type)
                    {
                        case DocumentMapType.TimeGAPStart:
                        case DocumentMapType.TimeGAPEnd:
                        case DocumentMapType.Event:
                        case DocumentMapType.Network:

                            node = new TreeNode(string.Format("{0}{1}", GetDocumentMapiLine(item), item.Name)) { Tag = item };
                            break;
                        case DocumentMapType.Value:
                            node = new TreeNode(string.Format("{0}{1} - {2}", GetDocumentMapiLine(item), item.Name, item.Value)) { Tag = item };
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    #region Level

                    switch (item.Level)
                    {
                        case DocumentMapLevel.Root:
                            lastLevel1 = null;
                            lastLevel2 = null;
                            lastLevel3 = null;
                            root.Add(node);
                            break;
                        case DocumentMapLevel.Level1:
                            if (root.Count == 0)
                                root.Add(new TreeNode("---"));

                            root.Add(node);

                            lastLevel1 = root[root.Count - 1].Nodes;
                            lastLevel2 = null;
                            lastLevel3 = null;
                            break;
                        case DocumentMapLevel.Level2:
                            if (root.Count == 0)
                                root.Add(new TreeNode("---"));
                            lastLevel1 = root[root.Count - 1].Nodes;

                            //if (lastLevel2 == null)
                            //{
                            //    if (lastLevel1.Count == 0)
                            //        lastLevel1.Add(new TreeNode("---"));
                            //}

                            lastLevel1.Add(node);

                            lastLevel2 = lastLevel1[lastLevel1.Count - 1].Nodes;
                            lastLevel3 = null;
                            break;
                        case DocumentMapLevel.Level3:
                            if (root.Count == 0)
                                root.Add(new TreeNode("---"));
                            lastLevel1 = root[root.Count - 1].Nodes;

                            if (lastLevel2 == null)
                            {
                                if (lastLevel1.Count == 0)
                                    lastLevel1.Add(new TreeNode("---"));
                                lastLevel2 = lastLevel1[lastLevel1.Count - 1].Nodes;
                            }

                            //if (lastLevel3 == null)
                            //{
                            //    if (lastLevel2.Count == 0)
                            //        lastLevel2.Add(new TreeNode("---"));

                            //}

                            lastLevel2.Add(node);
                            lastLevel3 = lastLevel2[lastLevel2.Count - 1].Nodes;
                            break;
                        case DocumentMapLevel.Sibling:
                            if (lastLevel3 != null)
                            {
                                lastLevel3.Add(node);
                                break;
                            }
                            if (lastLevel2 != null)
                            {
                                lastLevel2.Add(node);
                                break;
                            }
                            if (lastLevel1 != null)
                            {
                                lastLevel1.Add(node);
                                break;
                            }
                            root.Add(node);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    #endregion Level

                    ProcessDisplayTimeDuration(item, node);
                    ProcessGaps(item, node);
                }
                #endregion Loop

                MainViewModel.Current.Model.File.DocumentMapDurations = DocumentMapDurations;

                callback(root);
            }
        }
        private string GetDocumentMapiLine(DocumentEntry item)
        {
            // Note: The visible LineFS Number is 1 based where as iLine is 0 based.  Thus why I add 1. - Sara
            return XmlDal.DataModel.Options.ShowDocumentMapLineNumber ? (item.iLine + 1).ToString(CultureInfo.InvariantCulture) + " " : "";
        }
        /// <summary>
        /// If DisplayTimeDuration is True, then the system will calculate the Time Duration from the Parent Node to this Child Node
        /// </summary>
        private void ProcessGaps(DocumentEntry item, TreeNode node)
        {
            if (!_View.ShowGap) return;
            if (item.Type != DocumentMapType.Event) return;

            // First Node
            if (node.PrevNode == null) return;

            var target = node.PrevNode;

            var prevItem = target.Tag as DocumentEntry;
            if (prevItem == null) return;

            DateTimeExt.TryParseWithTimeZoneRemoval(RegularExpression.GetTextBetweenTwoCharacters("<", ">", prevItem.Text), out DateTime prevDateTime);
            DateTimeExt.TryParseWithTimeZoneRemoval(RegularExpression.GetTextBetweenTwoCharacters("<", ">", item.Text), out DateTime nodeDateTime);

            int gapDuration;
            if (!int.TryParse(_View.GapDuration, out gapDuration)) return;

            if (prevDateTime.Difference(nodeDateTime).Milliseconds < gapDuration) return;

            node.Text = string.Format("{0} <{1}>", node.Text,
                prevDateTime.Difference(nodeDateTime).ToReadableString());
            node.ForeColor = Color.Red;
            //ViewModel.AddDocumentMapDuration(node.Text, item.iLine);
        }
        /// <summary>
        /// If DisplayTimeDuration is True, 
        /// then the system will calculate the Time Duration from the Parent Node to this Child Node
        /// </summary>
        private void ProcessDisplayTimeDuration(DocumentEntry item, TreeNode node)
        {
            if (item.Type != DocumentMapType.Event) return;

            var e = XmlDal.DataModel.GetEvent(item.Id);
            if (e == null)
                return;

            DocumentEntry parentItem = null;

            if (e.DisplayDurationFromParent)
            {
                var target = node.Parent ?? _View.Root;
                parentItem = target.Tag as DocumentEntry;
            }
            else
            if (e.DisplayDurationFromSibling)
            {
                var target = node.PrevNode ?? node.Parent ?? _View.Root;
                parentItem = target.Tag as DocumentEntry;
            }
            else return;

            if (parentItem == null) return;

            DateTime parentDateTime;
            DateTimeExt.TryParseWithTimeZoneRemoval(RegularExpression.GetTextBetweenTwoCharacters("<", ">", parentItem.Text),
                out parentDateTime);

            DateTime childDateTime;
            DateTimeExt.TryParseWithTimeZoneRemoval(RegularExpression.GetTextBetweenTwoCharacters("<", ">", item.Text), out childDateTime);

            node.Text = string.Format("{0} [{1}]", node.Text,
                parentDateTime.Difference(childDateTime).ToReadableString());
            node.ForeColor = Color.Red;
            DocumentMapDurations.Add(new ValueBookMark { Name = node.Text, iLine = item.iLine, DocumentMapDuration = true });
        }
        #endregion

        #region GoTo
        public void GoToLine(int iLine)
        {
            MainViewModel.Current.GoToLine(iLine);
        }
        public void GoToEntry(DocumentEntry model)
        {
            switch (model.Type)
            {
                case DocumentMapType.TimeGAPStart:
                case DocumentMapType.TimeGAPEnd:
                    break;
                case DocumentMapType.Network:
                    MainViewModel.GoToEventId(model.Id);
                    break;
                case DocumentMapType.Value:
                    MainViewModel.GoToValueId(model.Id);
                    break;
                case DocumentMapType.Event:
                    MainViewModel.GoToEventId(model.Id);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public void SetFocusOnDocument()
        {
            if (MainViewModel.Current != null)
            {
                MainViewModel.Current.Focus();
            }
        }

        internal void RenderMainDocument()
        {
            if (MainViewModel.Current != null)
                MainViewModel.Current.RenderDocument();
        }
        #endregion
    }
}
