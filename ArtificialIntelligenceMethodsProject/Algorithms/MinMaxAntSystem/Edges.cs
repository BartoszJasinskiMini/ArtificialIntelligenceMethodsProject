using ArtificialIntelligenceMethodsProject.Misc;
using ArtificialIntelligenceMethodsProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtificialIntelligenceMethodsProject.Algorithms.MinMaxAntSystem
{
    class Edges
    {
        private Dictionary<int, MinMaxEdge> edges;

        public void AddEdge(Vertice u, Vertice v, MinMaxEdge edge)
        {
            if(u.Id > v.Id)
            {
                Vertice tmp = v;
                v = u;
                u = tmp;
            }
            int hashCode = Helper.HashFunction(u.Id, v.Id);
            if(edges.ContainsKey(hashCode) == false)
            {
                edges.Add(hashCode, edge);
            }
        }

        public MinMaxEdge GetEdge(Vertice v, Vertice u)
        {
            if (u.Id > v.Id)
            {
                Vertice tmp = v;
                v = u;
                u = tmp;
            }
            int hashCode = Helper.HashFunction(u.Id, v.Id);
            return edges.GetValueOrDefault(hashCode);
        }
    }
}
