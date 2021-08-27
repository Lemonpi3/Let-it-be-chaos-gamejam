using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 20f;
    public float pathfinderUpdateSeconds = 0.5f;
    public float jumpCd = 1f;
    private float range = 0; // 0 = melee

    [Header("Physics")]
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public GroundCheck groundCheck;
    public bool isGrounded;
    private float speed;
    private float jumpForce;
    
    [Header("Custom beheavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool infiniteJumps = false;

    private Path path;
    private int currentWaypoint = 0;
    private Seeker seeker;
    private Enemy enemy;
    
    Rigidbody2D rb;
    Animator animator;

    public virtual void Start()
    {
        seeker = GetComponent<Seeker>();
        enemy = GetComponent<Enemy>(); 
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        InvokeRepeating("UpdatePath", 0, pathfinderUpdateSeconds);
    }

    public virtual void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
    }

    protected void UpdatePath()
    {
        if (Vector2.Distance(transform.position, target.position) <= range)
        {
            followEnabled = false;
            rb.velocity = new Vector2(0, 0);
            enemy.Attacking(pathfinderUpdateSeconds);
            animator.SetBool("walking", false);
        }
        else
        {
            followEnabled = true;
            if (TargetInDistance() && followEnabled && seeker.IsDone())
            {
                seeker.StartPath(transform.position, target.position, OnPathComplete);
                animator.SetBool("walking", true);
            }
        }
    }

    protected void PathFollow()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }
        isGrounded = groundCheck.isGrounded;
        // Direction Calculation
        Vector2 _direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
        enemy.GetDirection(_direction);


        // Jump
        if (jumpEnabled && isGrounded)
        {
            if (_direction.y > jumpNodeHeightRequirement)
            {
                rb.velocity = new Vector2(rb.velocity.x,jumpForce);
                StartCoroutine(Cooldown(jumpCd));
                jumpEnabled = infiniteJumps;
            }
        }

        animator.SetBool("walking", true);
        // Movement
        rb.velocity = new Vector2(_direction.x * speed, rb.velocity.y);
        

        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    protected bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    protected void OnPathComplete(Path p)
    {
        animator.SetBool("walking", false);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void GetEnemyStats(float _speed,float _jumpForce,float attackRange)
    {
        speed = _speed;
        jumpForce = _jumpForce;
        range = attackRange;
    }

    protected IEnumerator Cooldown(float cooldown)
    {
        float timer = cooldown;
        while (timer > 1f)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }
        jumpEnabled = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, activateDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
