using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Sara.Common.DateTimeNS;
using Sara.LogReader.Common;
using Sara.LogReader.Service;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Output
{
    public partial class OutputView : ToolWindow, IViewDock<object, object>
    {
        #region Properties
        private List<string> RawOutput { get; set; }
        private List<int> Threads { get; set; }
        private List<Label> ThreadLabels { get; set; }
        private const int STARTING_LEFT = 250;
        private const int LABEL_SPACE = 10;
        private const string ALL = "ALL";
        private const string STOPWATCH = "STOPWATCH";
        private const string HIDE_STOPWATCH = "HIDE STOPWATCH";
        private int _nextLeft = STARTING_LEFT;
        public string CurrentThread
        {
            get { return _currentThread; }
            set
            {
                lblTest.Text = string.Format("Showing: <{0}>", value);
                var oldThreadId = _currentThread;
                _currentThread = value;
                if (oldThreadId != value)
                    RefreshOutput();
            }
        }
        public bool StartupReady
        {
            get
            {
                return MainViewModel.StartupComplete;
            }
        }
        private string _currentThread;
        public OutputViewModel ViewModel { get; internal set; }
        #endregion

        #region Setup
        public OutputView()
        {
            var sw = new Stopwatch("Constructor OutputView");
            InitializeComponent();

            tbOutput.Font = new Font(FontFamily.GenericMonospace, tbOutput.Font.Size);
            Threads = new List<int>();
            ThreadLabels = new List<Label>();
            RawOutput = new List<string>();
            AddLabel(ALL);
            AddLabel(STOPWATCH);
            AddLabel(HIDE_STOPWATCH);
            CurrentThread = ALL;
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }
        ~OutputView()
        {
            OutputService.OutputLogEvent -= OutputLog;
            OutputService.ClearOutputWindow -= Clear;
        }
        #endregion

        #region Events
        private void OutputView_Load(object sender, EventArgs e)
        {
            OutputService.OutputLogEvent += OutputLog;
            OutputService.ClearOutputWindow += Clear;
        }
        private void OutputView_FormClosed(object sender, FormClosedEventArgs e)
        {
            OutputService.OutputLogEvent -= OutputLog;
            OutputService.ClearOutputWindow -= Clear;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        #endregion

        #region Methods
        private void AddLabel(string threadId)
        {
            var lbl = new Label { Text = threadId, Parent = panel1, Left = _nextLeft, Top = 10, AutoSize = true };

            int id;

            if (int.TryParse(threadId, out id))
            {
                // Tag the MAIN UI Thread with bold - Sara
                if (id == MainViewModel.MainUIThreadId)
                    lbl.Font = new Font(lbl.Font, FontStyle.Bold);
            }

            lbl.MouseHover += delegate (object sender, EventArgs args)
            {
                var l = sender as Label;
                if (l == null)
                    return;

                CurrentThread = l.Text;
            };
            _nextLeft = _nextLeft + lbl.Width + LABEL_SPACE;
        }
        private void Clear()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(Clear));
                return;
            }
            tbOutput.Clear();
            RawOutput.Clear();
        }
        private void OutputLog(string message)
        {
            if (IsDisposed)
                return;

            if (InvokeRequired)
            {
                Invoke(new Action<string>(OutputLog), message);
                return;
            }

            try
            {
                lock (RawOutput)
                {
                    var threadId = GetThreadId(message);
                    if (threadId.HasValue)
                    {
                        if (!Threads.Exists(n => n == threadId))
                        {
                            Threads.Add(threadId.Value);
                            AddLabel(threadId.ToString());
                        }
                    }

                    AddMessage(string.Format("{0}{1} -> {2}", Environment.NewLine,
                        DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"), message));
                }
            }
            catch (ObjectDisposedException)
            {
                // Do Nothing
            }
        }
        private static int? GetThreadId(string line)
        {
            var threadId = RegularExpression.GetValue(@"\[(.*)\]", line);
            int threadIdInt;
            if (int.TryParse(threadId, out threadIdInt))
                return threadIdInt;

            return null;
        }
        private void AddMessage(string line)
        {
            if (IsDisposed)
                return;

            RawOutput.Add(line);
            if (CurrentThread != ALL)
            {
                if (CurrentThread == STOPWATCH)
                {
                    if (!line.Contains("Stopwatch"))
                        return;
                }
                if (CurrentThread == HIDE_STOPWATCH)
                {
                    if (line.Contains("Stopwatch"))
                        return;
                }
                else
                {
                    var threadId = GetThreadId(line);
                    if (threadId.HasValue)
                    {
                        if (threadId.Value.ToString(CultureInfo.InvariantCulture) != CurrentThread)
                        {
                            return;
                        }
                    }
                }
            }

            tbOutput.AppendText(line);
        }
        private void RefreshOutput()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(RefreshOutput));
                return;
            }

            lock (RawOutput)
            {
                tbOutput.Clear();
                var sb = new StringBuilder();
                if (CurrentThread == ALL)
                {
                    foreach (var line in RawOutput)
                    {
                        sb.Append(line);
                    }
                }
                else
                {
                    foreach (var line in RawOutput)
                    {
                        if (CurrentThread == STOPWATCH)
                        {
                            if (!line.Contains("Stopwatch"))
                                continue;
                        }
                        if (CurrentThread == HIDE_STOPWATCH)
                        {
                            if (line.Contains("Stopwatch"))
                                continue;
                        }
                        else
                        {
                            var threadId = GetThreadId(line);
                            if (threadId.HasValue && threadId.Value.ToString(CultureInfo.InvariantCulture) != CurrentThread)
                                continue;
                        }

                        sb.Append(line);
                    }
                }
                tbOutput.AppendText(sb.ToString());
            }
        }

        public void Render(object model)
        {
            StatusPanel.StatusUpdate(StatusModel.Completed);
        }

        public void StatusUpdate(IStatusModel model)
        {
            StatusPanel.StatusUpdate(model);
        }

        public void UpdateView(object selectedModel)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}