using System;
using System.Collections.Generic;
using System.Text;

namespace ArtificialIntelligenceMethodsProject.Algorithms.IACO
{
    public class IACOParameters
    {
        public int Alpha { get; }
        public int Beta { get; }
        public int PopulationSize { get; }
        public int MaxIterations { get; }
        public double Rho { get; }
        public double PBest { get;  }
        public double StartMinPheromone { get; }
        public double StartMaxPheromone { get; }
        public double pMin { get; set; }
        public double pMax { get; set; }
        

        public IACOParameters(int alpha, int beta, int populationSize, int maxIterations, double rho, double pBest, double startMinPheromone, double startMaxPheromone, double pMin, double pMax)
        {
            Alpha = alpha;
            Beta = beta;
            PopulationSize = populationSize;
            MaxIterations = maxIterations;
            Rho = rho;
            PBest = pBest;
            StartMinPheromone = startMinPheromone;
            StartMaxPheromone = startMaxPheromone;
            this.pMin = pMin;
            this.pMax = pMax;
        }
    }
}
