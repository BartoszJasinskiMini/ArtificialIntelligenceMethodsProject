﻿using System;
using System.Collections.Generic;
using System.IO;
using ArtificialIntelligenceMethodsProject.Algorithms;
using ArtificialIntelligenceMethodsProject.Algorithms.MinMaxAntSystem;
using ArtificialIntelligenceMethodsProject.CLI;
using ArtificialIntelligenceMethodsProject.IO;
using ArtificialIntelligenceMethodsProject.Models;
using CommandDotNet;

namespace ArtificialIntelligenceMethodsProject
{
    class Program
    {
        static int Main(string[] args)
        {
            //Problem problem = Reader.ReadProblem(DataSet.D, "M-n101-k10");
            MinMaxParameters parameters = new MinMaxParameters(1, 2, 15, 100, 0.95, 0.05, 0.2, 1.0);
            AutomatedCalculation.RunMinMaxAlgorithm(20, 1000.0, parameters, DataSet.M, OutputOptions.File, "minMaxM.txt");
            AutomatedCalculation.RunGreedyAlgorithm(1000.0, DataSet.M, OutputOptions.File, "test.txt");
            /*AutomatedCalculation.RunGreedyAlgorithm(1000.0, DataSet.S);
            AutomatedCalculation.RunGreedyAlgorithm(1000.0, DataSet.D);*/


            /*            ACO greedyAlgorithm = new ACO();
                        greedyAlgorithm.LoadProblemInstance(problem);
                        greedyAlgorithm.Solve();*/
            // Solution solution = greedyAlgorithm.GetSolution();

            return 0;
            // return new AppRunner<Cli>().Run(args);
        }
        // static void Main(string[] args)
        // {
        //     List<Point> points = TspFileReader.ReadTspFile(@"TSP\kroA100.tsp");    // Parse TSPlib file and load as List<Point>
        //
        //     Graph2 graph = new Graph2(points, true);  // Create Graph
        //     // GreedyAlgorithm greedyAlgorithm = new GreedyAlgorithm(graph);
        //     // double greedyShortestTourDistance = greedyAlgorithm.Run();  // get shortest tour using greedy algorithm
        //     Parameters parameters = new Parameters();  // Most parameters will be default. We only have to set T0 (initial pheromone level)
        //     // {
        //     //     T0 = (1.0 / (graph.Dimensions * greedyShortestTourDistance))
        //     // };
        //     parameters.Show();
        //
        //     Solver solver = new Solver(parameters, graph);
        //     List<double> results = solver.RunACS(); // Run ACS
        //
        //     Console.WriteLine("Time: " + solver.GetExecutionTime());
        //     Console.ReadLine();
        // }
    }
    
    
}