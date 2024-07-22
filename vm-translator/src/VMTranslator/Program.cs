using System;
using System.IO;
using System.Linq;
using System.Text;
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

            var path = args[0];


            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path, "*.vm", SearchOption.AllDirectories);
                var sb = new StringBuilder();
                foreach (var file in files)
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        var parser = new Parser(sr);
                        var commandList = parser.Parse();
                        var translator = new Translator(commandList);
                        sb.AppendLine(translator.Translate());
                    }
                }
            }

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    var parser = new Parser(sr);
                    var commandList = parser.Parse();

                    var translator = new Translator(commandList);
                    var asm = translator.Translate();
                    var inputDirectory = Path.GetDirectoryName(path);
                    string outputFilePath = Path.Combine(inputDirectory, $"{Path.GetFileNameWithoutExtension(path)}.asm");
                    File.WriteAllText(outputFilePath, asm);
                }
            }

            Console.WriteLine($"The file {path} does not exist.");
            return;
        }

        private static string RemoveEmptyLines(string input)
        {
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var nonEmptyLines = lines.Where(line => !string.IsNullOrWhiteSpace(line));
            return string.Join(Environment.NewLine, nonEmptyLines);
        }
    }
}