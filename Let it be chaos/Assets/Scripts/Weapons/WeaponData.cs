using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Stats")]
    [SerializeField]
    private string _weaponName;
    public string weaponName => _weaponName;

    [SerializeField]
    private int _damage = 1;
    public int damage => _damage; 

    [SerializeField]
    private float _attackSpeed = 1;
    public float attackSpeed => _attackSpeed;

    [SerializeField]
    private float _attackRange = 8;
    public float attackRange => _attackRange;

    [SerializeField]
    private bool _isExplosive;
    public bool isExplosive => _isExplosive;

    [Header("Proyectile Stats")]
    [SerializeField]
    private float _proyectileRadius = 1;
    public float proyectileRadius => _proyectileRadius;

    [SerializeField]
    private float _proyectileSpeed = 10;
    public float proyectileSpeed => _proyectileSpeed;

    [SerializeField]
    private float _proyectileGravityScale = 0;
    public float proyectileGravityScale => _proyectileGravityScale;

    [Header("Explosive Stats")]
    [SerializeField]
    private float _explosiveRange = 1;
    public float explosiveRange => _explosiveRange;

    [SerializeField]
    private int _explosiveDamage = 1;
    public int explosiveDamage => _explosiveDamage;

    [SerializeField]
    private GameObject _proyectilePrefab;
    public GameObject proyectilePrefab => _proyectilePrefab;

}
