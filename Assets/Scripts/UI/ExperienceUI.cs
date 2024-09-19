using TMPro;
using UnityEngine;

public class ExperienceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private Experience playerExperience;


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
    }

}
