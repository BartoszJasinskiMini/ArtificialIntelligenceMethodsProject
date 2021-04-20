using ArtificialIntelligenceMethodsProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtificialIntelligenceMethodsProject.IO
{
    class FileLine
    {
        private Problem problem;
        private Solution solution;
        private TimeSpan executionTime;
        private int testNumber;

        public FileLine(Problem problem, Solution solution, TimeSpan executionTime, int testNumber)
        {
            this.problem = problem;
            this.solution = solution;
            this.executionTime = executionTime;
            this.testNumber = testNumber;
        }
        override
        public string ToString()
        {
            string result = problem.Name + " ; ";
            result += problem.Solution.Cost.ToString() + " ; ";
            result += (testNumber + 1).ToString() + " ; ";
            result += (solution != null ? solution.Cost.ToString() : "-") + " ; ";
            result += executionTime.Minutes.ToString() + ':' + executionTime.Seconds.ToString() + ':' + executionTime.Milliseconds.ToString() + " ; ";
            result += (solution != null ? solution.MaxIterations.ToString() : "-" )+ " ; ";
            result += (solution != null ? solution.ResultIteration.ToString() : "-") + " ; ";
            result += (solution != null ? solution.ConvergentIteration.ToString() : "-") + " ; ";
            return result;
        }
    }
}
