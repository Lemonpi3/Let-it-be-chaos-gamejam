using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Charecter
{
    [SerializeField]
    private GroundCheck groundCheck;

    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        isGrounded = groundCheck.isGrounded;
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
    }
}
