namespace Sara.LogReader.Model.PatternNS
{
    public class RecipeSelection
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
