namespace Day_06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteHeader(6);

            var map = LoadInput();
            Part1(map);
        }

        static void WriteHeader(int day)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day {0:D2} |", day);
            Console.WriteLine("+==========================+");
        }

        static string[] LoadInput()
        {
            if (File.Exists("input.txt"))
            {
                return File.ReadAllLines("input.txt");
            }
            else
            {
                return [];
            }
        }

        static void Part1(string[] map)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var guard = new Guard();

            //Locate Guard
            foreach (var row in map)
            {
                if (row.Contains('^'))
                {
                    guard.Position.X = row.IndexOf('^');
                    break;
                }
                guard.Position.Y++;
            }

            //Navigate map
            var pathTraveled = guard.Navigate(map);
            var distinctPositions = pathTraveled.Distinct(new PositionComparer()).Count();

            Console.WriteLine($"Distinct positions visted: {distinctPositions}");
            Console.WriteLine();
        }
    }
}
