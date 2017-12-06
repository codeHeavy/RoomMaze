using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSectionCell  {

    public Vector3 cellCoordinate;
    public int turnsTaken = 0;
    public int firstTurn;  // 0 = Left 1 = Right

    public CrossSectionCell()
    {

    }

    public CrossSectionCell(Vector3 pos, int turn)
    {
        cellCoordinate = pos;
        firstTurn = turn;
        turnsTaken++;
    }
}
