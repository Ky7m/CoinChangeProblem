﻿using System;
using System.Diagnostics;

namespace CoinChangeProblem
{
    static class CoinChangeProblemProgram
    {
        static void Main()
        {
            var coins = new [] { 1, 2, 5, 10, 25, 50 };
            const int target = 10000000;

            var sw = Stopwatch.StartNew();
            var result = new CoinChangeProblemSolution()
                            .SetAvailableCoins(coins)
                            .SetTargetSum(target)
                            .GetCountOfPossibleWays();
            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine(sw.Elapsed);
        }
    }
}
