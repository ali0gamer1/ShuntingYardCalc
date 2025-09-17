
using ShuntingYardCalc.Specs;

namespace ShuntingYardCalc
{
    public class Registry: OperatorRegistry, FunctionRegistry
    {
    
        private Dictionary<string, OperatorSpec> operators = [];
        private Dictionary<string, FunctionSpec> functions = [];


        public bool IsOperator(string arg)
        {
            return operators.ContainsKey(arg);
        }

        public bool IsOperator(char arg)
        {
            return operators.ContainsKey(arg.ToString());
        }


        public Dictionary<string, OperatorSpec> Operators {

            get => operators;

        }
        
        public bool TryGetOperator(string symbol, out OperatorSpec opspec)
        {
            return operators.TryGetValue(symbol, out opspec);
        }


        public void RegisterOperator(string symbol, OperatorSpec opspec)
        {
            operators.Add(symbol, opspec);
        }


        public bool IsFunc(string symbol)
        {
            return functions.ContainsKey(symbol);
        }

        
        public Dictionary<string, FunctionSpec> Functions 
        { get => functions; }


        public void RegisterFunction(string symbol, FunctionSpec funcspec)
        {
            ArgumentException.ThrowIfNullOrEmpty(symbol);
            ArgumentNullException.ThrowIfNull(funcspec);


            functions.Add(symbol, funcspec);

        }


        public bool TryGetFunc(string symbol, out FunctionSpec funcspec)
        {
            return functions.TryGetValue(symbol, out funcspec);
        }




    }
}
