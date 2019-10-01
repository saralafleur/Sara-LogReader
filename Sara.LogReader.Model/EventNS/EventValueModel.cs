using System;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Model.EventNS
{
    public class EventValueModel : ICrudModel<EventValueModel, EventValue>
    {
        public InputMode Mode { get; set; }
        public Action<EventValueModel> SaveEvent { get; set; }
        public EventValue Item { get; set; }
    }
}
