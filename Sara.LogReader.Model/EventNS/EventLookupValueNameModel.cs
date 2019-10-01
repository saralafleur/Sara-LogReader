using System;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Model.EventNS
{
    public class EventLookupValueNameModel : ICrudModel<EventLookupValueNameModel, EventLookupValueName>
    {
        public InputMode Mode { get; set; }
        public Action<EventLookupValueNameModel> SaveEvent { get; set; }
        public EventLookupValueName Item { get; set; }
    }

    public class EventLookupValueName : ICloneable
    {
        public string Name { get; set; }
        public object Clone()
        {
            var o = new EventLookupValueName()
            {
                Name = this.Name
            };
            return o;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
