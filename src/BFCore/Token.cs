
namespace BFCore
{

    public enum TokenType
    {
        None,
        Plus,
        Minus,
        LessThan,
        GreaterThan,
        Dot,
        Comma,
        LBracket,
        RBracket,
    }

    public class Token
    {
        public TokenType Type { get; }

        public Token(TokenType type)
        {
            this.Type = type;
        }

        public static TokenType GetTokenTypeForChar(char c)
        {
            switch (c)
            {
                case '+':
                    return TokenType.Plus;
                case '-':
                    return TokenType.Minus;
                case '<':
                    return TokenType.LessThan;
                case '>':
                    return TokenType.GreaterThan;
                case '.':
                    return TokenType.Dot;
                case ',':
                    return TokenType.Comma;
                case '[':
                    return TokenType.LBracket;
                case ']':
                    return TokenType.RBracket;
                default:
                    return TokenType.None;
            }
        }
    }


}