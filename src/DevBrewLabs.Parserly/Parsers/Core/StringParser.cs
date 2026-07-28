using System;
using DevBrewLabs.Parserly.Resources;

namespace DevBrewLabs.Parserly
{
    internal class StringParser : Parser<StringResult>
    {
        public string Value { get; }
        public bool MatchCase { get; }

        public StringParser(string value, bool matchCase = false)
        {
            Value = value;
            MatchCase = matchCase;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var input = inputState.ActualInput;
            var index = inputState.Index;
            var remaining = input.AsSpan(index);
            var comparison = MatchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            if (remaining.Length >= Value.Length &&
                remaining.StartsWith(Value.AsSpan(), comparison))
            {
                return ParserStates.Result(inputState, new StringResult(Value), index + Value.Length);
            }

            return ParserStates.Error(inputState, new ParserError(index,
                string.Format(ParserMessages.UnexpectedInputError, index, Value,
                    remaining.Length > 0 ? remaining.Slice(0, Math.Min(Value.Length, remaining.Length)).ToString() : string.Empty)));
        }

        public override string ToString()
        {
            return $"{nameof(StringParser)}(\"{Value}\")";
        }
    }
}
