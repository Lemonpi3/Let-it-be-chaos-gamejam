using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("EnemyChaosRNG Values")]
    [SerializeField, Tooltip("Modifies Multiplicative")]
    private float[] _enemyGravity_ChaosMod;
    [SerializeField]
    private int[] _enemyDamage_ChaosMod;
    [SerializeField,Tooltip("Modifies Multiplicative")]
    private float[] _enemySpeed_ChaosMod;
    [SerializeField]
    private float[] _enemyJumpForce_ChaosMod;

    
    public int enemyDamage_ChaosMod { 
        get 
        {
           int rng = Random.Range(0, (_enemyDamage_ChaosMod.Length * (chaosLevelCurrent / chaosLevelMax)));
           return _enemyDamage_ChaosMod[rng];
        } 
    }

    public float enemyGravity_ChaosMod { 
        get
        {
            int rng = Random.Range(0, (_enemyGravity_ChaosMod.Length * (chaosLevelCurrent / chaosLevelMax)));
            return _enemyGravity_ChaosMod[rng];
        }
    }

    public float enemySpeed_ChaosMod {
        get 
        {
           int rng = Random.Range(0, (_enemySpeed_ChaosMod.Length * (chaosLevelCurrent / chaosLevelMax)));
           return _enemySpeed_ChaosMod[rng];
        }
    }

    public float enemyJumpForce_ChaosMod {
        get 
        {   
            int rng = Random.Range(0, (_enemyJumpForce_ChaosMod.Length * (chaosLevelCurrent / chaosLevelMax)));
            return  _enemyJumpForce_ChaosMod[rng];
        }
    }

    public static int smallEnemyHealth =>(int) instance.baseEnemyHealth * instance.smallHealthScaling;
    public static int medEnemyHealth => (int)instance.baseEnemyHealth * instance.medHealthScaling;
    public static int bigEnemyHealth => (int)instance.baseEnemyHealth * instance.bigHealthScaling;
    
    public static int smallEnemyDamage => (int)instance.baseDamage * instance.smallEnemyScaling;
    public static int medEnemyDamage => (int)instance.baseDamage * instance.medEnemyScaling;
    public static int bigEnemyDamage => (int)instance.baseDamage * instance.bigEnemyScaling;

    public static float smallEnemySpeed =>instance.baseEnemySpeed * instance.smallSpeedScaling;
    public static float medEnemySpeed =>instance.baseEnemySpeed* instance.medSpeedScaling;
    public static float bigEnemySpeed => instance.baseEnemySpeed * instance.bigSpeedScaling;

    /*-----------------------------------------------------*/

    [Header("playerChaosRNG Values")]
    [SerializeField]
    private int[] _playerGravity_ChaosMod;
    [SerializeField]
    private int[] _playerMaxHealth_ChaosMod;
    [SerializeField]
    private float[] _playerSpeed_ChaosMod;
    [SerializeField]
    private float[] _playerJumpForce_ChaosMod;

    public int playerGravity_ChaosMod
    {   
        get
        {
            int rng = Random.Range(0, (_playerGravity_ChaosMod.Length * (chaosLevelCurrent / chaosLevelMax)));
            return _playerGravity_ChaosMod[rng];
        }
    }
    public int playerMaxHealth_ChaosMod{
        get
        {
            int rng = Random.Range(0, (_playerMaxHealth_ChaosMod.Length * (chaosLevelCurrent / chaosLevelMax)));
            return _playerMaxHealth_ChaosMod[rng];
        }
    }
    public float playerSpeed_ChaosMod
    {
        get
        {
            int rng = Random.Range(0, (_playerSpeed_ChaosMod.Length * (chaosLevelCurrent / chaosLevelMax)));
            return _playerSpeed_ChaosMod[rng];
        }
    }
    public float playerJumpForce_ChaosMod
    {
        get
        {
            int rng = Random.Range(0, (_playerJumpForce_ChaosMod.Length * (chaosLevelCurrent / chaosLevelMax)));
            return _playerJumpForce_ChaosMod[rng];
        }
    }

    /*-----------------------------------------------------*/

    [Header("Weapons Chaos stuff")]

    [SerializeField]
    private GameObject[] _proyectilePool;
    [SerializeField]
    private int[] _proyectileGravity_ChaosMod;
    [SerializeField]
    private int[] _proyectileDamage_ChaosMod;
    [SerializeField]
    private float[] _weaponAttackSpeed_ChaosMod;
    
    //Returns one random value of its respective array
    public GameObject proyectilePool
    { 
        get
        {
            int rng = Random.Range(0, _proyectilePool.Length);
            return _proyectilePool[rng];
        } 
    }

    public int proyectileGravity_ChaosMod
    { 
        get 
        {
            int rng = Random.Range(0, (_proyectileGravity_ChaosMod.Length * (chaosLevelCurrent / chaosLevelMax)));
            return _proyectileGravity_ChaosMod[rng];
        } 
    }

    public int proyectileDamage_ChaosMod
    {
        get
        {
            int rng = Random.Range(0, (_proyectileDamage_ChaosMod.Length * (chaosLevelCurrent / chaosLevelMax)));
            return _proyectileDamage_ChaosMod[rng];
        }
    }

    public float weaponAttackSpeed_ChaosMod 
    { 
        get
        {
            int rng = Random.Range(0, (_weaponAttackSpeed_ChaosMod.Length * (chaosLevelCurrent / chaosLevelMax)));
            return _weaponAttackSpeed_ChaosMod[rng];
        }
    }

    /*-----------------------------------------------------*/

    [Header("Chaos Settings")]

    [SerializeField]
    private int chaosLevelCurrent = 1;
    [SerializeField]
    private int chaosLevelMax = 100;
    [SerializeField]
    private int chaosLevelMin = 1;
    [SerializeField]
    private float ChaosUpdateInterval = 30;

    /*-----------------------------------------------------*/

    [Header("Default values")]
    
    [SerializeField]
    private int defaultChaosLevel = 1;

    public static int _chaosLevel =>instance.chaosLevelCurrent;
    public static int _chaosLevelMax => instance.chaosLevelMax;
    public static float _chaosUpdateInterval => instance.ChaosUpdateInterval;

    /*-----------------------For save System-------------------------*/
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public int level;
    [HideInInspector]
    public bool[] bossesKilled = new bool[4];


    private void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        player = FindObjectOfType<Player>();
        InvokeRepeating("RandomizeChaos", 0, ChaosUpdateInterval);
    }

    public void RandomizeChaos()
    {
        //chaosLevelCurrent = Random.Range(chaosLevelMin, chaosLevelMax);
    }

    public void NewChaosLevel(int min,int max)
    {
        chaosLevelMin = min;
        chaosLevelMax = max;
        RandomizeChaos();
    }

    public void ResetAllChaos()
    {
        chaosLevelCurrent = defaultChaosLevel;
        RandomizeChaos();
    }

    public void SavePlayer(Player player)
    {
        SaveSystem.SavePlayer(player);
    }
}
