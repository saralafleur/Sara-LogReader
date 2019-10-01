using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sara.Common.DateTimeNS;
using Sara.Common.Extension;
using Sara.Common.Threading;
using Sara.Logging;
using Sara.LogReader.Common;
using Sara.LogReader.Model;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.NetworkMapping;
using Sara.LogReader.Model.Property;

namespace Sara.LogReader.Service.FileServiceNS
{
    public class LineFS
    {
        // ReSharper disable once InconsistentNaming
        public int iLine { get; set; }
        public string Text { get; set; }
    }

    /// <summary>
    /// For the given file:
    ///   Builds the Network Nodes
    ///   Builds the Network Message Source, included Property Values
    /// </summary>
    public class BuildFileNetworkMessagesLoop : ThreadedLoop<LineFS>
    {
        private FileNetwork Network { get; set; }
        // ReSharper disable once InconsistentNaming
        private FileData _file { get; set; }
        private readonly List<LogPropertyBaseModel> _properties = new List<LogPropertyBaseModel>();

        public BuildFileNetworkMessagesLoop()
        {
            Name = "Build.Network.Messages";
        }
        
        private LogPropertyBaseModel GetProperty(LineArgs args)
        {
            lock (this)
            {
                var item = _properties.SingleOrDefault(n => n.iLine == args.iLine);
                if (item != null) return item;

                item = PropertyService.GetProperty(args);
                _properties.Add(item);
                return item;
            }
        }

