using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Sara.Common.DateTimeNS;
using Sara.Logging;
using Sara.LogReader.Model;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.SockDrawer;
using Sara.LogReader.Service.FileServiceNS;
using Sara.LogReader.Service.SockDrawer;
using Sara.LogReader.WinForm.Views.SockDrawer;
using Sara.LogReader.WinForm.UI_Common;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class SockDrawerViewModel : ViewModelBase<SockDrawerWindow, SockDrawerModel, object>, IViewModelBaseNonGeneric
    {
        public SockDrawerViewModel()
        {
            var sw = new Stopwatch("Constructor SockDrawerViewModel");
            _lastUpdate = DateTime.Now;

            View = new SockDrawerWindow { RightToLeftLayout = false, ViewModel = this };
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }

        private DateTime _lastUpdate;
        public void StatusUpdate(string status)
        {
            // Only update every second - Sara
            if ((DateTime.Now - _lastUpdate).TotalMilliseconds < 500) return;
            _lastUpdate = DateTime.Now;

            View.StatusUpdate(StatusModel.Update(status));
        }

        internal void AddFiles(TreeNode node, List<string> files)
        {
            var model = node.Tag as TreeViewFile;
            if (model == null)
                return;

            if (model.FilePath != null)
                files.Add(model.FilePath);

            foreach (TreeNode targetNode in node.Nodes)
            {
                AddFiles(targetNode, files);
            }
        }

        public override SockDrawerModel GetModel()
        {
            var model = MainViewModel.Current == null
                                      ? null
                                      : MainViewModel.Current.Model;

            return SockDrawerService.GetModel(model);
        }

        /// <summary>
        /// Updates the entire SockDrawer
        /// </summary>
        public void Update(bool forceClear)
        {
            SockDrawerService.Update(View.UpdateCompletedCallback, View.StatusUpdate, forceClear);
        }
        public void UpdateActive(bool forceClear)
        {
            if (MainViewModel.Current == null)
                throw new Exception("You must open a log file first before you can update the active document!");

            SockDrawerService.Update(View.UpdateCompletedCallback,
                                     View.StatusUpdate,
                                     new List<string> { MainViewModel.Current.Model.File.Path }, forceClear);
        }
        public void UpdateSelectedGroup(TreeNode selectedNode, bool forceClear)
        {
            var files = new List<string>();

            AddFiles(selectedNode, files);
            SockDrawerService.Update(View.UpdateCompletedCallback, View.StatusUpdate, files, forceClear);
        }

        public string SockDrawerFolder
        {
            get { return XmlDal.DataModel.Options.SockDrawerFolder; }
        }

        public static void GoToValueId(int valueId)
        {
            MainViewModel.GoToValueId(valueId);
        }

        public void GoToDocument(TreeNode selectedNode, Action callback)
        {
            Log.WriteEnter(typeof(SockDrawerViewModel).FullName, MethodBase.GetCurrentMethod().Name);
            if (selectedNode == null)
                return;

            var model = selectedNode.Tag as TreeViewFile;
            if (model == null) return;
            if (model.FilePath == null) return;

            View.SkipTreeViewRender = true;
            MainViewModel.GoToFile(model.FilePath, callback);
        }

        /// <summary>
        /// Takes a copy of the Nodes in the TreeView and prepares them for display using a background thread
        /// </summary>
        public void BuildFileTreeView(Action<TreeNodeCollection> callback, bool showStatics)
        {
            TreeViewService.BuildTreeView(new BuildFileTreeViewArgs() { RunInBackgroundThread = true, ShowStatics = showStatics, Callback = callback });
        }

        public void Settings()
        {
            SettingsViewModel.ShowSettings();
        }

        public int RemoveDuplicateFiles()
        {
            return FileService.RemoveDuplicateFiles();
        }

        /// <summary>
        /// Allows external source to triger the Render of the TreeView
        /// </summary>
        public void RenderTreeView()
        {
            View.SkipTreeViewRender = false;
            RenderDocument();
        }

        internal void RemoveFavoriteFile(string filePath, string favoriteGroup)
        {
            XmlDal.CacheModel.RemoveFavoriteFile(filePath, favoriteGroup);
        }

        internal void AddFavorite(string filePath)
        {
            MainViewModel.AddFavorite(filePath);
        }

        internal void SetFocus(string path)
        {
            View.SetFocus(path);
        }
    }
}
