using ShuntingYardCalc.Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuntingYardCalc
{
 

    public interface OperatorRegistry
    {
        

        public Dictionary<string, OperatorSpec> Operators { get;}

        public bool IsOperator(string arg);
        public bool IsOperator(char arg);

        public bool TryGetOperator(string symbol, out OperatorSpec opspec);

        //could be an interface func
        public void RegisterOperator(string symbol, OperatorSpec opspec);
    }
}
