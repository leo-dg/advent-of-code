using AdventOfCode2025.Services;

namespace AdventOfCode2025.Day5
{
    public class Day5 : IDay<ulong>
    {
        private struct Range<T>
        {
            public T Min;
            public T Max;
        }

        private readonly IReaderService _reader;

        public Day5(IReaderService reader)
        {
            _reader = reader;
        }

        public ulong SolvePart1(string inputPath)
        {
            var input = _reader.ReadLinesAsStrings(inputPath);

            var ranges = input
                .TakeWhile(e => e.Length > 0)
                .Select(ToRange)
                .OrderBy(r => r.Min);

            var ids = input
                .Skip(ranges.Count() + 1)
                .Select(ulong.Parse);

            var freshCount = 0;

            foreach (var id in ids)
            {
                foreach (var range in ranges)
                {
                    if (id > range.Max)
                    {
                        continue;
                    }

                    if (id >= range.Min && id <= range.Max)
                    {
                        freshCount++;
                        break;
                    }
                }
            }

            return (ulong) freshCount;
        }

        public ulong SolvePart2(string inputPath)
        {
            var input = _reader.ReadLinesAsStrings(inputPath);

            var ranges = input
                .TakeWhile(e => e.Length > 0)
                .Select(ToRange)
                .OrderBy(r => r.Min)
                .ToArray();

            /**
             * given a range A and a range B where A.max > B.min
             * - if A.max > B.max then range B can be ignored
             * - if A.max <= B.max then the ranges can be joined such that the new range is [A.min, B.max]
             * - else they're disjointed
             * 
             * Since the ranges are ordered by min, we can stop iterating once A.max < B.min
             */

            var newRanges = new List<Range<ulong>>();

            for (var i = 0; i < ranges.Length; i++)
            {
                var range = ranges[i];
                var lastMergedRangeIndex = -1;

                for (var j = i + 1; j < ranges.Length; j++)
                {
                    var rangeAhead = ranges[j];

                    if (range.Max < rangeAhead.Min)
                    {
                        break;
                    }

                    if (range.Max < rangeAhead.Max)
                    {
                        range = new Range<ulong> { Min = range.Min, Max = rangeAhead.Max };
                    }

                    lastMergedRangeIndex = j;
                }

                if (lastMergedRangeIndex != -1)
                {
                    i = lastMergedRangeIndex; // (will be skipped because of i++)
                }

                newRanges.Add(range);
            }

            var total = newRanges
                .Select(r => r.Max - r.Min + 1)
                .Aggregate((ulong)0, (sum, current) => sum + current);

            return total;
        }

        private Range<ulong> ToRange(string rangeString)
        {
            var minMax = rangeString.Split("-").Select(ulong.Parse);

            return new Range<ulong>{ Min = minMax.First(), Max = minMax.Last() };
        }
    }
}
