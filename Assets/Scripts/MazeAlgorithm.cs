using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeAlgorithm : Maze {
    public int currentRow = 0, currentColumn = 0;
    public bool isComplete = false;
    public bool notGeneratingRooms = false;

    public MazeAlgorithm(MazeCell[,] mazeCells, float size) : base(mazeCells,size) {
    }

    public override void CreateMaze()
    {
        HuntAndKill();
    }

    public void HuntAndKill()
    {
        mazeCells[currentRow, currentColumn].visited = true;
        while (!isComplete)
        {
            Kill(notGeneratingRooms,0,-1);// visit cells till dead end found
            Hunt();// find next unvisited cell
        }
    }
    public void Kill(bool generatingRooms,int roomId,int roomsize)
    {
        if (!generatingRooms)
        {
            while (PathsAvailable(currentRow, currentColumn))
            {
                int direction = Random.Range(1, 5);

                //North
                if (direction == 1 && CellFree(currentRow - 1, currentColumn))
                {
                    DestroyWallIfExists(mazeCells[currentRow, currentColumn].northWall);
                    if (!mazeCells[currentRow - 1, currentColumn].isRoom)
                    {
                        DestroyWallIfExists(mazeCells[currentRow - 1, currentColumn].southWall);
                    }
                    currentRow--;
                }
                //South
                else if (direction == 2 && CellFree(currentRow + 1, currentColumn))
                {
                    DestroyWallIfExists(mazeCells[currentRow, currentColumn].southWall);
                    if (!mazeCells[currentRow + 1, currentColumn].isRoom)
                    {
                        DestroyWallIfExists(mazeCells[currentRow + 1, currentColumn].northWall);
                    }
                    currentRow++;
                }
                //East
                else if (direction == 3 && CellFree(currentRow, currentColumn + 1))
                {
                    DestroyWallIfExists(mazeCells[currentRow, currentColumn].eastWall);
                    if (!mazeCells[currentRow, currentColumn + 1].isRoom)
                    {
                        DestroyWallIfExists(mazeCells[currentRow, currentColumn + 1].westWall);
                    }
                    currentColumn++;
                }
                //West
                else if (direction == 4 && CellFree(currentRow, currentColumn - 1))
                {
                    DestroyWallIfExists(mazeCells[currentRow, currentColumn].westWall);
                    if (!mazeCells[currentRow, currentColumn - 1].isRoom)
                    {
                        DestroyWallIfExists(mazeCells[currentRow, currentColumn - 1].eastWall);
                    }
                    currentColumn--;
                }

                mazeCells[currentRow, currentColumn].visited = true;
            }
        }
        else if (generatingRooms)
        {
            while (PathsAvailableForRoom(currentRow, currentColumn))
            {
                int direction = Random.Range(1, 5);

                //North
                if (direction == 1 && CellFree(currentRow - 1, currentColumn))
                {
                    DestroyWallIfExists(mazeCells[currentRow, currentColumn].northWall);
                    DestroyWallIfExists(mazeCells[currentRow - 1, currentColumn].southWall);
                    mazeCells[currentRow, currentColumn].roomId = roomId;
                    mazeCells[currentRow, currentColumn].isRoom = true;
                    currentRow--;
                    mazeCells[currentRow, currentColumn].roomId = roomId;
                }
                //South
                else if (direction == 2 && CellFree(currentRow + 1, currentColumn))
                {
                    DestroyWallIfExists(mazeCells[currentRow, currentColumn].southWall);
                    DestroyWallIfExists(mazeCells[currentRow + 1, currentColumn].northWall);
                    mazeCells[currentRow, currentColumn].roomId = roomId;
                    mazeCells[currentRow, currentColumn].isRoom = true;
                    currentRow++;
                    mazeCells[currentRow, currentColumn].roomId = roomId;
                }
                //East
                else if (direction == 3 && CellFree(currentRow, currentColumn + 1))
                {
                    DestroyWallIfExists(mazeCells[currentRow, currentColumn].eastWall);
                    DestroyWallIfExists(mazeCells[currentRow, currentColumn + 1].westWall);
                    mazeCells[currentRow, currentColumn].roomId = roomId;
                    mazeCells[currentRow, currentColumn].isRoom = true;
                    currentColumn++;
                    mazeCells[currentRow, currentColumn].roomId = roomId;
                }
                //West
                else if (direction == 4 && CellFree(currentRow, currentColumn - 1))
                {
                    DestroyWallIfExists(mazeCells[currentRow, currentColumn].westWall);
                    DestroyWallIfExists(mazeCells[currentRow, currentColumn - 1].eastWall);
                    mazeCells[currentRow, currentColumn].roomId = roomId;
                    mazeCells[currentRow, currentColumn].isRoom = true;
                    currentColumn--;
                    mazeCells[currentRow, currentColumn].roomId = roomId;
                }

                mazeCells[currentRow, currentColumn].visited = true;
                roomsize--;
            }
        }
    }
    public bool PathsAvailableForRoom(int row, int column)
    {
        int availablePaths = 0;
        if(row > 0 && !mazeCells[row - 1, column].visited)
        {
            availablePaths++;
        }
        if (row < rows-1 && !mazeCells[row + 1, column].visited)
        {
            availablePaths++;
        }
        if (column > 0 && !mazeCells[row, column - 1].visited)
        {
            availablePaths++;
        }
        if (column < columns-1 && !mazeCells[row, column + 1].visited)
        {
            availablePaths++;
        }

        if (availablePaths > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PathsAvailable(int row, int column)
    {
        int availablePaths = 0;
        if (row > 0 && !mazeCells[row - 1, column].visited && !mazeCells[row - 1, column].isRoom)
        {
            availablePaths++;
        }
        if (row < rows - 1 && !mazeCells[row + 1, column].visited && !mazeCells[row + 1, column].isRoom)
        {
            availablePaths++;
        }
        if (column > 0 && !mazeCells[row, column - 1].visited && !mazeCells[row, column - 1].isRoom)
        {
            availablePaths++;
        }
        if (column < columns - 1 && !mazeCells[row, column + 1].visited && !mazeCells[row, column + 1].isRoom)
        {
            availablePaths++;
        }

        if (availablePaths > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CellFree(int row,int column)
    {
        if(row >= 0 && row < rows && column >= 0 && column < columns && !mazeCells[row, column].visited && !mazeCells[row, column].isRoom)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void DestroyWallIfExists(GameObject wall)
    {
        if(wall != null)
        {
            GameObject.Destroy(wall);
        }
    }
    public void Hunt()
    {
        isComplete = true;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                if(!mazeCells[r,c].visited && VisitedAdjacentCell(r, c))
                {
                    isComplete = false;
                    currentRow = r;
                    currentColumn = c;
                    DestroyAdjacentWall(currentRow, currentColumn);
                    mazeCells[currentRow, currentColumn].visited = true;
                    return;
                }
            }
        }
    }

    public bool VisitedAdjacentCell(int row,int column)
    {

        int cellsVisited = 0;
        if (row > 0 && !mazeCells[row - 1, column].visited)
        {
            cellsVisited++;
        }
        if (row < rows - 2 && !mazeCells[row + 1, column].visited)
        {
            cellsVisited++;
        }
        if (column > 0 && !mazeCells[row, column - 1].visited)
        {
            cellsVisited++;
        }
        if (column < columns - 2 && !mazeCells[row, column + 1].visited)
        {
            cellsVisited++;
        }

        if (cellsVisited > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DestroyAdjacentWall(int row,int column)
    {
        bool wallDestroyed = false;

        while (!wallDestroyed)
        {
            int direction = Random.Range(1, 5);

            //North
            if (direction == 1 && row > 0 && mazeCells[row -1,column].visited && !mazeCells[row - 1, column].isRoom) // Check if room
            {
                DestroyWallIfExists(mazeCells[row, column].northWall);
                DestroyWallIfExists(mazeCells[row - 1, column].southWall);
                wallDestroyed = true;
            }
            //South
            else if (direction == 2 && row < (rows-2) && mazeCells[row + 1, column].visited && !mazeCells[row + 1, column].isRoom)
            {
                DestroyWallIfExists(mazeCells[row, column].southWall);
                DestroyWallIfExists(mazeCells[row + 1, column].northWall);
                wallDestroyed = true;
            }
            //East
            else if (direction == 3 && column < (columns-2) && mazeCells[row, column + 1].visited && !mazeCells[row, column + 1].isRoom)
            {
                DestroyWallIfExists(mazeCells[row, column].eastWall);
                DestroyWallIfExists(mazeCells[row, column + 1].westWall);
                wallDestroyed = true;
            }
            //West
            else if (direction == 4 && column > 0 && mazeCells[row, column - 1].visited && !mazeCells[row, column - 1].isRoom)
            {
                DestroyWallIfExists(mazeCells[row, column].westWall);
                DestroyWallIfExists(mazeCells[row, column - 1].eastWall);
                wallDestroyed = true;
            }
        }
    }
}
