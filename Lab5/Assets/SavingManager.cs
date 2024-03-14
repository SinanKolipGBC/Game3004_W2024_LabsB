using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


[Serializable]
class SaveData
{
    public float[] position;
    public float[] rotation;

    public SaveData()
    {
        position = new float[3];
        rotation = new float[3];
    }
}


public class SavingManager : MonoBehaviour
{
    public Transform player;
    public void SaveGame()
    {
        /*PlayerPrefs.SetFloat("PlayerPositionX", player.position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", player.position.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", player.position.z);

        Debug.Log("Game data saved!");*/

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat"); 
        SaveData data = new SaveData();
        data.position[0] = player.position.x;
        data.position[1] = player.position.y;
        data.position[2] = player.position.z;

        data.rotation[0] = player.GetComponentInChildren<Camera>().transform.localEulerAngles.x;
        data.rotation[1] = player.localEulerAngles.y;
        data.rotation[2] = player.localEulerAngles.z;

        bf.Serialize(file, data); 
        file.Close();
        Debug.Log("Game data saved!");

    }

    public void LoadGame()
    {
        /*if (PlayerPrefs.HasKey("PlayerPositionX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPositionX");
            float y = PlayerPrefs.GetFloat("PlayerPositionY");
            float z = PlayerPrefs.GetFloat("PlayerPositionZ");

            player.GetComponent<CharacterController>().enabled = false;
            player.position = new Vector3(x, y, z);
            player.GetComponent<CharacterController>().enabled = true;

            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");*/

        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open); 
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            player.GetComponent<CharacterController>().enabled = false;
            player.position = new Vector3(data.position[0], data.position[1], data.position[2]);
            player.localEulerAngles = new Vector3(0, data.rotation[1], data.rotation[2]);
            player.GetComponentInChildren<Camera>().transform.localEulerAngles = new Vector3(data.rotation[0], 0, 0);
            player.GetComponent<CharacterController>().enabled = true;

            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }

    void ResetData()
    {
        /*  PlayerPrefs.DeleteAll(); 
          Debug.Log("Data reset complete");*/

        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveData.dat");
          
            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");

    }

}
