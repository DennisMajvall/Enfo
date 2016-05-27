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
	public float experience;
	public int level = 1;
	public string heroName; // class name, i.e. 'Ranger' 'Paladin'
	
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
	HeroStats heroStats;

	/**
	 * GETTERS
	 */
	public float 	Experience	{ get { return heroStats.experience; } }
	public int 		Level		{ get { return heroStats.level; } }
	public string 	HeroName 	{ get { return heroStats.heroName; } }
	public int Strength			{ get { return Mathf.RoundToInt(heroStats.strength); } }

	/**
	 * SETTERS AND CHANGERS
	 */
	public void ChangeExperience(float delta)	{ heroStats.experience += delta; }
	public void ChangeHeroName(string newName)	{ heroStats.heroName = newName; }
	public void ChangeLevel(int delta)			{ heroStats.level += delta; }
	public void AddExperience(float delta)
	{
		heroStats.experience += delta;
		// Something about leveling up here
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
	}
}
