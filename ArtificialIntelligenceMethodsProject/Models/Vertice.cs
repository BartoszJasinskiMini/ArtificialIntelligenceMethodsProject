namespace ArtificialIntelligenceMethodsProject.Models
{
    class Vertice
    {
        public Vertice(int number, int x, int y, int demand)
        {
            Number = number;
            X = x;
            Y = y;
            Demand = demand;
        }
        public int Number { get; }
        public int X { get; }
        public int Y { get; }
        public int Demand { get; }
    }
}
