using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell {

    public bool visited = false;
    public GameObject northWall, southWall, eastWall, westWall, floor;
    public int roomId = 0;
    public bool isRoom = false;
}
