using DevBrewLabs.Parserly.Resources;

namespace DevBrewLabs.Parserly
{
    public enum ParseMode
    {
        Both,
        UpperCase,
        LowerCase
    }

    internal class LetterParser : Parser<CharResult>
    {
        private readonly ParseMode _mode;

        public LetterParser(ParseMode mode = ParseMode.Both)
        {
            _mode = mode;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var input = inputState.ActualInput;
            var index = inputState.Index;

            if (index >= input.Length)
                return ParserStates.Error(inputState, new ParserError(index,
                    string.Format(ParserMessages.UnexpectedInputError, index, ParserMessages.Letters, string.Empty)));

            var character = input[index];

            bool isMatch;
            if (_mode == ParseMode.LowerCase)
                isMatch = char.IsLower(character);
            else if (_mode == ParseMode.UpperCase)
                isMatch = char.IsUpper(character);
            else
                isMatch = char.IsLetter(character);

            if (isMatch)
                return ParserStates.Result(inputState, new CharResult(character), index + 1);

            return ParserStates.Error(inputState, new ParserError(index,
                string.Format(ParserMessages.UnexpectedInputError, index, ParserMessages.Letters, character)));
        }
    }
}
