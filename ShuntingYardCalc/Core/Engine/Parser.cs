


using ShuntingYardCalc.Specs;

namespace ShuntingYardCalc
{
    public class Parser
    {
        

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
        public static List<string> ToRPN(List<Tokenizer.Token> tokenList, OperatorRegistry opreg)
        {

            var retList = new List<string>();

            var temp = new List<string>();




            for (int i = 0; i < tokenList.Count(); i++)
            {
                Tokenizer.Token currentToken = tokenList[i];
                temp.Add(currentToken.token);

                if (currentToken.type == TokenType.Number)
                {
                    retList.Add(currentToken.token);
                    TouchArgStartIfNeeded();

                }
                else
                if (currentToken.type == TokenType.Identifier)
                {
                    opStack.Push(currentToken.token);
                }
                else
                if (currentToken.type == TokenType.Comma)
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
                if (currentToken.type == TokenType.Operator || currentToken.type == TokenType.UnaryOperator)
                {
                    string? topStack;
                    bool isTopAccessible;

                    while ((isTopAccessible = opStack.TryPeek(out topStack)) && topStack != "(" && (opreg.GetOperator(topStack).Precedence > opreg.GetOperator(currentToken.token).Precedence || opreg.GetOperator(topStack).Precedence == opreg.GetOperator(currentToken.token).Precedence && opreg.GetOperator(currentToken.token).Associativity == Associativity.Left))
                    {
                        retList.Add(opStack.Pop());

                    }

                    opStack.Push(currentToken.token);

                }
                else
                if (currentToken.type == TokenType.Parenthesis)
                {
                    if (currentToken.token == "(")
                    {
                        opStack.Push(currentToken.token);

                        bool startsFunction = i > 0 && tokenList[i - 1].type == TokenType.Identifier;

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

                        if (Evaluator.IsValidFunc(topStack))
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

    }
}
