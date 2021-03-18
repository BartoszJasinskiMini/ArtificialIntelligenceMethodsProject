using System;
using System.Collections.Generic;
using System.Text;

namespace ArtificialIntelligenceMethodsProject
{
    class Graph
    {
        public List<Vertice> SupplyPoints { get; private set; }
        public Vertice Depot { get; private set; }
        public Graph()
        {
            SupplyPoints = new List<Vertice>();
        }
        public void AddSupplyPoint(Vertice vertice)
        {
            SupplyPoints.Add(vertice);
        }
    }
}
