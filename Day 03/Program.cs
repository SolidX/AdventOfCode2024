using System.Text.RegularExpressions;

namespace Day_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteHeader(3);

            var rawInput = LoadInput();
            
            Part1(rawInput);
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

        static void Part1(string[] rawInput)
        {
            var instructionRegex = new Regex("mul\\((\\d{1,3}),(\\d{1,3})\\)", RegexOptions.Compiled);
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var totalMultiplications = 0l;
            foreach (var line in rawInput)
            {
                var matches = instructionRegex.Matches(line);
                foreach (Match m in matches)
                {
                    totalMultiplications += Int32.Parse(m.Groups[1].Value) * Int32.Parse(m.Groups[2].Value);
                }
            }

            Console.WriteLine($"Total Multiplications: {totalMultiplications}");
            Console.WriteLine();
        }
    }
}
