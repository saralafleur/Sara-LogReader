using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Sara.Common.DateTimeNS;
using Sara.Logging;
using Sara.LogReader.Common;
using Sara.LogReader.Model;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.Model.Property;
using Sara.LogReader.Service.FileServiceNS;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Service
{
    public static class EventService
    {
        public static EventCacheData GetModel()
        {
            return XmlDal.DataModel.EventsModel;
        }

        public static EventModel Add()
        {
            var e = new EventLR { ValueNames = GetValueNames() };

            return Add(e);
        }

        public static IEnumerable<string> GetEventKeywords()
        {
            var model = XmlDal.DataModel;
            return model.EventsModel.Events.Select(n => n.Name).ToArray();
        }

        public static List<string> GetValueNames()
        {
            return new List<string>
            {
                    string.Empty,
                    Keywords.START,
                    Keywords.STOP
                };
        }

        public static EventModel Add(EventLR item)
        {
            var model = XmlDal.DataModel;
            return new EventModel
            {
                Mode = InputMode.Add,
                SaveEventEvent = Save,
                Item = item,
                Events = model.EventsModel.Events.OrderBy(n => n.ToString()).ToList(),
                Categories = model.CategoriesModel.Categories.OrderBy(n => n.Name).ToList(),
                SourceTypes = model.Options.FileTypes.OrderBy(n => n).ToList(),
            };
        }
        public static EventModel Edit(int eventId)
        {
            var model = XmlDal.DataModel;
            var e = model.GetEvent(eventId).Clone();
            e.ValueNames = GetValueNames();
            return new EventModel
            {
                Mode = InputMode.Edit,
                SaveEventEvent = Save,
                Item = e,
                Events = model.EventsModel.Events.OrderBy(n => n.ToString()).ToList(),
                Categories = model.CategoriesModel.Categories.OrderBy(n => n.Name).ToList(),
                SourceTypes = model.Options.FileTypes.OrderBy(n => n).ToList()
            };
        }
        public static void Delete(int eventId)
        {
            var data = XmlDal.DataModel;
            var model = new EventModel
            {
                Mode = InputMode.Delete,
                Item = data.GetEvent(eventId)
            };
            Save(model);
        }
        public static void ValidEventName(string name, int eventId)
        {
            var data = GetModel();
            var e = data.Events.FirstOrDefault(item => item.Name == name && item.EventId != eventId);
            if (e != null)
                throw new Exception("EventPattern name must be unique!");
            if (name.Contains(' '))
                throw new Exception("Space is not allowed in the EventPattern Name!");
            var regexItem = new Regex(@"^[a-zA-Z0-9\.]+$");
            if (!regexItem.IsMatch(name))
            {
                throw new Exception("Special Characters are not allowed in the EventPattern Name!");
            }
        }
        public static void Save(EventModel model)
        {
            Log.WriteEnter(typeof(FileService).FullName, MethodBase.GetCurrentMethod().Name);
            var stopwatch = new Stopwatch("Save EventPattern");
            try
            {
                var data = XmlDal.DataModel;

                switch (model.Mode)
                {
                    case InputMode.Add:
                        model.Item.EventId = data.GetUniqueEventId();
                        ValidEventName(model.Item.Name, -1);
                        data.EventsModel.Events.Add(model.Item);
                        break;
                    case InputMode.Edit:
                        ValidEventName(model.Item.Name, model.Item.EventId);
                        var item = data.GetEvent(model.Item.EventId);
                        item.Clone(model.Item);
                        break;
                    case InputMode.Delete:
                        data.EventsModel.Events.Remove(model.Item);
                        break;
                }

                XmlDal.Save();
                XmlDal.DataModel.EventCacheDataController.Invalidate();
            }
            finally
            {
                stopwatch.Stop();
            }
        }

        internal static string GetDateTimeRegularExpression(string sourceType)
        {
            foreach (var e in GetModel().Events.Where(ev => ev.ContainsSourceType(sourceType)))
            {
                foreach (var value in e.EventValues)
                {
                    if (value.PropertyName == Keywords.DATETIME)
                        return value.RegularExpression;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns a EventPattern for the given eventId
        /// </summary>
        public static EventLR GetEventById(int eventId)
        {
            var data = GetModel();
            return data.Events.FirstOrDefault(e => e.EventId == eventId);
        }

        /// <summary>
        /// Returns a unique EventPattern.
        /// Throws an Exception if the Name is not unique
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static EventLR GetEventByName(string name)
        {
            var data = GetModel();
            var e = data.Events.Where(n => n.Name == name);
            if (e.Count() == 0)
                return null;
            if (e.Count() > 1)
                throw new Exception("EventPattern Name is not unique!");

            return e.First();
        }
        /// <summary>
        /// Returns the first Name that is not blank
        /// Skips any EventPattern that have "IgnoreName"
        /// </summary>
        public static string GetFirstName(LogPropertyBaseModel property)
        {
            string distinctName = null;
            // Use the first EventPattern that has a Document Map Name
            foreach (var eventId in property.EventIds)
            {
                var e = XmlDal.DataModel.GetEvent(eventId);

                if (e.IgnoreName)
                    continue;

                if (string.IsNullOrEmpty(e.Name))
                    continue;

                distinctName = e.Name;
            }
            return distinctName;
        }

        /// <summary>
        /// Finds the first EventPattern that matches and contains a NETWORKMESSAGENAME value
        /// Then returns the NETWORKMESSAGENAME
        /// </summary>
        public static string FindNetworkMessageName(string line)
        {
            foreach (var e in XmlDal.DataModel.EventsModel.Events)
            {
                if (!RegularExpression.HasMatch(line, e.RegularExpression))
                    continue;

                if (!e.EventValues.Exists(n => n.PropertyName == Keywords.NETWORK_MESSAGE_NAME))
                    continue;

                return e.GetSingleEventValue(Keywords.NETWORK_MESSAGE_NAME, line) ?? Keywords.SOURCE_TYPE_UNKNOWN;
            }
            return null;
        }
        /// <summary>
        /// Returns the categories for the given EventPattern as a comma delimited string.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetCategoryText(EventLR e)
        {
            var dal = XmlDal.DataModel;
            var text = "";
            foreach (var item in e.Categories)
            {
                // ReSharper disable ConvertIfStatementToConditionalTernaryExpression
                if (String.IsNullOrEmpty(text))
                {
                    var category = dal.GetCategory(item.CategoryId);
                    if (category == null) text = "null";
                    else if (category.Name == null) text = "null";
                    else text = category.Name;
                }

                else
                {
                    var name = (dal.GetCategory(item.CategoryId) == null) ? "null" : dal.GetCategory(item.CategoryId).Name ?? "null";
                    text = $"{text}, {name}";
                }
                // ReSharper restore ConvertIfStatementToConditionalTernaryExpression
            }
            return text;
        }

    }
}