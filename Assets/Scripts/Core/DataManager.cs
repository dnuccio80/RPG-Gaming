using RPG.Combat;
using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    [Serializable]
    public class PlayerDataXX
    {
        public float healthPoints;
        public float xpPoints;
        public WeaponSO equippedWeapon;
    }

    private void Start()
    {
        LoadData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) SaveData();
    }

    private void SaveData()
    {

        PlayerDataXX playerData = new PlayerDataXX();

        PlayerData player = GameObject.Find(Dictionary.PLAYER_TAG).GetComponent<PlayerData>().GetCurrentStats();

        playerData.healthPoints = player.healthPoints;
        playerData.xpPoints = player.experiencePoints;
        playerData.equippedWeapon = player.equippedWeapon;

        string json = JsonUtility.ToJson(playerData);

        string path = Application.persistentDataPath + "/savefile.json";
        File.WriteAllText(path, json);

        Debug.Log("Saved Data on " + path);

    }

    private PlayerDataXX LoadData()
    {

        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);   

            PlayerDataXX playerData = JsonUtility.FromJson<PlayerDataXX>(json);
            Debug.Log("File loaded : health Points: " + playerData.healthPoints + " xp points: " + playerData.xpPoints + ", Equipped Weapon: " + playerData.equippedWeapon);
            return playerData;
        }
        else
        {
            Debug.Log("Nothing to Load");
            return null;
        }

    }


}
