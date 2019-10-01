
using System.Collections.Generic;

namespace Sara.LogReader.Model.PatternNS
{
    public class Pattern
    {
        public Pattern()
        {
            Script = string.Empty;
            SelectedFiles = new List<string>();
        }
        public List<string> SelectedFiles { get; set; }
        public int PatternId { get; set; }
        public string Name { get; set; }
        public string Script { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
