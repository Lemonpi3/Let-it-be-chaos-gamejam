using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : Boss
{
    [Header("Boss Stats")]
    [SerializeField]
    int chaosLevelModifierMin;
    int chaosLevelModifierMax;

    [SerializeField,Range(0,1)]
    float enragePercent= 0.3f;

    [Header("ChaosStorm")]

    [SerializeField]
    int chaosStormDamage;

    [SerializeField]
    float chaosStormRadius;

    [SerializeField]
    float chaosStormTickSpeed;

    [SerializeField]
    float lifeTime;

    [SerializeField]
    float chaosStormCD;

    [SerializeField]
    float timeToCastFirstChaosStorm;

    [SerializeField]
    GameObject chaosStormGO;
    
    [SerializeField]
    GameObject exitPortalGO;

    [SerializeField]
    GameObject bossChaosZone;

    private float gravityModifier;
    private int maxHealthModifier;
    private float speedModifier;
    

    protected override void Start()
    {
        base.Start();
        InvokeRepeating("SpawnChaosStorm", timeToCastFirstChaosStorm, chaosStormCD);
    }

    private void Update()
    {
        if (enemy.GetCurrentHealthPercent() <= enragePercent && enemy.GetCurrentHealthPercent() > 0)
        {
            Enrage();
        }
    }

    public override void Die()
    {
        exitPortalGO.SetActive(true);
        bossChaosZone.SetActive(false);
        ChaosManager.instance.NewChaosLevel(chaosLevelModifierMin, chaosLevelModifierMax);
        Destroy(gameObject);
    }

    private void SpawnChaosStorm()
    {
        if (Vector2.Distance(transform.position, enemy.target.transform.position) <= enemy.attackRange*5)
        {
            ChaosStorm chaosStorm = Instantiate(chaosStormGO, enemy.target.gameObject.transform.position, Quaternion.identity, this.transform).GetComponent<ChaosStorm>();
            chaosStorm.GetStats(chaosStormDamage, chaosStormTickSpeed, chaosStormRadius, lifeTime);
        }
    }

    private void Enrage()
    {
        bossChaosZone.SetActive(true);
    }
}
