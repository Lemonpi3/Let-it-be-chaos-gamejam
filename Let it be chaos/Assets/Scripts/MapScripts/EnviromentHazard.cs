using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentHazard : MonoBehaviour
{
    [SerializeField]
    protected int damage = 1;
    [SerializeField]
    protected float tickSpeed;
    [SerializeField]
    protected TypeOfHazzard typeOfHazzard;
    [SerializeField]
    private GameObject Explotion;
    [SerializeField]
    private float explotionRadius = 3f;

    float timer;
    protected enum TypeOfHazzard { Single, Continous, Explosive ,Special}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Enemy" && typeOfHazzard == TypeOfHazzard.Continous)
        {
            Charecter victim = collision.GetComponent<Charecter>();
            Tick(victim);
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

    protected void Tick(Charecter charecter)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpecialHazzard(charecter);
            charecter.TakeDamage(damage);
            timer = tickSpeed;
        }
    }

    public virtual void SpecialHazzard(Charecter charecter)
    {

    }

    public void GetStats(int dmg,float _tickspeed)
    {
        damage = dmg;
        tickSpeed = _tickspeed;
    }
}
