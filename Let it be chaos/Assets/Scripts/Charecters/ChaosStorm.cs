using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosStorm : EnviromentHazard
{
    private float gravityModifier;
    private int maxHealthModifier;
    private float speedModifier;

    private float lifeTime =1;
    private int minSpawnChance;

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void SpecialHazzard(Charecter charecter)
    {
        if(charecter.tag == "Player")
        {
            charecter.ModifyStats(maxHealthModifier, speedModifier, gravityModifier);
            charecter.Heal(Random.Range(0, damage * 2));
        }
    }
    public void GetStats(int _damage,float _tickSpeed,float _gravityModifier,int _maxHealthModifier,float _speedModifier,float _lifeTime)
    {
        damage = Random.Range(_damage, damage * 2);
        tickSpeed = _tickSpeed;
        gravityModifier = _gravityModifier;
        maxHealthModifier = _maxHealthModifier;
        speedModifier = _speedModifier;
        lifeTime = _lifeTime;

        int extraSpawnChance = (int)Random.Range(minSpawnChance, ChaosManager._chaosLevelMax * 1.5f );
        if(extraSpawnChance <= ChaosManager._chaosLevel)
        {
            minSpawnChance = extraSpawnChance;
            Instantiate(this.gameObject, transform.position * Random.Range(-3,3), Quaternion.identity);
        }
    }
    
}
