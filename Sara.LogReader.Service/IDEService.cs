using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sara.Common.DateTimeNS;
using Sara.Common.Debug;
using Sara.Common.Extension;
using Sara.LogReader.Model;
using Sara.LogReader.Model.IDE;
using Sara.LogReader.Model.PatternNS;
using Sara.LogReader.Service.Core.Pattern;
using Sara.LogReader.Service.PatternServiceNS;
using Sara.MonitorScript;
using Sara.MonitorScript.Lexer;
using Sara.MonitorScript.Parser;
using Sara.MonitorScript.Syntax;
using Sara.MonitorScript.Syntax.Declarations;
using Sara.MonitorScript.Syntax.Statements;
using Sara.WinForm.Notification;

namespace Sara.LogReader.Service
{
    public class TestArgs
    {
        public string Script { get; set; }
        public List<string> Files { get; set; }
        /// <summary>
        /// Content of the file we are performing the test on
        /// </summary>
        public bool ShowTokens { get; set; }
        internal MonitorScriptLexer Lexer { get; set; }
        internal MonitorScriptParser Parser { get; set; }
        public PatternTestResults Result { get; set; }
        internal SourceCode SourceCode { get; set; }
        internal List<Token> Tokens { get; set; }
        internal SourceDocument AbstractSyntaxTree { get; set; }
        internal List<PatternComplex> Patterns { get; set; }
        public Action<PatternTestResults> ResultCallback { get; set; }
        public Pattern Pattern { get; set; }
        public Action<IStatusModel> StatusUpdate { get; set; }
        public TestArgs()
        {
            Lexer = new MonitorScriptLexer();
            Parser = new MonitorScriptParser(Lexer.ErrorSink);
            Result = new PatternTestResults();
            Patterns = new List<PatternComplex>();
        }
    }

    public class OptionException : Exception
    {
        public OptionException(string message, SourceSpan span) : base(message)
        {
            Span = span;
        }

        public SourceSpan Span { get; set; }
    }

    public static class IDEService
    {
        private const string LEXER_ERROR = "Lexer Error";
        private const string ABSTRACT_SYNTAX_TREE_ERROR = "Abstract Syntax Tree Error";
        private const string PATTERN_SYNTAX_ERROR = "Pattern Syntax Error";

        public static IDEModel GetModel()
        {
            var model = XmlDal.DataModel.GetIDEModel();
            //model.Script = model.Script.Replace("\n", "\r\n");
            return model;
        }

        public static void SaveModel(IDEModel model)
        {
            XmlDal.DataModel.SaveIDEModel(model);
        }

        private static List<string> WriteError(ErrorEntry error)
        {
            var errorResult = new List<string>
            {
                error.Lines.First(),
                new string(' ', error.Span.Start.Column) + new string('^', error.Span.Length),
                $@"{error.Severity}: {error.Message}
{error.Span}"
            };
            return errorResult;
        }

        public static bool RunLexer(TestArgs args)
        {
            args.SourceCode = new SourceCode(args.Script);
            args.Tokens = args.Lexer.LexFile(args.SourceCode).ToList();

            if (args.ShowTokens)
            {
                args.Result.Log.Add("TOKENS");
                args.Result.Log.Add(new string('-', 20));
                foreach (var token in args.Tokens)
                {
                    args.Result.Log.Add($"{token.Kind} ( \"{token.Value.Replace("\n", "\\n").Replace("\r", "\\r")}\" ) ");
                }
                args.Result.Log.Add(new string('-', 20));
            }

            if (args.Lexer.ErrorSink.Count() > 0)
            {
                args.Result.Log.Add($"{DateTime.Now.ToShortTimeString()} {LEXER_ERROR}");
                args.Result.Log.Add(new string('-', 20));
                foreach (var error in args.Lexer.ErrorSink)
                {
                    args.Result.Log.AddRange(WriteError(error));
                }
                args.Lexer.ErrorSink.Clear();

                return false;
            }

            args.Result.Log.Add($"{DateTime.Now.ToShortTimeString()} Lexer Succeeded...");
            args.StatusUpdate(StatusModel.AddPersistentDetail("Lexer complete"));
            return true;
        }

