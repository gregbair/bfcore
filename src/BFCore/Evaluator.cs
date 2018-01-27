using System;

namespace BFCore
{

    public class Environment
    {
        private int[] _state = new int[30000];
        private int _curPos = 0;

        public int[] State { get { return _state; } }
        public int Position { get { return _curPos; } set { _curPos = value; } }
    }
    public static class Evaluator
    {
        public static Environment Eval(Node node, Environment env = null)
        {
            if (env == null) env = new Environment();
            switch (node)
            {
                case ProgramNode pn:
                    EvalProgram(pn, env);
                    break;
                case ManipulationStatement ms:
                    EvalManipulation(ms, env);
                    break;
                case PointerStatement ps:
                    EvalPointer(ps, env);
                    break;
                case Loop l:
                    EvalLoop(l, env);
                    break;
                case OutputStatment os:
                    EvalOutputStatement(os, env);
                    break;
                case InputStatement iStatement:
                    EvalInputStatement(iStatement, env);
                    break;
            }

            return env;
        }

        private static void EvalInputStatement(InputStatement node, Environment env)
        {
            Console.Write($"Enter character followed by <RET> to insert at position {env.Position}: ");
            string input = Console.ReadLine();
            int c = input[0];
            if (c <= 128)
            {
                env.State[env.Position] = c;
            }
            else
            {
                Console.WriteLine("Not a valid character. Try again.");
                EvalInputStatement(node, env);
            }
        }
        private static void EvalOutputStatement(OutputStatment node, Environment env)
        {
            int pos = env.Position;

            Console.Write((char)env.State[pos]);
        }

        private static void EvalLoop(Loop loop, Environment env)
        {
            int counterPos = env.Position;
            while (env.State[counterPos] > 0)
            {
                foreach (var stmt in loop.Statements)
                {
                    Eval(stmt, env);
                }
            }
        }

        private static void EvalProgram(ProgramNode prog, Environment env)
        {
            foreach (var statement in prog.Statements)
            {
                Eval(statement, env);
            }
        }

        private static void EvalPointer(PointerStatement statement, Environment env)
        {
            switch (statement.Direction)
            {
                case PointerDirection.Left:
                    if (env.Position > 0) { env.Position--; }
                    break;
                case PointerDirection.Right:
                    if (env.Position < env.State.Length) { env.Position++; }
                    break;
            }

        }

        private static void EvalManipulation(ManipulationStatement statement, Environment env)
        {
            switch (statement.Direction)
            {
                case ManipulationDirection.Plus:
                    env.State[env.Position]++;
                    break;
                case ManipulationDirection.Minus:
                    if (env.State[env.Position] > 0)
                        env.State[env.Position]--;
                    break;
            }
        }
    }
}