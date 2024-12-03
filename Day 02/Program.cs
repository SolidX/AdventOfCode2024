namespace Day_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteHeader(2);

            var rawInput = LoadInput();
            var reports = ParseInput(rawInput);

            Part1(reports);
            Part2(reports);
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

        static List<int[]> ParseInput(string[] rawInput)
        {
            var reports = new List<int[]>(rawInput.Length);
            foreach (var line in rawInput)
            {
                var report = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                reports.Add(report.Select(Int32.Parse).ToArray());
            }
            return reports;
        }

        static void Part1(List<int[]> reports)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var reportSafety = new Dictionary<int, bool>();
            for (var i = 0; i < reports.Count; i++)
                reportSafety.Add(i, IsSafe(reports[i]));

            var safeReports = reportSafety.Count(x => x.Value);

            Console.WriteLine($"Safe Reports: {safeReports}");
            Console.WriteLine();
        }

        static bool IsSafe(int[] report)
        {
            int prev;
            int curr;
            bool? trend = null;

            for (int j = 1; j < report.Length; j++)
            {
                prev = report[j - 1];
                curr = report[j];
                var diff = curr - prev;

                //Any two adjacent levels differ by at least one and at most three.
                if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
                    return false;

                bool? currTrend;
                if (diff > 0)
                    currTrend = true; //Increasing
                else
                {
                    if (diff < 0)
                        currTrend = false; //Decreasing
                    else
                        currTrend = null; //Steady
                }

                if (j == 1)
                    trend = currTrend;

                //The levels are either all increasing or all decreasing.
                if (trend != currTrend)
                    return false;
            }
            
            return true;
        }       

        static IEnumerable<int[]> GetDampenedReport(int[] report)
        {
            for (var i = 0; i < report.Length; i++)
            {
                var dampenedReport = new int[report.Length - 1];

                for (var j = 0; j < report.Length; j++)
                {
                    if (i == j)
                        continue;
                    if (j < i)
                        dampenedReport[j] = report[j];
                    else
                        dampenedReport[j - 1] = report[j];
                }

                yield return dampenedReport;
            }
        }

        static bool IsSafeWithDampening(int[] report)
        {
            if (IsSafe(report))
                return true;

            var dampenedReports = GetDampenedReport(report);
            return dampenedReports.Any(IsSafe);
        }

        static void Part2(List<int[]> reports)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var reportSafety = new Dictionary<int, bool>();
            for (var i = 0; i < reports.Count; i++)
                reportSafety.Add(i, IsSafeWithDampening(reports[i]));

            var safeReports = reportSafety.Count(x => x.Value);

            Console.WriteLine($"Safe Reports (with dampening): {safeReports}");
            Console.WriteLine();
        }
    }
}
