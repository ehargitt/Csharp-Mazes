using System;
using Csharp_Mazes.Grids;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeTests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void BreakWalls_MatchPattern()
        {
            // Arrange
            var knownGoodMatrix = new Cell[3, 3];
            Cell SWCell = new Cell
            {
                HasNorthWall = false,
                HasSouthWall = true,
                HasEastWall = false,
                HasWestWall = true
            };

            Cell SCell = new Cell
            {
                HasNorthWall = false,
                HasSouthWall = true,
                HasEastWall = false,
                HasWestWall = false
            };

            Cell SECell = new Cell
            {
                HasNorthWall = false,
                HasSouthWall = true,
                HasEastWall = true,
                HasWestWall = false
            };

            Cell WCell = new Cell
            {
                HasNorthWall = false,
                HasSouthWall = false,
                HasEastWall = false,
                HasWestWall = true
            };

            Cell CenterCell = new Cell
            {
                HasNorthWall = false,
                HasSouthWall = false,
                HasEastWall = false,
                HasWestWall = false
            };

            Cell ECell = new Cell
            {
                HasNorthWall = false,
                HasSouthWall = false,
                HasEastWall = true,
                HasWestWall = false
            };

            Cell NWCell = new Cell
            {
                HasNorthWall = true,
                HasSouthWall = false,
                HasEastWall = false,
                HasWestWall = true
            };

            Cell NCell = new Cell
            {
                HasNorthWall = true,
                HasSouthWall = false,
                HasEastWall = false,
                HasWestWall = false
            };

            Cell NECell = new Cell
            {
                HasNorthWall = true,
                HasSouthWall = false,
                HasEastWall = true,
                HasWestWall = false
            };

            knownGoodMatrix[0, 0] = SWCell;
            knownGoodMatrix[1, 0] = SCell;
            knownGoodMatrix[2, 0] = SECell;
            knownGoodMatrix[0, 1] = WCell;
            knownGoodMatrix[1, 1] = CenterCell;
            knownGoodMatrix[2, 1] = ECell;
            knownGoodMatrix[0, 2] = NWCell;
            knownGoodMatrix[1, 2] = NCell;
            knownGoodMatrix[2, 2] = NECell;

            var target = new Grid(3, 3);

            // Act
            target.BreakWall(0,0,GridDirection.East);
            target.BreakWall(0, 0, GridDirection.North);
            target.BreakWall(0, 1, GridDirection.East);
            target.BreakWall(0, 1, GridDirection.North);
            target.BreakWall(0, 2, GridDirection.East);

            target.BreakWall(2,0, GridDirection.West);
            target.BreakWall(2, 0, GridDirection.North);
            target.BreakWall(2, 1, GridDirection.West);
            target.BreakWall(2, 1, GridDirection.North);
            target.BreakWall(2, 2, GridDirection.West);

            target.BreakWall(1, 2, GridDirection.South);
            target.BreakWall(1, 0, GridDirection.North);

            // Assert
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    Assert.AreEqual(knownGoodMatrix[i, j], target.Matrix[i, j], $"Cell at [{i},{j}] doesn't match.");
                }
            }
        }
    }
}
