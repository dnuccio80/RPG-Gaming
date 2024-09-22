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
        healthPlayerText.text = "Player Health: " + string.Format("{0:0}/{1:0}", playerHealth.GetHealthPoints(), playerHealth.GetMaxHealtPointsByLevel());
    }

    private void Update()
    {
        if (!playerFighter.HasTarget())
        {
            healthEnemyText.text = "";
            return;
        }

        healthEnemyText.text = "Enemy Health: " + string.Format("{0:0}/{1:0}", playerFighter.GetTargetHealth().GetHealthPoints(), playerFighter.GetTargetHealth().GetMaxHealtPointsByLevel());

    }

}
