using System;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Model.EventNS
{
    public class EventLookupValueConditionModel : ICrudModel<EventLookupValueConditionModel, EventLookupValueCondition>
    {
        public InputMode Mode { get; set; }
        public Action<EventLookupValueConditionModel> SaveEvent { get; set; }
        public EventLookupValueCondition Item { get; set; }
    }
}
