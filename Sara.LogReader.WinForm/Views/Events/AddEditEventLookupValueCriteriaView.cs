using System;
using System.Windows.Forms;
using Sara.LogReader.Common;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.CRUD;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Events
{
    public partial class AddEditEventLookupValueCriteriaView : Form, IView<EventLookupValueCriteriaModel, object>
    {
        public EventLookupValueCriteriaModel Model { get; set; }
        public AddEditEventLookupValueCriteriaView()
        {
            InitializeComponent();
        }

        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void Render(EventLookupValueCriteriaModel model)
        {
            Model = model;

            cbTargetName.Items.Clear();
            foreach (var item in Keywords.GetKeyWords())
            {
                cbTargetName.Items.Add(item);
            }
            cbTargetName.Text = model.Item.TargetName;

            cbOperator.Items.Clear();
            foreach (var item in Keywords.GetOperators())
            {
                cbOperator.Items.Add(item);
            }
            cbOperator.Text = model.Item.Operator;

            CRUDUIService.RenderEnumList<LookupTargetType>(cbCriteriaType, model.Item.CriteriaType);
            RefreshCriteriaType();

            tbTargetValue.Text = model.Item.TargetValue;
            cbTargetName.Text = model.Item.TargetName;

            cbSourceName.Items.Clear();
            foreach (var item in cbTargetName.Items)
            {
                cbSourceName.Items.Add(item);
            }

            cbSourceName.Text = model.Item.SourceName;
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
                Model.Item.TargetName = cbTargetName.Text;
                Model.Item.Operator = cbOperator.Text;
                Model.Item.SourceName = cbSourceName.Text;
                Model.Item.TargetValue = tbTargetValue.Text;
                Model.Item.CriteriaType = CRUDUIService.GetEnumValue<LookupTargetType>(cbCriteriaType);
                Model.SaveEvent(Model);
            }
            Close();
        }

        private void cbCriteriaType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCriteriaType();
        }

        private void RefreshCriteriaType()
        {
            var value = CRUDUIService.GetEnumValue<LookupTargetType>(cbCriteriaType);

            switch (value)
            {
                case LookupTargetType.Value:
                    lblSourceName.Visible = false;
                    cbSourceName.Visible = false;
                    lblTargetValue.Visible = true;
                    tbTargetValue.Visible = true;
                    break;
                case LookupTargetType.Name:
                    lblSourceName.Visible = true;
                    cbSourceName.Visible = true;
                    lblTargetValue.Visible = false;
                    tbTargetValue.Visible = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
