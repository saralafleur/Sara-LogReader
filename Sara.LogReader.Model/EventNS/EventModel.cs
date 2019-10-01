using System;
using System.Collections.Generic;
using Sara.LogReader.Model.Categories;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Model.EventNS
{
    public class EventModel
    {
        public InputMode Mode { get; set; }
        public Action<EventModel> SaveEventEvent;
        public EventLR Item { get; set; }
        public List<EventLR> Events { get; set; }
        /// <summary>
        /// List of Categories in the system
        /// </summary>
        public List<Category> Categories { get; set; }
        /// <summary>
        /// List of SourceTypes in the system
        /// </summary>
        public List<string> SourceTypes { get; set; } 
    }
}
