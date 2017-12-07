using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    [Range(4, 20)]
    float bulletSpeed;
    [SerializeField]
    float hitPoint = 20;

    Vector3 startPos;
    float range = 20;

    
    void Start () {
        startPos = transform.position;
	}
	
	/// <summary>
    /// Bullet travels a certain range in the forward direction
    /// </summary>
	void FixedUpdate () {
        transform.position += transform.forward * bulletSpeed * Time.fixedDeltaTime;
        if(Vector3.Distance(startPos,transform.position) >= range)
        {
            Destroy(gameObject);
        }
	}

    /// <summary>
    /// Attack player if player is hit and destroy bullet when colliding
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerProperties>().TakeDamage(hitPoint);
        }
        Destroy(gameObject);
    }
}
