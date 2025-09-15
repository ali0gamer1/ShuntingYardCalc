namespace ShuntingYardCalc
{


    internal partial class Program
    {
        public class OperatorInfo
        {
            public int Precedence { get; set; }
            public string Associativity { get; set; }  // "Left" or "Right"

            public OperatorInfo(int precedence, string associativity)
            {
                Precedence = precedence;
                Associativity = associativity;
            }
        }
    }
}
