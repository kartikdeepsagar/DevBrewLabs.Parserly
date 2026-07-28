using System;

namespace DevBrewLabs.Parserly
{
    /// <summary>
    /// A lookahead parser that checks whether the upcoming input matches a given value
    /// without consuming any characters. Returns <see cref="BooleanResult"/> true on match.
    /// </summary>
    internal class PeekParser : Parser<BooleanResult>
    {
        private readonly string _value;
        private readonly StringComparison _comparison;

        public PeekParser(string value, bool matchCase = false)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _value = value;
            // Pre-compute the comparison mode once instead of branching on every parse call
            _comparison = matchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var input = inputState.ActualInput;
            var index = inputState.Index;
            var remaining = input.AsSpan(index);

            if (remaining.Length >= _value.Length &&
                remaining.StartsWith(_value.AsSpan(), _comparison))
            {
                // Peek — do NOT advance index
                return ParserStates.Result(inputState, new BooleanResult(true), index);
            }

            return ParserStates.Error(inputState, new ParserError(index, "peek value not found"));
        }

        public override string ToString()
        {
            return $"{nameof(PeekParser)}(\"{_value}\")";
        }
    }
}
