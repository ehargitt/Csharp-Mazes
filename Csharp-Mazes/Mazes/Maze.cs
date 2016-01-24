using System;
using System.Collections.Generic;
using Csharp_Mazes.Grids;

namespace Csharp_Mazes.Mazes
{
    public class Maze
    {
        private Grid _grid;

        public Maze(int sizeX, int sizeZ, MazeCreationMethod method)
        {
            SizeX = sizeX;
            SizeZ = sizeZ;
            createMaze(method);
        }

        public int SizeX { get; }
        public int SizeZ { get; }
        public Cell[,] Matrix => _grid.Matrix;

        private void createMaze(MazeCreationMethod method)
        {
            switch (method)
            {
                case MazeCreationMethod.DFSBacktracking:
                    createMazeWithDFSBacktracking();
                    break;

                default:
                    break;
            }
        }

        private void createMazeWithDFSBacktracking()
        {
            _grid = new Grid(SizeX, SizeZ);
            Random rand = new Random();

            // Set up stack for DFS.
            var cellStack = new Stack<CellWithPosition>();

            // Need to track number of unvisited cells for DFS. Decrement each time we visit a new cell.
            int numOfUnvisitedCells = SizeX * SizeZ;

            // Pick a random initial cell as currentCell and mark as visited.
            var randX = rand.Next(0, SizeX);
            var randZ = rand.Next(0, SizeZ);
            var currentCell = new CellWithPosition
            {
                Cell = Matrix[randX, randZ],
                XPos = randX,
                ZPos = randZ
            };
            currentCell.Cell.IsInMaze = true;
            --numOfUnvisitedCells;

            while (numOfUnvisitedCells != 0)
            {
                // Choose randomly one of the unvisited neighbours, or check for the absence of any.
                var randomUnvisitedNeighborOfCurrent = chooseRandomUnvisitedNeighbor(currentCell.XPos, currentCell.ZPos);

                // Check if the current cell has any neighbours which have not been visited.
                if (randomUnvisitedNeighborOfCurrent.CellWithPosition.Cell != null)
                {
                    // Push current cell on to the stack.
                    cellStack.Push(currentCell);

                    // Remove the wall between the current cell and the chosen cell.
                    _grid.BreakWall(currentCell.XPos, currentCell.ZPos, randomUnvisitedNeighborOfCurrent.RelativeLocation);

                    // Make the chosen cell the current cell and mark it as visited.
                    currentCell = randomUnvisitedNeighborOfCurrent.CellWithPosition;
                    currentCell.Cell.IsInMaze = true;
                    --numOfUnvisitedCells;
                }
                else if (cellStack.Count != 0)
                {
                    // Pop a cell off stack and make it the current cell.
                    currentCell = cellStack.Pop();
                }
            }
        }

        private NeighborWithPosition chooseRandomUnvisitedNeighbor(int startX, int startZ)
        {
            var unvisitedNeighbors = new List<NeighborWithPosition>();

            // Check first if start cell has unvisited neighbors. Add them to the list so we can
            // choose randomly later.

            // North
            if (!(startZ + 1 > SizeZ - 1) && !Matrix[startX, startZ + 1].IsInMaze)
            {
                var northCell = new NeighborWithPosition
                {
                    CellWithPosition = new CellWithPosition
                    {
                        Cell = Matrix[startX, startZ + 1],
                        XPos = startX,
                        ZPos = startZ + 1
                    },
                    RelativeLocation = GridDirection.North
                };
                unvisitedNeighbors.Add(northCell);
            }

            // South
            if (!(startZ - 1 < 0) && !Matrix[startX, startZ - 1].IsInMaze)
            {
                var southCell = new NeighborWithPosition
                {
                    CellWithPosition = new CellWithPosition
                    {
                        Cell = Matrix[startX, startZ - 1],
                        XPos = startX,
                        ZPos = startZ - 1
                    },
                    RelativeLocation = GridDirection.South
                };
                unvisitedNeighbors.Add(southCell);
            }

            //East
            if (!(startX + 1 > SizeX - 1) && !Matrix[startX + 1, startZ].IsInMaze)
            {
                var eastCell = new NeighborWithPosition
                {
                    CellWithPosition = new CellWithPosition
                    {
                        Cell = Matrix[startX + 1, startZ],
                        XPos = startX + 1,
                        ZPos = startZ
                    },
                    RelativeLocation = GridDirection.East
                };
                unvisitedNeighbors.Add(eastCell);
            }

            // West
            if (!(startX - 1 < 0) && !Matrix[startX - 1, startZ].IsInMaze)
            {
                var westCell = new NeighborWithPosition
                {
                    CellWithPosition = new CellWithPosition
                    {
                        Cell = Matrix[startX - 1, startZ],
                        XPos = startX - 1,
                        ZPos = startZ
                    },
                    RelativeLocation = GridDirection.West
                };
                unvisitedNeighbors.Add(westCell);
            }

            if (unvisitedNeighbors.Count <= 0)
            {
                // No unvisited neighbors found.
                return new NeighborWithPosition
                {
                    CellWithPosition = new CellWithPosition
                    {
                        Cell = null,
                        XPos = -1,
                        ZPos = -1
                    },
                };
            }

            // Choose random index of list
            Random rand = new Random();
            var randIndex = rand.Next(0, unvisitedNeighbors.Count - 1);

            // Return randomly chosen unvisited neighbor.
            var unvisitedNeighbor = unvisitedNeighbors[randIndex];
            return unvisitedNeighbor;
        }

        private struct NeighborWithPosition
        {
            public CellWithPosition CellWithPosition { get; set; }
            public GridDirection RelativeLocation { get; set; }
        }

        private struct CellWithPosition
        {
            public Cell Cell { get; set; }
            public int XPos { get; set; }
            public int ZPos { get; set; }
        }
    }
}
