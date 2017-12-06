using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    float playerSpeed  = 10; //speed player moves
    float attackDamage = 20;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("up"))//Press up arrow key to move forward on the Y AXIS
        {
            transform.position += transform.forward * playerSpeed * Time.fixedDeltaTime;
        }
        if (Input.GetKey("down"))
        {
            transform.position += -transform.forward * playerSpeed * Time.fixedDeltaTime;
        }
        if (Input.GetKey("right")) //Right arrow key to turn right
        {
            transform.position += transform.right * playerSpeed * Time.fixedDeltaTime;
            //StartCoroutine("TurnLeft");
        }

        if (Input.GetKey("left"))//Left arrow key to turn left
        {
            transform.position += -transform.right * playerSpeed * Time.fixedDeltaTime;
            //StartCoroutine("TurnRight");
        }
    }

    IEnumerator TurnLeft()
    {
        //Debug.Log("Turning left");
        transform.Rotate(Vector3.up, -90 * Time.deltaTime);
        yield return null;
    }
    IEnumerator TurnRight()
    {
        //Debug.Log("Turning right");
        transform.Rotate(Vector3.up, 90 * Time.deltaTime);
        yield return null;
    }
    IEnumerator TurnAround()
    {
        // Debug.Log("Turning around");
        transform.Rotate(Vector3.up, 180);
        yield return null;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "enemy")
        {
            //Debug.Log("Attacking...");
            other.gameObject.GetComponent<EnemyProperties>().TakeDamage(attackDamage);
        }
    }
}
