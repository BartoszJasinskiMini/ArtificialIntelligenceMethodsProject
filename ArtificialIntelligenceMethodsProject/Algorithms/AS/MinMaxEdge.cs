using System;
using System.Collections.Generic;
using System.Text;

namespace ArtificialIntelligenceMethodsProject.Algorithms.AS
{
    class MinMaxEdge
    {
        private double pheromone;
        // public static double MaxPheromone { private get; set; }
        public static double MinPheromone { private get; set; }
        public MinMaxEdge(double pheromone)
        {
            this.pheromone = pheromone;
        }
        public double GetPheromone()
        {
            // pheromone = pheromone > MaxPheromone ? MaxPheromone : pheromone;
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
