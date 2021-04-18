using System.Collections.Generic;
using System.Linq;
using ArtificialIntelligenceMethodsProject.Misc;
using ArtificialIntelligenceMethodsProject.Models;

namespace ArtificialIntelligenceMethodsProject.Algorithms
{
    public class AntSystem
    {
                public Graph Graph { get; set; }
                public double Alpha { get; set; }
                public int Beta { get; set; }
                public double Rho { get; set; }
                public double Omega { get; set; }
                public double Q0 { get; set; }
                public int StartNodeId { get; set; }
                public double Distance { get; set; }
                public List<Vertice> VisitedNodes { get; set; }
                public List<Vertice> UnvisitedNodes { get; set; }
                public List<Edge> Path { get; set; }

                
                public AntSystem(Graph graph, int beta, double q0)
                {
                    Graph = graph;
                    Beta = beta;
                    Q0 = q0;
                    VisitedNodes = new List<Vertice>();
                    UnvisitedNodes = new List<Vertice>();
                    Path = new List<Edge>();
                }
                
                public void Init(int startNodeId)
                {
                    StartNodeId = startNodeId;
                    Distance = 0;
                    VisitedNodes.Add(Graph.Vertices.Where(x => x.Id == startNodeId).First());
                    UnvisitedNodes = Graph.Vertices.Where(x => x.Id != startNodeId).ToList();
                    Path.Clear();
                }
                
                public Vertice CurrentNode()
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
                    Vertice choosenPoint = Helper.GetRandomEdge(cumSum);
                
                    return choosenPoint;
                }
        
    }
}
