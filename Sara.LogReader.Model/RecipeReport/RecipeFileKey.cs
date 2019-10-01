namespace Sara.LogReader.Model.RecipeReport
{
    public class RecipeFileKey
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string Path { get; set; }
        public override string ToString()
        {
            return $"{RecipeId}-{Path}";
        }
    }
}
