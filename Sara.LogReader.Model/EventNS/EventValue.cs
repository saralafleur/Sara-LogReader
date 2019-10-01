using System;

namespace Sara.LogReader.Model.EventNS
{
    /// <summary>
    /// A EventPattern Value is used in analysis and to dispaly values in the Property window when you are on the given line
    /// </summary>
    public class EventValue : ICloneable
    {
        /// <summary>
        /// The regular expression that is used to extract the value from the line
        /// </summary>
        public string RegularExpression { get; set; }
        /// <summary>
        /// Category that is displayed in the Property Window
        /// </summary>
        public string PropertyCategory { get; set; }
        /// <summary>
        /// Name that is displayed in the Property Window
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// Description that is displayed when you focus on this field in the Property Window
        /// </summary>
        public string PropertyDescription { get; set; }
        /// <summary>
        /// The first match that is returned by the RegularExpression for the given line
        /// </summary>
        public object Value { get; set; }

        public object Clone()
        {
            var o = new EventValue()
            {
                RegularExpression = this.RegularExpression,
                PropertyCategory = this.PropertyCategory,
                PropertyName = this.PropertyName,
                PropertyDescription = this.PropertyDescription,
                Value = this.Value
            };
            return o;
        }

        public override string ToString()
        {
            return PropertyName;
        }
    }
}
