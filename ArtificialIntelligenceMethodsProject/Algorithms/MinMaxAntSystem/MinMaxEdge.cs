using System;
using System.Collections.Generic;
using System.Text;

namespace ArtificialIntelligenceMethodsProject.Algorithms.MinMaxAntSystem
{
    class MinMaxEdge
    {
        private double pheromone;
        public double MaxPheromone { private get; set; }
        public double MinPheromone { private set; get; }
        public MinMaxEdge(double pheromone, double maxPheromone = double.MaxValue, double minPheromone = 0)
        {
            this.pheromone = pheromone;
            MaxPheromone = maxPheromone;
            MinPheromone = minPheromone;
        }
        public double GetPheromone()
        {
            pheromone = pheromone > MaxPheromone ? MaxPheromone : pheromone;
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
