using System;
using System.IO;

namespace ArtificialIntelligenceMethodsProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Reader.ReadProblem(DataSet.S, "A-n53-k7").ToString());
        }
    }
}