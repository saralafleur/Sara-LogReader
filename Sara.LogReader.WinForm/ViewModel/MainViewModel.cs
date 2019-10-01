using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Sara.Common.DateTimeNS;
using Sara.Common.Threading;
using Sara.Logging;
using Sara.LogReader.Model;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.LogReaderNS;
using Sara.LogReader.Service;
using Sara.LogReader.Service.FileServiceNS;
using Sara.LogReader.WinForm.Views;
using Sara.LogReader.WinForm.Views.Document;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.Common;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;
using WeifenLuo.WinFormsUI.Docking;
using File = System.IO.File;

namespace Sara.LogReader.WinForm.ViewModel
{
    /// <summary>
    /// Contains the list of documents and global events
    /// </summary>
    public static class MainViewModel
    {
        #region Properties
        private static SockDrawerViewModel _mSockDrawerViewModel;
        private static PropertyViewModel _mPropertyViewModel;
        private static FilterViewModel _mFilterViewModel;
        private static ResearchViewModel _mResearchViewModel;
        private static SourceInfoViewModel _mSourceInfoViewModel;
        private static PerformanceViewModel _mPerformanceViewModel;
        private static OutputViewModel _mOutputViewModel;
        private static CategoryViewModel _mCategoryViewModel;
        private static HideOptionViewModel _mHideOptionViewModel;
        private static EventViewModel _mEventViewModel;
        private static ValueViewModel _mValueViewModel;
        private static NetworkMapingViewModel _mNetworkMapingViewModel;
        private static NetworkMapViewModel _mNetworkMapViewModel;
        private static DocumentHelperViewModel _mDocumentHelperViewModel;
        private static DocumentMapViewModel _mDocumentMapViewModel;
        private static EventPerformanceTestViewModel _mEventPerformanceTestViewModel;
        private static PatternViewModel _mPatternViewModel;
        private static RecipeReportViewModel _mRecipeReportViewModel;
        private static NetworkReportViewModel _mNetworkReportViewModel;
        internal static int CONST_ViewModelLimit = 1000;
        private static List<DocumentViewModel> Documents { get; set; }

        internal static Color DocumentMapOverlayColor(int iLine)
        {
            return _mDocumentMapViewModel.OverlayColor(iLine);
        }

        internal static void GetLineHighlight(Line lineFs, int iLine)
        {
            lineFs.DocumentMapOverlayBackColor = _mDocumentMapViewModel.OverlayColor(iLine);
            lineFs.PatternOverlayBackColor = _mPatternViewModel.OverlayColor(iLine);
        }

