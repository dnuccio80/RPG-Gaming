using RPG.Combat;
using RPG.Resources;
using RPG.Stats;
using System;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class PlayerData : MonoBehaviour
{


    [Serializable]
    private class Data
    {
        public float healthPoints;
        public float xp;
        public WeaponSO equipedWeapon;
    }

    private Health health;
    private Fighter fighter;
    private Experience experience;

    private void Awake()
    {
        health = GetComponent<Health>();
        fighter = GetComponent<Fighter>();
        experience = GetComponent<Experience>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }
    }

    private void SaveData()
    {
        Data data = new Data();

        data.healthPoints = health.GetHealthPoints();
        data.equipedWeapon = fighter.GetEquippedWeapon();
        data.xp = experience.GetExperience();
        
        string json = JsonUtility.ToJson(data);
        
        using(StreamWriter writer = new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json"))
        {
            writer.Write(json);
        }
    }


    private void LoadData()
    {
        string json = string.Empty;

        Data dataLoaded = new Data();

        using(StreamReader streamReader = new StreamReader(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json"))
        {
            json = streamReader.ReadToEnd();
        }

        dataLoaded = JsonUtility.FromJson<Data>(json);

        health.SetHealthPoints(dataLoaded.healthPoints);
        if(dataLoaded.equipedWeapon != null) fighter.EquipWeapon(dataLoaded.equipedWeapon);
        experience.SetExperience(dataLoaded.xp);
    }




}
