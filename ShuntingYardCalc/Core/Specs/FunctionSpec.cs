
namespace ShuntingYardCalc
{
    public class FunctionSpec
    {
        private string symbol;
        private int precedence = 9;

        private Dictionary<int, Func<double[], double>> overloads = [];

        private Func<double[], double>? operation;



        private bool fixedArity;
        private int? arity;
        private int? minArity;


        public double Run(double[] args)
        {
            if (minArity != null && operation!= null) 
            {
                return operation(args);
            }

            return overloads[arity.GetValueOrDefault()](args);

        }



        public string Symbol { get => symbol; set => symbol = value; }
        public int Precedence { get => precedence; set => precedence = value; }
        public int? Arity { get => arity??0; 
            
            set 
            {

                arity = value??0;
                    
            
            }
        
        }
        public int? MinArity { get => minArity; set => minArity = value; }
        public bool FixedArity { get => fixedArity; set => fixedArity = value; }
        public Dictionary<int, Func<double[], double>> Overloads { get => overloads; set => overloads = value; }
        public Func<double[], double>? Operation { get => operation; set => operation = value; }
    }
}
