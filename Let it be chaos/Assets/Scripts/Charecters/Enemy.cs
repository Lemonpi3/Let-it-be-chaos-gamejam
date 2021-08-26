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
    private int damage;
    [SerializeField]
    TypeOfAttack typeOfAttack;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float attackSpeed;
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
    private Weapon enemyWeaponScript;

    private float timer = 0;
    private Charecter target;
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
        }
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
        float chaos = (ChaosManager.EnemyChaosLevel - (ChaosManager.EnemyChaosLevel * chaosResistance));
        maxHealth *= (int)Mathf.Abs(chaos);
        speed = Mathf.Abs(speed *= chaos);
        jumpForce = defaultGravityScale;
        damage *= (int)Mathf.Abs(chaos);
        
        if(enemyWeaponScript != null)
        {
            enemyWeaponScript.UpdateChaos();
        }
        base.UpdateStats();
    }

    public override void SetCharecterDefaultStats()
    {
        SetStats();
    }
}
