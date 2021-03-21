using System;
using System.IO;
using ArtificialIntelligenceMethodsProject.CLI;
using ArtificialIntelligenceMethodsProject.IO;
using CommandDotNet;

namespace ArtificialIntelligenceMethodsProject
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine(Reader.ReadProblem(DataSet.S, "A-n53-k7").ToString());
            return 0;
            // return new AppRunner<Cli>().Run(args);
        }
    }
}