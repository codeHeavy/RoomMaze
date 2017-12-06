using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    List<Vector3> pathToGoal;
    IDictionary<Vector3, Vector3> nodeParents = new Dictionary<Vector3, Vector3>();
    Vector3 startPos;
    Vector3 goalPos;
    int i;

    // Use this for initialization
    void Start () {
        startPos = GetCurrentMazeCellCoordinates();
        goalPos = new Vector3(10, 0, 10);
        pathToGoal = GetPath();
        ChangeToWorldCoordinates();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(pathToGoal);

        float speed = 5;
        float step = Time.deltaTime * speed;
        transform.position = Vector3.MoveTowards(transform.position, pathToGoal[i], step);
        if (transform.position.Equals(pathToGoal[i]) && i >= 0)
            i--;
        
    }

    public void ChangeToWorldCoordinates()
    {
        for(int j = 0; j < pathToGoal.Count; j++)
        {
            pathToGoal[j] = new Vector3(pathToGoal[j].x * Maze.size, 0, pathToGoal[j].z * Maze.size);
        }
    }

    public List<Vector3> GetPath()
    {
        List<Vector3> path = new List<Vector3>();
        Vector3 goal;
        goal = FindPath(startPos, goalPos);
        Vector3 curr = goal;
        while (curr != GetCurrentMazeCellCoordinates())
        {
            path.Add(curr);
            curr = nodeParents[curr];
        }

        return path;
    }

    public Vector3 GetCurrentMazeCellCoordinates()
    {
        Vector3 currentCellPos = new Vector3();
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 50, Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 50f))
        {
            //Debug.Log("Name" + hit.collider.gameObject.transform.position);
            currentCellPos = hit.collider.gameObject.transform.position;
            currentCellPos.x /= Maze.size;
            currentCellPos.z /= Maze.size;
            return currentCellPos;
        }
        else
        {
            Debug.Log("No floor below!!");
            return Vector3.zero;
        }
    }

    // Get list of walkable nodes from current nodes
    public List<Vector3> GetTraversableCells(Vector3 currentCell)
    {
        List<MazeCell> traversable = new List<MazeCell>();
        List<Vector3> traverablePos = new List<Vector3>();
        if(Maze.mazeCells[(int)currentCell.x,(int)currentCell.z].northWall == null)
        {
            traversable.Add(Maze.mazeCells[((int)currentCell.x)+1, (int)currentCell.z]);
            traverablePos.Add(new Vector3(((int)currentCell.x) + 1, (int)currentCell.z));
        }
        if (Maze.mazeCells[(int)currentCell.x, (int)currentCell.z].southWall == null)
        {
            traversable.Add(Maze.mazeCells[((int)currentCell.x) -1, (int)currentCell.z]);
            traverablePos.Add(new Vector3(((int)currentCell.x) - 1, (int)currentCell.z));
        }
        if (Maze.mazeCells[(int)currentCell.x, (int)currentCell.z].eastWall == null)
        {
            traversable.Add(Maze.mazeCells[(int)currentCell.x , ((int)currentCell.z) + 1]);
            traverablePos.Add(new Vector3((int)currentCell.x, ((int)currentCell.z) + 1));
        }
        if (Maze.mazeCells[(int)currentCell.x, (int)currentCell.z].westWall == null)
        {
            traversable.Add(Maze.mazeCells[(int)currentCell.x + 1, ((int)currentCell.z) - 1]);
            traverablePos.Add(new Vector3((int)currentCell.x + 1, ((int)currentCell.z) - 1));
        }
        return traverablePos;
    }

    public Vector3 FindPath(Vector3 startPosition, Vector3 goalPosition)
    {
        Stack<Vector3> stack = new Stack<Vector3>();
        HashSet<Vector3> exploredNodes = new HashSet<Vector3>();
        stack.Push(startPosition);

        while (stack.Count != 0)
        {
            Vector3 currentNode = stack.Pop();
            if (currentNode == goalPosition)
            {
                return currentNode;
            }

            IList<Vector3> nodes = GetTraversableCells(currentNode);

            foreach (Vector3 node in nodes)
            {
                if (!exploredNodes.Contains(node))
                {
                    //Mark the node as explored
                    exploredNodes.Add(node);

                    //Store a reference to the previous node
                    nodeParents.Add(node, currentNode);

                    //Add this to the queue of nodes to examine
                    stack.Push(node);
                }
            }
        }

        return startPosition;
    }
}
