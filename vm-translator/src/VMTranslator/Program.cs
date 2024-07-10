using System;
using System.IO;
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
