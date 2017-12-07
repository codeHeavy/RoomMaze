using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArbiter : MonoBehaviour {

    EnemyProperties properties;
    EnemyMovement movement;

    void Start () {
        properties = gameObject.GetComponent<EnemyProperties>();
        movement = gameObject.GetComponent<EnemyMovement>();
	}
	
    /// <summary>
    /// Check data in the blackboard (EnemyProperties) and decide on next action
    /// </summary>
	void Update () {
        movement.CheckEnemyPresence();
        if (properties.attacking)
        {
            properties.AttackPlayer();
        }
        else if (properties.patroling)
        {
            movement.MoveAbout();
            movement.CheckEnemyPresence();
        }
	}
}
