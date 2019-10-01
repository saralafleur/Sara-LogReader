using System;
using System.Collections.Generic;
using Sara.Common.Extension;

namespace Sara.LogReader.Model.EventNS
{
    public class EventLookupValue : ICloneable
    {
        /// <summary>
        /// When True the system will group the Lookup Values in their own Category
        /// </summary>
        public bool UseCategory { get; set; }
        public string Category { get; set; }
        /// <summary>
        /// When True, the sysytem will only look at Cached Network Data
        /// </summary>
        public bool OnlyNetworkValues { get; set; }
        /// <summary>
        /// When True the system will only look at Cached Network Data
        /// </summary>
        public bool LookupNetworkOnly { get; set; }
        public List<EventLookupValueCriteria> Criteria { get; set; }
        public List<EventLookupValueName> ValueNames { get; set; }
        public List<EventLookupValueCondition> Conditions { get; set; }
        public LookupDirection LookupDirection { get; set; }

        public EventLookupValue()
        {
            OnlyNetworkValues = true; // Default
            LookupDirection = LookupDirection.Prior; // Default;

            Criteria = new List<EventLookupValueCriteria>();
            ValueNames = new List<EventLookupValueName>();
            Conditions = new List<EventLookupValueCondition>();
        }

        public override string ToString()
        {
            var result = string.Empty;
            foreach (var valueName in ValueNames)
            {
                if (string.IsNullOrEmpty(result))
                {
                    result = valueName.Name;
                    continue;
                }
                result = $"{result}, {valueName.Name}";
            }

            return string.IsNullOrEmpty(result) ? "No values specified" : result;
        }

        public object Clone()
        {
            var o = new EventLookupValue()
            {
                UseCategory = this.UseCategory,
                Category = this.Category,
                OnlyNetworkValues = this.OnlyNetworkValues,
                LookupNetworkOnly = this.LookupNetworkOnly,
                LookupDirection = this.LookupDirection,
                Criteria = (List<EventLookupValueCriteria>)this.Criteria.Clone(),
                ValueNames = (List<EventLookupValueName>)this.ValueNames.Clone(),
                Conditions = (List<EventLookupValueCondition>)this.Conditions.Clone()
            };
            return o;
        }
    }
}
