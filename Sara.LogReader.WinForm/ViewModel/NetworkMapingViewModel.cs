using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Sara.Common.DateTimeNS;
using Sara.Logging;
using Sara.LogReader.Model;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.NetworkMapping;
using Sara.LogReader.Service;
using Sara.LogReader.Service.FileServiceNS;
using Sara.LogReader.WinForm.Views.NetworkMapping;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class NetworkMapingViewModel : ViewModelBase<NetworkMappingWindow,NetworkMapCacheData, object>, IViewModelBaseNonGeneric
    {
        public NetworkMapingViewModel()
        {
            var sw = new Stopwatch("Constructor NetworkMapingViewModel");
            View = new NetworkMappingWindow { ViewModel = this };
            MainViewModel.CurrentLineChangedEvent += RenderByLineChange;
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }


        ~NetworkMapingViewModel()
        {
            MainViewModel.CurrentLineChangedEvent -= RenderByLineChange;
        }

        private string LastFilePath { get; set; }
        private int LastiLine { get; set; }

        public void RenderCommon(object m, bool lineChanged, bool forceRender)
        {
            Log.WriteWarning("Network Mapping - Entering RenderCommon", typeof(NetworkMapingViewModel).FullName, MethodBase.GetCurrentMethod().Name);
            if (MainViewModel.Current == null)
            {
                View.RenderCallback(null, false);
                return;
            }

            // Do not render anything until the MainViewModel is Ready - Sara
            if (!MainViewModel.StartupComplete)
                return;

            if (!forceRender &&
                LastFilePath != null &&
                LastFilePath == MainViewModel.Current.Model.File.Path &&
                LastiLine == MainViewModel.Current.CurrentiLine)
            {
                Log.WriteWarning("Network Mapping Window Render was called with the same File and iLine", typeof(NetworkMappingWindow).FullName, MethodBase.GetCurrentMethod().Name);
                return;
            }

            LastFilePath = MainViewModel.Current.Model.File.Path;
            LastiLine = MainViewModel.Current.CurrentiLine;

            if (MainViewModel.Current == null)
                return;

            BuildSourceNetworkMessages(View.RenderCallback, m, lineChanged, MainViewModel.Current.Model.File);
        }

        #region Build Network
        public const string STATUS_TITLE = "Loading...";
        public class BuildNetworkCallbackModel
        {
            public Action<object, bool> Callback { get; set; }
            public object Model { get; set; }
            public bool LineChanged { get; set; }
            public FileData File { get; set; }
        }

        public class BuildNetwork2CallbackModel
        {
            public Action Callback { get; set; }
            public FileData File { get; set; }
        }

        /// <summary>
        /// Builds the Network for the specified file in a background thread
        /// </summary>
        public void BuildSourceNetworkMessages(Action<object, bool> callback, object m, bool lineChanged, FileData file)
        {
            ThreadPool.QueueUserWorkItem(delegate(object state)
            {
                lock (this)
                {
                    Log.WriteWarning("Network Mapping - Entering BuildNetworkAsync", typeof(NetworkMapingViewModel).FullName, MethodBase.GetCurrentMethod().Name);
                    var model = state as BuildNetworkCallbackModel;
                    if (model == null) return;

                    NetworkService.CheckNetworkMessages(model.File, View.StatusUpdate, "Source");

                    model.Callback(model.Model, model.LineChanged);
                    Log.Write("Network Mapping - Exiting BuildNetworkAsync", typeof(NetworkMapingViewModel).FullName, MethodBase.GetCurrentMethod().Name);
                }
            },
            new BuildNetworkCallbackModel
            {
                File = file,
                Callback = callback,
                Model = m,
                LineChanged = false
            });
        }

        /// <summary>
        /// Builds the Network for the specified file in a background thread
        /// </summary>
        public void BuildTargetNetworkMessages(Action callback, FileData file)
        {
            Log.WriteEnter(typeof(NetworkMapingViewModel).FullName, MethodBase.GetCurrentMethod().Name);
            ThreadPool.QueueUserWorkItem(delegate(object m)
            {
                lock (this)
                {
                    Log.WriteWarning("Network Mapping - Entering BuildNetwork2Async", typeof(NetworkMapingViewModel).FullName, MethodBase.GetCurrentMethod().Name);
                    var model = m as BuildNetwork2CallbackModel;
                    if (model == null) return;

                    NetworkService.CheckNetworkMessages(model.File, View.StatusUpdate, "Target");

                    model.Callback();
                    Log.WriteWarning("Network Mapping - Exiting BuildNetwork2Async", typeof(NetworkMapingViewModel).FullName, MethodBase.GetCurrentMethod().Name);
                }
            }, new BuildNetwork2CallbackModel
            {
                File = file,
                Callback = callback
            });

        }
        /// <summary>
        /// Using the current selected LineFS, this method will get the Network Messages that Match
        /// </summary>
        public void GetCurrentLineNetworkMessages(Action<NetworkTargets> callback)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                lock (this)
                {
                    var lineArgs = MainViewModel.Current.CurrentLineArgs;
                    var sourceFile = XmlDal.CacheModel.GetFile(lineArgs.Path);
                    var sourceMessage = sourceFile.GetNetworkMessage(lineArgs.iLine);
                    if (sourceMessage == null)
                    {
                        Log.WriteError("sourceMessage should never be null!", typeof(NetworkMapingViewModel).FullName, MethodBase.GetCurrentMethod().Name);
                        return;
                    }

                    lock (this)
                        lock (sourceMessage.InternalTargets)
                        {
                            if (sourceMessage.IsCached_Targets)
                            {
                                // The TargetsCached is True so we can simple call the method without showing a Status Message - Sara
                                callback(NetworkMapService.GetNetworkMessagesBySourceLine(lineArgs));
                                return;
                            }

                            try
                            {
                                View.StatusUpdate(StatusModel.StartBackground);
                                View.StatusUpdate(StatusModel.StartStopWatch);
                                View.StatusUpdate(StatusModel.Update(STATUS_TITLE,
                                    "Building Current LineFS Network Messages..."));

                                callback(NetworkMapService.GetNetworkMessagesBySourceLine(lineArgs));
                            }
                            finally
                            {
                                View.StatusUpdate(StatusModel.EndBackground);
                                View.StatusUpdate(StatusModel.Completed);
                            }
                        }
                }
            });
        }

        #endregion // Build Network

        public void RenderForced()
        {
            RenderCommon(GetModel(), false, true);
        }

        /// <summary>
        /// Used when the CurrentLineChangedEvent is Triggered
        /// </summary>
        public void RenderByLineChange()
        {
            if (!View.Visible)
                return;

            RenderCommon(GetModel(), true, false);
        }

        public override void RenderDocument()
        {
            Log.WriteEnter(typeof(NetworkMapingViewModel).FullName, MethodBase.GetCurrentMethod().Name);
            if (!View.Visible)
                return;

            RenderCommon(GetModel(), false, false);
        }
        public override NetworkMapCacheData GetModel()
        {
            return NetworkMapService.GetModel();
        }

        public void Add(NetworkMap item)
        {
            var model = NetworkMapService.Add();
            model.Item = item;
            Add(model);
        }
        public bool Add()
        {
            return Add(NetworkMapService.Add());
        }
        private bool Add(NetworkMapModel model)
        {
            var window = new AddEditNetworkMap(this);
            window.Render(model);
            return (window.ShowDialog() == DialogResult.OK);
        }
        public bool Edit(int id)
        {
            var model = NetworkMapService.Edit(id);
            var window = new AddEditNetworkMap(this);
            window.Render(model);
            return (window.ShowDialog() == DialogResult.OK);
        }
        public void Delete(int id)
        {
            var model = NetworkMapService.Delete(id);
            if (View != null)
                View.UpdateView(model);
        }
        public void Save(NetworkMapModel model)
        {
            NetworkMapService.Save(model);
            if (View != null)
                View.UpdateView(model);
        }
        public void GoToLine(int iLine)
        {
            MainViewModel.Current.GoToLine(iLine);
        }

        /// <summary>
        /// Using the NetworkMapService to return a list of files that match
        /// </summary>
        /// <returns></returns>
        public List<NetworkMapFile> GetNetworkMapFiles()
        {
            return NetworkMapService.GetNetworkMapFiles(MainViewModel.Current.CurrentLineArgs);
        }

        /// <summary>
        /// </summary>
        /// <param name="networkMapId">Network Map record identifier</param>
        /// <param name="target"></param>
        /// <param name="noNetworkMessages">When True only File Values are used in the Target</param>
        /// <returns></returns>
        public CriteriaMatchModel GetCriteriaMatches(int networkMapId, LineArgs target, bool noNetworkMessages)
        {
            return NetworkMapService.GetCriteriaMatches(networkMapId,
                MainViewModel.Current.CurrentLineArgs,
                target,
                noNetworkMessages);
        }

        public List<SimpleProperty> GetCurrentLineValues()
        {
            return NetworkMapService.GetCurrentLineValues(MainViewModel.Current.CurrentLineArgs);
        }

        public List<SimpleProperty> GetPropertyValues(string path, int iLine)
        {
            return NetworkMapService.GetPropertyValues(new LineArgs
            {
                Path = path,
                iLine = iLine
            });
        }

        public List<CriteriaMatch> GetRecommendedMatches(LineArgs target)
        {
            return NetworkMapService.GetRecommendedMatches(MainViewModel.Current.CurrentLineArgs, target);
        }

        public void OpenFile(string path)
        {
            MainViewModel.OpenDocument(path);
        }

        public void Copy(int networkMapId)
        {
            NetworkMapService.Copy(networkMapId);
        }

        public NetworkMessageInfo GetSourceMessage()
        {
            return
                MainViewModel.Current.Model.File.GetNetworkMessage(
                    MainViewModel.Current.CurrentiLine);
        }

        public void ResetNetworkLinks()
        {
            FileService.ResetFileNetworkLinks();
        }
    }
}
