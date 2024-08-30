using System;
using System.Collections.Generic;
using System.IO;
using JackAnalyzer.Files;
using JackAnalyzer.Parser;


namespace jack_analyzer
{
    class Program
    {
        static bool hadError = false;
        static void Main(string[] args)
        {
            // if (string.IsNullOrWhiteSpace(args[0]))
            // {
            //     Console.WriteLine("No file(s) provided.");
            //     return;
            // }

            // todo: dependency injection

            var path = "/home/tgrx/Documents/Dev/projects/Nand2Tetris/jack-analyzer/C#/test-files/ExpressionLessSquare/Main.jack";
            var source = File.ReadAllText(path);
            var scanner = new Scanner(source);
            var tokens = scanner.Scan();

            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }

            // var inputFiles = new List<JackFile>();

            // inputFiles.Add(new JackFile(path));

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
}