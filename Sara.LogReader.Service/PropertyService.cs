using System;
using System.Collections.Generic;
using System.Linq;
using Sara.LogReader.Common;
using Sara.LogReader.Model;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.NetworkMapping;
using Sara.LogReader.Model.Property;

namespace Sara.LogReader.Service
{
    public static class PropertyService
    {
        /// <summary>
        /// Returns a Property object that combines all Events into a single data object
        /// </summary>
        public static LogPropertyBaseModel GetProperty(LineArgs model)
        {
            return LookupEvent(model.Path, new LogPropertyBaseModel { Line = model.Line, iLine = model.iLine });
        }
        /// <summary>
        /// Returns a property model for the existing line
        /// Loads the first EventPattern that matches into the property object
        /// Loads any values found for the given line into the property object
        /// </summary>
        /// <returns></returns>
        private static LogPropertyBaseModel LookupEvent(string path, LogPropertyBaseModel property)
        {
            var file = XmlDal.CacheModel.GetFile(path);

            PropertyLookup item = null;
            var eventMatches = 0;
            var eventIds = new List<int>();
            var values = new List<EventValue>();
            var storedEvents = new List<EventLR>();

            string highlightColor = null;
            foreach (var e in EventService.GetModel().Events.Where(n => n.ContainsSourceType(file.SourceType)))
            {
                if (!RegularExpression.HasMatch(property.Line, e.RegularExpression))
                    continue;

                storedEvents.Add(e);

                if (e.HighlightColor != null && e.HighlightColor != "Transparent")
                {
                    highlightColor = e.HighlightColor;
                }

                foreach (var value in e.EventValues)
                {
                    values.Add(new EventValue
                    {
                       PropertyCategory = value.PropertyCategory,
                       PropertyDescription = value.PropertyDescription,
                       PropertyName =  value.PropertyName,
                       Value = RegularExpression.GetFirstValue(property.Line, value.RegularExpression)
                    });
                }

                // If the last EventPattern found has IgnoredDocumentation, then display the next EventPattern in the Property Window
                if (item == null || item.Event.IgnoreDocumentation)
                {
                    var oldItem = item;

                    item = new PropertyLookup(property)
                    {
                        Documentation = e.Documentation,
                        SourceType = e.SourceType,
                        iLine = property.iLine,
                        Properties = oldItem != null ? oldItem.Properties : new List<DynamicProperty>(),
                        Direction = oldItem != null ? oldItem.Direction : NetworkDirection.Na
                    };

                    eventMatches++;
                    eventIds.Add(e.EventId);
                    LoadEvent(item, e);
                    LoadFileValues(file, item);
                    item.IsDocumentated = !item.Event.IgnoreDocumentation;
                    item.HighlightColor = e.HighlightColor;
                }
                else
                {
                    eventMatches++;
                    eventIds.Add(e.EventId);
                }

                if (e.Network == NetworkDirection.Na) continue;

                item.Direction = e.Network;
                item.AddProperty(Keywords.NETWORK, "Direction of the Network Message", Keywords.NETWORK_DIRECTION, NetworkHelper.NetworkDirectionToString(e.Network));
            }



            if (item != null)
                property = item;

            property.EventMatches = eventMatches;
            property.EventIds = eventIds;
            property.HighlightColor = highlightColor;

            foreach (var value in values)
            {
                property.AddProperty(value.PropertyCategory, value.PropertyDescription, value.PropertyName, value.Value);
            }

            LoadLookupValues(new LookupArgs
            {
                FilePath = path,
                SourceProperty = property,
            }, storedEvents);
            LoadFileValues(file, property);
            return property;
        }

        private class LookupArgs
        {
            public LogPropertyBaseModel SourceProperty { get; set; }
            public EventLookupValue LookupValue { get; set; }
            public EventLR Event { get; set; }
            public string FilePath { get; set; }
        }

