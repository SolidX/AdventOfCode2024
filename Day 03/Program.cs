using System.Text.RegularExpressions;

namespace Day_03
{
    internal class Program
    {
        private static Regex mulInstrRegex = new Regex("mul\\((?<Operand1>\\d{1,3}),(?<Operand2>\\d{1,3})\\)", RegexOptions.Compiled);

        static void Main(string[] args)
        {
            WriteHeader(3);

            var rawInput = LoadInput();
            
            Part1(rawInput);
            Part2(rawInput);
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
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var totalMultiplications = 0l;
            foreach (var line in rawInput)
            {
                var matches = mulInstrRegex.Matches(line);
                foreach (Match m in matches)
                {
                    totalMultiplications += Int32.Parse(m.Groups[1].Value) * Int32.Parse(m.Groups[2].Value);
                }
            }

            Console.WriteLine($"Total Multiplications: {totalMultiplications}");
            Console.WriteLine();
        }

        static void Part2(string[] rawInput)
        {
            var processingRegex = new Regex("(do|don't)\\(\\)", RegexOptions.Compiled);
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var directive = true;
            var totalMultiplications = 0l;
            var processingDirectives = new Dictionary<int, bool>() { { 0, true } };

            foreach (var line in rawInput)
            {
                var pMatches = processingRegex.Matches(line);
                foreach (Match m in pMatches)
                    processingDirectives.Add(m.Index, m.Value.Equals("do()", StringComparison.OrdinalIgnoreCase));

                var iMatches = mulInstrRegex.Matches(line);                
                foreach (Match m in iMatches)
                {                    
                    var op1 = Int32.Parse(m.Groups["Operand1"].Value);
                    var op2 = Int32.Parse(m.Groups["Operand2"].Value);

                    var mostRecentDirective = processingDirectives.Keys.Where(idx => idx <= m.Index).Max();
                    directive = processingDirectives[mostRecentDirective];
                    if (directive)
                        totalMultiplications += op1 * op2;
                }

                processingDirectives.Clear();
                processingDirectives.Add(0, directive);
            }

            Console.WriteLine($"Total Multiplications: {totalMultiplications}");
            Console.WriteLine();
        }
    }
}
