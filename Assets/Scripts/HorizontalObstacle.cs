using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacle : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 pos1, pos2;
    public bool horizontal = true;
    void Awake()
    {
        pos1 = new Vector3(12.88f, transform.position.y, transform.position.z);
        pos2 = new Vector3(-12.8f, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        ObstacleMovement();
    }
    void ObstacleMovement()
    {
        if (horizontal == true)
        {
            transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));
        }
        else
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}
