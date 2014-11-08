using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoinChangeProblem.Tests
{
    [TestClass]
    public class CoinChangeProblemSolutionTests
    {
        [TestMethod]
        [Description("Check count of possible ways. Target sum: 1. Available coins: 1, 2, 5, 10, 25, 50")]
        public void CheckCountFor1Using_1_2_5_10_25_50()
        {
            RunTest(new ushort[]{ 1, 2, 5, 10, 25, 50 }, 1, 1u);
        }

        [TestMethod]
        [Description("Check coins set sort functionality. Target sum: 100. Available coins: 1, 2, 5, 10, 25, 50")]
        public void CheckCoinsSetSortFunctionality()
        {
            RunTest(new ushort[] { 50, 1, 5, 25, 10, 2 }, 100, 3953u);
        }

        [TestMethod]
        [Description("Check count of possible ways. Target sum: 10. Available coins: 1, 2, 5, 10, 25, 50")]
        public void CheckCountFor10Using_1_2_5_10_25_50()
        {
            RunTest(new ushort[] { 1, 2, 5, 10, 25, 50 }, 10, 11u);
        }

        [TestMethod]
        [Description("Check count of possible ways. Target sum: 100. Available coins: 1, 2, 5, 10, 25, 50")]
        public void CheckCountFor100Using_1_2_5_10_25_50()
        {
            RunTest(new ushort[] { 1, 2, 5, 10, 25, 50 }, 100, 3953u);
        }

        [TestMethod]
        [Description("Check count of possible ways. Target sum: 1000. Available coins: 1, 2, 5, 10, 25, 50")]
        public void CheckCountFor1000Using_1_2_5_10_25_50()
        {
            RunTest(new ushort[] { 1, 2, 5, 10, 25, 50 }, 1000,83472746u);
        }

        [TestMethod]
        [Description("Check count of possible ways. Target sum: 10000. Available coins: 1, 2, 5, 10, 25, 50")]
        public void CheckCountFor10000Using_1_2_5_10_25_50()
        {
            RunTest(new ushort[] { 1, 2, 5, 10, 25, 50 }, 10000, 2526163903u);
        }

        [TestMethod]
        [Description("Check count of possible ways. Target sum: 10. Available coins: 1, 2")]
        public void CheckCountFor10Using_1_2()
        {
            RunTest(new ushort[] { 1, 2 }, 10, 6u);
        }

        private static void RunTest(ushort[] coins, uint targetSum, uint expected)
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

            Assert.AreEqual(solutionMethods.Length, results.Length);
            foreach (var result in results)
            {
                Assert.AreEqual(result, expected);
            }
        }
    }
}
