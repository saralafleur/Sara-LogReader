namespace Sara.LogReader.Model.NetworkMapping
{
    public class CriteriaMatch
    {
        public bool Enabled { get; set; }
        [System.ComponentModel.DisplayName(@"IsMatch")]
        public bool? IsMatch { get; set; }
        [System.ComponentModel.DisplayName(@"Type")]
        public string SourceType { get; set; }
        [System.ComponentModel.DisplayName(@"Name")]
        public string SourceName { get; set; }
        [System.ComponentModel.DisplayName(@"Value")]
        public string SourceValue { get; set; }
        [System.ComponentModel.DisplayName(@"Operator")]
        public string Operator { get; set; }
        [System.ComponentModel.DisplayName(@"Type")]
        public string TargetType { get; set; }
        [System.ComponentModel.DisplayName(@"Name")]
        public string TargetName { get; set; }
        [System.ComponentModel.DisplayName(@"Value")]
        public string TargetValue { get; set; }
    }

}
