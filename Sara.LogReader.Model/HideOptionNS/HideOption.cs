using System;
using System.Collections.Generic;
using Sara.Common.Extension;

namespace Sara.LogReader.Model.HideOptionNS
{
    public class HideOption : ICloneable
    {
        public HideOption()
        {
            SourceTypes = new List<string>();
        }
        public int HideOptionId { get; set; }
        public string Name { get; set; }
        public List<string> SourceTypes { get; set; }
        public string RegularExpression { get; set; }
        public string ReplaceWith { get; set; }

        public object Clone()
        {
            var c = new HideOption()
            {
                Name = this.Name,
                HideOptionId = this.HideOptionId,
                SourceTypes = (List<string>)this.SourceTypes.Clone()
            };
            return c;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
