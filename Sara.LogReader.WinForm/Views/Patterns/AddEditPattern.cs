using System;
using System.Windows.Forms;
using Sara.LogReader.Model.PatternNS;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.ControlsNS;
using Sara.WinForm.CRUD;

namespace Sara.LogReader.WinForm.Views.Patterns
{
    public partial class AddEditPattern : Form
    {
        private PatternModel Model { get; set; }
        public PatternViewModel ViewModel { get; set; }

        public AddEditPattern()
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
            if (string.IsNullOrEmpty(tbName.Text))
            {
                ErrorPanel.ShowError(this, "You must enter a Name", null);
                return;
            }

            Model.Item.Name = tbName.Text;

            ViewModel.SavePattern(Model);

            Close();
        }
        
        public void Render(PatternModel model)
        {
            switch (model.Mode)
            {
                case InputMode.Add:
                    Text = @"Add Pattern";
                    break;
                case InputMode.Edit:
                    Text = @"Edit Pattern";
                    break;
            }

            Model = model;
            tbName.Text = Model.Item.Name;
        }

        private void AddEditPattern_Shown(object sender, EventArgs e)
        {
            ColorService.Setup(this);
        }
    }
}