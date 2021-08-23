using UnityEngine;

public abstract class Charecter : MonoBehaviour
{
    [SerializeField]
    protected float speed = 1;
    [SerializeField]
    protected float jumpForce = 5f;
    [SerializeField]
    protected int maxHealth = 6;
    [SerializeField]
    protected int currentHealth = 6;

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
    }
  
    protected virtual void Update()
    {
        AnimateMovement(direction);
    }

    private void FixedUpdate()
    {
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
    }

    public void UpdateStats()
    {
        rb.gravityScale = rb.gravityScale * ChaosManager.PhysicsChaosLevel;

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
}