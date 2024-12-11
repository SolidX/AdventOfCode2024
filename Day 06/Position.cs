
namespace Day_06
{
    internal class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position Up(int distance)
        {
            return new Position(X, Y - distance);
        }
        public Position Down(int distance)
        {
            return new Position(X, Y + distance);
        }
        public Position Left(int distance)
        {
            return new Position(X - distance, Y);
        }
        public Position Right(int distance)
        {
            return new Position(X + distance, Y);
        }

        public bool IsValid(string[] map)
        {
            return Y >= 0 && Y < map.Length && X >= 0 && X < map[Y].Length;
        }

        public override string ToString() => $"({X}, {Y})";
    }
}
