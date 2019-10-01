using System.Collections.Generic;
using Sara.Common.DateTimeNS;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.Model.Property;
using Sara.LogReader.Service;
using Sara.LogReader.WinForm.Views.Property;
using Sara.WinForm.MVVM;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class PropertyViewModel : ViewModelBase<PropertyWindow,LogPropertyBaseModel, object>, IViewModelBaseNonGeneric
    {
        public PropertyViewModel()
        {
            var sw = new Stopwatch("Constructor PropertyViewModel");
            View = new PropertyWindow { ViewModel = this };
            MainViewModel.CurrentLineChangedEvent += RenderDocument;
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }

        ~PropertyViewModel()
        {
            MainViewModel.CurrentLineChangedEvent -= RenderDocument;
        }

        public void DeleteEvent(int eventId)
        {
            MainViewModel.DeleteEvent(eventId);
        }

        public void EditEvent(int eventId)
        {
            MainViewModel.EditEvent(eventId);
        }

        public void AddEvent(EventLR e)
        {
            MainViewModel.AddEvent(e);
        }

        public override LogPropertyBaseModel GetModel()
        {
            return MainViewModel.Current == null ? null : PropertyService.GetProperty(MainViewModel.Current.CurrentLineArgs);
        }

        public List<string> GetValues()
        {
            return EventService.GetValueNames();
        }
    }
}
