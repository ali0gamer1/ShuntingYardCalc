namespace ShuntingYardCalc
{


    internal partial class Program
    {

        public class OperatorInfo
        {
            public int Precedence { get; set; }
            public string Associativity { get; set; }  // "Left" or "Right"

            public OperatorInfo(int precedence, string associativity)
            {
                Precedence = precedence;
                Associativity = associativity;
            }
        }

        public enum TokenType
        {
            None,
            Number,
            Identifier,
            UnaryOperator,
            Parenthesis,
            Comma,
            Operator
        }


        public enum Associativity { Left, Right, None }



        public static Dictionary<string, OperatorInfo> operators = new Dictionary<string, OperatorInfo>()
        {
            { "+", new OperatorInfo(1, "Left") },
            { "-", new OperatorInfo(1, "Left") },
            { "*", new OperatorInfo(2, "Left") },
            { "/", new OperatorInfo(2, "Left") },
            { "^", new OperatorInfo(3, "Right") },
            { "u+", new OperatorInfo(4, "Right") },
            { "u-", new OperatorInfo(4, "Right") },
            {"func", new OperatorInfo(9, "none") }
        };


        public static class Util
        {



            public static bool IsValidFunc(string token)
            {
                return token == "pow" || token == "max" || token == "min" || token == "sin" || token == "cos";
            }


            public static bool IsOperator(char c)
            {
                return c == '+' || c == '/' || c == '-' || c == '*' || c == '^';
            }

            public static bool IsOperator(string c)
            {
                return c == "+" || c == "/" || c == "-" || c == "*" || c == "^";
            }


            public static List<(TokenType, string)> Tokenize(string input)
            {
                List<(TokenType, string)> retList = new List<(TokenType, string)>();

                input = input.Replace(" ", "");

                string currentToken = "";
                //string currentTokenType = "";
                //string lastTokenType = "";


                TokenType currentTokenType, lastTokenType;

                currentTokenType = lastTokenType = TokenType.None;


                int NumOfArgument = 0;

                for (int i = 0; i < input.Length; i++)
                {
                    char c = input[i];


                    if ((c == '+' || c == '-') && (lastTokenType == TokenType.Operator || lastTokenType == TokenType.Comma || lastTokenType == TokenType.None || lastTokenType == TokenType.Parenthesis))
                    {


                        retList.Add((TokenType.UnaryOperator, $"u{c.ToString()}"));
                    }

                    else
                    if (char.IsDigit(c) || c == '.' && currentTokenType == TokenType.Number)
                    {


                        if (currentToken == "")
                            currentTokenType = TokenType.Number;

                        if (currentTokenType == TokenType.Identifier)
                        {


                            retList.Add((TokenType.Identifier, currentToken));
                            currentToken = "";
                            currentTokenType = TokenType.Number;
                        }

                        lastTokenType = TokenType.Number;
                        currentToken += c;

                    }
                    else if (char.IsAsciiLetter(c)) //change to support lower versions
                    {
                        if (currentToken == "")
                            currentTokenType = TokenType.Identifier;

                        if (currentTokenType == TokenType.Number)
                        {
                            lastTokenType = TokenType.Number;


                            retList.Add((TokenType.Number, currentToken));

                            currentToken = "";
                            currentTokenType = TokenType.Identifier;
                        }

                        lastTokenType = TokenType.Identifier;
                        currentToken += c;

                    }

                    else if (IsOperator(c))
                    {

                        if (currentToken != "")
                        {

                            retList.Add((lastTokenType, currentToken));
                            currentToken = "";
                            currentTokenType = TokenType.None;
                        }

                        lastTokenType = TokenType.Operator;


                        retList.Add((TokenType.Operator, c.ToString()));

                    }
                    else if (c == '(' || c == ')')
                    {


                        if (currentToken != "")
                        {





                            retList.Add((lastTokenType, currentToken));

                            currentToken = "";
                            currentTokenType = TokenType.None;

                        }

                        if (c == '(')
                            lastTokenType = TokenType.Parenthesis;



                        retList.Add((TokenType.Parenthesis, c.ToString()));

                    }
                    else if (c == ',')
                    {

                        if (currentToken != "")
                        {


                            retList.Add((lastTokenType, currentToken));

                            currentToken = "";
                            currentTokenType = TokenType.None;
                        }

                        lastTokenType = TokenType.Comma;

                        NumOfArgument++;

                        retList.Add((TokenType.Comma, c.ToString()));
                    }


                }

                if (currentToken != "")
                {
                    retList.Add((currentTokenType, currentToken));
                }



                return retList;
            }
        }
    }
}
