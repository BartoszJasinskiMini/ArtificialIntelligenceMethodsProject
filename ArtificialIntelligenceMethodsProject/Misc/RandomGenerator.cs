using System;
using System.Collections.Generic;
using System.Linq;

namespace ArtificialIntelligenceMethodsProject.Misc
{
    public class RandomGenerator
    {
        public static RandomGenerator Instance { get; } = new RandomGenerator();
        public Random Random { get; set; }

        private RandomGenerator() => Random = new Random();

        public static double GetDoubleRangeRandomNumber(double minimum, double maximum)
        {
            return Instance.Random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static bool GetTrueWithSpecifiedProbability(double probability)
        {
            if (probability < 0 || probability > 1)
            {
                Console.WriteLine("GetTrueWithSpecifiedProbability(double probability): PROBABILITY IS OUTSIDE [0, 1] RANGE");
            }

            return GetDoubleRangeRandomNumber(0, 1) <= probability;
        }


        public static List<int> GenerateRandom(int count, int min, int max)
        {
            return Enumerable.Range(min, max).OrderBy(x => Instance.Random.Next()).Take(count).ToList();
        }

        public static int GetRandomElementFromList(List<int> list)
        {
            Random r = new Random();
            return list[r.Next(list.Count)];
        }

        public static int GetRandomIntFromRange(int lowerBoundary, int upperBoundary)
        {
            var random = new Random();
            return random.Next(lowerBoundary, upperBoundary);
        }
        
        public static int GetRandom(List<int> list, List<double> probabilities)
        {
            Random rand = new Random();
            double u = probabilities.Sum();

            double r = rand.NextDouble() * u;

            double sum = 0;
            for(int i = 0; i < list.Count(); i++)
            {
                if(r <= (sum = sum + probabilities[i]))
                {
                    return list[i];
                }
            }
            
            return -1000000;
        }
    }
}