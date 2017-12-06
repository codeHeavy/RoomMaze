using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeLoader : MonoBehaviour {

    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    GameObject playerPrefab;
    public int minEnemies = 3;
    public int maxEnemies = 10;

    public int minRooms = 3;
    public int maxRooms = 5;


    public int rows , columns;
    public GameObject wall;
    public GameObject floor;
    private GameObject stage;
    public float size = 2f;

    public MazeCell[,] mazeCells;

    [SerializeField]
    List<Material> floorMaterials = new List<Material>();

    public void Start()
    {
        stage = new GameObject("Stage");
        Initialize2();
        GenerateRooms roomsGenerator = new GenerateRooms();
        Room room = new Room();
        roomsGenerator.CreateRooms(mazeCells,minRooms,maxRooms);
        room.PersonalizeRoom(mazeCells, floorMaterials);
        Maze maze = new MazeAlgorithm(mazeCells,size);
        maze.CreateMaze();
        Spawn sp = new Spawn();
        sp.SpawnEnemies(rows, columns, size, enemyPrefab,minEnemies,maxEnemies);
        sp.SpawnPlayer(playerPrefab);
    }

    public void Initialize()
    {
        mazeCells = new MazeCell[rows, columns];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                mazeCells[r, c] = new MazeCell();

                mazeCells[r, c].floor = Instantiate(floor, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].floor.name = "Floor " + r + "," + c;
                mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);

                if (c == 0)
                {
                    mazeCells[r, c].westWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) - (size / 2f)), Quaternion.identity) as GameObject;
                    mazeCells[r, c].westWall.name = "West Wall " + r + "," + c;
                }

                mazeCells[r, c].eastWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) + (size / 2f)), Quaternion.identity) as GameObject;
                mazeCells[r, c].eastWall.name = "East Wall " + r + "," + c;

                if (r == 0)
                {
                    mazeCells[r, c].northWall = Instantiate(wall, new Vector3((r * size) - (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].northWall.name = "North Wall " + r + "," + c;
                    mazeCells[r, c].northWall.transform.Rotate(Vector3.up * 90f);
                }

                mazeCells[r, c].southWall = Instantiate(wall, new Vector3((r * size) + (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].southWall.name = "South Wall " + r + "," + c;
                mazeCells[r, c].southWall.transform.Rotate(Vector3.up * 90f);
            }
        }
    }

    public void Initialize2()
    {
        mazeCells = new MazeCell[rows, columns];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                mazeCells[r, c] = new MazeCell();

                mazeCells[r, c].floor = Instantiate(floor, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].floor.name = "Floor " + r + "," + c;
                mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);
                mazeCells[r, c].floor.transform.SetParent(stage.transform);

                mazeCells[r, c].northWall = Instantiate(wall, new Vector3((r * size) - (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].northWall.name = "North Wall " + r + "," + c;
                mazeCells[r, c].northWall.transform.Rotate(Vector3.up * 90f);
                mazeCells[r, c].northWall.transform.SetParent(stage.transform);

                mazeCells[r, c].southWall = Instantiate(wall, new Vector3((r * size) + (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].southWall.name = "South Wall " + r + "," + c;
                mazeCells[r, c].southWall.transform.Rotate(Vector3.up * 90f);
                mazeCells[r, c].southWall.transform.SetParent(stage.transform);

                mazeCells[r, c].eastWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) + (size / 2f)), Quaternion.identity) as GameObject;
                mazeCells[r, c].eastWall.name = "East Wall " + r + "," + c;
                mazeCells[r, c].eastWall.transform.SetParent(stage.transform);

                mazeCells[r, c].westWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) - (size / 2f)), Quaternion.identity) as GameObject;
                mazeCells[r, c].westWall.name = "West Wall " + r + "," + c;
                mazeCells[r, c].westWall.transform.SetParent(stage.transform);


            }
        }
    }
}
