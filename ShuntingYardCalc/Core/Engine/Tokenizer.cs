


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


            TokenType currentTokenType;

            TokenContext lastContext = TokenContext.ExpectValue;


            currentTokenType = TokenType.None;


            int NumOfArgument = 0;

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];



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

                    
                    currentToken += c;

                }
                else if (char.IsAsciiLetter(c)) //change to support lower versions
                {
                    if (currentToken == "")
                        currentTokenType = TokenType.Identifier;

                    if (currentTokenType == TokenType.Number)
                    {


                        retList.Add(new Token(TokenType.Number, currentToken));

                        currentToken = "";
                        currentTokenType = TokenType.Identifier;
                    }

                    
                    currentToken += c;

                }

                else if (registry.IsOperator(c))
                {
                    bool hasPending = currentToken.Length > 0;
                    var contextForOp = hasPending ? TokenContext.ValueEnded : lastContext;

                    if (hasPending)
                    {
                        retList.Add(new Token(currentTokenType, currentToken));
                        currentToken = "";
                        currentTokenType = TokenType.None;
                    }

                    bool isPlusMinus = (c == '+' || c == '-');
                    bool isUnary = isPlusMinus && contextForOp == TokenContext.ExpectValue;

                    if (isUnary)
                        retList.Add(new Token(TokenType.UnaryOperator, $"u{c}"));
                    else
                        retList.Add(new Token(TokenType.Operator, c.ToString()));


                    lastContext = TokenContext.ExpectValue;


                    
                }
                else if (c == '(' || c == ')')
                {


                    if (currentToken != "")
                    {


                        retList.Add(new Token(currentTokenType, currentToken));

                        currentToken = "";
                        currentTokenType = TokenType.None;

                    }


                    if (c == '(')
                        lastContext = TokenContext.ExpectValue;

                    else
                        lastContext = TokenContext.ValueEnded;


                    retList.Add(new Token(TokenType.Parenthesis, c.ToString()));

                }
                else if (c == ',')
                {

                    if (currentToken != "")
                    {


                        retList.Add(new Token(currentTokenType, currentToken));

                        currentToken = "";
                        currentTokenType = TokenType.None;
                    }

                    lastContext = TokenContext.ExpectValue;

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
