using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objects;
    [SerializeField]
    private string triggerTag = "Player";
    [SerializeField]
    private bool hasTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == triggerTag && !hasTriggered)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(!objects[i].activeSelf);
            }
            hasTriggered = true;
        }
    }
}
