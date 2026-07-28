using System.Text;

namespace DevBrewLabs.Parserly
{
    internal class StringValueParser : Parser<StringResult>
    {
        private readonly char _quoteChar;

        public StringValueParser(bool doubleQuotes)
        {
            _quoteChar = doubleQuotes ? '"' : '\'';
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            var input = inputState.ActualInput;
            var start = inputState.Index;

            if (start >= input.Length || input[start] != _quoteChar)
                return ParserStates.Error(inputState, new ParserError(start, $"Unexpected input. Expected '{_quoteChar}'."));

            // Scan for closing quote, handling doubled-quote escapes
            // We only need a StringBuilder if there are escaped quotes
            StringBuilder buffer = null;
            var segmentStart = start + 1; // skip opening quote

            for (int i = start + 1; i < input.Length; i++)
            {
                if (input[i] == _quoteChar)
                {
                    // Doubled quote escape: "" or ''
                    if (i + 1 < input.Length && input[i + 1] == _quoteChar)
                    {
                        // Lazy-init the builder only when an escape is found
                        if (buffer == null)
                        {
                            buffer = new StringBuilder(i - segmentStart + 16);
                            buffer.Append(input, segmentStart, i - segmentStart);
                        }
                        else
                        {
                            buffer.Append(input, segmentStart, i - segmentStart);
                        }
                        buffer.Append(_quoteChar);
                        segmentStart = i + 2;
                        i++; // skip second quote
                    }
                    else
                    {
                        // Closing quote found
                        string result;
                        if (buffer != null)
                        {
                            buffer.Append(input, segmentStart, i - segmentStart);
                            result = buffer.ToString();
                        }
                        else
                        {
                            // No escapes encountered — single allocation via Substring
                            result = input.Substring(segmentStart, i - segmentStart);
                        }
                        return ParserStates.Result(inputState, new StringResult(result), i + 1);
                    }
                }
            }

            return ParserStates.Error(inputState, new ParserError(start, "Unexpected input. Expected a string value"));
        }
    }
}
