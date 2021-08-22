using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosManager : MonoBehaviour
{
    [SerializeField]
    private int chaosLevel = 1;
    [SerializeField]
    public static float EnemyChaosLevel = 1;
    [SerializeField]
    public static float PlayerChaosLevel = 1;
    [SerializeField]
    public static float WeaponChaosLevel = 1;
    [SerializeField]
    public static float PhysicsChaosLevel = 1;

    public void RandomizeChaos()
    {
        EnemyChaosLevel = Random.Range(0, chaosLevel);
        PlayerChaosLevel = Random.Range(0, chaosLevel);
        WeaponChaosLevel = Random.Range(0, chaosLevel);
        PhysicsChaosLevel = Random.Range(0, chaosLevel);
    }

    public void IncreaseChaos(int amount)
    {
        chaosLevel += amount;
        RandomizeChaos();
    }
}
