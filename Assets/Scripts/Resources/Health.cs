using RPG.Stats;
using System;
using UnityEngine;

namespace RPG.Resources
{
    public class Health : MonoBehaviour
    {
        public event EventHandler OnDead;
        public event EventHandler OnHealthUpdated;

        [SerializeField] private float healthPoints = 100;

        private bool isDead;
        private BaseStats baseStats;

        private void Start()
        {
            baseStats = GetComponent<BaseStats>();
            healthPoints = GetBaseHealthPoints();
            OnHealthUpdated?.Invoke(this, EventArgs.Empty);
        }


        public void TakeDamage(float damage)
        {
            if(isDead) return;
            healthPoints = MathF.Max(healthPoints - damage, 0);
            OnHealthUpdated?.Invoke(this, EventArgs.Empty);
            if (healthPoints == 0) Die();
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger(Dictionary.DIE_ANIMATOR);
            isDead = true;
            OnDead?.Invoke(this, EventArgs.Empty);
        }

        public bool IsDead() => isDead;
        private float GetBaseHealthPoints() => baseStats.GetHealth();
        public float GetPercentage() => Mathf.RoundToInt(healthPoints * 100 / GetBaseHealthPoints());
      

    }
}
