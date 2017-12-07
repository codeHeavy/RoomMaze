using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    public float sensitivityX = 15F;


    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
            rb.freezeRotation = true;
    }
    /// <summary>
    /// Rotate corresponding to mouse X-axis movement
    /// </summary>
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
    }

}
