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
            readChar();
        }

        private void readChar()
        {
            _ch = _readPosition >= _input.Length ? Token.EOFChar : _input[_readPosition];

            _position = _readPosition;
            _readPosition++;
        }

        private void skipWhiteSpace()
        {
            while (Token.GetTokenTypeForChar(_ch) == TokenType.None)
            {
                readChar();
            }
        }

        public Token NextToken()
        {

            skipWhiteSpace();

            TokenType tt = Token.GetTokenTypeForChar(_ch);
            char c = _ch;
            readChar();

            return new Token(tt, c);
        }
    }
}