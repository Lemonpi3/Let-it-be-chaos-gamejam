using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosZone : MonoBehaviour
{
    [SerializeField]
    private float ChaosUpdateIntervall = 30;
    public BoxCollider2D boxCollider2D;

    void Start()
    {
        InvokeRepeating("UpdateChaos", 0, ChaosUpdateIntervall);
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

    public void UpdateChaos()
    {
        ChaosManager.instance.RandomizeChaos();
        boxCollider2D.enabled = false;
        Debug.Log("Updated chaoszone");
        boxCollider2D.enabled = true;
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
