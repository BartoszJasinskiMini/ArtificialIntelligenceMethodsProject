using System;
using System.Collections.Generic;
using System.Text;

namespace ArtificialIntelligenceMethodsProject
{
    class Graph
    {
        public List<Vertice> Vertices { get; }
        public int DepotIndex { get; }
        public Graph(int depotIndex)
        {
            DepotIndex = depotIndex;
            Vertices = new List<Vertice>();
        }
        public void AddVertice(Vertice vertice)
        {
            Vertices.Add(vertice);
        }
    }
}
