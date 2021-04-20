using ArtificialIntelligenceMethodsProject.Algorithms;
using ArtificialIntelligenceMethodsProject.Algorithms.AS;
using ArtificialIntelligenceMethodsProject.Algorithms.MinMaxAntSystem;
using ArtificialIntelligenceMethodsProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArtificialIntelligenceMethodsProject.IO
{
    enum OutputOptions
    {
        Console, File
    }
    class AutomatedCalculation
    {
        public static void RunACOlgorithm(int iterations, double maxVehicleDistance, ACOParameters parameters, DataSet set, OutputOptions options = OutputOptions.Console, string outPutFileName = null)
        {
            List<string> problems = ReadSet(set);
            foreach (string problem in problems)
            {
                Console.WriteLine(set + " " + problem);
                Problem problemInstance = Reader.ReadProblem(set, problem);
                for (int i = 0; i < iterations; i++)
                {
                    ACOAntSystem mmas = new ACOAntSystem(parameters, 1000.0);
                    mmas.LoadProblemInstance(problemInstance);
                    Console.WriteLine("Iteration " + i.ToString());
                    TimeSpan executionTime = mmas.Solve();
                    Solution sol = mmas.GetSolution();
                    string result;
                    switch (options)
                    {
                        case OutputOptions.Console:
                            result = sol != null ? sol.Cost.ToString() : "no solution found";
                            Console.WriteLine(problemInstance.Name + " Perfect Solution: " + problemInstance.Solution.Cost + " GreedyAlgorithm cost: " + result);
                            break;
                        case OutputOptions.File:
                            using (StreamWriter sw = File.AppendText(outPutFileName))
                            {
                                string line = new FileLine(problemInstance, sol, executionTime, i).ToString();
                                sw.WriteLine(line);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public static void RunMinMaxAlgorithm(int iterations, double maxVehicleDistance, MinMaxParameters parameters, DataSet set, OutputOptions options = OutputOptions.Console, string outPutFileName = null)
        {
            List<string> problems = ReadSet(set);
            foreach (string problem in problems)
            {
                Console.WriteLine(set + " " + problem);   
                Problem problemInstance = Reader.ReadProblem(set, problem);
                for (int i = 0; i < iterations; i++)
                {
                    MinMaxAntSystem minMaxAntSystem = new MinMaxAntSystem(parameters, maxVehicleDistance);
                    minMaxAntSystem.LoadProblemInstance(problemInstance);
                    Console.WriteLine("Iteration " + i.ToString());
                    TimeSpan executionTime = minMaxAntSystem.Solve();
                    Solution sol = minMaxAntSystem.GetSolution();
                    string result;
                    switch (options)
                    {
                        case OutputOptions.Console:
                            result = sol != null ? sol.Cost.ToString() : "no solution found";
                            Console.WriteLine(problemInstance.Name + " Perfect Solution: " + problemInstance.Solution.Cost + " GreedyAlgorithm cost: " + result);
                            break;
                        case OutputOptions.File:
                            using (StreamWriter sw = File.AppendText(outPutFileName))
                            {
                                string line = new FileLine(problemInstance, sol, executionTime, i).ToString();
                                sw.WriteLine(line);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

        }
        public static void RunGreedyAlgorithm(double maxVehicleDistance, DataSet set, OutputOptions options = OutputOptions.Console, string outPutFileName = null)
        {
            List<string> problems = ReadSet(set);
            List<FileLine> lines = new List<FileLine>();
            foreach (string problem in problems)
            {
                Console.WriteLine(set + " " + problem);
                GreedyAlgorithm greedyAlgorithm = new GreedyAlgorithm(maxVehicleDistance);
                Problem problemInstance = Reader.ReadProblem(set, problem);
                greedyAlgorithm.LoadProblemInstance(problemInstance);
                TimeSpan executionTime = greedyAlgorithm.Solve();
                Solution sol = greedyAlgorithm.GetSolution();
                string result;
                switch (options)
                {
                    case OutputOptions.Console:
                        result = sol != null ? sol.Cost.ToString() : "no solution found";
                        Console.WriteLine(problemInstance.Name + " Perfect Solution: " + problemInstance.Solution.Cost + " GreedyAlgorithm cost: " + result);
                        break;
                    case OutputOptions.File:
                        lines.Add(new FileLine(problemInstance, sol, executionTime, 1));
                        break;
                    default:
                        break;
                }
            }
            if(options == OutputOptions.File)
                WriteToFile(lines, outPutFileName);
        }
        private static void WriteToFile(List<FileLine> lines, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                for(int i = 0; i < lines.Count; i++)
                {
                    writer.WriteLine(lines[i].ToString());
                }
            }
        }
        public static List<string> ReadSet(DataSet dataset)
        {
            List<string> instanceNames = new List<string>();
            var envSlashSetting = "\\";
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                envSlashSetting = "/";
            }

            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo != null)
            {
                var directory = directoryInfo.Parent?.FullName + envSlashSetting + "Graphs" + envSlashSetting + dataset;
                string[] result = Directory.GetFiles(directory);
                foreach(string r in result)
                {
                    if (r.EndsWith("vrp"))
                    {
                        string end = r.Substring(r.LastIndexOf(envSlashSetting) + 1);
                        string name = end.Remove(end.LastIndexOf('.'), 4);
                        instanceNames.Add(name);
                    }
                }
            }
            return instanceNames;
        }
    }
}
