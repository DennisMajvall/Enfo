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
	public float	experience = 0;
	public float 	reqExperience = GameplayConstants.ExpRequiredAtLevelOne; // This says 0 in Inspector, but the Start() method defaults it to the GameplayConstant's value.
	public int 		level;
	public string 	heroClassName; // class name, i.e. 'Ranger' 'Paladin'
	public bool		isInWestTeam;
	
	//Attributes
	public AttributeTypes primaryAttribute;
	public float strengthPerLevel;
	public float agilityPerLevel;
	public float intelligencePerLevel;
	public float strength;
	public float agility;
	public float intelligence;

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
	public string 	HeroName 			{ get { return heroStats.heroClassName; } }
	public int 		Strength			{ get { return Mathf.RoundToInt(heroStats.strength); } }
	public bool		IsInWestTeam		{ get { return heroStats.isInWestTeam; } }

	/**
	 * SETTERS AND CHANGERS
	 */
	public void ChangeHeroName(string newName)	{ heroStats.heroClassName = newName; }
	public void AddLevel()			{ heroStats.level += 1; }
	public void SetRequiredExperience(float req){ heroStats.reqExperience = req; }
	public void AddExperience(float amount)
	{
		// Check for Faenella's Grace and Enfeeble here, and adjust amount accordingly

		// Add experience
		heroStats.experience += amount;

		if (Experience >= RequiredExperience) {
			// Level up
			while (Experience >= RequiredExperience) {
				AddLevel ();
				LevelUpAttributes ();
				heroStats.experience -= RequiredExperience;
				heroStats.reqExperience *= GameplayConstants.ExpRequiredIncreaseFactorPerLevel;
			}

			// Do a level up special effect here
		}
	}

	//Attributes
	public void LevelUpAttributes() {
		ChangeAgility (heroStats.agilityPerLevel);
		ChangeIntelligence (heroStats.intelligencePerLevel);
		ChangeStrength (heroStats.strengthPerLevel);
	}
	public void ChangeStrength(float delta)
	{
		heroStats.strength += delta;
		ChangeMaxHealth(delta * GameplayConstants.HealthPerStrength, true);
		IncreaseHealthRegeneration(delta * GameplayConstants.HealthRegenPerStrength);
		if (heroStats.primaryAttribute == AttributeTypes.Strength)
			IncreaseDamage(delta * GameplayConstants.DamagePerPrimaryAttribute);
	}
	public void ChangeAgility(float delta)
	{
		heroStats.agility += delta;
		ChangeAttackSpeedPercentage(delta * GameplayConstants.AttackSpeedPerAgility);
		IncreaseArmor(delta * GameplayConstants.ArmorPerAgility);
		if (heroStats.primaryAttribute == AttributeTypes.Agility)
			IncreaseDamage(delta * GameplayConstants.DamagePerPrimaryAttribute);
	}
	public void ChangeIntelligence(float delta)
	{
		heroStats.intelligence += delta;
		ChangeMaxMana(delta * GameplayConstants.ManaPerIntelligence, true);
		IncreaseManaRegeneration(delta * GameplayConstants.ManaRegenPerIntelligence);
		if (heroStats.primaryAttribute == AttributeTypes.Intelligence)
			IncreaseDamage(delta * GameplayConstants.DamagePerPrimaryAttribute);
	}

	/**
	 * Others
	 */
	void Start()
	{
		SetStartingAttributes();
		heroStats.reqExperience = GameplayConstants.ExpRequiredAtLevelOne;
		heroStats.level = 1;
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
