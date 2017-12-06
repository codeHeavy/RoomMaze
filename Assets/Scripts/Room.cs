using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room:MonoBehaviour {
    int id;
    int size;
    List<MazeCell> cells;

    public void Start()
    {
       // personalizeRoom(Maze.mazeCells); ;
    }
    public void PersonalizeRoom(MazeCell[,] mazeCells, List<Material> floorMaterials)
    {
        if (floorMaterials.Count == 0)
        {
            Debug.Log("No floor materials");
        }
        else
        {
            int rows = mazeCells.GetLength(0);
            int columns = mazeCells.GetLength(1);
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    switch (mazeCells[r, c].roomId)
                    {
                        case 1:
                            mazeCells[r, c].floor.GetComponent<Renderer>().material = floorMaterials[0];
                            break;
                        case 2:
                            mazeCells[r, c].floor.GetComponent<Renderer>().material = floorMaterials[1];
                            break;
                        case 3:
                            mazeCells[r, c].floor.GetComponent<Renderer>().material = floorMaterials[2];
                            break;
                    }
                }
            }
        }
    }

    
}
