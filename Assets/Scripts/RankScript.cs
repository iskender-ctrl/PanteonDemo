using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class RankScript : MonoBehaviour
{
    public GameObject[] contestants;
    public TextMeshProUGUI order;
    void Update()
    {
        Rank();
    }
    //Here, the distance of the competitors to the finish is taken and the ranking of our main character is printed on the screen.
    void Rank()
    {
        foreach (GameObject i in contestants)
        {
            Array.Sort(contestants, delegate (GameObject a, GameObject b)
            {
                return Vector3.Distance(a.transform.position, transform.position).
                CompareTo(Vector3.Distance(b.transform.position, gameObject.transform.position));
            });
            if (i.gameObject.tag == "Player")
            {
                int index = System.Array.IndexOf(contestants, i);
                order.text = (index + 1).ToString();
            }
        }
    }
}
