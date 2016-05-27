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
	public int		level = 1;
	public string	heroName; // class name, i.e. 'Ranger' 'Paladin'


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

	void Start()
	{
	}

	// Getters
	public float Experience { get { return heroStats.experience; } }
	public int Level { get { return heroStats.level; } }
	public string HeroName { get { return heroStats.heroName; } }
	public int Strength { get { return Mathf.RoundToInt(heroStats.strength); } }


	// Setters
	public void ChangeExperience(float delta) { heroStats.experience += delta; }

	public void ChangeHeroName(string newName) { heroStats.heroName = newName; }

	public void ChangeLevel(int delta) { heroStats.level += delta; }

	public void ChangeStrength(int delta)
	{
		ChangeMaxHealth(delta * GameplayConstants.HealthPerStrength, true);
		ChangeHealthRegeneration(delta * GameplayConstants.HealthRegenPerStrength);
		if (heroStats.primaryAttribute == AttributeTypes.Strength)
			ChangeDamage(delta * GameplayConstants.DamagePerPrimaryAttribute);
	}
	public void ChangeAgility(int delta)
	{
		ChangeAttackSpeedPercentage(delta * GameplayConstants.AttackSpeedPerAgility);
		ChangeArmor(delta * GameplayConstants.ArmorPerAgility);
		if (heroStats.primaryAttribute == AttributeTypes.Agility)
			ChangeDamage(delta * GameplayConstants.DamagePerPrimaryAttribute);
	}
	public void ChangeIntelligence(int delta)
	{
		ChangeMaxMana(delta * GameplayConstants.HealthPerStrength, true);
		ChangeManaRegeneration(delta * GameplayConstants.ManaRegenPerIntelligence);
		if (heroStats.primaryAttribute == AttributeTypes.Intelligence)
			ChangeDamage(delta * GameplayConstants.DamagePerPrimaryAttribute);
	}
}
