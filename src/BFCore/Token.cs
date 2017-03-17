
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
        EOF,
    }

    public class Token
    {
        public TokenType Type { get; }
        public string Literal { get; }

        public Token(TokenType type)
        {
            this.Type = type;
        }

        public Token(TokenType type, string literal)
        : this(type)
        {
            Literal = literal;
        }

        public const char EOFChar = '\0';

        public Token(TokenType type, char literal)
        : this(type, literal.ToString()) { }

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
                case EOFChar:
                    return TokenType.EOF;
                default:
                    return TokenType.None;
            }
        }
    }


}