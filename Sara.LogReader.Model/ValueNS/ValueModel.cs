using System;
using System.Collections.Generic;
using Sara.LogReader.Model.Categories;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Model.ValueNS
{
    public class ValueModel
    {
        public InputMode Mode { get; set; }
        public Action<ValueModel> SaveEvent;
        public Value Item { get; set; }
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
