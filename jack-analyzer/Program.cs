using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            Console.WriteLine(tokens.Any());
        }
    }
}
