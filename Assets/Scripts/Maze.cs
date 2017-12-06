using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public abstract class Maze
    {
        public static MazeCell[,] mazeCells;
        public static int rows, columns;
        public static float size;
        

        protected Maze(MazeCell[,] maze, float s) : base()
        {
            mazeCells = maze;
            rows = mazeCells.GetLength(0);
            columns = mazeCells.GetLength(1);
            size = s;
        }

        public abstract void CreateMaze();
    }

