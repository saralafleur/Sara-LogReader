using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Drawing;
using System.Linq;
using Sara.LogReader.Common;
using Sara.LogReader.Model;
using Sara.LogReader.Model.ColorScheme;
using Sara.LogReader.Model.LogReaderNS;
using Sara.LogReader.Service;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm;
using Sara.WinForm.ColorScheme;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.ControlsNS;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views
{
    public sealed partial class MainForm : Form, IMainView, IView<object, object>, IColorSchemeControl
    {
        #region Properties
        public DockPanel DockPanel { get { return dockPanel; } }
        /// <summary>
        /// Used to open a file on start.  The OS will pass in a File Path when you double click on a log file that is associated with the application.
        /// </summary>
        private readonly string _openFileOnStartupPath;
        public bool StartupReady { get { return MainViewModel.StartupComplete; } }
        #endregion

        #region Setup
        public MainForm(string openFileOnStartupPath)
        {
            MainViewModel.MainUIThreadId = Thread.CurrentThread.ManagedThreadId;
            _openFileOnStartupPath = openFileOnStartupPath;

            InitializeComponent();

            ColorService.ColorSchemeType = typeof(ColorSchemeLogReaderModal);
            ColorService.Setup(this);

            // Handles that position and state of the window - Sara
            // ReSharper disable ObjectCreationAsStatement
            new PersistWindowState { Parent = this, RegistryPath = @"Software\Sara\Sara.LogReader" };
            // ReSharper restore ObjectCreationAsStatement
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            DataPath.ExecutablePath = Application.ExecutablePath;
            mainMenu.Enabled = false;
            mainMenu.Visible = false;

            Text = string.Format("Sara.LogReader {0}", Application.ProductVersion);
            PerformanceService.StartEvent(PerformanceService.CONST_Startup);
            MainViewModel.StartUp(this, _openFileOnStartupPath);
            PerformanceService.StopEvent(PerformanceService.CONST_Startup);
        }
        #endregion

        #region Render
        public void Render(object model)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object>(Render), model);
                return;
            }

            mainMenu.Enabled = true;
        }
        public void UpdateView(object selectedModel)
        {
            throw new NotImplementedException();
        }
        public void LoadLayout(string configFile, DeserializeDockContent mDeserializeDockContent)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, DeserializeDockContent>(LoadLayout), configFile, mDeserializeDockContent);
                return;
            }

            DockPanel.LoadFromXml(configFile, mDeserializeDockContent);
        }
        #endregion

        #region Methods
        public void SetOptions(OptionsCacheData options)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<OptionsCacheData>(SetOptions), options);
                return;
            }
            mainMenu.Visible = true;
            mainMenu.SendToBack();
            CurrentLineBar.Visible = MenuItemCurrentLineBar.Checked = options.ShowCurrentLineBar;
            MenuItemDocumentMapLineNumber.Checked = options.ShowDocumentMapLineNumber;
            MenuItemDocumentFolding.Checked = options.DocumentFolding;
            menuItemSourceInfo.Checked = options.DocumentSourceInfo;
            tsmiRestoreOpenedDocuments.Checked = options.RestoreOpenDocuments;
        }
        public void ShowError(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(ShowError), message);
                return;
            }

            ErrorPanel.ShowError(this, message, null);
        }
        public void SetCursor(Cursor cursor)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Cursor>(SetCursor), cursor);
                return;
            }

            Cursor = cursor;
        }
        #endregion

        #region Events
        public void ActiveDocumentChanged()
        {
        }
        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void menuItemSolutionExplorer_Click(object sender, EventArgs e)
        {
            MainViewModel.ShowSockDrawerWindow();
        }
        private void menuItemPropertyWindow_Click(object sender, EventArgs e)
        {
            MainViewModel.ShowPropertyWindow();
        }
        private void menuItemToolbox_Click(object sender, EventArgs e)
        {
            MainViewModel.ShowToolBoxWindow();
        }
        private void menuItemOutputWindow_Click(object sender, EventArgs e)
        {
            MainViewModel.ShowOutputWindow();
        }
        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            var aboutDialog = new AboutDialog();
            aboutDialog.ShowDialog(this);
        }
        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            var openFile = new OpenFileDialog
            {
                InitialDirectory = Application.ExecutablePath,
                Filter = @"All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (openFile.ShowDialog() != DialogResult.OK) return;

            MainViewModel.OpenDocument(openFile.FileName);
        }
        private void menuItemFile_Popup(object sender, EventArgs e)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                menuItemClose.Enabled =
                    menuItemCloseAll.Enabled =
                    menuItemCloseAllButThisOne.Enabled = (ActiveMdiChild != null);
            }
            else
            {
                menuItemClose.Enabled = (dockPanel.ActiveDocument != null);
                menuItemCloseAll.Enabled =
                    menuItemCloseAllButThisOne.Enabled = (dockPanel.DocumentsCount > 0);
            }
        }
        private void menuItemClose_Click(object sender, EventArgs e)
        {
            MainViewModel.CloseDocument();
        }
        private void menuItemCloseAll_Click(object sender, EventArgs e)
        {
            MainViewModel.CloseAllDocuments();
        }
        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
            MainViewModel.Shutdown();
        }
        private void menuItemToolBar_Click(object sender, EventArgs e)
        {
        }
        private void menuItemCurrentLineBar_Click(object sender, EventArgs e)
        {
            CurrentLineBar.Visible = XmlDal.DataModel.Options.ShowCurrentLineBar = MenuItemCurrentLineBar.Checked = !MenuItemCurrentLineBar.Checked;
        }
        private void MenuItemDocumentMapLineNumber_Click(object sender, EventArgs e)
        {
            var options = XmlDal.DataModel.Options;
            options.ShowDocumentMapLineNumber = MenuItemDocumentMapLineNumber.Checked = !MenuItemDocumentMapLineNumber.Checked;
            MainViewModel.RenderDocumentMap();
        }
        private void menuItemTools_Popup(object sender, EventArgs e)
        {
            menuItemLockLayout.Checked = !dockPanel.AllowEndUserDocking;
        }
        private void menuItemLockLayout_Click(object sender, EventArgs e)
        {
            dockPanel.AllowEndUserDocking = !dockPanel.AllowEndUserDocking;
        }
        private void menuItemCloseAllButThisOne_Click(object sender, EventArgs e)
        {
            MainViewModel.CloseAllButThisOne();
        }
        private void exitWithoutSavingLayout_Click(object sender, EventArgs e)
        {
            MainViewModel.SkipSaveLayout = true;
            Close();
        }
        private void analysizeDocumentatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewModel.AnalysizeDocument();
        }
        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            MainViewModel.ShowCategoryWindow();
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            MainViewModel.ShowEventWindow();
        }
        private void documentHelperWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewModel.ShowDocumentHelperWindow();
        }
        private void collaspeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewModel.Current.CollaspeAll();
        }
        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewModel.Current.ExpandAll();
        }
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            MainViewModel.ShowDocumentMapWindow();
        }
        public void StatusUpdate(IStatusModel model)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<IStatusModel>(StatusUpdate), model);
                return;
            }

            StatusPanel.StatusUpdate(model);
        }
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            MainViewModel.ShowValueWindow();
        }
        public void ActiveDocumentLineChanged()
        {
            if (MainViewModel.Current == null)
            {
                UpdateCurrentLine(string.Empty);
                return;
            }

            var line = MainViewModel.Current.CurrentLine;
            UpdateCurrentLine(line);
        }
        public void UpdateCurrentLine(string line)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateCurrentLine), line);
                return;
            }

            tbCurrentLine.Text = line;
        }
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            MainViewModel.ClearCache();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainViewModel.IsClosing = true;
        }
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            MainViewModel.ShowPatternWindow();
        }
        private void toolStripMenuItem9_Click_1(object sender, EventArgs e)
        {
            MainViewModel.ShowRecipeReportView();
        }
        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            MainViewModel.RebuildActiveDocument();
        }
        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            MainViewModel.ShowNetworkMapingView();
        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsViewModel.ShowSettings();
        }
        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            StatusUpdate(StatusModel.Update(@"Reseting Network", ""));
            MainViewModel.ResetFileNetworkData();
        }
        private void networkReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewModel.ShowNetworkReportView();
        }
        private void followNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainViewModel.Current == null)
                return;

            MainViewModel.Current.FollowNetworkMessage();
        }
        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            MainViewModel.ShowNetworkMapView();
        }
        private void MenuItemDocumentFolding_Click(object sender, EventArgs e)
        {
            var options = XmlDal.DataModel.Options;
            options.DocumentFolding = MenuItemDocumentFolding.Checked = !MenuItemDocumentFolding.Checked;
            MainViewModel.RenderDocuments();
        }
        private void toolStripMenuItem15_Click_1(object sender, EventArgs e)
        {
            MainViewModel.ShowEventPerformanceTest();
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainViewModel.Exit();
        }
        private void btnCopyCurrentLine_Click(object sender, EventArgs e)
        {
            if (tbCurrentLine.Text == string.Empty) return;
            Clipboard.SetText(tbCurrentLine.Text);
        }
        private void addToFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewModel.AddFavorite();
        }
        private void locateDocumentInSockDrawerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainViewModel.LocateDocumentInSockDrawer();
        }
        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            MainViewModel.ShowResearchWindow();
        }
        private void menuItemSourceInfo_Click(object sender, EventArgs e)
        {
            MainViewModel.ShowSourceInfoWindow();
        }
        private void menuItemDocumentSourceInfo_Click(object sender, EventArgs e)
        {
            var options = XmlDal.DataModel.Options;
            options.DocumentSourceInfo = menuItemDocumentSourceInfo.Checked = !menuItemDocumentSourceInfo.Checked;
            MainViewModel.RenderDocuments();
        }
        private void toolStripMenuItem16_Click_1(object sender, EventArgs e)
        {
            MainViewModel.ShowPerformanceWindow();
        }
        private void tsmiRestoreOpenedDocuments_Click(object sender, EventArgs e)
        {
            var options = XmlDal.DataModel.Options;
            options.RestoreOpenDocuments = tsmiRestoreOpenedDocuments.Checked = !tsmiRestoreOpenedDocuments.Checked;
        }
        private void tsmiRunScript_Click(object sender, EventArgs e)
        {
            MainViewModel.RunScripts();
        }
        private void tsmiRunScriptOnAll_Click(object sender, EventArgs e)
        {
            MainViewModel.RunScriptsOnAll();
        }
        #endregion

        private const string FontPlus = "A +";
        private const string FontMinus = "A - ";
        private void btnFont_Click(object sender, EventArgs e)
        {
            switch (btnFont.Text)
            {
                case FontPlus:
                    tbCurrentLine.Font = ColorService.ColorScheme.Current.GeneralPlusFontObject;
                    break;
                case FontMinus:
                    tbCurrentLine.Font = ColorService.ColorScheme.Current.GeneralFontObject;
                    break;
            }

            btnFont.Text = btnFont.Text == FontPlus ? FontMinus : btnFont.Text = FontPlus;
        }

        private void toolStripMenuItem8_Click_1(object sender, EventArgs e)
        {
            MainViewModel.ShowHideOptionWindow();
        }

        public void PrepareHideOptions()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(PrepareHideOptions));
                return;
            }

            while (tsmiHide.DropDownItems.Count > 2)
                tsmiHide.DropDownItems.RemoveAt(2);

            var options = MainService.GetOptions();


            var i = new ToolStripMenuItem()
            {
                Name = $"tsmiHideAll",
                Text = "All",
                CheckOnClick = true,
                Checked = options.HideOptions.Contains($"{MainViewModel.SourceType}_All")
            };
            i.Click += HideOptionClick;
            tsmiHide.DropDownItems.Add(i);

            foreach (var item in XmlDal.DataModel.HideOptionsModel.Options.Where(n => n.SourceTypes.Count == 0 ||
                   n.SourceTypes.Contains(Keywords.ALL) ||
                   n.SourceTypes.Contains(MainViewModel.SourceType)).OrderBy(n => n.Name))
            {
                i = new ToolStripMenuItem()
                {
                    Name = $"tsmi{item.Name}",
                    Text = item.Name,
                    CheckOnClick = true,
                    Checked = options.HideOptions.Contains($"{MainViewModel.SourceType}_{item.Name}")
                };
                i.Click += HideOptionClick;
                tsmiHide.DropDownItems.Add(i);
            }
        }

        private void HideOptionClick(object sender, EventArgs e)
        {
            var options = MainService.GetOptions();
            var tsmiItem = (ToolStripMenuItem)sender;

            if (tsmiItem.Text == "All")
            {
                for (int i = 3; i < tsmiHide.DropDownItems.Count; i++)
                {
                    var m = tsmiHide.DropDownItems[i] as ToolStripMenuItem;
                    if (m == null) continue;

                    m.CheckState = tsmiItem.CheckState;
                    if (m.CheckState == CheckState.Checked)
                        options.HideOptions.Add($"{MainViewModel.SourceType}_{m.Text}");
                    else
                        options.HideOptions.Remove($"{MainViewModel.SourceType}_{m.Text}");
                }
            }

            if (tsmiItem.CheckState == CheckState.Checked)
                options.HideOptions.Add($"{MainViewModel.SourceType}_{tsmiItem.Text}");                
            else
                options.HideOptions.Remove($"{MainViewModel.SourceType}_{tsmiItem.Text}");

            MainViewModel.ApplyHideOptions();
        }

        public void ApplyColorScheme()
        {
            BackColor = ColorService.ColorScheme.Current.ControlBackColor;
            dockPanel.DockBackColor = ColorService.ColorScheme.Current.ControlBackColor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var size = MainViewModel.DocumentFont.Size;
            size++;
            MainViewModel.DocumentFont = new Font(MainViewModel.DocumentFont.OriginalFontName, size);
            MainViewModel.DocumentFontChanged(MainViewModel.DocumentFont);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var size = MainViewModel.DocumentFont.Size;
            size--;
            MainViewModel.DocumentFont = new Font(MainViewModel.DocumentFont.OriginalFontName, size);
            MainViewModel.DocumentFontChanged(MainViewModel.DocumentFont);
        }

        private void addToFavoriteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }
    }
}