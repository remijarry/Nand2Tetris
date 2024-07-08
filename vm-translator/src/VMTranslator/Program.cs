using System;
using System.IO;
using System.Text;
using VMTranslator.Segments;

namespace VMTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            // stage I: Stack arithmetic commands + push constant x.
            // stage II: Memory Access commands.

            // dotnet VMTranslator MyProg.vm

            Console.WriteLine("Hello, World!");

            SegmentManager segmentManager = new SegmentManager();
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
                    foreach (var cmd in commands.GetCommands())
                    {
                        sb.Append(codeWriter.WriteCommand(cmd));
                    }
                    Console.WriteLine(sb.ToString());
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error reading the file: {e.Message}");
            }

        }
    }
}
