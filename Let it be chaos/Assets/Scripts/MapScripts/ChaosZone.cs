using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosZone : MonoBehaviour
{
    float timer;


    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Player")
        {
            collision.GetComponent<Charecter>().SetCharecterDefaultStats();
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Player")
        {
            ChaosUpdate(collision);
        }
    }

    public virtual void ChaosUpdate(Collider2D collision)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            collision.GetComponent<Charecter>().UpdateStats();
            timer = ChaosManager._chaosUpdateInterval;
        }
    }
}
