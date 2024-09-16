using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] private int startingLevel = 1;
        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private ProgressionSO progressionSO;

        public float GetHealth()
        {
            return progressionSO.GetHealth(characterClass, startingLevel);
        }

        public float GetAwardExperience()
        {
            return 10;
        }

    }
}
