
namespace ShuntingYardCalc.Specs
{

    public class OperatorSpec
    {

        private string symbol;
        private int precedence, arity;
        private Associativity associativity;

        private Func<double, double, double>? operation;
        private Func<double, double>? unaryOperation;

        public string Symbol { get => symbol; set => symbol = value; }
        public int Precedence { get => precedence; set => precedence = value; }
        public int Arity { get => arity; set => arity = value; }
        public Associativity Associativity { get => associativity; set => associativity = value; }
        public Func<double, double, double>? Operation { get => operation; set => operation = value; }
        public Func<double, double>? UnaryOperation { get => unaryOperation; set => unaryOperation = value; }
    }






}
