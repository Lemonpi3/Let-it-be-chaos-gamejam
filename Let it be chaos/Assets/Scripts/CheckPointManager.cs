using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField]
    private CheckPoint[] checkPoints;
    
    void Start()
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            checkPoints[i].checkpointNumber = i;
        }
    }
    
}
