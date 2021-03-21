using System;
using System.Collections.Generic;
using System.Text;

namespace ArtificialIntelligenceMethodsProject
{
    interface Algorithm
    {
        public void LoadProblemInstance(Problem problem);
        public void Solve();
    }
}
