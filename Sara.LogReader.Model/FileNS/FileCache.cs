namespace Sara.LogReader.Model.FileNS
{

    // Data					Build			Flag						Trigger
    // ----					----			----						-------
    // Start and Stop		Build(false)	IsCached					Invalidate
    // Title				Build(false)	IsCached_Title				Invalidate, Values
    // Source Type			Build(false)	IsCached_SourcedType		Invalidate, Events
    // Non-Lazy FileValues	Build(false)	IsCached_Values				Invalidate, Values
    // Lazy FileValues		Build(true)		IsCached_Lazy_Values		Invalidate, Values
    // Document Map			Build(true)		IsCached_Lazy_DocumentMap	Invalidate,	Values, Events
    // Folding Events		Build(true)		IsCached_Lazy_Events		Invalidate, Events
    // 
    // Network				BuildNetwork	IsCached_Lazy_Network		Invalidate, Values, Events

    public partial class FileData
    {
        /// <summary>
        /// When the CacheData is Invalidated, IsCached is set to False
        /// </summary>
        public bool IsCached { get; set; }
        // ReSharper disable once InconsistentNaming
        public bool IsCached_Title { get; set; }
        // ReSharper disable once InconsistentNaming
        public bool IsCached_Values { get; set; }
        // ReSharper disable once InconsistentNaming
        public bool IsCached_Lazy_Values { get; set; }
        // ReSharper disable once InconsistentNaming
        public bool IsCached_Lazy_Events { get; set; }
        // ReSharper disable once InconsistentNaming
        private bool _isCached_Lazy_Network;

        /// <summary>
        /// When True the following has occured
        ///   NetworkMessages are generated from the file
        ///   NetworkMessages Values are generated
        ///   Network Nodes are generated
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public bool IsCached_Lazy_NetworkMessages
        {
            get { return _isCached_Lazy_Network; }
            set
            {
                lock (this)
                {
                    if (!value)
                    {
                        foreach (var msg in Network.NetworkMessages)
                        {
                            msg.IsCached_Targets = false;
                        }
                    }

                    _isCached_Lazy_Network = value;
                }
            }
        }
        // ReSharper disable once InconsistentNaming
        public bool IsCached_Lazy_DocumentMap { get; set; }
        // ReSharper disable once InconsistentNaming
        public bool IsCached_SourceType { get; set; }
        public bool Delete { get; set; }

        public void Clear()
        {
            Initialize();
            IsCached = false;
            IsCached_Title = false;
            IsCached_SourceType = false;
            IsCached_Values = false;
            IsCached_Lazy_Values = false;
            IsCached_Lazy_DocumentMap = false;
            IsCached_Lazy_Events = false;
            IsCached_Lazy_NetworkMessages = false;
        }

        public void InvalidateEvent()
        {
            IsCached_SourceType = false;
            IsCached_Lazy_DocumentMap = false;
            IsCached_Lazy_Events = false;
            IsCached_Lazy_NetworkMessages = false;
            DataController.InvalidatePart();
        }

        public void InvalidateValue()
        {
            IsCached_Title = false;
            IsCached_Values = false;
            IsCached_Lazy_Values = false;
            IsCached_Lazy_DocumentMap = false;
            IsCached_Lazy_NetworkMessages = false;
            DataController.InvalidatePart();
        }

    }
}
