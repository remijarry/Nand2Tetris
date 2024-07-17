using System;
using System.IO;
using VMTranslator.Parsing;

namespace VMTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = "/home/tgrx/Documents/Dev/Nand2Tetris/vm-translator/src/Refactor/VMTranslator/Files/test.vm";

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
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error reading the file: {e.Message}");
            }
        }
    }
}
