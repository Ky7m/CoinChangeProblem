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
        private int[] _availableCoins;
        private uint _targetSum;
        private readonly Dictionary<ulong, uint> _cache = new Dictionary<ulong, uint>();

        public CoinChangeProblemSolution SetAvailableCoins(int[] coins)
        {
            Array.Sort(coins);
            _availableCoins = coins.Reverse().ToArray();
            return this;
        }

        public CoinChangeProblemSolution SetTargetSum(uint target)
        {
            _targetSum = target;
            return this;
        }

        public uint GetCountOfPossibleWays()
        {
            return _targetSum == 0 ? 0 : FindPossibleWays(_targetSum, 0);
        }

        private uint FindPossibleWays(uint sum, byte coinIndex)
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
                            result = FindPossibleWaysRecursive(sum, coinIndex);
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

                if (stackSize >= Int32.MaxValue/2)
                {
                    stackSize *= 2;
                }
                else
                {
                    stackSize = Int32.MaxValue;
                }
            }
        }

        private uint FindPossibleWaysRecursive(uint sum, byte coinIndex)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack();
            while (_availableCoins[coinIndex] > sum)
            {
                coinIndex++;
            }
            var c = (uint)_availableCoins[coinIndex];

            uint count;
            if (sum == c)
            {
                count = 1;
            }
            else
            {
                var remainSum = sum - c;
                var keyForRemainSum = ((ulong)remainSum << 32) | coinIndex;
                if (!_cache.TryGetValue(keyForRemainSum, out count))
                {
                    count = FindPossibleWaysRecursive(remainSum, coinIndex);
                    _cache.Add(keyForRemainSum, count);
                }
            }

            if (coinIndex >= (_availableCoins.Length - 1))
            {
                return count;
            }
            var keyForSubSum = ((ulong)sum << 32) | ((ulong)coinIndex + 1);
            uint subCount;
            if (!_cache.TryGetValue(keyForSubSum, out subCount))
            {
                subCount = FindPossibleWaysRecursive(sum, (byte)(coinIndex + 1));
                _cache.Add(keyForSubSum, subCount);
            }

            count += subCount;
            return count;
        }
    }
}