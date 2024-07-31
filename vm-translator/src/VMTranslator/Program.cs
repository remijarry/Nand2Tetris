using System;
using System.IO;
using VMTranslator.Files;
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
            // var path = "test-files";

            if (!Path.IsPathRooted(path))
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                path = Path.Combine(currentDirectory, path);
            }

            // if (!ContainsPath(path))
            // {
            //     var dir = Directory.GetCurrentDirectory();
            //     path = $"{dir}/{path}/";
            // }

            var inputFiles = new VMFiles();
            var isDirectory = false;
            if (Directory.Exists(path))
            {
                inputFiles.Files.AddRange(Directory.GetFiles(path, "*.vm", SearchOption.AllDirectories));
                inputFiles.IsDirectory = true;
                isDirectory = true;
                path = $"{path}/";
            }
            else if (File.Exists(path))
            {
                inputFiles.Files.Add(path);
            }

            var parser = new Parser(inputFiles);
            var commandList = parser.Parse();
            var translator = new Translator(commandList);
            var assemblyCode = translator.Translate();
            var fileWriter = new FileWriter(assemblyCode, path);
            if (isDirectory)
            {
                fileWriter.WriteDirectory(path);
            }
            else
            {
                fileWriter.WriteFile(path);
            }

            return;
        }

        public static bool ContainsPath(string filePath)
        {
            // Check if the path contains directory separator characters
            return filePath.Contains(Path.DirectorySeparatorChar.ToString()) || filePath.Contains(Path.AltDirectorySeparatorChar.ToString());
        }
    }

}