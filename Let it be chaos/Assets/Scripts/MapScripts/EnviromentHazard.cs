using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentHazard : MonoBehaviour
{
    [SerializeField]
    int damage = 1;
    [SerializeField]
    float tickSpeed;
    [SerializeField]
    TypeOfHazzard typeOfHazzard;
    [SerializeField]
    private GameObject Explotion;
    [SerializeField]
    private float explotionRadius = 3f;

    float timer;
    enum TypeOfHazzard { Single, Continous, Explosive }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Enemy" && typeOfHazzard == TypeOfHazzard.Continous)
        {
            Charecter victim = collision.GetComponent<Charecter>();
            TickDamage(victim);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy" && typeOfHazzard != TypeOfHazzard.Continous)
        {
            if(typeOfHazzard == TypeOfHazzard.Single)
            {
                collision.GetComponent<Charecter>().TakeDamage(damage);
            }
            if (typeOfHazzard == TypeOfHazzard.Explosive)
            {
                GameObject explotion =Instantiate(Explotion, transform.position, Quaternion.identity);
                explotion.GetComponent<Explotion>().Explode(explotionRadius, damage);
                Destroy(gameObject);
            }
        }
    }

    private void TickDamage(Charecter charecter)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            charecter.TakeDamage(damage);
            timer = tickSpeed;
        }
    }
    
}
