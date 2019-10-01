using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Sara.LogReader.Model.Settings;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Settings
{
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbSockDrawerFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void SockDrawerSettingsWindow_Load(object sender, EventArgs e)
        {
            Render();
        }

        public void Render()
        {
            var model = SettingsViewModel.GetSettingsModel();
            tbSockDrawerFolder.Text = model.SockDrawerFolder;
            tbNonLazyValueWarningTimeout.Text = model.NonLazyValueWarningTimeout.ToString(CultureInfo.InvariantCulture);
            foreach (var item in model.FileTypes)
                lbFileTypes.Items.Add(item);
            RenderColorScheme();
        }

        private void RenderColorScheme()
        {
            var model = ColorService.ColorScheme;
            cbColorScheme.Items.Clear();
            foreach (var item in model.Collection)
            {
                cbColorScheme.Items.Add(item.Name);
            }

            cbColorScheme.SelectedItem = model.ActiveColorScheme;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            StatusPanel.StatusUpdate(StatusModel.Update("Saving"));
            var fileTypes = (from object item in lbFileTypes.Items select item.ToString()).ToList();

            if (cbColorScheme.SelectedItem != null)
            {
                if (ColorService.ColorScheme.ActiveColorScheme != cbColorScheme.SelectedItem.ToString())
                {
                    ColorService.ColorScheme.ActiveColorScheme = cbColorScheme.SelectedItem.ToString();
                    ColorService.Invalidate();
                }
            }

            SettingsViewModel.Save(new SettingsModel
            {
                SockDrawerFolder = tbSockDrawerFolder.Text,
                NonLazyValueWarningTimeout = int.Parse(tbNonLazyValueWarningTimeout.Text),
                FileTypes = fileTypes
            });
            Close();
            StatusPanel.StatusUpdate(StatusModel.Completed);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            lbFileTypes.Items.Add(tbFileType.Text);
            tbFileType.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lbFileTypes.SelectedItem != null)
                lbFileTypes.Items.RemoveAt(lbFileTypes.SelectedIndex);
        }
        private void btnEditColorScheme_Click(object sender, EventArgs e)
        {
            SettingsViewModel.ShowColorScheme();
            
            RenderColorScheme();   
        }

        private void SettingsWindow_Shown(object sender, EventArgs e)
        {
            ColorService.Setup(this);
        }
    }
}
