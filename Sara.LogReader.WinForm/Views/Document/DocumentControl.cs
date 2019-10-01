using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FastColoredTextBoxNS;
using Sara.LogReader.Model.LogReaderNS;

namespace Sara.LogReader.WinForm.Views.Document
{
    public class DocumentControl : IDocumentControl
    {
        public DocumentControl(FastColoredTextBox control)
        {
            Control = control;
        }

        FastColoredTextBox Control { get; set; }

        public List<int> FindLines(string regularExpression, RegexOptions none)
        {
            return Control.FindLines(regularExpression, none);
        }

        public List<string> Lines { get { return Control.Lines.ToList(); } }
        public void Render(string model)
        {
            throw new System.NotImplementedException();
        }
    }

}
