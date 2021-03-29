using System.Collections.Generic;

namespace ArtificialIntelligenceMethodsProject.Models
{
    public class Solution
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
