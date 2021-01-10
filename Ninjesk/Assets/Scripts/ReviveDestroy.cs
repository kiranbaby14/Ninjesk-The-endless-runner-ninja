using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveDestroy : MonoBehaviour
{
   

    private Transform _player;
    public int distance = 50;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Ninja").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _player.position) <= distance)
        {
            if (Explosion.caughtPlayer == true || Collision.isObstacles == true)
            {
          
                if (DeathMenu.isRevive == true)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
