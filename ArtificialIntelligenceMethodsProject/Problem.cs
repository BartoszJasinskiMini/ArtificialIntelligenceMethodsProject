using System;
using System.Collections.Generic;
using System.Text;

namespace ArtificialIntelligenceMethodsProject
{
    class Problem
    {
        public string Name { get; }
        public string Comment { get; }
        public string Type { get;  }
        public int NodesCount { get;  }
        public int Capacity { get; }
        public Graph Graph { get; }
        public Solution Solution { get; }
        public Problem(string name, string comment, string type, int neighboursCount, int capacity, Graph graph, Solution solution)
        {
            Name = name;
            Comment = comment;
            Type = type;
            NodesCount = neighboursCount;
            Capacity = capacity;
            Graph = graph;
            Solution = solution;
        }
        public override string ToString()
        {
            return Name + " " + Comment;
        }
    }
}
