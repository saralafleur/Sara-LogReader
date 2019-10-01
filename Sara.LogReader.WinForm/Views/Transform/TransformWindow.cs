using System;
using System.Linq;
using System.Windows.Forms;
using Sara.LogReader.Model.HideOptionNS;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.CRUD;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.HideOptions
{
    public partial class HideOptionsWindow : ToolWindow, IViewDock<HideOptionsModel, HideOptionModel>
    {
        protected internal HideOptionViewModel ViewModel { get; set; }

        public HideOptionsWindow()
        {
            InitializeComponent();
            ColorService.Setup(this);
        }

        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void Render(HideOptionsModel model)
        {
            // Note: Render is handled by ViewModelBase - Sara
            lbTransforms.Items.Clear();
            foreach (var item in model.Options.OrderBy(n => n.Name))
            {
                lbTransforms.Items.Add(item);
            }
            StatusUpdate(StatusModel.Completed);
        }
        public void UpdateView(HideOptionModel model)
        {
            switch (model.Mode)
            {
                case InputMode.Add:
                    lbTransforms.Items.Add(model.Item);
                    lbTransforms.SelectedIndex = lbTransforms.Items.Count - 1;
                    break;
                case InputMode.Edit:
                    var index = lbTransforms.SelectedIndex;
                    lbTransforms.Items[index] = model.Item;
                    break;
                case InputMode.Delete:
                    lbTransforms.Items.Remove(model.Item);
                    break;
            }

            InvalidateButtons();
            StatusUpdate(StatusModel.Completed);
        }
        public void StatusUpdate(IStatusModel model)
        {
            StatusPanel.StatusUpdate(model);
        }

        private void lbCategories_Enter(object sender, EventArgs e)
        {
            InvalidateButtons();
        }
        private void lbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvalidateButtons();
        }
        private void InvalidateButtons()
        {
            btnEdit.Enabled = btnDelete.Enabled = lbTransforms.Items.Count > 0 && lbTransforms.SelectedIndex > -1;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            ViewModel.Edit(((HideOption)lbTransforms.SelectedItem).HideOptionId);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ViewModel.Add();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(@"Are you sure you want to delete this item?", @"Confirm Delete!",
                                                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                ViewModel.Delete(((HideOption)lbTransforms.SelectedItem).HideOptionId);
            }
        }
    }
}
