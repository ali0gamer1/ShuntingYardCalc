using ShuntingYardCalc.Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuntingYardCalc
{
 

    public class OperatorRegistry
    {
        private Dictionary<string, OperatorSpec> operators = new Dictionary<string, OperatorSpec>();
        


        public bool IsOperator<T>(T arg)
        {
            string c = arg.ToString();

            return operators.Keys.Contains(c);

        }

        public OperatorSpec GetOperator(string op)
        {
            return operators[op];
        }


        public bool TryGetOperator(string op, out OperatorSpec opspec)
        {
            return operators.TryGetValue(op, out opspec);
        }


        public  void Register(string symbol, OperatorSpec opspec)
        {
            operators.Add(symbol, opspec);

        }

    }
}
