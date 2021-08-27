using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : Boss
{
    [Header("Boss Stats")]
    [SerializeField]
    int chaosLevelModifier;
    
    [Header("ChaosStorm")]
    [SerializeField]
    int chaosStormDamage;
    [SerializeField]
    float chaosStormStatModifier;
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

    public override void Die()
    {
        exitPortalGO.SetActive(true);
        bossChaosZone.SetActive(false);
        ChaosManager.instance.modifyChaosLevel(chaosLevelModifier);
        Destroy(gameObject);
    }

    private void SpawnChaosStorm()
    {
        if (Vector2.Distance(transform.position, enemy.target.transform.position) <= enemy.attackRange*5)
        {
            gravityModifier = Random.Range(-chaosStormStatModifier, chaosStormStatModifier);
            if (gravityModifier == 0)
            {
                gravityModifier = 1;
            }
            maxHealthModifier = chaosStormDamage + (int)Random.Range(-chaosStormStatModifier, chaosStormStatModifier);
            speedModifier = Random.Range(-chaosStormStatModifier, chaosStormStatModifier);
            lifeTime = Random.Range(0, lifeTime * (1 / chaosStormStatModifier));

            ChaosStorm chaosStorm = Instantiate(chaosStormGO, enemy.target.gameObject.transform.position, Quaternion.identity, this.transform).GetComponent<ChaosStorm>();
            chaosStorm.GetStats(chaosStormDamage, chaosStormTickSpeed, gravityModifier, maxHealthModifier, speedModifier, lifeTime);
        }
    }
}
