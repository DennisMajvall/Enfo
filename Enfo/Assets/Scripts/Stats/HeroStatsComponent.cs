using UnityEngine;
using System.Collections;

public enum AttributeTypes
{
	Agility,
	Intelligence,
	Strength
}

[System.Serializable]
public class HeroStats
{
	public float	experience;
	public float 	reqExperience;
	public int 		level;
	public string 	heroName; // class name, i.e. 'Ranger' 'Paladin'
	public bool		isInWestTeam;
	
	//Attributes
	public AttributeTypes primaryAttribute;
	public float agilityPerLevel = 2.0f;
	public float intelligencePerLevel = 2.0f;
	public float strengthPerLevel = 2.0f;
	public float strength = 6;
	public float intelligence = 6;
	public float agility = 6;

}

public class HeroStatsComponent : UnitStatsComponent
{
	[SerializeField]
	HeroStats heroStats = new HeroStats();

	/**
	 * GETTERS
	 */
	public float 	Experience			{ get { return heroStats.experience; } }
	public float	RequiredExperience 	{ get { return heroStats.reqExperience; } }
	public int 		Level				{ get { return heroStats.level; } }
	public string 	HeroName 			{ get { return heroStats.heroName; } }
	public int 		Strength			{ get { return Mathf.RoundToInt(heroStats.strength); } }
	public bool		IsInWestTeam		{ get { return heroStats.isInWestTeam; } }

	/**
	 * SETTERS AND CHANGERS
	 */
	public void ChangeHeroName(string newName)	{ heroStats.heroName = newName; }
	public void AddLevels(int levels)			{ heroStats.level += levels; }
	public void SetRequiredExperience(float req){ heroStats.reqExperience = req; }
	public void AddExperience(float amount)
	{
		// Check for Faenella's Grace and Enfeeble here, and adjust amount accordingly

		// Add experience
		heroStats.experience += amount;

		if (Experience >= RequiredExperience) {
			// Level up
			while (Experience >= RequiredExperience) {
				AddLevels (1);
				heroStats.experience -= RequiredExperience;
				heroStats.reqExperience *= GameplayConstants.ExpRequiredIncreaseFactorPerLevel;
			}

			// Do a level up special effect here
		}
	}

	//Attributes
	public void ChangeStrength(float delta)
	{
		heroStats.strength += delta;
		ChangeMaxHealth(delta * GameplayConstants.HealthPerStrength, true);
		IncreaseHealthRegeneration(delta * GameplayConstants.HealthRegenPerStrength);
		if (heroStats.primaryAttribute == AttributeTypes.Strength)
			ChangeDamage(delta * GameplayConstants.DamagePerPrimaryAttribute);
	}
	public void ChangeAgility(float delta)
	{
		heroStats.agility += delta;
		ChangeAttackSpeedPercentage(delta * GameplayConstants.AttackSpeedPerAgility);
		ChangeArmor(delta * GameplayConstants.ArmorPerAgility);
		if (heroStats.primaryAttribute == AttributeTypes.Agility)
			ChangeDamage(delta * GameplayConstants.DamagePerPrimaryAttribute);
	}
	public void ChangeIntelligence(float delta)
	{
		heroStats.intelligence += delta;
		ChangeMaxMana(delta * GameplayConstants.ManaPerIntelligence, true);
		IncreaseManaRegeneration(delta * GameplayConstants.ManaRegenPerIntelligence);
		if (heroStats.primaryAttribute == AttributeTypes.Intelligence)
			ChangeDamage(delta * GameplayConstants.DamagePerPrimaryAttribute);
	}

	/**
	 * Others
	 */
	void Start()
	{
		SetStartingAttributes();
	}

	void SetStartingAttributes()
	{
		float str = heroStats.strength;
		float intel = heroStats.intelligence;
		float agi = heroStats.agility;

		heroStats.strength = 0f;
		heroStats.intelligence = 0f;
		heroStats.agility = 0f;

		ChangeStrength(str);
		ChangeIntelligence(intel);
		ChangeAgility(agi);

		heroStats.reqExperience = GameplayConstants.ExpRequiredAtLevelOne;
		heroStats.level = 1;
	}
}
