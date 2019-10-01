using System;
using System.Windows.Forms;
using Sara.LogReader.Common;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.Model.ValueNS;
using Sara.LogReader.WinForm.ViewModel;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Events
{
    public partial class AddEditEventValueView : Form, IView<EventValueModel, object>
    {
        public EventValueModel Model { get; set; }
        public AddEditEventValueView()
        {
            InitializeComponent();
        }

        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void Render(EventValueModel model)
        {
            Model = model;
            tbRegularExpression.Text = model.Item.RegularExpression;

            cbName.Items.Clear();
            foreach (var item in Keywords.GetKeyWords())
            {
                cbName.Items.Add(item);
            }
            cbName.Text = model.Item.PropertyName;

            
            tbCategory.Text = model.Item.PropertyCategory;
            tbDescription.Text = model.Item.PropertyDescription;
        }

        public void StatusUpdate(IStatusModel model)
        {
            throw new NotImplementedException();
        }

        public void UpdateView(object selectedModel)
        {
            throw new NotImplementedException();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Model.SaveEvent != null)
            {
                Model.Item.RegularExpression = tbRegularExpression.Text;
                Model.Item.PropertyName = cbName.Text;
                Model.Item.PropertyCategory = tbCategory.Text;
                Model.Item.PropertyDescription = tbDescription.Text;
                Model.SaveEvent(Model);
            }
            Close();
        }

        private void btnTestEvent_Click(object sender, EventArgs e)
        {
            RegularExpressionTest.Test(tbRegularExpression.Text, (Value m) => { tbRegularExpression.Text = m.Expression; });
        }
    }
}
