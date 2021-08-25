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
    private int chaosLevelCurrent = 1;
    [SerializeField]
    private int chaosLevelMax = 10;
    [SerializeField]
    private int defaultChaosLevel = 1;
    [SerializeField]
    private int defaultEnemyChaosLevel = 1;
    [SerializeField]
    private int defaultPlayerChaosLevel = 1;
    [SerializeField]
    private int defaultWeaponChaosLevel = 1;
    [SerializeField]
    private int defaultPhysicsChaosLevel = 1;

    public static int _chaosLevel =>instance.chaosLevelCurrent;
    public static int _chaosLevelMax => instance.chaosLevelMax;

    public static float EnemyChaosLevel;
    public static float PlayerChaosLevel;
    public static float WeaponChaosLevel;
    public static float PhysicsChaosLevel;

    private void Start()
    {
        EnemyChaosLevel = defaultEnemyChaosLevel;
        PlayerChaosLevel = defaultPlayerChaosLevel;
        PhysicsChaosLevel = defaultPhysicsChaosLevel;
        WeaponChaosLevel = defaultWeaponChaosLevel;
  
    }

    public void RandomizeChaos()
    {
        EnemyChaosLevel = Random.Range(-_chaosLevel, _chaosLevel);
        PlayerChaosLevel = Random.Range(-_chaosLevel, _chaosLevel);
        WeaponChaosLevel = Random.Range(-_chaosLevel, _chaosLevel);
        PhysicsChaosLevel = Random.Range(-_chaosLevel, _chaosLevel);
    }

    public void modifyChaosLevel(int amount)
    {
        instance.chaosLevelCurrent += amount;
        RandomizeChaos();
    }

    public void ResetChaos()
    {
        chaosLevelCurrent = defaultChaosLevel;
        EnemyChaosLevel = defaultEnemyChaosLevel;
        PlayerChaosLevel = defaultPlayerChaosLevel;
        WeaponChaosLevel = defaultWeaponChaosLevel;
        PhysicsChaosLevel = defaultPhysicsChaosLevel;
    }
}
