
namespace ShuntingYardCalc
{




    internal partial class Program
    {

        public static readonly Dictionary<string, Func<double, double, double>>
            binaryOperations = new Dictionary<string, Func<double, double, double>>
        {
            { "+", (a, b) => a + b },
            { "-", (a, b) => a - b },
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
                if (Util.IsOperator(token))
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
                if (Util.IsValidFunc(token))
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



        public static Stack<string> opStack = new Stack<string>();
        public static Stack<int> argCountStack = new Stack<int>();
        public static Stack<bool> seenArgStack = new Stack<bool>();

        static void TouchArgStartIfNeeded()
        {
            if (argCountStack.Count > 0 && seenArgStack.Peek() == false)
            {
                seenArgStack.Pop(); seenArgStack.Push(true);
                if (argCountStack.Peek() == 0)
                {
                    var c = argCountStack.Pop();
                    argCountStack.Push(c + 1); // first argument
                }
            }
        }

        public static List<string> ToRPN(List<(TokenType, string)> tokenList)
        {

            var retList = new List<string>();

            var temp = new List<string>();




            for (int i = 0; i < tokenList.Count(); i++)
            {
                var tokTup = tokenList[i];
                temp.Add(tokTup.Item2);

                if (tokTup.Item1 == TokenType.Number)
                {
                    retList.Add(tokTup.Item2);
                    TouchArgStartIfNeeded();

                }
                else
                if (tokTup.Item1 == TokenType.Identifier)
                {
                    opStack.Push(tokTup.Item2);
                }
                else
                if (tokTup.Item1 == TokenType.Comma)
                {
                    string? topStack;


                    bool isTopAccessible;

                    while ((isTopAccessible = opStack.TryPeek(out topStack)) && topStack != "(")
                    {
                        retList.Add(opStack.Pop());


                    }


                    if (argCountStack.Count == 0)
                        throw new Exception("syntax error: comma outside function");

                    if (seenArgStack.Peek() == false)
                    {
                        foreach (var item in temp)
                        {
                            Console.Write(item + " ");
                        }
                        Console.WriteLine();

                        throw new Exception("syntax error: missing argument between commas");
                    }

                    // Count next argument and reset “seen” for the next one
                    argCountStack.Push(argCountStack.Pop() + 1);
                    seenArgStack.Pop(); seenArgStack.Push(false);




                    if (topStack != "(" && !isTopAccessible)
                    {
                        throw new Exception("syntax error: misplaced comma/missing parenthesis");
                    }

                }
                else
                if (tokTup.Item1 == TokenType.Operator || tokTup.Item1 == TokenType.UnaryOperator)
                {
                    string? topStack;
                    bool isTopAccessible;

                    while ((isTopAccessible = opStack.TryPeek(out topStack)) && topStack != "(" && (operators[topStack].Precedence > operators[tokTup.Item2].Precedence || operators[topStack].Precedence == operators[tokTup.Item2].Precedence && operators[tokTup.Item2].Associativity == "Left"))
                    {
                        retList.Add(opStack.Pop());

                    }

                    opStack.Push(tokTup.Item2);

                }
                else
                if (tokTup.Item1 == TokenType.Parenthesis)
                {
                    if (tokTup.Item2 == "(")
                    {
                        opStack.Push(tokTup.Item2);

                        bool startsFunction = i > 0 && tokenList[i - 1].Item1 == TokenType.Identifier;

                        if (startsFunction)
                        {
                            argCountStack.Push(0);
                            seenArgStack.Push(false);
                        }



                    }
                    else
                    {
                        string? topStack;
                        bool isTopAccessible;

                        while (isTopAccessible = opStack.TryPop(out topStack))
                        {
                            if (topStack == "(")
                                break;

                            retList.Add(topStack);
                        }




                        isTopAccessible = opStack.TryPeek(out topStack);

                        if (Util.IsValidFunc(topStack))
                        {
                            opStack.Pop();

                            if (argCountStack.Count == 0 || seenArgStack.Count == 0)
                                throw new Exception("syntax error: internal arg frame mismatch");

                            int n = argCountStack.Pop();
                            bool seen = seenArgStack.Pop();

                            // If we never hit a comma but did see a value, it was a single-arg call
                            if (n == 0 && seen) n = 1;

                            // Policy: forbid empty lists (change if you want max() to be allowed)
                            if (n == 0) throw new Exception("syntax error: empty argument list");

                            // Emit variadic function token (minimal change: encode arity in the token)
                            //retList.Add($"{topStack}#{n}");


                            retList.Add($"#{n}");
                            retList.Add(topStack);
                            TouchArgStartIfNeeded();


                        }



                    }

                }
            }



            while (opStack.TryPop(out string? topStack))
            {
                if (topStack == "(" || topStack == ")")
                {
                    throw new Exception("syntax error: mismatched parenthesis");
                }


                retList.Add(topStack);

            }


            return retList;
        }


        static void Main(string[] args)
        {




            while (true)
            {
                int i = 1;

                string? inp = Console.ReadLine();
                List<(TokenType, string)> ls = Util.Tokenize(inp);


                List<string> hay = ToRPN(ls);

                Console.WriteLine(EvalRPN(hay));



            }



        }
    }
}
