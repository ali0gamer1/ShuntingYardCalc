using static ShuntingYardCalc.Program;

namespace ShuntingYardCalc
{
    public class Evaluator 
    {

        


        public static readonly Dictionary<string, Func<double, double, double>>
            binaryOperations = new Dictionary<string, Func<double, double, double>>
        {
            { "+", (a, b) => a + b },
            { "-", (a, b) => a - b } ,
            { "*", (a, b) => a * b },
            { "/", (a, b) => {

                if (b == 0)
                {  throw new DivideByZeroException();

                } else
                {
                    return a / b;
                }

            }},
            { "^", (a, b) => Math.Pow(a, b) },

        };


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

        public static double EvalRPN(List<string> tokenList)
        {
            Stack<double> resultstack = new Stack<double>();
            double a, b;


            foreach (string token in tokenList)
            {
                if (OperatorRegistry.IsOperator(token))
                {
                    if (TryPopTwo(resultstack, out a, out b))
                    {

                        if (binaryOperations.TryGetValue(token, out var operation))
                        {
                            resultstack.Push(operation(a, b));
                        }
                        else
                            throw new Exception("Evaluation error");


                    }
                    else
                    {
                        throw new Exception("Syntax error");
                    }
                }
                else
                if (IsValidFunc(token))
                {// use argument list later (if possible) TODO.

                    double tempArgCount;
                    if (!resultstack.TryPop(out tempArgCount))
                    {
                        throw new Exception("illegal operation");
                    }


                    int argCount = (int)tempArgCount;

                    if (token == "pow")
                    {
                        if (TryPopTwo(resultstack, out a, out b))
                        {

                            if (functionOperations.TryGetValue(token, out var operation))
                            {
                                resultstack.Push(operation(a, b));
                            }
                            else
                                throw new Exception("Evaluation error");

                        }
                        else
                        {
                            throw new Exception("Syntax error");
                        }
                    }
                    else
                    if (token == "max")
                    {
                        double maxNum;
                        resultstack.TryPop(out maxNum);
                        argCount--;
                        while (argCount-- > 0)
                        {


                            double num = resultstack.Pop();

                            if (num > maxNum)
                                maxNum = num;


                        }

                        resultstack.Push(maxNum);



                    }



                }
                else
                if (token == "u+")
                { }
                else
                if (token == "u-")
                {
                    resultstack.Push(-1 * resultstack.Pop());

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


        public static bool IsValidFunc(string token)
        {
            return token == "pow" || token == "max" || token == "min" || token == "sin" || token == "cos";
        }





    }
}
