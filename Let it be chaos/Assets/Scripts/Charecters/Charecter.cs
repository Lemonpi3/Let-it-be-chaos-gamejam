using System.Collections;
using UnityEngine;

public abstract class Charecter : MonoBehaviour
{
    [SerializeField,Range(0,1)]
    protected float chaosResistance = 0;

    [Header("Default Stats")]
    [SerializeField]
    protected float defaultGravityScale;
    [SerializeField]
    protected float defaultJumpForce;
    [SerializeField]
    protected float defaultSpeed;
    [SerializeField]
    protected int defaultMaxHealth;
    [SerializeField]
    [Header("Current Stats")]
    protected float speed = 10;
    [SerializeField]
    protected float jumpForce = 5f;
    [SerializeField]
    protected int currentHealth;
    [SerializeField]
    protected int maxHealth = 6;

    protected Vector2 direction;
    protected Rigidbody2D rb;
    protected Animator animator;
    public bool inChaosZone;

    public bool isMoveing 
    {
        get
        {
            return  direction.x != 0;
        }
    }
    public bool isGrounded = true;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        UpdateStats();
        currentHealth = maxHealth;
    }
  
    protected virtual void Update()
    {
        AnimateMovement(direction);
    }

    public virtual void AnimateMovement(Vector2 direction)
    {
        animator.SetBool("walking",isMoveing);
        animator.SetBool("jumping",!isGrounded);

        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(direction.x == 0)
        {
            return;
        }
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void Move(Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x * speed,rb.velocity.y);
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name+" took "+ amount + "damage");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void UpdateStats()
    {
        ApplyChaos_Physics();
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public virtual void SetCharecterDefaultStats()
    {
        rb.gravityScale = defaultGravityScale;
        maxHealth = defaultMaxHealth;
        jumpForce = defaultJumpForce;
        speed = defaultSpeed;
        Heal(maxHealth);
    }

    public void ApplyChaos_Physics()
    {
        //Attempt to prevent unplayability
        float chaos = (ChaosManager.PhysicsChaosLevel - (ChaosManager.PhysicsChaosLevel * chaosResistance));

        float gravity = Mathf.Clamp(defaultGravityScale + defaultGravityScale * chaos, -defaultGravityScale * 2, defaultGravityScale * 2);
        rb.gravityScale = gravity;

        if(gravity == 0)
        {
            rb.gravityScale = defaultGravityScale;
        } else
        if(gravity < 0)
        {
            transform.rotation = Quaternion.Euler(180, 0, 0);
        } else
        if (gravity > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void ModifyStats(int _maxHealthMod, float speedMod, float gravityMod)
    {
        float gravity = rb.gravityScale;
        rb.gravityScale = gravity * gravityMod;
        maxHealth = maxHealth +  _maxHealthMod;
        speed = speed + speed * speedMod;
        if (maxHealth <= 0)
        {
            maxHealth = Mathf.Abs(_maxHealthMod);
        }
        if (speed == 0)
        {
            speed = defaultSpeed;
        }
        if (rb.gravityScale == 0)
        {
            rb.gravityScale = defaultGravityScale;
        }
    }
}
