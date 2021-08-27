using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Charecter
{
    [Header("EnemyStats")]
    [SerializeField]
    private EnemyData enemyData;
    [SerializeField]
    protected int damage;
    [SerializeField]
    TypeOfAttack typeOfAttack;
    [SerializeField]
    public float attackRange;
    [SerializeField]
    protected float attackSpeed;
    [SerializeField]
    private EnemySize enemySize;
    [Header("Suicidal & Ranged settings")]
    [SerializeField]
    private GameObject specialAttack; //proyectile,beam,expltion
    [SerializeField]
    private float specialRadius;
    [SerializeField]
    private Transform specialSpawn;
    [SerializeField]
    private GameObject enemyWeapon;
    [Header("Loot")]
    [SerializeField]
    private GameObject[] loot = new GameObject[1];
    [SerializeField,Range(0,100)]
    private float lootChance;
    public bool isBoss = false;
    [SerializeField]
    Boss bossScript;
    private Weapon enemyWeaponScript;

    private int defaultDamage;

    private float timer = 0;
    public Charecter target;
    EnemyAI enemyAI;

    private void Awake()
    {
        if (enemyData == null && !isBoss)
        {
            Debug.Log("No enemyData found in: " + gameObject.name);
            Destroy(gameObject);
        }
    }

    protected override void Start()
    {
        base.Start();
        animator.runtimeAnimatorController = enemyData.animatorController;
        gameObject.name = enemyData.enemyName;
        SetStats();
        currentHealth = maxHealth;
        enemyAI = GetComponent<EnemyAI>();
        enemyAI.GetEnemyStats(speed, jumpForce,attackRange);
        target = enemyAI.target.gameObject.GetComponent<Charecter>();
        
    }
    
    protected override void Update()
    {
        base.Update();
    }

    private void SetStats()
    {
        typeOfAttack = enemyData.typeOfAttack;
        attackRange = enemyData.attackRange;
        attackSpeed = enemyData.attackSpeed;
        enemySize = enemyData.enemySize;
        specialRadius = enemyData.specialRadius;
        specialAttack = enemyData.specialAttack;
       
        switch (enemySize)
        {
            case EnemySize.Small:
                maxHealth = ChaosManager.smallEnemyHealth;
                speed = ChaosManager.smallEnemySpeed;
                damage = ChaosManager.smallEnemyDamage;
                break;
            case EnemySize.Medium:
                maxHealth = ChaosManager.medEnemyHealth;
                speed = ChaosManager.medEnemySpeed;
                damage = ChaosManager.medEnemyDamage;
                transform.localScale = Vector3.one * 1.5f;
                break;
            case EnemySize.Big:
                maxHealth = ChaosManager.bigEnemyHealth;
                speed = ChaosManager.bigEnemySpeed;
                damage = ChaosManager.bigEnemyDamage;
                transform.localScale =Vector3.one * 2f;
                break;
            case EnemySize.Boss:
                maxHealth = enemyData.maxHealth;
                damage = enemyData.damage;
                speed = enemyData.speed;
                rb.gravityScale = enemyData.gravityScale;
                break;
        }

        defaultDamage = damage;

        if (specialAttack != null)
        {
            if (specialAttack.gameObject.tag == "Weapon")
            {
                enemyWeapon = Instantiate(specialAttack, transform.position, Quaternion.identity, transform);
                Debug.Log(enemyWeapon);
                enemyWeaponScript = enemyWeapon.GetComponent<Weapon>();
                enemyWeaponScript.attackSpeed = attackSpeed;
                enemyWeaponScript.SetWeaponStats(damage, attackSpeed, attackRange);
            }
        }

    }

    public void GetDirection(Vector2 _direction)
    {
        direction = _direction;
    }
    
    public void Attacking(float callInterval)
    {
        timer -= callInterval;
        if(timer <= 0)
        {
            Attack();
            timer = attackSpeed;
        }
    }

    public void Attack()
    {
        Debug.Log("Attacking" + enemyAI.target);
        switch (typeOfAttack)
        {
            case TypeOfAttack.Melee:
                target.TakeDamage(damage);
                break;
            case TypeOfAttack.Melee_Suicidal:
                GameObject explotion = Instantiate<GameObject>(specialAttack, transform.position, Quaternion.identity);
                explotion.GetComponent<Explotion>().Explode(specialRadius, damage);
                Destroy(gameObject);
                break;
            case TypeOfAttack.Range_Proyectile:
                enemyWeaponScript.Shoot(specialSpawn);
                break;
            case TypeOfAttack.Range_Beam:
                break;
        }
    }
    public override void UpdateStats()
    {
        if(EnemySize.Boss == enemySize)
        {
            return;
        }
        float chaos = (ChaosManager.EnemyChaosLevel - (ChaosManager.EnemyChaosLevel * chaosResistance));
        maxHealth = Mathf.Clamp(defaultMaxHealth + (int)Random.Range(-defaultMaxHealth, chaos), 1, 100);
        Heal((int)Random.Range(1, maxHealth));
        speed = Mathf.Clamp(defaultSpeed * chaos, -defaultSpeed * 3, defaultSpeed * 3);
        if (speed == 0)
        {
            speed = defaultSpeed;
        }

        jumpForce = Mathf.Clamp(defaultJumpForce + (defaultJumpForce * chaos), 0, defaultJumpForce * 3);
        damage =(int) Mathf.Clamp(defaultDamage + (defaultDamage * chaos), 1, defaultDamage * 2);

        if (enemyWeaponScript != null)
        {
            enemyWeaponScript.UpdateChaos();
        }
        base.UpdateStats();
        //Debug.Log(gameObject.name + " MaxHealth: " + maxHealth + " Speed: " + speed + " JumpForce: " + jumpForce + " Damage "+damage+ " GrabScale: " + rb.gravityScale);
    }

    public override void SetCharecterDefaultStats()
    {
        SetStats();
    }
    public override void Die()
    {
        if(enemySize == EnemySize.Boss)
        {
            bossScript.Die();
        }
        DropLoot();
        base.Die();
    }
    public void DropLoot()
    {
        if(loot[0]!= null)
        {
            for (int i = 0; i < loot.Length; i++)
            {
                float rng = Random.Range(0, 100);
                if(rng <= lootChance)
                {
                    Instantiate(loot[i], transform.position, Quaternion.identity);
                }
            }
        }
    }
        private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
