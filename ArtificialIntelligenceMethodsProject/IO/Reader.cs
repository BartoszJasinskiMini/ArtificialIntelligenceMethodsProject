using System;
using System.Collections.Generic;
using System.IO;
using ArtificialIntelligenceMethodsProject.Models;

namespace ArtificialIntelligenceMethodsProject.IO
{
    public enum DataSet { D = 68, M = 77, S = 83}

    public static class Reader
    {
        public static Problem ReadProblem(DataSet dataset, string filename)
        {
            var envSlashSetting = "\\";
            if(Environment.OSVersion.Platform == PlatformID.Unix)
            {
                envSlashSetting = "/";
            }

            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo != null)
            {
                var directory = directoryInfo.Parent?.FullName + envSlashSetting + "Graphs";
                var filepath = directory + envSlashSetting + dataset + envSlashSetting + filename;
                return Parse(filepath);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
        public static Problem ReadProblem(string filepath)
        {
            return Parse(filepath);  
        }
        private static Problem Parse(string filepath)
        {
            // PROBLEM PARSING 
            string[] lines = File.ReadAllLines(filepath + ".vrp");
            int index = 0;
            string name = lines[index].Substring(lines[index].LastIndexOf(' ')).Trim();
            int vehiclesCount = int.Parse(name.Substring(name.LastIndexOf('k') + 1));
            index++;
            string comment = lines[index].Substring(lines[index].LastIndexOf('(')).TrimEnd(')').TrimStart('(');
            index++;
            string type = lines[index].Substring(lines[index].LastIndexOf(' ')).Trim();
            index++;
            int nodesCount = int.Parse(lines[index].Substring(lines[index].LastIndexOf(' ')));
            index += 2;
            int capacity = int.Parse(lines[index].Substring(lines[index].LastIndexOf(' ')));
            index += 2;
            int demandIndex = index + nodesCount + 1;
            Graph graph = new Graph(int.Parse(lines[demandIndex + nodesCount + 1]));
            for (int i = 0; i < nodesCount; i++)
            {
                graph.AddVertice(GetVertice(lines[index + i], lines[demandIndex + i]));
            }

            // for (int i = 0; i < nodesCount; i++)
            // {
            //     for (int j = 0; j < nodesCount; j++)
            //     {
            //         if (j == i)
            //         {
            //             continue;
            //         }
            //         
            //         graph.AddEdge(new Edge(Math.Min(graph.Vertices[i].Number, graph.Vertices[j].Number)));
            //         
            //     }
            // }

            // SOLUTION PARSING
            List<int[]> routes = new List<int[]>();
            lines = File.ReadAllLines(filepath + ".sol");
            int j = 0;
            while(lines[j].Contains("Route"))
            {
                string[] tokens = lines[j].Substring(lines[j].IndexOf(':') + 1).TrimStart().TrimEnd().Split(' ');
                int[] route = new int[tokens.Length];
                for(int i = 0; i < tokens.Length; i++)
                {
                    route[i] = int.Parse(tokens[i]);
                }
                routes.Add(route);
                j++;
            }
            int cost = int.Parse(lines[j].Split(' ')[1]);
            Solution solution = new Solution(cost, routes);

            return new Problem(name, comment, type, nodesCount, vehiclesCount, capacity, graph, solution);
        }
        private static Vertice GetVertice(string verticePosition, string verticeDemand)
        {
            string[] tokens = verticePosition.Split(' ');
            int demand = int.Parse(verticeDemand.Substring(verticeDemand.IndexOf(' ')));
            return new Vertice(int.Parse(tokens[1]), int.Parse(tokens[2]), int.Parse(tokens[3]), demand);
        }
        
        // private static Edge GetEdge()
    }
}
