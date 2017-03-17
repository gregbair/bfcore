using System.Collections.Generic;
using Xunit;

namespace BFCore.Tests
{
    public class LexerTests
    {
        [Fact]
        public void TestNextToken()
        {
            string input = "<>+-.,[]";

            List<Token> expectedTokens = new List<Token>{
                new Token(TokenType.LessThan, '<'),
                new Token(TokenType.GreaterThan,'>'),
                new Token(TokenType.Plus,'+'),
                new Token(TokenType.Minus,'-'),
                new Token(TokenType.Dot,'.'),
                new Token(TokenType.Comma,','),
                new Token(TokenType.LBracket,'['),
                new Token(TokenType.RBracket,']'),
                new Token(TokenType.EOF, "\0"),
            };

            Lexer l = new Lexer(input);

            foreach (var tt in expectedTokens)
            {
                Token tok = l.NextToken();

                Assert.Equal(tt.Type, tok.Type);
                Assert.Equal(tt.Literal, tok.Literal);
            }
        }
    }
}
