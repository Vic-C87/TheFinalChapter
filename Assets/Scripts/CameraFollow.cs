using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform myTarget;

    Vector3 myPosition;

    void LateUpdate()
    {
        myPosition = myTarget.position;
        myPosition.z = -10;
        transform.position = myPosition;
    }
}