        /// <summary>
        /// When True the layout will not be saved on close
        /// </summary>
        public static bool SkipSaveLayout { get; set; }
        private static DeserializeDockContent _mDeserializeDockContent;
        private static OptionsCacheData Options
        {
            get
            {
                return XmlDal.DataModel.Options;
            }
        }
        private static DocumentViewModel _current;
        public static bool EnabledSupportingViews { get; set; }
        /// <summary>
        /// List of Views that are supporiting a Document.  
        /// When the focused Document changes, each of these views will need to be refreshed.
        /// </summary>
        public static List<IViewModelBaseNonGeneric> DocumentViewModels { get; set; }
        /// <summary>
        /// List of Other Views that are not based on the Document, 
        /// thus do not need to be refreshed when the focused Document changes.
        /// </summary>
        public static List<IViewModelBaseNonGeneric> OtherViewModels { get; set; }
        public static List<IViewModelBaseNonGeneric> AllViewModels { get; set; }
        public static IMainView View { get; set; }
        private static bool _isClosing;
        /// <summary>
        /// True when the Application is Closing
        /// </summary>
        public static bool IsClosing
        {
            get { return _isClosing; }
            set
            {
                try
                {
                    _isClosing = value;

                    foreach (var view in AllViewModels)
                        if (view != null) view.IsClosing = value;

                    foreach (var documentViewModel in Documents)
                        documentViewModel.IsClosing = value;
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch
                {
                    // Do nothing since we are closing - Sara
                }
            }
        }
        /// <summary>
        /// Set to True when the Startup Method is complete.
        /// </summary>
        public static bool StartupComplete { get; set; }
        public static int MainUIThreadId { get; set; }
        private static IgnoreBacklogQueue<DocumentViewModel> _queue { get; set; }
        #endregion Properties

        #region Document Navigation
        private static object _syncOpenDocument = new object();
        /// <summary>
        /// STARTING POINT for Open Document
        /// 
        /// Order of Operations
        /// 1.) MainViewModel.OnOpenDocument
        /// 2.) DocumentWindow.OpenFile
        /// 3.) MainViewModel.AddDocument
        /// </summary>
        public static void OpenDocument(string path, Action callback = null)
        {
            #region InvokeRequired
            if (View.InvokeRequired)
            {
                View.Invoke(new Action<string, Action>(OpenDocument), path, callback);
                return;
            }
            #endregion

            PerformanceService.StartEvent($"{PerformanceService.CONST_OpenDocument}{path}");
            Log.WriteEnter(typeof(MainViewModel).FullName, MethodBase.GetCurrentMethod().Name);

            lock (_syncOpenDocument)
            {
                #region Check if already open
                if (!File.Exists(path))
                {
                    MessageBox.Show(string.Format("They system cannot find the file specified: \"{0}\"", path));
                    return;
                }
                var doc = FindDocument(path) as DocumentWindow;

                if (doc != null)
                {
                    doc.DockHandler.Show();

                    var model = doc as DocumentWindow;
                    if (model != null)
                        SetCurrent(model.ViewModel);

                    callback?.Invoke();
                    return;
                }
                #endregion

                #region Create DocumentWindow
                doc = new DocumentWindow { Text = Path.GetFileName(path) };
                try
                {
                    var model = new DocumentModel
                    {
                        Control = doc.PrepareDocument(path),
                        File = XmlDal.CacheModel.GetFile(path)
                    };

                    doc.ViewModel.View.StatusUpdate(StatusModel.Update("Loading"));
                    doc.ViewModel.Model = model;
                    model.File.Options = Options;
                    model.File.DocumentModel = model;
                    Documents.Add(doc.ViewModel);
                    DocumentService.BuildDocument(model);
                }
                catch (Exception exception) { doc.Close(); MessageBox.Show(exception.Message); }
                #endregion

                #region Show
                if (View.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                {
                    doc.MdiParent = View as Form;
                    doc.Show();
                }
                else
                    doc.Show(View.DockPanel);
                #endregion

                if (StartupComplete)
                    SetCurrent(doc.ViewModel);

                callback?.Invoke();

                PerformanceService.StopEvent($"{PerformanceService.CONST_OpenDocument}{path}");
                Log.WriteExit(typeof(MainViewModel).FullName, MethodBase.GetCurrentMethod().Name);
            }
        }
        /// <summary>
        /// Current Document that has focus.
        /// Note: Supporting Views only act on the Current Document.
        /// </summary>
        public static DocumentViewModel Current
        {
            get
            {
                return _current;
            }
        }

        /// <summary>
        /// Returns the Source Type for the current (Focused) document
        /// </summary>
        public static string SourceType
        {
            get
            {
                return Current == null ? "null" : Current.Model.File.SourceType;
            }
        }
        public static Font DocumentFont { get; set; }
        public static Action<Font> DocumentFontChanged { get; internal set; }

        /// <summary>
        /// Current is only set by the following Events
        /// 1.) MainViewModel.Startup Method
        /// 2.) OpenDocument
        /// 3.) DocumentWindow.Enter
        /// 4.) When all documents are closed
        /// </summary>
        private static void QueueCurrent(DocumentViewModel value)
        {
            var complete = true;
            try
            {
                var sw = new Stopwatch(MethodBase.GetCurrentMethod().Name);
                if (_current == value)
                    return;

                if (_current != null &&
                    _current.Model.File.DataController.InvalidateNotificationEvent != null)
                {
                    foreach (var viewModel in DocumentViewModels)
                    {
                        if (viewModel.GetType().Name == "DocumentViewModel") continue;
                        _current.Model.File.DataController.InvalidateNotificationEvent -= viewModel.RenderDocument;
                    }
                    _current.Model.File.DataController.InvalidateNotificationEvent -= _current.RenderDocument;
                }

                _current = value;

                if (_current == null)
                    return;

                OutputService.Log($"SET Current for {value.Model.File.Path}");

                foreach (var viewModel in DocumentViewModels)
                {
                    if (viewModel.GetType().Name == "DocumentViewModel") continue;
                    _current.Model.File.DataController.InvalidateNotificationEvent += viewModel.RenderDocument;
                }
                _current.Model.File.DataController.InvalidateNotificationEvent += _current.RenderDocument;

                if (!StartupComplete)
                    return;

                // We only Render the Document if 'IsCached' is false OR the first time we render the Document
                // Otherwise we are just setting focus to this document - Sara
                if ((!_current.IsCached || !_current.FirstRenderComplete) & !_current.IsRendering)
                {
                    _current.RenderDocument();
                    complete = false;
                }

                RenderViews();

                PrepareHideOptions();

                sw.Stop();
            }
            finally
            {
                if (complete)
                    _queue.ItemComplete();
            }
        }

        public static void PrepareHideOptions()
        {
            View.PrepareHideOptions();
        }

        internal static void DocumentRenderStarted()
        {
            // The current Document is being updated, so go ahead and tag the Document Map as 'Loading'
            // TODO: I need to add this back in and on DocumentRenderComplete, clear it - Sara
            // _mDocumentMapViewModel.StatusUpdate(StatusModel.Update("Loading"));
        }
        public static void DocumentRenderComlete()
        {
            _queue.ItemComplete();
        }
        public static void SetCurrent(DocumentViewModel value)
        {
            if (value == null) return;
            OutputService.Log($"SET Current request for {value.Model.File.Path}");
            _queue.Add(value);
        }
        /// <summary>
        /// VALUE OR EVENT Changed - Update Open Files
        /// </summary>
        public static void ValueOrEventChangedUpdateOpenFiles()
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                Thread.CurrentThread.Name = "RefreshOpenFiles";
                Parallel.ForEach(Documents.Select(n => n.Model.File), file =>
                {
                    if (file == null)
                    {
                        Log.WriteError("state should be of type DocumentViewModel", typeof(MainViewModel).FullName, MethodBase.GetCurrentMethod().Name);
                        return;
                    }
                    file.DataController.Invalidate();
                });
                StatusUpdate(StatusModel.Completed);
            });
        }
        /// <summary>
        /// CHECK ACTIVE DOCUMENT
        /// </summary>
        public static bool CheckCurrent(DocumentViewModel viewModel)
        {
            if (!StartupComplete)
                return false;

            if (IsClosing)
                return false;

            if (Current == viewModel)
                return false;

            SetCurrent(viewModel);
            return true;
        }
        /// <summary>
        /// FIND DOCUMENT CONTROL
        /// </summary>
        private static IDockContent FindDocument(string path)
        {
            // ReSharper disable LoopCanBeConvertedToQuery
            if (View.DockPanel.DocumentStyle == DocumentStyle.DockingMdi)
            {
                foreach (var mdiChild in View.MdiChildren)
                {
                    var model = mdiChild as DocumentWindow;
                    if (model == null) continue;
                    if (model.FileName == path) return mdiChild as IDockContent;
                }
            }
            else
            {
                foreach (var dockContent in View.DockPanel.Documents)
                {
                    var model = dockContent as DocumentWindow;
                    if (model == null) continue;
                    if (model.FileName == path) return dockContent;
                }
            }
            return null;
            // ReSharper restore LoopCanBeConvertedToQuery
        }
        #endregion Document Navigation

