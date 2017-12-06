using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

    IDictionary<Vector3, Vector3> nodeParents = new Dictionary<Vector3, Vector3>();
    public IDictionary<Vector3, bool> walkablePositions;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  

    Vector3 FindShortestPathDFS(Vector3 startPosition, Vector3 goalPosition)
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

            IList<Vector3> nodes = GetWalkableNodes(currentNode);

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

    IList<Vector3> GetWalkableNodes(Vector3 curr)
    {

        IList<Vector3> walkableNodes = new List<Vector3>();

        IList<Vector3> possibleNodes = new List<Vector3>() {
            new Vector3 (curr.x + 1, curr.y, curr.z),
            new Vector3 (curr.x - 1, curr.y, curr.z),
            new Vector3 (curr.x, curr.y, curr.z + 1),
            new Vector3 (curr.x, curr.y, curr.z - 1),
            new Vector3 (curr.x + 1, curr.y, curr.z + 1),
            new Vector3 (curr.x + 1, curr.y, curr.z - 1),
            new Vector3 (curr.x - 1, curr.y, curr.z + 1),
            new Vector3 (curr.x - 1, curr.y, curr.z - 1)
        };

        foreach (Vector3 node in possibleNodes)
        {
            if (CanMove(node))
            {
                walkableNodes.Add(node);
            }
        }

        return walkableNodes;
    }

    bool CanMove(Vector3 nextPosition)
    {
        return (walkablePositions.ContainsKey(nextPosition) ? walkablePositions[nextPosition] : false);
    }
}
