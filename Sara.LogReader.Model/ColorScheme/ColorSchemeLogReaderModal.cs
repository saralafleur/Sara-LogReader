using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;
using Sara.WinForm.ColorScheme.Modal;

namespace Sara.LogReader.Model.ColorScheme
{
    public class ColorSchemeLogReaderModal : ColorSchemeBaseModal, IColorSchemeModal
    {
        public override void Apply(object control)
        {
            base.Apply(control);
        }

        public new void SetDarkDefault()
        {
            base.SetDarkDefault();

            DocumentMapOverlayBackColor = BackColor;
            DocumentMapOverlayForeColor = Color.LightSalmon;
            PatternOverlayBackColor = BackColor;
            PatternOverlayForeColor = Color.LimeGreen;

            ReadableLineForeColor = ForeColor;
            ReadableLineBackground = BackColor;
        }

        public new void SetLightDefault()
        {
            base.SetLightDefault();

            DocumentMapOverlayBackColor = BackColor;
            DocumentMapOverlayForeColor = Color.LightSalmon;
            PatternOverlayBackColor = BackColor;
            PatternOverlayForeColor = Color.LimeGreen;

            ReadableLineForeColor = ForeColor;
            ReadableLineBackground = Color.Beige;
        }

        #region Document Map
        [XmlIgnore]
        [Description("Document Map Overlay BackColor used in highlight the Document."), Category("Overlay")]
        public Color DocumentMapOverlayBackColor { get { return StringToColor(DocumentMapOverlayBackColorXML); } set { DocumentMapOverlayBackColorXML = ColorToString(value); } }
        [Browsable(false)]
        public string DocumentMapOverlayBackColorXML { get; set; }


        [XmlIgnore]
        [Description("Document Map Overlay ForeColor used in highlight the Document."), Category("Overlay")]
        public Color DocumentMapOverlayForeColor { get { return StringToColor(DocumentMapOverlayForeColorXML); } set { DocumentMapOverlayForeColorXML = ColorToString(value); } }
        [Browsable(false)]
        public string DocumentMapOverlayForeColorXML { get; set; }
        #endregion Document Map

        #region Pattern
        [XmlIgnore]
        [Description("Pattern Overlay color used to highlight the Document."), Category("Overlay")]
        public Color PatternOverlayBackColor { get { return StringToColor(PatternOverlayBackColorXML); } set { PatternOverlayBackColorXML = ColorToString(value); } }
        [Browsable(false)]
        public string PatternOverlayBackColorXML { get; set; }

        [XmlIgnore]
        [Description("Pattern Overlay color used to highlight the Document."), Category("Overlay")]
        public Color PatternOverlayForeColor { get { return StringToColor(PatternOverlayForeColorXML); } set { PatternOverlayForeColorXML = ColorToString(value); } }
        [Browsable(false)]
        public string PatternOverlayForeColorXML { get; set; }
        #endregion Pattern

        #region Current Line
        [XmlIgnore]
        [Description("Background color for the current line Control at the top of the Application."), Category("General")]
        public Color ReadableLineBackground { get { return StringToColor(ReadableLineBackgroundXML); } set { ReadableLineBackgroundXML = ColorToString(value); } }
        [Browsable(false)]
        public string ReadableLineBackgroundXML { get; set; }

        [XmlIgnore]
        [Description("Forecolor for the current line Control at the top of the Application."), Category("General")]
        public Color ReadableLineForeColor { get { return StringToColor(ReadableLineForeColorXML); } set { ReadableLineForeColorXML = ColorToString(value); } }
        [Browsable(false)]
        public string ReadableLineForeColorXML { get; set; }
        #endregion Current Line

    }
}
