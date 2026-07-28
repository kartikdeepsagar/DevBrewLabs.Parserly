using System.Globalization;
using DevBrewLabs.Parserly.Resources;

namespace DevBrewLabs.Parserly.Parsers
{
    internal class NumberParser : Parser<DoubleResult>
    {
        private readonly bool _canParseDecimal;
        private readonly char _decimalSeparator;

        public NumberParser(bool canParseDecimal, char decimalSeparator)
        {
            _canParseDecimal = canParseDecimal;
            _decimalSeparator = decimalSeparator;
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var input = inputState.ActualInput;
            var start = inputState.Index;
            var pos = start;
            var length = input.Length;

            // Optional leading sign
            if (pos < length && (input[pos] == '+' || input[pos] == '-'))
                pos++;

            // Must have at least one digit
            var digitStart = pos;
            while (pos < length && char.IsDigit(input[pos]))
                pos++;

            if (pos == digitStart)
            {
                return ParserStates.Error(inputState, new ParserError(start,
                    string.Format(ParserMessages.UnexpectedInputError, start, "number",
                        pos < length ? input[pos].ToString() : string.Empty)));
            }

            // Optional decimal part
            if (_canParseDecimal && pos < length && input[pos] == _decimalSeparator)
            {
                var decimalPos = pos + 1;
                while (decimalPos < length && char.IsDigit(input[decimalPos]))
                    decimalPos++;

                // Only consume the decimal point if digits follow it
                if (decimalPos > pos + 1)
                    pos = decimalPos;
            }

            var numberStr = input.Substring(start, pos - start);
            if (double.TryParse(numberStr, NumberStyles.Float, CultureInfo.CurrentCulture, out double value))
            {
                return ParserStates.Result(inputState, new DoubleResult(value), pos);
            }

            return ParserStates.Error(inputState, new ParserError(start,
                string.Format(ParserMessages.UnexpectedInputError, start, "number", numberStr)));
        }
    }
}
