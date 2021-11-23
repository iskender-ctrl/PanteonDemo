using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStickScript : MonoBehaviour
{
    public float turnSpeed = 5;
    void Update()
    {
        transform.Rotate(turnSpeed * Time.deltaTime, 0, 0);
    }
}
