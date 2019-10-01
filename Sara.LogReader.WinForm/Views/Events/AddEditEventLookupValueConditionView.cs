using System;
using System.Windows.Forms;
using Sara.LogReader.Common;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Events
{
    public partial class AddEditEventLookupValueConditionView : Form, IView<EventLookupValueConditionModel, object>
    {
        public EventLookupValueConditionModel Model { get; set; }
        public AddEditEventLookupValueConditionView()
        {
            InitializeComponent();
        }
        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void Render(EventLookupValueConditionModel model)
        {
            Model = model;

            cbName.Items.Clear();
            foreach (var item in Keywords.GetKeyWords())
            {
                cbName.Items.Add(item);
            }
            cbName.Text = model.Item.Name;

            cbOperator.Items.Clear();
            foreach (var item in Keywords.GetOperators())
            {
                cbOperator.Items.Add(item);
            }
            cbOperator.Text = model.Item.Operator;


            tbValue.Text = model.Item.Value;
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
                Model.Item.Name = cbName.Text;
                Model.Item.Value = tbValue.Text;
                Model.Item.Operator = cbOperator.Text;
                Model.SaveEvent(Model);
            }
            Close();
        }
    }
}
