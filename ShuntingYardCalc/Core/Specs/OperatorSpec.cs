
namespace ShuntingYardCalc.Specs
{
    public static class OperatorSpec
    {

        public static Dictionary<string, OperatorInfo> operators = new Dictionary<string, OperatorInfo>()
        {
            { "+", new OperatorInfo(1, "Left") },
            { "-", new OperatorInfo(1, "Left") },
            { "*", new OperatorInfo(2, "Left") },
            { "/", new OperatorInfo(2, "Left") },
            { "^", new OperatorInfo(3, "Right") },
            { "u+", new OperatorInfo(4, "Right") },
            { "u-", new OperatorInfo(4, "Right") },
            {"func", new OperatorInfo(9, "none") }
        };

    }
}
