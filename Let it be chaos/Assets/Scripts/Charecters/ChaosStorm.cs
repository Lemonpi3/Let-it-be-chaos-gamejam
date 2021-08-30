using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosStorm : EnviromentHazard
{
    private float gravityModifier;
    private int maxHealthModifier;
    private float speedModifier;

    private float lifeTime =1;
    private Charecter player;

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
            charecter.ApplyChaosStats();
            player = charecter;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.SetCharecterDefaultStats();
    }

    public void GetStats(int _damage,float radius,float _tickSpeed,float _lifeTime)
    {
        damage = _damage;
        tickSpeed = _tickSpeed;
        lifeTime = _lifeTime;
        transform.localScale *= radius;
    }
}
