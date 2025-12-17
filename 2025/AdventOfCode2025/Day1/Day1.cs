using AdventOfCode2025.Services;

namespace AdventOfCode2025.Day1
{
    public class Day1 : IDay<int>
    {
        private readonly IReaderService _reader;        

        public Day1(IReaderService reader)
        {
            _reader = reader;
        }

        public int SolvePart1(string inputPath)
        {
            var zeroCount = 0;

            _reader
                .ReadLinesAsStrings(inputPath)
                .Select(EncodeToDistance)
                .Aggregate(50, (position, distance) =>
                {
                    // NB: % here is remainder NOT modulo. Corrected in SolvePart2.
                    var newPosition = (position + distance) % 100;
                    
                    if (newPosition == 0)
                    {
                        zeroCount++;
                    }

                    return newPosition;
                });

            return zeroCount;
        }

        public int SolvePart2(string inputPath)
        {
            var zeroCount = 0;

            _reader
                .ReadLinesAsStrings(inputPath)
                .Select(EncodeToDistance)
                .Aggregate(50, (position, distance) =>
                {
                    var newPosition = MathHelpers.Mod(position + distance, 100);
                    var absoluteDistance = Math.Abs(distance);

                    if (absoluteDistance >= 100)
                    {
                        // Each hundredth goes past 0
                        var fullRotationsCount = absoluteDistance / 100;
                        zeroCount += fullRotationsCount;
                    }

                    if (newPosition == 0)
                    {
                        zeroCount++;
                    }
                    else if (newPosition < position && distance > 0)
                    {
                        // Went over 0 going right
                        zeroCount++;
                    }
                    else if (newPosition > position && position != 0 && distance < 0)
                    {
                        // Went over 0 going left
                        zeroCount++;
                    }

                    return newPosition;
                });

            return zeroCount;
        }

        private int EncodeToDistance(string rotation)
        {
            var direction = rotation[0];
            var distance = int.Parse(rotation[1..]!);

            return direction == 'L' ? -distance : distance;
        }
    }
}
