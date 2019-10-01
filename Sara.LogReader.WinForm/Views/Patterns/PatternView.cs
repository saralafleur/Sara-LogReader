using System;
using System.Windows.Forms;
using Sara.LogReader.WinForm.Views.ToolWindows;
using System.Drawing;
using System.Linq;
using Sara.LogReader.Model.PatternNS;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.Common;
using Sara.WinForm.CRUD;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Patterns
{
    public partial class PatternView : ToolWindow, IViewDock<PatternsModel, PatternModel>
    {
        protected internal PatternViewModel ViewModel { get; set; }
        public PatternView()
        {
            InitializeComponent();
            IDE.OnSave += Save;
            IDE.OnFullScreen += FullScreen;
            ColorService.Setup(this);
        }

        private void Save()
        {
            if (lbPatterns.SelectedItem == null)
                return;

            var detail = lbPatterns.SelectedItem as Pattern;
            if (detail == null)
                throw new Exception("Detail must be of type Pattern!");

            StatusPanel.SP_FullScreen = false;
            StatusPanel.StatusUpdate(StatusModel.Update("Saving"));
            try
            {
                IDE.SaveEnabled = false;

                detail.Script = IDE.Script.Replace(Environment.NewLine, "\n");
                var model = new PatternModel() { Item = detail, Mode = InputMode.Edit };
                ViewModel.SavePattern(model);
            }
            finally
            {
                StatusPanel.StatusUpdate(StatusModel.Completed);
                StatusPanel.SP_FullScreen = true;
            }
        }

        private void FullScreen(bool value)
        {
            scBody.Panel1Collapsed = !value;
            pnlTop.Visible = value;
        }

        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        internal void ListenToEventChange()
        {
            IDE.Render();
        }

        public void UpdateView(PatternModel model)
        {
            switch (model.Mode)
            {
                case InputMode.Add:
                    lbPatterns.Items.Add(model.Item);
                    lbPatterns.SelectedIndex = lbPatterns.Items.Count - 1;
                    break;
                case InputMode.Edit:
                    var index = lbPatterns.SelectedIndex;
                    lbPatterns.Items[index] = model.Item;
                    break;
                case InputMode.Delete:
                    lbPatterns.Items.Remove(model.Item);
                    break;
            }

            InvalidateButtons();
        }
        public void Render(PatternsModel model)
        {
            // Note: Invoke is handled by ViewModelBase - Sara

            lbPatterns.Items.Clear();
            foreach (var item in model.Patterns.OrderBy(n => n.Name))
                lbPatterns.Items.Add(item);

            IDE.Render();

            StatusUpdate(StatusModel.Completed);
        }
        internal void RunScriptsOnAll()
        {
            IDE.RunScriptsOnAll(IDE.Pattern);
        }
        internal void RunScripts()
        {
            IDE.RunScripts();
        }
        public void StatusUpdate(IStatusModel model)
        {
            StatusPanel.StatusUpdate(model);
        }
        private void lbPatterns_Enter(object sender, EventArgs e)
        {
            InvalidateButtons();
            //RenderDetail();
        }
        private void RenderDetail()
        {
            if (lbPatterns.SelectedItem == null)
                return;

            var detail = lbPatterns.SelectedItem as Pattern;
            if (detail == null) 
                throw new Exception("Detail must be of type Pattern!");
            IDE.Script = detail.Script.Replace("\n", Environment.NewLine);
            IDE.Pattern = detail;
            IDE.SaveEnabled = false;
        }

        internal Color OverlayColor(int iLine)
        {
            return IDE.OverlayColor(iLine);
        }

        bool _skip = false;
        int _lastIndex = 0;
        private void lbPatterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_skip)
            {
                _skip = false;
                return;
            }
            if (IDE.SaveEnabled && MessageBox.Show("The Pattern has changed, do you want to return and save those changes?", "Cancel", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _skip = true;
                lbPatterns.SelectedIndex = _lastIndex;
                return;
            }

            _lastIndex = lbPatterns.SelectedIndex;

            InvalidateButtons();
            RenderDetail();
        }
        private void InvalidateButtons()
        {
            btnEdit.Enabled = btnDelete.Enabled = lbPatterns.Items.Count > 0 && lbPatterns.SelectedIndex > -1;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            ViewModel.EditPattern(((Pattern)lbPatterns.SelectedItem).PatternId);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ViewModel.AddPattern();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(@"Are you sure you want to delete this item?", @"Confirm Delete!",
                                                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                StatusUpdate(StatusModel.FullScreenOff);
                StatusUpdate(StatusModel.Update("Deleting an Item"));
                ViewModel.DeletePattern(((Pattern)lbPatterns.SelectedItem).PatternId);
                StatusUpdate(StatusModel.Completed);
                StatusUpdate(StatusModel.FullScreenOn);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
        }
        private void tbScript_TextChanged(object sender, EventArgs e)
        {
            IDE.SaveEnabled = true;
        }

        private void PatternView_Paint(object sender, PaintEventArgs e)
        {
            UICommon.AddWindowBorder(e, this);
        }

        private void PatternView_Load(object sender, EventArgs e)
        {
        }
    }
}
