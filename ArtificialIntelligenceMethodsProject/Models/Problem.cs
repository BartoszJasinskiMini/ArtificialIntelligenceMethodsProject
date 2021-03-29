namespace ArtificialIntelligenceMethodsProject.Models
{
    public class Problem
    {
        public string Name { get; }
        public string Comment { get; }
        public string Type { get;  }
        public int NodesCount { get;  }
        public int Capacity { get; }
        public int VehiclesCount { get; }
        public Graph Graph { get; }
        public Solution Solution { get; }
        public Problem(string name, string comment, string type, int nodesCount, int vehiclesCount , int capacity, Graph graph, Solution solution)
        {
            Name = name;
            Comment = comment;
            Type = type;
            NodesCount = nodesCount;
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
