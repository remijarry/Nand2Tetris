using JackAnalyzer.Scan;

namespace JackAnalyzer;

class Program
{
    static void Main(string[] args)
    {
        // TODO: handle multiple files.
        if (args.Length > 1)
        {
            Console.WriteLine("Usage JackAnalyzer [script]");
            return;
        }

        if (args.Length == 1)
        {
            RunFile(args[0]);
        }
        else 
        {
            RunPrompt();
        }
    }

    private static void RunFile(string path)
    {
        return;
    }

    private static void RunPrompt()
    {
        for(;;)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            if (line == null)
            {
                break;
            }

            Run(line);
        }
    }

    private static void Run(string line)
    {
        var tokenizer = new Tokenizer(line);
    }
}
