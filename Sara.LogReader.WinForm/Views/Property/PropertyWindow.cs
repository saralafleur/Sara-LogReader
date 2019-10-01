using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Sara.Logging;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.Model.Property;
using Sara.LogReader.Service;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.LogReader.WinForm.ViewModel;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.MVVM;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Property
{
    public partial class PropertyWindow : ToolWindow, IViewDock<LogPropertyBaseModel, object>
    {
        public PropertyViewModel ViewModel { get; set; }

        public PropertyWindow()
        {
            InitializeComponent();
            rtbComment.Text = @"...";
            ShowWarning();
            ColorService.Setup(this);
        }
        public bool StartupReady { get { return MainViewModel.StartupComplete; } }

        public LogPropertyBaseModel Model { get; set; }

        public void Render(LogPropertyBaseModel model)
        {
            // Note: Invoke is handled by ViewModelBase - Sara
            
            try
            {
                Log.WriteEnter(typeof(PropertyWindow).FullName, MethodBase.GetCurrentMethod().Name);

                SuspendLayout();
                propertyGrid.SuspendLayout();

                if (model == null)
                {
                    ShowWarning();

                    return;
                }

                warningPanel1.Visible = false;
                panel4.Visible = true;

                Model = model;

                if (!ViewModel.IsRenderReady)
                    return;

                try
                {
                    Model = model;

                    if (Model == null)
                    {
                        Log.WriteError("PropertyWindow.Render Model is NULL", typeof(PropertyWindow).FullName, MethodBase.GetCurrentMethod().Name);
                        return;
                    }
                    propertyGrid.SelectedObject = Model;

                    btnAdd.Enabled = true;
                    btnEdit.Enabled = Model is PropertyLookup;
                    btnDelete.Enabled = Model is PropertyLookup;

                    var lookup = Model as PropertyLookup;
                    SetComment(lookup);
                }
                finally
                {
                    Log.WriteExit(typeof(PropertyWindow).FullName, MethodBase.GetCurrentMethod().Name);
                }
            }
            finally
            {
                propertyGrid.ResumeLayout();
                ResumeLayout();                
                StatusUpdate(StatusModel.Completed);
            }
        }

        private void ShowWarning()
        {
            panel4.Visible = false;
            warningPanel1.Visible = true;
            warningPanel1.Dock = DockStyle.Fill;
        }

        private void SetComment(PropertyLookup lookup)
        {
            if (!IsHandleCreated)
            {
                Log.WriteTrace("Property Window IsHandleCreated is false!", typeof(PropertyWindow).FullName, MethodBase.GetCurrentMethod().Name);
                return;
            }

            if (rtbComment.InvokeRequired)
            {
                Invoke(new Action<PropertyLookup>(SetComment), lookup);
                return;
            }

            if (lookup != null)
            {
                try
                {
                    rtbComment.Rtf = lookup.Documentation;

                    // Temp fix to see text in Dark Color Scheme
                    rtbComment.SelectionStart = 0;
                    rtbComment.SelectionLength = rtbComment.TextLength;
                    rtbComment.SelectionColor = Color.White;
                }
                catch (Exception)
                {
                    rtbComment.Text = @"Documentation is corrupted in the xml data file!";
                }
            }
            else
            {
                rtbComment.Clear();
                rtbComment.Text = @"Not Avaiable...";
            }
        }

        public void StatusUpdate(IStatusModel model)
        {
            StatusPanel.StatusUpdate(model);
        }

        public void UpdateView(object selectedModel)
        {
            throw new NotImplementedException();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ViewModel.AddEvent(new EventLR
                {
                    ValueNames = ViewModel.GetValues()
                });
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Model.EventIds.Count == 1)
            {
                ViewModel.EditEvent(Model.EventIds[0]);
                return;
            }

            SetupMenu(sender,Edit_Click);
        }

        private void SetupMenu(object sender, EventHandler eventHandler)
        {
            cmsEvent.Items.Clear();
            foreach (var eventId in Model.EventIds)
            {
                var e = EventService.GetEventById(eventId);
                var text = e != null ? string.Format("{0} - {1}", e.EventId, e.RegularExpression) : "NULL";
                var item = new ToolStripLabel(text, null, false, eventHandler) { Tag = eventId };
                cmsEvent.Items.Add(item);
            }

            var btnSender = (Button) sender;
            var ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            cmsEvent.Width++;
            cmsEvent.Width--;
            cmsEvent.Show(ptLowerLeft);
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            var item = (ToolStripLabel)sender;
            ViewModel.EditEvent((int)item.Tag);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Model.EventIds.Count == 1)
            {
                DeleteEvent(Model.EventIds[0]);
                return;
            }

            SetupMenu(sender,Delete_Click);
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            var item = (ToolStripLabel)sender;
            DeleteEvent((int)item.Tag);
        }

        private void DeleteEvent(int eventId)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Log.WriteTrace("Deleting eventPattern from the property window", typeof(PropertyWindow).FullName, MethodBase.GetCurrentMethod().Name);
                ViewModel.DeleteEvent(eventId);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}