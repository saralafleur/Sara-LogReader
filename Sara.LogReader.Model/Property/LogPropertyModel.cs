using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Sara.LogReader.Common;
using Sara.LogReader.Model.Categories;
using Sara.LogReader.Model.EventNS;

namespace Sara.LogReader.Model.Property
{
    public enum NetworkDirection
    {
        Na,
        Send,
        SendBlocking,
        Receive
    }

    public static class NetworkHelper
    {
        public static object NetworkDirectionToString(NetworkDirection network)
        {
            switch (network)
            {
                case NetworkDirection.Na:
                    return Keywords.NA;
                case NetworkDirection.Send:
                    return Keywords.SEND_TEXT;
                case NetworkDirection.SendBlocking:
                    return Keywords.SEND_BLOCKING_TEXT;
                case NetworkDirection.Receive:
                    return Keywords.RECEIVE_TEXT;
                default:
                    throw new ArgumentOutOfRangeException("network");
            }
        }

        public static object NetworkDirectionToAbbr(NetworkDirection direction)
        {
            switch (direction)
            {
                case NetworkDirection.Na:
                    return Keywords.NA;
                case NetworkDirection.Send:
                    return Keywords.SEND_ABBR;
                case NetworkDirection.SendBlocking:
                    return Keywords.SEND_BLOCKING_ABBR;
                case NetworkDirection.Receive:
                    return Keywords.RECEIVE_ABBR;
                default:
                    throw new ArgumentOutOfRangeException("network");
            }
        }
    }

    public class LogPropertyBaseModel : DynamicObjectType
    {
        public LogPropertyBaseModel()
        {
            EventIds = new List<int>();
        }

        private string _line;

        [Category("Raw")]
        [Description("The raw text for the line.")]
        public string Line
        {
            get { return _line; }
            set
            {
                _line = value;
                Process();
            }
        }
// ReSharper disable InconsistentNaming
        [Browsable(false)]
        public int iLine { get; set; }
// ReSharper restore InconsistentNaming
        public bool IsNetwork
        {
            get { return Direction != NetworkDirection.Na; }
        }

        private void Process()
        {
            // If the first line is not of the expected format, then none of our parsing will find any real value - Sara
            if (string.IsNullOrEmpty(_line) || _line[0] != '<')
                return;
            Properties = new List<DynamicProperty>();
        }
        /// <summary>
        /// Adds a Dynamic property to the class
        /// This property will be visible in the Property Grid
        /// </summary>
        /// <param name="category"></param>
        /// <param name="description">The description that will appear when you hover over the property</param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddProperty(string category, string description, string name, object value)
        {
            Properties.Add(new DynamicProperty(category, description, name, value));
        }

        [Browsable(false)]
        public List<int> EventIds { get; set; }
        [Category("EventPattern")]
        public int EventMatches { get; set; }
        [Browsable(false)]
        public NetworkDirection Direction { get; set; }
        [Browsable(false)]
        public bool IsDocumentated { get; set; }
        [Browsable(false)]
        public string HighlightColor { get; set; }

        /// <summary>
        /// Returns a property value with the given name ignoring case
        /// </summary>
        public string FindPropertyValue(string name)
        {
            return Properties.Where(value => String.Equals(value.Name, name, StringComparison.CurrentCultureIgnoreCase)).Select(value => (string)value.Value).FirstOrDefault();
        }
        /// <summary>
        /// Returns a property with the given name ignoring case
        /// </summary>
        public DynamicProperty FindProperty(string name)
        {
            return Properties.Where(value => String.Equals(value.Name, name, StringComparison.CurrentCultureIgnoreCase)).Select(n => n).FirstOrDefault();
        }

        /// <summary>
        /// Removes a property by name
        /// </summary>
        /// <param name="name"></param>
        public void RemoveProperty(string name)
        {
            var prop = Properties.SingleOrDefault(n => n.Name == name);
            if (prop == null)
                return;
            Properties.Remove(prop);
        }
    }

    public class PropertyLookup : LogPropertyBaseModel
    {
        public PropertyLookup()
        {
            Categories = new List<Category>();
        }

        public PropertyLookup(LogPropertyBaseModel property)
        {
            Line = property.Line;
            Property = property;
            Categories = new List<Category>();
        }

        [Browsable(false)]
        public LogPropertyBaseModel Property { get; set; }

        [Browsable(false)]
        public string Documentation { get; set; }

        [Category("EventPattern")]
        public string Category { get; set; }

        [Browsable(false)]
        public List<Category> Categories { get; set; }

        [Category("EventPattern")]
        public string EndingEvent { get; set; }

        [Browsable(false)]
        public EventLR Event { get; set; }

        [Browsable(false)]
        public string SourceType { get; set; }
    }
}
