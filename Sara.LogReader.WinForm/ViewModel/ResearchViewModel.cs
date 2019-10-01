using Sara.Common.DateTimeNS;
using Sara.LogReader.Model.ResearchNS;
using Sara.LogReader.Service;
using Sara.LogReader.WinForm.Views.Research;
using Sara.LogReader.WinForm.Views.Values;
using Sara.WinForm.MVVM;

namespace Sara.LogReader.WinForm.ViewModel
{
    public class ResearchViewModel : ViewModelBase<ResearchWindow, ResearchCacheData, ResearchModel>, IViewModelBaseNonGeneric
    {
        public ResearchViewModel()
        {
            var sw = new Stopwatch("Constructor ResearchViewModel");
            View = new ResearchWindow { ViewModel = this };
            sw.Stop(MainViewModel.CONST_ViewModelLimit);
        }

        public override ResearchCacheData GetModel()
        {
            return ResearchService.GetModel();
        }

        public void Add(Research value)
        {
            var model = ResearchService.Add();
            model.Item = value;
            Add(model);
        }
        public void Add()
        {
            Add(ResearchService.Add());
        }
        private void Add(ResearchModel model)
        {
            var window = new AddEditResearch(this);
            window.Render(model);
            window.ShowDialog();
        }
        public void Edit(int id)
        {
            var model = ResearchService.Edit(id);
            var window = new AddEditResearch(this);
            window.Render(model);
            window.ShowDialog();
        }
        public void Delete(int id)
        {
            var model = ResearchService.Delete(id);
            if (View == null) return;
            RenderDocument();
            View.UpdateView(model);
        }
        public void Save(ResearchModel model)
        {
            ResearchService.Save(model);
            if (View == null) return;
            RenderDocument();
            View.UpdateView(model);
        }
    }
}
