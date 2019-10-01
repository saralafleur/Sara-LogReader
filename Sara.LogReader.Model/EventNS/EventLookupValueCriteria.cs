using System;
using Sara.LogReader.Common;

namespace Sara.LogReader.Model.EventNS
{
    public enum LookupTargetType
    {
        Value = 0,
        Name = 1
    }
    public class EventLookupValueCriteria : ICloneable
    {
        public EventLookupValueCriteria()
        {
            CriteriaType = LookupTargetType.Name;
            Operator = Keywords.EQUAL;
        }
        public string TargetName { get; set; }
        public string Operator { get; set; }
        public LookupTargetType CriteriaType { get; set; }
        public string TargetValue { get; set; }
        public string SourceName { get; set; }

        public object Clone()
        {
            var o = new EventLookupValueCriteria()
            {
                TargetName = this.TargetName,
                Operator = this.Operator,
                CriteriaType = this.CriteriaType,
                TargetValue = this.TargetValue,
                SourceName = this.SourceName
            };
            return o;
        }
        public override string ToString()
        {
            switch (CriteriaType)
            {
                case LookupTargetType.Value:
                    return $"{TargetName} {Operator} {TargetValue}";
                case LookupTargetType.Name:
                    return $"{TargetName} {Operator} {TargetName}";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
