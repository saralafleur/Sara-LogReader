using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Sara.LogReader.Common;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.LogReaderNS;
using Sara.LogReader.Model.NetworkMapping;
using Sara.LogReader.Service;
using Sara.LogReader.Service.FileServiceNS;
using Sara.LogReader.WinForm.Views.Document;
using Sara.WinForm.MVVM;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class DocumentViewModel : ViewModelBase<DocumentWindow, string, object>
    {
        #region Properties
        private DocumentModel _model;
        /// <summary>
        /// Provides direct access to the File.IsCached without forcing a rebuild of the file
        /// </summary>
        public bool IsCached
        {
            get
            {
                return _model.File.IsCached;
            }
        }
        public DocumentModel Model
        {
            get
            {
                if (_model == null)
                    return null;

                // If the File has been invalidated, then build it again.
                FileService.Build(_model.File, true);
                return _model;
            }
            set
            {
                _model = value;
            }
        }
        public LineArgs CurrentLineArgs
        {
            get
            {
                return new LineArgs
                {
                    Path = Model.File.Path,
                    iLine = CurrentiLine,
                    Line = CurrentLine,
                    StatusUpdateEvent = View.StatusUpdate
                };
            }
        }
        public int CurrentiLine { get { return View.CurrentiLine; } }
        public string CurrentLine
        {
            get
            {
                if (CurrentiLine >= 0 && Model.File.RawText != null && CurrentiLine <= Model.File.RawText.Count - 1)
                    return Model.File.RawText[CurrentiLine];

                return string.Empty;
            }
        }
        public string Text { get { return Model.File.RawTextString; } }
        public List<FoldingEvent> FoldingEvents { get { return Model.File.FoldingEvents; } }

        internal void GetLineHighlight(Line line, int iLine)
        {
            MainViewModel.GetLineHighlight(line, iLine);
        }

        #endregion

        #region Setup
        public DocumentViewModel(DocumentWindow view)
        {
            View = view;
        }
        #endregion

        #region Render
        public override string GetModel()
        {
            if (Model == null)
                return "";
            if (Model.File == null)
                return "";

            return HideOptionService.ApplyHideOptions(MainViewModel.SourceType, Model.File.RawTextString);
        }
        internal void RenderStarted()
        {
            MainViewModel.DocumentRenderStarted();
        }
        internal void DocumentRenderComplete()
        {
            MainViewModel.DocumentRenderComlete();
        }
        public void CurrentLineChanged()
        {
            if (!IsRenderReady)
                return;

            MainViewModel.CurrentLineChanged();
        }
        #endregion

        #region Events
        public event Action CollaspeAllEvent;
        public event Action ExpandAllEvent;
        public event Action FocusEvent;
        public event Action<int> GoToLineEvent;
        public new void Ready()
        {
            base.Ready();
        }
        #endregion

        #region Methods
        public void CollaspeAll()
        {
            CollaspeAllEvent?.Invoke();
        }
        public void ExpandAll()
        {
            ExpandAllEvent?.Invoke();
        }
        public string AnalysizePercentDocumentated()
        {
            return DocumentService.AnalysizePercentDocumentated(Model);
        }
        public void Focus()
        {
            FocusEvent?.Invoke();
        }
        public void GoToLine(int iLine)
        {
            GoToLineEvent?.Invoke(iLine);
        }
        public void GoTo(ValueBookMark item)
        {
            MainViewModel.Current.GoToLine(item.iLine);
            MainViewModel.GoToValueId(item.ValueId);
        }
        /// <summary>
        /// Calculates the char position of each hidden text.
        /// </summary>
        /// <param name="line"></param>
        public void CalculateHiddenText(Line line)
        {
            if (Model.File.Options.DateTime)
                CheckForHiddenTextWithDate(line, "<", "> ");

            if (Model.File.Options.EntryType)
                CheckForHiddenText(line, "(", ") ");

            if (Model.File.Options.ThreadId)
                CheckForHiddenTextWithNumber(line, "[", "] ");

            if (Model.File.Options.Class)
                FindEverythingBetweenTwoWordsIncludingTheFirstWords(line, "Class: ", " Method:");

            if (Model.File.Options.Method)
                FindEverythingBetweenTwoWordsIncludingTheFirstWords(line, "Method: ", " Message:");

            if (Model.File.Options.NetworkInfo)
                FindNetworkInfo(line);
        }
        private static void CheckForHiddenText(Line line, string startWord, string endWord)
        {
            var match = RegularExpression.FindEverythingBetweenTwoWordsIncludingTheWords(line.Text, startWord, endWord);
            if (match.Success)
            {
                line.HiddenText.Add(new HideRange
                {
                    Start = match.Start,
                    End = match.End
                });
            }
        }
        private static void FindNetworkInfo(Line line)
        {
            var match = RegularExpression.FindEverythingBetweenTwoWordsIncludingTheFirstWords(line.Text, "Message[", "]");
            const string messageName = "MessageName: ";
            var match2 = RegularExpression.FindEverythingBetweenTwoWordsIncludingTheFirstWords(line.Text, messageName, ",");
            if (match.Success)
            {
                line.HiddenText.Add(new HideRange { Start = match.Start, End = match2.Start + messageName.Length - 1 });
                line.HiddenText.Add(new HideRange { Start = match2.End, End = match.End });
            }
        }
        private static void FindEverythingBetweenTwoWordsIncludingTheFirstWords(Line line, string startWord, string endWord)
        {
            var match = RegularExpression.FindEverythingBetweenTwoWordsIncludingTheFirstWords(line.Text, startWord, endWord);
            if (match.Success)
            {
                line.HiddenText.Add(new HideRange
                {
                    Start = match.Start,
                    End = match.End
                });
            }
        }
        private static void CheckForHiddenTextWithNumber(Line line, string startWord, string endWord)
        {
            var match = RegularExpression.FindNumberBetweenTwoWordsIncludingTheWords(line.Text, startWord, endWord);
            if (match.Success)
            {
                line.HiddenText.Add(new HideRange
                {
                    Start = match.Start,
                    End = match.End
                });
            }
        }
        private static void CheckForHiddenTextWithDate(Line line, string startWord, string endWord)
        {
            var match = RegularExpression.FindDateBetweenTwoWordsIncludingTheWords(line.Text, startWord, endWord);
            if (match.Success)
            {
                line.HiddenText.Add(new HideRange
                {
                    Start = match.Start,
                    End = match.End
                });
            }
        }
        public bool CurrentLineIsNetworkMessage
        {
            get
            {
                var property = PropertyService.GetProperty(MainViewModel.Current.CurrentLineArgs);

                return property.IsNetwork;
            }
        }
        public void FollowNetworkMessage()
        {
            View.SetupMenuShortCut();
        }
        public void GetNetworkMessages(Action<NetworkTargets> callback)
        {
            NetworkMapService.GetNetworkMessagesBySourceLine(MainViewModel.Current.CurrentLineArgs, callback);
        }
        public void GoToFileAndLine(string filePath, int iLine)
        {
            MainViewModel.GoToFile(filePath);
            MainViewModel.GoToLine(iLine);
            MainViewModel.Current.View.PingCurrentLine();
        }
        public void RemoveDocument()
        {
            MainViewModel.CloseDocument(this);
        }
        public void CheckCurrent()
        {
            MainViewModel.CheckCurrent(this);
        }
        public void CloseAllDocuments()
        {
            MainViewModel.CloseAllDocuments();
        }
        public void CloseAllButThisOne()
        {
            MainViewModel.CloseAllButThisOne();
        }
        public void SetHideOptions(OptionsCacheData options)
        {
            Model.File.Options = options;
            RenderDocument();
        }
        #endregion
    }
}
