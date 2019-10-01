using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sara.LogReader.Model.NetworkMapping;
using Sara.LogReader.Model.ValueNS;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm;
using Sara.WinForm.CRUD;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.NetworkMapping
{
    public partial class AddEditNetworkMap : Form
    {
        private NetworkMapingViewModel ViewModel { get; set; }
        public NetworkMapModel Model { get; set; }

        public AddEditNetworkMap(NetworkMapingViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DialogResult = DialogResult.OK;
                StatusPanel.StatusUpdate(StatusModel.Update("Saving..."));
                var i = Model.Item;

                i.RegularExpression = tbExpression.Text;
                i.Name = tbCriteriaName.Text;
                int priority;
                if (int.TryParse(nudPriority.Text, out priority))
                    i.Priority = priority;
                i.OnlyUseFallThrough = cbOnlyUseFallthrough.Checked;
                i.Criteria.Clear();
                foreach (MapCriteria value in lbCriteria.Items)
                {
                    i.Criteria.Add(value);
                }
                i.Enabled = cbEnabled.Checked;
                ViewModel.Save(Model);

                Close();
            }
            finally
            {
                StatusPanel.StatusUpdate(StatusModel.Completed);
                Cursor.Current = Cursors.Default;
            }
        }

        public void Render(NetworkMapModel model)
        {
            Model = model;
            var i = model.Item;
            tbExpression.Text = i.RegularExpression;
            tbCriteriaName.Text = i.Name;
            nudPriority.Text = i.Priority.ToString();
            cbOnlyUseFallthrough.Checked = i.OnlyUseFallThrough;
            cbEnabled.Checked = i.Enabled;
            switch (model.Mode)
            {
                case InputMode.Add:
                    Text = @"Add Network Map";
                    break;
                case InputMode.Edit:
                    Text = @"Edit Network Map";
                    break;
            }
            RenderCriteria(i.Criteria);
        }

        private void RenderCriteria(IEnumerable<MapCriteria> model)
        {
            try
            {
                lbCriteria.BeginUpdate();
                lbCriteria.Items.Clear();
                foreach (var value in model)
                {
                    lbCriteria.Items.Add(value);
                }
            }
            finally
            {
                lbCriteria.EndUpdate();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            TestRegularExpression(OnUpdateExpression);
        }

        private void TestRegularExpression(Action<Value> callback)
        {
            var testWindow = new RegularExpressionTesterWindow
            {
                Model =
                {
                    Expression = tbExpression.Text,
                }
            };
            testWindow.ViewModel.UpdateRegularExpressionEvent += callback;
            testWindow.ShowDialog();
        }

        private void OnUpdateExpression(Value model)
        {
            tbExpression.Text = model.Expression;
        }

        private void btnAddValue_Click(object sender, EventArgs e)
        {
            var window = new AddEditNetworkMapCriteria();
            window.Render(new NetworkMapCriteriaModel
            {
                Item = new MapCriteria
                {
                    SourceType = MappingDataType.EventValue,
                    TargetType = MappingDataType.EventValue
                },
                Mode = InputMode.Add,
                SaveMapCriteriaEvent = MapCriteriaSave
            });
            window.ShowDialog();
        }

        private void MapCriteriaSave(NetworkMapCriteriaModel model)
        {
            switch (model.Mode)
            {
                case InputMode.Add:
                    lbCriteria.Items.Add(model.Item);
                    break;
                case InputMode.Edit:
                    lbCriteria.SelectedItem = model.Item;
                    ListBoxHelper.UpdateToString(lbCriteria);
                    break;
            }
        }

        private void btnEditValue_Click(object sender, EventArgs e)
        {
            if (lbCriteria.SelectedItem == null)
            {
                MessageBox.Show(@"You must select a criteria first");
                return;
            }

            var window = new AddEditNetworkMapCriteria();
            window.Render(new NetworkMapCriteriaModel
            {
                Item = lbCriteria.SelectedItem as MapCriteria,
                Mode = InputMode.Edit,
                SaveMapCriteriaEvent = MapCriteriaSave
            });
            window.ShowDialog();
        }

        private void btnDeleteValue_Click(object sender, EventArgs e)
        {
            if (lbCriteria.SelectedItem == null)
            {
                MessageBox.Show(@"You must select a criteria first");
                return;
            }

            if (lbCriteria.SelectedItem != null) lbCriteria.Items.Remove(lbCriteria.SelectedItem);
        }
    }
}
