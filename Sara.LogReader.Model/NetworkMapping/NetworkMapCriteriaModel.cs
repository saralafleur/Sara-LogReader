using System;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.Model.NetworkMapping
{
    public class NetworkMapCriteriaModel
    {
        public InputMode Mode { get; set; }
        public Action<NetworkMapCriteriaModel> SaveMapCriteriaEvent;
        public MapCriteria Item { get; set; }
    }
}
