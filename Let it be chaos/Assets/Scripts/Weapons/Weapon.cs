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
    [SerializeField]
    protected float proyectileGravityScale = 0;

    protected int defaultDmg;
    protected float defaultAttackSpeed;
    protected float defaultGravityScale;
    protected GameObject defaultProyectile;

    protected bool isShooting;
    Transform playerTransform;
    [SerializeField]
    private Transform bulletParentTransform;
    private Vector2 direction;
    private float chaos;

    protected void Start()
    {
        playerTransform = gameObject.GetComponentInParent<Transform>();
        defaultDmg = damage;
        defaultGravityScale = proyectileGravityScale;
        defaultAttackSpeed = attackSpeed;
        defaultProyectile = proyectile;
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

    public void UpdateChaos(float chaosResistance = 0)
    {
        chaos = (ChaosManager.WeaponChaosLevel - (ChaosManager.WeaponChaosLevel * chaosResistance));

        damage = (int)Mathf.Clamp(defaultDmg * Mathf.Abs(chaos), 1, damage * Mathf.Abs((chaos)));
        attackSpeed = Mathf.Clamp(defaultAttackSpeed * Mathf.Abs(chaos), 0.1f, 3f);
        proyectileGravityScale = 10 * chaos;
        if (ChaosManager.instance.proyectilePool[0] != null)
        {
            proyectile = ChaosManager.instance.proyectilePool[(int)Random.Range(0, ChaosManager.instance.proyectilePool.Length)];
        }
      //  Debug.Log("Weapon chaos Level: "+ chaos+ weaponName + " Damage: " + damage + " attackSpeed: " + attackSpeed + " proyectile and gravity scale: " + proyectile.name + " " + proyectileGravityScale);
    }

    public void ResetStats()
    {
        damage =defaultDmg;
        attackSpeed = defaultAttackSpeed;
        proyectileGravityScale = defaultGravityScale;
        proyectile = defaultProyectile;
        chaos = 0;
    }

    public virtual void SpawnBullet(Vector2 shootDir,Transform spawnPos)
    {
        GameObject _proyectile = Instantiate<GameObject>(proyectile, spawnPos.position, Quaternion.identity, bulletParentTransform);
        _proyectile.GetComponent<Proyectile>().GetStats(shootDir, damage, proyectileSpeed, proyectileRadius, weaponRange, explosiveWeapon, explosionRange, proyectileGravityScale);
    }
}
