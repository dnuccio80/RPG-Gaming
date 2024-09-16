using RPG.Combat;
using RPG.Resources;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private TextMeshProUGUI healthPlayerText;
    [SerializeField] private Health playerHealth;
    [SerializeField] private Fighter playerFighter;
    [Header("Enemy")]
    [SerializeField] private TextMeshProUGUI healthEnemyText;

    private void Start()
    {
        playerHealth.OnHealthUpdated += PlayerHealth_OnHealthUpdated;
    }

    private void PlayerHealth_OnHealthUpdated(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        healthPlayerText.text = "Player Health: " + playerHealth.GetPercentage().ToString() + "%";
    }

    private void Update()
    {
        if (!playerFighter.HasTarget())
        {
            healthEnemyText.text = "";
            return;
        }

        healthEnemyText.text = "Enemy Health: " + playerFighter.GetTargetHealth().GetPercentage().ToString() + "%";

    }

}
