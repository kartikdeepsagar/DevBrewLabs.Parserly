namespace DevBrewLabs.Parserly
{
    /// <summary>
    /// Interface that represents a parser state.
    /// </summary>
    public interface IParserState : ICloneable<IParserState>
    {
        /// <summary>
        /// Gets or sets the current state index.
        /// </summary>
        int Index { get; set; }
        /// <summary>
        /// Gets or sets the actual input.
        /// </summary>
        string ActualInput { get; set; }
        /// <summary>
        /// Gets if the state has error.
        /// </summary>
        bool IsError { get; }
        /// <summary>
        /// Gets or sets the result of this state.
        /// </summary>
        IParserResult Result { get; set; }
        /// <summary>
        /// Gets or sets the error of this state.
        /// </summary>
        IParserError Error { get; set; }
    }
}
