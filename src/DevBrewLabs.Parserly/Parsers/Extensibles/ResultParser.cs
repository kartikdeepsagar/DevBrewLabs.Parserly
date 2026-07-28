namespace DevBrewLabs.Parserly
{
    internal class ResultParser : Parser<IParserResult>
    {
        private IParserResult _result;

        public ResultParser(IParserResult result)
        {
            _result = result;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            return ParserStates.Result(inputState, _result, inputState.Index);
        }
    }
}
