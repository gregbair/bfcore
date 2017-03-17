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
    public static partial class Evaluator
    {
        public static Environment Eval(Node node, Environment env = null)
        {
            if (env == null) env = new Environment();
            TypeSwitch.Do(
                node,
                TypeSwitch.Case<ProgramNode>(() => evalProgram((ProgramNode)node, env)),
                TypeSwitch.Case<ManipulationStatement>(() => evalManipulation((ManipulationStatement)node, env)),
                TypeSwitch.Case<PointerStatement>(() => evalPointer((PointerStatement)node, env)),
                TypeSwitch.Case<Loop>(() => evalLoop((Loop)node, env)),
                TypeSwitch.Case<OutputStatment>(() => evalOutputStatement((OutputStatment)node, env)),
                TypeSwitch.Case<InputStatement>(() => evalInputStatement((InputStatement)node, env))
            );

            return env;
        }

        private static void evalInputStatement(InputStatement node, Environment env)
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
                evalInputStatement(node, env);
            }
        }
        private static void evalOutputStatement(OutputStatment node, Environment env)
        {
            int pos = env.Position;

            Console.Write((char)env.State[pos]);
        }

        private static void evalLoop(Loop loop, Environment env)
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

        private static void evalProgram(ProgramNode prog, Environment env)
        {
            foreach (var statement in prog.Statements)
            {
                Eval(statement, env);
            }
        }

        private static void evalPointer(PointerStatement statement, Environment env)
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

        private static void evalManipulation(ManipulationStatement statement, Environment env)
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