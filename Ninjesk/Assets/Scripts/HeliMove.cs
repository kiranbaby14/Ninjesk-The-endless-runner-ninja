using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliMove : MonoBehaviour
{
    public Vector3 helicopterPosition;
    public Vector3 helicopterRotation;
    public Transform lower;
    public Transform upper;
    public Transform player;
    private int distance = 50;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, helicopterPosition, 0.05f);
        lower.transform.Rotate(0, 1000 * Time.deltaTime, 0);
        upper.transform.Rotate(0, 1000 * Time.deltaTime, 0);
        if (Vector3.Distance(player.position, transform.position) <= distance)
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(helicopterRotation), 0.03f);
        }

        if (Player.isRunning)
            Destroy(gameObject);
    }
}
