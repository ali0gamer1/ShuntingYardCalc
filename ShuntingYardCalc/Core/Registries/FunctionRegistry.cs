
namespace ShuntingYardCalc
{
    public  interface FunctionRegistry
    {
         Dictionary<string, FunctionSpec> Functions { get; }
         



        public bool IsFunc(string symbol);


        public bool TryGetFunc(string symbol, out FunctionSpec funcspec);


        //could be an interface func
        public void RegisterFunction(string symbol, FunctionSpec funcspec);

        

    }
}
