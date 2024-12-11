using System.Diagnostics.CodeAnalysis;

namespace Day_06
{
    internal class PositionComparer : IEqualityComparer<Position>
    {
        public bool Equals(Position? p, Position? q)
        {
            if (p == null && q == null)
                return true;
            if (p != null && q != null && p.X == q.X && p.Y == q.Y)
                return true;
            return false;
        }

        public int GetHashCode([DisallowNull] Position obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
