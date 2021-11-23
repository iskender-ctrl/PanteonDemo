using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent nav;
    private Vector3 targetDir;
    private GameObject slideObstacle;
    public float speed = 5f;
    Rigidbody rb;
    Vector3 beginPosition;
    Animator Animations;
    bool isCollision;
    public float forceSpeed = 4;
    void Awake()
    {
        slideObstacle = GameObject.Find("HorizontalObstacle (2)");
        Animations = GetComponent<Animator>();
        beginPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("FinishLine").transform;
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        EnemyMove();
    }
    void EnemyMove()
    {
        float distance = Vector3.Distance(transform.position, slideObstacle.transform.position);
        transform.Translate(0, 0, speed * Time.deltaTime);
        targetDir = target.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);

        if (angle >= -50 && angle <= 50)
        {
            nav.SetDestination(target.position);
        }
        if (distance < 10f)
        {
            StartCoroutine(SlideAnimation());
        }
    }
    void FixedUpdate()
    {
        if (isCollision == true)
        {
            OnPlatformMove();
        }

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
    // İf the object is on the rotate platform add force
    void OnPlatformMove()
    {
        rb.AddForce(Vector3.forward * forceSpeed);
        transform.eulerAngles = new Vector3(0.0f, 40f, 0.0f);
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "RotationPlatform")
        {
            isCollision = false;
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
}
