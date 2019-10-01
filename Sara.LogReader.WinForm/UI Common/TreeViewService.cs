using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Sara.Common.DateTimeNS;
using Sara.Common.Extension;
using Sara.LogReader.Common;
using Sara.LogReader.Model;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.SockDrawer;

namespace Sara.LogReader.WinForm.UI_Common
{
    public enum FileTreeViewGroupEnum
    {
        Customer,
        CustomerYearMonth,
        YearWeekCustomer,
        YearMonthCustomer
    }

    public class BuildFileTreeViewArgs
    {
        public BuildFileTreeViewArgs()
        {
            Nodes = new TreeNode().Nodes;
            Grouping = FileTreeViewGroupEnum.Customer;
        }
        public bool RunInBackgroundThread { get; set; }
        public TreeNodeCollection Nodes { get; set; }
        public Action<TreeNodeCollection> Callback { get; set; }
        // When true, statics are added to the Node Name
        public bool ShowStatics { get; set; }
        public FileTreeViewGroupEnum Grouping { get; set; }
    }

    /// <summary>
    /// Contains the functions used to generated the TreeView list of sources
    /// I.e. the sock drawer tree view
    /// </summary>
    public static class TreeViewService
    {
        public static object SyncObject = new object();
        private static string SockDrawerFolder
        {
            get { return XmlDal.DataModel.Options.SockDrawerFolder; }
        }

        public static void BuildTreeView(BuildFileTreeViewArgs args)
        {
            if (args.RunInBackgroundThread)
                ThreadPool.QueueUserWorkItem(BuildFileTreeViewAsync, args);
            else
                BuildFileTreeViewAsync(args);
        }

        public static void AddRange(TreeNodeCollection source, TreeNodeCollection target)
        {
            foreach (TreeNode node in source)
            {
                var copy = node.Clone() as TreeNode;
                if (copy == null)
                    throw new Exception("Copy cannot be null");
                target.Add(copy);
            }
        }

