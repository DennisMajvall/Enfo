using UnityEngine;
using System.Collections;

[System.Serializable]
public class UnitStats
{
	// defence
	public float health;
	public float maxHealth;
	public float healthRegeneration;
	public float evasionChance;
	public float armor;
	public int 	 armorType; // what int corresponds to what armor type can be found in GameplayConstants.cs

	// offence
	public float damage;
	public int 	 attackType; // what int corresponds to what attack type can be found in GameplayConstants.cs
	public float projectileSpeed;
	public float range;
	public float acquisitionRange;
	public float critChance;
	public float critExtraMultiplier;

	// magic
	public float mana;
	public float maxMana;
	public float manaRegeneration;

	// utility
	public float movementSpeed;
	public float goldDropped;
	public float experienceDropped;
	public string name;




	[HideInInspector]
	public float movementSpeedPercentage = 1.0f;
}

public class UnitStatsComponent : MonoBehaviour
{

	[SerializeField] UnitStats unitStats;

	/**
	 * GETTERS
	 */
	// defence
	public float Health 					{ get { return unitStats.health; } }
	public float MaxHealth 					{ get { return unitStats.maxHealth; } }
	public float HealthRegeneration 		{ get { return unitStats.healthRegeneration; } }
	public float EvasionChance				{ get { return unitStats.evasionChance; } }
	public float Armor						{ get { return unitStats.armor; } }
	public int 	 ArmorType					{ get { return unitStats.armorType; } }

	// offence
	public float Damage 					{ get { return unitStats.damage; } }
	public int   AttackType 				{ get { return unitStats.attackType; } }
	public float ProjectileSpeed 			{ get { return unitStats.projectileSpeed; } }
	public float Range 						{ get { return unitStats.range; } }
	public float AcquisitionRange 			{ get { return unitStats.acquisitionRange; } }
	public float CritChance					{ get { return unitStats.critChance; } }
	public float CritExtraMultiplier		{ get { return unitStats.critExtraMultiplier; } }

	// magic
	public float Mana 						{ get { return unitStats.mana; } }
	public float MaxMana 					{ get { return unitStats.maxMana; } }
	public float ManaRegeneration 			{ get { return unitStats.manaRegeneration; } }

	// utility
	public float MovementSpeed				{ get { return unitStats.movementSpeed * unitStats.movementSpeedPercentage; } }
	public float MovementSpeedPercentage	{ get { return unitStats.movementSpeedPercentage; } }
	public float GoldDropped 				{ get { return unitStats.goldDropped; } }
	public float ExperienceDropped			{ get { return unitStats.experienceDropped; } }
	public string Name						{ get { return unitStats.name; } }

	// other
	public bool  IsDead 					{ get { return Health <= 0f; } }


	/**
	 * SETTERS AND MODIFIERS
	 */
	// defence
	public void DealDamage(float amount, UnitStats attackerStats)
	{
		// Check if the target evades the projectile and proceed if it doesn't
		if (!(EvasionChance > 0f && Random.value < EvasionChance)) {
			
			// Check if the thrower crits and increase damage if successful
			if (attackerStats.critChance > 0f && Random.value < attackerStats.critChance) {
				// Crit was successful
				amount *= (1 + attackerStats.critExtraMultiplier);
			}

			// Modify damage from target's armor and armor type, and attacker's attack type.
			amount *= GameplayConstants.ArmorDamageReduction (attackerStats.attackType, ArmorType, Armor);

			unitStats.health -= amount;

			if (IsDead) {
				// Give gold to killing player (always gives to Client as-of-now)
				GameObject client = GameObject.Find ("Client");
				client.GetComponent<GoldContainer> ().ChangeGold (gameObject.GetComponent<UnitStatsComponent> ().GoldDropped);

				// Give experience to the killing team (always gives to west team as-of-now)
				Teams team = GameObject.Find ("Globals").GetComponent<Teams>();
				int teamMembers = team.countTeam(true);
				foreach (GameObject hero in team.WestTeam) {
					if (hero)
						hero.GetComponent<HeroStatsComponent> ().AddExperience (ExperienceDropped / teamMembers);
				}

				// and destroy
				Destroy (gameObject);
			} else if (Health > MaxHealth) {
				unitStats.health = unitStats.maxHealth;
			}
		}
	}

	void changeHealth(float delta) {
		if (Health + delta > MaxHealth) {
			unitStats.health = MaxHealth;
		} else {
			unitStats.health += delta;
		}
	}

	public void IncreaseHealthRegeneration(float amount) 	{ unitStats.healthRegeneration += amount; }
	public void IncreaseEvasionChance(float amount)			{ unitStats.evasionChance += amount; }

	// offence
	public void IncreaseCritChance(float amount)			{ unitStats.critChance += amount; }
	public void IncreaseCritExtraMultiplier(float amount)	{ unitStats.critExtraMultiplier += amount; }

	// magic
	public void ChangeMana(float delta)
	{
		unitStats.mana += delta;

		if (Mana <= 0f)
			unitStats.mana = 0f;
		else if (Mana > MaxMana)
			unitStats.mana = unitStats.maxMana;
	}
		
	// utility
	public void ChangeMovementSpeedPercentage(float delta)	{ unitStats.movementSpeedPercentage += delta; }

	/**
	 * UPDATE()
	 */
	void Update()
	{
		if (HealthRegeneration != 0f) {
			float amount = HealthRegeneration * Time.deltaTime;

			if (Health + amount > MaxHealth) {
				unitStats.health = MaxHealth;
			} else {
				unitStats.health += amount;
			}

		}

		if (ManaRegeneration != 0f)
			ChangeMana(ManaRegeneration * Time.deltaTime);
	}





}
