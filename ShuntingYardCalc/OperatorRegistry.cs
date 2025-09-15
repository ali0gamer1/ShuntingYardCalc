using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuntingYardCalc
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

    
    public class OperatorRegistry
    {

        public static bool IsOperator<T>(T arg)
        {
            string c = arg.ToString();

            return c == "+" || c == "/" || c == "-" || c == "*" || c == "^";
        }

    }
}
