using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    [SerializeField]
    float Health = 100;
    [SerializeField]
    GameObject bulletPrefab;
    
    public bool patroling = true;
    bool dead = false;
    public bool attacking = false;
    void Update()
    {
        
    }
    /// <summary>
    /// Reduce health by hitpoint amount
    /// </summary>
    /// <param name="hitPoint"></param>
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
    /// <summary>
    /// Shoot bullets in the forward direction
    /// </summary>
    public void AttackPlayer()
    {
        Instantiate(bulletPrefab,new Vector3(transform.position.x,(float)-0.7,transform.position.z),Quaternion.LookRotation(transform.forward));
        patroling = true;
        attacking = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
