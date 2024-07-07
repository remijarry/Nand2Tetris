using System;
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

        }
    }
}
