using System.Collections.Generic;
using System.Linq;
using ArtificialIntelligenceMethodsProject.Models;

namespace ArtificialIntelligenceMethodsProject.Misc
{
    public static class Helper
    {
        public static IEnumerable<Edge> EdgeCumulativeSum(IEnumerable<Edge> sequence)
        {
            double sum = 0;
            foreach (var item in sequence)
            {
                sum += item.Weight;
                item.Weight = sum;
            }

            return sequence;
        }

        public static Vertice GetRandomEdge(IEnumerable<Edge> cumSum)
        {
            var random = RandomGenerator.Instance.Random.NextDouble();
            return cumSum.First(j => j.Weight >= random).End;
        }
        
        public static double Pow(double num, int exp)
        {
            double result = 1.0;
            while (exp > 0)
            {
                if (exp % 2 == 1)
                    result *= num;
                exp >>= 1;
                num *= num;
            }

            return result;
        }


        public static int HashFunction(int x, int y)
        {
            return (10000000 * x) + y;
        }
    }
}