using System;
using System.Reflection;
using Sara.Common.DateTimeNS;
using Sara.Logging;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.Service;
using Sara.LogReader.WinForm.Views.Events;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class EventViewModel : ViewModelBase<EventView, EventCacheData, EventModel>, IViewModelBaseNonGeneric
    {
        public EventViewModel()
        {
            var sw = new Stopwatch("Constructor EventViewModel");
            View = new EventView { ViewModel = this };
            GoToEventIdEvent += View.GoToEventId;
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }

        public event Action<int> GoToEventIdEvent;
        /// <summary>
        /// Navigates to the Value based on the valueId
        /// </summary>
        public void GoToEventId(int eventId)
        {
            if (GoToEventIdEvent == null) return;

            if (!View.Visible)
                MainViewModel.ShowEventWindow();

            GoToEventIdEvent(eventId);
        }

        public override EventCacheData GetModel()
        {
            return EventService.GetModel();
        }

        public void Add(EventLR e)
        {
            var model = EventService.Add();
            model.Item = e;
            Add(model);
        }

        public void Add()
        {
            Add(EventService.Add());
        }
        private void Add(EventModel model)
        {
            var window = new AddEditEventView { ViewModel = this };
            window.Render(model);
            window.ShowDialog();
        }
        public void Edit(int id)
        {
            var model = EventService.Edit(id);
            var window = new AddEditEventView { ViewModel = this };
            window.Render(model);
            window.ShowDialog();
        }
        public void Delete(int id)
        {
            try
            {
                View.StatusUpdate(StatusModel.Update("Deleting..."));
                Log.WriteTrace("Deleting a EventPattern", typeof(EventViewModel).FullName, MethodBase.GetCurrentMethod().Name);
                EventService.Delete(id);
                RenderDocument();
            }
            finally
            {
                View.StatusUpdate(StatusModel.Completed);
            }
        }
        public void Save(EventModel model)
        {
            EventService.Save(model);
            
            if (View == null) return;
            Render(model);
        }

        public string GetCategoryText(EventLR item)
        {
            return EventService.GetCategoryText(item);
        }

    }
}
