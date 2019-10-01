using System;
using System.Windows.Forms;
using Sara.LogReader.Model.EventNS;
using Sara.WinForm.CRUD;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Events
{
    public partial class AddEditEventLookupValueView : Form
    {
        public EventLookupValueModel Model { get; set; }
        public AddEditEventLookupValueView()
        {
            InitializeComponent();
        }

        public void Render(EventLookupValueModel model)
        {
            Model = model;

            ckbUseCategory.Checked = Model.Item.UseCategory;
            tbCategory.Text = Model.Item.Category;
            ckbOnlyNetwork.Checked = Model.Item.OnlyNetworkValues;

            RefreshCategory();

            CRUDUIService.RenderList(Model.Item.Criteria, lbCriteria);
            CRUDUIService.RenderList(Model.Item.ValueNames, lbValueNames);
            CRUDUIService.RenderList(Model.Item.Conditions, lbConditions);
            CRUDUIService.RenderEnumList<LookupDirection>(cbLookupDirection, Model.Item.LookupDirection);
        }

        public void StatusUpdate(IStatusModel model)
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
                Model.Item.UseCategory = ckbUseCategory.Checked;
                Model.Item.Category = tbCategory.Text;
                Model.Item.OnlyNetworkValues = ckbOnlyNetwork.Checked;
                Model.Item.LookupDirection = CRUDUIService.GetEnumValue<LookupDirection>(cbLookupDirection);
                CRUDUIService.SaveList(Model.Item.Criteria, lbCriteria);
                CRUDUIService.SaveList(Model.Item.ValueNames, lbValueNames);
                CRUDUIService.SaveList(Model.Item.Conditions, lbConditions);

                Model.SaveEvent(Model);
            }
            Close();
        }

        private void ckbUseCategory_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCategory();
        }

        private void btnAddCriteria_Click(object sender, EventArgs e)
        {
            CRUDUIService
                .Add<AddEditEventLookupValueCriteriaView, EventLookupValueCriteriaModel, EventLookupValueCriteria>
                (model => CRUDUIService.Save(lbCriteria, model));
        }

        private void btnEditCriteria_Click(object sender, EventArgs e)
        {
            CRUDUIService
                .Edit<AddEditEventLookupValueCriteriaView, EventLookupValueCriteriaModel, EventLookupValueCriteria>
                (lbCriteria, @"You must select a Criteria first", model => CRUDUIService.Save(lbCriteria, model));
        }

        private void btnDeleteCriteria_Click(object sender, EventArgs e)
        {
            CRUDUIService.Delete(lbCriteria, @"You must select a Criteria first");
        }

        private void btnAddValueName_Click(object sender, EventArgs e)
        {
            CRUDUIService
                .Add<AddEditEventLookupValueValueFieldView, EventLookupValueNameModel, EventLookupValueName>
                (model => CRUDUIService.Save(lbValueNames, model));
        }

        private void btnEditValueName_Click(object sender, EventArgs e)
        {
            CRUDUIService
                .Edit<AddEditEventLookupValueValueFieldView, EventLookupValueNameModel, EventLookupValueName>
                (lbCriteria, @"You must select a Field first", model => CRUDUIService.Save(lbValueNames, model));
        }

        private void btnDeleteValueName_Click(object sender, EventArgs e)
        {
            CRUDUIService.Delete(lbValueNames, @"You must select a Field first");
        }

        private void RefreshCategory()
        {
            lblCategory.Visible = ckbUseCategory.Checked;
            tbCategory.Visible = ckbUseCategory.Checked;
        }

        private void btnAddCondition_Click(object sender, EventArgs e)
        {
            CRUDUIService
                .Add<AddEditEventLookupValueConditionView, EventLookupValueConditionModel, EventLookupValueCondition>
                (model => CRUDUIService.Save(lbConditions, model));
        }

        private void btnEditCondition_Click(object sender, EventArgs e)
        {
            CRUDUIService
                .Edit<AddEditEventLookupValueConditionView, EventLookupValueConditionModel, EventLookupValueCondition>
                (lbCriteria, @"You must select a Condition first", model => CRUDUIService.Save(lbConditions, model));
        }

        private void btnDeleteCondition_Click(object sender, EventArgs e)
        {
            CRUDUIService.Delete(lbConditions, @"You must select a Condition first");
        }
    }
}
