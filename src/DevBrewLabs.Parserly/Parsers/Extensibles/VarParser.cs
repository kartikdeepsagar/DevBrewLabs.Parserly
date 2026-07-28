using DevBrewLabs.Parserly.Resources;

namespace DevBrewLabs.Parserly
{
    internal class VarParser : Parser<StringResult>
    {
        protected override IParserState ParseInput(IParserState inputState)
        {
            var input = inputState.ActualInput;
            var index = inputState.Index;

            // First character must be a letter  (mirrors [a-zA-Z])
            if (index >= input.Length || !char.IsLetter(input[index]))
            {
                return ParserStates.Error(inputState, new ParserError(index,
                    string.Format(ParserMessages.InputError, index, "word")));
            }

            var pos = index + 1;

            // Remaining characters: letter, digit, or underscore  (mirrors \w*)
            while (pos < input.Length && (char.IsLetterOrDigit(input[pos]) || input[pos] == '_'))
                pos++;

            return ParserStates.Result(inputState, new StringResult(input.Substring(index, pos - index)), pos);
        }
    }
}
