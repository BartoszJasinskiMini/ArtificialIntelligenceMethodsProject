using System;


using static System.Math;
namespace ArtificialIntelligenceMethodsProject.Models
{
    public class Vertice
    {
        public int Id { get; }
        public int X { get; }
        public int Y { get; }
        public int Demand { get; }
        public Vertice(int id, int x, int y, int demand)
        {
            Id = id;
            X = x;
            Y = y;
            Demand = demand;
        }

        public static double GetDistance(Vertice v, Vertice u)
        {
            return Sqrt(Pow(v.X - u.X, 2) + Pow(v.Y - u.Y, 2));
        }
        
        public double GetDistance(Vertice u)
        {
            return Sqrt(Pow(X - u.X, 2) + Pow(Y - u.Y, 2));
        }
    }
}
