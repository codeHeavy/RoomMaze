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

                    GameObject.Destroy(mazeCells[randRow, randCol].eastWall);
                    GameObject.Destroy(mazeCells[randRow, randCol + 1].westWall);
                    GameObject.Destroy(mazeCells[randRow, randCol].southWall);
                    GameObject.Destroy(mazeCells[randRow, randCol + 1].southWall);
                    //for Door
                    GameObject.Destroy(mazeCells[randRow, randCol].northWall);
                    GameObject.Destroy(mazeCells[randRow - 1, randCol].southWall);

                    mazeCells[randRow + 1, randCol].visited = true;
                    mazeCells[randRow + 1, randCol].isRoom = true;
                    mazeCells[randRow + 1, randCol].roomId = roomId;

                    mazeCells[randRow + 1, randCol + 1].visited = true;
                    mazeCells[randRow + 1, randCol + 1].isRoom = true;
                    mazeCells[randRow + 1, randCol + 1].roomId = roomId;

                    GameObject.Destroy(mazeCells[randRow + 1, randCol].eastWall);
                    GameObject.Destroy(mazeCells[randRow + 1, randCol + 1].westWall);
                    GameObject.Destroy(mazeCells[randRow + 1, randCol].northWall);
                    GameObject.Destroy(mazeCells[randRow + 1, randCol + 1].northWall);
                    numberOfRooms--;
                    roomId++;
                    if (roomId > 3) //number of materials
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

    public bool CheckRoomOverlap(MazeCell[,] mazeCells,int row,int col)
    {
        if(mazeCells[row,col].roomId == 0 || mazeCells[row, col+1].roomId == 0 || mazeCells[row+1, col].roomId == 0 || mazeCells[row+1, col+1].roomId == 0)
        {
            return true;
        }

        return false;
    }
}
