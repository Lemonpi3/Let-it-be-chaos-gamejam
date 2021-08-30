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
    public float attackRange;
    [SerializeField]
    protected float attackSpeed;

    [SerializeField]
    TypeOfAttack typeOfAttack;
    [SerializeField]
    private EnemySize enemySize;

    [Header("Suicidal & Ranged settings")]
    [SerializeField]
    private GameObject specialAttack; //proyectile,beam,explotion
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

    [Header("BossStuff")]
    public bool inmuneToChaos;
    [SerializeField]
    Boss bossScript;
    private Weapon enemyWeaponScript;

    private float timer = 0;
    public Charecter target;
    EnemyAI enemyAI;

    private void Awake()
    {
        if (enemyData == null)
        {
            Debug.Log("No enemyData found in: " + gameObject.name);
            Destroy(gameObject);
        }
    }

    protected override void Start()
    {
        base.Start();
        animator.runtimeAnimatorController = enemyData.animatorController;
        SetStats();
        Heal(maxHealth);
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
        gameObject.name = enemyData.enemyName;
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
                inmuneToChaos = true;
                break;
        }

        if (specialAttack != null)
        {
            if (specialAttack.gameObject.tag == "Weapon")
            {
                enemyWeapon = Instantiate(specialAttack, transform.position, Quaternion.identity, transform);
                Debug.Log(enemyWeapon);
                enemyWeaponScript = enemyWeapon.GetComponent<Weapon>();
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

    public override void ApplyChaosStats()
    {
        if(inmuneToChaos)
        {
            return;
        }

        damage = ChaosManager.instance.enemyDamage_ChaosMod;
        rb.gravityScale =defaultGravityScale + (defaultGravityScale * ChaosManager.instance.enemyGravity_ChaosMod);
        speed = defaultSpeed + (defaultSpeed * ChaosManager.instance.enemySpeed_ChaosMod);
        jumpForce = ChaosManager.instance.enemyJumpForce_ChaosMod;

        if(enemyWeapon != null)
        {
            enemyWeaponScript.UpdateChaos();
        }
        FlipUpsideDown(enemyAI.groundCheck);
    }

    public override void SetCharecterDefaultStats(bool heal = false)
    {
        base.SetCharecterDefaultStats();
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
