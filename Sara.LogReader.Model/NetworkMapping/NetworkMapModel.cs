using System;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Model.NetworkMapping
{
    public class NetworkMapModel
    {
        public InputMode Mode { get; set; }
        public Action<NetworkMapModel> SaveEvent;
        public NetworkMap Item { get; set; }
    }
}
