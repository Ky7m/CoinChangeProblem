using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CoinChangeProblem
{
    public sealed class CoinChangeProblemSolution
    {
        private int[] _availableCoins;
        private uint _targetSum;
        private readonly Hashtable _cache = new Hashtable();
        private readonly HashSet<ulong> _cacheIndex = new HashSet<ulong>();

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
                if (!_cacheIndex.Contains(keyForRemainSum))
                {
                    count = FindPossibleWays(remainSum, coinIndex);
                    _cache[keyForRemainSum] = count;
                    _cacheIndex.Add(keyForRemainSum);
                }
                else
                {
                    count = (uint)_cache[keyForRemainSum];
                }
            }

            if (coinIndex >= (_availableCoins.Length - 1))
            {
                return count;
            }
            var keyForSubSum = ((ulong)sum << 32) | ((ulong)coinIndex + 1);
            uint subCount;
            if (!_cacheIndex.Contains(keyForSubSum))
            {
                subCount = FindPossibleWays(sum, (byte)(coinIndex + 1));
                _cache[keyForSubSum] = subCount;
                _cacheIndex.Add(keyForSubSum);
            }
            else
            {
                subCount = (uint)_cache[keyForSubSum];
            }
            count += subCount;
            return count;
        }
    }
}