using TMPro;
using UnityEngine;
using RPG.Stats;

public class ExperienceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Experience playerExperience;
    [SerializeField] private BaseStats baseStats;


    private void Start()
    {
        UpdateVisual();
        playerExperience.OnXpGained += PlayerExperience_OnXpGained;
    }

    private void PlayerExperience_OnXpGained(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        experienceText.text = "XP: " + playerExperience.GetExperience().ToString();
        levelText.text = "Level: " + baseStats.GetLevel();
    }

}
