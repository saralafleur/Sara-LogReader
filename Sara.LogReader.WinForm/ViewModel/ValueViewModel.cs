using System;
using System.Windows.Forms;
using Sara.Common.DateTimeNS;
using Sara.LogReader.Model.ValueNS;
using Sara.LogReader.Service;
using Sara.LogReader.WinForm.Views.Values;
using Sara.WinForm.MVVM;
using AddEditValue = Sara.LogReader.WinForm.Views.Values.AddEditValue;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class ValueViewModel : ViewModelBase<ValuesWindow, ValueCacheData, ValueModel>, IViewModelBaseNonGeneric
    {
        public ValueViewModel()
        {
            var sw = new Stopwatch("Constructor ValueViewModel");
            View = new ValuesWindow { ViewModel = this };
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }

        public override ValueCacheData GetModel()
        {
            return ValueService.GetModel();
        }

        public void Add(Value value)
        {
            var model = ValueService.Add();
            model.Item = value;
            Add(model);
        }
        public void Add()
        {
            Add(ValueService.Add());
        }
        private void Add(ValueModel model)
        {
            var window = new AddEditValue(this);
            window.Render(model);
            window.ShowDialog();
        }
        public void Edit(int id)
        {
            var model = ValueService.Edit(id);
            var window = new AddEditValue(this);
            window.Render(model);
            window.ShowDialog();
        }
        public void Delete(int id)
        {
            var model = ValueService.Delete(id);
            MainViewModel.ValueOrEventChangedUpdateOpenFiles();
            if (View == null) return;
            RenderDocument();
            View.UpdateView(model);
        }
        public void Save(ValueModel model)
        {
            ValueService.Save(model);
            MainViewModel.ValueOrEventChangedUpdateOpenFiles();
            if (View == null) return;
            RenderDocument();
            View.UpdateView(model);
        }
        public event Action<int> GoToValueIdEvent;
        /// <summary>
        /// Navigates to the Value based on the valueId
        /// </summary>
        public void GoToValueId(int valueId)
        {
            if (GoToValueIdEvent == null) return;

            if (!View.Visible)
                MainViewModel.ShowValueWindow();

            GoToValueIdEvent(valueId);
        }
    }
}
