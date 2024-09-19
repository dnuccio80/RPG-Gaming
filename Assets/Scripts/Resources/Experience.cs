using System;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public event EventHandler OnXpGained;

    public float experience;

    public void GainExperience(float awardExperience)
    {
        experience += awardExperience;
        OnXpGained?.Invoke(this, EventArgs.Empty);
    }

    public float GetExperience() => experience;

}
