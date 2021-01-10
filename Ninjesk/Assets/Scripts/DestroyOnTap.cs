using UnityEngine;

public class DestroyOnTap : MonoBehaviour
{
  
    void Update()
    {
        if (Player.isRunning)
            Destroy(gameObject);
    }

}
