namespace AdventOfCode2025
{
    public interface IDay<T>
    {
        public T SolvePart1(string inputPath);

        public T SolvePart2(string inputPath);
    }
}
