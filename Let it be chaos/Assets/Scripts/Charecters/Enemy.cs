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

    float timer = 0;

    EnemyAI enemyAI;
    protected override void Start()
    {
        base.Start();
        animator.runtimeAnimatorController = enemyData.animatorController;
        gameObject.name = enemyData.enemyName;
        SetStats();
        enemyAI = GetComponent<EnemyAI>();
        enemyAI.GetEnemyStats(speed, jumpForce,attackRange);
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

        switch (enemyData.enemySize)
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
                transform.localScale *= 1.5f;
                break;
            case EnemySize.Big:
                maxHealth = ChaosManager.bigEnemyHealth;
                speed = ChaosManager.bigEnemySpeed;
                damage = ChaosManager.bigEnemyDamage;
                transform.localScale *= 2f;
                break;
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
                break;
            case TypeOfAttack.Melee_Suicidal:
                break;
            case TypeOfAttack.Range_Proyectile:
                break;
            case TypeOfAttack.Range_Beam:
                break;
        }
    }
   
}
