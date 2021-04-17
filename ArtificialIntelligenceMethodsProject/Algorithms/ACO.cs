using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArtificialIntelligenceMethodsProject.Misc;
using ArtificialIntelligenceMethodsProject.Models;

using static System.Math;

namespace ArtificialIntelligenceMethodsProject.Algorithms
{
    public class ACO : IAlgorithm
    {
        private Problem problem;
        private double cost;
        private List<int[]> routes;

        private int Alpha = 2;
        private int Beta = 5;
        private int Sigma = 3;
        private double ro = 0.8;
        private int th = 80;
        private int iterations = 100;
        private int ants = 22;

        private Dictionary<Tuple<int, int>, double> pheromones = new Dictionary<Tuple<int, int>, double>();
        
        public void LoadProblemInstance(Problem problem)
        {
            this.problem = problem;
            routes = null;
            cost = double.MaxValue;
            pheromones = GetPheromones();
        }
        
        private List<List<int>> solutionOfOneAnt(List<int> vertices, Dictionary<Tuple<int, int>, double> edges,
            int capacityLimit, Dictionary<int, int> demand, Dictionary<Tuple<int, int>, double> pheromones)
        {
            var solution = new List<List<int>>();

            while (vertices.Count != 0)
            {
                var path = new List<int>();
                int city = RandomGenerator.GetRandomElementFromList(vertices);
                int capacity = capacityLimit - demand[city];
                path.Add(city);
                vertices.Remove(city);
                
                while (vertices.Count != 0)
                {
                    var probabilities = new List<double>();
                    probabilities = vertices.Select(x =>
                        Pow(pheromones[Tuple.Create(Min(x, city), Max(x, city))], Alpha) *
                        (1 / Pow(edges[Tuple.Create(Min(x, city), Max(x, city))], Beta))).ToList();
                    
                    double sumOfProbabilities = probabilities.Sum();
                    for (int i = 0; i < probabilities.Count; i++)
                    {
                        probabilities[i] = probabilities[i] / sumOfProbabilities;
                    }

                    int secondCity = RandomGenerator.GetRandom(vertices, probabilities);
                    capacity = capacity - demand[secondCity];
                    if (capacity > 0)
                    {
                        path.Add(city);
                        vertices.Remove(city);
                    }
                    else
                    {
                        break;
                    }
                }
                solution.Add(path);
            }

            return solution;
        }

        private double RateSolution(List<List<int>> solution, Dictionary<Tuple<int, int>, double> edges)
        {
            double s = 0;
            foreach (var i in solution)
            {
                int a = 1;
                foreach (var j in i)
                {
                    int b = j;
                    if (a == b)
                    {
                        continue;
                    }
                    s = s + edges[Tuple.Create(Min(a, b), Max(a, b))];
                    a = b;
                }

                int c = 1;
                if (a == c)
                {
                    continue;
                }
                s = s + edges[Tuple.Create(Min(a, c), Max(a, c))];
            }

            return s;
        }
        //PROBABLY WE SHOULD USE GLOBAL PEHEROMEOS
        private Tuple<List<List<int>>, double> updatePheromone(/*Dictionary<Tuple<int, int>, double> pheromones, */List<Tuple<List<List<int>>, double>> solutions, ref Tuple<List<List<int>>, double> bestSolution)
        {
            double lavg = 0;
            foreach (var solutionAndRate in solutions)
            {
                lavg = solutionAndRate.Item2;
            }

            lavg = lavg / solutions.Count;
            
            Dictionary<Tuple<int, int>, double> tempPheromones = new Dictionary<Tuple<int, int>, double>();
            foreach (var pheromone in pheromones)
            {
                tempPheromones.Add(pheromone.Key, (ro + th / lavg) * pheromone.Value);
            }

            pheromones = tempPheromones;
            solutions = solutions.OrderBy(x => x.Item2).ToList();

            if (bestSolution != null)
            {
                if (solutions[0].Item2 < bestSolution.Item2)
                {
                    bestSolution = solutions[0];
                }
                foreach (var path in bestSolution.Item1)
                {
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        pheromones[Tuple.Create(Min(path[i], path[i + 1]), Max(path[i], path[i + 1]))] =
                            Sigma / bestSolution.Item2
                            + pheromones[Tuple.Create(Min(path[i], path[i + 1]), Max(path[i], path[i + 1]))];
                    }
                }
            }
            else
            {
                bestSolution = solutions[0];
                for (int l = 0; l < Sigma; l++)
                {
                    var paths = solutions[l].Item1;
                    var L = solutions[l].Item2;
                    foreach (var path in paths)
                    {
                        for (int i = 0; i < path.Count - 1; i++)
                        {
                            pheromones[Tuple.Create(Min(path[i], path[i + 1]), Max(path[i], path[i + 1]))] = Sigma -
                                (l + 1) / Pow(L, l + 1) +
                                pheromones[Tuple.Create(Min(path[i], path[i + 1]), Max(path[i], path[i + 1]))];
                        }
                    }
                }
            }

