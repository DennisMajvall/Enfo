using UnityEngine;
using System.Collections;

[System.Serializable]
public class HeroStats
{
	public float experience;
	public string heroName; // class name, i.e. 'Ranger' 'Paladin'
	public string properName; // unit name, i.e. 'Sai', 'Oak'
}

public class HeroStatsComponent : UnitStatsComponent
{
	[SerializeField]
	HeroStats heroStats;

	// Getters
	public float Experience { get { return heroStats.experience; } }
	public string HeroName { get { return heroStats.heroName; } }
	public string ProperName { get { return heroStats.properName; } }


	// Setters
	public void ChangeExperience(float delta) { heroStats.experience += delta; }

	public void ChangeHeroName(string newName) { heroStats.heroName = newName; }

	public void ChangeProperName(string newProperName) { heroStats.properName = newProperName; }

}
