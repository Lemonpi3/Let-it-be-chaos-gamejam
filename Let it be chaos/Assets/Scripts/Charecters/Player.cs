using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Charecter
{
    [SerializeField]
    private GroundCheck groundCheck;
    [SerializeField]
    private Transform bulletSpawnpoint;

    public Weapon weapon;

    bool isShooting
    {
        get
        {
            return Input.GetButton("Fire1");
        }
    }

    protected override void Start()
    {
        base.Start();
        animator.SetFloat("attackSpeed", 0.25f / weapon.attackSpeed);
    }

    private void FixedUpdate()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        isGrounded = groundCheck.isGrounded;
        animator.SetBool("shooting", isShooting);
    }

    protected override void Update()
    {
        Inputs();
        base.Update();
    }

    private void Inputs()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Move(direction);
        }
        if (Input.GetButton("Fire1"))
        {
            weapon.Shoot(bulletSpawnpoint.position);
        }
    }
}
