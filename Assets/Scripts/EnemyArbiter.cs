using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArbiter : MonoBehaviour {

    EnemyProperties properties;
    EnemyMovement movement;
	// Use this for initialization
	void Start () {
        properties = gameObject.GetComponent<EnemyProperties>();
        movement = gameObject.GetComponent<EnemyMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        movement.CheckEnemyPresence();
        if (properties.attacking)
        {
            //properties.patroling = false;
            properties.AttackPlayer();
        }
        else if (properties.patroling)
        {
            movement.MoveAbout();
            movement.CheckEnemyPresence();
        }
	}
}
