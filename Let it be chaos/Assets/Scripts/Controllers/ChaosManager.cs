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
    [Header("Enemy Health")]
    //Enemy Stats                  //Inspector was showing this headers inversed
    [SerializeField, Header("Enemy Stats")]
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

    [Header("Weapons Chaos stuff")]
   
    public GameObject[] proyectilePool;

    [Header("Chaos Settings")]

    [SerializeField]
    private int chaosLevelCurrent = 1;
    [SerializeField]
    private int chaosLevelMax = 100;
    [SerializeField]
    private float ChaosUpdateInterval = 30;

    [Header("Default values")]
    
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

    [Header("General Chaos Scaling")]

    [Range(0, 1), SerializeField]
    private float chaosScalingPlayer=1;
    [Range(0, 1), SerializeField]
    private float chaosScalingPhysics=1;
    [Range(0, 1), SerializeField]
    private float chaosScalingEnemies=1;
    [Range(0, 1), SerializeField]
    private float chaosScalingWeapons=1;

    public static int _chaosLevel =>instance.chaosLevelCurrent;
    public static int _chaosLevelMax => instance.chaosLevelMax;
    public static float _chaosUpdateInterval => instance.ChaosUpdateInterval;
    public static float EnemyChaosLevel;
    public static float PlayerChaosLevel;
    public static float WeaponChaosLevel;
    public static float PhysicsChaosLevel;

    private void Start()
    {
        defaultChaosLevel = chaosLevelCurrent;
        PlayerChaosLevel = defaultPlayerChaosLevel;
        PhysicsChaosLevel = defaultPhysicsChaosLevel;
        WeaponChaosLevel = defaultWeaponChaosLevel;
        EnemyChaosLevel = defaultEnemyChaosLevel;
        InvokeRepeating("RandomizeChaos", 0, ChaosUpdateInterval);
    }

    public void RandomizeChaos()
    {
        EnemyChaosLevel = Random.Range(-_chaosLevel * chaosScalingEnemies, _chaosLevel * chaosScalingEnemies);
        PlayerChaosLevel = Random.Range(-_chaosLevel * chaosScalingPlayer, _chaosLevel*chaosScalingPlayer);
        WeaponChaosLevel = Random.Range(-1, _chaosLevel * chaosScalingWeapons);
        PhysicsChaosLevel = Random.Range(-1, _chaosLevel*chaosScalingPhysics);

        if(PhysicsChaosLevel <= 0 && PhysicsChaosLevel > -1)
        {
            PhysicsChaosLevel = -1;
        }
        if(WeaponChaosLevel <= 0 || WeaponChaosLevel > -1)
        {
            WeaponChaosLevel = -1;
        }
    }

    public void modifyChaosLevel(int amount)
    {
        instance.chaosLevelCurrent += amount;
        if(chaosLevelCurrent < chaosLevelMax)
        {
            chaosLevelCurrent = chaosLevelMax;
        }
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
