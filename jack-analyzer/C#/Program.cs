using System;
using System.Collections.Generic;

using System.Linq;
using JackAnalyzer.Files;
using JackAnalyzer.Parser;
using JackAnalyzer.Tokenizer;

namespace jack_analyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (string.IsNullOrWhiteSpace(args[0]))
            {
                Console.WriteLine("No file(s) provided.");
                return;
            }

            var path = args[0];
            var inputFiles = new List<JackFile>();

            inputFiles.Add(new JackFile(path));

            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Tokenize(inputFiles);

            var parser = new Parser(tokens, "class");
            parser.Parse();

            Console.WriteLine(tokens.Any());
        }
    }
}