using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Csharp_Mazes.Grids
{
    public class Grid
    {
        private readonly int _sizeX;
        private readonly int _sizeZ;

        public Grid(int sizeX, int sizeZ)
        {
            _sizeX = sizeX;
            _sizeZ = sizeZ;
            Matrix = new Cell[_sizeX, _sizeZ];

            for (int i = 0; i < _sizeX; ++i)
            {
                for (int j = 0; j < _sizeZ; ++j)
                {
                    Matrix[i, j] = new Cell();
                }
            }
        }

        public void BreakWall(int cellLocationX, int cellLocationZ, GridDirection direction)
        {
            if (cellLocationX < 0 || cellLocationX >= _sizeX)
            {
                return;
            }

            if (cellLocationZ < 0 || cellLocationZ >= _sizeZ)
            {
                return;
            }

            switch (direction)
            {
                case GridDirection.North:
                    if (!(cellLocationZ + 1 > _sizeZ - 1))
                    {
                        Matrix[cellLocationX, cellLocationZ].HasNorthWall = false;
                        Matrix[cellLocationX, cellLocationZ + 1].HasSouthWall = false;
                    }
                    break;
                case GridDirection.South:
                    if (!(cellLocationZ - 1 < 0))
                    {
                        Matrix[cellLocationX, cellLocationZ].HasSouthWall = false;
                        Matrix[cellLocationX, cellLocationZ - 1].HasNorthWall = false;
                    }
                    break;
                case GridDirection.East:
                    if (!(cellLocationX + 1 >_sizeX - 1))
                    {
                        Matrix[cellLocationX, cellLocationZ].HasEastWall = false;
                        Matrix[cellLocationX + 1, cellLocationZ].HasWestWall = false;
                    }
                    break;
                case GridDirection.West:
                    if (!(cellLocationX - 1 < 0))
                    {
                        Matrix[cellLocationX, cellLocationZ].HasWestWall = false;
                        Matrix[cellLocationX - 1, cellLocationZ].HasEastWall = false;
                    }
                    break;
                default:
                    break;
            }
        }

        public Cell[,] Matrix { get; private set; }
    }
}
