using System;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Model.EventNS
{
    public class EventLookupValueModel : ICrudModel<EventLookupValueModel, EventLookupValue>
    {
        public InputMode Mode { get; set; }
        public Action<EventLookupValueModel> SaveEvent { get; set; }
        public EventLookupValue Item { get; set; }
    }
}
