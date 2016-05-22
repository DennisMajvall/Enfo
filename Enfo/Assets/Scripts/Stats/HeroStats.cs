using UnityEngine;
using System.Collections;

public class HeroStats : UnitStats
{

    // Fields
    public float currentExperience;
    public string heroName;
    public string properName; // class name, i.e. 'Paladin', 'Berzerker'

    // Getters
    public float CurrentExperience { get { return currentExperience; } }
    public string HeroName { get { return heroName; } }
    public string ProperName { get { return properName; } }


    // Setters
    public void changeExperience(float delta)
    {
        currentExperience += delta;
    }

    public void changeHeroName(string newName)
    {
        heroName = newName;
    }

    public void changeProperName(string newProperName)
    {
        properName = newProperName;
    }

}