        #region Startup and Shutdown
        static MainViewModel()
        {
            Documents = new List<DocumentViewModel>();
            DocumentViewModels = new List<IViewModelBaseNonGeneric>();
            OtherViewModels = new List<IViewModelBaseNonGeneric>();
            AllViewModels = new List<IViewModelBaseNonGeneric>();
            _queue = new IgnoreBacklogQueue<DocumentViewModel>("RenderDocument Queue");
            _queue.ProcessItemEvent += QueueCurrent;
            // TODO: Remove this when I'm done testing - Sara
            EnabledSupportingViews = true;
        }

        public static void StartUp(MainForm view, string fileOnStartupPath)
        {
            StopWatchOutput.Log = OutputService.Log;
            ReleaseOutput.Log = OutputService.Log;
            View = view;

            Log.WriteTrace("Starting Sara.LogReader", typeof(MainViewModel).Name, MethodBase.GetCurrentMethod().Name);

            #region Loading Data
            PerformanceService.StartEvent(PerformanceService.CONST_LoadingDataFile);
            View.StatusUpdate(StatusModel.StartCountdown(PerformanceService.GetLastDuration("Loading DataFile")));
            View.StatusUpdate(StatusModel.Update("Starting Sara.LogReader", "Loading Cache"));

            try
            {
                MainService.LoadDataFile();
            }
            catch (Exception ex)
            {
                View.ShowError(ex.Message);
            }
            PerformanceService.StopEvent(PerformanceService.CONST_LoadingDataFile);

            View.StatusUpdate(StatusModel.StopCountdown);

            #endregion

            #region Loading Layout
            PerformanceService.StartEvent(PerformanceService.CONST_LoadingLayout);
            View.StatusUpdate(StatusModel.Update("Starting Sara.LogReader", "Initializing Controls"));
            StartupCreateWindows();
            View.StatusUpdate(StatusModel.StartStopWatch);
            View.StatusUpdate(StatusModel.Update(null, "Loading Layout"));
            _mDeserializeDockContent = StartupGetContentFromPersistString;
            var configFile = DataPath.GetConfigFilePath();

            try
            {
                if (File.Exists(configFile))
                    View.LoadLayout(configFile, _mDeserializeDockContent);
            }
            catch (Exception)
            {
                var msg =
                    $"\"{configFile}\" has become corrupted.  \nTo resolve the system will automatically reset the file to default settings.";
                Log.WriteTrace(msg, typeof(MainForm).FullName, MethodBase.GetCurrentMethod().Name);
                MessageBox.Show(msg, "System Error");
                File.Delete(configFile);
            }

            Log.WriteTrace($"Main UI Thread Id: {MainUIThreadId}", typeof(MainForm).FullName, MethodBase.GetCurrentMethod().Name);

            View.StatusUpdate(StatusModel.StartStopWatch);
            PerformanceService.StopEvent(PerformanceService.CONST_LoadingLayout);
            #endregion

            PerformanceService.StartEvent(PerformanceService.CONST_LoadingOther);
            View.StatusUpdate(StatusModel.Update(null, "Rendering..."));
            CurrentLineChangedEvent += View.ActiveDocumentLineChanged;

            view.Render(null);

            #region Options
            View.SetOptions(Options);
            ApplyHideOptions(false);
            #endregion Options

            Application.DoEvents();

            StartupOpenFile(fileOnStartupPath);

            // Note: The majority of the cpu cycles will occur on the MainUI thread.
            // This is why we are not running this in Parallel or on it's own Thread.
            // This means that if you have 10 documents to open and each takes 2 seconds your will be waiting for 10+ seconds. - Sara
            if (Options.RestoreOpenDocuments)
            {
                View.StatusUpdate(StatusModel.Update("Restoring Open Documents..."));
                var i = 0;
                var count = Options.RestoreOpenDocumentsList.Count;
                foreach (var path in Options.RestoreOpenDocumentsList)
                {
                    i++;
                    OpenDocument(path);
                    View.StatusUpdate(StatusModel.UpdateDetail($"{i} of {count}"));
                }
            }

            ReadyViews();

            // Render the Filter window any time a Category changes - Sara
            XmlDal.DataModel.CategoryCacheDataController.InvalidateNotificationEvent += _mFilterViewModel.RenderDocument;

            StartupComplete = true;

            // Render Focused Document
            var _current = Documents.FirstOrDefault(model => model.View.DockHandler.IsActiveContentHandler);
            if (_current != null) SetCurrent(_current);

            DocumentFont = ColorService.ColorScheme.Current.GeneralFontObject;

            View.StatusUpdate(StatusModel.Completed);
            PerformanceService.StopEvent(PerformanceService.CONST_LoadingOther);
        }
        #region Startup Private Methods
        private static void StartupCreateWindows()
        {
            if (View.InvokeRequired)
                throw new Exception("You must be on the MainUI Thread!");

            var sw = new Stopwatch("StartupCreateWindows");

            _mOutputViewModel = new OutputViewModel();
            _mFilterViewModel = new FilterViewModel();
            _mSourceInfoViewModel = new SourceInfoViewModel();
            _mDocumentHelperViewModel = new DocumentHelperViewModel();
            _mDocumentMapViewModel = new DocumentMapViewModel();
            _mEventPerformanceTestViewModel = new EventPerformanceTestViewModel();
            _mPropertyViewModel = new PropertyViewModel();
            _mNetworkMapingViewModel = new NetworkMapingViewModel();
            _mNetworkMapViewModel = new NetworkMapViewModel();
            _mSockDrawerViewModel = new SockDrawerViewModel();
            _mHideOptionViewModel = new HideOptionViewModel();
            _mCategoryViewModel = new CategoryViewModel();
            _mValueViewModel = new ValueViewModel();
            _mEventViewModel = new EventViewModel();
            _mPatternViewModel = new PatternViewModel();
            _mRecipeReportViewModel = new RecipeReportViewModel();
            _mNetworkReportViewModel = new NetworkReportViewModel();
            _mResearchViewModel = new ResearchViewModel();
            _mPerformanceViewModel = new PerformanceViewModel();

            DocumentViewModels.Add(_mFilterViewModel);
            DocumentViewModels.Add(_mSourceInfoViewModel);
            DocumentViewModels.Add(_mDocumentHelperViewModel);
            DocumentViewModels.Add(_mDocumentMapViewModel);
            DocumentViewModels.Add(_mEventPerformanceTestViewModel);
            DocumentViewModels.Add(_mPropertyViewModel);
            DocumentViewModels.Add(_mNetworkMapingViewModel);
            DocumentViewModels.Add(_mNetworkMapViewModel);

            // Other Views are supporing views
            OtherViewModels.Add(_mOutputViewModel);
            OtherViewModels.Add(_mSockDrawerViewModel);
            OtherViewModels.Add(_mCategoryViewModel);
            OtherViewModels.Add(_mHideOptionViewModel);
            OtherViewModels.Add(_mValueViewModel);
            OtherViewModels.Add(_mEventViewModel);
            OtherViewModels.Add(_mPatternViewModel);
            OtherViewModels.Add(_mRecipeReportViewModel);
            OtherViewModels.Add(_mNetworkReportViewModel);
            OtherViewModels.Add(_mResearchViewModel);
            OtherViewModels.Add(_mPerformanceViewModel);

            AllViewModels.AddRange(DocumentViewModels);
            AllViewModels.AddRange(OtherViewModels);
        }
        private static void StartupOpenFile(string fileOnStartupPath)
        {
            if (!File.Exists(fileOnStartupPath)) return;

            if (FindDocument(fileOnStartupPath) != null) return;

            // If the application was called with an OpenWidth, then open the file passed in. - Sara
            if (File.Exists(fileOnStartupPath))
            {
                OpenDocument(fileOnStartupPath);
            }
        }
        private static IDockContent StartupGetContentFromPersistString(string persistString)
        {
            foreach (var viewModel in AllViewModels)
            {
                if (persistString == viewModel.DockView.GetType().ToString())
                    return viewModel.DockView;
            }

            // At this point we are assuming that we have handled all window types but a Document.
            // To remove this ability I am simple going to return here and leave the code in place in case we need it later - Sara
            return null;

            // DummyDoc overrides GetPersistString to add extra information into persistString.
            // Any DockContent may override this value to add any needed information for deserialization.

            //string[] parsedStrings = persistString.Split(new[] { ',' });
            //if (parsedStrings.Length != 3)
            //    return null;

            //if (parsedStrings[0] != typeof(DocumentWindow).ToString())
            //    return null;

            //// Validate the file exists
            //if (!File.Exists(parsedStrings[1]))
            //    return null;

            //var doc = new DocumentWindow();
            //if (parsedStrings[1] != string.Empty)
            //    doc.FileName = parsedStrings[1];
            //if (parsedStrings[2] != string.Empty)
            //    doc.Text = parsedStrings[2];

            //return doc;
        }
        #endregion 
        public static void Shutdown()
        {
            View.StatusUpdate(StatusModel.StartStopWatch);
            View.StatusUpdate(StatusModel.Update("Closing"));

            // ReSharper disable AssignNullToNotNullAttribute
            var configFile = DataPath.GetConfigFilePath();
            // ReSharper restore AssignNullToNotNullAttribute

            var path = Path.GetDirectoryName(configFile);
            if (path != null)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            else
            {
                throw new Exception("DockPanel.config path is null");
            }

            if (SkipSaveLayout)
            {
                if (File.Exists(configFile))
                    File.Delete(configFile);
            }
            else
                View.DockPanel.SaveAsXml(configFile);

            SaveRestoreOpenDocuments();

            XmlDal.Save();
        }
        #region Shutdown Private Methods
        private static void SaveRestoreOpenDocuments()
        {
            var options = XmlDal.DataModel.Options;
            options.RestoreOpenDocumentsList.Clear();
            foreach (var item in Documents)
            {
                options.RestoreOpenDocumentsList.Add(item.Model.File.Path);
            }
        }
        #endregion
        internal static void Exit()
        {
            Sara.Common.Debug.Shutdown.Set();
            _mDocumentMapViewModel.Exit();
            OutputService.Exit();
            Log.Exit(new TimeSpan(0, 0, 30));
            _queue.Exit();
        }
        /// <summary>
        /// True when the system is shutting down
        /// </summary>
        #endregion Startup and Shutdown

