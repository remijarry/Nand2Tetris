using System;
using System.IO;
using System.Linq;
using VMTranslator.Parsing;
using VMTranslator.Translation;

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
                    var commandList = parser.Parse();

                    var translator = new Translator(commandList);
                    var asm = translator.Translate();
                    var inputDirectory = Path.GetDirectoryName(fileName);
                    string outputFilePath = Path.Combine(inputDirectory, $"{Path.GetFileNameWithoutExtension(fileName)}.asm");
                    File.WriteAllText(outputFilePath, asm);
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