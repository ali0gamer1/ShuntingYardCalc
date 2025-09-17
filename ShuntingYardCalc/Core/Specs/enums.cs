namespace ShuntingYardCalc
{


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

    public enum Associativity
    {
        None, Left, Right
    }

    
}
