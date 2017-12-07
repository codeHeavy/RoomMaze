using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeAlgorithm : Maze {
    public int currentRow = 0, currentColumn = 0;
    public bool isDone = false; // to check if the algorithm is finished
    public bool notGeneratingRooms = false;

    public MazeAlgorithm(MazeCell[,] mazeCells, float size) : base(mazeCells,size) {
    }

    public override void CreateMaze()
    {
        HuntAndKill();
    }

    /// <summary>
    /// Carryout hunt and kill algorithm
    /// </summary>
    public void HuntAndKill()
    {
        mazeCells[currentRow, currentColumn].visited = true;
        while (!isDone)
        {
            Kill(notGeneratingRooms,0,-1);// visit cells till dead end found
            Hunt();// find next unvisited cell
        }
    }

    /// <summary>
    /// Pick a random direction and if cell in that direction isn't visited destroy wall leading to that cell
    /// if room generation is not being done then
    /// Check if the next cell is part of a room and if not make a path to it by destroying a wall in the opposite direction
    /// </summary>
    /// <param name="generatingRooms"></param>
    /// <param name="roomId"></param>
    /// <param name="roomsize"></param>
    public void Kill(bool generatingRooms,int roomId,int roomsize)
    {
        if (!generatingRooms)
        {
            while (PathsAvailable(currentRow, currentColumn))
            {
                int direction = Random.Range(1, 5);

                //North
                if (direction == 1 && CellAvailable(currentRow - 1, currentColumn))
                {
                    RemoveWall(mazeCells[currentRow, currentColumn].northWall);
                    if (!mazeCells[currentRow - 1, currentColumn].isRoom)
                    {
                        RemoveWall(mazeCells[currentRow - 1, currentColumn].southWall);
                    }
                    currentRow--;
                }
                //South
                else if (direction == 2 && CellAvailable(currentRow + 1, currentColumn))
                {
                    RemoveWall(mazeCells[currentRow, currentColumn].southWall);
                    if (!mazeCells[currentRow + 1, currentColumn].isRoom)
                    {
                        RemoveWall(mazeCells[currentRow + 1, currentColumn].northWall);
                    }
                    currentRow++;
                }
                //East
                else if (direction == 3 && CellAvailable(currentRow, currentColumn + 1))
                {
                    RemoveWall(mazeCells[currentRow, currentColumn].eastWall);
                    if (!mazeCells[currentRow, currentColumn + 1].isRoom)
                    {
                        RemoveWall(mazeCells[currentRow, currentColumn + 1].westWall);
                    }
                    currentColumn++;
                }
                //West
                else if (direction == 4 && CellAvailable(currentRow, currentColumn - 1))
                {
                    RemoveWall(mazeCells[currentRow, currentColumn].westWall);
                    if (!mazeCells[currentRow, currentColumn - 1].isRoom)
                    {
                        RemoveWall(mazeCells[currentRow, currentColumn - 1].eastWall);
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
                if (direction == 1 && CellAvailable(currentRow - 1, currentColumn))
                {
                    RemoveWall(mazeCells[currentRow, currentColumn].northWall);
                    RemoveWall(mazeCells[currentRow - 1, currentColumn].southWall);
                    mazeCells[currentRow, currentColumn].roomId = roomId;
                    mazeCells[currentRow, currentColumn].isRoom = true;
                    currentRow--;
                    mazeCells[currentRow, currentColumn].roomId = roomId;
                }
                //South
                else if (direction == 2 && CellAvailable(currentRow + 1, currentColumn))
                {
                    RemoveWall(mazeCells[currentRow, currentColumn].southWall);
                    RemoveWall(mazeCells[currentRow + 1, currentColumn].northWall);
                    mazeCells[currentRow, currentColumn].roomId = roomId;
                    mazeCells[currentRow, currentColumn].isRoom = true;
                    currentRow++;
                    mazeCells[currentRow, currentColumn].roomId = roomId;
                }
                //East
                else if (direction == 3 && CellAvailable(currentRow, currentColumn + 1))
                {
                    RemoveWall(mazeCells[currentRow, currentColumn].eastWall);
                    RemoveWall(mazeCells[currentRow, currentColumn + 1].westWall);
                    mazeCells[currentRow, currentColumn].roomId = roomId;
                    mazeCells[currentRow, currentColumn].isRoom = true;
                    currentColumn++;
                    mazeCells[currentRow, currentColumn].roomId = roomId;
                }
                //West
                else if (direction == 4 && CellAvailable(currentRow, currentColumn - 1))
                {
                    RemoveWall(mazeCells[currentRow, currentColumn].westWall);
                    RemoveWall(mazeCells[currentRow, currentColumn - 1].eastWall);
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

    /// <summary>
    /// Check if paths are available to move to when rooms are being generated
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Check if paths are available to move to when not generating rooms
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Check if cell is in bounds and is not a room so that it can be visited
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public bool CellAvailable(int row,int column)
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

    /// <summary>
    /// Destroy the wall object if it is present
    /// </summary>
    /// <param name="wall"></param>
    public void RemoveWall(GameObject wall)
    {
        if(wall != null)
        {
            GameObject.Destroy(wall);
        }
    }

    /// <summary>
    /// Search for the next unvisited cell next to a visited cell
    /// </summary>
    public void Hunt()
    {
        isDone = true;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                if(!mazeCells[r,c].visited && VisitedNeighbourCell(r, c))
                {
                    isDone = false;
                    currentRow = r;
                    currentColumn = c;
                    DestroyNextWall(currentRow, currentColumn);
                    mazeCells[currentRow, currentColumn].visited = true;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Check if neighbouring cell is visited
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public bool VisitedNeighbourCell(int row,int column)
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

    /// <summary>
    /// Destroy wall to the neibouring cell
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    public void DestroyNextWall(int row,int column)
    {
        bool wallDestroyed = false;

        while (!wallDestroyed)
        {
            int direction = Random.Range(1, 5);

            //North
            if (direction == 1 && row > 0 && mazeCells[row -1,column].visited && !mazeCells[row - 1, column].isRoom) // Check if room
            {
                RemoveWall(mazeCells[row, column].northWall);
                RemoveWall(mazeCells[row - 1, column].southWall);
                wallDestroyed = true;
            }
            //South
            else if (direction == 2 && row < (rows-2) && mazeCells[row + 1, column].visited && !mazeCells[row + 1, column].isRoom)
            {
                RemoveWall(mazeCells[row, column].southWall);
                RemoveWall(mazeCells[row + 1, column].northWall);
                wallDestroyed = true;
            }
            //East
            else if (direction == 3 && column < (columns-2) && mazeCells[row, column + 1].visited && !mazeCells[row, column + 1].isRoom)
            {
                RemoveWall(mazeCells[row, column].eastWall);
                RemoveWall(mazeCells[row, column + 1].westWall);
                wallDestroyed = true;
            }
            //West
            else if (direction == 4 && column > 0 && mazeCells[row, column - 1].visited && !mazeCells[row, column - 1].isRoom)
            {
                RemoveWall(mazeCells[row, column].westWall);
                RemoveWall(mazeCells[row, column - 1].eastWall);
                wallDestroyed = true;
            }
        }
    }
}
