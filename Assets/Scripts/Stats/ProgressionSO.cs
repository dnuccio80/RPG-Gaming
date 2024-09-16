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

        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach(CharacterClassProgression progressionClass in characterClassesProgression)
            {
                if(progressionClass.characterClass == characterClass)
                {
                    return progressionClass.health[level - 1];
                }
            }
            return 0;
        }


        [Serializable]
        class CharacterClassProgression
        {
            public CharacterClass characterClass;
            public float[] health;
            public float[] damage;
        }

    }
}
