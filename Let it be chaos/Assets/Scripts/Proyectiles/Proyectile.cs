using UnityEngine;

public class Proyectile : MonoBehaviour
{
    protected int damage = 1;
    protected float speed = 10;
    protected float maxRange = 10;

    protected bool explosive;
    protected float explotionRadius = 1;
    // public GameObject explotionGO;
    bool hasExploded;

    protected Vector3 startPos;
    protected Vector2 direction;
    protected Rigidbody2D rb;
    
    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        rb.velocity = speed * direction;
        Debug.Log(direction);
    }

    protected virtual void Update()
    {
        float distance = Vector3.Distance(startPos, transform.position);
        if (distance >= maxRange)
        {
            Explode();
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Explode();
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Charecter>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    protected virtual void Explode()
    {
        if (explosive)
        {
            if (!hasExploded)
            {
                animator.SetBool("Explosive", explosive);
                transform.localScale *= explotionRadius;
                hasExploded = true;
            }
            Destroy(gameObject);
            return;
        }
        animator.SetBool("Hit", true);
    }

    public void GetStats(Vector2 _direction,int _damage = 1,float _speed = 10,float _proyectileRadius =1,float _maxRange =5,bool _explosive = false,float _explotionRadius =1)
    {
        damage = _damage;
        speed = _speed;
        maxRange = _maxRange;
        explosive = _explosive;
        explotionRadius = _explotionRadius;
        direction = new Vector2(_direction.x,0);
        if(direction.x < 0)
        {
            direction.x = -1;
        } 
        else
        {
            direction.x = 1;
        }
        transform.localScale *= _proyectileRadius;
    }
}
