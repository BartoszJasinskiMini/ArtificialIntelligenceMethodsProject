using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArtificialIntelligenceMethodsProject.Algorithms;
using ArtificialIntelligenceMethodsProject.CLI;
using ArtificialIntelligenceMethodsProject.IO;
using ArtificialIntelligenceMethodsProject.Models;

namespace ArtificialIntelligenceMethodsTests
{
    [TestClass]
    public class GreedyAlgorithmTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Problem problem = Reader.ReadProblem(DataSet.M, "A-n32-k5");
            GreedyAlgorithm greedyAlgorithm = new GreedyAlgorithm();
            greedyAlgorithm.LoadProblemInstance(problem);
            greedyAlgorithm.Solve();
            Solution solution = greedyAlgorithm.GetSolution();


        }
    }
}
