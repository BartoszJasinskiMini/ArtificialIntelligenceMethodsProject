using ArtificialIntelligenceMethodsProject.Misc;
using ArtificialIntelligenceMethodsProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtificialIntelligenceMethodsProject.Algorithms.AS
{
    class Edges
    {
        private Dictionary<int, ACOEdge> edges;
        public Edges()
        {
            edges = new Dictionary<int, ACOEdge>();
        }
        public void AddEdge(int u, int v, ACOEdge edge)
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

        public ACOEdge GetEdge(int u, int v)
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
