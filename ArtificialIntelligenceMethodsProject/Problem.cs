using System;
using System.Collections.Generic;
using System.Text;

namespace ArtificialIntelligenceMethodsProject
{
    class Problem
    {
        public string Name { get; private set; }
        public string Comment { get; private set; }
        public string Type { get; private set; }
        public int NeighboursCount { get; private set; }
        public int Capacity { get; private set; }
        public Graph Graph { get; private set; }
        public Problem(string name, string comment, string type, int neighboursCount, int capacity, Graph graph)
        {
            Name = name;
            Comment = comment;
            Type = type;
            NeighboursCount = neighboursCount;
            Capacity = capacity;
            Graph = graph;
        }
        public override string ToString()
        {
            return Name + " " + Comment;
        }
    }
}
