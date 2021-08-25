using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform pos1, pos2;
    [SerializeField]
    private Transform platform;
    [SerializeField]
    float speed = 1;

    Vector3 nextPos;
    void Start()
    {
        nextPos = pos1.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlataform();
    }

    void MovePlataform()
    {
        if(platform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if (platform.position == pos2.position)
        {
            nextPos = pos1.position;
        }
        platform.position = Vector3.MoveTowards(platform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
