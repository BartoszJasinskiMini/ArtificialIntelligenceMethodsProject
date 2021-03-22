using ArtificialIntelligenceMethodsProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ArtificialIntelligenceMethodsProject.Algorithms
{
    class GreedyAlgorithm : IAlgorithm
    {
        private Problem problem;
        private Solution solution;
        private List<int[]> routes;
        public GreedyAlgorithm()
        {

        }

        public Solution GetSolution()
        {
            if (routes != null)
                return new Solution(0, routes);
            return null;
        }

        public void LoadProblemInstance(Problem problem)
        {
            this.problem = problem;
        }

        public TimeSpan Solve()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            routes = new List<int[]>();
            List<Vertice> notVisited = new List<Vertice>();
            for (int i = 1; i < problem.NodesCount; i++)
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
                    int closestNodeIndex = FindClosestNodeIndex(notVisited, currentNode, capacity);
                    if (closestNodeIndex == -1)
                        break;
                    capacity -= notVisited[closestNodeIndex].Demand;
                    if (capacity >= 0)
                    {
                        route.Add(notVisited[closestNodeIndex].Number);
                        currentNode = notVisited[closestNodeIndex];
                        notVisited.RemoveAt(closestNodeIndex);
                    }
                }
                routes.Add(route.ToArray());
            }
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        private int FindClosestNodeIndex(List<Vertice> node, Vertice from, int capacityLeft)
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
            return closestIndex;
        }


    }
}
