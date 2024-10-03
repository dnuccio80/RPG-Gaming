using RPG.Combat;
using RPG.Resources;
using RPG.Stats;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private Health health;
    private Fighter fighter;
    private Experience experience;

    public float healthPoints;
    public WeaponSO equippedWeapon;
    public float experiencePoints;

    private void Awake()
    {
        health = GetComponent<Health>();
        fighter = GetComponent<Fighter>();
        experience = GetComponent<Experience>();
    }

    public PlayerData GetCurrentStats()
    {
        healthPoints = health.GetHealthPoints();
        experiencePoints = experience.GetExperience();
        equippedWeapon = fighter.GetCurrentWeapon();

        return this;
    }



}
