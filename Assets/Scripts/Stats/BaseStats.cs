using System;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        public event EventHandler OnLevelUp;

        [Range(1,99)]
        [SerializeField] private int startingLevel = 1;
        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private ProgressionSO progressionSO;
        [SerializeField] private GameObject levelUpGameObject;

        private Experience experience;
        int currentLevel;

        private void Start()
        {
            currentLevel = GetLevel();
            if (gameObject.CompareTag(Dictionary.PLAYER_TAG))
            {
                experience = GetComponent<Experience>();
                experience.OnXpGained += Experience_OnXpGained;
            }
        }

        private void Experience_OnXpGained(object sender, EventArgs e)
        {
            TryLevelUp();
        }

        public float GetStat(Stat stat)
        {
            return progressionSO.GetStat(stat, characterClass, GetLevel());
        }

        public float GetMaxLevel(Stat stat)
        {
            print(progressionSO.GetLevels(stat, characterClass));
            return progressionSO.GetLevels(stat, characterClass);
        }

        public int GetLevel()
        {

            Experience experience = GetComponent<Experience>();

            if (experience == null) return startingLevel;

            float currentXP = experience.GetExperience();
            int penultimateLevel = progressionSO.GetLevels(Stat.ExperienceToLevelUp, characterClass);

            for (int level = 1; level <= penultimateLevel; level++)
            {
                float xpToLevelUp = progressionSO.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if(xpToLevelUp > currentXP)
                {
                    return level;
                }
            }
            return penultimateLevel + 1;

        }

        void TryLevelUp()
        {
            if (currentLevel < GetLevel())
            {
                currentLevel = GetLevel();
                TryEmitLevelParticles();
                OnLevelUp?.Invoke(this, EventArgs.Empty);
            }

        }

        private void TryEmitLevelParticles()
        {
            if (levelUpGameObject != null) Instantiate(levelUpGameObject, transform.position, Quaternion.identity, transform);
        }


    }
}
