
using ShuntingYardCalc.Specs;

namespace ShuntingYardCalc
{
    public class Evaluator 
    {


        public static Dictionary<string, Func<double, double, double>>
            functionOperations = new Dictionary<string, Func<double, double, double>>
        {
            { "pow", (a, b) => Math.Pow(a,b) },

        };

        static bool TryPopTwo(Stack<double> stack, out double a, out double b)
        {


            bool f = stack.TryPop(out b);
            bool s = stack.TryPop(out a);

            return f && s;
        }

        public static double EvalRPN(List<string> tokenList, Registry registry)
        {
            Stack<double> resultstack = new Stack<double>();
            double a, b;


            foreach (string token in tokenList)
            {
                if (registry.IsOperator(token))
                {

                    OperatorSpec op;
                    if (!registry.TryGetOperator(token, out op))
                        throw new Exception("Cannot get operator");

                    if (op.UnaryOperation == null && op.Operation != null)
                    {
                        if (TryPopTwo(resultstack, out a, out b))
                        {
                            var operation = op.Operation;
                            resultstack.Push(operation(a,b));
                        }
                        else
                        {
                            throw new Exception("Syntax error");
                        }

                    }
                    else 
                    if (op.UnaryOperation != null)
                    {
                        if (resultstack.TryPop(out double topstack))
                        {
    
                            var operation = op.UnaryOperation;
                            resultstack.Push(operation(topstack));

                        }    
                        
                    }
                    
                }
                else
                if (registry.IsFunc(token))
                {

                    double tempArgCount;
                    if (!resultstack.TryPop(out tempArgCount))
                    {
                        throw new Exception("illegal operation");
                    }


                    int argCount = (int)tempArgCount;

                    FunctionSpec funcspec;

                    double[] args = new double[argCount];

                    for(int i = 0; i < argCount; i++)
                    {
                        args[i] = resultstack.Pop();
                    }

                    if (registry.TryGetFunc(token, out funcspec))
                    {
                        resultstack.Push(funcspec.Operation(args));    
                        
                    }    

                }
              
                else
                if (token.Length > 0 && token[0] == '#')
                {
                    resultstack.Push(double.Parse(token.Substring(1, token.Length - 1)));

                }
                else
                    resultstack.Push(double.Parse(token));



            }

            double retval;

            resultstack.TryPop(out retval);
            return retval;

        }






    }
}
