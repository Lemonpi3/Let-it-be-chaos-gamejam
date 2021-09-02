using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1;

    public void LoadNextLevel(int Level) {

        StartCoroutine(LoadLevel(Level));
    }
    public void LoadSavedGame()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        StartCoroutine(LoadLevel(data.level, true));
    }

    private IEnumerator LoadLevel(int Level, bool loadSave = false)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(Level);
        if (loadSave)
        {
            LoadPlayer();
        }
    }


    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        ChaosManager.instance.level = data.level;
        ChaosManager.instance.player._currentHealth = data.health;

        for (int i = 0; i < ChaosManager.instance.bossesKilled.Length; i++)
        {
            ChaosManager.instance.bossesKilled[i] = data.bossesKilled[i];
        }

        Vector3 position = new Vector3(data.position[0],data.position[1],data.position[2]);
        ChaosManager.instance.player.transform.position = position;

        Debug.Log("Loaded Game" +data.level);
    }

    public void DeleteSave()
    {
        SaveSystem.DeleteSave();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