        #region Events
        public static event Action InvalidateDocumentMapCacheEvent;
        /// <summary>
        /// Call this method when you want to invalidate the Cache data and force a reload
        /// </summary>
        public static void InvalidateDocumentMapCache()
        {
            InvalidateDocumentMapCacheEvent?.Invoke();
        }
        /// <summary>
        /// Occurs when the current lineFs of the Active Document changed
        /// </summary>
        public static event Action CurrentLineChangedEvent;
        /// <summary>
        /// Called when the current lineFs of the Active document changes
        /// </summary>
        public static void CurrentLineChanged()
        {
            if (IsClosing)
                return;

            ThreadPool.QueueUserWorkItem(m =>
            {
                CurrentLineChangedEvent();
            });
        }
        private static void ReadyViews()
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                if (IsClosing)
                    return;

                // DocumentViews will be Rendered when you open a file
                foreach (var viewModel in OtherViewModels)
                {
                    viewModel.RenderDocument();
                }
            });
        }
        #endregion Events

        #region Close Document
        public static void CloseAllDocuments()
        {
            if (View.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (var form in View.MdiChildren)
                    form.Close();
            }
            else
            {
                foreach (var document in View.DockPanel.DocumentsToArray())
                {
                    document.DockHandler.Close();
                }
            }
            CheckAllClosed();
        }
        public static void CloseAllButThisOne()
        {
            if (View.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                var activeMdi = View.ActiveMdiChild;
                foreach (var form in View.MdiChildren)
                {
                    if (form != activeMdi)
                        form.Close();
                }
            }
            else
            {
                foreach (var document in View.DockPanel.DocumentsToArray())
                {
                    if (!document.DockHandler.IsActivated)
                        document.DockHandler.Close();
                }
            }
            CheckAllClosed();
        }
        public static void CloseDocument()
        {
            if (View.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                if (View.ActiveMdiChild != null)
                    View.ActiveMdiChild.Close();
            }
            else if (View.DockPanel.ActiveDocument != null)
                View.DockPanel.ActiveDocument.DockHandler.Close();
            CheckAllClosed();
        }
        private static void CheckAllClosed()
        {
            if (Documents.Count > 0) return;

            SetCurrent(null);
            View.UpdateCurrentLine(string.Empty);
        }
        /// <summary>
        /// CLOSE DOCUMENT
        /// </summary>
        public static void CloseDocument(DocumentViewModel viewModel)
        {
            Documents.Remove(viewModel);
            CheckAllClosed();
        }
        #endregion Close Document

        #region Render
        public static void RenderDocumentMap()
        {
            _mDocumentMapViewModel.RenderDocument();
        }
        private static void RenderViews()
        {
            Log.WriteEnter(typeof(MainViewModel).FullName, MethodBase.GetCurrentMethod().Name);

            if (!EnabledSupportingViews)
                return;

            if (IsClosing)
                return;

            ThreadPool.QueueUserWorkItem(delegate
            {
                foreach (var viewModel in DocumentViewModels)
                {
                    if (viewModel.GetType().Name == "DocumentViewModel") continue;
                    viewModel.RenderDocument();
                }
            });
        }
        public static void RenderDocuments()
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                foreach (var documentViewModel in Documents)
                {
                    documentViewModel.RenderDocument();
                }
            });
        }
        #endregion Render

        #region Hide Options
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Options"></param>
        /// <param name="runInBackground">
        /// When we start the application, we don't want to run this method in a background thread! - Sara
        /// </param>
        public static void ApplyHideOptions(bool runInBackground = true)
        {
            var method = new WaitCallback(delegate
            {
                Log.WriteEnter(typeof(MainViewModel).FullName, MethodBase.GetCurrentMethod().Name);
                try
                {
                    View.SetCursor(Cursors.WaitCursor);
                    View.StatusUpdate(StatusModel.Update("Updating Hide Options"));
                    foreach (var item in Documents)
                    {
                        View.StatusUpdate(StatusModel.Update(null, item.Model.File.Path));
                        item.SetHideOptions(Options);
                    }
                    // We use to wait till each document was done, I took out this code.  Don't know why it was here - Sara
                }
                finally
                {
                    View.SetCursor(Cursors.Default);
                    View.StatusUpdate(StatusModel.Completed);
                }
            });

            if (runInBackground)
            {
                ThreadPool.QueueUserWorkItem(method);
                return;
            }

            method(null);
        }
        #endregion

        #region Show Window
        public static void ShowValueWindow()
        {
            _mValueViewModel.RenderView(View.DockPanel);
        }
        public static void ShowDocumentMapWindow()
        {
            _mDocumentMapViewModel.RenderView(View.DockPanel);
        }
        public static void ShowDocumentHelperWindow()
        {
            _mDocumentHelperViewModel.RenderView(View.DockPanel);
        }
        public static void ShowEventWindow()
        {
            _mEventViewModel.RenderView(View.DockPanel);
        }
        public static void ShowCategoryWindow()
        {
            _mCategoryViewModel.RenderView(View.DockPanel);
        }
        public static void ShowHideOptionWindow()
        {
            _mHideOptionViewModel.RenderView(View.DockPanel);
        }
        public static void ShowSockDrawerWindow()
        {
            _mSockDrawerViewModel.RenderView(View.DockPanel);
        }
        public static void ShowPropertyWindow()
        {
            _mPropertyViewModel.RenderView(View.DockPanel);
        }
        public static void ShowToolBoxWindow()
        {
            _mFilterViewModel.RenderView(View.DockPanel);
        }
        public static void ShowResearchWindow()
        {
            _mResearchViewModel.RenderView(View.DockPanel);
        }
        public static void ShowSourceInfoWindow()
        {
            _mSourceInfoViewModel.RenderView(View.DockPanel);
        }
        public static void ShowPerformanceWindow()
        {
            _mPerformanceViewModel.RenderView(View.DockPanel);
        }
        public static void ShowOutputWindow()
        {
            _mOutputViewModel.RenderView(View.DockPanel);
        }
        public static void ShowPatternWindow()
        {
            _mPatternViewModel.RenderView(View.DockPanel);
        }
        public static void ShowRecipeReportView()
        {
            _mRecipeReportViewModel.RenderView(View.DockPanel);
        }
        public static void ShowNetworkReportView()
        {
            _mNetworkReportViewModel.RenderView(View.DockPanel);
        }
        public static void ShowNetworkMapingView()
        {
            _mNetworkMapingViewModel.RenderView(View.DockPanel);
        }
        public static void ShowNetworkMapView()
        {
            _mNetworkMapViewModel.RenderView(View.DockPanel);
        }
        public static void ShowEventPerformanceTest()
        {
            _mEventPerformanceTestViewModel.RenderView(View.DockPanel);
        }

        #endregion Show Window

        #region GoTo
        public static void GoToValueId(int valueId)
        {
            _mValueViewModel.GoToValueId(valueId);
        }
        public static void GoToEventId(int eventId)
        {
            _mEventViewModel.GoToEventId(eventId);
        }
        public static void GoToFile(string path, Action callback = null)
        {
            OpenDocument(path, callback);
        }
        public static void GoToLine(int iLine)
        {
            if (Current == null)
                return;
            Current.GoToLine(iLine);
        }
        #endregion GoTo

        #region EventPattern Helpers
        public static void DeleteEvent(int eventId)
        {
            _mEventViewModel.Delete(eventId);
        }
        public static void EditEvent(int eventId)
        {
            _mEventViewModel.Edit(eventId);
        }
        public static void AddEvent(EventLR e)
        {
            _mEventViewModel.Add(e);
        }
        #endregion EventPattern Helpers

        #region Tools
        internal static void RunScriptsOnAll()
        {
            _mPatternViewModel.RunScriptsOnAll();
        }
        internal static void RunScripts()
        {
            _mPatternViewModel.RunScripts();
        }
        public static void ClearCache()
        {
            var sw = new Stopwatch("ClearCache");
            View.StatusUpdate(StatusModel.Update("Clearing Cache"));
            XmlDal.CacheModel.Clear();
            View.StatusUpdate(StatusModel.Completed);
            sw.Stop();

        }
        /// <summary>
        /// Used to quickly rebuild the active documents cache.  Used to speed up development and exercise code
        /// </summary>
        public static void RebuildActiveDocument()
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                OutputService.ClearOutputWindow?.Invoke();
                PerformanceService.StartEvent($"Rebuild Active Document for {Current.Model.File.Path}");
                Thread.CurrentThread.Name = "Rebuild.Active.Document";

                var file = Current.Model.File;
                file.Clear();
                FileService.Build(file, true);
                file.IsCached_Lazy_NetworkMessages = false;
                NetworkService.CheckNetworkMessages(file, View.StatusUpdate, "Source");

                // Invalidate will force dependent Components to Render
                file.DataController.Invalidate();
            });
        }
        public static void ResetFileNetworkData()
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                FileService.ResetFileNetworkData();
                if (Current != null)
                    Current.Model.File.DataController.InvalidatePart();
                View.StatusUpdate(StatusModel.FadeOutUpdate(@"Reset Complete"));
            });
        }
        public static void AnalysizeDocument()
        {
            MessageBox.Show(Current.AnalysizePercentDocumentated());
        }
        internal static void AddFavorite(string filePath)
        {
            var FavoriteFiles = XmlDal.CacheModel.Favorites.Files;

            FavoriteFiles.Add(new FavoriteFile() { FavoriteGroup = "Default", Path = filePath });

            _mSockDrawerViewModel.RenderTreeView();
        }
        internal static void AddFavorite()
        {
            if (Current == null)
            {
                MessageBox.Show("You must first open a Document before you can add it to favorites", "Warning");
                return;
            }
            AddFavorite(Current.Model.File.Path);
        }
        internal static void LocateDocumentInSockDrawer()
        {
            if (Current == null)
            {
                MessageBox.Show("You must first open a Document before you can locate in in the Sock Drawer", "Warning");
                return;
            }
            _mSockDrawerViewModel.SetFocus(Current.Model.File.Path);
        }
        #endregion Tools

        public static void StatusUpdate(IStatusModel status)
        {
            View.StatusUpdate(status);
        }
    }
}
