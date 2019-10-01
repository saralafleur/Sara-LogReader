using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Sara.LogReader.Common;
using Sara.LogReader.Model;
using Sara.LogReader.Model.Categories;
using Sara.LogReader.Model.DocumentMap;
using Sara.LogReader.Model.EventNS;
using Sara.LogReader.Model.Property;
using Sara.LogReader.Model.ValueNS;
using Sara.LogReader.WinForm.ViewModel;
using Sara.LogReader.WinForm.Views.ToolWindows;
using Sara.WinForm.ColorScheme;
using Sara.WinForm.ColorScheme.Modal;
using Sara.WinForm.Common;
using Sara.WinForm.CRUD;
using Sara.WinForm.Notification;

namespace Sara.LogReader.WinForm.Views.Events
{
    public partial class AddEditEventView : Form, IColorSchemeControl
    {
        public EventModel Model { get; set; }
        public EventViewModel ViewModel { get; set; }

        public AddEditEventView()
        {
            InitializeComponent();
            ColorService.Setup(this);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            StatusPanel.StatusUpdate(StatusModel.Update("Saving..."));

            var i = Model.Item;

            i.Documentation = tbDocumentation.Rtf;
            i.DocumentMap = ckbDocumentMap.Checked;
            i.Name = tbName.Text;
            i.Sort = tbSort.Text;
            i.GapNormal = ckbGapNormal.Checked;
            i.DisplayDurationFromParent = ckbTimeFromParent.Checked;
            i.DisplayDurationFromSibling = ckbDurationFromSibling.Checked;
            i.Example = tbExample.Text;

            i.RegularExpression = tbRegularExpression.Text;

            i.Categories.Clear();
            foreach (var checkedItem in clbCategory.CheckedItems)
            {
                i.Categories.Add((Category)checkedItem);
            }
            i.SourceType = string.IsNullOrEmpty(cbSourceType.Text) ? null : cbSourceType.Text;

            i.SourceTypes.Clear();
            foreach (var checkedItem in clbSourceType.CheckedItems)
            {
                i.SourceTypes.Add(checkedItem.ToString());
            }

            if (i.SourceTypes.Count == 0)
                i.SourceTypes.Add(Keywords.ALL);

            if (cbFoldingEventId.SelectedItem != null)
                i.FoldingEventId = ((EventLR)cbFoldingEventId.SelectedItem).EventId;
            else
                i.FoldingEventId = -1;

            i.Network = cbNetworkCommunication.Text == "" ? NetworkDirection.Na : CRUDUIService.GetEnumValue<NetworkDirection>(cbNetworkCommunication);

            i.Level = (DocumentMapLevel)Enum.Parse(typeof(DocumentMapLevel), cbLevel.Text, true);
            i.IgnoreDocumentation = cbIgnoreDocumentation.Checked;
            i.HighlightColor = cbColor.Text == "" ? Color.Transparent.Name : cbColor.Text;
            i.DocumentMapHighlightColor = cbDocumentMapHighlightColor.Text == "" ? Color.Transparent.Name : cbDocumentMapHighlightColor.Text;

            CRUDUIService.SaveList(i.EventValues, lbValues);
            CRUDUIService.SaveList(i.EventLookupValues, lbLookupValues);

            i.IgnoreName = ckbIgnoreName.Checked;

            try
            {
                ViewModel.Save(Model);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            StatusPanel.StatusUpdate(StatusModel.Completed);
        }

        public void Render(EventModel model)
        {
            Model = model;
            var i = model.Item;
            tbSort.Text = i.Sort;
            tbDocumentation.Rtf = i.Documentation;
            ckbDocumentMap.Checked = i.DocumentMap;
            tbName.Text = i.Name;
            ckbGapNormal.Checked = i.GapNormal;
            ckbTimeFromParent.Checked = i.DisplayDurationFromParent;
            ckbDurationFromSibling.Checked = i.DisplayDurationFromSibling;
            tbRegularExpression.Text = i.RegularExpression;
            tbExample.Text = i.Example;

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
            UICommon.AutoColumnWidth(clbCategory);

            clbSourceType.Items.Clear();
            clbSourceType.Items.Add(Keywords.ALL, model.Item.SourceTypes.Contains(Keywords.ALL));
            foreach (var item in model.SourceTypes)
            {
                clbSourceType.Items.Add(item);
                foreach (var sourceType in model.Item.SourceTypes)
                {
                    if (item == sourceType)
                        clbSourceType.SetItemChecked(clbSourceType.Items.Count - 1, true);
                }
            }
            UICommon.AutoColumnWidth(clbSourceType);

            cbFoldingEventId.Items.Clear();
            foreach (EventLR item in model.Events)
            {
                cbFoldingEventId.Items.Add(item);
                if (item.EventId == model.Item.FoldingEventId)
                    cbFoldingEventId.SelectedItem = item;
            }

            switch (model.Mode)
            {
                case InputMode.Add:
                    Text = @"Add EventPattern";
                    break;
                case InputMode.Edit:
                    Text = @"Edit EventPattern";
                    break;
            }

            cbSourceType.Items.Clear();
            cbSourceType.Items.Add("");
            foreach (var item in XmlDal.DataModel.Options.FileTypes)
            {
                cbSourceType.Items.Add(item);
            }
            cbSourceType.Text = i.SourceType ?? "";

            cbNetworkCommunication.Items.Clear();
            cbNetworkCommunication.Items.Add("");
            foreach (var item in Enum.GetNames(typeof(NetworkDirection)).ToList())
            {
                if (item == "Na")
                    continue;
                cbNetworkCommunication.Items.Add(item);
            }
            cbNetworkCommunication.Text = i.Network == NetworkDirection.Na ? "" : i.Network.ToString();

            CRUDUIService.RenderEnumList<DocumentMapLevel>(cbLevel, i.Level);

            cbIgnoreDocumentation.Checked = i.IgnoreDocumentation;

            Type t = typeof(Color);
            var p = t.GetProperties();
            foreach (var item in p)
            {
                if (item.PropertyType.FullName.Equals("System.Drawing.Color", StringComparison.CurrentCultureIgnoreCase))
                {
                    cbColor.Items.Add(item.Name);
                    cbDocumentMapHighlightColor.Items.Add(item.Name);
                }
            }

            cbColor.Text = i.HighlightColor == Color.Transparent.Name ? "" : i.HighlightColor;
            cbDocumentMapHighlightColor.Text = i.DocumentMapHighlightColor == Color.Transparent.Name ? "" : i.DocumentMapHighlightColor;

            CRUDUIService.RenderList(i.EventValues, lbValues);
            CRUDUIService.RenderList(i.EventLookupValues, lbLookupValues);

            ckbIgnoreName.Checked = i.IgnoreName;
        }

        private void btnTestEvent_Click(object sender, EventArgs e)
        {
            if (MainViewModel.Current == null)
            {
                MessageBox.Show("A document must be open before you can test an Expression.", "Warning");
                return;
            }

            RegularExpressionTest.Test(tbRegularExpression.Text, (Value m) => { tbRegularExpression.Text = m.Expression; });
        }
        private Brush GetCurrentBrush(string colorName)
        {
            return new SolidBrush(Color.FromName(colorName));
        }

        private void cbColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index != -1)
            {
                e.DrawBackground();
                e.Graphics.FillRectangle(GetCurrentBrush(cbColor.Items[e.Index].ToString()), e.Bounds);
                Font f = cbColor.Font;
                e.Graphics.DrawString(cbColor.Items[e.Index].ToString(), f, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
                e.DrawFocusRectangle();
            }
        }

