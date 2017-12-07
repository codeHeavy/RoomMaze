using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    int currentDirection;
    bool hitWall = false;
    int direction;
    [Range(1, 30)]
    public float speed = 3f;
    Rigidbody rb;

    Stack<Vector3> visitedCells;
    Stack<CrossSectionCell> turningPoints;
    bool wallOnLeft = false;
    bool wallOnRight = false;
    bool wallFront = false;

    Vector3 startCell;
    Vector3 currentCell;
    public LayerMask playerLayer;

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        direction = Random.Range(1, 5);
        currentDirection = direction;

        turningPoints = new Stack<CrossSectionCell>();
        visitedCells = new Stack<Vector3>();
        currentCell = new Vector3();

        startCell = GetCurrentMazeCellCoordinates();
        currentCell = startCell;
        visitedCells.Push(startCell);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentCell = GetCurrentMazeCellCoordinates();
    }
    /// <summary>
    /// Turn left or right depending on the surroundings
    /// Turn left if no wall to the left
    /// Turn right if no right wall
    /// </summary>
    /// <returns></returns>
    IEnumerator PickDirection()
    {
        int pathsAvailable = LookAround();
        if (pathsAvailable >= 1)
        {
            if (wallFront)
            {

                if (!wallOnLeft && !wallOnRight)
                {

                    //pick random direction
                    int randTurn = Random.Range(0, 2);
                    if (randTurn == 0)
                    {
                        //turn left & set wall front to false to reset flags
                        //Debug.Log("Turning Left .. walls either side");
                        wallFront = false;
                        yield return StartCoroutine("TurnLeft");
                    }
                    else if (randTurn == 1)
                    {
                        //turn right & set wall front to false to reset flags
                        //Debug.Log("Turning Right .. walls either side");
                        wallFront = false;
                        yield return StartCoroutine("TurnRight");
                    }

                    turningPoints.Push(new CrossSectionCell(new Vector3(GetCurrentMazeCellCoordinates().x, 0, GetCurrentMazeCellCoordinates().z), randTurn));
                }
                else if (!wallOnLeft && wallOnRight)
                {
                    //turn left & set wall front to false to reset flags
                    wallFront = false;
                    yield return StartCoroutine("TurnLeft");
                }
                else if (!wallOnRight && wallOnLeft)
                {
                    //turn right & set wall front to false to reset flags
                    //Debug.Log("Turning Right ");
                    wallFront = false;
                    yield return StartCoroutine("TurnRight");
                }
                else
                {
                    // all routes are blocked... Turn around
                    yield return StartCoroutine("TurnAround");
                    wallFront = false;
                    wallOnLeft = false;
                    wallOnRight = false;
                    LookAround();
                    //Debug.Log("Wall on all 3 sides...turn around");
                }
            }
            else
            {

                Debug.Log("No wall in front... Moving");
                LookAround();

            }
        }
    }

    IEnumerator TurnLeft()
    {
        //Debug.Log("Turning left");
        transform.Rotate(Vector3.up, -90);
        yield return null;
    }
    IEnumerator TurnRight()
    {
        //Debug.Log("Turning right");
        transform.Rotate(Vector3.up, 90);
        yield return null;
    }
    IEnumerator TurnAround()
    {
        // Debug.Log("Turning around");
        transform.Rotate(Vector3.up, 180);
        yield return null;
    }

    /// <summary>
    /// Move forward if no wall is present ahead
    /// </summary>
    public void MoveAbout()
    {

        if (!wallFront)
        {
            //move forward
            //Debug.Log("Moving Forward");
            transform.position += transform.forward * speed * Time.fixedDeltaTime;
            LookAround();
            wallOnLeft = false;
            wallOnRight = false;
        }
        else if (wallFront) // wall ahead
        {
            // pick a direction to turn to
            StartCoroutine("PickDirection");
        }

    }
    
    /// <summary>
    /// Keep track of junction cells (cells were 2 turns can be made)
    /// </summary>
    public void CheckTurningPoints()
    {
        if (turningPoints.Count > 0)
        {
            Vector3 cur = GetCurrentMazeCellCoordinates();
            CrossSectionCell top = turningPoints.Peek();
            Debug.Log("top " + top.cellCoordinate + "cur " + GetCurrentMazeCellCoordinates() + "turns" + top.turnsTaken);
            if (top.cellCoordinate.x == cur.x && top.cellCoordinate.z == cur.z && top.turnsTaken == 1)
            {
                turningPoints.Peek().turnsTaken++;
            }
            if (top.cellCoordinate.x == cur.x && top.cellCoordinate.z == cur.z && top.turnsTaken == 2)
            {
                if (top.firstTurn == 0) // if first turn was left
                {
                    //turn right
                    StartCoroutine("TurnRight");
                }
                else if (top.firstTurn == 1) // if first turn was right
                {
                    //turn left
                    StartCoroutine("TurnLeft");
                }
            }
        }
    }

    /// <summary>
    /// Check if walls are present to the left, right and/or ahead of current position
    /// </summary>
    /// <returns></returns>
    public int LookAround()
    {
        List<int> paths = new List<int>();
        int pathsAvailable = 0;
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 4, Color.green);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 4, Color.blue);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 4, Color.yellow);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 4f))
        {
            // Check if wall is present ahead of current position
            wallFront = true;
            pathsAvailable++;
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 4f))
        {
            // Check if wall is present to the left
            wallOnLeft = true;
            pathsAvailable++;
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 4f))
        {
            //Check if wall is present to the right
            wallOnRight = true;
            pathsAvailable++;
        }
        return pathsAvailable;
    }

    /// <summary>
    /// Get maze cell coordinates of the current cell
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCurrentMazeCellCoordinates()
    {
        Vector3 currentCellPos = new Vector3();
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 50, Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 50f))
        {
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

    /// <summary>
    /// Check if enemy is withing shooting range
    /// </summary>
    /// <returns></returns>
    public bool CheckEnemyPresence()
    {
        gameObject.GetComponent<EnemyProperties>().patroling = true;
        gameObject.GetComponent<EnemyProperties>().attacking = false;
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20, Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20f, playerLayer))
        {
            Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "Player")
            {
                //Debug.Log("Attack player");
                gameObject.GetComponent<EnemyProperties>().attacking = true;
                gameObject.GetComponent<EnemyProperties>().patroling = false;
            }
            
        }
        return true;
    }

}
