using ArtificialIntelligenceMethodsProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ArtificialIntelligenceMethodsProject.Algorithms.MinMaxAntSystem
{
    public class MinMaxAntSystem : IAlgorithm
    {
        private Problem problem;
        private double cost;
        private List<int[]> routes;

        private int populationSize = 10;
        public MinMaxAntSystem()
        {

        }
        public Solution GetSolution()
        {
            throw new NotImplementedException();
        }

        public void LoadProblemInstance(Problem problem)
        {
            this.problem = problem;
            routes = null;
            cost = double.MaxValue;
        }

        public TimeSpan Solve()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
                // Create Ant Population
                // Iterate 
                // Update pheromone levels
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}