        private static void BuildFileTreeViewAsync(object state)
        {
            lock (SyncObject)
            {
                var args = state as BuildFileTreeViewArgs;
                if (args == null)
                    throw new Exception("state must be of type BuildFileTreeViewArgs");

                var stopwatch = new Stopwatch(MethodBase.GetCurrentMethod().Name);
                try
                {
                    TreeNode node = null;

                    if (XmlDal.CacheModel.Favorites.Files.Count() > 0)
                    {
                        var parentNode = new TreeNode("Favorite(s)") { Tag = new TreeViewFile { Favorite = true } };
                        args.Nodes.Add(parentNode);

                        foreach (var favoriteFile in XmlDal.CacheModel.Favorites.Files.OrderByDescending(n => n.FavoriteGroup).OrderByDescending(n => n.Path))
                        {
                            var file = XmlDal.CacheModel.GetFile(favoriteFile.Path);
                            var groupNode = PrepareFavoriteGroup(favoriteFile.FavoriteGroup, parentNode.Nodes);
                            node = PrepareTreeNodeCollection(file.GetGroupFolder(SockDrawerFolder), groupNode.Nodes);
                            node = PrepareTreeNode(file.SourceType ?? Keywords.SOURCE_TYPE_UNKNOWN, node, true);
                            var titleNode = PrepareTitleNode(file, node, true);
                            PrepareDateTimeNode(file, titleNode, args.ShowStatics, true, favoriteFile.FavoriteGroup);
                        }
                    }


                    foreach (var file in XmlDal.CacheModel.Files.OrderByDescending(n => n.Start))
                    {
                        switch (args.Grouping)
                        {
                            case FileTreeViewGroupEnum.Customer:
                                node = PrepareTreeNodeCollection(file.GetGroupFolder(SockDrawerFolder), args.Nodes);
                                node = PrepareTreeNode(file.SourceType ?? Keywords.SOURCE_TYPE_UNKNOWN, node);
                                break;
                            case FileTreeViewGroupEnum.CustomerYearMonth:
                                node = PrepareTreeNodeCollection(file.GetGroupFolder(SockDrawerFolder), args.Nodes);
                                node = PrepareTreeNode(file.Start.Year.ToString(), node);
                                node = PrepareTreeNode(file.Start.Month.ToString(), node );
                                node = PrepareTreeNode(file.SourceType ?? Keywords.SOURCE_TYPE_UNKNOWN, node);
                                break;
                            case FileTreeViewGroupEnum.YearWeekCustomer:
                                node = PrepareTreeNodeCollection(file.Start.Year.ToString(), args.Nodes);
                                node = PrepareTreeNode(file.Start.WeekOfYear().ToString(), node);
                                node = PrepareTreeNode(file.GetGroupFolder(SockDrawerFolder), node);
                                node = PrepareTreeNode(file.SourceType ?? Keywords.SOURCE_TYPE_UNKNOWN, node);
                                break;
                            case FileTreeViewGroupEnum.YearMonthCustomer:
                                node = PrepareTreeNodeCollection(file.Start.Year.ToString(), args.Nodes);
                                node = PrepareTreeNode(file.Start.Month.ToString(), node);
                                node = PrepareTreeNode(file.GetGroupFolder(SockDrawerFolder), node);
                                node = PrepareTreeNode(file.SourceType ?? Keywords.SOURCE_TYPE_UNKNOWN, node);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                        node = PrepareTitleNode(file, node, false);
                        PrepareDateTimeNode(file, node, args.ShowStatics, false, "");
                    }

                    RemoveNotFound(args.Nodes);
                    PrepareCount(args.Nodes);
                }
                finally
                {
                    stopwatch.Stop(300);
                }
                args.Callback(args.Nodes);
            }
        }

        private static TreeNode PrepareFavoriteGroup(string group, TreeNodeCollection nodes)
        {
            var groupNode = nodes.Cast<TreeNode>().FirstOrDefault(pNode => RemoveCount(pNode.Text) == group);

            if (groupNode != null) return groupNode;

            groupNode = new TreeNode(group) { Tag = new TreeViewFile { Favorite = true } };
            nodes.Add(groupNode);
            return groupNode;
        }

        private static TreeNode PrepareCustomerNode(FileData file, TreeNodeCollection nodes, bool favorite)
        {
            var groupFolder = file.GetGroupFolder(SockDrawerFolder);
            var folderNode = nodes.Cast<TreeNode>().FirstOrDefault(pNode => RemoveCount(pNode.Text) == groupFolder);

            // If the Type is not found then create an 'Unknown' type
            if (folderNode != null) return folderNode;

            folderNode = new TreeNode(groupFolder) { Tag = new TreeViewFile { Favorite = favorite } };
            nodes.Add(folderNode);
            return folderNode;
        }
        private static TreeNode PrepareTreeNodeCollection(string nodeName, TreeNodeCollection nodes, bool favorite = false)
        {
            var parentNode = nodes.Cast<TreeNode>().FirstOrDefault(pNode => RemoveCount(pNode.Text) == nodeName);

            if (parentNode != null) return parentNode;

            parentNode = new TreeNode(nodeName) { Tag = new TreeViewFile { Favorite = favorite } };
            nodes.Add(parentNode);
            return parentNode;
        }
        private static TreeNode PrepareTreeNode(string nodeName, TreeNode node, bool favorite = false)
        {
            var parentNode = node.Nodes.Cast<TreeNode>().FirstOrDefault(n => RemoveCount(n.Text) == nodeName);
            if (parentNode != null) return parentNode;
            parentNode = new TreeNode(nodeName) { Tag = new TreeViewFile { Favorite = favorite } };
            node.Nodes.Add(parentNode);
            return parentNode;
        }

        private static string RemoveCount(string value)
        {
            if (value.IndexOf(" - ", StringComparison.Ordinal) == -1)
                return value;

            var result = value.Substring(0, value.IndexOf(" - ", StringComparison.Ordinal));
            return result;
        }

        private static TreeNode PrepareTypeNode(FileData file, TreeNode folderNode, bool favorite)
        {
            var parentNode = folderNode.Nodes.Cast<TreeNode>().FirstOrDefault(pNode => RemoveCount(pNode.Text) == (file.SourceType ?? Keywords.SOURCE_TYPE_UNKNOWN));

            // If the Type is not found then create an 'Unknown' type
            if (parentNode != null) return parentNode;

            parentNode = new TreeNode(file.SourceType ?? Keywords.SOURCE_TYPE_UNKNOWN) { Tag = new TreeViewFile { Favorite = favorite } };
            folderNode.Nodes.Add(parentNode);
            return parentNode;
        }

        private static TreeNode PrepareTitleNode(FileData file, TreeNode typeNode, bool favorite)
        {
            TreeNode titleNode = null;

            if (file.HasTitle)
            {
                // Check for Title
                foreach (var tNode in typeNode.Nodes.Cast<TreeNode>().Where(tNode => RemoveCount(tNode.Text) == file.Title))
                {
                    titleNode = tNode;
                    break;
                }

                if (titleNode != null) return titleNode;

                titleNode = new TreeNode(file.Title) { Tag = new TreeViewFile { Favorite = favorite } };
                typeNode.Nodes.Add(titleNode);
            }
            else
            {
                // Find the "Unknown" node
                foreach (var uNode in typeNode.Nodes.Cast<TreeNode>().Where(uNode => RemoveCount(uNode.Text) == Keywords.SOURCE_TYPE_UNKNOWN))
                {
                    titleNode = uNode;
                    break;
                }
                if (titleNode != null) return titleNode;

                titleNode = new TreeNode(Keywords.SOURCE_TYPE_UNKNOWN) { Tag = new TreeViewFile { Favorite = favorite } };
                typeNode.Nodes.Insert(0, titleNode);
            }
            return titleNode;
        }

        private static void PrepareDateTimeNode(FileData file, TreeNode titleNode, bool showStatics, bool favorite, string favoriteGroup)
        {

            var found = false;
            foreach (var fNode in titleNode.Nodes.Cast<TreeNode>().Where(fNode => ((TreeViewFile)fNode.Tag).FilePath == file.Path))
            {
                fNode.Tag = new TreeViewFile { FilePath = file.Path, Favorite = favorite, FavoriteGroup = favoriteGroup };
                fNode.ToolTipText = file.Path;
                found = true;
                break;
            }

            if (found) return;

            var caption = string.Empty;
            if (showStatics)
            {
                var BuildDuration = file.FindFileValue(Fields.BUILD_DURATION);
                var LastBuild = file.FindFileValue(Fields.BUILD_LASTBUILD);
                caption = string.Format("[{3} {1}] {2} -> {0}", file.Range, BuildDuration, LastBuild, file.Start.DayOfWeek.ToString().Substring(0, 1));
            }
            else
                caption = string.Format("{1} {0}", file.Range, file.Start.DayOfWeek.ToString().Substring(0, 1));

            var fileNode = new TreeNode
            {
                Text = caption,
                Tag = new TreeViewFile { FilePath = file.Path, Favorite = favorite, FavoriteGroup = favoriteGroup },
                ToolTipText = file.Path
            };

            titleNode.Nodes.Add(fileNode);
        }

        /// <summary>
        /// Remove any files that exists in the Cache but he raw File is no longer in the File System
        /// </summary>
        private static void RemoveNotFound(TreeNodeCollection nodes)
        {
            for (var i = nodes.Count - 1; i >= 0; i--)
            {
                var node = nodes[i];
                if (node == null) return;

                RemoveNotFound(node.Nodes);

                var model = node.Tag as TreeViewFile;

                if ((model == null || !System.IO.File.Exists(model.FilePath) || model.FilePath == null) && node.Nodes.Count == 0)
                {
                    nodes.Remove(node);
                }
            }
        }

        /// <summary>
        /// Counts the File Nodes for each parent
        /// </summary>
        private static int PrepareCount(TreeNodeCollection nodes)
        {
            var totalCount = 0;
            foreach (TreeNode node in nodes)
            {
                var count = 0;
                var o = (TreeViewFile)node.Tag;
                if (o.FilePath != null)
                    count++;
                count += PrepareCount(node.Nodes);
                if (node.Nodes.Count > 0)
                    node.Text = string.Format("{0} - ({1})", RemoveCount(node.Text), count);
                totalCount += count;
            }
            return totalCount;
        }
    }
}