        private static void LoadLookupValues(LookupArgs args, IEnumerable<EventLR> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                args.Event = e;
                foreach (var lookupValue in args.Event.EventLookupValues)
                {
                    args.LookupValue = lookupValue;
                    CheckLookupCondition(args);
                }
            }
        }
        private static void CheckLookupCondition(LookupArgs args)
        {
            bool? success = null;
            foreach (var condition in args.LookupValue.Conditions)
            {
                var prop = args.SourceProperty.FindProperty(condition.Name);
                if (prop == null)
                    continue;

                switch (condition.Operator)
                {
                    case Keywords.EQUAL:
                        success = String.Equals(condition.Value, prop.Value == null ? "" : prop.Value.ToString(),
                            StringComparison.CurrentCultureIgnoreCase);
                        break;
                    case Keywords.NOT_EQUAL:
                        success = !String.Equals(condition.Value, prop.Value == null ? "" : prop.Value.ToString(),
                            StringComparison.CurrentCultureIgnoreCase);
                        break;
                }
                if (success != null && (bool)!success)
                    break;
            }
            if (success != null && (bool)success)
            {
                SearchForLookupValue(args);
            }
        }
        private static void SearchForLookupValue(LookupArgs args)
        {
            var file = XmlDal.CacheModel.GetFile(args.FilePath);

            if (args.LookupValue.OnlyNetworkValues)
            {
                lock (file.Network)
                {
                    foreach (var networkMessageInfo in file.Network.NetworkMessages)
                    {
                        if (networkMessageInfo.Source.iLine == args.SourceProperty.iLine)
                            continue;

                        bool? success = null;
                        foreach (var criteria in args.LookupValue.Criteria)
                        {
                            var targetValue = networkMessageInfo.Source.FindEventValue(criteria.TargetName);
                            switch (criteria.CriteriaType)
                            {
                                case LookupTargetType.Value:
                                    switch (criteria.Operator)
                                    {
                                        case Keywords.EQUAL:
                                            success = String.Equals(criteria.TargetValue ?? "", targetValue ?? "",
                                                StringComparison.CurrentCultureIgnoreCase);
                                            break;
                                        case Keywords.NOT_EQUAL:
                                            success =
                                                !String.Equals(criteria.TargetValue ?? "", targetValue ?? "",
                                                    StringComparison.CurrentCultureIgnoreCase);
                                            break;
                                    }
                                    break;
                                case LookupTargetType.Name:
                                    var sourceProperty = args.SourceProperty.FindProperty(criteria.SourceName);
                                    success =
                                        String.Equals(
                                            sourceProperty.Value == null ? "" : sourceProperty.Value.ToString(),
                                            targetValue, StringComparison.CurrentCultureIgnoreCase);
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                            if ((bool)!success)
                                break;
                        }
                        if (success == null || !(bool)success) continue;

                        foreach (var valueName in args.LookupValue.ValueNames)
                        {
                            var sourceProperty = args.SourceProperty.FindProperty(valueName.Name);
                            if (sourceProperty == null)
                                continue;

                            var targetProperty = networkMessageInfo.Source.FindEventValue(valueName.Name);
                            if (targetProperty == null)
                                continue;

                            sourceProperty.Name = string.Format("_{0}", sourceProperty.Name);

                            var category = args.LookupValue.UseCategory
                                ? args.LookupValue.Category
                                : sourceProperty.Category;
                            args.SourceProperty.Properties.Add(new DynamicProperty(category, sourceProperty.Desc,
                                valueName.Name, targetProperty));
                        }
                        break;
                    }
                }
            }
            else
            {
                switch (args.LookupValue.LookupDirection)
                {
                    case LookupDirection.Prior:
                        for (var i = args.SourceProperty.iLine - 1; i >= 0; i--)
                        {
                            var searchProperty = PropertyService.GetProperty(new LineArgs { iLine = i, Path = args.FilePath });

                            bool? success = null;
                            foreach (var criteria in args.LookupValue.Criteria)
                            {
                                var targetValue = FindFirstEventValue(file.GetLine(i), criteria.TargetName);
                                if (targetValue == null)
                                    continue;

                                switch (criteria.CriteriaType)
                                {
                                    case LookupTargetType.Value:

                                        success = String.Equals(criteria.TargetValue, targetValue, StringComparison.CurrentCultureIgnoreCase);
                                        break;
                                    case LookupTargetType.Name:
                                        var sourceProperty = args.SourceProperty.FindProperty(criteria.SourceName);
                                        success = String.Equals(sourceProperty.Value.ToString(), targetValue, StringComparison.CurrentCultureIgnoreCase);
                                        break;
                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }

                                if ((bool)!success)
                                    break;
                            }
                            if (success == null || !(bool)success) continue;

                            foreach (var valueName in args.LookupValue.ValueNames)
                            {
                                var sourceProperty = args.SourceProperty.FindProperty(valueName.Name);
                                if (sourceProperty == null)
                                    continue;

                                var targetProperty = searchProperty.FindProperty(valueName.Name);
                                if (targetProperty == null)
                                    continue;

                                sourceProperty.Name = string.Format("_{0}", sourceProperty.Name);

                                var category = args.LookupValue.UseCategory ? args.LookupValue.Category : sourceProperty.Category;
                                args.SourceProperty.Properties.Add(new DynamicProperty(category, sourceProperty.Desc, valueName.Name, sourceProperty.Value));
                            }
                            break;
                        }

                        break;
                    case LookupDirection.Next:
                    case LookupDirection.TopDown:
                    case LookupDirection.BottomUp:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        /// <summary>
        /// Loads any values that were pulled for the given line into the property object
        /// </summary>
        private static void LoadFileValues(FileData file, LogPropertyBaseModel p)
        {
            if (file == null)
                return;

            foreach (var item in file.FileValues.Where(item => item.iLine == p.iLine))
            {
                p.AddProperty(Keywords.FILE_VALUES, "", item.Name, item.Value);
            }
        }
        /// <summary>
        /// Loads a single EventPattern into the Property object
        /// </summary>
        private static void LoadEvent(PropertyLookup p, EventLR value)
        {
            p.Event = value;
            if (value == null) return;

            p.EventIds.Add(value.EventId);
            var data = XmlDal.DataModel;

            p.Category = EventService.GetCategoryText(value);
            p.Categories = value.Categories;

            var e = data.GetEvent(value.FoldingEventId);
            if (e != null)
                p.EndingEvent = e.ToString();
        }
        private static string FindFirstEventValue(string line, string name)
        {
            return (from e in XmlDal.DataModel.EventsModel.Events
                    where RegularExpression.HasMatch(line, e.RegularExpression)
                    where e.EventValues.Exists(n => n.PropertyName == name)
                    select e.GetSingleEventValue(name, line)).FirstOrDefault();
        }

    }
}
