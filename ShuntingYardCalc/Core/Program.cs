
using ShuntingYardCalc.Specs;
using System;

namespace ShuntingYardCalc
{




    internal class Program
    {
        public static OperatorRegistry opreg = new OperatorRegistry();
        
        public static void RegisterDefaults()
        {

            

            var ops= new[]
            {
                new OperatorSpec {Symbol = "+", Associativity = Associativity.Left, Precedence = 1, Arity = 2, Operation = (double a, double b)=> (a + b)   },
                new OperatorSpec {Symbol = "-", Associativity = Associativity.Left, Precedence = 1, Arity = 2, Operation = (double a, double b)=>(a - b)  },
                new OperatorSpec {Symbol = "*", Associativity = Associativity.Left, Precedence = 2, Arity = 2, Operation = (double a, double b)=>(a * b)  },
                new OperatorSpec {Symbol = "/", Associativity = Associativity.Left, Precedence = 2, Arity = 2, Operation = (double a, double b)=>{ if (b == 0) {throw new DivideByZeroException(); } else return a / b; }  },
                
                new OperatorSpec {Symbol = "^", Associativity = Associativity.Right, Precedence = 3, Arity = 2, Operation = (double a, double b)=>(Math.Pow(a, b))  },
                new OperatorSpec {Symbol = "u-", Associativity = Associativity.Right, Precedence = 3, Arity = 2, UnaryOperation = (double a)=>(a * -1)  },
                new OperatorSpec {Symbol = "u+", Associativity = Associativity.Right, Precedence = 3, Arity = 2, UnaryOperation = (double a)=>(a)  },
            };
            foreach(OperatorSpec opspec in ops)
            {
                opreg.Register(opspec.Symbol, opspec);
            }


        }
  

        static void Main(string[] args)
        {




            while (true)
            {
                int i = 1;

                RegisterDefaults();
                string? inp = Console.ReadLine();
                List<Tokenizer.Token> ls = Tokenizer.Tokenize(inp, opreg);


                List<string> hay = Parser.ToRPN(ls, opreg);

                Console.WriteLine(Evaluator.EvalRPN(hay, opreg));



            }



        }
    }
}
