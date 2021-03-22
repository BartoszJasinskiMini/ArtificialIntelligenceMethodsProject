using ArtificialIntelligenceMethodsProject.Models;
using System;

namespace ArtificialIntelligenceMethodsProject.Algorithms
{
    interface IAlgorithm
    {
        public void LoadProblemInstance(Problem problem);
        public TimeSpan Solve();
        public Solution GetSolution();
    }
}
