using System;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour
    {
        public event EventHandler OnXpChanged;

        public float experience;

        public void GainExperience(float awardExperience)
        {
            experience += awardExperience;
            OnXpChanged?.Invoke(this, EventArgs.Empty);
        }

        public float GetExperience() => experience;

        public void SetExperience(float loadedXp)
        {
            experience = loadedXp;
            OnXpChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
