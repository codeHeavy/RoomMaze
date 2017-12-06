using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour {

    [SerializeField]
    float Health = 100;
    bool dead = false;
    
    public void TakeDamage(float hitPoint)
    {
        StartCoroutine(takeDamage(hitPoint));
    }

    IEnumerator takeDamage(float hitPoint)
    {
        Health -= hitPoint;
        if (Health <= 0)
        {
            dead = true;
            Die();
        }
        yield return new WaitForSeconds(2);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
