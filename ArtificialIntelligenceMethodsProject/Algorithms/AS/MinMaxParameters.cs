using System;
using System.Collections.Generic;
using System.Text;

namespace ArtificialIntelligenceMethodsProject.Algorithms.AS
{
    public class MinMaxParameters
    {
        public int Alpha { get; }
        public int Beta { get; }
        public int PopulationSize { get; }
        public int MaxIterations { get; }
        public double Rho { get; }
        public double PBest { get;  }
        public double StartMinPheromone { get; }
        public double StartMaxPheromone { get; }

        public MinMaxParameters(int alpha, int beta, int populationSize, int maxIterations, double rho, double pBest, double startMinPheromone, double startMaxPheromone)
        {
            Alpha = alpha;
            Beta = beta;
            PopulationSize = populationSize;
            MaxIterations = maxIterations;
            Rho = rho;
            PBest = pBest;
            StartMinPheromone = startMinPheromone;
            StartMaxPheromone = startMaxPheromone;
        }
    }
}
