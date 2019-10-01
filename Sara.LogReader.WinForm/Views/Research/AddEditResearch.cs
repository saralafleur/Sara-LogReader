using System;
using System.Windows.Forms;
using Sara.LogReader.Common;
using Sara.LogReader.Model.Categories;
using Sara.LogReader.Model.ResearchNS;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.WinForm.Views.Values
{
    public partial class AddEditResearch : Form
    {
        private ResearchViewModel ViewModel { get; set; }
        public ResearchModel Model { get; set; }

        public AddEditResearch(ResearchViewModel viewModel)
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
            StatusPanel.DoAction("Saving...", delegate {
                var i = Model.Item;

                i.Categories.Clear();
                foreach (var checkedItem in clbCategory.CheckedItems)
                {
                    i.Categories.Add((Category)checkedItem);
                }

                i.Name = cbValueName.Text;
                ViewModel.Save(Model);

                Close();
            });
        }

        public void Render(ResearchModel model)
        {
            Model = model;
            var i = model.Item;
            cbValueName.Items.Clear();
            foreach (var item in Keywords.GetKeyWords())
            {
                cbValueName.Items.Add(item);
            }
            cbValueName.Text = i.Name;

            clbCategory.Items.Clear();
            foreach (var item in model.Categories)
            {
                clbCategory.Items.Add(item);

                foreach (var category in model.Item.Categories)
                {
                    if (item.CategoryId == category.CategoryId)
                        clbCategory.SetItemChecked(clbCategory.Items.Count - 1, true);
                }
            }

            switch (model.Mode)
            {
                case InputMode.Add:
                    Text = @"Add Research";
                    break;
                case InputMode.Edit:
                    Text = @"Edit Research";
                    break;
            }
        }
    }
}
