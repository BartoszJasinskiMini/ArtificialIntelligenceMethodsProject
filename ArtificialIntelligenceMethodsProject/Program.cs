using System;
using ArtificialIntelligenceMethodsProject.CLI;
using CommandDotNet;

namespace ArtificialIntelligenceMethodsProject
{
    class Program
    {
        static int Main(string[] args)
        {
            return new AppRunner<Cli>().Run(args);
        }
    }
}