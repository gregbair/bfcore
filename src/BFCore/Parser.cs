using System;
using System.Collections.Generic;

namespace BFCore
{
    public class Parser
    {
        private Lexer _lexer;
        private Token _curToken;
        private Token _peekToken;

        public Parser(Lexer l)
        {
            this._lexer = l;
            // read two tokens, so _curToken and _peekToken are both set.
            NextToken();
            NextToken();
        }

        private void NextToken()
        {
            _curToken = _peekToken;
            _peekToken = _lexer.NextToken();
        }

        public ProgramNode ParseProgram()
        {
            var program = new ProgramNode();

            program.Statements = new List<Statement>();

            while (_curToken.Type != TokenType.EOF)
            {
                var stmt = ParseStatement();
                if (stmt != null)
                {
                    program.Statements.Add(stmt);
                }
                NextToken();
            }

            return program;
        }

        private Statement ParseStatement()
        {
            switch (_curToken.Type)
            {
                case TokenType.LessThan:
                    return new PointerStatement(_curToken, PointerDirection.Left);
                case TokenType.GreaterThan:
                    return new PointerStatement(_curToken, PointerDirection.Right);
                case TokenType.Plus:
                    return new ManipulationStatement(_curToken, ManipulationDirection.Plus);
                case TokenType.Minus:
                    return new ManipulationStatement(_curToken, ManipulationDirection.Minus);
                case TokenType.LBracket:
                    return parseLoop();
                case TokenType.RBracket:
                    // We should never get this. Taken care of by case above.
                    throw new InvalidOperationException("Encounterd LBracket");
                case TokenType.Comma:
                    return new InputStatement(_curToken);
                case TokenType.Dot:
                    return new OutputStatment(_curToken);
                default:
                    return null;
            }

        }

        private Loop ParseLoop()
        {
            Loop loop = new Loop();
            loop.Token = _curToken;
            NextToken();
            while (_curToken.Type != TokenType.RBracket)
            {
                loop.Statements.Add(ParseStatement());
                NextToken();
            }
            return loop;
        }
    }
}