        private void cbIgnoreDocumentation_CheckedChanged(object sender, EventArgs e)
        {
            tbDocumentation.Visible = !cbIgnoreDocumentation.Checked;
        }

        #region CRUD
        private void btnEditValue_Click(object sender, EventArgs e)
        {
            CRUDUIService.Edit<AddEditEventValueView, EventValueModel, EventValue>(lbValues, @"You must select a Value first", (model => CRUDUIService.Save(lbValues, model)));
        }

        private void btnDeleteValue_Click(object sender, EventArgs e)
        {
            CRUDUIService.Delete(lbValues, @"You must select a Value first");
        }

        private void btnAddValue_Click(object sender, EventArgs e)
        {
            CRUDUIService.Add<AddEditEventValueView, EventValueModel, EventValue>((model => CRUDUIService.Save(lbValues, model)));
        }

        private void btnAddLookupValue_Click(object sender, EventArgs e)
        {
            CRUDUIService.Add<AddEditEventLookupValueView, EventLookupValueModel, EventLookupValue>((model => CRUDUIService.Save(lbLookupValues, model)));
        }

        private void btnEditLookupValue_Click(object sender, EventArgs e)
        {
            CRUDUIService.Edit<AddEditEventLookupValueView, EventLookupValueModel, EventLookupValue>(lbLookupValues, @"You must select a Lookup Value first", (model => CRUDUIService.Save(lbLookupValues, model)));
        }

        private void btnDeleteLookupValue_Click(object sender, EventArgs e)
        {
            CRUDUIService.Delete(lbLookupValues, @"You must select a Lookup Value first");
        }
        #endregion CRUD

        private void btnCopyExample_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbExample.Text);
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (MainViewModel.Current == null)
            {
                MessageBox.Show("Capture will copy the current line in an open Document.  You must have a Document open to capture.", "Warning");
                return;
            }
            tbExample.Text = MainViewModel.Current.CurrentLine;
        }

        private void cbDocumentMapHighlightColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index != -1)
            {
                e.DrawBackground();
                e.Graphics.FillRectangle(GetCurrentBrush(cbColor.Items[e.Index].ToString()), e.Bounds);
                Font f = cbColor.Font;
                e.Graphics.DrawString(cbColor.Items[e.Index].ToString(), f, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
                e.DrawFocusRectangle();
            }

        }

        public void ApplyColorScheme()
        {
        }
    }
}
