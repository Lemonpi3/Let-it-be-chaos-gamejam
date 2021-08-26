using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosZone : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;

    void Start()
    {
        InvokeRepeating("UpdateChaos", 0, 2);
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Player")
        {
            collision.GetComponent<Charecter>().UpdateStats();
            Debug.Log("Updated " + collision.name);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Player")
        {
            collision.GetComponent<Charecter>().UpdateStats();
            Debug.Log("Updated " + collision.name);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Player")
        {
            collision.GetComponent<Charecter>().SetCharecterDefaultStats();
            Debug.Log("Reseted " + collision.name);
        }
    }

  
}
