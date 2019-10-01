using System.Collections.Generic;

namespace Sara.LogReader.Model.PatternNS
{
    public class PatternOptions
    {
        public bool HideFilePath;

        public PatternOptions()
        {
            KnownIdleParameters = new List<string>();
            UnknownIdleParameters = new List<string>();
        }
        public bool TotalTimeOption { get; set; }
        public bool IdleTimeOption { get; set; }
        public bool HidePattern { get; set; }
        public bool PatternUnexpected { get; set; }
        public bool KnownIdle { get; set; }
        public List<string> KnownIdleParameters { get; set; }
        public bool UnknownIdle { get; set; }
        public List<string> UnknownIdleParameters { get; set; }
        public PatternOptions Clone()
        {
            return new PatternOptions()
            {
                IdleTimeOption = IdleTimeOption,
                TotalTimeOption = TotalTimeOption,
                HidePattern = HidePattern,
                HideFilePath = HideFilePath,
                PatternUnexpected = PatternUnexpected,
                KnownIdle = KnownIdle,
                KnownIdleParameters = new List<string>(KnownIdleParameters),
                UnknownIdle = UnknownIdle,
                UnknownIdleParameters = new List<string>(UnknownIdleParameters),
            };
        }
    }
}
