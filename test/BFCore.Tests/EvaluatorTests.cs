using Xunit;
using System.Linq;
using System;

namespace BFCore.Tests
{
    public class EvaluatorTests
    {
        [Fact]
        public void TestEnvirornment()
        {
            Environment sut = new Environment();

            var state = sut.State;

            Assert.Equal(30000, state.Length);
            Assert.True(state.All(x => x == (char)0));
        }

        [Fact]
        public void TestSimple()
        {
            string input = "++>++-<->++"; // end should be [1][3]
            Lexer l = new Lexer(input);
            Parser p = new Parser(l);
            var prog = p.ParseProgram();

            Environment env = Evaluator.Eval(prog);

            int[] state = env.State;

            Assert.Equal(1, state[0]);
            Assert.Equal(3, state[1]);
        }

        [Fact]
        public void TestLoop()
        {
            string input = @"+++++ +++++            initialize counter (cell #0) to 10
[                       use loop to set 70/100/30/10
    > +++++ ++              add  7 to cell #1
    > +++++ +++++           add 10 to cell #2
    > +++                   add  3 to cell #3
    > +                     add  1 to cell #4
<<<< -                  decrement counter (cell #0)
]";

            int[] expectedState = new int[] { 0, 70, 100, 30, 10, 0 };

            Lexer l = new Lexer(input);
            Parser p = new Parser(l);
            var prog = p.ParseProgram();

            Environment env = Evaluator.Eval(prog);

            int[] state = env.State;

            for (int i = 0; i < expectedState.Length; i++)
            {
                Assert.Equal(expectedState[i], state[i]);
            }
        }
    }
}