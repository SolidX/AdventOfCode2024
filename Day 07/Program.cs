using System.Collections.Concurrent;

namespace Day_07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteHeader(7);

            var rawInput = LoadInput();
            var parsedInput = ParseInput(rawInput);
            Part1(parsedInput);
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

        static List<Equation> ParseInput(string[] rawInput)
        {
            var equations = new ConcurrentBag<Equation>();
            Parallel.ForEach(rawInput, (input, i) =>
            {
                var vals = input.Split(':', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var testVal = Int64.Parse(vals[0]);
                var operands = vals[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse);
                equations.Add(new Equation(testVal, operands));
            });
            return equations.ToList();
        }

        static void Part1(List<Equation> calibrations)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var total = calibrations.Where(x => x.Passes()).Sum(x => x.TestValue);

            Console.WriteLine($"Total Calibration Result: {total}");
            Console.WriteLine();
        }
    }
}
