using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaintScript : MonoBehaviour
{
    public GameObject paintPrefab;
    GameObject spawn;
    void Update()
    {
        Paint();
    }
    void Paint()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform && hit.transform.tag == "RayWall")
                {
                    if (hit.collider != null)
                    {
                        if (Input.touches[0].phase == TouchPhase.Began)
                        {
                            spawn = Instantiate(paintPrefab, hit.point, Quaternion.identity);
                        }
                        if (Input.touches[0].phase == TouchPhase.Moved)
                        {
                            spawn = Instantiate(paintPrefab, hit.point, Quaternion.identity);
                        }
                    }
                }
            }

        }
    }
}
