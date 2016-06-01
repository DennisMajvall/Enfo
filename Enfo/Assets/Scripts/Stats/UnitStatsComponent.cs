using UnityEngine;
using System.Collections;

[System.Serializable]
public class UnitStats
{
	public string[] abilitiesHero;			// Needed for the Wc3 porting
	public string[] abilitiesNormal;		// Needed for the Wc3 porting
	public float selectionScale = 1.7f;		// Needed for the Wc3 porting

	// defence
	public float 			health;
	public float 			maxHealth;
	public float 			healthRegeneration = 0.5f;
	public float 			evasionChance;
	public float 			armor;
	public ArmorTypeEnum	armorType = ArmorTypeEnum.Normal;
	public bool				invulnerable;
	public float			lifeStealPercentage;

	// offence
	public float 			attackCooldownTime;
	public float 			damage = 2f;
	public float 			damageFactorMedium;					// Needed for the Wc3 porting
	public float 			damageFactorSmall;                  // Needed for the Wc3 porting
	public float 			damageNumDice = 1f;					// Needed for the Wc3 porting
	public float 			damageSidesPerDice = 8f;            // Needed for the Wc3 porting
	public AttackTypeEnum 	attackType = AttackTypeEnum.Hero;
	public float 			projectileSpeed = 900f;
	public float 			range = 180f;
	public float 			acquisitionRange = 400f;
	public float 			critChance;
	public float 			critExtraMultiplier;
	public bool 			projectileHomingEnabled;

	// magic
	public float 			mana = 30f;
	public float 			maxMana = 30f;
	public float 			manaRegeneration = 0.02f;

	// utility
	public float 			movementSpeed = 350f;
	public float 			turnRate = 0.6f;
	public float 			goldDropped;
	public float 			goldBountyAwardedBase;					// Needed for the Wc3 porting
	public float 			goldBountyAwardedNumbersPerDice	= 1f;	// Needed for the Wc3 porting
	public float 			goldBountyAwardedSidesPerDice;			// Needed for the Wc3 porting
	public string 			name = "ProperName/TextName";
	public int 				monsterLevel;


	[HideInInspector] public float movementSpeedPercentage = 1.0f;
	[HideInInspector] public float attackSpeedPercentage = 1.0f;
}

public class UnitStatsComponent : MonoBehaviour
{

	[SerializeField]
	UnitStats unitStats = new UnitStats();

	/**
	 * GETTERS
	 */
	// defence
	public float 			Health 				{ get { return unitStats.health; } }
	public float 			MaxHealth 			{ get { return unitStats.maxHealth; } }
	public float 			HealthRegeneration	{ get { return unitStats.healthRegeneration; } }
	public float 			EvasionChance 		{ get { return unitStats.evasionChance; } }
	public float 			Armor 				{ get { return unitStats.armor; } }
	public ArmorTypeEnum 	ArmorType 			{ get { return unitStats.armorType; } }
	public bool				Invulnerable		{ get { return unitStats.invulnerable; } }

	// offence
	public float 			Damage 				{ get { return unitStats.damage; } }
	public AttackTypeEnum	AttackType			{ get { return unitStats.attackType; } }
	public float 			ProjectileSpeed 	{ get { return unitStats.projectileSpeed; } }
	public float 			Range 				{ get { return unitStats.range; } }
	public float 			AcquisitionRange 	{ get { return unitStats.acquisitionRange; } }
	public float 			CritChance 			{ get { return unitStats.critChance; } }
	public float 			CritExtraMultiplier	{ get { return unitStats.critExtraMultiplier; } }

	// magic
	public float 			Mana 				{ get { return unitStats.mana; } }
	public float 			MaxMana 			{ get { return unitStats.maxMana; } }
	public float 			ManaRegeneration	{ get { return unitStats.manaRegeneration; } }

	// utility
	public float 			MovementSpeed 		{ get { return unitStats.movementSpeed * unitStats.movementSpeedPercentage; } }
	public float 			GoldDropped 		{ get { return unitStats.goldDropped; } }
	public string 			Name 				{ get { return unitStats.name; } }
	public int 				MonsterLevel		{ get { return unitStats.monsterLevel; } }

	// other
	public bool 			IsDead 				{ get { return Health <= 0f; } }


