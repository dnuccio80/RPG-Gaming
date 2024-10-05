using RPG.Resources;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyData : MonoBehaviour
{
    public string enemyName;
    public Health health;


    private void Awake()
    {
        health = GetComponent<Health>();
        enemyName = SceneManager.GetActiveScene().name + gameObject.name;
    }

    private void Start()
    {
        Invoke("LoadData", 0.1f);
    }

    private void LoadData()
    {
        float healthLoaded = EnemyDataManager.Instance.GetLoadedData(enemyName);
        health.SetHealthPoints(healthLoaded);
    }

    private void OnDestroy()
    {
        EnemyDataManager.Instance.SetData(enemyName, health.GetHealthPoints());
    }
}
