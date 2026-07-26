namespace DevBrewLabs.Parserly
{
    public class BooleanResult : ParserResult<bool>
    {
        public static BooleanResult Invalid = new BooleanResult();

        public BooleanResult(bool value) : base(value, ParserResultType.Boolean)
        {

        }

        public BooleanResult() : base()
        {

        }
    }
}
