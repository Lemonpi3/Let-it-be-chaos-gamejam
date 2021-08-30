using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected WeaponData weaponData;
    [SerializeField]
    protected string weaponName;
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
    protected float explotionRange = 1;
    [SerializeField]
    protected int explotionDamage=1;
    [SerializeField]
    protected GameObject proyectile;
    [SerializeField]
    protected float proyectileGravityScale = 0;
    
    protected bool isShooting;
  
    [SerializeField]
    private Transform bulletParentTransform;

    protected void Start()
    {
        if(weaponData == null)
        {
            Debug.LogWarning("No WeaponData in: " + gameObject.name + " at: " + gameObject.transform.parent.name);
            Destroy(gameObject);
        }
        ResetStats();
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
        SpawnBullet(shootDir, spawnPos);

        return true;
    }
    protected IEnumerator Shooting()
{
        isShooting = true;
        yield return new WaitForSeconds(attackSpeed);
        isShooting = false;
    }
    public void StopShooting()
    {
        StopCoroutine(Shooting());
        isShooting = false;
    }

    public void UpdateChaos()
    {
        damage = ChaosManager.instance.proyectileDamage_ChaosMod;
        attackSpeed = ChaosManager.instance.weaponAttackSpeed_ChaosMod;
        proyectile = ChaosManager.instance.proyectilePool;
        proyectileGravityScale = ChaosManager.instance.proyectileGravity_ChaosMod;
    }

    public void ResetStats()
    {
        weaponName = weaponData.weaponName;
        damage = weaponData.damage;
        attackSpeed = weaponData.attackSpeed;
        weaponRange = weaponData.attackRange;

        proyectileRadius = weaponData.proyectileRadius;
        proyectileSpeed = weaponData.proyectileSpeed;
        proyectileGravityScale = weaponData.proyectileGravityScale;

        explosiveWeapon = weaponData.isExplosive;
        explotionRange = weaponData.explosiveRange;
        explotionDamage = weaponData.explosiveDamage;
        proyectile = weaponData.proyectilePrefab;
    }

    public virtual void SpawnBullet(Vector2 shootDir,Transform spawnPos)
    {
        GameObject _proyectile = Instantiate<GameObject>(proyectile, spawnPos.position, Quaternion.identity, bulletParentTransform);
        _proyectile.GetComponent<Proyectile>().GetStats(shootDir, damage, proyectileSpeed, proyectileRadius, weaponRange, explosiveWeapon, explotionRange, proyectileGravityScale,explotionDamage);
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }
}
