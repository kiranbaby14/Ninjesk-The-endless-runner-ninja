using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform lookAt;

    
    void LateUpdate()
    {
        Vector3 desiredPosition = lookAt.position;
        desiredPosition.y = 3f;
        
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 3f);
    }
}
