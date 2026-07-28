using System;
using System.Collections.Generic;
using System.Text;

namespace DevBrewLabs.Parserly.Resources
{
    public static class ParserMessages
    {
        public const string AtleastCount = "atleast {0} count{1}";
        public const string AtleastOneParserMatch = "atleast 1 parser match";
        public const string AtmostCount = "atmost {0} count{1}";
        public const string Digits = "0-9";
        public const string EndofInput = "end of input";
        public const string GotCount = "{0} count{1}";
        public const string InputError = "Position ({0}): Expected '{1}'";
        public const string Letters = "a-z/A-Z";
        public const string NoParserMatch = "0 parser match";
        public const string TypeConvertError = "Unable to parse '{0}' to '{1}'";
        public const string UnexpectedInputError = "Position ({0}): Unexpected input. Expected '{1}' but got '{2}'";
    }
}
