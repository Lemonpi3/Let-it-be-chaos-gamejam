using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("NextLevel");
        }
    }

    private void GoToNextLevel()
    {

    }
}
