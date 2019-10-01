using System;
using System.Threading;
using Sara.Common.Threading;

namespace Sara.LogReader.Service
{
    public class OutputMessage
    {
        public OutputMessage(string msg)
        {
            ThreadId = Thread.CurrentThread.ManagedThreadId;
            Message = msg;
        }
        public int ThreadId { get; internal set; }
        public string Message { get; internal set; }
        public override string ToString()
        {
            var t = ThreadId.ToString();
            if (t.Length == 1)
                t = $"{t} ";

            return $"[{t}] {Message}";
        }
    }

    public static class OutputService
    {
        #region Properties
        private static ThreadQueue<OutputMessage> _queue { get; set; }
        public static Action ClearOutputWindow { get; set; }
        public static event Action<string> OutputLogEvent;
        #endregion

        static OutputService()
        {
            _queue = new ThreadQueue<OutputMessage>();
            _queue.ProcessItemEvent += ProcessItem;
        }
        private static void ProcessItem(OutputMessage obj)
        {
            OutputLogEvent?.Invoke(obj.ToString());
        }
        public static void Log(string message)
        {
            _queue.Enqueue(new OutputMessage(message));
        }
        public static void Exit()
        {
            _queue.Exit();
        }
    }

    //public static class OutputService
    //{
    //    public static bool DebugOn { get; set; }

    //    #region Queue
    //    private const int QUEUE_START_SIZE = 100;
    //    private static Thread _consumerThread;
    //    private static readonly AutoResetEvent _newMessageEvent = new AutoResetEvent(false);
    //    private static readonly AutoResetEvent _shutdownEvent = new AutoResetEvent(false);
    //    private static readonly AutoResetEvent _shutdownAckEvent = new AutoResetEvent(false);
    //    private static readonly object _queueSyncObject = new object();
    //    private static Queue<OutputMessage> _queuedMessages { get; set; }
    //    public static Action ClearOutputWindow { get; set; }
    //    #endregion

    //    static OutputService()
    //    {
    //        _queuedMessages = new Queue<OutputMessage>(QUEUE_START_SIZE);

    //        try
    //        {
    //            _consumerThread = new Thread(ConsumeMessages)
    //            {
    //                Name = "OutputService Worker",
    //                IsBackground = false
    //            };
    //            _consumerThread.Start();
    //        }
    //        catch (Exception ex)
    //        {
    //            Sara.Common.Logging.Log.Write(typeof(OutputService).FullName, MethodBase.GetCurrentMethod().Name, LogEntryType.Error, ex);
    //        }
    //    }

    //    public static void Exit()
    //    {
    //        _shutdownEvent.Set();
    //    }

    //    private static void ConsumeMessages()
    //    {
    //        var waitHandles = new WaitHandle[]
    //                  {
    //                                      _newMessageEvent,
    //                                      _shutdownEvent
    //                  };
    //        var done = false;
    //        while (done == false)
    //        {
    //            // block and wait for the message gets to the message queue
    //            switch (WaitHandle.WaitAny(waitHandles))
    //            {
    //                case 0: //new message
    //                    {
    //                        Queue<OutputMessage> messagesToProcess;
    //                        var newQueue = new Queue<OutputMessage>(QUEUE_START_SIZE);

    //                        lock (_queueSyncObject)
    //                        {
    //                            messagesToProcess = _queuedMessages;
    //                            _queuedMessages = newQueue;
    //                        }
    //                        foreach (var msg in messagesToProcess)
    //                        {
    //                            OutputLogEvent?.Invoke(msg.ToString());
    //                        }
    //                    }
    //                    break;
    //                case 1: //told to exit
    //                    done = true;
    //                    break;
    //            }
    //        }
    //        _shutdownAckEvent.Set();
    //    }

    //    public static eventPattern Action<string> OutputLogEvent;

    //    private static bool _flushingQueue;
    //    public static void Log(string message)
    //    {
    //        lock (_queueSyncObject)
    //        {
    //            _queuedMessages.Enqueue(new OutputMessage(message));
    //        }

    //        _newMessageEvent.Set(); // notify the consumer thread that it has work to do.
    //    }
    //}

}
