using RPG.Combat;
using RPG.Resources;
using RPG.Stats;
using System;
using System.Data;
using System.IO;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private Health health;
    private Fighter fighter;
    private Experience experience;

    [Serializable]
    public class Data
    {
        public float healthPoints;
        public float experiencePoints;
        public WeaponSO equippedWeapon;
    }

    private Data data = new Data();

    private void Awake()
    {
        health = GetComponent<Health>();
        fighter = GetComponent<Fighter>();
        experience = GetComponent<Experience>();
    }

    private void Start()
    {
        Invoke("LoadData", .1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) SaveData(); 
    }

    private void LoadData()
    {
        string path = Application.persistentDataPath + "/playerData.json";

        if(File.Exists(path))
        {
            data = new Data();
            string json = File.ReadAllText(path);
            
            data = JsonUtility.FromJson<Data>(json);

            health.SetHealthPoints(data.healthPoints);
            fighter.EquipWeapon(data.equippedWeapon);
            experience.SetExperience(data.experiencePoints);
            
            Debug.Log("Player data loaded: Health: " + data.healthPoints + ", experience: " + data.experiencePoints + ", equipped weapon: " + data.equippedWeapon);

        } else
        {
            Debug.Log("There is no PlayerData to load baby");
        }
    }

    private void SaveData()
    {
        string path = Application.persistentDataPath + "/playerData.json";

        data = new Data();

        data.healthPoints = health.GetHealthPoints();
        data.experiencePoints = experience.GetExperience();
        data.equippedWeapon = fighter.GetCurrentWeapon();

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(path, json);
        Debug.Log("Player Data saved...");
    }

    private void OnDestroy()
    {
        SaveData();
    }




}
