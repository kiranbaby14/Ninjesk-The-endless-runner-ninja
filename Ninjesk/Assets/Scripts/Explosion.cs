using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public Animator enemyninja_Animator;

    public GameObject explosion;
    public GameObject Ninja;
    public static bool caughtPlayer;
    public static bool hasExploded;
    void Start()
    {
        explosion.SetActive(false);
        caughtPlayer = false;
        hasExploded = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name.Equals("Ninja") && Player.isAttack)
        {
            hasExploded = true;
            FindObjectOfType<AudioManager>().Play("Explosion_Enemy");
            explosion.SetActive(true);
            Destroy(Ninja);

        }
        else if(other.gameObject.name.Equals( "Ninja"))
        {
            caughtPlayer = true;
            enemyninja_Animator.Play("Punch");
        }
     
    }
}
