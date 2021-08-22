using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected string weaponName;
    protected string descriptionWeapon { get; set; }

    [SerializeField]
    protected int damage = 1;
    [SerializeField]    
    protected float attackSpeed = 1;
    [SerializeField]
    protected GameObject proyectile;

    private float shootCD;

    protected virtual void Update()
    {
        shootCD -= Time.deltaTime;
    }

    public virtual void Shoot(Vector3 spawnPos)
    {
        if(shootCD <= 0)
        {
            Instantiate<GameObject>(proyectile, spawnPos, Quaternion.identity,transform);
        }
    }
}
