using DevBrewLabs.Parserly.Resources;

namespace DevBrewLabs.Parserly
{
    internal class EndOfInputParser : Parser<IParserResult>
    {
        protected override IParserState ParseInput(IParserState inputState)
        {
            if (inputState.Index >= inputState.ActualInput.Length)
            {
                return ParserStates.Result(inputState, inputState.Result, inputState.Index);
            }

            return ParserStates.Error(inputState, new ParserError(inputState.Index,
                string.Format(ParserMessages.UnexpectedInputError, inputState.Index, ParserMessages.EndofInput,
                    inputState.ActualInput[inputState.Index])));
        }
    }
}
