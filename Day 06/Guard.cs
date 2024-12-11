namespace Day_06
{
    internal class Guard
    {
        public Position Position { get; set; }
        public GuardDirectionEnum Direction { get; set; }

        public Guard()
        {
            Position = new Position(0, 0);
            Direction = GuardDirectionEnum.Up;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        /// <returns>Path travelled</returns>
        public List<Position> Navigate(string[] map)
        {
            var start = Position;
            var visitedLocations = new List<Position>() { start };
            
            do
            {
                Position nextLocation = start;
                switch (Direction)
                {
                    case GuardDirectionEnum.Up:
                        nextLocation = start.Up(1);
                        break;
                    case GuardDirectionEnum.Right:
                        nextLocation = start.Right(1);
                        break;
                    case GuardDirectionEnum.Down:
                        nextLocation = start.Down(1);
                        break;
                    case GuardDirectionEnum.Left:
                        nextLocation = start.Left(1);
                        break;
                }

                if (!nextLocation.IsValid(map))
                    break;

                if (map[nextLocation.Y][nextLocation.X] == '#')
                {
                    Direction = (GuardDirectionEnum)(((int)Direction + 1) % 4); //Turn right 90 degrees
                    continue;
                }

                //Advance
                visitedLocations.Add(nextLocation);
                start = nextLocation;
            }
            while (true);

            return visitedLocations;
        }
    }
}