        private static bool RunParser(TestArgs args)
        {
            args.AbstractSyntaxTree = args.Parser.ParseFile(args.SourceCode, args.Tokens, MonitorScriptParserOptions.OptionalSemicolons);

            if (args.Lexer.ErrorSink.Count() > 0)
            {
                args.Result.Log.Add($"{DateTime.Now.ToShortTimeString()} {ABSTRACT_SYNTAX_TREE_ERROR}");
                args.Result.Log.Add(new string('-', 20));
                foreach (var error in args.Lexer.ErrorSink)
                {
                    //result.Add(new string('-', 20));
                    args.Result.Log.AddRange(WriteError(error));
                }
                args.Lexer.ErrorSink.Clear();
                return false;
            }
            args.Result.Log.Add($"{DateTime.Now.ToShortTimeString()} Abstract Syntax Tree Succeeded");

            if (args.AbstractSyntaxTree.Children.Where(n => n.Kind == SyntaxKind.PatternDeclaration).Count() == 0)
            {
                args.Result.Log.Add($@"{DateTime.Now.ToShortTimeString()} The script does not contain a Pattern!");
                return false;
            }

            args.StatusUpdate(StatusModel.AddPersistentDetail("Abstract Syntax Tree ready"));
            return true;
        }

        /// <summary>
        /// Using the Abstract Syntax Tree, this method will create concreate Pattern's
        /// </summary>
        private static bool AbstractSyntaxTreeToPatterns(TestArgs args)
        {
            var noErrors = true;
            foreach (PatternDeclaration pd in args.AbstractSyntaxTree.Children.Where(n => n.Kind == SyntaxKind.PatternDeclaration))
            {
                // Default
                ScanType scanType = ScanType.FirstOccurance;
                if (pd.Options.FirstOrDefault(n => n.Value == Keyword._Repeating) != null)
                    scanType = ScanType.Repeating;

                var pc = new PatternComplex(pd.Name, scanType);

                foreach (var source in pd.Sources)
                    pc.Sources.Add(source.Value);

                foreach (var option in pd.Options)
                {
                    switch (option.Name)
                    {
                        case Keyword._TotalTime:
                            pc.Options.TotalTimeOption = true;
                            break;
                        case Keyword._KnownIdle:
                            pc.Options.KnownIdle = true;
                            pc.Options.KnownIdleParameters = option.Parameters;
                            break;
                        case Keyword._UnknownIdle:
                            pc.Options.UnknownIdle = true;
                            pc.Options.UnknownIdleParameters = option.Parameters;
                            break;
                        case Keyword._IdleTime:
                            pc.Options.IdleTimeOption = true;
                            break;
                        case Keyword._HidePattern:
                            pc.Options.HidePattern = true;
                            break;
                        case Keyword._HideFilePath:
                            pc.Options.HideFilePath = true;
                            break;
                        case Keyword._Unexpected:
                            pc.Options.PatternUnexpected = true;
                            break;
                        default:
                            break;
                    }
                }

                foreach (var item in pd.Body.Contents)
                {
                    try
                    {
                        var e = item as EventStatement;

                        if (e == null)
                        {
                            var temp = DateTime.Now.ToShortTimeString();
                            throw new OptionException($@"{temp} {PATTERN_SYNTAX_ERROR}
{new string(' ', temp.Length)} Unexpected '{item.GetType()}'.Expected 'EventStatement'", item.Span);
                        }

                        var regularExpression = LookupEvent(e.Event, e.Span);

                        EventOption option = ParseOption(e);

                        var regularExpressionBodyStop = string.Empty;

                        if (option.BodyStop)
                        {
                            regularExpressionBodyStop = LookupEvent(option.BodyStopParameter, e.Span);
                        }

                        pc.AddEvent(new EventPattern(ConvertEventType(e.EventType), e.Event, regularExpression, regularExpressionBodyStop, option));
                    }
                    catch (OptionException ex)
                    {
                        var e = new ErrorEntry(ex.Message, args.SourceCode.GetLines(ex.Span.Start.Line, ex.Span.End.Line), Severity.Fatal, ex.Span);
                        args.Result.Log.AddRange(WriteError(e));
                        noErrors = false;
                    }
                }
                args.Patterns.Add(pc);
            }
            return noErrors;
        }

        private static EventOption ParseOption(EventStatement e)
        {
            if (e.Options == null || e.Options.Count == 0)
                return new EventOption() { Required = true };

            var o = new EventOption();

            foreach (var option in e.Options.Where(n => n.Kind == SyntaxKind.OptionDeclaration))
            {
                switch (option.Value)
                {
                    case Keyword._Required:
                        o.Required = true;
                        break;
                    case Keyword._RequiredInOrder:
                        o.RequiredInOrder = true;
                        break;
                    case Keyword._Optional:
                        o.Optional = true;
                        break;
                    case Keyword._Unexpected:
                        o.Unexpected = true;
                        break;
                    case Keyword._TimeToNext:
                        o.TimeToNext = true;
                        o.TimeToNextParameters = option.Parameters;
                        break;
                    case Keyword._TimeToNextKnownIdle:
                        o.TimeToNextKnownIdle = true;
                        o.TimeToNextKnownIdleParameters = option.Parameters;
                        break;
                    case Keyword._OneOrMore:
                        o.OneOrMore = true;
                        break;
                    case Keyword._Frequency:
                        o.Frequency = true;
                        o.FrequencyParameters = option.Parameters;
                        break;
                    case Keyword._FrequencyPerFile:
                        o.FrequencyPerFile = true;
                        o.FrequencyPerFileParameters = option.Parameters;
                        break;
                    case Keyword._TimeTo:
                        o.TimeTo = true;
                        o.TimeToParameters = option.Parameters;
                        break;
                    case Keyword._TimeToOr:
                        o.TimeToOr = true;
                        o.TimeToOrParameters = option.Parameters;
                        break;
                    case Keyword._TimeFrom:
                        o.TimeFrom = true;
                        o.TimeFromParameters = option.Parameters;
                        break;
                    case Keyword._HideEvent:
                        o.HideEvent = true;
                        break;
                    case Keyword._Hide:
                        o.Hide = true;
                        break;
                    case Keyword._Value:
                        o.Value = true;
                        o.ValueParameters = option.Parameters;
                        break;
                    case Keyword._LastRepeat:
                        o.LastRepeat = true;
                        // OneOrMore is required when using LastRepeat as you are looking for OneOrMore Events that repeat - Sara
                        o.OneOrMore = true;
                        break;
                    case Keyword._FirstRepeat:
                        o.FirstRepeat = true;
                        // OneOrMore is required when using FirstRepeat as you are looking for OneOrMore Events that repeat - Sara
                        o.OneOrMore = true;
                        break;
                    case Keyword._Prior:
                        o.Prior = true;

                        o.PriorIndex = -1;
                        o.PriorPattern = "-1";
                        foreach (var item in option.Parameters)
                        {
                            if (int.TryParse(item, out int index))
                                o.PriorIndex = index;
                            else
                                o.PriorPattern = item;
                        }
                        if (o.PriorIndex == -1)
                            o.PriorIndex = 1;
                        if (o.PriorPattern == "-1")
                            throw new OptionException($@"{PATTERN_SYNTAX_ERROR}
UnExpected '<>'  Expected 'Prior(<EventPattern>)' - You must specify the prior EventPattern", option.Span);
                        break;
                    case Keyword._Name:
                        o.Name = true;
                        if (option.Parameters.Count == 0)
                            throw new OptionException($@"{PATTERN_SYNTAX_ERROR}
UnExpected Name  Expected 'Name (<Name>)'", option.Span);
                        if (option.Parameters.Count > 1)
                            throw new OptionException($@"{PATTERN_SYNTAX_ERROR}
UnExpected Name ({option.Parameters.ToCsv()})  Expected 'Name (""<Name>"")'", option.Span);
                        o.NameParameter = option.Parameters[0];
                        break;
                    case Keyword._BodyStop:
                        o.BodyStop = true;
                        if (option.Parameters.Count == 0)
                            throw new OptionException($@"{PATTERN_SYNTAX_ERROR}
UnExpected BodyStop  Expected 'BodyStop (<EventPattern>)'", option.Span);
                        if (option.Parameters.Count > 1)
                            throw new OptionException($@"{PATTERN_SYNTAX_ERROR}
UnExpected BodyStop ({option.Parameters.ToCsv()})  Expected 'BodyStop (""<EventPattern>"")'", option.Span);
                        o.BodyStopParameter = option.Parameters[0];
                        break;
                }
            }

            return o;
        }

        private static bool RunPatternScan(TestArgs args)
        {
            args.StatusUpdate(StatusModel.Update("Preparing Scan"));
            if (!AbstractSyntaxTreeToPatterns(args))
                return false;

            #region Prepare Files for scanning
            if (args.Files.Count == 0)
            {
                var temp = DateTime.Now.ToShortTimeString();

                args.Result.Log.Add($@"{temp} {PATTERN_SYNTAX_ERROR}
{new string(' ', temp.Length)} You must open or select a file before you can scan for the Pattern!");
                return false;
            }

            var files = args.Files.Distinct().ToList();

            long current = 0;
            var total = files.Count();

            var sw2 = new Stopwatch("Filtering Files for Pattern Scan");
            var sourceTypes = new List<string>();
            foreach (var pattern in args.Patterns)
            {
                foreach (var source in pattern.Sources)
                {
                    if (!sourceTypes.Contains(source))
                        sourceTypes.Add(source);
                }
            }

            // Only Scan Files that have a Pattern for their given SourceType
            List<string> ff = new List<string>();
            foreach (var source in sourceTypes)
            {
                var p = XmlDal.CacheModel.Options.CachedSourceTypes.FirstOrDefault(n => n.Type == source);
                if (p != null)
                    ff.AddRange(p.Files);
            }

            var query = from f in ff
                        join f2 in args.Files on f equals f2
                        select f;

            current = 0;
            total = query.Count();
            #endregion

            PreProcessPatterns(args);

            Parallel.ForEach(query, (path) =>
             {
                 if (Shutdown.Now) return;
                 var file = XmlDal.CacheModel.GetFile(path);
                 var sw = new Stopwatch($"Pattern Scan on \"{file.Path}\"");
                 Interlocked.Increment(ref current);
                 int _current = (int)Interlocked.Read(ref current);
                 args.StatusUpdate(StatusModel.Update("Scanning for Patterns", $@"{_current} of {total}", total, _current));

                 var result = new PatternTestResults(args.Result);

                 new PatternScanService().Scan(args.Patterns, file.Path, result);
                 lock (args)
                 {
                     args.Result.Merge(result);
                 }
                 // If we do not unload each file after a Scan then we will eat up all the memory. - Sara
                 XmlDal.CacheModel.GetFile(file.Path).UnLoad();
                 sw.Stop(100);
             });

            PostProcessPatterns(args.Result);

            return true;
        }

        private static void PostProcessPatterns(PatternTestResults result)
        {
            foreach (var summary in result.PatternSummaries)
            {
                var removeEventNames = new List<string>();
                foreach (var freq in summary.Frequencies.Where(n => n.Path == null))
                {
                    if (summary.Frequencies.Count(n => n.Name == freq.Name) > 1)
                        removeEventNames.Add(freq.Name);
                }
                for (int i = summary.Frequencies.Count - 1; i >= 0; i--)
                {
                    if (summary.Frequencies[i].Path == null && removeEventNames.Contains(summary.Frequencies[i].Name))
                    {
                        removeEventNames.Remove(summary.Frequencies[i].Name);
                        summary.Frequencies.RemoveAt(i);
                        if (removeEventNames.Count == 0)
                            break;
                    }
                   
                }
            }
        }

        /// <summary>
        /// Look for all the attributes that determine 'Clean', meaning that we meet a set of critera
        /// Per each Clean attriute on an eventPattern, create a PatternTestCleanEvent record
        /// These Clean events will be used to compare against the pattern results and for rendering.
        /// </summary>
        private static void PreProcessPatterns(TestArgs args)
        {
            foreach (var p in args.Patterns)
            {
                var summary = new PatternTestSummary(p.Name) { Options = p.Options.Clone() };

                // Collect all of the validation attributes for the given pattern
                foreach (var e in p.Events.Where(n => (n.Options.Unexpected ||
                                                       n.Options.Frequency ||
                                                       n.Options.FrequencyPerFile ||
                                                       // Known Idle
                                                       n.Options.TimeToNextKnownIdle ||
                                                       // Duration
                                                       n.Options.TimeFrom ||
                                                       n.Options.TimeTo ||
                                                       n.Options.TimeToNext ||
                                                       n.Options.TimeToOr)))
                {
                    if (e.Options.Unexpected)
                        summary.Unexpected.Add(new CleanAttribute() { EventName = e.Name });

                    if (e.Options.Frequency && e.Options.FrequencyParameters.ToMinAndMax(out int _min, out int _max))
                        summary.Frequencies.Add(new Frequency() { EventName = e.Name, FrequencyType = FrequencyType.PerPattern, Min = _min, Max = _max });

                    if (e.Options.FrequencyPerFile && e.Options.FrequencyPerFileParameters.ToMinAndMax(out _min, out _max))
                        summary.Frequencies.Add(new Frequency() { EventName = e.Name, FrequencyType = FrequencyType.PerFile, Min = _min, Max = _max });

                    if (e.Options.TimeToNextKnownIdle)
                    {
                        var _baseLine = e.Options.TimeToNextKnownIdleParameters.ToBaseLineList().First();
                        summary.KnownIdle.Add(new Duration() { EventName = e.Name, Min = _baseLine.Min, Max = _baseLine.Max });
                    }

                    if (e.Options.TimeFrom)
                    {
                        var parameters = e.Options.TimeFromParameters.ToBaseLineList();

                        foreach (var param in parameters)
                            summary.Durations.Add(new Duration() { EventName = e.Name, Min = param.Min, Max = param.Max });
                    }

                    if (e.Options.TimeTo)
                    {
                        var parameters = e.Options.TimeToParameters.ToBaseLineList();
                        foreach (var param in parameters)
                            summary.Durations.Add(new Duration() { EventName = e.Name, Min = param.Min, Max = param.Max });
                    }

                    if (e.Options.TimeToNext)
                    {
                        var _baseLine = e.Options.TimeToNextParameters.ToBaseLineList().First();
                        summary.Durations.Add(new Duration() { EventName = e.Name, Min = _baseLine.Min, Max = _baseLine.Max });
                    }

                    if (e.Options.TimeToOr)
                    {
                        var parameters = e.Options.TimeToOrParameters.ToBaseLineList();
                        foreach (var param in parameters)
                            summary.Durations.Add(new Duration() { EventName = e.Name, Min = param.Min, Max = param.Max });
                    }
                }

                args.Result.CleanPatternSummaries.Add(summary);
            }
        }

        public static PatternTestResults Test(TestArgs args)
        {
            args.StatusUpdate(StatusModel.Update("Running..."));
            var time = new Stopwatch("Pattern Scan");
            try
            {
                if (!RunLexer(args))
                    return args.Result;

                if (!RunParser(args))
                    return args.Result;

                if (!RunPatternScan(args))
                    return args.Result;
            }
            finally
            {
                time.Stop();
                args.Result.Log.Add(new string('-', 20));
                args.Result.Log.Add($@"{DateTime.Now.ToShortTimeString()} Test Complete {time.Duration.Value.ToReadableString()}");
                args.StatusUpdate(StatusModel.ClearPersistentDetail);
                //args.StatusUpdate(StatusModel.Completed);
            }

            return args.Result;
        }

        private static EventType ConvertEventType(string eventType)
        {
            if (eventType == Keyword._Start)
                return EventType.Start;
            if (eventType == Keyword._Body)
                return EventType.Body;
            if (eventType == Keyword._Stop)
                return EventType.Stop;
            if (eventType == Keyword._Reset)
                return EventType.Reset;
            if (eventType == Keyword._Search)
                return EventType.Search;
            if (eventType == Keyword._Restart)
                return EventType.Restart;

            var temp = DateTime.Now.ToShortTimeString();
            throw new Exception($@"{temp} {PATTERN_SYNTAX_ERROR}
{new string(' ', temp.Length)} Unexpected '{eventType}'.Expected 'Start', 'Body', 'Stop', 'Reset', or 'Search'");
        }

        private static string LookupEvent(string @event, SourceSpan span)
        {
            var e = EventService.GetEventByName(@event);
            if (e == null)
                throw new OptionException($@"{PATTERN_SYNTAX_ERROR} - EventPattern ""{@event}"" was not found!", span);

            return e.RegularExpression;
        }
    }
}