	/**
	 * SETTERS AND CHANGERS
	 */
	// defence
	public void DealDamage(float amount, UnitStats attackerStats, UnitStatsComponent attackerStatsComponent)
	{
		if (Invulnerable)
			return;

		// Check if the target evades the projectile and proceed if it doesn't
		if (!(EvasionChance > 0f && Random.value < EvasionChance)) {

			// Check if the thrower crits and increase damage if successful
			if (attackerStats.critChance > 0f && Random.value < attackerStats.critChance) {
				// Crit was successful
				amount *= (1 + attackerStats.critExtraMultiplier);
			}

			// Modify damage from target's armor and armor type, and attacker's attack type.
			amount *= GameplayConstants.ArmorDamageReduction(attackerStats.attackType, ArmorType, Armor);

			unitStats.health -= amount;

			if (attackerStatsComponent) {
				attackerStatsComponent.ApplyOnAttackEffects(amount);
			}

			if (IsDead) {
				// Give gold to killing player (always gives to Client as-of-now)
				GameObject client = GameObject.Find("Client");
				client.GetComponent<GoldContainer>().ChangeGold(gameObject.GetComponent<UnitStatsComponent>().GoldDropped);

				// Give experience to the killing team (always gives to west team as-of-now)
				Teams team = Globals.Teams;
				int teamMembers = team.CountTeam(true);
				foreach (GameObject hero in team.WestTeam) {
					if (hero) {
						// Divide experience between all Heroes on the killing team, calculated from a base value factored by the killed unit's level
						float experience = GameplayConstants.MonsterLevelOneExpDrop * Mathf.Pow(GameplayConstants.MonsterExpDropIncreaseFactorPerLevel, MonsterLevel) / teamMembers;
						hero.GetComponent<HeroStatsComponent>().AddExperience(experience);
					}
				}

				// and destroy
				Destroy(gameObject);
			} else if (Health > MaxHealth) {
				unitStats.health = unitStats.maxHealth;
			}
		}
	}
	public void IncreaseHealth(float delta)
	{
		unitStats.health += delta;

		if (Health > MaxHealth) {
			unitStats.health = unitStats.maxHealth;
		}
	}
	public void ChangeMaxHealth(float delta, bool alsoChangeCurrentHealth)
	{
		float oldMaxHealth = MaxHealth;
		unitStats.maxHealth += delta;

		if (alsoChangeCurrentHealth) {
			float relativeHealth = Health / (Mathf.Max(oldMaxHealth, 1));
			IncreaseHealth(relativeHealth * delta);
		}
	}

	public void ChangeArmor(float delta)				{ unitStats.armor += delta; }
	public void IncreaseHealthRegeneration(float delta) { unitStats.healthRegeneration += delta; }
	public void IncreaseEvasionChance(float amount)		{ unitStats.evasionChance += amount; }
	public void SetInvulnerable(bool value)				{ unitStats.invulnerable = value; }
	public void ChangeLifeStealPercentage(float delta)	{ unitStats.lifeStealPercentage += delta; }
	
	// offence
	public void ChangeDamage(float delta) { unitStats.damage += delta; }
	public void ChangeAttackSpeedPercentage(float delta) { unitStats.attackSpeedPercentage += delta; }
	public void IncreaseCritChance(float amount) { unitStats.critChance += amount; }
	public void IncreaseCritExtraMultiplier(float amount) { unitStats.critExtraMultiplier += amount; }

	// magic
	public void ChangeMana(float delta)
	{
		unitStats.mana += delta;

		if (Mana <= 0f)
			unitStats.mana = 0f;
		else if (Mana > MaxMana)
			unitStats.mana = unitStats.maxMana;
	}
	public void ChangeMaxMana(float delta, bool alsoChangeCurrentMana)
	{
		float oldMaxMana = Mana;
		unitStats.maxMana += delta;

		if (alsoChangeCurrentMana) {
			float relativeMana = (Mathf.Max(Mana, 1)) / (Mathf.Max(oldMaxMana, 1));
			ChangeMana(relativeMana * delta);
		}
	}

	public void IncreaseManaRegeneration(float delta) { unitStats.manaRegeneration += delta; }

	// utility
	public void ChangeMovementSpeedPercentage(float delta) { unitStats.movementSpeedPercentage += delta; }

	/**
	 * UPDATE()
	 */
	void Update()
	{
		if (HealthRegeneration > 0f)
			IncreaseHealth(HealthRegeneration * Time.deltaTime);

		if (ManaRegeneration > 0f)
			ChangeMana(ManaRegeneration * Time.deltaTime);
	}

	// Apply Life-steal etc
	public void ApplyOnAttackEffects(float damageAmount)
	{
		if (gameObject && !IsDead && unitStats.lifeStealPercentage > 0f) {
			float lifeStealAmount = damageAmount * unitStats.lifeStealPercentage;
			IncreaseHealth(lifeStealAmount);
		}
	}
}
