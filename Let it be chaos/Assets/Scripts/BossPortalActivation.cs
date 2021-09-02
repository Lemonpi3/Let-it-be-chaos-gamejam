using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPortalActivation : MonoBehaviour
{
    public GameObject[] portals;

    void Start()
    {
        for (int i = 0; i < ChaosManager.instance.bossesKilled.Length; i++)
        {
            if (ChaosManager.instance.bossesKilled[i])
            {
                portals[i].SetActive(false);
            }
            else portals[i].SetActive(true);
        }
    }
}
