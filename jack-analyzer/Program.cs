using System;
using System.Collections.Generic;

using System.Linq;
using JackAnalyzer.Files;
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

            var xml = tokenizer.ToXml(tokens);

            Console.WriteLine(tokens.Any());
        }
    }
}