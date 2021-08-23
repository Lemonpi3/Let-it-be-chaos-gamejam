using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    [SerializeField]
    private float smoothSpeed = 0.125f;
    [SerializeField]
    private Vector3 offSet;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 disiredPos = target.position + offSet;
        Vector3 finalPos = Vector3.SmoothDamp(transform.position, disiredPos,ref velocity, smoothSpeed);
        transform.position = finalPos;
    }
}
