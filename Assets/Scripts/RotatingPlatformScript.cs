using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatformScript : MonoBehaviour
{
    public float turnSpeed = 5;
    Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rb.AddRelativeTorque(Vector3.forward * turnSpeed);
    }
}
