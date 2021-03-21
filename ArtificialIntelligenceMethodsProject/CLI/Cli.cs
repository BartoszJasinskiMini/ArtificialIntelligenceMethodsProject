using System;
using CommandDotNet;

namespace ArtificialIntelligenceMethodsProject.CLI
{
    public class Cli
    {
        [Command(Name="sum",
            Usage="sum <int> <int>",
            Description="sums two numbers",
            ExtendedHelpText="more details and examples")]
            public void As(int value1, int value2)
            {
                Console.WriteLine($"Answer:  {value1 + value2}");
            }
        
            public void Mmas(int value1, int value2)
            {
                Console.WriteLine($"Answer:  {value1 - value2}");
            }
            
            public void Iaco(int value1, int value2)
            {
                Console.WriteLine($"Answer:  {value1 - value2}");
            }
                        
            public void Greedy(int value1, int value2)
            {
                Console.WriteLine($"Answer:  {value1 - value2}");
            }
                                    
            
    }
}