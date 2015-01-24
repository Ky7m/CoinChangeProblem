using System;
using Xunit;
using Xunit.Extensions;

namespace CoinChangeProblem.Tests
{
    public class CoinChangeProblemSolutionTests
    {
        [Theory(DisplayName = "CheckCountOfPossibleWaysForCoinsSet", Timeout = 5000),
         InlineData(new ushort[] {1, 2, 5, 10, 25, 50}, 1u, 1u),
         InlineData(new ushort[] {50, 1, 5, 25, 10, 2}, 100u, 3953u),
         InlineData(new ushort[] {1, 2, 5, 10, 25, 50}, 10u, 11u),
         InlineData(new ushort[] {1, 2, 5, 10, 25, 50}, 100u, 3953u),
         InlineData(new ushort[] {1, 2, 5, 10, 25, 50}, 1000u, 83472746u),
         InlineData(new ushort[] {1, 2, 5, 10, 25, 50}, 10000u, 2526163903u),
         InlineData(new ushort[] {1, 2}, 10u, 6u)
        ]
        public void CheckCountOfPossibleWaysForCoinsSet(ushort[] coinsSet, uint targetSum, uint expected)
        {
            var results = GetResultsFromSolutionsMethods(coinsSet, targetSum);
            foreach (var result in results)
            {
                Assert.Equal(result, expected);
            }
        }

        private static uint[] GetResultsFromSolutionsMethods(ushort[] coins, uint targetSum)
        {
            var solutionMethods = (SolutionMethods[])Enum.GetValues(typeof(SolutionMethods));
            var results = new uint[solutionMethods.Length];

            for (var index = 0; index < solutionMethods.Length; index++)
            {
                SolutionMethods solutionMethod = solutionMethods[index];
                results[index] = new CoinChangeProblemSolution()
                            .SetAvailableCoins(coins)
                            .SetTargetSum(targetSum)
                            .Using(solutionMethod)
                            .GetCountOfPossibleWays();
            }
            Assert.Equal(solutionMethods.Length, results.Length);
            return results;
        }
    }
}
