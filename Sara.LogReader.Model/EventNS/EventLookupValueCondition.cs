using System;
using Sara.LogReader.Common;

namespace Sara.LogReader.Model.EventNS
{
    public class EventLookupValueCondition : ICloneable
    {
        public EventLookupValueCondition()
        {
            Operator = Keywords.EQUAL;
        }
        
        public string Name { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }

        public object Clone()
        {
            var o = new EventLookupValueCondition()
            {
                Name = this.Name,
                Operator = this.Operator,
                Value = this.Value
            };
            return o;
        }

        public override string ToString()
        {
            return $"{Name} {Operator} {Value}";
        }
    }
}
