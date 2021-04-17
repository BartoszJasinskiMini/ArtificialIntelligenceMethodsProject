using System;

namespace ArtificialIntelligenceMethodsProject.Models
{
    public class Edge2
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public double Length { get; set; }
        public double Pheromone { get; set; }
        public double Weight { get; set; }

        public Edge2() { }

        public Edge2(Point start, Point end)
        {
            Start = start;
            End = end;
            Length = Math.Round(Start.DistanceTo(End));
        }
    }
}