using JackAnalyzer.SyntaxAnalyzer;
using JackAnalyzer.SyntaxAnalyzer.Interfaces;

namespace JackAnalyzer;

class Program
{
    static bool hadError;
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
            RunFiles(args[0]);
        }
        else
        {
            RunPrompt();
        }
    }

    private static void RunFiles(string path)
    {
        // if (!Path.IsPathRooted(path))
        // {

        // }
        // var currentDirectory = Directory.GetCurrentDirectory();
        // path = Path.Combine(currentDirectory, path);

        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Tests/ExpressionLessSquare/Main.jack");
        string content = File.ReadAllText(filePath);
        // var source = File.ReadAllText(path);
        
        ITokenizer tokenizer = new Tokenizer(content);
        var tokens = tokenizer.ScanTokens();
        
        Console.WriteLine("<tokens>");
        foreach (var token in tokens)
        {
            Console.WriteLine(token);
        }
        Console.WriteLine("</tokens>");

        return;
    }

    private static void RunPrompt()
    {
        for (; ; )
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
        var tokens = tokenizer.ScanTokens();

        foreach (var token in tokens)
        {
            Console.WriteLine(token);
        }
    }

    public static void Error(int line, string message)
    {
        Report(line, "", message);
    }

    private static void Report(int line, string where, string message)
    {
        Console.Error.WriteLine($"[line {line}] Error {where}: {message}");
        hadError = true;
    }
}
