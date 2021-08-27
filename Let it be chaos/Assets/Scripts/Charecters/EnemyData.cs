using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Enemy",menuName ="New Charecter/New Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Grafics")]
    [SerializeField]
    private string _enemyName;
    public string enemyName { get => _enemyName; }

    [SerializeField]
    private Sprite _sprite;
    public Sprite sprite { get => _sprite; }

    [SerializeField]
    private RuntimeAnimatorController _animatorController;
    public RuntimeAnimatorController animatorController { get => _animatorController; }

    [SerializeField]
    private EnemySize _enemySize;
    public EnemySize enemySize{ get => _enemySize; }

    [Header("Enemy Stats")]
    [SerializeField]
    private TypeOfAttack _typeOfAttack;
    public TypeOfAttack typeOfAttack { get => _typeOfAttack; }

    [SerializeField, Range(0, 1)]
    private float _chaosResistance;
    public float chaosResistance { get => _chaosResistance; }

    [SerializeField]
    private float _defaultJumpForce= 15f;
    public float defaultJumpForce { get => _defaultJumpForce; }

    [SerializeField]
    private float _sightRadius = 10f;
    public float sightRadius { get => _sightRadius; }

    [SerializeField]
    private float _attackRange = 1;
    public float attackRange { get => _attackRange; }

    [SerializeField]
    private float _attackSpeed = 1;
    public float attackSpeed { get => _attackSpeed; }

    [Header("Suicidal & Ranged Attack settings")]
    [SerializeField]
    private GameObject _specialAttack;
    public GameObject specialAttack { get => _specialAttack; }

    [SerializeField]
    private float _specialRadius = 10f;
    public float specialRadius { get => _specialRadius; }
    [Header("Boss Stats")]

    [SerializeField]
    private int _damage = 1;
    public int damage { get => _damage; }

    [SerializeField]
    private int _maxHealth = 1;
    public int maxHealth { get => _maxHealth; }

    [SerializeField]
    private float _speed= 10f;
    public float speed { get => _speed; }

    [SerializeField]
    private float _gravityScale = 10f;
    public float gravityScale { get => _gravityScale; }
}
public enum EnemySize { Small,Medium,Big,Boss}
public enum TypeOfAttack { Melee,Melee_Suicidal,Range_Proyectile,Range_Beam}