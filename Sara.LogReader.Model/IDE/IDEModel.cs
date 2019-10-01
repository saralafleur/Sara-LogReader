namespace Sara.LogReader.Model.IDE
{
    public class IDEModel
    {
        public string Script { get; set; }
        /// <summary>
        /// When a script is processed by the Lexer, if ShowTokens is True then the Tokens will be displayed
        /// </summary>
        public bool ShowTokens { get; set; }
    }
}
