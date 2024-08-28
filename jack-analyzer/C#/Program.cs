using System;
using System.Collections.Generic;
using JackAnalyzer.Files;


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

        }
    }
}