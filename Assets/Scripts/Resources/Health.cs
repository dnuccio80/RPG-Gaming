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
        public event EventHandler<OnDamageTakenEventArgs> OnDamageTaken;

        public class OnDamageTakenEventArgs : EventArgs
        {
            public float Damage;
        }

        [SerializeField] private float healthPoints = 100;

        private bool isDead;
        private BaseStats baseStats;

        private void Start()
        {
            baseStats = GetComponent<BaseStats>();
            healthPoints = GetBaseHealthPoints();
            OnHealthUpdated?.Invoke(this, EventArgs.Empty);

            if (gameObject.CompareTag(Dictionary.PLAYER_TAG))
            {
                baseStats.OnLevelUp += BaseStats_OnLevelUp;
            }
        }

        private void BaseStats_OnLevelUp(object sender, EventArgs e)
        {
            if(GetBaseHealthPoints() != 0)
            {
                healthPoints = GetBaseHealthPoints();
                OnHealthUpdated?.Invoke(this, EventArgs.Empty);
            } 
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            if(isDead) return;
            healthPoints = MathF.Max(healthPoints - damage, 0);
            OnHealthUpdated?.Invoke(this, EventArgs.Empty);
            
            if (healthPoints == 0)
            {
                Die(instigator);
                return;
            } 

            OnDamageTaken?.Invoke(this, new OnDamageTakenEventArgs
            {
                Damage = damage
            });

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

        // Used by EnemyData only
        private void DestroyEnemyOnHierarchy()
        {
            Destroy(gameObject);
        }

        public void IncrementHealth(float healthIncrement)
        {
            healthPoints = MathF.Min(healthPoints + healthIncrement, GetBaseHealthPoints());
            OnHealthUpdated?.Invoke(this, EventArgs.Empty);
        }

        public bool IsDead() => isDead;
        private float GetBaseHealthPoints() => baseStats.GetStat(Stat.Health);
        public float GetHealthPoints() => healthPoints;
        public float GetMaxHealtPointsByLevel() => GetBaseHealthPoints();
        public float GetPercentage() => Mathf.Max(Mathf.RoundToInt(healthPoints * 100 / GetBaseHealthPoints()), 0); 

        // Used by EnemyData only
        public void SetHealthPoints(float loadedHealthPoints)
        {
            if (loadedHealthPoints == -1)
            {
                healthPoints = GetBaseHealthPoints();
                OnHealthUpdated?.Invoke(this, EventArgs.Empty);
                return;
            }

            if(loadedHealthPoints == 0)
            {
                DestroyEnemyOnHierarchy();
            }

            healthPoints = loadedHealthPoints;
            OnHealthUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
