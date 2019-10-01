using System;
using System.Windows.Forms;
using Sara.LogReader.Common;
using Sara.LogReader.Model.NetworkMapping;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.WinForm.Views.NetworkMapping
{
    public partial class AddEditNetworkMapCriteria : Form
    {
        public NetworkMapCriteriaModel Model { get; set; }

        public AddEditNetworkMapCriteria()
        {
            InitializeComponent();

            tbSourceValue.Visible = cbSourceValue.Checked;
            lblSourceValue.Visible = cbSourceValue.Checked;
            tbTargetValue.Visible = cbTargetValue.Checked;
            lblTargetValue.Visible = cbTargetValue.Checked;
        }

        public void Render(NetworkMapCriteriaModel model)
        {
            Model = model;

            cbOperator.Items.Clear();
            foreach (var item in Keywords.GetOperators())
            {
                cbOperator.Items.Add(item);
            }
            cbOperator.Text = model.Item.Operator;

            cbSourceName.Items.Clear();
            foreach (var item in Keywords.GetKeyWords())
            {
                cbSourceName.Items.Add(item);
            }
            cbSourceName.Text = model.Item.SourceName;

            CRUDUIService.RenderEnumList<MappingDataType>(cbSourceType,model.Item.SourceType);

            cbTargetName.Items.Clear();
            foreach (var item in Keywords.GetKeyWords())
            {
                cbTargetName.Items.Add(item);
            }
            cbTargetName.Text = model.Item.SourceName;

            CRUDUIService.RenderEnumList<MappingDataType>(cbTargetType, model.Item.TargetType);

            tbTimeCondition.Text = model.Item.TimeConditionMs.ToString();
            cbSourceValue.Checked = model.Item.UseSourceValue;
            tbSourceValue.Text = model.Item.SourceValue;
            cbTargetValue.Checked = model.Item.UseTargetValue;
            tbTargetValue.Text = model.Item.TargetValue;
            cbEnabled.Checked = model.Item.Enabled;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Model.SaveMapCriteriaEvent != null)
            {
                Model.Item.SourceName = cbSourceName.Text;
                Model.Item.SourceType = CRUDUIService.GetEnumValue<MappingDataType>(cbSourceType);
                Model.Item.TargetName = cbTargetName.Text;
                Model.Item.TargetType = CRUDUIService.GetEnumValue<MappingDataType>(cbTargetType);
                Model.Item.UseSourceValue = cbSourceValue.Checked;
                Model.Item.SourceValue = tbSourceValue.Text;
                Model.Item.UseTargetValue = cbTargetValue.Checked;
                Model.Item.TargetValue = tbTargetValue.Text;
                Model.Item.Enabled = cbEnabled.Checked;
                Model.Item.Operator = cbOperator.Text;
                try
                {
                    if (string.IsNullOrEmpty(tbTimeCondition.Text))
                        Model.Item.TimeConditionMs = null;
                    else
                        Model.Item.TimeConditionMs = int.Parse(tbTimeCondition.Text);
                }
                catch (FormatException)
                {
                    Model.Item.TimeConditionMs = null;
                }

                Model.SaveMapCriteriaEvent(Model);
            }
            Close();

        }

        private void cbSourceValue_CheckedChanged(object sender, EventArgs e)
        {
            tbSourceValue.Visible = cbSourceValue.Checked;
            lblSourceValue.Visible = cbSourceValue.Checked;
        }

        private void cbTargetValue_CheckedChanged(object sender, EventArgs e)
        {
            tbTargetValue.Visible = cbTargetValue.Checked;
            lblTargetValue.Visible = cbTargetValue.Checked;
        }

    }
}
