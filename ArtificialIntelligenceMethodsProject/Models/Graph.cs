using System.Collections.Generic;

namespace ArtificialIntelligenceMethodsProject.Models
{
    public class Graph
    {
        public List<Vertice> Vertices { get; }
        public List<Edge> Edges { get; }
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

        public void AddEdge(Edge edge)
        {
            Edges.Add(edge);
        }

        public Vertice GetVertice(int numberOfVertice)
        {
            return Vertices.Find(v => v.Number == numberOfVertice);
        }
    }
}
