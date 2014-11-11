using System;
using System.Diagnostics;

namespace CoinChangeProblem
{
    static class CoinChangeProblemProgram
    {
        static void Main()
        {
            var coins = new ushort[] { 1, 2, 5, 10, 25, 50 };
            const uint target = 1000000000;

            var sw = Stopwatch.StartNew();
            var result = new CoinChangeProblemSolution()
                            .SetAvailableCoins(coins)
                            .SetTargetSum(target)
                            .Using(SolutionMethods.DynamicProgramming)
                            .GetCountOfPossibleWays();
            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine(sw.Elapsed);
        }
    }
}
