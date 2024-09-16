using UnityEngine;

public class Experience : MonoBehaviour
{

    public float experience;

    public void GainExperience(float awardExperience)
    {
        experience += awardExperience;
    }



}
