using AdventOfCode2025.Services;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2025.Day2
{
    public class Day2 : IDay<long>
    {

        private readonly IReaderService _reader;

        public Day2(IReaderService reader)
        {
            _reader = reader;
        }

        public long SolvePart1(string inputPath)
        {
            long invalidIdTotal = 0;

            var enumeratedRanges = _reader
                .ReadLinesAsStrings(inputPath)[0]
                .Split(",")
                .Select(EnumerateRange);

            foreach (var range in enumeratedRanges)
            {
                var maybeInvalidIdsByLength = range
                    .ToLookup(id => id.Length)
                    .Where(idByLength => idByLength.Key % 2 == 0);

                foreach (var idsGrouping in maybeInvalidIdsByLength)
                {
                    var patternLength = idsGrouping.Key / 2;

                    invalidIdTotal += idsGrouping
                        .Where(id => id[..patternLength] == id[patternLength..])
                        .Aggregate((long)0, (sum, id) => long.Parse(id) + sum);
                }
            }

            return invalidIdTotal;
        }

        public long SolvePart2(string inputPath)
        {
            long invalidIdTotal = 0;

            var enumeratedRanges = _reader
                .ReadLinesAsStrings(inputPath)[0]
                .Split(",")
                .Select(EnumerateRange);

            foreach (var range in enumeratedRanges)
            {
                var idsByLength = range
                    .ToLookup(id => id.Length)
                    .Where(group => group.Key > 1);

                foreach (var idsGroup in idsByLength)
                {
                    var lengthFactors = GetFactors(idsGroup.Key);

                    foreach (var id in idsGroup)
                    {
                        invalidIdTotal += lengthFactors.Any(length => HasRepeatingPattern(id, length))
                            ? long.Parse(id)
                            : 0;
                    }
                }
            }

            return invalidIdTotal;
        }
        private IEnumerable<int> GetFactors(int idLength)
        {
            var list = new List<int>() { 1 };

            for (int i = 2; i < idLength; i++)
            {
                if (idLength % i == 0)
                {
                    list.Add(i);
                }
            }

            return list;
        }
        private bool HasRepeatingPattern(string value, int length)
        {
            var pattern = value[..length];
            var restOfValue = value[length..];

            for (int i = 0; i < restOfValue.Length; i += length)
            {
                if (restOfValue.Substring(i, length) != pattern)
                {
                    return false;
                }
            }

            return true;
        }

        private IEnumerable<string> EnumerateRange(string range)
        {
            var bounds = range.Split("-");
            var lowerBound = long.Parse(bounds[0]);
            var upperBound = long.Parse(bounds[1]);

            var enumerated = new long[upperBound - lowerBound + 1];

            for (int i = 0; i < enumerated.Length; i++)
            {
                enumerated[i] = lowerBound + i;
            }

            return enumerated.Select(n => n.ToString());
        }

    }
}
