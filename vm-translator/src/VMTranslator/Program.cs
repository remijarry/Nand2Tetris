using System;
using System.IO;
using System.Linq;
using System.Text;

namespace VMTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            // You have already handled the constant segment.
            // 1. Next, handle the segments local, argument, this, and that.
            // 2. Next, handle the pointer and temp segments, in particular allowing modiﬁca-
            // tion of the bases of the this and that segments.
            // 3. Finally, handle the static segment.
            // dotnet VMTranslator MyProg.vm

            Console.WriteLine("Hello, World!");

            if (string.IsNullOrWhiteSpace(args[0]))
            {
                Console.WriteLine("Usage: dotnet VMTranslator <filename>");
                return;
            }

            var fileName = args[0];
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    var parser = new Parser(sr);
                    var commands = parser.Parse();

                    var codeWriter = new CodeWriter(fileName);
                    var sb = new StringBuilder();

                    var cmds = commands.Commands.GetCommands();
                    for (var i = 0; i < cmds.Count; i++)
                    {
                        sb.Append(codeWriter.WriteCommand(cmds[i]));
                    }

                    foreach (var func in commands.Functions)
                    {
                        sb.Append(codeWriter.WriteFunction(func));
                    }

                    var finalInstructions = RemoveEmptyLines(sb.ToString());
                    var outputFilePath = "src/VMTranslator/test-files/test-files.asm";

                    File.WriteAllText(outputFilePath, finalInstructions);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error reading the file: {e.Message}");
            }

        }

        private static string RemoveEmptyLines(string input)
        {
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var nonEmptyLines = lines.Where(line => !string.IsNullOrWhiteSpace(line));
            return string.Join(Environment.NewLine, nonEmptyLines);
        }
    }
}
