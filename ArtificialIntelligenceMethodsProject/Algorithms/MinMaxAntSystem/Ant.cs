using System;
using System.Collections.Generic;
using ArtificialIntelligenceMethodsProject.Models;
using System.Text;
using ArtificialIntelligenceMethodsProject.Misc;

namespace ArtificialIntelligenceMethodsProject.Algorithms.MinMaxAntSystem
{
    class Ant
    {
        public static MinMaxParameters parameters { private get; set; }
        public static Problem problem { private get; set; }
        public static Edges edges { private get; set; }

        public double Cost { get; private set; }
        public List<int[]> Routes { get; private set; }
        private int startingNode;
        private List<int> pheromoneTrait;

        public Ant(int startingNode)
        {
            Routes = null;
            this.startingNode = startingNode;
            pheromoneTrait = new List<int>();
        }
        public void FindRoute()
        {
            Routes = new List<int[]>(); 
            List<Vertice> notVisited = new List<Vertice>();
            for (int i = 1; i < problem.Dimensions; i++)
            {
                notVisited.Add(problem.Graph.Vertices[i]);
            }

            pheromoneTrait.Add(problem.Graph.DepotIndex);

            List<int> route = new List<int>();
            Vertice currentNode = problem.Graph.Vertices[startingNode];
            pheromoneTrait.Add(startingNode);
    
            route.Add(startingNode);
            int capacity = problem.Capacity - problem.Graph.Vertices[startingNode].Demand;
            Cost = Vertice.GetDistance(problem.Graph.Vertices[startingNode], problem.Graph.Vertices[problem.Graph.DepotIndex]);
            notVisited.RemoveAt(startingNode - 1);
            while (notVisited.Count > 0)
            {          
                while (capacity > 0 && notVisited.Count > 0)
                {
                    int nextNodeIndex = GetNextVertice(notVisited, currentNode, capacity);
                    if (nextNodeIndex == -1)
                    {
                        break;
                    }
                    Cost += Vertice.GetDistance(currentNode, notVisited[nextNodeIndex]);
                    capacity -= notVisited[nextNodeIndex].Demand;
                    route.Add(notVisited[nextNodeIndex].Id);
                    pheromoneTrait.Add(notVisited[nextNodeIndex].Id);
                    currentNode = notVisited[nextNodeIndex];
                    notVisited.RemoveAt(nextNodeIndex);
                }
                Cost += Vertice.GetDistance(currentNode, problem.Graph.Vertices[problem.Graph.DepotIndex]);
                Routes.Add(route.ToArray());
                pheromoneTrait.Add(problem.Graph.DepotIndex);
                currentNode = problem.Graph.Vertices[problem.Graph.DepotIndex];
                route = new List<int>();
                capacity = problem.Capacity;
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
                    double val = Math.Pow(edges.GetEdge(currentNode.Id, notVisited[i].Id).GetPheromone(), parameters.Alpha) / Math.Pow(Vertice.GetDistance(currentNode, notVisited[i]), parameters.Beta);
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
        public void EvaporatePheromone(double percentile)
        {
            edges.EvaporatePheromone(percentile);
        }
        public void UpdatePheromone()
        {
            for(int i = 0; i < pheromoneTrait.Count - 1; i++)
            {
                edges.GetEdge(pheromoneTrait[i], pheromoneTrait[i + 1]).AddPheromone(1 / Cost);
            }
        }
    }
}
