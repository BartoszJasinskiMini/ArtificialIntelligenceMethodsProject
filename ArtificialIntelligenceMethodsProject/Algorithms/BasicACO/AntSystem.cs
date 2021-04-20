using System.Collections.Generic;
using System.Linq;
using ArtificialIntelligenceMethodsProject.Misc;
using ArtificialIntelligenceMethodsProject.Models;

namespace ArtificialIntelligenceMethodsProject.Algorithms.BasicACO
{
    public class AntSystem
    {
        private Graph Graph { get; set; }
        private double Alpha { get; set; }

        private int Beta { get; set; }
                // public double Rho { get; set; }
                // public double Omega { get; set; }
                private double Ro { get; set; }
                private int Th { get; set; } //Probably Omega
                private double Q0 { get; set; }
                private int StartNodeId { get; set; }
                public double Distance { get; set; }
                private List<Vertice> VisitedNodes { get; set; }
                private List<Vertice> UnvisitedNodes { get; set; }
                public List<Edge> Path { get; private set; }
                
                private int Capacity { get; set; }
                
                public AntSystem(Graph graph, Parameters parameters, Problem problem)
                {
                    Graph = graph;
                    Alpha = parameters.Alpha;
                    Beta = parameters.Beta;
                    Q0 = parameters.Q0;
                    Ro = parameters.ro;
                    Th = parameters.th;
                    Capacity = problem.Capacity;
                    
                    VisitedNodes = new List<Vertice>();
                    UnvisitedNodes = new List<Vertice>();
                    Path = new List<Edge>();
                }
                
                public void Init(int startNodeId)
                {
                    StartNodeId = startNodeId;
                    Distance = 0;
                    VisitedNodes.Add(Graph.Vertices.First(x => x.Id == (startNodeId - 1)));
                    UnvisitedNodes = Graph.Vertices.Where(x => x.Id != startNodeId).ToList();
                    Path.Clear();
                }

                private Vertice CurrentNode()
                {
                    return VisitedNodes[VisitedNodes.Count - 1];
                }
                
                public bool CanMove()
                {
                    return VisitedNodes.Count != Path.Count;
                }
                
                public Edge Move()
                {
                    Vertice endPoint;
                    var startPoint = CurrentNode();
                    var currentCapacity = Capacity - startPoint.Demand;
                    if (UnvisitedNodes.Count == 0)
                    {
                        endPoint = VisitedNodes[0]; // if ant visited every node, just go back to start
                    }
                    else
                    {
                        endPoint = ChooseNextPoint();
                        VisitedNodes.Add(endPoint);
                        UnvisitedNodes.RemoveAt(UnvisitedNodes.FindIndex(x => x.Id == endPoint.Id));
                    }
                
                    var edge = Graph.GetEdge(startPoint.Id, endPoint.Id);
                    Path.Add(edge);
                    Distance += edge.Length;
                    
                    return edge;
                }
                
                private Vertice ChooseNextPoint()
                {
                    List<Edge> edgesWithWeight = new List<Edge>();
                    Edge bestEdge = new Edge();
                    int currentNodeId = CurrentNode().Id;
                
                    foreach (var node in UnvisitedNodes)
                    {
                        if (currentNodeId == node.Id)
                        {
                            continue;
                        }
                        var edge = Graph.GetEdge(currentNodeId, node.Id);
                        edge.Weight = Weight(edge);
                
                        if (edge.Weight > bestEdge.Weight)
                        {
                            bestEdge = edge;
                        }
                
                        edgesWithWeight.Add(edge);
                    }
                
                    var random = RandomGenerator.Instance.Random.NextDouble();
                    if (random < Q0)
                    {
                        return Exploitation(bestEdge);
                    }
                    else
                    {
                        return Exploration(edgesWithWeight);
                    }
                }
                
                private double Weight(Edge edge)
                {
                    double heuristic = 1 / edge.Length;
                    return edge.Pheromone * Helper.Pow(heuristic, Beta);
                }
                
                private Vertice Exploitation(Edge bestEdge)
                {
                    return bestEdge.End;
                }
                
                private Vertice Exploration(List<Edge> edgesWithWeight)
                {
                    double totalSum = edgesWithWeight.Sum(x => x.Weight);
                    var edgeProbabilities = edgesWithWeight.Select(w => { w.Weight = (w.Weight / totalSum); return w; }).ToList();
                    var cumSum = Helper.EdgeCumulativeSum(edgeProbabilities);
                    Vertice chosenPoint = Helper.GetRandomEdge(cumSum);
                
                    return chosenPoint;
                }
        
    }
}
