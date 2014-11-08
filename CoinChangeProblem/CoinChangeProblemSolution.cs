using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace CoinChangeProblem
{
    public sealed class CoinChangeProblemSolution
    {
        private static ushort[] availableCoins;
        private uint _targetSum;
        private SolutionMethods _targetSolutionMethods;
        private static readonly Dictionary<ulong, uint> Cache = new Dictionary<ulong, uint>();

        private readonly Dictionary<SolutionMethods, Func<uint, uint>> _registeredMethods = new Dictionary
            <SolutionMethods, Func<uint, uint>>
                                        {
                                            { SolutionMethods.Recursive, FindPossibleWaysUsingRecursion },
                                            { SolutionMethods.DynamicProgramming, FindPossibleWaysUsingDynamicProgramming }
                                        };


        public CoinChangeProblemSolution SetAvailableCoins(ushort[] coins)
        {
            Array.Sort(coins);
            availableCoins = coins.Reverse().ToArray();
            return this;
        }

        public CoinChangeProblemSolution SetTargetSum(uint target)
        {
            _targetSum = target;
            return this;
        }
        public CoinChangeProblemSolution Using(SolutionMethods method)
        {
            _targetSolutionMethods = method;
            return this;
        }

        public uint GetCountOfPossibleWays()
        {
            return _targetSum == 0 ? 0 : _registeredMethods[_targetSolutionMethods](_targetSum);
        }

        private static uint FindPossibleWaysUsingRecursion(uint sum)
        {
            uint? result = null;
            var stackSize = 0x40000;

            while (true)
            {
                var currentStackSize = stackSize;
                var worker = new Thread(
                    () =>
                    {
                        try
                        {
                            result = FindPossibleWaysRecursive(sum, 0);
                        }
                        catch (InsufficientExecutionStackException)
                        {
                            Debug.Print("InsufficientExecutionStackException on stack size:{0}", Convert.ToString(currentStackSize));
                        }
                    },
                    stackSize);

                worker.Start();
                worker.Join();

                if (result != null)
                {
                    return result.Value;
                }

                if (stackSize >= Int32.MaxValue / 2)
                {
                    stackSize *= 2;
                }
                else
                {
                    stackSize = Int32.MaxValue;
                }
            }
        }

        private static uint FindPossibleWaysRecursive(uint sum, byte coinIndex)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack();
            while (availableCoins[coinIndex] > sum)
            {
                coinIndex++;
            }
            var c = (uint)availableCoins[coinIndex];

            uint count;
            if (sum == c)
            {
                count = 1;
            }
            else
            {
                var remainSum = sum - c;
                var keyForRemainSum = ((ulong)remainSum << 32) | coinIndex;
                if (!Cache.TryGetValue(keyForRemainSum, out count))
                {
                    count = FindPossibleWaysRecursive(remainSum, coinIndex);
                    Cache.Add(keyForRemainSum, count);
                }
            }

            if (coinIndex >= (availableCoins.Length - 1))
            {
                return count;
            }
            var keyForSubSum = ((ulong)sum << 32) | ((ulong)coinIndex + 1);
            uint subCount;
            if (!Cache.TryGetValue(keyForSubSum, out subCount))
            {
                subCount = FindPossibleWaysRecursive(sum, (byte)(coinIndex + 1));
                Cache.Add(keyForSubSum, subCount);
            }

            count += subCount;
            return count;
        }

        private static uint FindPossibleWaysUsingDynamicProgramming(uint sum)
        {
            var table = new uint[sum + 1];
            table[0] = 1;

            // Pick all coins one by one and update the table[] values
            // after the index greater than or equal to the value of the
            // picked coin
            for (var i = 0; i < availableCoins.Length; i++)
            {
                for (var j = availableCoins[i]; j <= sum; j++)
                {
                    table[j] += table[j - availableCoins[i]];
                }
            }

            return table[sum];
        }
    }
}