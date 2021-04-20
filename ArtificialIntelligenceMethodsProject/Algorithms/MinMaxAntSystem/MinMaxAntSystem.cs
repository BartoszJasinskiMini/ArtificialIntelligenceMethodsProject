using ArtificialIntelligenceMethodsProject.Misc;
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
        private MinMaxParameters parameters;
        private double maxVehicleDistance;
        public MinMaxAntSystem(MinMaxParameters parameters, double maxVehicleDistance)
        {
            this.parameters = parameters;
            this.maxVehicleDistance = maxVehicleDistance;
        }
        public Solution GetSolution()
        {
            return new Solution((int)cost, routes);
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
            Ant.edges = CreateEdges(problem);
            Ant.problem = problem;
            Ant.parameters = parameters;
            for (int i = 0; i < parameters.MaxIterations; i++)
            {
                List<Ant> population = CreatePopulation();
                for(int j = 0; j < population.Count; j++)
                {
                    population[j].FindRoute();
                }
                Ant bestAnt = FindBestSolution(population);
                bestAnt.EvaporatePheromone(parameters.Rho);
                bestAnt.UpdatePheromone();
                if(bestAnt.Cost < cost)
                {
                    cost = bestAnt.Cost;
                    routes = bestAnt.Routes;
                    SetPheromoneBounds();
                }
            }
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
        private void SetPheromoneBounds()
        {
            double maxValue = 1 / (1 - parameters.Rho) / (cost);
            int nG = problem.Graph.Vertices.Count;
            MinMaxEdge.MaxPheromone = maxValue;
            MinMaxEdge.MinPheromone = (maxValue * (1 - Math.Sqrt(parameters.PBest) * nG) ) / (((nG/2) - 1) * (Math.Sqrt(parameters.PBest) * nG) );
        }
        private List<Ant> CreatePopulation()
        {
            List<Ant> population = new List<Ant>();
            List<int> startVertices = RandomGenerator.GenerateRandom(parameters.PopulationSize, 1, problem.Graph.Vertices.Count - 1);
            foreach (int vertice in startVertices)
            {
                Ant ant = new Ant(vertice);
                population.Add(ant);
            }
            return population;
        }
        private Edges CreateEdges(Problem problem)
        {
            Edges edges = new Edges();
            MinMaxEdge.MaxPheromone = parameters.StartMaxPheromone;
            MinMaxEdge.MinPheromone = parameters.StartMinPheromone;
            for(int i = 0; i < problem.Graph.Vertices.Count; i++)
            {
                for(int j = i; j < problem.Graph.Vertices.Count; j++)
                {
                    edges.AddEdge(i, j, new MinMaxEdge(parameters.StartMaxPheromone));
                }
            }
            
            return edges;
        }
        private Ant FindBestSolution(List<Ant> population)
        {
            int bestIndex = 0;
            for(int i = 0; i < population.Count; i++)
            {
                if (population[bestIndex].Cost > population[i].Cost)
                    bestIndex = i;
            }
            return population[bestIndex];
        }
    }
}