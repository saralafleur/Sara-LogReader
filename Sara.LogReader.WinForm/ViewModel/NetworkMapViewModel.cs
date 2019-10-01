using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Sara.Common.DateTimeNS;
using Sara.Common.Extension;
using Sara.LogReader.Common;
using Sara.LogReader.Model;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.Property;
using Sara.LogReader.Service;
using Sara.LogReader.WinForm.Views.NetworkMapNS;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;
using File = Sara.LogReader.Model.FileNS.FileData;
using NetworkMapFile = Sara.LogReader.Model.NetworkMapNS.NetworkMapFile;
using NetworkMapModel = Sara.LogReader.Model.NetworkMapNS.NetworkMapModel;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class NetworkMapViewModel : ViewModelBase<NetworkMapView, NetworkMapModel, object>, IViewModelBaseNonGeneric
    {
        public NetworkMapViewModel()
        {
            var sw = new Stopwatch("Constructor NetworkMapViewModel");
            View = new NetworkMapView { ViewModel = this };
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }

        #region Sequence
        public enum RightLeft
        {
            Right,
            Left
        }

        private const string ARROW_BODY = "------";
        private const string LEFT_ARROW = "<<" + ARROW_BODY;
        private const string RIGHT_ARROW = ARROW_BODY + ">>";
        private const string LEFT_BLOCKING_ARROW = "<X" + ARROW_BODY;
        private const string RIGHT_BLOCKING_ARROW = ARROW_BODY + "X>";
        public class SequenceMessage
        {
            public SequenceMessage()
            {
                // Default
                ArrowDirection = RightLeft.Right;
                Duration = null;
            }
            public override string ToString()
            {
                var result = string.Empty;
                switch (Message.Source.Direction)
                {
                    case NetworkDirection.Na:
                        break;
                    case NetworkDirection.Send:
                        result = string.Format("{0}{1}{2}",
                            ArrowDirection == RightLeft.Left ? LEFT_ARROW : "",
                            Message.Source.NetworkMessageName,
                            ArrowDirection == RightLeft.Right ? RIGHT_ARROW : "");
                        break;
                    case NetworkDirection.SendBlocking:
                        result = string.Format("{0}{1}{2}",
                            ArrowDirection == RightLeft.Left ? LEFT_BLOCKING_ARROW : "",
                            Message.Source.NetworkMessageName,
                            ArrowDirection == RightLeft.Right ? RIGHT_BLOCKING_ARROW : "");
                        break;
                    case NetworkDirection.Receive:
                        result = string.Format("{0}{1}{2}",
                            ArrowDirection == RightLeft.Left ? RIGHT_ARROW : "",
                            Message.Source.NetworkMessageName,
                            ArrowDirection == RightLeft.Right ? LEFT_ARROW : "");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                result = string.Format("{0}{1}", result,
                    Duration != null ? " (" + Duration.Value.ToShortReadableString() + ")" : "");

                result = string.Format("{0}({1}) {2}", Message.Source.DateTime.ToString(DateTimeExt.DATE_FORMAT), Message.Source.FindFileValue(Keywords.IP), result);

                return result;
            }

            public NetworkMessageInfo Message { get; set; }
            public RightLeft ArrowDirection { get; set; }
            public TimeSpan? Duration { get; set; }
        }

        #endregion Sequence

        public string LastAnchorFilePath { get; set; }

        public override NetworkMapModel GetModel()
        {
            if (MainViewModel.Current == null)
                return null;

            View.StatusUpdate(StatusModel.Update("Getting Model"));
            try
            {
                var model = new NetworkMapModel
                {
                    NetworkMap = XmlDal.DataModel.Options.NetworkMap
                };

                if (MainViewModel.Current == null)
                    return model;

                if (!model.NetworkMap.Anchored)
                {
                    model.NetworkMap.AnchorFilePath = MainViewModel.Current.Model.File.Path;
                    if (model.NetworkMap.AnchorFilePath == string.Empty)
                        return model;
                }

                if (!System.IO.File.Exists(model.NetworkMap.AnchorFilePath))
                    return model;

                BuildNodes(model);
                BuildSequenceDiagram(model);

                return model;
            }
            finally
            {
                View.StatusUpdate(StatusModel.Completed);
            }
        }

        private void BuildNodes(NetworkMapModel model)
        {
            var anchorFile = XmlDal.CacheModel.GetFile(model.NetworkMap.AnchorFilePath);

            var nodes = new List<NetworkMapFile>();
            var tempNodes = new List<NetworkMapFile>();

            NetworkService.CheckNetworkMessages(anchorFile, View.StatusUpdate, "Anchor");

            // Add Anchor
            var map = AddFileAsNode(anchorFile, nodes, true);
            AddFileAsNode(anchorFile, tempNodes, true);
            model.CurrentAnchor = NodeBase.NodeToString(anchorFile.SourceType, anchorFile.Ip, anchorFile.Host);
            model.CurrentAnchorMapFile = map;

            // Only use the prior selected if we are on the same file
            if (LastAnchorFilePath == anchorFile.Path)
            {
                // Add Selected
                foreach (var selectedFile in model.NetworkMap.Nodes.Where(n => n.Selected))
                {
                    AddNode(selectedFile.Node, nodes, true, anchorFile.Path);
                    AddNode(selectedFile.Node, tempNodes, true, anchorFile.Path);
                }
            }
            LastAnchorFilePath = anchorFile.Path;

            // Expand Selected
            foreach (var selectedNode in tempNodes)
            {
                // For the selected Node find any files that match
                var expandNodes = NetworkService.GetNetworkFilesByNode(selectedNode.Node);
                foreach (var expandFile in expandNodes)
                {
                    NetworkService.CheckNetworkMessages(expandFile, View.StatusUpdate, "Extended");
                    ExpandNodes(expandFile, nodes);
                }
            }

            model.NetworkMap.Nodes = nodes;
        }
        private static void BuildSequenceDiagram(NetworkMapModel model)
        {
            var dt = new DataTable();

            dt.Columns.Add(AddColumn(model.CurrentAnchor, model.CurrentAnchorMapFile));

            foreach (var map in model.NetworkMap.Nodes.Where(n => n.Selected))
            {
                var columnName = GetColumnName(map.Node.ToString());
                if (!dt.Columns.Contains(columnName))
                    dt.Columns.Add(AddColumn(columnName, map));
            }

            // TODO: Order messages by DateTime with a rolling window so we insert in the correct location

            var columnIndex = 0;
            var rowIndex = 0;
            foreach (DataColumn column in dt.Columns)
            {
                var map = column.ExtendedProperties["node"] as NetworkMapFile;
                if (map == null)
                    throw new InvalidCastException();

                var firstColumn = dt.Rows.Count == 0;

                foreach (var filePath in map.FilePaths)
                {
                    var file = XmlDal.CacheModel.GetFile(filePath);
                    foreach (var msg in file.Network.NetworkMessages.OrderBy(n => n.Source.DateTime))
                    {
                        var row = new List<SequenceMessage>();

                        for (var i = 0; i < columnIndex; i++)
                        {
                            row.Add(null);
                        }

                        var c = new SequenceMessage
                        {
                            Message = msg,
                            ArrowDirection = RightLeft.Left
                        };

                        row.Add(c);

                        for (var i = columnIndex + 1; i < dt.Columns.Count; i++)
                        {
                            row.Add(null);
                        }

                        DataRow dr = dt.NewRow();
                        dr.ItemArray = row.ToArray();

                        if (firstColumn)
                            dt.Rows.Add(dr);
                        else
                        {
                            var currentRowDateTime = GetRowDateTime(dt, rowIndex);

                            while (currentRowDateTime <= c.Message.Source.DateTime)
                            {
                                if (rowIndex == dt.Rows.Count - 1)
                                    break;

                                rowIndex++;
                                currentRowDateTime = GetRowDateTime(dt, rowIndex);
                            }

                            if (rowIndex == dt.Rows.Count)
                                dt.Rows.Add(dr);
                            else
                                dt.Rows.InsertAt(dr, rowIndex);
                        }


                    }
                }

                columnIndex++;
            }

            //foreach (var anchor in anchorFile.Network.NetworkMessages.OrderBy(n => n.Source.iLine))
            //{
            //    var anchorSequence = new SequenceMessage { Message = anchor, ArrowDirection = RightLeft.Right };
            //    var row = new List<object> { anchorSequence };

            //    var test = NetworkMapService.GetNetworkMessagesBySourceLine(anchor.SourceItem);

            //    if (test.HasTargetFiles)
            //    {
            //        foreach (var target in test.Targets)
            //        {
            //            var targetFile = XmlDal.CacheModel.GetFile(target.Item.Source.FilePath);

            //            AddSource(model, targetFile.SourceType, targetFile.FindFileValue(Keywords.HOSTNAME), targetFile.FindFileValue(Keywords.IP));

            //            NetworkService.CheckNetworkMessages(targetFile, View.StatusUpdate, "Target");

            //            var targetTitle = GetTitle(targetFile);
            //            if (targetTitle == anchorTitle)
            //                continue;

            //            if (!dt.Columns.Contains(targetTitle))
            //                dt.Columns.Add(targetTitle);
            //            if (test.Targets.Count > 1)
            //                row.Add("* Multiple Links");
            //            else
            //            {
            //                var targetSequence = new SequenceMessage
            //                {
            //                    Message = target.Item,
            //                    ArrowDirection = RightLeft.Left
            //                };

            //                if (anchor.Source.Direction == NetworkDirection.Receive)
            //                    anchorSequence.Duration = target.Item.Source.DateTime - anchor.Source.DateTime;
            //                else
            //                    targetSequence.Duration = anchor.Source.DateTime - target.Item.Source.DateTime;

            //                row.Add(targetSequence);
            //            }

            //            break;
            //        }
            //    }
            //    dt.Rows.Add(row.ToArray());
            //}

            model.DataTable = dt;
        }

        private static DateTime GetRowDateTime(DataTable dt, int rowIndex)
        {
            if (dt.Rows.Count == 0)
                return new DateTime();

            foreach (var column in dt.Columns)
            {
                if (dt.Rows[rowIndex][column.ToString()] == null)
                    continue;
                var msg = dt.Rows[rowIndex][column.ToString()] as SequenceMessage;
                if (msg != null)
                    return msg.Message.Source.DateTime;
            }
            return new DateTime();
        }

        private static DataColumn AddColumn(string columnName, NetworkMapFile map)
        {
            var c = new DataColumn(GetColumnName(columnName), typeof(SequenceMessage));
            c.ExtendedProperties.Add("node", map);
            return c;
        }
        private static string GetColumnName(string name)
        {
            return name.Replace(":", ":" + Environment.NewLine).Replace("(", Environment.NewLine + "(");
        }
        private static void AddNode(NodeBase node, ICollection<NetworkMapFile> nodes, bool selected, string filePath)
        {
            AddSource(nodes, node.SourceType, node.Host, node.Ip, node.SourceStart, node.SourceEnd, selected, filePath);
        }
        private static NetworkMapFile AddFileAsNode(File file, ICollection<NetworkMapFile> nodes, bool selected)
        {
            return AddSource(nodes, file.SourceType, file.Host, file.Ip, file.Start, file.End, selected, file.Path);
        }
        private static void ExpandNodes(File file, ICollection<NetworkMapFile> nodes)
        {
            foreach (var node in file.Network.Nodes)
            {
                AddSource(nodes, node.SourceType, node.Host, node.Ip, node.SourceStart, node.SourceEnd, false, file.Path);
            }
        }
        private static NetworkMapFile AddSource(ICollection<NetworkMapFile> nodes, string sourceType, string host, string ip, DateTime start, DateTime end, bool selected, string filePath)
        {
            var source = new NodeBase
                {
                    SourceType = sourceType,
                    Host = host,
                    Ip = ip,
                    SourceStart = start,
                    SourceEnd = end,
                };

            var map =
                nodes.FirstOrDefault(
                    target => target.Node.Ip == source.Ip && target.Node.SourceType == source.SourceType);

            if (map == null)
            {
                map = new NetworkMapFile
                {
                    Node = source,
                    Selected = selected
                };
                nodes.Add(map);
            }

            if (!map.FilePaths.Contains(filePath))
                map.FilePaths.Add(filePath);

            return map;
        }

        public void GoToFile(string path)
        {
            MainViewModel.GoToFile(path);
        }
        public void GoToLine(int iLine)
        {
            MainViewModel.GoToLine(iLine);
        }
        public void SetAnchor(bool @checked)
        {
            var networkMap = XmlDal.DataModel.Options.NetworkMap;
            networkMap.Anchored = @checked;
            networkMap.AnchorIp = !@checked ? "" : MainViewModel.Current.Model.File.FindFileValue(Keywords.IP);
            networkMap.AnchorFilePath = !@checked ? "" : MainViewModel.Current.Model.File.Path;
            if (!@checked)
                RenderDocument();
        }
    }
}
