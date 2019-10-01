using System;
using System.Collections.Generic;
using Sara.LogReader.Model.Categories;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Model.ResearchNS
{
    public class ResearchModel
    {
        public InputMode Mode { get; set; }
        public Action<ResearchModel> SaveEvent;
        public Research Item { get; set; }
        /// <summary>
        /// List of Categories in the system
        /// </summary>
        public List<Category> Categories { get; set; }
    }
}
