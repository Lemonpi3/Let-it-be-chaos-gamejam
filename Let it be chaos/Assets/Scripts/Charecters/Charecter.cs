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
        SetCharecterDefaultStats();
        UpdateStats();
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

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name+" took "+ amount + "damage");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void UpdateStats()
    {
        ApplyChaos_Physics();

        if(this.tag == "Player")
        {
            maxHealth = (int)(maxHealth * ChaosManager.PlayerChaosLevel);

            if(maxHealth <= 0)
            {
                maxHealth = 1;
            }

            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            speed = speed * ChaosManager.PlayerChaosLevel;
            jumpForce = jumpForce * ChaosManager.PlayerChaosLevel;
        }
        if (this.tag == "Enemy")
        {
            maxHealth = (int)(maxHealth * ChaosManager.EnemyChaosLevel);
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            speed = speed * ChaosManager.EnemyChaosLevel;
            jumpForce = jumpForce * ChaosManager.EnemyChaosLevel;
        }
    }

    private void SetCharecterDefaultStats()
    {
        rb.gravityScale = defaultGravityScale;
        maxHealth = defaultMaxHealth;
        currentHealth = maxHealth;
        jumpForce = defaultJumpForce;
        speed = defaultSpeed;
    }

    public void ApplyChaos_Physics()
    {
        rb.gravityScale *= (ChaosManager.PhysicsChaosLevel-(ChaosManager.PhysicsChaosLevel * chaosResistance));

        if(rb.gravityScale == 0)
        {
            rb.gravityScale = defaultGravityScale;
        } else
        if(rb.gravityScale < 0)
        {
            transform.rotation = Quaternion.Euler(180, 0, 0);
        } else
        if (rb.gravityScale > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
