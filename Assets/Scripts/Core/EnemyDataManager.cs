using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class EnemyDataManager : MonoBehaviour
{

    public static EnemyDataManager Instance {  get; private set; }

    private Dictionary<string, float> enemyDataDictionary = new Dictionary<string, float>();

    private void Awake()
    {
        Instance = this;
        LoadData();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)) { LoadData(); }
    }

    public void SetData(string enemyName, float currentHealth)
    {
        if (enemyDataDictionary.ContainsKey(enemyName)) enemyDataDictionary[enemyName] = currentHealth;
        else enemyDataDictionary.Add(enemyName, currentHealth);
        SaveData();
    }

    public float GetLoadedData(string enemyName)
    {
        if (enemyDataDictionary.ContainsKey(enemyName))
        {
            return enemyDataDictionary[enemyName];
        } else
        {
            return -1;
        }
    }


    private void LoadData()
    {
        string path = Application.persistentDataPath + "/enemyData.json";

        enemyDataDictionary = new Dictionary<string, float>();

        if (File.Exists(path))
        {
            string jsonFromFile = File.ReadAllText(path);
            enemyDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, float>>(jsonFromFile);
        }
    }

    private void SaveData()
    {
        string json = JsonConvert.SerializeObject(enemyDataDictionary);
        string path = Application.persistentDataPath + "/enemyData.json";
        File.WriteAllText(path, json);

        Debug.Log("Guardando enemyData...");
    }




}
