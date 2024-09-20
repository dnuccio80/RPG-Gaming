using RPG.Combat;
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

        public void TakeDamage(GameObject instigator, float damage)
        {
            if(isDead) return;
            healthPoints = MathF.Max(healthPoints - damage, 0);
            OnHealthUpdated?.Invoke(this, EventArgs.Empty);
            if (healthPoints == 0) Die(instigator);
        }

        private void Die(GameObject instigator)
        {
            GetComponent<Animator>().SetTrigger(Dictionary.DIE_ANIMATOR);
            isDead = true;
            GetComponent<Fighter>().Cancel();
            OnDead?.Invoke(this, EventArgs.Empty);

            if (instigator.CompareTag(Dictionary.PLAYER_TAG))
            {
                instigator.GetComponent<Experience>().GainExperience(baseStats.GetStat(Stat.ExperienceReward));
            } 
           
        }

        public bool IsDead() => isDead;
        private float GetBaseHealthPoints() => baseStats.GetStat(Stat.Health);
        public float GetPercentage() => Mathf.Max(Mathf.RoundToInt(healthPoints * 100 / GetBaseHealthPoints()), 0);
      

    }
}
