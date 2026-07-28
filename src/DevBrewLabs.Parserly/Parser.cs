using DevBrewLabs.Parserly.Parsers;
using DevBrewLabs.Parserly.Resources;
using System;
using System.Globalization;

namespace DevBrewLabs.Parserly
{
    public static class Parser
    {
        /// <summary>
        /// Gets the any letter or digit parser.
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser AnyLetterOrDigit(ParseMode mode = ParseMode.Both, bool allowTrace = true)
        {
            return AnyLetter(mode, allowTrace)
                    .Or(Digit(allowTrace))
                    .MapError(x => new ParserError(x.Index, 
                        string.Format(ParserMessages.InputError, x.Index, "a letter or digit")), allowTrace);
        }

        /// <summary>
        /// Gets the whitespace parser.
        /// </summary>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser<CharResult> WhiteSpace(bool allowTrace = false)
        {
            return Char(' ', allowTrace);
        }

        /// <summary>
        /// Gets the digit parser.
        /// </summary>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser<DoubleResult> Digit(bool allowTrace = false)
        {
            return new DigitParser() { AllowTrace = allowTrace };
        }

        /// <summary>
        /// Gets the boolean parser.
        /// </summary>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser<BooleanResult> Boolean(bool allowTrace = false)
        {
            return new BooleanParser() { AllowTrace = allowTrace };
        }

        /// <summary>
        /// Gets the letter parser.
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser<CharResult> AnyLetter(ParseMode mode = ParseMode.Both, bool allowTrace = false) 
            => new LetterParser(mode) { AllowTrace = allowTrace };

        /// <summary>
        /// Gets the character parser.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser<CharResult> Char(char value, bool allowTrace = false) => new CharParser(value) { AllowTrace = allowTrace };

        /// <summary>
        /// Gets the string parser.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="matchCase"></param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser<StringResult> String(string value, bool matchCase = false, bool allowTrace = false) => new StringParser(value, matchCase);

        /// <summary>
        /// Gets the until parser.
        /// </summary>
        /// <param name="selector">Parses the input until selector is found</param>
        /// <param name="matchCase"></param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser<StringResult> UntilFound(string selector, bool matchCase = false, bool allowTrace = false) => 
            new UntilFoundParser(selector, matchCase) {  AllowTrace = allowTrace };

        /// <summary>
        /// Gets the string value parser.
        /// </summary>
        /// <param name="doubleQuotes">Specifies to parse double quoted string otherwise single quoted string</param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser<StringResult> StringValue(bool doubleQuotes = true, bool allowTrace = false) => 
            new StringValueParser(doubleQuotes) { AllowTrace = allowTrace };

        /// <summary>
        /// Gets the number parser.
        /// </summary>
        /// <param name="canParseDecimal"></param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser<DoubleResult> Number(bool canParseDecimal = true, bool allowTrace = false) =>
            new NumberParser(canParseDecimal, CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0]) { AllowTrace = allowTrace };

        /// <summary>
        /// Gets the lazy parser.
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser Lazy(Func<IParser> parser, bool allowTrace = false) => 
            new LazyParser(parser) { AllowTrace = allowTrace };

        /// <summary>
        /// Gets the parser which acts as a proxy parser to return a result.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser FromResult(IParserResult result, bool allowTrace = false)
        {
            return new ResultParser(result) { AllowTrace = allowTrace };
        }

        /// <summary>
        /// Gets the parser which acts as a proxy parser to return an error.
        /// </summary>
        /// <param name="error"></param>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser FromError(IParserError error, bool allowTrace = false)
        {
            return new ErrorParser(error) { AllowTrace = allowTrace };
        }

        /// <summary>
        /// Gets a lookahead parser that checks whether the upcoming input starts with
        /// the given value without consuming any characters.
        /// Returns <see cref="BooleanResult"/> true on match, error on mismatch.
        /// </summary>
        /// <param name="value">The string to peek for.</param>
        /// <param name="matchCase">Whether the comparison is case-sensitive.</param>
        /// <param name="allowTrace">Allow trace</param>
        /// <returns>A peek parser.</returns>
        public static IParser<BooleanResult> Peek(string value, bool matchCase = false, bool allowTrace = false) => 
            new PeekParser(value, matchCase) { AllowTrace = allowTrace };

        /// <summary>
        /// Gets a parser that matches a valid identifier — a letter followed by
        /// zero or more letters, digits, or underscores (e.g. <c>myVar</c>, <c>_count</c> is invalid, <c>abc123</c> is valid).
        /// </summary>
        /// <returns>A parser that produces a <see cref="StringResult"/> containing the matched identifier.</returns>
        public static IParser<StringResult> Var(bool allowTrace) => new VarParser() { AllowTrace = allowTrace };

        /// <summary>
        /// Gets the end of input parser.
        /// </summary>
        /// <param name="allowTrace"></param>
        /// <returns></returns>
        public static IParser EndOfInput(bool allowTrace) => new EndOfInputParser() { AllowTrace = allowTrace };
    }
}