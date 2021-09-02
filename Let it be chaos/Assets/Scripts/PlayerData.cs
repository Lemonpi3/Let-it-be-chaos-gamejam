using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int health;
    public int level;
    public bool[] bossesKilled;
    public float[] position;

    public PlayerData(Player player)
    {
        level = ChaosManager.instance.level;
        health = player._currentHealth;
        bossesKilled = new bool[ChaosManager.instance.bossesKilled.Length];

        for (int i = 0; i < bossesKilled.Length; i++)
        {
            bossesKilled[i] = ChaosManager.instance.bossesKilled[i];
        }

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
