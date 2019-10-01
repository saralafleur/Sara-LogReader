using System;
using System.Linq;
using System.Windows.Forms;
using Sara.LogReader.Model.Categories;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.CRUD;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Categories
{
    public partial class CategoryWindow : ToolWindow, IViewDock<CategoriesModel, CategoryModel>
    {
        protected internal CategoryViewModel ViewModel { get; set; }

        public CategoryWindow()
        {
            InitializeComponent();
            ColorService.Setup(this);
        }

        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public void Render(CategoriesModel model)
        {
            // Note: Render is handled by ViewModelBase - Sara
            lbCategories.Items.Clear();
            foreach (var item in model.Categories.OrderBy(n => n.Name))
            {
                lbCategories.Items.Add(item);
            }
            StatusUpdate(StatusModel.Completed);
        }
        public void UpdateView(CategoryModel model)
        {
            switch (model.Mode)
            {
                case InputMode.Add:
                    lbCategories.Items.Add(model.Item);
                    lbCategories.SelectedIndex = lbCategories.Items.Count - 1;
                    break;
                case InputMode.Edit:
                    var index = lbCategories.SelectedIndex;
                    lbCategories.Items[index] = model.Item;
                    break;
                case InputMode.Delete:
                    lbCategories.Items.Remove(model.Item);
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
            btnEdit.Enabled = btnDelete.Enabled = lbCategories.Items.Count > 0 && lbCategories.SelectedIndex > -1;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            ViewModel.Edit(((Category)lbCategories.SelectedItem).CategoryId);
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
                ViewModel.Delete(((Category)lbCategories.SelectedItem).CategoryId);
            }
        }
    }
}
