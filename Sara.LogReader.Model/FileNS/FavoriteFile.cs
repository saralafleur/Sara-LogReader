namespace Sara.LogReader.Model.FileNS
{
    /// <summary>
    /// Used to Tag a FileData as a Favorite
    /// </summary>
    public class FavoriteFile
    {
        /// <summary>
        /// Used to group Favorites together.
        /// </summary>
        public string FavoriteGroup { get; set; }
        public string Path { get; set; }
    }
}
