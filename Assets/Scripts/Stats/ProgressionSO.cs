using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/ New progression")]
    public class ProgressionSO : ScriptableObject
    {

        [SerializeField] private CharacterClassProgression[] characterClassesProgression;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable;

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();

            float[] levels = lookupTable[characterClass][stat];

            if (levels.Length < level) return levels[levels.Length - 1];

            return levels[level - 1];
        }

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }

        private void BuildLookup()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach(CharacterClassProgression progressionClass in characterClassesProgression)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();
                
                foreach(ProgressionStat progressionStat in progressionClass.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }

                lookupTable[progressionClass.characterClass] = statLookupTable;
            }
        }


        [Serializable]
        class CharacterClassProgression
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;

        }

        [Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;

        }

    }
}
