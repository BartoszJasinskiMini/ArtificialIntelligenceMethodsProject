using System;

namespace ArtificialIntelligenceMethodsProject.Algorithms
{
    public class Parameters
    {
        public double Alpha { get; set; }
        /// <summary>
        /// Relative importance of distance(default=2)
        /// </summary>
        public int Beta { get; set; }
        public double Sigma { get; set; }
        public double ro { get; set; }
        public int th { get; set; }

        /// <summary>
        /// Global evaporation rate of pheromone(0..1, default=0.1)
        /// </summary>
        public double GlobalEvaporationRate { get; set; }

        /// <summary>
        /// Local evaporation rate of pheromone(0..1, default=0.01)
        /// </summary>
        public double LocalEvaporationRate { get; set; }

        /// <summary>
        /// Probability of choosing best ant path instead of random roulette
        /// </summary>
        public double Q0 { get; set; } //Probably th

        /// <summary>
        /// Initial pheromone level along each Edge
        /// </summary>
        public double T0 { get; set; }

        /// <summary>
        /// Define how many ants will be used
        /// </summary>
        public int AntCount { get; set; }

        /// <summary>
        /// Number of iterations to perform (default=2500)
        /// </summary>
        public int Iterations { get; set; }



        public Parameters(int Beta, double GlobalEvaporationRate, double LocalEvaporationRate, double T0, double Q0, int AntCount, int Iterations)
        {
            this.Beta = Beta;
            this.GlobalEvaporationRate = GlobalEvaporationRate;
            this.LocalEvaporationRate = LocalEvaporationRate;
            this.T0 = T0;
            this.Q0 = Q0;
            this.AntCount = AntCount;
            this.Iterations = Iterations;
        }

        /// <summary>
        /// Default parameters
        /// </summary>
        public Parameters()
        {
            Alpha = 2;
            Beta = 5;
            Sigma = 3;
            ro = 0.8;
            th = 80;
            GlobalEvaporationRate = 0.1;
            LocalEvaporationRate = 0.01;
            Q0 = 0.9;
            AntCount = 22;
            Iterations = 10000;
            T0 = 0.01;
        }

        public void Show()
        {
            Console.WriteLine("Beta: " + Beta);
            Console.WriteLine("Global Evaporation Rate: " + GlobalEvaporationRate);
            Console.WriteLine("Local Evaporation Rate: " + LocalEvaporationRate);
            Console.WriteLine("Q0: " + Q0);
            Console.WriteLine("AntCount: " + AntCount);
            Console.WriteLine("Iterations: " + Iterations);
            Console.WriteLine("T0: " + T0);
            Console.WriteLine();
        }
    }
}