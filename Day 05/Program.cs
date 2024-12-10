namespace Day_05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteHeader(5);

            var rawInput = LoadInput();
            var orderingRules = new Dictionary<int, HashSet<int>>();
            var updates = new List<List<int>>();

            ParseInput(rawInput, orderingRules, updates);
            Part1(orderingRules, updates);
            Part2(orderingRules, updates);
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

        static void ParseInput(string[] rawInput, Dictionary<int, HashSet<int>> orderingRules, List<List<int>> updates)
        {
            var splitOpts = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

            foreach (string line in rawInput)
            {
                if (String.IsNullOrWhiteSpace(line))
                    continue;

                if (line.Contains('|'))
                {
                    var order = line.Split('|', 2, splitOpts).Select(Int32.Parse).ToArray();
                    
                    if (orderingRules.ContainsKey(order[0]))
                        orderingRules[order[0]].Add(order[1]);
                    else
                        orderingRules.Add(order[0], new HashSet<int> { order[1] });
                }
                else
                {
                    updates.Add(line.Split(',', splitOpts).Select(Int32.Parse).ToList());
                }
            }
        }

        static bool InCorrectOrder(Dictionary<int, HashSet<int>> orderingRules, List<int> update)
        {
            for (int i = 0; i < update.Count; i++)
            {
                var page = update[i];
                //If an update includes both page numbers in an ordering rule
                if (orderingRules.ContainsKey(page))
                {
                    var pagesThatComeAfter = update.Intersect(orderingRules[page]).ToHashSet();

                    //Then for ordering rule X|Y, Y must come after X in the update
                    if (pagesThatComeAfter.All(pg => update.FindIndex(i, p => p == pg) != -1))
                        continue;
                    else
                        return false;
                }
            }

            return true;
        }

        static void Part1(Dictionary<int, HashSet<int>> orderingRules, List<List<int>> updates)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var middlePageSum = 0;
            foreach (var u in updates)
            {
                if (InCorrectOrder(orderingRules, u))
                    middlePageSum += u[u.Count / 2];
            }

            Console.WriteLine($"Sum of correctly-ordered middle page numbers: {middlePageSum}");
            Console.WriteLine();
        }

        static List<int> OrderPages(Dictionary<int, HashSet<int>> orderingRules, List<int> update)
        {
            if (InCorrectOrder(orderingRules, update))
                return update;

            for (var i = 0; i < update.Count; i++)
            {
                if (orderingRules.ContainsKey(update[i]))
                {
                    var mustComeAfter = update.Intersect(orderingRules[update[i]]).ToHashSet();
                    
                    if (!mustComeAfter.Any())
                        continue;

                    var positionToUse = mustComeAfter.Select(pg => update.FindIndex(0, p => p == pg)).Min();

                    if (positionToUse < i)
                    {
                        var fixedUpdate = new List<int>();
                        fixedUpdate.AddRange(update);
                        fixedUpdate.RemoveAt(i);
                        fixedUpdate.Insert(positionToUse, update[i]);

                        return OrderPages(orderingRules, fixedUpdate);
                    }
                }
            }

            return new List<int>();
        }

        static void Part2(Dictionary<int, HashSet<int>> orderingRules, List<List<int>> updates)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var middlePageSum = 0;
            foreach (var u in updates)
            {
                if (!InCorrectOrder(orderingRules, u))
                {
                    var fixedUpdate = OrderPages(orderingRules, u);
                    middlePageSum += fixedUpdate[u.Count / 2];
                }
                    
            }

            Console.WriteLine($"Sum of correctly-ordered middle page numbers: {middlePageSum}");
            Console.WriteLine();
        }
    }
}
