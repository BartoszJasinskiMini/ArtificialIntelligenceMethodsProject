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
        public double GetPheromone()
        {
            pheromone = pheromone > MaxPheromone ? MaxPheromone : pheromone;
            pheromone = pheromone < MinPheromone ? MinPheromone : pheromone;
            return pheromone;
        }
        public void UpdatePhremone(double newValue)
        {
            pheromone = newValue;
        }
    }
}
