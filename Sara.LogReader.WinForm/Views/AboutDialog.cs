using System;
using System.Reflection;
using System.Windows.Forms;
using Sara.WinForm.ColorScheme.Modal;

namespace Sara.LogReader.WinForm.Views
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
        }
        public string GetAssemblyAttribute<T>(Func<T, string> value)
        where T : Attribute
        {
            var attribute = (T)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(T));
            return value.Invoke(attribute);
        }
        private void AboutDialog_Load(object sender, EventArgs e)
        {
            labelAppVersion.Text = typeof(MainForm).Assembly.GetName().Version.ToString();
            lblCopyRight.Text = GetAssemblyAttribute<AssemblyCopyrightAttribute>(a => a.Copyright);
        }

        private void AboutDialog_Shown(object sender, EventArgs e)
        {
            ColorService.Setup(this);
        }
    }
}