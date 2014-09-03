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
            var result = new CoinChangeProblemSolution()
                            .SetAvailableCoins(new[] { 1, 2, 5, 10, 25, 50 })
                            .SetTargetSum(1)
                            .GetCountOfPossibleWays();
            Assert.AreEqual(result, 1u);
        }

        [TestMethod]
        [Description("Check coins set sort functionality. Target sum: 100. Available coins: 1, 2, 5, 10, 25, 50")]
        public void CheckCoinsSetSortFunctionality()
        {
            var result = new CoinChangeProblemSolution()
                            .SetAvailableCoins(new[] { 50, 1, 5, 25, 10, 2 })
                            .SetTargetSum(100)
                            .GetCountOfPossibleWays();
            Assert.AreEqual(result, 3953u);
        }

        [TestMethod]
        [Description("Check count of possible ways. Target sum: 10. Available coins: 1, 2, 5, 10, 25, 50")]
        public void CheckCountFor10Using_1_2_5_10_25_50()
        {
            var result = new CoinChangeProblemSolution()
                            .SetAvailableCoins(new[] { 1, 2, 5, 10, 25, 50 })
                            .SetTargetSum(10)
                            .GetCountOfPossibleWays();
            Assert.AreEqual(result, 11u);
        }

        [TestMethod]
        [Description("Check count of possible ways. Target sum: 100. Available coins: 1, 2, 5, 10, 25, 50")]
        public void CheckCountFor100Using_1_2_5_10_25_50()
        {
            var result = new CoinChangeProblemSolution()
                            .SetAvailableCoins(new[] { 1, 2, 5, 10, 25, 50 })
                            .SetTargetSum(100)
                            .GetCountOfPossibleWays();
            Assert.AreEqual(result, 3953u);
        }

        [TestMethod]
        [Description("Check count of possible ways. Target sum: 1000. Available coins: 1, 2, 5, 10, 25, 50")]
        public void CheckCountFor1000Using_1_2_5_10_25_50()
        {
            var result = new CoinChangeProblemSolution()
                            .SetAvailableCoins(new[] { 1, 2, 5, 10, 25, 50 })
                            .SetTargetSum(1000)
                            .GetCountOfPossibleWays();
            Assert.AreEqual(result, 83472746u);
        }
        
        [TestMethod]
        [Description("Check count of possible ways. Target sum: 10000. Available coins: 1, 2, 5, 10, 25, 50")]
        public void CheckCountFor10000Using_1_2_5_10_25_50()
        {
            var result = new CoinChangeProblemSolution()
                            .SetAvailableCoins(new[] { 1, 2, 5, 10, 25, 50 })
                            .SetTargetSum(10000)
                            .GetCountOfPossibleWays();
            Assert.AreEqual(result, 2526163903u);
        }
        
        [TestMethod]
        [Description("Check count of possible ways. Target sum: 100000. Available coins: 1, 2, 5, 10, 25, 50")]
        public void CheckCountFor100000Using_1_2_5_10_25_50()
        {
            var result = new CoinChangeProblemSolution()
                            .SetAvailableCoins(new[] { 1, 2, 5, 10, 25, 50 })
                            .SetTargetSum(100000)
                            .GetCountOfPossibleWays();
            Assert.AreEqual(result, 3613522941u);
        }

        [TestMethod]
        [Description("Check count of possible ways. Target sum: 10. Available coins: 1, 2")]
        public void CheckCountFor10Using_1_2()
        {
            var result = new CoinChangeProblemSolution()
                            .SetAvailableCoins(new[] { 1,2})
                            .SetTargetSum(10)
                            .GetCountOfPossibleWays();
            Assert.AreEqual(result, 6u);
        }
    }
}
