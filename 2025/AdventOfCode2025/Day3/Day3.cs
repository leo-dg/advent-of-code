using AdventOfCode2025.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Day3
{
    public class Day3 : IDay<ulong>
    {
        private readonly IReaderService _reader;

        public Day3(IReaderService reader)
        {
            _reader = reader;
        }

        public ulong SolvePart1(string inputPath) =>
            _reader
                .ReadLinesAsStrings(inputPath)
                .Select(b => GetMaxJoltage(b, 2))
                .Aggregate((ulong)0, (sum, joltage) => sum + joltage);

        public ulong SolvePart2(string inputPath) => 
            _reader
                .ReadLinesAsStrings(inputPath)
                .Select(b => GetMaxJoltage(b, 12))
                .Aggregate((ulong)0, (sum, joltage) => sum + joltage);

        private ulong GetMaxJoltage(string bank, int batteryLimit)
        {
            var intBank = bank
                .Select(b => (int)char.GetNumericValue(b))
                .ToArray();

            ulong jolts = 0;
            var indexOfLastMax = -1;

            for (int i = 0; i < batteryLimit; i++)
            {
                var scopedIntBank = intBank[..(intBank.Length - batteryLimit + i + 1)];

                var (max, indexOfMax) = GetMaxFrom(scopedIntBank, indexOfLastMax + 1);

                indexOfLastMax = indexOfMax;
                jolts += ((ulong)max * (ulong)Math.Pow(10, batteryLimit - i - 1));
            }

            return jolts;
        }

        private (int max, int index) GetMaxFrom(int[] ints, int from)
        {
            var indexOfMax = from;

            for (var i = indexOfMax + 1; i < ints.Length; i++)
            {
                if (ints[i] > ints[indexOfMax])
                {
                    indexOfMax = i;
                }
            }

            return (ints[indexOfMax], indexOfMax);
        }   
    }
}
