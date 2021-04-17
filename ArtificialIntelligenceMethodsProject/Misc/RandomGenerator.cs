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

        /// <summary>
        /// Generate List of unique numbers
        /// </summary>
        public static List<int> GenerateRandom(int count, int min, int max)
        {
            return Enumerable.Range(min, max).OrderBy(x => Instance.Random.Next()).Take(count).ToList();
        }

        public static int GetRandomElementFromList(List<int> list)
        {
            Random r = new Random();
            return list[r.Next(list.Count)];
        }
        
        public static int GetRandom(List<int> list, List<double> probabilities)
        {
            Random rand = new Random();
            // get universal probability 
            double u = probabilities.Sum();

            // pick a random number between 0 and u
            double r = rand.NextDouble() * u;

            double sum = 0;
            for(int i = 0; i < list.Count(); i++)
            {
                // loop until the random number is less than our cumulative probability
                if(r <= (sum = sum + probabilities[i]))
                {
                    return list[i];
                }
            }
            
            // should never get here
            return -1000000;
        }
    }
}