using System.Windows.Forms;

namespace Sara.LogReader.WinForm.Views.NetworkReport
{
    public partial class RenameView : Form
    {
        public RenameView()
        {
            InitializeComponent();
        }

        public string NameField
        {
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
