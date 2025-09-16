namespace ShuntingYardCalc
{


    public class OperatorInfo
    {

        public int Precedence { get; set; }
        public string Associativity { get; set; }  // "Left" or "Right"


        public static bool IsOperator<T>(T arg)
        {
            string c = arg.ToString();

            return c == "+" || c == "/" || c == "-" || c == "*" || c == "^";
        }


        public OperatorInfo(int precedence, string associativity)
        {
            Precedence = precedence;
            Associativity = associativity;
        }
    }
}