            return bestSolution;
        }
        
        public TimeSpan Solve()
        {
            Tuple<List<List<int>>, double> bestSolution = null;
            for (int i = 0; i < iterations; i++)
            {
                var solutions = new List<Tuple<List<List<int>>, double>>();
                for (int j = 0; j < ants; j++)
                {
                    var solution = solutionOfOneAnt(GetVertices(), GetEdges(), problem.Capacity, GetDemand(),
                        pheromones);
                    solutions.Add(Tuple.Create(solution, RateSolution(solution, GetEdges())));
                }

                int a = 0;
                bestSolution = updatePheromone(solutions, ref bestSolution);
                Console.WriteLine("BEST SOLUTION " + bestSolution.Item2);
            }

            Console.WriteLine("BEST SOLUTION " + bestSolution.Item2);
            return new TimeSpan();
        }

        
        private List<int> GetVertices()
        {
            List<int> vertices = new List<int>();
            int i = 1;
            foreach (var vertice in problem.Graph.Vertices)
            {
                vertices.Add(vertice.Number);
                i++;
            }

            return vertices;
        }


        private Dictionary<Tuple<int, int>, double> GetEdges()
        {
            Dictionary<Tuple<int, int>, double> edges = new Dictionary<Tuple<int, int>, double>();
            for (int i = 0; i < problem.NodesCount; i++)
            {
                for (int j = i; j < problem.NodesCount; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }
                    
                    edges.Add(Tuple.Create(Min(problem.Graph.Vertices[i].Number, problem.Graph.Vertices[j].Number), Max(problem.Graph.Vertices[i].Number, problem.Graph.Vertices[j].Number)), 
                        Vertice.GetDistance(problem.Graph.Vertices[i], problem.Graph.Vertices[j]));
                }
            }

            return edges;
        }

        private Dictionary<int, int> GetDemand()
        {
            var demand = new Dictionary<int, int>();
            foreach (var vertice in problem.Graph.Vertices)
            {
                demand.Add(vertice.Number, vertice.Demand);
            }

            return demand;
        }

        private Dictionary<Tuple<int, int>, double> GetPheromones()
        {
            Dictionary<Tuple<int, int>, double> pheromones = new Dictionary<Tuple<int, int>, double>();
            for (int i = 0; i < problem.NodesCount; i++)
            {
                for (int j = i; j < problem.NodesCount; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }
                    
                    pheromones.Add(Tuple.Create(Min(problem.Graph.Vertices[i].Number, problem.Graph.Vertices[j].Number), Max(problem.Graph.Vertices[i].Number, problem.Graph.Vertices[j].Number)), 
                        1);
                }
            }

            return pheromones;
        }

        
        


        public Solution GetSolution()
        {
            throw new NotImplementedException();
        }
    }


    
}