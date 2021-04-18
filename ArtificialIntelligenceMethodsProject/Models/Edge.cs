namespace ArtificialIntelligenceMethodsProject.Models
{
    public class Edge
    {
        public Vertice Start { get; set; }
        public Vertice End { get; set; }
        public double Length { get; set; }
        public double Pheromone { get; set; }
        public double Weight { get; set; }
        public Edge() { }

        public Edge(Vertice start, Vertice end)
        {
            Start = start;
            End = end;
            Length = start.GetDistance(end);
        }
    }
}