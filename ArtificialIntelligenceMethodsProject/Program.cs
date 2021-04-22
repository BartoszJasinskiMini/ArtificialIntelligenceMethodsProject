using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using ArtificialIntelligenceMethodsProject.Algorithms;
using ArtificialIntelligenceMethodsProject.Algorithms.AS;
using ArtificialIntelligenceMethodsProject.Algorithms.BasicACO;
using ArtificialIntelligenceMethodsProject.Algorithms.IACO;
// using ArtificialIntelligenceMethodsProject.Algorithms.MinMaxAntSystem;
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
            IACOParameters parameters = new IACOParameters(1, 2, 15, 5000, 0.95, 0.05, 0.2, 1.0, 0.05, 0.2);
            AutomatedCalculation.RunIACOlgorithm(5, 1000.0, parameters, DataSet.M, OutputOptions.File, "IACOM.txt");
            AutomatedCalculation.RunIACOlgorithm(5, 1000.0, parameters, DataSet.S, OutputOptions.File, "IACOS.txt");
            AutomatedCalculation.RunIACOlgorithm(5, 1000.0, parameters, DataSet.D, OutputOptions.File, "IACOD.txt");
            // AutomatedCalculation.RunMinMaxAlgorithm(5, 1000.0, parameters, DataSet.M, OutputOptions.File, "minMaxM.txt");
            // AutomatedCalculation.RunMinMaxAlgorithm(5, 1000.0, parameters, DataSet.S, OutputOptions.File, "minMaxS.txt");
            // AutomatedCalculation.RunMinMaxAlgorithm(5, 1000.0, parameters, DataSet.D, OutputOptions.File, "minMaxD.txt");



            /*           var problem = Reader.ReadProblem(DataSet.M, "A-n32-k5");*/
            /*            IACOAntSystem iaco = new IACOAntSystem(parameters, 1000.0);
                        iaco.LoadProblemInstance(problem);
                        var executionTime = iaco.Solve();
                        var sol = iaco.GetSolution();
                        var result = sol != null ? sol.Cost.ToString() : "no solution found";
                        Console.WriteLine(" GreedyAlgorithm cost: " + result);*/
            // var solver = new Solver(parameters2, problem.Graph, problem);
            // Console.WriteLine("TIME = " + solver.Solve());


            /*            ACO greedyAlgorithm = new ACO();
                        greedyAlgorithm.LoadProblemInstance(problem);
                        greedyAlgorithm.Solve();*/
            // Solution solution = greedyAlgorithm.GetSolution();

            return 0;
            // return new AppRunner<Cli>().Run(args);
        }

    }
    
    
}