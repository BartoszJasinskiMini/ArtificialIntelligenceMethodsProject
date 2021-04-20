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
        string filepath = "C:\\Users\\mikol\\Source\\Repos\\BartoszJasinskiMini\\ArtificialIntelligenceMethodsProject\\ArtificialIntelligenceMethodsProject\\Graphs\\M\\A-n32-k5";

        [TestMethod]
        public void TestMethod1()
        {
            Problem problem = Reader.ReadProblem(filepath);
            GreedyAlgorithm greedyAlgorithm = new GreedyAlgorithm(1000.0);
            greedyAlgorithm.LoadProblemInstance(problem);
            greedyAlgorithm.Solve();
            Solution solution = greedyAlgorithm.GetSolution();
            Assert.IsNotNull(solution);
        }
    }
}
