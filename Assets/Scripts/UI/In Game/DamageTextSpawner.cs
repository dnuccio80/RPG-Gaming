using RPG.Resources;
using UnityEngine;

namespace RPG.UI
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject damageText;
        [SerializeField] private Health health;

        private void Start()
        {
            health.OnDamageTaken += Health_OnDamageTaken;
        }

        private void Health_OnDamageTaken(object sender, Health.OnDamageTakenEventArgs e)
        {
            GameObject damageTextSpawned = Instantiate(damageText, transform);

            damageTextSpawned.GetComponent<DamageText>().SetDamageText(e.Damage);

        }

    }

}