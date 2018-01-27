namespace BFCore
{
    public class Lexer
    {
        private string _input;
        private int _position;
        private int _readPosition;
        private char _ch;

        public Lexer(string input)
        {
            _input = input;
            ReadChar();
        }

        private void ReadChar()
        {
            _ch = _readPosition >= _input.Length ? Token.EOFChar : _input[_readPosition];

            _position = _readPosition;
            _readPosition++;
        }

        private void SkipWhiteSpace()
        {
            while (Token.GetTokenTypeForChar(_ch) == TokenType.None)
            {
                ReadChar();
            }
        }

        public Token NextToken()
        {

            SkipWhiteSpace();

            TokenType tt = Token.GetTokenTypeForChar(_ch);
            char c = _ch;
            ReadChar();

            return new Token(tt, c);
        }
    }
}