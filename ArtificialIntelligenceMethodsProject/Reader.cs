using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ArtificialIntelligenceMethodsProject
{
    enum DataSet { D = 68, M = 77, S = 83}
    class Reader
    {
        public static Problem ReadProblem(DataSet dataset, string filename)
        {
            string directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Graphs";
            string filepath = directory + "\\" + dataset.ToString() + "\\" + filename;
            return Parse(filepath);

        }
        public static Problem ReadProblem(string filepath)
        {
            return Parse(filepath);  
        }
        private static Problem Parse(string filepath)
        {
            string[] lines = System.IO.File.ReadAllLines(filepath);
            int index = 0;
            string name = lines[index].Substring(lines[index].LastIndexOf(' ')).Trim();
            index++;
            string comment = lines[index].Substring(lines[index].LastIndexOf('(')).TrimEnd(')').TrimStart('(');
            index++;
            string type = lines[index].Substring(lines[index].LastIndexOf(' ')).Trim();
            index++;
            int neightboursCount = int.Parse(lines[index].Substring(lines[index].LastIndexOf(' ')));
            index += 2;
            int capacity = int.Parse(lines[index].Substring(lines[index].LastIndexOf(' ')));
            index += 2;
            Graph graph = new Graph();
            for(int i = 0; i < neightboursCount; i++)
            {
                string[] tokens = lines[index + i].Split(' ');
                int demandIndex = index + i + neightboursCount + 1;
                int demand = int.Parse(lines[demandIndex].Substring(lines[demandIndex].IndexOf(' ')));
                graph.AddSupplyPoint(new Vertice(int.Parse(tokens[1]), int.Parse(tokens[2]), int.Parse(tokens[3]), demand));
            }
            return new Problem(name, comment, type, neightboursCount, capacity, graph);
        }
    }
}
