using System;
using System.Collections.Generic;
using ArtificialIntelligenceMethodsProject.Models;
using System.Text;
using ArtificialIntelligenceMethodsProject.Misc;

namespace ArtificialIntelligenceMethodsProject.Algorithms.MinMaxAntSystem
{
    class Ant
    {
        double alpha = 0.5;
        double beta = 0.5;

        private Problem problem;
        private Edges edges;
        private double cost;
        private List<int[]> routes;

        public Ant(Problem problem, Edges edges)
        {
            this.problem = problem;
            this.edges = edges;
            routes = null;
            cost = double.MaxValue;
        }
        public void FindRoute()
        {
            List<Vertice> notVisited = new List<Vertice>();
            for (int i = 1; i < problem.Dimensions; i++)
            {
                notVisited.Add(problem.Graph.Vertices[i]);
            }
            
            while (notVisited.Count > 0)
            {
                Vertice currentNode = problem.Graph.Vertices[problem.Graph.DepotIndex];
                List<int> route = new List<int>();
                int capacity = problem.Capacity;
                while (capacity > 0 && notVisited.Count > 0)
                {
                    int nextNodeIndex = GetNextVertice(notVisited, currentNode, capacity);
                    if (nextNodeIndex == -1)
                    {
                        break;
                    }
                    cost += Vertice.GetDistance(currentNode, notVisited[nextNodeIndex]);
                    capacity -= notVisited[nextNodeIndex].Demand;
                    route.Add(notVisited[nextNodeIndex].Id);
                    currentNode = notVisited[nextNodeIndex];
                    notVisited.RemoveAt(nextNodeIndex);
                }
                cost += Vertice.GetDistance(currentNode, problem.Graph.Vertices[problem.Graph.DepotIndex]);
                routes.Add(route.ToArray());
            }
        }
        private int GetNextVertice(List<Vertice> notVisited, Vertice currentNode, int capacity)
        {
            List<(int index, double value)> probability = new List<(int index, double value)>();
            double sum = 0.0;
            for(int i = 0; i < notVisited.Count; i++)
            {
                if (notVisited[i].Demand < capacity)
                {
                    double val = edges.GetEdge(currentNode, notVisited[i]).GetPheromone() / Vertice.GetDistance(currentNode, notVisited[i]);
                    probability.Add((i, val));
                    sum += val;
                }
            }
            double random = RandomGenerator.GetDoubleRangeRandomNumber(0.0, 1.0);
            double prob = 0.0;
            for (int i = 0; i < probability.Count; i++)
            {
                prob += probability[i].value / sum;
                if (random < prob)
                    return probability[i].index;
            }
            return -1;

        }

        public void UpdatePheromone()
        {

        }
    }
}
