


namespace ShuntingYardCalc
{
    public class Tokenizer
    {


        public struct Token(TokenType type, string token)
        {
            public TokenType type = type;
            public string token = token;
        }

        public static List<Token> Tokenize(string input, Registry registry)
        {
            var retList = new List<Token>();

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


                    retList.Add(new Token(TokenType.UnaryOperator, $"u{c.ToString()}"));
                }

                else
                if (char.IsDigit(c) || c == '.' && currentTokenType == TokenType.Number)
                {


                    if (currentToken == "")
                        currentTokenType = TokenType.Number;

                    if (currentTokenType == TokenType.Identifier)
                    {


                        retList.Add(new Token(TokenType.Identifier, currentToken));
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


                        retList.Add(new Token(TokenType.Number, currentToken));

                        currentToken = "";
                        currentTokenType = TokenType.Identifier;
                    }

                    lastTokenType = TokenType.Identifier;
                    currentToken += c;

                }

                else if (registry.IsOperator(c))
                {

                    if (currentToken != "")
                    {

                        retList.Add(new Token(lastTokenType, currentToken));
                        currentToken = "";
                        currentTokenType = TokenType.None;

                    }

                    lastTokenType = TokenType.Operator;


                    retList.Add(new Token(TokenType.Operator, c.ToString()));

                }
                else if (c == '(' || c == ')')
                {


                    if (currentToken != "")
                    {





                        retList.Add(new Token(lastTokenType, currentToken));

                        currentToken = "";
                        currentTokenType = TokenType.None;

                    }

                    if (c == '(')
                        lastTokenType = TokenType.Parenthesis;



                    retList.Add(new Token(TokenType.Parenthesis, c.ToString()));

                }
                else if (c == ',')
                {

                    if (currentToken != "")
                    {


                        retList.Add(new Token(lastTokenType, currentToken));

                        currentToken = "";
                        currentTokenType = TokenType.None;
                    }

                    lastTokenType = TokenType.Comma;

                    NumOfArgument++;

                    retList.Add(new Token(TokenType.Comma, c.ToString()));
                }


            }

            if (currentToken != "")
            {
                retList.Add(new Token(currentTokenType, currentToken));
            }



            return retList;
        }

    }
}
