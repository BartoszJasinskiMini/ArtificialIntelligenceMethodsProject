using System;
using System.Collections.Generic;
using System.Text;

namespace ArtificialIntelligenceMethodsProject.Algorithms.AS
{
    class ACOEdge
    {
        private double pheromone;
        public static double MinPheromone { private get; set; }
        public ACOEdge(double pheromone)
        {
            this.pheromone = pheromone;
        }
        public double GetPheromone()
        {
            pheromone = pheromone < MinPheromone ? MinPheromone : pheromone;
            return pheromone;
        }
        public void AddPheromone(double addedPheromone)
        {
            pheromone += addedPheromone;
        }
        public void EvaporatePheromone(double percentile)
        {
            pheromone = pheromone * percentile;
        }
    }
}
