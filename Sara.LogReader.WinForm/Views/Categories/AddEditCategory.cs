using System;
using System.Windows.Forms;
using Sara.LogReader.Model.Categories;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.WinForm.Views.Categories
{
    public partial class AddEditCategory : Form
    {
        private CategoryModel Model { get; set; }
        public CategoryViewModel ViewModel { get; set; }

        public AddEditCategory()
        {
            InitializeComponent();
            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Model.Item.Name = tbCategory.Text;
            ViewModel.Save(Model);
            Close();
        }
        public void Render(CategoryModel model)
        {
            switch (model.Mode)
            {
                case InputMode.Add:
                    Text = @"Add Category";
                    break;
                case InputMode.Edit:
                    Text = @"Edit Category";
                    break;
            }

            Model = model;
            tbCategory.Text = Model.Item.Name;
        }

        private void AddEditCategory_Shown(object sender, EventArgs e)
        {
            ColorService.Setup(this);
        }
    }
}
