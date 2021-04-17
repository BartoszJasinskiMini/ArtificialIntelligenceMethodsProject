using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ArtificialIntelligenceMethodsProject.Misc;
using ArtificialIntelligenceMethodsProject.Models;

namespace ArtificialIntelligenceMethodsProject.Algorithms
{
    public class Solver
    {
        public Parameters Parameters { get; set; }
        private AntSystem GlobalBestAnt { get; set; }
        private List<double> Results { get; set; }
        private Graph2 Graph2 { get; set; }
        private Stopwatch Stopwatch { get; set; }

        public Solver(Parameters parameters, Graph2 graph)
        {
            Parameters = parameters;
            graph.MinimumPheromone = parameters.T0;
            Graph2 = graph;
            Results = new List<double>();
            Stopwatch = new Stopwatch();
        }

        /// <summary>
        /// Main loop of ACS algorithm
        /// </summary>
        public List<double> RunACS()
        {
            Stopwatch.Start();
            Graph2.ResetPheromone(Parameters.T0);
            for (int i = 0; i < Parameters.Iterations; i++)
            {
                List<AntSystem> antColony = CreateAnts();
                GlobalBestAnt ??= antColony[0];

                AntSystem localBestAnt = BuildTours(antColony);
                if (Math.Round(localBestAnt.Distance, 2) < Math.Round(GlobalBestAnt.Distance, 2))
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
        /// Create ants and place every ant in random point on graph (warning AntCount < Dimensions)
        /// </summary>
        public List<AntSystem> CreateAnts()
        {
            List<AntSystem> antColony = new List<AntSystem>();
            List<int> randomPoints = RandomGenerator.GenerateRandom(Parameters.AntCount, 1, Graph2.Points.Count);
            foreach (int random in randomPoints)
            {
                AntSystem ant = new AntSystem(Graph2, Parameters.Beta, Parameters.Q0);
                ant.Init(random);
                antColony.Add(ant);
            }
            return antColony;
        }

        /// <summary>
        /// This method builds solution for every ant in AntColony and return the best ant (with shortest distance tour)
        /// </summary>
        public AntSystem BuildTours(List<AntSystem> antColony)
        {
            for (int i = 0; i < Graph2.Dimensions; i++)
            {
                foreach (AntSystem ant in antColony)
                {
                    Edge2 edge2 = ant.Move();
                    LocalUpdate(edge2);
                }
            }

            GlobalUpdate();

            return antColony.OrderBy(x => x.Distance).FirstOrDefault(); // find shortest ant tour (path)
        }

        /// <summary>
        /// Update pheromone level on edge passed in parameter
        /// </summary>
        public void LocalUpdate(Edge2 edge2)
        {
            double evaporate = (1 - Parameters.LocalEvaporationRate);
            Graph2.EvaporatePheromone(edge2, evaporate);

            double deposit = Parameters.LocalEvaporationRate * Parameters.T0;
            Graph2.DepositPheromone(edge2, deposit);
        }

        /// <summary>
        /// Update pheromone level on path for best ant
        /// </summary>
        public void GlobalUpdate()
        {
            double deltaR = 1 / GlobalBestAnt.Distance;
            foreach (Edge2 edge in GlobalBestAnt.Path)
            {
                double evaporate = (1 - Parameters.GlobalEvaporationRate);
                Graph2.EvaporatePheromone(edge, evaporate);

                double deposit = Parameters.GlobalEvaporationRate * deltaR;
                Graph2.DepositPheromone(edge, deposit);
            }
        }

        public TimeSpan GetExecutionTime()
        {
            return Stopwatch.Elapsed;
        }
    }
}