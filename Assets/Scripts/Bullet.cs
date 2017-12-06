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

    // Use this for initialization
    void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position += transform.forward * bulletSpeed * Time.fixedDeltaTime;
        if(Vector3.Distance(startPos,transform.position) >= range)
        {
            Destroy(gameObject);
        }
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerProperties>().TakeDamage(hitPoint);
        }
        Destroy(gameObject);
    }
}
