using System.Collections.Generic;

namespace ArtificialIntelligenceMethodsProject.Models
{
    public class Solution
    {
        public int MaxIterations { get; set; }
        public int ResultIteration { get; set; }
        public int ConvergentIteration { get; set; }
        public List<int[]> Routes { get; }
        public int Cost { get; }
        public Solution(int cost, List<int[]> routes)
        {
            Cost = cost;
            Routes = routes;
        }
        public int Calculator(Graph graph)
        {
            double result = 0;
            for(int i = 0; i < Routes.Count; i++)
            {
                int index = (Routes[i])[0];
                result += Vertice.GetDistance(graph.Vertices[graph.DepotIndex], graph.Vertices[index]);
                for(int j = 0; j < Routes[i].Length - 1; j++)
                {
                    result += Vertice.GetDistance(graph.Vertices[(Routes[i])[j]], graph.Vertices[(Routes[i])[j + 1]]);
                }
                result += Vertice.GetDistance(graph.Vertices[graph.DepotIndex], graph.Vertices[(Routes[i])[Routes[i].Length - 1]]);
            }
            return (int)result;
        }
    }
}
