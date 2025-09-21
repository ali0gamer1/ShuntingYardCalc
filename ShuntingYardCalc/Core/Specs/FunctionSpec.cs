
namespace ShuntingYardCalc
{
    public class FunctionSpec
    {
        private string symbol;
        private int precedence;
        private Func<double[], double>? operation;
        private bool fixedArity;
        private int? arity;
        private int? minArity;
        private int? maxArity;


        public Func<double[], double>? Operation 
        { get => operation; set => operation = value; }
        public string Symbol { get => symbol; set => symbol = value; }
        public int Precedence { get => precedence; set => precedence = value; }
        public int? Arity { get => arity; set => arity = value; }
        public int? MinArity { get => minArity; set => minArity = value; }
        public int? MaxArity { get => maxArity; set => maxArity = value; }
        public bool FixedArity { get => fixedArity; set => fixedArity = value; }
    }
}
