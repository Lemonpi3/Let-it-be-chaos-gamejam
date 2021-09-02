using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.lemon";
        FileStream fileStream= new FileStream(path,FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(fileStream, data);
        fileStream.Close();
        Debug.Log("GameSaved");
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.lemon";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data= formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            Debug.Log("PlayerDataLoaded");
            return data;
        }
        else
        {
            Debug.Log("Savefile not found in: " + path);
            return null;
        }
    }

    
    public static void DeleteSave()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.lemon";
        File.Delete(path);

        Debug.Log("SaveDeleted");
    }
}
