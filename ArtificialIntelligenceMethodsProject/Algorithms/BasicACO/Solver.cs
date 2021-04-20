using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ArtificialIntelligenceMethodsProject.Misc;
using ArtificialIntelligenceMethodsProject.Models;
using static System.Math;

namespace ArtificialIntelligenceMethodsProject.Algorithms.BasicACO
{
    public class Solver: IAlgorithm
    {
        private Parameters Parameters { get; set; }
        private AntSystem GlobalBestAnt { get; set; }
        private List<double> Results { get; set; }
        private Graph Graph { get; set; }
        private Stopwatch Stopwatch { get; set; }
        private Problem Problem { get; set; }
        
        
        private double cost;
        private List<int[]> routes;
        
        public Solver(Parameters parameters, Graph graph, Problem problem)
        {
            Parameters = parameters;
            graph.MinimumPheromone = parameters.T0;
            Graph = graph;
            Problem = problem;
            
            Results = new List<double>();
            Stopwatch = new Stopwatch();
        }

        /// <summary>
        /// Main loop of ACS algorithm
        /// </summary>
        public List<double> RunAcs()
        {
            Stopwatch.Start();
            Graph.ResetPheromone(Parameters.T0);
            for (int i = 0; i < Parameters.Iterations; i++)
            {
                List<AntSystem> antColony = CreateAnts();
                GlobalBestAnt = GlobalBestAnt ?? antColony[0];

                AntSystem localBestAnt = BuildTours(antColony);
                if (Round(localBestAnt.Distance, 2) < Round(GlobalBestAnt.Distance, 2))
                {
                    GlobalBestAnt = localBestAnt;
                    Console.WriteLine("Current Global Best: " + GlobalBestAnt.Distance + " found in " + i + " iteration");
                }
                Results.Add(localBestAnt.Distance);
            }
            Stopwatch.Stop();
            return Results;
        }

        /// <summary>
        /// Create ants and place every ant in random point on graph (warning AntCount < Dimensions />)
        /// </summary>
        private List<AntSystem> CreateAnts()
        {
            List<AntSystem> antColony = new List<AntSystem>();
            List<int> randomPoints = RandomGenerator.GenerateRandom(Parameters.AntCount, 1, Graph.Vertices.Count);
            foreach (int random in randomPoints)
            {
                AntSystem ant = new AntSystem(Graph, Parameters, Problem);
                ant.Init(random);
                antColony.Add(ant);
            }
            return antColony;
        }

        /// <summary>
        /// This method builds solution for every ant in AntColony and return the best ant (with shortest distance tour)
        /// </summary>
        private AntSystem BuildTours(List<AntSystem> antColony)
        {
            // BUG I'm not sure about this loop maybe we have to delete it (just outer loop, foreach seems ok)
            for (int i = 0; i < Graph.Vertices.Count; i++)
            {
                foreach (AntSystem ant in antColony)
                {
                    Edge edge = ant.Move();
                    LocalUpdate(edge);
                }
            }

            GlobalUpdate();

            return antColony.OrderBy(x => x.Distance).FirstOrDefault(); // find shortest ant tour (path)
        }

        /// <summary>
        /// Update pheromone level on edge passed in parameter
        /// </summary>
        private void LocalUpdate(Edge edge)
        {
            double evaporate = (1 - Parameters.LocalEvaporationRate);
            Graph.EvaporatePheromone(edge, evaporate);

            double deposit = Parameters.LocalEvaporationRate * Parameters.T0;
            Graph.DepositPheromone(edge, deposit);
        }

        /// <summary>
        /// Update pheromone level on path for best ant
        /// </summary>
        private void GlobalUpdate()
        {
            double deltaR = 1 / GlobalBestAnt.Distance;
            foreach (Edge edge in GlobalBestAnt.Path)
            {
                double evaporate = (1 - Parameters.GlobalEvaporationRate);
                Graph.EvaporatePheromone(edge, evaporate);

                double deposit = Parameters.GlobalEvaporationRate * deltaR;
                Graph.DepositPheromone(edge, deposit);
            }
        }

        public TimeSpan GetExecutionTime()
        {
            return Stopwatch.Elapsed;
        }

        public void LoadProblemInstance(Problem problem)
        {
            Problem = problem;
            routes = null;
            cost = double.MaxValue;
        }

        public TimeSpan Solve()
        {
            RunAcs();
            return GetExecutionTime();
        }

        public Solution GetSolution()
        {
            return new Solution((int)cost, routes);
        }
    }
}