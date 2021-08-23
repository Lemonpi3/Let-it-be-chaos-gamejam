using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    //Deprecated
    int damage = 1;
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
