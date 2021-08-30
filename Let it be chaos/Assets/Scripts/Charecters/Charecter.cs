using System.Collections;
using UnityEngine;

public abstract class Charecter : MonoBehaviour
{
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

    protected SpriteRenderer spriteRenderer;
    protected Vector2 direction;
    protected Rigidbody2D rb;
    protected Animator animator;
    public bool inChaosZone;

    protected bool isMoveing 
    {
        get
        {
            return  direction.x != 0;
        }
    }
    protected bool isGrounded = true;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetCharecterDefaultStats(true);
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
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, 0);
        }
        else if(direction.x == 0)
        {
            return;
        }
        else
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, 0);
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

    public virtual void ApplyChaosStats()
    {
        
    }

    public virtual void SetCharecterDefaultStats(bool heal = false)
    {
        rb.gravityScale = defaultGravityScale;
        maxHealth = defaultMaxHealth;
        jumpForce = defaultJumpForce;
        speed = defaultSpeed;
        if (heal)
        {
            Heal(maxHealth);
        }
    }


    protected virtual void FlipUpsideDown(GroundCheck groundCheck)
    {
        if(rb.gravityScale >= 0)
        {
            groundCheck.transform.localPosition = groundCheck.defaultPos;
            spriteRenderer.flipY = false;
            if (jumpForce < 0)
            {
                jumpForce *= -1;
            }
        }
       
        if (rb.gravityScale < 0)
        {
            groundCheck.transform.localPosition = groundCheck.defaultPos * -1;
            spriteRenderer.flipY = true;
            if(jumpForce > 0)
            {
                jumpForce *= -1;
            }
        }
    }

    public float GetCurrentHealthPercent()
    {
        return currentHealth / maxHealth;
    }
}
