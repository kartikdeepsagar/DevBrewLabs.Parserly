using System;
using DevBrewLabs.Parserly.Resources;

namespace DevBrewLabs.Parserly
{
    internal class UntilFoundParser : Parser<StringResult>
    {
        private readonly string _selector;
        private readonly bool _matchCase;

        public UntilFoundParser(string selector, bool matchCase)
        {
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            _selector = selector;
            _matchCase = matchCase;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var input = inputState.ActualInput;
            var start = inputState.Index;
            var comparison = _matchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            var index = input.IndexOf(_selector, start, comparison);

            if (index >= 0)
            {
                // Return everything from start up to (not including) the selector
                var matched = input.Substring(start, index - start);
                return ParserStates.Result(inputState, new StringResult(matched), index + _selector.Length);
            }

            return ParserStates.Error(inputState, new ParserError(start,
               string.Format(ParserMessages.UnexpectedInputError, start, _selector,
                   input.Length > start ? input.Substring(start) : string.Empty)));
        }
    }
}
