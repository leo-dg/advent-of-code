using AdventOfCode2025.Services;
using System.Diagnostics;

namespace AdventOfCode2025.Day4
{
    public class Day4 : IDay<int>
    {
        private struct Coord
        {
            public int X;
            public int Y;
        }

        private const char ROLL_TOKEN = '@';
        private const char FORKLIFTED_TOKEN = 'x';

        private readonly IReaderService _reader;

        public Day4(IReaderService reader)
        {
            _reader = reader;
        }

        public int SolvePart1(string inputPath)
        {
            var locations = _reader
                .ReadLinesAsStrings(inputPath)
                .ToArray();

            return FindForkliftablePositions(locations).Length;
        }

        public int SolvePart2(string inputPath)
        {
            var locations = _reader
                .ReadLinesAsStrings(inputPath)
                .ToArray();

            var locationState = new string[locations.Length];
            locations.CopyTo(locationState, 0);

            var forkliftable = 0;

            var canForklift = true;

            while (canForklift)
            {
                var forkliftablePositions = FindForkliftablePositions(locationState);

                if (forkliftablePositions.Length == 0)
                {
                    break;
                }

                forkliftable += forkliftablePositions.Length;

                foreach (var p in forkliftablePositions)
                {
                    var lineToUpdate = locationState[p.Y];

                    if (p.X == 0)
                    {
                        locationState[p.Y] = FORKLIFTED_TOKEN + lineToUpdate[1..];
                    }
                    else if (p.X == lineToUpdate.Length - 1)
                    {
                        locationState[p.Y] = lineToUpdate[..(lineToUpdate.Length - 1)] + FORKLIFTED_TOKEN;
                    }
                    else
                    {
                        locationState[p.Y] = lineToUpdate[..p.X] + FORKLIFTED_TOKEN + lineToUpdate[(p.X + 1)..];
                    }
                }
            }

            return forkliftable;
        }

        private Coord[] FindForkliftablePositions(string[] locations)
        {
            var forkliftableRollPositions = new List<Coord>();

            for (var i = 0; i < locations.Length; i++)
            {
                for (var j = 0; j < locations[i].Length; j++)
                {
                    var currentCoord = new Coord { X = j, Y = i };

                    if (!IsRoll(currentCoord, locations))
                    {
                        continue;
                    }

                    var adjacentCoords = GetAdjacentCoords(currentCoord, locations);
                    var adjacentRolls = CountRolls(adjacentCoords, locations);

                    if (adjacentRolls < 4)
                    {
                        forkliftableRollPositions.Add(currentCoord);
                    }
                }
            }

            return forkliftableRollPositions.ToArray();
        }

        private Coord[] GetAdjacentCoords(Coord p, string[] lines)
        {
            /**
             * Cases
             * top left char -  X=0             Y=0
             * top chars -      0<X<Len(line)-1 Y=0
             * top right char - X=Len(line)-1   Y=0
             * left chars -     X=0             0<Y<Len(locations)-1
             * right chars -    X=Len(line)-1   0<Y<Len(locations)-1
             * bot left char -  X=0             Y=Len(line)-1
             * bot right char - X=Len(line)-1   Y=Len(line)-1
             * bot chars -      0<X<Len(line)-1 Y=Len(line)-1
             */

            Debug.Assert(lines.Length > 1);

            var line = lines[p.Y];

            Debug.Assert(line.Length > 1);

            if (p.X == 0)
            {
                // Start of line
                if (p.Y == 0)
                {
                    // Top left char
                    return
                    [
                        new() { X = p.X + 1, Y = p.Y },
                        new() { X = p.X,     Y = p.Y + 1 },
                        new() { X = p.X + 1, Y = p.Y + 1 }
                    ];
                }
                else if (p.Y == lines.Length - 1)
                {
                    // Bottom left char
                    return
                    [
                        new() { X = p.X,     Y = p.Y - 1 },
                        new() { X = p.X + 1, Y = p.Y - 1 },
                        new() { X = p.X + 1, Y = p.Y }
                    ];
                }

                // In between top and bottom lines
                return
                [
                    new() { X = p.X,     Y = p.Y - 1 },
                    new() { X = p.X + 1, Y = p.Y - 1 },
                    new() { X = p.X + 1, Y = p.Y },
                    new() { X = p.X,     Y = p.Y + 1 },
                    new() { X = p.X + 1, Y = p.Y + 1 }
                ];
            }
            else if (p.X == line.Length - 1)
            {
                // End of line
                if (p.Y == 0)
                {
                    // Top right char
                    return
                    [
                        new() { X = p.X - 1, Y = p.Y },
                        new() { X = p.X - 1, Y = p.Y + 1 },
                        new() { X = p.X,     Y = p.Y + 1 }
                    ];
                }
                else if (p.Y == lines.Length - 1)
                {
                    // Bottom right char
                    return
                    [
                        new() { X = p.X - 1, Y = p.Y - 1 },
                        new() { X = p.X,     Y = p.Y - 1 },
                        new() { X = p.X - 1, Y = p.Y }
                    ];
                }

                // In between top and bottom lines
                return
                [
                    new() { X = p.X - 1, Y = p.Y - 1 },
                    new() { X = p.X,     Y = p.Y - 1 },
                    new() { X = p.X - 1, Y = p.Y },
                    new() { X = p.X - 1, Y = p.Y + 1 },
                    new() { X = p.X,     Y = p.Y + 1 }
                ];
            }

            // In between start and end of line

            if (p.Y == 0)
            {
                return
                [
                    new() { X = p.X - 1, Y = p.Y },
                    new() { X = p.X + 1, Y = p.Y },
                    new() { X = p.X - 1, Y = p.Y + 1 },
                    new() { X = p.X,     Y = p.Y + 1 },
                    new() { X = p.X + 1, Y = p.Y + 1 },
                ];
            }
            else if (p.Y == lines.Length - 1)
            {
                return
                [
                    new() { X = p.X - 1, Y = p.Y - 1 },
                    new() { X = p.X,     Y = p.Y - 1 },
                    new() { X = p.X + 1, Y = p.Y - 1 },
                    new() { X = p.X - 1, Y = p.Y },
                    new() { X = p.X + 1, Y = p.Y },
                ];
            }

            return
            [
                new() { X = p.X - 1, Y = p.Y - 1 },
                new() { X = p.X,     Y = p.Y - 1 },
                new() { X = p.X + 1, Y = p.Y - 1 },
                new() { X = p.X - 1, Y = p.Y },
                new() { X = p.X + 1, Y = p.Y },
                new() { X = p.X - 1, Y = p.Y + 1 },
                new() { X = p.X,     Y = p.Y + 1 },
                new() { X = p.X + 1, Y = p.Y + 1 },
            ];
            
        }

        private int CountRolls(Coord[] positions, string[] lines)
            => positions
                .Select(p => IsRoll(p, lines))
                .Where(isRoll => isRoll)
                .Count();

        private bool IsRoll(Coord p, string[] lines)
            => lines[p.Y][p.X] == ROLL_TOKEN;
    }
}
 