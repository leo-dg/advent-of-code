namespace AdventOfCode2025
{
    public static class MathHelpers
    {
        public static int Mod(int a, int m)
        {
            var r = a % m;

            return r < 0 ? r + m : r;
        }
    }
}
