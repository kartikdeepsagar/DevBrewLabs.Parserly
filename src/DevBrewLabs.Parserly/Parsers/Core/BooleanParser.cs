using System;
using DevBrewLabs.Parserly.Resources;

namespace DevBrewLabs.Parserly
{
    internal class BooleanParser : Parser<BooleanResult>
    {
        private const string TRUE = "true";
        private const string FALSE = "false";

        protected override IParserState ParseInput(IParserState inputState)
        {
            var input = inputState.ActualInput;
            var index = inputState.Index;
            var remaining = input.AsSpan(index);

            if (remaining.Length >= TRUE.Length &&
                remaining.StartsWith(TRUE.AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return ParserStates.Result(inputState, new BooleanResult(true), index + TRUE.Length);
            }

            if (remaining.Length >= FALSE.Length &&
                remaining.StartsWith(FALSE.AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return ParserStates.Result(inputState, new BooleanResult(false), index + FALSE.Length);
            }

            return ParserStates.Error(inputState, new ParserError(index,
                string.Format(ParserMessages.UnexpectedInputError, index, $"{TRUE}/{FALSE}", remaining.Length > 0 ? remaining[0].ToString() : string.Empty)));
        }
    }
}
