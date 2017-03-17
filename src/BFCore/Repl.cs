using System;

namespace BFCore
{
    public static class Repl
    {
        private const string PROMPT = ">> ";

        public static void Start()
        {
            while (true)
            {
                Console.Write(PROMPT);
                string input = Console.ReadLine();

                var l = new Lexer(input);
                var p = new Parser(l);
                var env = Evaluator.Eval(p.ParseProgram());

                Console.WriteLine();
            }
        }
    }
}