        protected override void RunItem(LineFS t)
        {
            var model = GetProperty(new LineArgs
            {
                Path = _file.Path,
                iLine = t.iLine,
                Line = t.Text
            });

            if (model.Direction == NetworkDirection.Na) return;

            var networkMessageName = model.FindPropertyValue(Keywords.NETWORK_MESSAGE_NAME);
            var dateTimeString = model.FindPropertyValue(Keywords.DATETIME);
            DateTime dateTime;
            DateTimeExt.TryParseWithTimeZoneRemoval(dateTimeString, out dateTime);

            var x = new NetworkMessageInfo
            {
                Source = new NetworkPacketInfo
                {
                    FilePath = _file.Path,
                    iLine = t.iLine,
                    Direction = model.Direction,
                    NetworkMessageName = networkMessageName,
                    DateTime = dateTime
                }
            };

            string receiverIp = null;
            string receiverPort = null;
            string receiverHost = null;
            string senderIp = null;
            string senderPort = null;
            string senderHost = null;
            foreach (var prop in model.Properties)
            {
                if (prop.Value == null)
                    continue;
                var value = prop.Value.ToString();
                x.Source.Values.Add(new ValuePair
                {
                    Name = prop.Name,
                    Value = value
                });

                switch (prop.Name)
                {
                    case Keywords.RECEIVER_IP:
                        receiverIp = value;
                        break;
                    case Keywords.RECEIVER_HOST:
                        receiverHost = value;
                        break;
                    case Keywords.RECEIVER_PORT:
                        receiverPort = value;
                        break;
                    case Keywords.SENDER_IP:
                        senderIp = value;
                        break;
                    case Keywords.SENDER_HOST:
                        senderHost = value;
                        break;
                    case Keywords.SENDER_PORT:
                        senderPort = value;
                        break;
                }
            }

            lock (Network)
            {
                switch (model.Direction)
                {
                    case NetworkDirection.Na:
                        Log.Write("A direction of NA should never be added as a Node!",typeof(BuildFileNetworkMessagesLoop).FullName, MethodBase.GetCurrentMethod().Name);
                        break;
                    case NetworkDirection.Send:
                    case NetworkDirection.SendBlocking:
                        x.Source.Node = NodeBase.FormatIpAndHostHame(senderIp, senderHost);
                        if (!IsNullOrBlank(receiverIp) ||
                            !IsNullOrBlank(receiverPort) ||
                            !IsNullOrBlank(receiverHost))
                        {
                            AddNode(t, receiverIp, receiverPort, receiverHost, NodeSource.Remote, x);
                        }
                        if (!IsNullOrBlank(senderIp) ||
                            !IsNullOrBlank(senderPort) ||
                            !IsNullOrBlank(senderHost))
                        {
                            AddNode(t, senderIp, senderPort, senderHost, NodeSource.Local, x, _file.SourceType);
                        }
                        break;
                    case NetworkDirection.Receive:
                        x.Source.Node = NodeBase.FormatIpAndHostHame(receiverIp, receiverHost);
                        if (!IsNullOrBlank(senderIp) ||
                            !IsNullOrBlank(senderPort) ||
                            !IsNullOrBlank(senderHost))
                        {
                            AddNode(t, senderIp, senderPort, senderHost, NodeSource.Remote, x);
                        }
                        if (!IsNullOrBlank(receiverIp) ||
                            !IsNullOrBlank(receiverPort) ||
                            !IsNullOrBlank(receiverHost))
                        {
                            AddNode(t, receiverIp, receiverPort, receiverHost, NodeSource.Local, x, _file.SourceType);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                var msg = Network.NetworkMessages.FirstOrDefault(n => n.Source.iLine == x.Source.iLine);

                if (msg == null)
                    Network.NetworkMessages.Add(x);
                else
                {
                    msg.Source.Values = x.Source.Values;
                }
            }
        }

        private void AddNode(LineFS lineFs, string ip, string port, string hostName, NodeSource nodeSource, NetworkMessageInfo source, string sourceType = null)
        {
            if (_file.Start > _file.End)
                throw new DateTimeExt.InvalidDateRangeException();
            
            var node = new Node
            {
                Ip = ip,
                Port = port,
                Host = hostName,
                iLine = lineFs.iLine,
                Source = nodeSource,
                SourceStart = _file.Start,
                SourceEnd = _file.End
            };
            // Check if the Node already exists
            var found =
                Network.Nodes.Any(
                    n => n.Ip == node.Ip && n.Host == node.Host);

            if (found) return;

            if (sourceType == null)
            {
                var results = NetworkMapService.GetNetworkMapFiles(lineFs.Text, source);

                switch (results.Count)
                {
                    case 0:
                        node.SourceType = Keywords.NO_FILE;
                        break;
                    case 1:
                        node.SourceType = results[0].File.SourceType;
                        break;
                    default:
                        node.SourceType = "Multiple Files";
                        break;
                }
            }
            else
                node.SourceType = sourceType;

            // Here we would need to find the target file to determine Source Type
            Network.Nodes.Add(node);
        }

        private bool IsNullOrBlank(string value)
        {
            return string.IsNullOrEmpty(value) || value.Trim() == string.Empty;
        }

        protected override void ProgressUpdate(int current, int total)
        {
            // Do nothing
        }

        public void Build(FileData file)
        {
            var stopwatchOuter = new Stopwatch(MethodBase.GetCurrentMethod().Name);
            try
            {
                lock (file.Network)
                {
                    Queue<LineFS> queue2;
                    var stopwatch =
                        new Stopwatch($"{MethodBase.GetCurrentMethod().Name} - {"CANDIDATES"}");
                    try
                    {
                        Network = file.Network;
                        lock (Network)
                            Network.NetworkMessages.Clear();
                        Network.Nodes.Clear();
                        _file = file;

                        var candidates = Candidates(file);

                        var wasLoaded = false;
                        try
                        {
                            if (!file.IsLoaded)
                            {
                                wasLoaded = true;
                                file.Load(true);
                            }

                            var content = ConvertToLineCollection(candidates, file.RawText).ToList();
                            Queue = new Queue<LineFS>(content);
                            queue2 = new Queue<LineFS>(content);
                        }
                        finally
                        {
                            if (wasLoaded)
                                file.UnLoad();
                        }

                        WorkerLimit = 5;
                    }
                    finally
                    {
                        stopwatch.Stop(1000);
                    }

                    Log.WriteTrace($"{Queue.Count} lines to process in BuildFileNetworkLoop",
                        typeof(BuildFileNetworkMessagesLoop).FullName, MethodBase.GetCurrentMethod().Name);
                    if (Queue.Count > 0)
                    {
                        var stopWatchLoop1 = new Stopwatch($"{MethodBase.GetCurrentMethod().Name} - {"Loop 1"}");
                        try
                        {
                            Run();
                        }
                        finally
                        {
                            stopWatchLoop1.Stop(2000);
                        }

                        var stopWatchLoop2 = new Stopwatch($"{MethodBase.GetCurrentMethod().Name} - {"Loop 2"}");
                        try
                        {
                            // In order for EventLookupValues to work you must populate all Network Messages with values then perform a second pass to gather the Lookup Values
                            // This can be optimized later, but for not I am going to simple run through the messages twice. - Sara
                            Queue = queue2;
                            Run();
                        }
                        finally
                        {
                            stopWatchLoop2.Stop(2000);
                        }
                    }

                    Log.WriteTrace("COMPLETE - BuildFileNetworkLoop", typeof(BuildFileNetworkMessagesLoop).FullName,
                        MethodBase.GetCurrentMethod().Name);

                    // Queue contains text lines, which take up a large amount of memory, calling GC.Collect will free up that memory - Sara
                    Queue = null;
                    GC.Collect();

                    file.Network = Network;
                }
            }
            finally
            {
                stopwatchOuter.Stop(2000);
            }
        }
        /// <summary>
        /// Build a list of lines that contain Network Messages
        /// </summary>
        private static IEnumerable<int> Candidates(FileData file)
        {
            var stopWatch = new Stopwatch($"{MethodBase.GetCurrentMethod().Name} - {"Candidates Method"}");
            try
            {
                var candidates = new List<int>();

                foreach (var e in XmlDal.DataModel.EventsModel.Events.Where(n => n.Network != NetworkDirection.Na))
                {
                    Log.WriteTrace($"Building Candidates for \"{e.RegularExpression}\"",typeof(BuildFileNetworkMessagesLoop).FullName, MethodBase.GetCurrentMethod().Name);
                    var lines = RegularExpression.GetLines(e.RegularExpression, file.RawTextString);
                    candidates.AddRange(lines.Select(n => n.iLine));
                }

                candidates = candidates.Distinct().ToList();
                return candidates;
            }
            finally
            {
                stopWatch.Stop(2000);
            }
        }

        private IEnumerable<LineFS> ConvertToLineCollection(IEnumerable<int> candidates, IList<string> rawText)
        {
            return candidates.Select(iLine => new LineFS { iLine = iLine, Text = rawText[iLine] }).ToList();
        }
    }
}
