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
            if (string.IsNullOrWhiteSpace(args[0]))
            {
                Console.WriteLine("Usage: dotnet VMTranslator <filename>");
                return;
            }

            var fileName = args[0];

            if (!File.Exists(fileName))
            {
                Console.WriteLine($"The file {fileName} does not exist.");
                return;
            }
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

                    sb.Append(codeWriter.WriteEnd());

                    foreach (var func in commands.Functions)
                    {
                        sb.Append(codeWriter.WriteFunction(func));
                    }

                    var finalInstructions = RemoveEmptyLines(sb.ToString());
                    var inputDirectory = Path.GetDirectoryName(fileName);
                    string inputFileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                    string outputFilePath = Path.Combine(inputDirectory, $"{inputFileNameWithoutExtension}.asm");
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