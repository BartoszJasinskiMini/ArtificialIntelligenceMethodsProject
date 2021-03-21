using System;
using System.Collections.Generic;
using System.Text;

namespace ArtificialIntelligenceMethodsProject
{
    class Solution
    {
        public List<int[]> Routes { get; }
        public int Cost { get; }
        public Solution(int cost, List<int[]> routes)
        {
            Cost = cost;
            Routes = routes;
        }
    }
}
