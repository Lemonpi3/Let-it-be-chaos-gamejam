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
    public float attackSpeed = 1;
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
    [SerializeField]
    private Transform bulletParentTransform;
    private Vector2 direction;

    protected void Start()
    {
        playerTransform = gameObject.GetComponentInParent<Transform>();
    }
    public bool Shoot(Transform spawnPos, Vector2 direction = new Vector2())
    {
        Vector2 shootDir;
        if(direction != Vector2.zero)
        {
            shootDir = direction;
        }
        else
        {
            shootDir = spawnPos.forward;
        }

        if (isShooting)
        {
            return false;
        }

        StartCoroutine(Shooting());
        GameObject _proyectile =Instantiate<GameObject>(proyectile, spawnPos.position, Quaternion.identity, bulletParentTransform);
        _proyectile.GetComponent<Proyectile>().GetStats(shootDir,damage, proyectileSpeed,proyectileRadius,weaponRange,explosiveWeapon,explosionRange);
        return true;
    }
    protected IEnumerator Shooting()
{
        isShooting = true;
        yield return new WaitForSeconds(attackSpeed);
        isShooting = false;
    }
    public void SetWeaponStats(int _damage, float _attackSpeed, float _range,bool isExplosive = false,float _explotionRange = 1, float _proyectileSpeed = 10, float _proyectileRadius = 1)
    {
        damage = _damage;
        attackSpeed = _attackSpeed;
        weaponRange = _range;
        explosionRange = _explotionRange;
        proyectileSpeed = _proyectileSpeed;
        proyectileRadius = _proyectileRadius;
        explosiveWeapon = isExplosive;
    }
}
