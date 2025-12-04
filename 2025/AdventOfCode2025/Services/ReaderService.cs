namespace AdventOfCode2025.Services
{
    public class ReaderService : IReaderService
    {
        public string[] ReadLinesAsStrings(string path)
        {
            var lines = new List<string>();

            using var reader = new StreamReader(path);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (line == null)
                {
                    break;
                }

                lines.Add(line);
            }

            return lines.ToArray();
        }
    }
}
