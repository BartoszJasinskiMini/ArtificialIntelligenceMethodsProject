using ArtificialIntelligenceMethodsProject.Misc;
using ArtificialIntelligenceMethodsProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtificialIntelligenceMethodsProject.Algorithms.IACO
{
    class Edges
    {
        private Dictionary<int, IACOEdge> edges;
        public Edges()
        {
            edges = new Dictionary<int, IACOEdge>();
        }
        public void AddEdge(int u, int v, IACOEdge edge)
        {
            if(u > v)
            {
                int tmp = v;
                v = u;
                u = tmp;
            }
            int hashCode = Helper.HashFunction(u, v);
            if(edges.ContainsKey(hashCode) == false)
            {
                edges.Add(hashCode, edge);
            }
        }

        public IACOEdge GetEdge(int u, int v)
        {
            if (u > v)
            {
                int tmp = v;
                v = u;
                u = tmp;
            }
            int hashCode = Helper.HashFunction(u, v);
            return edges.GetValueOrDefault(hashCode);
        }

        public void EvaporatePheromone(double percentile)
        {
            foreach(var element in edges)
            {
                element.Value.EvaporatePheromone(percentile);
            }
        }
    }
}
