using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosManager : MonoBehaviour
{
    //Actually is the game manager >:)
    #region Singleton
    public static ChaosManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("There can only be one ChaosManager instance. Destroying: " + gameObject.name);
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    //GeneralGameSettings
    
    //Enemy Stats
    [Header("Enemy Health")]
    [SerializeField]
    private int baseEnemyHealth = 10;
    [SerializeField]
    private int smallHealthScaling = 1;
    [SerializeField]
    private int medHealthScaling = 3;
    [SerializeField]
    private int bigHealthScaling = 5;
    [Header("Enemy Damage")]
    [SerializeField]
    private int baseDamage = 1;
    [SerializeField]
    private int smallEnemyScaling = 1;
    [SerializeField]
    private int medEnemyScaling = 5;
    [SerializeField]
    private int bigEnemyScaling = 10;
    [Header("Enemy Speed")]
    [SerializeField]
    private float baseEnemySpeed = 10;
    [SerializeField]
    private float smallSpeedScaling = 1.5f;
    [SerializeField]
    private float medSpeedScaling = 1f; 
    [SerializeField]
    private float bigSpeedScaling = 0.8f;

    public static int smallEnemyHealth => instance.baseEnemyHealth * instance.smallHealthScaling;
    public static int medEnemyHealth => instance.baseEnemyHealth * instance.medHealthScaling;
    public static int bigEnemyHealth => instance.baseEnemyHealth * instance.bigHealthScaling;
    
    public static int smallEnemyDamage => instance.baseDamage * instance.smallEnemyScaling;
    public static int medEnemyDamage => instance.baseDamage * instance.medEnemyScaling;
    public static int bigEnemyDamage => instance.baseDamage * instance.bigEnemyScaling;

    public static float smallEnemySpeed =>instance.baseEnemySpeed * instance.smallSpeedScaling;
    public static float medEnemySpeed =>instance.baseEnemySpeed* instance.medSpeedScaling;
    public static float bigEnemySpeed => instance.baseEnemySpeed * instance.bigSpeedScaling;


    [Header("Chaos Settings")]
    [SerializeField]
    private static int chaosLevel = 1;

    public int currentChaosLevel { get => chaosLevel; }

    [SerializeField]
    public static float EnemyChaosLevel = 1;
    [SerializeField]
    public static float PlayerChaosLevel = 1;
    [SerializeField]
    public static float WeaponChaosLevel = 1;
    [SerializeField]
    public static float PhysicsChaosLevel = 1;

    public static void RandomizeChaos()
    {
        EnemyChaosLevel = Random.Range(0, chaosLevel);
        PlayerChaosLevel = Random.Range(0, chaosLevel);
        WeaponChaosLevel = Random.Range(0, chaosLevel);
        PhysicsChaosLevel = Random.Range(0, chaosLevel);
    }

    public static void IncreaseChaos(int amount)
    {
        chaosLevel += amount;
        RandomizeChaos();
    }
}
