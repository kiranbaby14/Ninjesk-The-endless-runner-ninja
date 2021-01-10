using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
   
    void Update()
    {
        transform.Rotate(0, 200 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Equals("Ninja"))
        {
            FindObjectOfType<AudioManager>().Play("coin");
            SaveManager.Instance.state.coin++;
            SaveManager.Instance.Save();
            Destroy(gameObject);
            Player.numberOfCoins += 1;
        }
    }
}
