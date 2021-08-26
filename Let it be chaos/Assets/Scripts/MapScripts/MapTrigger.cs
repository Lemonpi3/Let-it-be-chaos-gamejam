using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectsOnEnter;
    [SerializeField]
    private GameObject[] objectsOnExit;
    [SerializeField]
    private string triggerTag = "Player";
    
    private bool hasTriggeredEnter;
    private bool hasTriggeredExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == triggerTag && !hasTriggeredEnter)
        {
            for (int i = 0; i < objectsOnEnter.Length; i++)
            {
                objectsOnEnter[i].SetActive(!objectsOnEnter[i].activeSelf);
            }
            hasTriggeredEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == triggerTag && !hasTriggeredExit)
        {
            for (int i = 0; i < objectsOnExit.Length; i++)
            {
                objectsOnExit[i].SetActive(!objectsOnExit[i].activeSelf);
            }
            hasTriggeredExit = true;
        }
    }
}
