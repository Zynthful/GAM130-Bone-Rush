using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField]
    private int CurrentExperience;
    private int ExperienceToNextLevel;

    void AddExperience()//ExperianceModifier)
    {
        //CurrentExperience += ExperianceModifier;
        if (CurrentExperience >= ExperienceToNextLevel)
        {

        }
    }
}
