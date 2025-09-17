
namespace ShuntingYardCalc
{
    public class FunctionSpec
    {
        private string symbol;
        private int precedence;
        
        private Func<double[], double>? operation;

        public Func<double[], double>? Operation 
        { get => operation; set => operation = value; }
        public string Symbol { get => symbol; set => symbol = value; }
        public int Precedence { get => precedence; set => precedence = value; }
    }
}
