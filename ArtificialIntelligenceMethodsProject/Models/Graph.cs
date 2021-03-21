using System.Collections.Generic;

namespace ArtificialIntelligenceMethodsProject.Models
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
