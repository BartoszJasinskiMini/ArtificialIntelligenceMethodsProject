using ArtificialIntelligenceMethodsProject.Models;

namespace ArtificialIntelligenceMethodsProject.Algorithms
{
    interface IAlgorithm
    {
        public void LoadProblemInstance(Problem problem);
        public void Solve();
    }
}
