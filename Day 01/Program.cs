namespace Day_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteHeader(1);

            var rawInput = LoadInput();
            var leftList = new List<int>(rawInput.Length);
            var rightList = new List<int>(rawInput.Length);

            ParseInput(rawInput, leftList, rightList);
            Part1(leftList, rightList);
        }
        static void WriteHeader(int day)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day {0:D2} |", day);
            Console.WriteLine("+==========================+");
        }

        static string[] LoadInput()
        {
            if (File.Exists("input"))
            {
                return File.ReadAllLines("input");
            }
            else
            {
                return [];
            }
        }

        static void ParseInput(string[] rawiInput, List<int> leftList, List<int> rightList)
        {
            foreach (var line in rawiInput)
            {
                var locationIds = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                
                if (locationIds.Length == 2)
                {
                    leftList.Add(Int32.Parse(locationIds[0]));
                    rightList.Add(Int32.Parse(locationIds[1]));
                }
                else
                {
                    throw new Exception($"Unexpected number of Locations IDs found in input: '{line}'");
                }
            }
        }

        static void Part1(List<int> leftList, List<int> rightList)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();
            var leftSorted = leftList.Order().ToList();
            var rightSorted = rightList.Order().ToList();
            var totalDistance = 0;

            for (int i = 0; i < leftSorted.Count; i++)
            {
                totalDistance += Math.Abs(rightSorted[i] - leftSorted[i]);
            }

            Console.WriteLine($"Total Distance: {totalDistance}");
            Console.WriteLine();
        }
    }
}
