using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public float dragSpeed = 3;
    public float forceSpeed = 4;
    bool canMove = true;
    bool isCollision;
    bool paintWallMove = false;
    private GameObject paintWall;
    public float SmoothTime = 1f;
    private Vector3 velocity = Vector3.zero;
    Rigidbody rb;
    Animator Animations;
    Vector3 beginPosition;
    float distance;
    GameObject[] girls;

    private void Start()
    {
        girls = GameObject.FindGameObjectsWithTag("Girl");
        beginPosition = transform.position;
        paintWall = GameObject.FindGameObjectWithTag("PaintWall");
        rb = GetComponent<Rigidbody>();
        Animations = GetComponent<Animator>();
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, paintWall.transform.position);
        if (canMove == true)
        {
            ControllPlayer();
        }
        if (paintWallMove == true)
        {
            paintWall.transform.position = Vector3.SmoothDamp(paintWall.transform.position, new Vector3(-1.33f, 5.39f, paintWall.transform.position.z), ref velocity, SmoothTime);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(paintWall.transform.position.x,
            transform.position.y, paintWall.transform.position.z - 5f), 5 * Time.deltaTime);
        }
        if (distance <= 8)
        {
            Animations.SetBool("Run", false);
        }
    }
    private void FixedUpdate()
    {
        if (isCollision == true)
        {
            OnPlatformMove();
        }
        else
        {
            transform.eulerAngles = new Vector3(0.0f, 0f, 0.0f);
        }
    }
    void ControllPlayer()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        float xPos = Mathf.Clamp(transform.position.x, -13f, 13f);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(xPos + touch.deltaPosition.x * dragSpeed * Time.deltaTime, transform.position.y,
                transform.position.z);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                if (touch.deltaPosition.y < 0)
                {
                    StartCoroutine(SlideAnimation());
                }
            }
        }
    }
    IEnumerator SlideAnimation()
    {
        Animations.SetBool("Slide", true);
        GetComponent<CapsuleCollider>().isTrigger = true;
        yield return new WaitForSeconds(1);
        Animations.SetBool("Slide", false);
        GetComponent<CapsuleCollider>().isTrigger = false;
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "RotationPlatform")
        {
            isCollision = true;
        }
        if (col.gameObject.tag == "Obstacle")
        {
            gameObject.transform.position = beginPosition;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "RotationPlatform")
        {
            isCollision = false;
        }
    }
    void OnPlatformMove()
    {
        rb.AddForce(Vector3.forward * forceSpeed);
        transform.eulerAngles = new Vector3(0.0f, 40f, 0.0f);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "FinishLine")
        {
            foreach (GameObject i in girls)
            {
                Destroy(i);
            }
            gameObject.GetComponent<PlayerPaintScript>().enabled = true;
            canMove = false;
            col.gameObject.SetActive(false);
            paintWallMove = true;
        }
    }
}

