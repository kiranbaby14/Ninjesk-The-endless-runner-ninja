using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform lookAt;
    public Vector3 Offset;
    public Vector3 rotation;

    public bool IsMoving{ set; get; }



    void LateUpdate()
    {
        if (IsMoving)
        {
            Vector3 desiredPosition = lookAt.position + Offset;
            desiredPosition.y = 2.04f;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, 3f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 0.06f);
           
        }

    }
}
