using DevBrewLabs.Parserly.Resources;

namespace DevBrewLabs.Parserly
{
    internal class DigitParser : Parser<DoubleResult>
    {
        protected override IParserState ParseInput(IParserState inputState)
        {
            var input = inputState.ActualInput;
            var index = inputState.Index;

            if (index >= input.Length)
                return ParserStates.Error(inputState, new ParserError(index,
                    string.Format(ParserMessages.UnexpectedInputError, index, ParserMessages.Digits, string.Empty)));

            var character = input[index];
            if (char.IsDigit(character))
            {
                return ParserStates.Result(inputState, new DoubleResult(character - '0'), index + 1);
            }

            return ParserStates.Error(inputState, new ParserError(index,
                string.Format(ParserMessages.UnexpectedInputError, index, ParserMessages.Digits, character)));
        }
    }
}
