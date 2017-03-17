using Xunit;

namespace BFCore.Tests
{

    public class ParserTests
    {

        [Fact]
        public void TestParsingPointerMovement()
        {
            string input = "<<<>>>";

            Lexer l = new Lexer(input);
            Parser p = new Parser(l);

            ProgramNode prog = p.ParseProgram();
            Assert.Equal(input.Length, prog.Statements.Count);

            var stmts = prog.Statements;
            for (int i = 0; i < input.Length; i++)
            {
                PointerDirection d = input[i] == '<' ? PointerDirection.Left : PointerDirection.Right;
                Assert.IsType<PointerStatement>(stmts[i]);
                Assert.Equal(d, ((PointerStatement)stmts[i]).Direction);
            }
        }

        [Fact]
        public void TestParsingManipulation()
        {
            string input = "+-+-+-";

            Lexer l = new Lexer(input);
            Parser p = new Parser(l);

            ProgramNode prog = p.ParseProgram();

            Assert.Equal(input.Length, prog.Statements.Count);

            var stmts = prog.Statements;
            for (int i = 0; i < input.Length; i++)
            {
                ManipulationDirection d = input[i] == '+' ? ManipulationDirection.Plus : ManipulationDirection.Minus;
                Assert.IsType<ManipulationStatement>(stmts[i]);
                Assert.Equal(d, ((ManipulationStatement)stmts[i]).Direction);
            }
        }

        [Fact]
        public void TestParsingLoops()
        {
            string input = "[>>++]++";
            Lexer l = new Lexer(input);
            Parser p = new Parser(l);

            ProgramNode prog = p.ParseProgram();

            Assert.Equal(3, prog.Statements.Count);

            Assert.IsType<Loop>(prog.Statements[0]);
            var loop = (Loop)prog.Statements[0];

            for (int i = 0; i < 4; i++)
            {
                var stmt = loop.Statements[i];
                switch (input[i + 1])
                {
                    case '>':
                        Assert.IsType<PointerStatement>(stmt);
                        Assert.Equal(PointerDirection.Right, ((PointerStatement)stmt).Direction);
                        break;
                    case '+':
                        Assert.IsType<ManipulationStatement>(stmt);
                        Assert.Equal(ManipulationDirection.Plus, ((ManipulationStatement)stmt).Direction);
                        break;
                    default:
                        Assert.False(true, $"Whoops! {stmt}");
                        break;
                }
            }
        }

        [Fact]
        public void TestParsingInputOutput()
        {
            string input = ",.,.";

            Lexer l = new Lexer(input);
            Parser p = new Parser(l);

            ProgramNode prog = p.ParseProgram();

            Assert.Equal(4, prog.Statements.Count);

            for (int i = 0; i < input.Length; i++)
            {
                var stmt = prog.Statements[i];
                switch (input[i])
                {
                    case ',':
                        Assert.IsType<InputStatement>(stmt);
                        break;
                    case '.':
                        Assert.IsType<OutputStatment>(stmt);
                        break;
                    default:
                        Assert.False(true, $"Whoops! {stmt}");
                        break;
                }
            }
        }
    }
}