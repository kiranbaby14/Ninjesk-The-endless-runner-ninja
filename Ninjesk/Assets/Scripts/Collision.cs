using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public static bool isObstacles;

    void Start()
    {
        isObstacles = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Obstacles"))
        {
           
            isObstacles = true;

        }
    }
}
