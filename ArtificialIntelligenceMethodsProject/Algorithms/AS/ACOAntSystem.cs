using ArtificialIntelligenceMethodsProject.Misc;
using ArtificialIntelligenceMethodsProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ArtificialIntelligenceMethodsProject.Algorithms.AS
{
    public class ACOAntSystem : IAlgorithm
    {
        private Problem problem;
        private double cost;
        private List<int[]> routes;
        private ACOParameters parameters;
        private double maxVehicleDistance;
        private int solutionIteration;
        private List<(int iteration, double cost)> results;
        public ACOAntSystem(ACOParameters parameters, double maxVehicleDistance)
        {
            this.parameters = parameters;
            this.maxVehicleDistance = maxVehicleDistance;
        }
        public Solution GetSolution()
        {
            Solution solution = new Solution((int)cost, routes);  
            for(int i = 0; i < results.Count; i++)
            {
                double percentile = (results[i].cost - cost) / cost;
                if (percentile < 0.05)
                {
                    solution.ConvergentIteration = results[i].iteration;
                    break;
                }
            }
            solution.MaxIterations = parameters.MaxIterations;
            solution.ResultIteration = solutionIteration;
            return solution;
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
            results = new List<(int iteration, double cost)>();
            stopwatch.Start();
            Ant.edges = CreateEdges(problem);
            Ant.problem = problem;
            Ant.parameters = parameters;
            for (int i = 0; i < parameters.MaxIterations; i++)
            {
                List<Ant> population = CreatePopulation();
                for (int j = 0; j < population.Count; j++)
                {
                    population[j].FindRoute();
                    population[j].EvaporatePheromone(parameters.Rho);
                    population[j].UpdatePheromone();

                }
                Ant bestAnt = FindBestSolution(population);
                bestAnt.EvaporatePheromone(parameters.Rho);
                bestAnt.UpdatePheromone();
                if(bestAnt.Cost < cost)
                {
                    cost = bestAnt.Cost;
                    solutionIteration = i;
                    results.Add((i, cost));
                    routes = bestAnt.Routes;
                }
            }
            stopwatch.Stop();
            return stopwatch.Elapsed;
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
            // MinMaxEdge.MaxPheromone = parameters.StartMaxPheromone;
            // MinMaxEdge.MinPheromone = parameters.StartMinPheromone;
            for(int i = 0; i < problem.Graph.Vertices.Count; i++)
            {
                for(int j = i; j < problem.Graph.Vertices.Count; j++)
                {
                    edges.AddEdge(i, j, new ACOEdge(parameters.StartMaxPheromone));
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