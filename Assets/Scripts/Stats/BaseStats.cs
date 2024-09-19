using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] private int startingLevel = 1;
        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private ProgressionSO progressionSO;

        public float GetStat(Stat stat)
        {
            return progressionSO.GetStat(stat, characterClass, startingLevel);
        }

    }
}
