using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    int damage = 1;

    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Finished"))
        {
            Destroy(gameObject);
        }
    }

    public void Explode(float radius,int _damage)
    {
        damage = _damage;
        transform.localScale *= radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Charecter>().TakeDamage(damage);
        }
    }
}
