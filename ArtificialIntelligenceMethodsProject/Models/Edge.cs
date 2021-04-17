namespace ArtificialIntelligenceMethodsProject.Models
{
    public class Edge
    {
        public Vertice a { get; set; }
        public Vertice b { get; set; }
        public double Distance { get; set; }

        public Edge(Vertice a, Vertice b, double distance)
        {
            this.a = a;
            this.b = b;
            this.Distance = distance;
        }
    }
}