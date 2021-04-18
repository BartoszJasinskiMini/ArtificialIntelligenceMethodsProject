using ArtificialIntelligenceMethodsProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ArtificialIntelligenceMethodsProject.Algorithms
{
    public class GreedyAlgorithm : IAlgorithm
    {
        private Problem problem;
        private double cost;
        private List<int[]> routes;
        public GreedyAlgorithm()
        {

        }
        public Solution GetSolution()
        {
            if (routes != null && problem != null && routes.Count <= problem.VehiclesCount)
                return new Solution((int) cost, routes);
            return null;
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
            routes = new List<int[]>();
            cost = 0;
            List<Vertice> notVisited = new List<Vertice>();
            for (int i = 1; i < problem.Dimensions; i++)
            {
                notVisited.Add(problem.Graph.Vertices[i]);
            }
            
            while (notVisited.Count > 0)
            {
                List<int> route = new List<int>();
                Vertice currentNode = problem.Graph.Vertices[problem.Graph.DepotIndex];
                int capacity = problem.Capacity;
                while (capacity > 0 && notVisited.Count > 0)
                {
                    (int closestNodeIndex, double closestNodeCost) = FindClosestNodeIndex(notVisited, currentNode, capacity);
                    if (closestNodeIndex == -1)
                    {
                        break;
                    }
                    
                    cost += closestNodeCost;
                    capacity -= notVisited[closestNodeIndex].Demand;
                    route.Add(notVisited[closestNodeIndex].Id);
                    currentNode = notVisited[closestNodeIndex];
                    notVisited.RemoveAt(closestNodeIndex);
                }
                cost += Vertice.GetDistance(currentNode, problem.Graph.Vertices[problem.Graph.DepotIndex]);
                routes.Add(route.ToArray());
            }
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
        private (int index, double cost) FindClosestNodeIndex(List<Vertice> node, Vertice from, int capacityLeft)
        {
            int closestIndex = -1;
            double closestValue = double.MaxValue;
            for (int i = 0; i < node.Count; i++)
            {
                if (node[i].Demand <= capacityLeft)
                {
                    double currentValue = Vertice.GetDistance(node[i], from);
                    if (currentValue < closestValue)
                    {
                        closestIndex = i;
                        closestValue = currentValue;
                    }
                }
            }
            return (closestIndex, closestValue);
        }
    }
}
