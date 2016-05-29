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
		ChangeMaxHealth(delta * GameplayConstants.HEALTH_PER_STRENGTH, true);
		IncreaseHealthRegeneration(delta * GameplayConstants.HEALTH_REGEN_PER_STRENGTH);
		if (heroStats.primaryAttribute == AttributeTypes.Strength)
			ChangeDamage(delta * GameplayConstants.DAMAGE_PER_PRIMARY_ATTRIBUTE);
	}
	public void ChangeAgility(float delta)
	{
		heroStats.agility += delta;
		ChangeAttackSpeedPercentage(delta * GameplayConstants.ATTACK_SPEED_PER_AGILITY);
		ChangeArmor(delta * GameplayConstants.ARMOR_PER_AGILITY);
		if (heroStats.primaryAttribute == AttributeTypes.Agility)
			ChangeDamage(delta * GameplayConstants.DAMAGE_PER_PRIMARY_ATTRIBUTE);
	}
	public void ChangeIntelligence(float delta)
	{
		heroStats.intelligence += delta;
		ChangeMaxMana(delta * GameplayConstants.MANA_PER_INTELLIGENCE, true);
		IncreaseManaRegeneration(delta * GameplayConstants.MANA_REGEN_PER_INTELLIGENCE);
		if (heroStats.primaryAttribute == AttributeTypes.Intelligence)
			ChangeDamage(delta * GameplayConstants.DAMAGE_PER_PRIMARY_ATTRIBUTE);
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
