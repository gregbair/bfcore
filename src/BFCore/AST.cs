using System.Collections.Generic;
using System.Text;

namespace BFCore
{
    public abstract class Node
    {
        public Token Token { get; set; }
        public virtual string TokenLiteral() => Token.Literal;
    }

    public abstract class Statement : Node { }
    public abstract class Expression : Node { }

    public class ProgramNode
    {
        public List<Statement> Statements { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var s in Statements)
            {
                sb.Append(s.ToString());
            }
            return sb.ToString();
        }
    }

    public enum PointerDirection
    {
        Left,
        Right,
    }
    public class PointerStatement : Statement
    {
        public PointerDirection Direction { get; set; }

        public PointerStatement(Token tok, PointerDirection dir)
        {
            Token = tok;
            Direction = dir;
        }

        public override string ToString()
        {
            return $"Pointer - ({TokenLiteral()}: {Direction})";
        }

    }

    public enum ManipulationDirection
    {
        Plus,
        Minus,
    }

    public class ManipulationStatement : Statement
    {
        public ManipulationDirection Direction { get; set; }

        public ManipulationStatement(Token tok, ManipulationDirection dir)
        {
            Token = tok;
            Direction = dir;
        }

        public override string ToString()
        {
            return $"Manipulation - ({TokenLiteral()}: {Direction})";
        }
    }

    public class InputStatement : Statement
    {
        public InputStatement(Token token) => Token = token;

        public override string ToString() => $"Input - ({TokenLiteral()})";
    }

    public class OutputStatment : Statement
    {
        public OutputStatment(Token token) => Token = token;
        public override string ToString() => $"Output - ({TokenLiteral()})";
    }

    public class ExpressionStatement : Statement
    {
        public Expression Expression { get; set; }

        public override string ToString() => Expression?.ToString() ?? "";
    }

    public class LoopExpression : Expression
    {
        public List<Statement> Statements { get; set; } = new List<Statement>();
    }
}