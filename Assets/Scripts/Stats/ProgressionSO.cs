using RPG.Combat;
using RPG.Stats;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/ New progression")]
    public class ProgressionSO : ScriptableObject
    {

        [SerializeField] private CharacterClassProgression[] characterClassesProgression;

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            foreach(CharacterClassProgression progressionClass in characterClassesProgression)
            {
                if (progressionClass.characterClass != characterClass) continue;
                
                foreach(ProgressionStat progressionStat in progressionClass.stats)
                {
                    if(progressionStat.stat != stat) continue;
                    
                    if(progressionStat.levels.Length < level) continue;

                    return progressionStat.levels[level - 1];

                }
            }
            return 0;
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
