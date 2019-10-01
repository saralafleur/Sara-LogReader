using System.Collections.Generic;
using System.Text.RegularExpressions;
using Sara.LogReader.Model.FileNS;

namespace Sara.LogReader.Model.LogReaderNS
{
    // TODO: Remove this from use - Sara
    public interface IDocumentControl
    {
        List<int> FindLines(string regularExpression, RegexOptions none);
        List<string> Lines { get; }
    }

    public class DocumentModel
    {
        public IDocumentControl Control { get; set; }
        public FileData File;
    }
}

