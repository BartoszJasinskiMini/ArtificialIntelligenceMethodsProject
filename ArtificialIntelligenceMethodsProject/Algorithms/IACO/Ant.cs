using System;
using System.Collections.Generic;
using System.Linq;
using ArtificialIntelligenceMethodsProject.Misc;
using ArtificialIntelligenceMethodsProject.Models;

using static System.Math;

namespace ArtificialIntelligenceMethodsProject.Algorithms.IACO
{
    class Ant
    {
        public static IACOParameters parameters { private get; set; }
        public static Problem problem { private get; set; }
        public static Edges edges { private get; set; }

        public double Cost { get; private set; }
        public List<int[]> Routes { get; private set; }
        private int startingNode;
        private List<int> pheromoneTrait;

        public Ant(int startingNode)
        {
            Routes = null;
            this.startingNode = startingNode;
            pheromoneTrait = new List<int>();
        }
        public void FindRoute(int iterationNumber)
        {
            Routes = new List<int[]>(); 
            List<Vertice> notVisited = new List<Vertice>();
            for (int i = 1; i < problem.Dimensions; i++)
            {
                notVisited.Add(problem.Graph.Vertices[i]);
            }

            pheromoneTrait.Add(problem.Graph.DepotIndex);

            List<int> route = new List<int>();
            Vertice currentNode = problem.Graph.Vertices[startingNode];
            pheromoneTrait.Add(startingNode);
    
            route.Add(startingNode);
            int capacity = problem.Capacity - problem.Graph.Vertices[startingNode].Demand;
            Cost = Vertice.GetDistance(problem.Graph.Vertices[startingNode], problem.Graph.Vertices[problem.Graph.DepotIndex]);
            notVisited.RemoveAt(startingNode - 1);
            while (notVisited.Count > 0)
            {          
                while (capacity > 0 && notVisited.Count > 0)
                {
                    int nextNodeIndex = GetNextVertice(notVisited, currentNode, capacity);
                    if (nextNodeIndex == -1)
                    {
                        break;
                    }
                    Cost += Vertice.GetDistance(currentNode, notVisited[nextNodeIndex]);
                    capacity -= notVisited[nextNodeIndex].Demand;
                    route.Add(notVisited[nextNodeIndex].Id);
                    pheromoneTrait.Add(notVisited[nextNodeIndex].Id);
                    currentNode = notVisited[nextNodeIndex];
                    notVisited.RemoveAt(nextNodeIndex);
                }
                Cost += Vertice.GetDistance(currentNode, problem.Graph.Vertices[problem.Graph.DepotIndex]);
                Routes.Add(route.ToArray());
                pheromoneTrait.Add(problem.Graph.DepotIndex);
                currentNode = problem.Graph.Vertices[problem.Graph.DepotIndex];
                route = new List<int>();
                capacity = problem.Capacity;
            }
            
            Mutate(iterationNumber);

            if (show)
            {
                Console.WriteLine("%%%%%%%%%%%%%");

                foreach (var route2 in Routes)
                {
                    foreach (var customer in route2)
                    {
                        Console.Write(customer + " ");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    
                }
                Console.WriteLine("%%%%%%%%%%%%%");
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");

            }
        }

        private bool show = false;
        private void Mutate(int iterationNumber)
        {
            
            var routesToMutate = new List<int[]>();
            for(int i = 0; i < Routes.Count; i++)
            {
                var probabilityOfMutation = parameters.pMin + Pow((parameters.pMax - parameters.pMin),
                    1 - (iterationNumber / parameters.MaxIterations));
                if (RandomGenerator.GetTrueWithSpecifiedProbability(probabilityOfMutation))
                {
                    routesToMutate.Add(Routes[i]);
                    Routes.RemoveAt(i);
                }
            }

            if (routesToMutate.Count % 2 == 1)
            {
                routesToMutate.RemoveAt(RandomGenerator.GetRandomIntFromRange(0, routesToMutate.Count));
            }

            for (var i = 0; i < routesToMutate.Count / 2; i++)
            {
                // int[] tempFirstRouteToMutate = new int[routesToMutate[2 * i].Length];
                // routesToMutate[2 * i].CopyTo(tempFirstRouteToMutate, 0);
                // int[] tempSecondRouteToMutate = new int[routesToMutate[2 * i].Length];
                // routesToMutate[2 * i + 1].CopyTo(tempSecondRouteToMutate, 0);
                
                var firstCustomerIndexToMutate = RandomGenerator.GetRandomIntFromRange(0, routesToMutate[2 * i].Length);
                var secondCustomerIndexToMutate = RandomGenerator.GetRandomIntFromRange(0, routesToMutate[2 * i + 1].Length);

                if (ShouldMutate(firstCustomerIndexToMutate, routesToMutate[2 * i], secondCustomerIndexToMutate,
                    routesToMutate[2 * i + 1]))
                {
                    var tempFirstCustomerToMutate = routesToMutate[2 * i][firstCustomerIndexToMutate];
                    foreach (var customer in routesToMutate[2 * i])
                    {
                        Console.Write(customer + " ");
                    }
                    Console.WriteLine();

                    Console.WriteLine(tempFirstCustomerToMutate);
                    Console.WriteLine("!!!!!");
                    Console.WriteLine(tempFirstCustomerToMutate);
                    
                    foreach (var customer in routesToMutate[2 * i + 1])
                    {
                        Console.Write(customer + " ");
                    }
                    Console.WriteLine("???????");
                    
                    routesToMutate[2 * i][firstCustomerIndexToMutate] =
                        routesToMutate[2 * i + 1][secondCustomerIndexToMutate];
                    routesToMutate[2 * i + 1][secondCustomerIndexToMutate] = tempFirstCustomerToMutate;
                    
                    foreach (var customer in routesToMutate[2 * i])
                    {
                        Console.Write(customer + " ");
                    }

                    Console.WriteLine("#####");

                    foreach (var customer in routesToMutate[2 * i + 1])
                    {
                        Console.Write(customer + " ");
                    }
                    
                    // Console.WriteLine("MUTATEED");
                    var firstRoutes = routesToMutate[2 * i];
                    var secondRoutes = routesToMutate[2 * i + 1];
                    TwoOpt(ref firstRoutes);
                    TwoOpt(ref secondRoutes);
                }

            }
            
            Routes.AddRange(routesToMutate);
            
            
        }

        private bool ShouldMutate(int firstCustomerIndexToMutate, int[] firstRouteToMutate,
            int secondCustomerIndexToMutate, int[] secondRouteToMutate)
        {
            var firstRouteCost = GetRouteCost(firstRouteToMutate);
            var secondRouteCost = GetRouteCost(secondRouteToMutate);
            double firstRouteCostAfterMutation = 0.0, secondRouteCostAfterMutation = 0.0;
            
            for (var i = 0; i < firstRouteToMutate.Length; i++)
            {
                var routeDemand = 0;
                // if (i == firstCustomerIndexToMutate - 1)
                // {
                //     firstRouteCostAfterMutation += Vertice.GetDistance(problem.Graph.Vertices[secondRouteToMutate[secondCustomerIndexToMutate]],
                //         problem.Graph.Vertices[firstRouteToMutate[i - 1]]);
                // }
                /*else*/ if (i == firstCustomerIndexToMutate)
                {
                    routeDemand += problem.Graph.Vertices[secondRouteToMutate[secondCustomerIndexToMutate]].Demand;
                    // firstRouteCostAfterMutation += Vertice.GetDistance(problem.Graph.Vertices[secondRouteToMutate[secondCustomerIndexToMutate]],
                    //     problem.Graph.Vertices[firstRouteToMutate[(i + 1) % firstRouteToMutate.Length]]);
                    if (routeDemand > problem.Capacity)
                    {
                        return false;
                    }
                    continue;
                }
                    
                routeDemand += problem.Graph.Vertices[firstRouteToMutate[i]].Demand;
                if (routeDemand > problem.Capacity)
                {
                    return false;
                }
            }

            for (var i = 0; i < secondRouteToMutate.Length; i++)
            {
                var routeDemand = 0;
                // if (i == secondCustomerIndexToMutate - 1)
                // {
                //     if(secondCustomerIndexToMutate - 1)
                //     secondRouteCostAfterMutation += Vertice.GetDistance(problem.Graph.Vertices[firstRouteToMutate[firstCustomerIndexToMutate]],
                //         problem.Graph.Vertices[secondRouteToMutate[i - 1]]);
                // }
                /*else*/ if (i == secondCustomerIndexToMutate)
                {
                    routeDemand += problem.Graph.Vertices[firstRouteToMutate[firstCustomerIndexToMutate]].Demand;
                    // secondRouteCostAfterMutation += Vertice.GetDistance(problem.Graph.Vertices[firstRouteToMutate[firstCustomerIndexToMutate]],
                        // problem.Graph.Vertices[secondRouteToMutate[(i + 1) % secondRouteToMutate.Length]]);
                    if (routeDemand > problem.Capacity)
                    {
                        return false;
                    }
                    continue;
                }
                routeDemand += problem.Graph.Vertices[secondRouteToMutate[i]].Demand;
                if (routeDemand > problem.Capacity)
                {
                    return false;
                }
            }

            return
                true; //firstRouteCostAfterMutation + secondRouteCostAfterMutation > firstRouteCost + secondRouteCost;
        }

        private void TwoOpt(ref int[] route)
        {
            var bestCost = GetRouteCost(route);
            for (var i = 1; i < route.Length - 1; i++)
            {
                for (var k = i + 1; k < route.Length; k++)
                {
                    var newRoute = TwoOptSwap(ref route, i, k);
                    var newRouteCost = GetRouteCost(newRoute);
                    if (bestCost > GetRouteCost(newRoute))
                    {
                        show = true;
                        Console.WriteLine("TWO OPT IMPROVED!!!");
                        foreach (var customer in route)
                        {
                            Console.Write(customer + " ");
                        }
                        route = newRoute;
                        bestCost = newRouteCost;
                        Console.WriteLine("@@@@@@@");
                        foreach (var customer in route)
                        {
                            Console.Write(customer + " ");
                        }
                    }
                }
            }
        }

        private int[] TwoOptSwap(ref int[] route, int i, int k)
        {
            var newRoute = new int[route.Length];

            for (var j = 0; j < i; j++)
            {
                newRoute[j] = route[j];
            }
            
            for (int j = i, l = k - 1; j < k; j++, l--)
            {
                newRoute[j] = route[l];
            }
            
            for (var j = k; j < route.Length; j++)
            {
                newRoute[j] = route[j];
            }

            return newRoute;
        }
        
        
        private double GetRouteDemand(int[] route)
        {
            double routeDemand = 0;
            foreach (var customer in route)
            {
                routeDemand += problem.Graph.Vertices[customer].Demand;
            }

            return routeDemand;
        }

        private double GetRouteCost(int[] route)
        {
            double routeCost = 0.0;

            for (var i = 0; i < route.Length - 1; i++)
            {
                routeCost += Vertice.GetDistance(problem.Graph.Vertices[route[i]],
                    problem.Graph.Vertices[route[i + 1]]);
            }

            return routeCost;
        }
        
        private int GetNextVertice(List<Vertice> notVisited, Vertice currentNode, int capacity)
        {
            List<(int index, double value)> probability = new List<(int index, double value)>();
            double sum = 0.0;
            for(int i = 0; i < notVisited.Count; i++)
            {
                if (notVisited[i].Demand < capacity)
                {
                    double val = Pow(edges.GetEdge(currentNode.Id, notVisited[i].Id).GetPheromone(), parameters.Alpha) / Math.Pow(Vertice.GetDistance(currentNode, notVisited[i]), parameters.Beta);
                    probability.Add((i, val));
                    sum += val;
                }
            }
            double random = RandomGenerator.GetDoubleRangeRandomNumber(0.0, 1.0);
            double prob = 0.0;
            for (int i = 0; i < probability.Count; i++)
            {
                prob += probability[i].value / sum;
                if (random < prob)
                    return probability[i].index;
            }
            return -1;

        }
        public void EvaporatePheromone(double percentile)
        {
            edges.EvaporatePheromone(percentile);
        }
        public void UpdatePheromone()
        {
            for(int i = 0; i < pheromoneTrait.Count - 1; i++)
            {
                edges.GetEdge(pheromoneTrait[i], pheromoneTrait[i + 1]).AddPheromone(1 / Cost);
            }
        }
    }
}
