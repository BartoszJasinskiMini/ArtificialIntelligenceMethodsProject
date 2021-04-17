using System;
using System.Collections.Generic;
using ArtificialIntelligenceMethodsProject.Misc;

namespace ArtificialIntelligenceMethodsProject.Models
{
    public class Graph2
    {
          public List<Point> Points { get; set; }
        public Dictionary<int, Edge2> Edges { get; set; }
        public int Dimensions { get; set; }
        public double MinimumPheromone { get; set; }
        private bool IsSymetric { get; set; }

        public Graph2(List<Point> Points, bool isSymetric)
        {
            Edges = new Dictionary<int, Edge2>();
            this.Points = Points;
            Dimensions = Points.Count;
            IsSymetric = isSymetric;
            CreateEdges();
        }

        /// <summary>
        /// Create edges between all points. 
        /// NOTE: For every two points there is two edges between them in case of asymetric problem (1 -> 2, 2 -> 1).
        /// </summary>
        private void CreateEdges()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                for (int j = 0; j < Points.Count; j++)
                {
                    if (i != j)
                    {
                        Edge2 edge2 = new Edge2(Points[i], Points[j]);
                        Edges.Add(Helper.HashFunction(Points[i].Id, Points[j].Id), edge2);
                    }
                }
            }
        }

        /// <summary>
        /// Return edge beetwen two points (their ID's) from Dictionary
        /// </summary>
        public Edge2 GetEdge(int firstPointId, int secondPointId)
        {
            return Edges[Helper.HashFunction(firstPointId, secondPointId)];
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

        public void EvaporatePheromone(Edge2 edge2, double value)
        {
            edge2.Pheromone = Math.Max(MinimumPheromone, edge2.Pheromone * value); // Math.Max is here to prevent Pheromon = 0

            if (IsSymetric)
            {
                var secondEdge = GetEdge(edge2.End.Id, edge2.Start.Id);
                secondEdge.Pheromone = Math.Max(MinimumPheromone, secondEdge.Pheromone * value);
            }
        }

        public void DepositPheromone(Edge2 edge2, double value)
        {
            edge2.Pheromone += value;

            if (IsSymetric)
            {
                var secondEdge = GetEdge(edge2.End.Id, edge2.Start.Id);
                secondEdge.Pheromone += value;
            }
        }
    }
}