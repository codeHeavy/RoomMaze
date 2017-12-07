using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRooms {

    //public MazeCell[,] mazeCells;
    int numberOfRooms = 4;
    int roomSize;   //number of cells in a room
    int numberOfCells;
    int roomId = 1;
    bool generate = true;

    MazeAlgorithm ma;

    /// <summary>
    /// Select random row and column to insert room at
    /// Check if the cell is not a room already and make room if it isn't
    /// Each room will be a square with 4 cells oriented like so [cell 1][ cell 2]
    ///                                                          [cell 3][ cell 4]
    /// </summary>
    /// <param name="mazeCells"></param>
    /// <param name="minRooms"></param>
    /// <param name="maxRooms"></param>
    public void CreateRooms(MazeCell[,] mazeCells,int minRooms, int maxRooms)
    {
        numberOfRooms = Random.Range(minRooms, maxRooms);
        while (numberOfRooms != 0)
        {
            int randRow = Random.Range(1, mazeCells.GetLength(0));
            int randCol = Random.Range(1, mazeCells.GetLength(1));
            if (CheckRoomOverlap(mazeCells, randRow, randCol))
            {
                if (randCol + 1 < mazeCells.GetLength(1) && randRow + 1 < mazeCells.GetLength(0))
                {
                    mazeCells[randRow, randCol].visited = true;
                    mazeCells[randRow, randCol].isRoom = true;
                    mazeCells[randRow, randCol].roomId = roomId;

                    mazeCells[randRow, randCol + 1].visited = true;
                    mazeCells[randRow, randCol + 1].isRoom = true;
                    mazeCells[randRow, randCol + 1].roomId = roomId;

                    // Remove walls on cells to connect to adjacent cell
                    GameObject.Destroy(mazeCells[randRow, randCol].eastWall);
                    GameObject.Destroy(mazeCells[randRow, randCol + 1].westWall);
                    GameObject.Destroy(mazeCells[randRow, randCol].southWall);
                    GameObject.Destroy(mazeCells[randRow, randCol + 1].southWall);
                    
                    // remove wall that joins with corridor to make a doo
                    // TODO: make door prefab and insert
                    GameObject.Destroy(mazeCells[randRow, randCol].northWall);
                    GameObject.Destroy(mazeCells[randRow - 1, randCol].southWall);

                    mazeCells[randRow + 1, randCol].visited = true;
                    mazeCells[randRow + 1, randCol].isRoom = true;
                    mazeCells[randRow + 1, randCol].roomId = roomId;

                    mazeCells[randRow + 1, randCol + 1].visited = true;
                    mazeCells[randRow + 1, randCol + 1].isRoom = true;
                    mazeCells[randRow + 1, randCol + 1].roomId = roomId;

                    // Remove walls on cells to connect to adjacent cell
                    GameObject.Destroy(mazeCells[randRow + 1, randCol].eastWall);
                    GameObject.Destroy(mazeCells[randRow + 1, randCol + 1].westWall);
                    GameObject.Destroy(mazeCells[randRow + 1, randCol].northWall);
                    GameObject.Destroy(mazeCells[randRow + 1, randCol + 1].northWall);
                    numberOfRooms--;
                    roomId++;
                    if (roomId > 3) // 3 - number of materials that is used to customize the room
                    {
                        roomId = 1;
                    }
                }
                else
                {
                    Debug.Log("Out of bounds");
                }
            }
        }
    }

    /// <summary>
    /// Check if the random cell and corresponding cells that will make up a room are already part of a room
    /// </summary>
    /// <param name="mazeCells"></param>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    public bool CheckRoomOverlap(MazeCell[,] mazeCells,int row,int col)
    {
        if(mazeCells[row,col].roomId == 0 || mazeCells[row, col+1].roomId == 0 || mazeCells[row+1, col].roomId == 0 || mazeCells[row+1, col+1].roomId == 0)
        {
            return true;
        }

        return false;
    }
}
