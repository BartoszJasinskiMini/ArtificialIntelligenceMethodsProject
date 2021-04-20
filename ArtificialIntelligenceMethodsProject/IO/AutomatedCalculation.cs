using ArtificialIntelligenceMethodsProject.Algorithms;
using ArtificialIntelligenceMethodsProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArtificialIntelligenceMethodsProject.IO
{
    class AutomatedCalculation
    {
        public static void RunGreedyAlgorithm(double maxVehicleDistance, DataSet set)
        {
            List<string> problems = ReadSet(set);
            foreach(string problem in problems)
            {
                Console.WriteLine(set + " " + problem);
                GreedyAlgorithm greedyAlgorithm = new GreedyAlgorithm(maxVehicleDistance);
                Problem problemInstance = Reader.ReadProblem(set, problem);
                greedyAlgorithm.LoadProblemInstance(problemInstance);
                greedyAlgorithm.Solve();
                Solution sol = greedyAlgorithm.GetSolution();

                string result = sol != null ? sol.Cost.ToString() : "no solution found";
                Console.WriteLine(problemInstance.Name + " Perfect Solution: " + problemInstance.Solution.Cost + " GreedyAlgorithm cost: " + result);
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
