using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Sara.Common.Extension;
using Sara.LogReader.Common;
using Sara.LogReader.Model.Categories;
using Sara.LogReader.Model.DocumentMap;
using Sara.LogReader.Model.Property;

namespace Sara.LogReader.Model.EventNS
{
    public class EventLR
    {
        #region Properties
        /// <summary>
        /// Name displayed in the Document Map
        /// <remarks>
        /// This use to be DocumentMapName, however a more generic Name made more sense
        /// </remarks>
        /// </summary>
        [DisplayName("Name")]
        public string Name { get; set; }
        [Browsable(false)]
        public int EventId { get; set; }
        /// <summary>
        /// Regular Expression used to determine if the log matches this EventPattern
        /// </summary>
        [DisplayName("Expression")]
        public string RegularExpression { get; set; }
        /// <summary>
        /// True when a gap in the logging is normal before this message is displayed. - Sara LaFleur
        /// </summary>
        [Browsable(false)]
        public bool GapNormal { get; set; }
        [Browsable(false)]
        public string Documentation { get; set; }
        [Browsable(false)]
        public int FoldingEventId { get; set; }
        /// <summary>
        /// When True matches will appear in the Document map as a bookmark
        /// </summary>
        [Browsable(false)]
        public bool DocumentMap { get; set; }
        [Browsable(false)]
        public DocumentMapLevel Level { get; set; }
        /// <summary>
        /// When True the time duration from the Parent to this node will be displayed in the Document Map
        /// </summary>
        [Browsable(false)]
        public bool DisplayDurationFromParent { get; set; }
        /// <summary>
        /// When True the time duration from the Parent to this node will be displayed in the Document Map
        /// </summary>
        [Browsable(false)]
        public bool DisplayDurationFromSibling { get; set; }
        /// <summary>
        /// When true this EventPattern will be ignored by the Document View and the Property window when possible
        /// The red mark left of each line in the log will not appear for this EventPattern
        /// The Property window will move to another EventPattern if found and not show this EventPattern in the Property Window
        /// </summary>
        [Browsable(false)]
        public bool IgnoreDocumentation { get; set; }
        /// <summary>
        /// Used to indicate that this message is a network message
        /// When NULL the message is not a network message
        /// </summary>
        //[Browsable(false)]
        public NetworkDirection Network { get; set; }
        /// <summary>
        /// When value is Transparent, then the log line is left alone
        /// When the value is a valid color then this will be used to highlight the line
        /// </summary>
        [Browsable(false)]
        public string HighlightColor { get; set; }
        [Browsable(false)]
        public string DocumentMapHighlightColor { get; set; }
        /// <summary>
        /// Used to tag Events into a Filter Category that can be used by the DocumentMap 
        /// </summary>
        [Browsable(false)]
        public bool DocumentMapFiltered
        {
            get
            {
                return (from category
                            in Categories
                        from category1
                            in XmlDal.DataModel.CategoriesModel.Categories
                        where category.Name == category1.Name
                           && category1.Checked
                        select category).Any();
            }
        }
        /// <summary>
        /// When true the DocumentMapName(Name) will not be used in data analaysis
        /// I.e. The Anomilie column name for Patterns
        /// </summary>
        [Browsable(false)]
        public bool IgnoreName { get; set; }
        /// <summary>
        /// Example Text for the EventPattern
        /// </summary>
        [Browsable(false)]
        public string Example { get; set; }
        /// <summary>
        /// When not NULL this EventPattern is used to define the Source Type of the file.
        /// </summary>
        [DisplayName("Source Type")]
        public string SourceType { get; set; }
        /// <summary>
        /// Returns a string of SourceTypes
        /// </summary>
        [DisplayName("Source Filter")]
        public string SourceFilter
        {
            get
            {
                if (SourceTypes.Count == 0)
                    return Keywords.SOURCE_TYPE_UNKNOWN;
                var result = string.Empty;

                foreach (var sourceType in SourceTypes)
                {
                    if (string.IsNullOrEmpty(result))
                    {
                        result = sourceType;
                        continue;
                    }
                    result = $"{result}, {sourceType}";
                }
                return result;
            }
        }
        public string Values
        {
            get
            {
                if (EventValues.Count() == 0) return "";
                return EventValues.Count().ToString();
            }
        }
        public List<EventValue> EventValues { get; set; }
        public List<EventLookupValue> EventLookupValues { get; set; }
        [Browsable(false)]
        public List<string> ValueNames { get; set; }
        [Browsable(false)]
        public List<Category> Categories { get; set; }
        /// <summary>
        /// List of FileTypes that will use the value.
        /// </summary>
        [Browsable(false)]
        public List<string> SourceTypes { get; set; }
        public string Sort { get; set; }
        #endregion

        #region Setup
        public EventLR()
        {
            Level = DocumentMapLevel.Sibling;

            Categories = new List<Category>();
            SourceTypes = new List<string>();
            ValueNames = new List<string>();
            EventValues = new List<EventValue>();
            EventLookupValues = new List<EventLookupValue>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a single EventPattern value based on Value Name
        /// </summary>
        public string GetSingleEventValue(string valueName, string line)
        {
            foreach (EventValue eventValue in EventValues)
            {
                if (eventValue.PropertyName == valueName)
                {
                    var value = Common.RegularExpression.GetFirstValue(line, eventValue.RegularExpression);
                    return value;
                }
            }
            return null;
        }
        /// <summary>
        /// Returns True if the SourceTypes contains ALL or the sourceType parameter.
        /// </summary>
        public bool ContainsSourceType(string sourceType)
        {
            // SourceTypes.Count == 0 is here when there is no Source Type selected - Sara
            return SourceTypes.Count == 0 ||
                   SourceTypes.Contains(Keywords.ALL) ||
                   SourceTypes.Contains(sourceType);
        }
        public override string ToString()
        {
            return $"{(DocumentMap ? Name : RegularExpression)}";
        }
        public EventLR Clone()
        {
            var e = new EventLR();
            e.Clone(this);
            return e;
        }
        public void Clone(EventLR model)
        {
            Name = model.Name;
            Sort = model.Sort;
            EventId = model.EventId;
            RegularExpression = model.RegularExpression;
            GapNormal = model.GapNormal;
            Documentation = model.Documentation;
            FoldingEventId = model.FoldingEventId;
            DocumentMap = model.DocumentMap;
            Level = model.Level;
            DisplayDurationFromParent = model.DisplayDurationFromParent;
            DisplayDurationFromSibling = model.DisplayDurationFromSibling;
            IgnoreDocumentation = model.IgnoreDocumentation;
            Network = model.Network;
            HighlightColor = model.HighlightColor;
            DocumentMapHighlightColor = model.DocumentMapHighlightColor;
            IgnoreName = model.IgnoreName;
            Example = model.Example;
            SourceType = model.SourceType;
            Categories = (List<Category>)model.Categories.Clone();
            SourceTypes = (List<string>)model.SourceTypes.Clone();
            ValueNames = (List<string>)model.ValueNames.Clone();
            EventValues = (List<EventValue>)model.EventValues.Clone();
            EventLookupValues = (List<EventLookupValue>)model.EventLookupValues.Clone();
        }
        #endregion
    }
}
