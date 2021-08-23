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
    protected float weaponRange = 5;
    [SerializeField]
    protected float proyectileSpeed = 10;
    [SerializeField]
    protected float proyectileRadius = 1;
    [SerializeField]
    protected bool explosiveWeapon;
    [SerializeField]
    protected float explosionRange = 1;
    [SerializeField]
    protected GameObject proyectile;

    protected bool isShooting;
    Transform playerTransform;

    protected void Start()
    {
        playerTransform = gameObject.GetComponentInParent<Transform>();
    }
    public bool Shoot(Vector3 spawnPos)
    {
        if (isShooting)
        {
            return false;
        }

        StartCoroutine(Shooting());
        GameObject _proyectile =Instantiate<GameObject>(proyectile, spawnPos, Quaternion.identity, transform);
        _proyectile.GetComponent<Proyectile>().GetStats(playerTransform.forward,damage, proyectileSpeed,proyectileRadius,weaponRange,explosiveWeapon,explosionRange);
        return true;
    }
    protected IEnumerator Shooting()
{
        isShooting = true;
        yield return new WaitForSeconds(attackSpeed);
        isShooting = false;
    }
}
