using System.Collections.Generic;
using System.Text;
using DevBrewLabs.Parserly.Resources;

namespace DevBrewLabs.Parserly
{
    public static class HelperExtensions
    {
        /// <summary>
        /// Converts array result to double result. Returns 0 if conversion fails.
        /// </summary>
        /// <param name="arrayResult"></param>
        /// <returns></returns>
        public static DoubleResult ToDoubleResult(this ArrayResult arrayResult)
        {
            string input = arrayResult.ToStringResult().Value;

            if (!double.TryParse(input, out double result))
                throw new ParsingException(string.Format(ParserMessages.TypeConvertError, input, typeof(DoubleResult)));

            return new DoubleResult(result);
        }

        /// <summary>
        /// Converts array result to string result.
        /// </summary>
        /// <param name="arrayResult"></param>
        /// <returns></returns>
        public static StringResult ToStringResult(this ArrayResult arrayResult)
        {
            // Use a Stack to flatten nested ArrayResults without LINQ or Queue overhead
            var stack = new Stack<IParserResult>(arrayResult.Value.Length);
            // Push in reverse order so we process in forward order
            for (int i = arrayResult.Value.Length - 1; i >= 0; i--)
                stack.Push(arrayResult.Value[i]);

            // Estimate capacity: each result contributes at least 1 char
            var stringBuilder = new StringBuilder(arrayResult.Value.Length * 2);

            while (stack.Count > 0)
            {
                var item = stack.Pop();

                if (item is ArrayResult aResult)
                {
                    for (int i = aResult.Value.Length - 1; i >= 0; i--)
                        stack.Push(aResult.Value[i]);
                }
                else
                {
                    stringBuilder.Append(item.Value);
                }
            }

            return new StringResult(stringBuilder.ToString());
        }

        /// <summary>
        /// Converts string result to double result.
        /// </summary>
        /// <param name="stringResult"></param>
        /// <returns></returns>
        public static DoubleResult ToDoubleResult(this StringResult stringResult)
        {
            if (!string.IsNullOrEmpty(stringResult.Value) &&
                double.TryParse(stringResult.Value, out double result))
            {
                return new DoubleResult(result);
            }

            return DoubleResult.Invalid;
        }

        /// <summary>
        /// Converts string result to boolean result.
        /// </summary>
        /// <param name="stringResult"></param>
        /// <returns></returns>
        public static BooleanResult ToBooleanResult(this StringResult stringResult)
        {
            if (!string.IsNullOrEmpty(stringResult.Value) &&
                bool.TryParse(stringResult.Value, out bool result))
            {
                return new BooleanResult(result);
            }

            return BooleanResult.Invalid;
        }
    }
}
