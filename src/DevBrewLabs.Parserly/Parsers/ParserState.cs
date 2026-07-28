namespace DevBrewLabs.Parserly
{
    internal class ParserState : IParserState
    {
        public int Index { get; set; }
        public string ActualInput { get; set; }
        public IParserResult Result { get; set; }
        public bool IsError => Error != null;
        public IParserError Error { get; set; }

        public ParserState()
        {
        }

        public ParserState(string actualInput, int index, IParserResult result, IParserError error)
        {
            ActualInput = actualInput;
            Index = index;
            Result = result;
            Error = error;
        }

        public IParserState Clone()
        {
            return new ParserState(ActualInput, Index, Result, Error);
        }
    }
}
