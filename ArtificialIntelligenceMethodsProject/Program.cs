using System;
using System.IO;

namespace ArtificialIntelligenceMethodsProject
{
    class Program
    {
        static void Main(string[] args)
        {
            GreedyAlgorithm greedyAlgorithm = new GreedyAlgorithm();
            greedyAlgorithm.LoadProblemInstance(Reader.ReadProblem(DataSet.S, "A-n53-k7"));
            greedyAlgorithm.Solve();
        }
    }
}