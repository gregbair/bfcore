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
            nextToken();
            nextToken();
        }

        private void nextToken()
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
                var stmt = parseStatement();
                if (stmt != null)
                {
                    program.Statements.Add(stmt);
                }
                nextToken();
            }

            return program;
        }

        private Statement parseStatement()
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
                    ExpressionStatement stmt = new ExpressionStatement();
                    stmt.Token = _curToken;
                    stmt.Expression = parseLoop();
                    return stmt;
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

        private LoopExpression parseLoop()
        {
            LoopExpression loop = new LoopExpression();
            loop.Token = _curToken;
            nextToken();
            while (_curToken.Type != TokenType.RBracket)
            {
                loop.Statements.Add(parseStatement());
                nextToken();
            }
            return loop;
        }
    }
}