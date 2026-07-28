using DevBrewLabs.Parserly.Resources;

namespace DevBrewLabs.Parserly
{
    internal class CharParser : Parser<CharResult>
    {
        public char Value { get; }

        public CharParser(char value)
        {
            Value = value;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var input = inputState.ActualInput;
            var index = inputState.Index;

            if (index >= input.Length)
                return ParserStates.Error(inputState, new ParserError(index,
                    string.Format(ParserMessages.UnexpectedInputError, index, Value.ToString(), string.Empty)));

            var character = input[index];

            if (character == Value)
            {
                return ParserStates.Result(inputState, new CharResult(Value), index + 1);
            }

            return ParserStates.Error(inputState, new ParserError(index,
                string.Format(ParserMessages.UnexpectedInputError, index, Value.ToString(), character)));
        }

        public override string ToString()
        {
            return $"{nameof(CharParser)}('{Value}')";
        }
    }
}
