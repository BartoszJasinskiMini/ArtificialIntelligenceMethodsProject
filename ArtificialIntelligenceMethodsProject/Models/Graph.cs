using System.Collections.Generic;
using ArtificialIntelligenceMethodsProject.Misc;

using static System.Math;

namespace ArtificialIntelligenceMethodsProject.Models
{
    public class Graph
    {
        public List<Vertice> Vertices { get; }
        public Dictionary<int, Edge> Edges { get; set; }
        public double MinimumPheromone { get; set; }
        private bool IsSymmetric { get; set; }
        public int DepotIndex { get; }
        public Graph(int depotIndex, bool isSymmetric = true)
        {
            DepotIndex = depotIndex - 1;
            Vertices = new List<Vertice>();
            IsSymmetric = isSymmetric;
            Edges = new Dictionary<int, Edge>();
            // CreateEdges();
        }
        public void AddVertice(Vertice vertice)
        {
            Vertices.Add(vertice);
        }

        public Vertice GetVertice(int numberOfVertice)
        {
            return Vertices.Find(v => v.Id == numberOfVertice);
        }
        
        /// <summary>
        /// Create edges between all points. 
        /// NOTE: For every two points there is two edges between them in case of asymetric problem (1 -> 2, 2 -> 1).
        /// </summary>
        public void CreateEdges()
        {
            for (int i = 0; i < Vertices.Count; i++)
            {
                for (int j = 0; j < Vertices.Count; j++)
                {
                    if (i != j)
                    {
                        Edge edge = new Edge(Vertices[i], Vertices[j]);
                        Edges.Add(Helper.HashFunction(Vertices[i].Id, Vertices[j].Id), edge);
                    }
                }
            }
        }
        
        /// <summary>
        /// Return edge beetwen two points (their ID's) from Dictionary
        /// </summary>
        public Edge GetEdge(int firstPointId, int secondPointId)
        {
            return Edges.GetValueOrDefault(Helper.HashFunction(firstPointId, secondPointId));
        }

        /// <summary>
        /// Set specific pheromone to all edges
        /// </summary>
        public void ResetPheromone(double pheromoneValue)
        {
            foreach (var edge in Edges)
            {
                edge.Value.Pheromone = pheromoneValue;
            }
        }

        public void EvaporatePheromone(Edge edge, double value)
        {
            edge.Pheromone = Max(MinimumPheromone, edge.Pheromone * value); // Math.Max is here to prevent Pheromon = 0

            if (IsSymmetric)
            {
                var secondEdge = GetEdge(edge.End.Id, edge.Start.Id);
                secondEdge.Pheromone = Max(MinimumPheromone, secondEdge.Pheromone * value);
            }
        }

        public void DepositPheromone(Edge edge, double value)
        {
            edge.Pheromone += value;

            if (IsSymmetric)
            {
                var secondEdge = GetEdge(edge.End.Id, edge.Start.Id);
                secondEdge.Pheromone += value;
            }
        }
    }
}
