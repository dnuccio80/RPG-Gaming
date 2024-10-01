using RPG.Resources;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{

    [SerializeField] private Image healthImage;
    [SerializeField] private Health health;

    private void Start()
    {
        health.OnHealthUpdated += Health_OnHealthUpdated;
        health.OnDead += Health_OnDead;
    }

    private void Health_OnDead(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Health_OnHealthUpdated(object sender, System.EventArgs e)
    {
        healthImage.fillAmount = health.GetPercentage() / 100f;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);

    }

}
