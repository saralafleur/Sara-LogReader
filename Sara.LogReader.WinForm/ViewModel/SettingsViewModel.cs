using Sara.LogReader.Model;
using Sara.LogReader.Model.ColorScheme;
using Sara.LogReader.Model.Settings;
using Sara.LogReader.WinForm.Views.Settings;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.ColorScheme.ViewModel;

namespace Sara.LogReader.WinForm.ViewModel
{
    public static class SettingsViewModel
    {
        public static SettingsModel GetSettingsModel()
        {
            return new SettingsModel
            {
                SockDrawerFolder = XmlDal.DataModel.Options.SockDrawerFolder,
                FileTypes = XmlDal.DataModel.Options.FileTypes,
                NonLazyValueWarningTimeout = XmlDal.DataModel.Options.NonLazyValueWarningTimeout
            };
        }

        public static void Save(SettingsModel model)
        {
            XmlDal.DataModel.Options.SockDrawerFolder = model.SockDrawerFolder;
            XmlDal.DataModel.Options.NonLazyValueWarningTimeout = model.NonLazyValueWarningTimeout;
            XmlDal.DataModel.Options.FileTypes = model.FileTypes;

            #region ColorScheme
            XmlDal.DataModel.Options.ColorScheme.Collection.Clear();
            foreach (var item in ColorService.ColorScheme.Collection)
                XmlDal.DataModel.Options.ColorScheme.Collection.Add(item as ColorSchemeLogReaderModal);

            XmlDal.DataModel.Options.ColorScheme.ActiveColorScheme = ColorService.ColorScheme.ActiveColorScheme;
            #endregion ColorScheme

            XmlDal.DataModel.Save();
        }

        public static void ShowSettings()
        {
            var window = new SettingsWindow();
            window.ShowDialog();
        }

        internal static void ShowColorScheme()
        {
            ColorSchemeViewModel.ShowColorScheme();
        }
    }
}
