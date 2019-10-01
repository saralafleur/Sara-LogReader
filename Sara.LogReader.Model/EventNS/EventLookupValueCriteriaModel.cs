using System;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Model.EventNS
{
    public class EventLookupValueCriteriaModel : ICrudModel<EventLookupValueCriteriaModel, EventLookupValueCriteria>
    {
        public InputMode Mode { get; set; }
        public Action<EventLookupValueCriteriaModel> SaveEvent { get; set; }
        public EventLookupValueCriteria Item { get; set; }
    }
}
