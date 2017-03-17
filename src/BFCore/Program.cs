using System.IO;

namespace BFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string path = args[0];
                string contents = File.ReadAllText(path);
                Repl.Start(contents);
            }
            else
            {
                Repl.Start();
            }
        }
    }
}
