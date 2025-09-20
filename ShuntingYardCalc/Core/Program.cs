
using ShuntingYardCalc.Specs;
using System;

namespace ShuntingYardCalc
{




    internal class Program
    {
        public static Registry registry = new();
        
        public static void RegisterDefaults()
        {

            
            //default operators
            var ops= new[]
            {
                //i could've included isUnary but instead i defined separate operations for unary and binary operators
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
                registry.RegisterOperator(opspec.Symbol, opspec);
            }

            //default functions

            var funcs = new[]
            {
                new FunctionSpec{Symbol = "max", Precedence = 9,
                    Operation = (double[] args)=>
                    {
                        return args.Max();
                    }
                
                },

                new FunctionSpec{Symbol = "pow", Precedence = 9,
                    Operation = (double[] args)=>
                    {
                        if (args.Length == 2)
                            return Math.Pow(args[0], args[1]);
                        else
                            throw new Exception("argument number invalid");
                    }

                },

                new FunctionSpec{Symbol = "floor", Precedence = 9,

                    Operation = (double[] args)=>{return Math.Floor(args[0]); }
                
                },
                new FunctionSpec{Symbol = "ceil", Precedence = 9,

                    Operation = (double[] args)=>{return Math.Ceiling(args[0]); }

                },



            };


            foreach(var funcspec in funcs)
            {
                registry.RegisterFunction(funcspec.Symbol, funcspec);
            }



        }
  

        static void Main(string[] args)
        {


            RegisterDefaults();

            while (true)
            {

               
                string? inp = Console.ReadLine();
                
                if (string.IsNullOrEmpty(inp))
                {
                    throw new ArgumentNullException();
                }

                List<Tokenizer.Token> ls = Tokenizer.Tokenize(inp, registry);


                List<string> hay = Parser.ToRPN(ls, registry);

                Console.WriteLine(Evaluator.EvalRPN(hay, registry));



            }



        }
    }
}
