namespace Day_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteHeader(4);

            var puzzle = LoadInput(); //.ToCharArray();
            Part1(puzzle);
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

        static IEnumerable<Tuple<int, int>> GetSurroundingCoordinates(int row, int col, string[] grid)
        {
            var maxRow = grid.Length - 1;
            var maxCol = grid[row].Length - 1;

            for (var y = Math.Max(0, row - 1); y <= Math.Min(row + 1, maxRow); y++)
            {
                for (var x = Math.Max(0, col - 1); x <= Math.Min(col + 1, maxCol); x++)
                {
                    if (y == row && x == col)
                        continue;

                    yield return Tuple.Create(x, y);
                }
            }
        }

        static bool IsSafeAddress(int val, int min, int max)
        {
            return val == Math.Min(Math.Max(0, val), max);
        }

        static void Part1(string[] puzzle)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var maxY = puzzle.Length - 1;
            var maxX = puzzle[0].Length - 1;
            var xmasCount = 0;

            for (var y = 0; y <= maxY; y++)
            {
                for (var x = 0; x <= maxX; x++)
                {
                    if (puzzle[y][x] != 'X')
                        continue;

                    var neighbors_m = GetSurroundingCoordinates(y, x, puzzle).Where(coords => puzzle[coords.Item2][coords.Item1] == 'M').ToList();
                    foreach (var neighbor in neighbors_m)
                    {
                        var xSlope = neighbor.Item1 - x;
                        var ySlope = neighbor.Item2 - y;

                        var potentialA_X = neighbor.Item1 + xSlope;
                        var potentialA_Y = neighbor.Item2 + ySlope;

                        if (IsSafeAddress(potentialA_Y, 0, maxY) && IsSafeAddress(potentialA_X, 0, maxX) && puzzle[potentialA_Y][potentialA_X] == 'A')
                        {
                            var potentialS_X = neighbor.Item1 + (2 * xSlope);
                            var potentialS_Y = neighbor.Item2 + (2 * ySlope);

                            if (IsSafeAddress(potentialS_Y, 0, maxY) && IsSafeAddress(potentialS_X, 0, maxX) && puzzle[potentialS_Y][potentialS_X] == 'S')
                                xmasCount++;
                        }
                    }
                }
            }

            Console.WriteLine($"'XMAS'-es Found: {xmasCount}");
            Console.WriteLine();
        }
    }
}
