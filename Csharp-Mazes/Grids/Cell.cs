namespace Csharp_Mazes.Grids
{
    public class Cell
    {
        public bool HasNorthWall { get; set; } = true;
        public bool HasSouthWall { get; set; } = true;
        public bool HasEastWall { get; set; } = true;
        public bool HasWestWall { get; set; } = true;
        public bool IsInMaze { get; set; } = false;

        public override bool Equals(object other)
        {
            var castCell = other as Cell;
            if (castCell  != null &&
                HasNorthWall == castCell.HasNorthWall &&
                HasSouthWall == castCell.HasSouthWall &&
                HasEastWall  == castCell.HasEastWall &&
                HasWestWall  == castCell.HasWestWall &&
                IsInMaze  == castCell.IsInMaze)
            {
                return true;
            }

            return false;
        }
    }
}
